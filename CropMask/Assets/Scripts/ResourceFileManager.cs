

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public  class ResourceFileManager : MonoBehaviour {


    
    public MiniJsonObject dmo;

    public int totalDownloadableFile = -999;
    public int totalFileDownloaded = 0;
    public float downloadPercent = 0.0f;
    public int downloadStarted = 0;

    public int fileChecked=0;

    public Text downloadInfoUIText;
    [HideInInspector]
    public string downloadInfoString = "Downloading : {0} %\n[{1}/{2}]";

    [HideInInspector]
    public string imageUrlFormat = "http://demowebz.cu.cc/venusfashion/img/item_image/{0}";

    public Dictionary<string, string> dataPathDict;
    public string resourceDataPath;

    public int maxTotalCoroutine = 8;
    public int currentTotalCoroutine = 0;


    public bool standAloneDownloader = false;

    public GameObject popUp;
    public GameObject canvasObject;

    public GameObject downloadInfoPanel;
    public GameObject checkForUpdatePanel;
    public GameObject retryButton;

    public object lockObjectFileChecked=new object();
    public object lockObjectFileDownloaded = new object();
    public object lockObjectCurrentTotalCoroutine = new object();

    public int totalFemaleData;
    public int currentFemaleData = 0;
    public int totalMaleData;
    public int currentMaleData = 0;

    public int isNewInstall;


    public string firstInstallUrl;
    public string testUrl;
    public string defaultUrl;

    private void Awake()
    {

        firstInstallUrl = "http://demowebz.cu.cc/venusfashion/api/Headings/firstTime";
        testUrl = "http://demowebz.cu.cc/venusfashion/api/Headings/test";
        defaultUrl = "http://demowebz.cu.cc/venusfashion/api/Headings";
        resourceDataPath = Application.persistentDataPath;
        if (Application.platform != RuntimePlatform.OSXPlayer)
        {
            resourceDataPath += "/resources/images";
        }
        dataPathDict = new Dictionary<string, string>();

        
        dataPathDict["dressFemaleDataPath"] = Path.Combine(resourceDataPath, "dress");
        dataPathDict["wigFemaleDataPath"] = Path.Combine(resourceDataPath, "wig");
        dataPathDict["wigMaleDataPAth"] =dataPathDict["wigFemaleDataPath"];
        dataPathDict["wigFemaleDataPath"] = Path.Combine(dataPathDict["wigFemaleDataPath"], "female");
        dataPathDict["wigMaleDataPAth"] = Path.Combine(dataPathDict["wigMaleDataPAth"], "male");
        dataPathDict["ornamentFemaleDataPath"] = Path.Combine(resourceDataPath, "ornament");
        dataPathDict["shoeFemaleDataPath"] = Path.Combine(resourceDataPath, "shoe");
        dataPathDict["tieMaleDataPath"] = Path.Combine(resourceDataPath, "tie");

        dataPathDict["resourceDataPath"] = resourceDataPath;


        totalDownloadableFile = -999;
        totalFileDownloaded = 0;
        downloadPercent = 0.0f;
        imageUrlFormat = "http://demowebz.cu.cc/venusfashion/img/item_image/{0}";
    downloadInfoString = "Downloading : {0} %\n[{2}/{1}]";

        isNewInstall = PlayerPrefs.GetInt("isNewInstall", 1);
    }
    // Use this for initialization
    void Start () {
        totalDownloadableFile = -999;
        totalFileDownloaded = 0;
        downloadPercent = 0.0f;
        imageUrlFormat = "http://demowebz.cu.cc/venusfashion/img/item_image/{0}";
        downloadInfoString = "Downloading : {0} %\n[{2}/{1}]";
        dmo = new MiniJsonObject();
        if(standAloneDownloader)
        {
            StartCoroutine(CreateDirectories());
            StartCoroutine(DownloadAllFile0());
           
        }
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    
    private void FixedUpdate()
    {
        if(fileChecked==totalDownloadableFile)
        {
            StartCoroutine(LoadMainScene());
        }
    }
    
    public void RetryDownload()
    {
        StartCoroutine(DownloadAllFile0());
        retryButton.SetActive(false);
    }

    


#region ZERO
    public IEnumerator DownloadAllFile0()
    {
        WWWForm form = new WWWForm();
        string device_id;
        string device_type;
#if UNITY_ANDROID
        device_type = "A";
        device_id = SystemInfo.deviceUniqueIdentifier;

#elif UNITY_IPHONE
        device_type="I";
        device_id = SystemInfo.deviceUniqueIdentifier;
#elif UNITY_EDITOR
        device_type="A"
        device_id = SystemInfo.deviceUniqueIdentifier;
#endif
        form.AddField("device_id", device_id);
        form.AddField("device_type", device_type);



        if (isNewInstall == 1)
        {
            using (UnityWebRequest www = UnityWebRequest.Post(firstInstallUrl, form))
            {
                //print(www.url);
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    InstantiateInfoPopup("No Internet Connection");
                    retryButton.SetActive(true);
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log("Form upload complete!");

                    string jsonString = www.downloadHandler.text;
                    //print(jsonString);




                    dmo = new MiniJsonObject(jsonString);

                    int error_code = dmo.GetField("error_code", -1);
                    print(error_code);
                    if (error_code == 0)
                    {
                        MiniJsonObject rData = dmo.GetJsonObject("data");
                        MiniJsonArray malesArrayData = rData.GetJsonArray("males");
                        MiniJsonArray femalesArrayData = rData.GetJsonArray("females");
                        totalDownloadableFile = malesArrayData.Count + femalesArrayData.Count;
                        downloadInfoUIText.text = string.Format(downloadInfoString, downloadPercent, totalDownloadableFile, totalFileDownloaded);
                        print(string.Format("total male image : {0}   total Female Image : {1}", malesArrayData.Count, femalesArrayData.Count));

                        if (femalesArrayData.Count > 0)
                        {
                            checkForUpdatePanel.SetActive(false);
                            downloadInfoPanel.SetActive(true);
                            yield return new WaitForFixedUpdate();
                            StartCoroutine(DownloadFemalesData0(femalesArrayData));
                        }
                        if (malesArrayData.Count > 0)
                        {
                            checkForUpdatePanel.SetActive(false);
                            downloadInfoPanel.SetActive(true);
                            yield return new WaitForFixedUpdate();
                            StartCoroutine(DownloadMalesData0(malesArrayData));
                        }

                    }



                }
                www.Dispose();
            }
        }

        else
        {

#if !UNITY_EDITOR
        using (UnityWebRequest www = UnityWebRequest.Post(defaultUrl, form))  
        //using (UnityWebRequest www = UnityWebRequest.Post(testUrl, form))
#else
            using (UnityWebRequest www = UnityWebRequest.Post(defaultUrl, form))
#endif
            {
                //print(www.url);
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    InstantiateInfoPopup("No Internet Connection");
                    retryButton.SetActive(true);
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log("Form upload complete!");

                    string jsonString = www.downloadHandler.text;
                    //print(jsonString);




                    dmo = new MiniJsonObject(jsonString);

                    int error_code = dmo.GetField("error_code", -1);
                    print(error_code);
                    if (error_code == 0)
                    {
                        MiniJsonObject rData = dmo.GetJsonObject("data");
                        MiniJsonArray malesArrayData = rData.GetJsonArray("males");
                        MiniJsonArray femalesArrayData = rData.GetJsonArray("females");
                        totalDownloadableFile = malesArrayData.Count + femalesArrayData.Count;
                        downloadInfoUIText.text = string.Format(downloadInfoString, downloadPercent, totalDownloadableFile, totalFileDownloaded);
                        print(string.Format("total male image : {0}   total Female Image : {1}", malesArrayData.Count, femalesArrayData.Count));
                        if(totalDownloadableFile==0)
                        {

                        }
                        if (femalesArrayData.Count > 0)
                        {
                            checkForUpdatePanel.SetActive(false);
                            downloadInfoPanel.SetActive(true);
                            yield return new WaitForFixedUpdate();
                            StartCoroutine(DownloadFemalesData0(femalesArrayData));
                        }
                        if (malesArrayData.Count > 0)
                        {
                            checkForUpdatePanel.SetActive(false);
                            downloadInfoPanel.SetActive(true);
                            yield return new WaitForFixedUpdate();
                            StartCoroutine(DownloadMalesData0(malesArrayData));
                        }


                    }



                }
                //www.Dispose();
            }


        }

    }


    public IEnumerator DownloadMalesData0(MiniJsonArray ma)  // download males data
    {
        totalMaleData =ma.Count;
        currentMaleData = 0;

        int j = 0;
        yield return new WaitForEndOfFrame();
        while ((currentMaleData < totalMaleData))
        {
            if (currentTotalCoroutine < maxTotalCoroutine)
            {
                MiniJsonObject m = ma.Get(j);
                StartCoroutine(DownloadSingleData0(m));
                j += 1;
                currentMaleData += 1;
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }



    public IEnumerator DownloadFemalesData0(MiniJsonArray fa)  // download females data
    {
        
        totalFemaleData = fa.Count;
        currentFemaleData = 0;

        int j = 0;
        yield return new WaitForEndOfFrame();
        while((currentFemaleData < totalFemaleData))
        {
            //while ((currentFemaleData < totalFemaleData)&&(currentTotalCoroutine < maxTotalCoroutine))
            //{
            //    MiniJsonObject m = fa.Get(j);
            //    StartCoroutine(DownloadSingleData0(m));
            //    j+=1;
            //    currentFemaleData += 1;
            //    yield return new WaitForEndOfFrame();
            //}
            if ( (currentTotalCoroutine < maxTotalCoroutine))
            {
                currentFemaleData += 1;
                MiniJsonObject m = fa.Get(j);
                StartCoroutine(DownloadSingleData0(m));
                j += 1;

                //yield return new WaitForEndOfFrame();
                yield return null;
            }
            //yield return new WaitForFixedUpdate();
            yield return null;
        }
        yield return null;
    }






    public IEnumerator DownloadSingleData0(MiniJsonObject mo)   //download a single minijson object
    {
            currentTotalCoroutine += 1;
        
        MiniJsonObject typeObject = mo.GetJsonObject("type");
        int wearingCode = typeObject.GetField("id", -1);
        int mfType = typeObject.GetField("dress_type", -1);
        string imgName = mo.GetField("icon", "");

        

        switch (wearingCode)
        {
            case 1: //download female dress
                {
                    if (imgName != "")
                    {
                        string finalImageUrl = string.Format(imageUrlFormat, imgName);
                        string finalSavePath = Path.Combine(dataPathDict["dressFemaleDataPath"], imgName);
                        //print(string.Format("downloading : {0} to {1}", finalImageUrl, finalSavePath));

                        StartCoroutine(DownloadImage0(finalImageUrl, finalSavePath));

                        //UpdateDownloadInfo();
                    }
                    break;
                }
            case 2:  //download female wig
                {
                    if (imgName != "")
                    {
                        string finalImageUrl = string.Format(imageUrlFormat, imgName);
                        string finalSavePath = Path.Combine(dataPathDict["wigFemaleDataPath"], imgName);
                        //print(string.Format("downloading : {0} to {1}", finalImageUrl, finalSavePath));

                        StartCoroutine(DownloadImage0(finalImageUrl, finalSavePath));

                        //UpdateDownloadInfo();
                    }
                    break;
                }
            case 3:  //download female ornament
                {
                    if (imgName != "")
                    {
                        string finalImageUrl = string.Format(imageUrlFormat, imgName);
                        string finalSavePath = Path.Combine(dataPathDict["ornamentFemaleDataPath"], imgName);
                        //print(string.Format("downloading : {0} to {1}", finalImageUrl, finalSavePath));

                        StartCoroutine(DownloadImage0(finalImageUrl, finalSavePath));

                        //UpdateDownloadInfo();
                    }
                    break;
                }
            case 4:  //download female shoe
                {
                    if (imgName != "")
                    {
                        string finalImageUrl = string.Format(imageUrlFormat, imgName);
                        string finalSavePath = Path.Combine(dataPathDict["shoeFemaleDataPath"], imgName);
                        //print(string.Format("downloading : {0} to {1}", finalImageUrl, finalSavePath));

                        StartCoroutine(DownloadImage0(finalImageUrl, finalSavePath));

                        //UpdateDownloadInfo();
                    }
                    break;
                }
            case 5: //download male wig
                {
                    if (imgName != "")
                    {
                        string finalImageUrl = string.Format(imageUrlFormat, imgName);
                        string finalSavePath = Path.Combine(dataPathDict["wigMaleDataPAth"], imgName);
                        //print(string.Format("downloading : {0} to {1}", finalImageUrl, finalSavePath));

                        StartCoroutine(DownloadImage0(finalImageUrl, finalSavePath));

                        //UpdateDownloadInfo();
                    }
                    break;
                }
            case 6: // download male tie
                {
                    if (imgName != "")
                    {
                        string finalImageUrl = string.Format(imageUrlFormat, imgName);
                        string finalSavePath = Path.Combine(dataPathDict["tieMaleDataPath"], imgName);
                        //print(string.Format("downloading : {0} to {1}", finalImageUrl, finalSavePath));

                        StartCoroutine(DownloadImage0(finalImageUrl, finalSavePath));

                        //UpdateDownloadInfo();
                    }
                    break;
                }
            default:
                {
                    break;
                }
        }


        yield return null;
    }



    public IEnumerator DownloadImage0(string imageUrl, string savePath)  //download and saves single image
    {
        
        //print("Starting Download");
        downloadStarted += 1;
        bool shouldDownloadFile = !File.Exists(savePath);
        if(shouldDownloadFile)
        {
            using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl))
            {
                //print(www.url);
                print("Request sent waiting for response");
                yield return www.SendWebRequest();
                print("Got response");
                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Texture2D t2d = ((DownloadHandlerTexture)www.downloadHandler).texture as Texture2D;
                    t2d.Apply();
                    print("downloaded .. writing data");
                    File.WriteAllBytes(savePath, t2d.EncodeToPNG());
                    print("Writing data completed");
                    yield return new WaitForFixedUpdate();
                    DestroyImmediate(t2d, true);
                    //DestroyImmediate(t2d);
                    UpdateDownloadInfoStatic();

                }

                www.Dispose();
                
                    fileChecked += 1;
                

                //print("Download Completed");

            }
        }
        else
        {
            UpdateDownloadInfoStatic();
            
                fileChecked += 1;
            
        }
        yield return null;
        //lock (lockObjectCurrentTotalCoroutine)
        //{
            currentTotalCoroutine -= 1;
            if(currentTotalCoroutine<0)
            {
                currentTotalCoroutine = 0;
            }
        //}
        yield return null;
    }




    #endregion ZERO




    #region GOOD


    public IEnumerator DownloadAllFile()
    {
        WWWForm form = new WWWForm();
        string device_id;
        string device_type;
#if UNITY_ANDROID
        device_type = "A";
        device_id = SystemInfo.deviceUniqueIdentifier;

#elif UNITY_IPHONe
        device_type="I";
        device_id = SystemInfo.deviceUniqueIdentifier;
#elif UNITY_EDITOR
        device_type="A";
        device_id = SystemInfo.deviceUniqueIdentifier;
#endif
        form.AddField("device_id", device_id);
        form.AddField("device_type", device_type);

#if !UNITY_EDITOR
        using (UnityWebRequest www = UnityWebRequest.Post("http://demowebz.cu.cc/venusfashion/api/Headings", form))  
#else
        using (UnityWebRequest www = UnityWebRequest.Post("http://demowebz.cu.cc/venusfashion/api/Headings/test", form)) // debug
#endif
        {
            //print(www.url);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                InstantiateInfoPopup("No Internet Connection");
                retryButton.SetActive(true);
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");

                string jsonString = www.downloadHandler.text;
                //print(jsonString);




                dmo = new MiniJsonObject(jsonString);

                int error_code = dmo.GetField("error_code", -1);
                print(error_code);
                if (error_code == 0)
                {
                    MiniJsonObject rData = dmo.GetJsonObject("data");
                    MiniJsonArray malesArrayData = rData.GetJsonArray("males");
                    MiniJsonArray femalesArrayData = rData.GetJsonArray("females");
                    totalDownloadableFile = malesArrayData.Count + femalesArrayData.Count;
                    downloadInfoUIText.text = string.Format(downloadInfoString, downloadPercent, totalDownloadableFile, totalFileDownloaded);
                    print(string.Format("total male image : {0}   total Female Image : {1}", malesArrayData.Count, femalesArrayData.Count));

                    if (femalesArrayData.Count > 0)
                    {
                        StartCoroutine(DownloadFemalesData(femalesArrayData));
                    }
                    if (malesArrayData.Count > 0)
                    {
                        StartCoroutine(DownloadMalesData(malesArrayData));
                    }

                }



            }
            www.Dispose();
        }


    }

    public void UpdateDownloadInfoStatic()
    {

        totalFileDownloaded += 1;
        downloadPercent = (100 * totalFileDownloaded) / totalDownloadableFile;
        downloadInfoUIText.text = string.Format(downloadInfoString, downloadPercent, totalDownloadableFile, totalFileDownloaded);


    }



    public IEnumerator DownloadAllFile2()
    {
        WWWForm form = new WWWForm();
        string device_id;
        string device_type;
#if UNITY_ANDROID
        device_type = "A";
        device_id = SystemInfo.deviceUniqueIdentifier;

#elif UNITY_IPHONe
        device_type="I";
        device_id = SystemInfo.deviceUniqueIdentifier;
#elif UNITY_EDITOR
        device_type="A";
        device_id = SystemInfo.deviceUniqueIdentifier;
#endif
        form.AddField("device_id", device_id);
        form.AddField("device_type", device_type);
#if !UNITY_EDITOR
        using (UnityWebRequest www = UnityWebRequest.Post("http://demowebz.cu.cc/venusfashion/api/Headings", form))  
#else
        using (UnityWebRequest www = UnityWebRequest.Post("http://demowebz.cu.cc/venusfashion/api/Headings/test", form)) // debug
#endif
        {
            //print(www.url);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                InstantiateInfoPopup("No Internet Connection");
                retryButton.SetActive(true);
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");

                string jsonString = www.downloadHandler.text;
                //print(jsonString);




                dmo = new MiniJsonObject(jsonString);

                int error_code = dmo.GetField("error_code", -1);
                print(error_code);
                if (error_code == 0)
                {
                    MiniJsonObject rData = dmo.GetJsonObject("data");
                    MiniJsonArray malesArrayData = rData.GetJsonArray("males");
                    MiniJsonArray femalesArrayData = rData.GetJsonArray("females");
                    totalDownloadableFile = malesArrayData.Count + femalesArrayData.Count;
                    downloadInfoUIText.text = string.Format(downloadInfoString, downloadPercent, totalDownloadableFile, totalFileDownloaded);
                    print(string.Format("total male image : {0}   total Female Image : {1}", malesArrayData.Count, femalesArrayData.Count));

                    if (femalesArrayData.Count > 0)
                    {
                        for (int i = 0; i < femalesArrayData.Count; i++)
                        {
                            MiniJsonObject mo = femalesArrayData.Get(i);

                            MiniJsonObject typeObject = mo.GetJsonObject("type");
                            int wearingCode = typeObject.GetField("id", -1);
                            int mfType = typeObject.GetField("dress_type", -1);
                            string imgName = mo.GetField("icon", "");


                            switch (wearingCode)
                            {
                                case 1: //download female dress
                                    {
                                        if (imgName != "")
                                        {
                                            string finalImageUrl = string.Format(imageUrlFormat, imgName);
                                            string finalSavePath = Path.Combine(dataPathDict["dressFemaleDataPath"], imgName);
                                            //print(string.Format("downloading : {0} to {1}", finalImageUrl, finalSavePath));
                                            {
                                                using (UnityWebRequest www2 = UnityWebRequestTexture.GetTexture(finalImageUrl))
                                                {
                                                    //print(www.url);
                                                    yield return www2.SendWebRequest();

                                                    if (www2.isNetworkError || www2.isHttpError)
                                                    {
                                                        Debug.Log(www2.error);
                                                    }
                                                    else
                                                    {
                                                        Texture2D t2d = ((DownloadHandlerTexture)www2.downloadHandler).texture as Texture2D;
                                                        t2d.Apply();

                                                        File.WriteAllBytes(finalSavePath, t2d.EncodeToPNG());
                                                        DestroyImmediate(t2d, true);
                                                        UpdateDownloadInfoStatic();
                                                    }
                                                    www2.Dispose();

                                                    fileChecked += 1;


                                                }
                                            }  //bolck download image

                                            //UpdateDownloadInfo();
                                        }
                                        break;
                                    }
                                case 2:  //download female wig
                                    {
                                        if (imgName != "")
                                        {
                                            string finalImageUrl = string.Format(imageUrlFormat, imgName);
                                            string finalSavePath = Path.Combine(dataPathDict["wigFemaleDataPath"], imgName);
                                            //print(string.Format("downloading : {0} to {1}", finalImageUrl, finalSavePath));

                                            {
                                                using (UnityWebRequest www2 = UnityWebRequestTexture.GetTexture(finalImageUrl))
                                                {
                                                    //print(www.url);
                                                    yield return www2.SendWebRequest();

                                                    if (www2.isNetworkError || www2.isHttpError)
                                                    {
                                                        Debug.Log(www2.error);
                                                    }
                                                    else
                                                    {
                                                        Texture2D t2d = ((DownloadHandlerTexture)www2.downloadHandler).texture as Texture2D;
                                                        t2d.Apply();

                                                        File.WriteAllBytes(finalSavePath, t2d.EncodeToPNG());
                                                        DestroyImmediate(t2d, true);
                                                        UpdateDownloadInfoStatic();
                                                    }
                                                    www2.Dispose();

                                                    fileChecked += 1;


                                                }
                                            }  //bolck download image

                                            //UpdateDownloadInfo();
                                        }
                                        break;
                                    }
                                case 3:  //download female ornament
                                    {
                                        if (imgName != "")
                                        {
                                            string finalImageUrl = string.Format(imageUrlFormat, imgName);
                                            string finalSavePath = Path.Combine(dataPathDict["ornamentFemaleDataPath"], imgName);
                                            //print(string.Format("downloading : {0} to {1}", finalImageUrl, finalSavePath));

                                            {
                                                using (UnityWebRequest www2 = UnityWebRequestTexture.GetTexture(finalImageUrl))
                                                {
                                                    //print(www.url);
                                                    yield return www2.SendWebRequest();

                                                    if (www2.isNetworkError || www2.isHttpError)
                                                    {
                                                        Debug.Log(www2.error);
                                                    }
                                                    else
                                                    {
                                                        Texture2D t2d = ((DownloadHandlerTexture)www2.downloadHandler).texture as Texture2D;
                                                        t2d.Apply();

                                                        File.WriteAllBytes(finalSavePath, t2d.EncodeToPNG());
                                                        DestroyImmediate(t2d, true);
                                                        UpdateDownloadInfoStatic();
                                                    }
                                                    www2.Dispose();

                                                    fileChecked += 1;


                                                }
                                            }  //bolck download image

                                            //UpdateDownloadInfo();
                                        }
                                        break;
                                    }
                                case 4:  //download female shoe
                                    {
                                        if (imgName != "")
                                        {
                                            string finalImageUrl = string.Format(imageUrlFormat, imgName);
                                            string finalSavePath = Path.Combine(dataPathDict["shoeFemaleDataPath"], imgName);
                                            //print(string.Format("downloading : {0} to {1}", finalImageUrl, finalSavePath));

                                            {
                                                using (UnityWebRequest www2 = UnityWebRequestTexture.GetTexture(finalImageUrl))
                                                {
                                                    //print(www.url);
                                                    yield return www2.SendWebRequest();

                                                    if (www2.isNetworkError || www2.isHttpError)
                                                    {
                                                        Debug.Log(www2.error);
                                                    }
                                                    else
                                                    {
                                                        Texture2D t2d = ((DownloadHandlerTexture)www2.downloadHandler).texture as Texture2D;
                                                        t2d.Apply();

                                                        File.WriteAllBytes(finalSavePath, t2d.EncodeToPNG());
                                                        DestroyImmediate(t2d, true);
                                                        UpdateDownloadInfoStatic();
                                                    }
                                                    www2.Dispose();

                                                    fileChecked += 1;


                                                }
                                            }  //bolck download image

                                            //UpdateDownloadInfo();
                                        }
                                        break;
                                    }
                                case 5: //download male wig
                                    {
                                        if (imgName != "")
                                        {
                                            string finalImageUrl = string.Format(imageUrlFormat, imgName);
                                            string finalSavePath = Path.Combine(dataPathDict["wigMaleDataPAth"], imgName);
                                            //print(string.Format("downloading : {0} to {1}", finalImageUrl, finalSavePath));

                                            {
                                                using (UnityWebRequest www2 = UnityWebRequestTexture.GetTexture(finalImageUrl))
                                                {
                                                    //print(www.url);
                                                    yield return www2.SendWebRequest();

                                                    if (www2.isNetworkError || www2.isHttpError)
                                                    {
                                                        Debug.Log(www2.error);
                                                    }
                                                    else
                                                    {
                                                        Texture2D t2d = ((DownloadHandlerTexture)www2.downloadHandler).texture as Texture2D;
                                                        t2d.Apply();

                                                        File.WriteAllBytes(finalSavePath, t2d.EncodeToPNG());
                                                        DestroyImmediate(t2d, true);
                                                        UpdateDownloadInfoStatic();
                                                    }
                                                    www2.Dispose();

                                                    fileChecked += 1;


                                                }
                                            } //bolck download image

                                            //UpdateDownloadInfo();
                                        }
                                        break;
                                    }
                                case 6: //bolck download image
                                    {
                                        if (imgName != "")
                                        {
                                            string finalImageUrl = string.Format(imageUrlFormat, imgName);
                                            string finalSavePath = Path.Combine(dataPathDict["tieMaleDataPath"], imgName);
                                            //print(string.Format("downloading : {0} to {1}", finalImageUrl, finalSavePath));

                                            {
                                                using (UnityWebRequest www2 = UnityWebRequestTexture.GetTexture(finalImageUrl))
                                                {
                                                    //print(www.url);
                                                    yield return www2.SendWebRequest();

                                                    if (www2.isNetworkError || www2.isHttpError)
                                                    {
                                                        Debug.Log(www2.error);
                                                    }
                                                    else
                                                    {
                                                        Texture2D t2d = ((DownloadHandlerTexture)www2.downloadHandler).texture as Texture2D;
                                                        t2d.Apply();

                                                        File.WriteAllBytes(finalSavePath, t2d.EncodeToPNG());
                                                        DestroyImmediate(t2d, true);
                                                        UpdateDownloadInfoStatic();
                                                    }
                                                    www2.Dispose();

                                                    fileChecked += 1;


                                                }
                                            } //block Download Image

                                            //UpdateDownloadInfo();
                                        }
                                        break;
                                    }
                                default:
                                    {
                                        break;
                                    }
                            }
                        }

                    }
                    if (malesArrayData.Count > 0)
                    {
                        for (int i = 0; i < malesArrayData.Count; i++)
                        {
                            MiniJsonObject mo = malesArrayData.Get(i);

                            MiniJsonObject typeObject = mo.GetJsonObject("type");
                            int wearingCode = typeObject.GetField("id", -1);
                            int mfType = typeObject.GetField("dress_type", -1);
                            string imgName = mo.GetField("icon", "");


                            switch (wearingCode)
                            {
                                case 1: //download female dress
                                    {
                                        if (imgName != "")
                                        {
                                            string finalImageUrl = string.Format(imageUrlFormat, imgName);
                                            string finalSavePath = Path.Combine(dataPathDict["dressFemaleDataPath"], imgName);
                                            //print(string.Format("downloading : {0} to {1}", finalImageUrl, finalSavePath));
                                            {
                                                using (UnityWebRequest www2 = UnityWebRequestTexture.GetTexture(finalImageUrl))
                                                {
                                                    //print(www.url);
                                                    yield return www2.SendWebRequest();

                                                    if (www2.isNetworkError || www2.isHttpError)
                                                    {
                                                        Debug.Log(www2.error);
                                                    }
                                                    else
                                                    {
                                                        Texture2D t2d = ((DownloadHandlerTexture)www2.downloadHandler).texture as Texture2D;
                                                        t2d.Apply();

                                                        File.WriteAllBytes(finalSavePath, t2d.EncodeToPNG());
                                                        DestroyImmediate(t2d, true);
                                                        UpdateDownloadInfoStatic();
                                                    }
                                                    www2.Dispose();

                                                    fileChecked += 1;


                                                }
                                            }  //bolck download image

                                            //UpdateDownloadInfo();
                                        }
                                        break;
                                    }
                                case 2:  //download female wig
                                    {
                                        if (imgName != "")
                                        {
                                            string finalImageUrl = string.Format(imageUrlFormat, imgName);
                                            string finalSavePath = Path.Combine(dataPathDict["wigFemaleDataPath"], imgName);
                                            //print(string.Format("downloading : {0} to {1}", finalImageUrl, finalSavePath));

                                            {
                                                using (UnityWebRequest www2 = UnityWebRequestTexture.GetTexture(finalImageUrl))
                                                {
                                                    //print(www.url);
                                                    yield return www2.SendWebRequest();

                                                    if (www2.isNetworkError || www2.isHttpError)
                                                    {
                                                        Debug.Log(www2.error);
                                                    }
                                                    else
                                                    {
                                                        Texture2D t2d = ((DownloadHandlerTexture)www2.downloadHandler).texture as Texture2D;
                                                        t2d.Apply();

                                                        File.WriteAllBytes(finalSavePath, t2d.EncodeToPNG());
                                                        DestroyImmediate(t2d, true);
                                                        UpdateDownloadInfoStatic();
                                                    }
                                                    www2.Dispose();

                                                    fileChecked += 1;


                                                }
                                            }  //bolck download image

                                            //UpdateDownloadInfo();
                                        }
                                        break;
                                    }
                                case 3:  //download female ornament
                                    {
                                        if (imgName != "")
                                        {
                                            string finalImageUrl = string.Format(imageUrlFormat, imgName);
                                            string finalSavePath = Path.Combine(dataPathDict["ornamentFemaleDataPath"], imgName);
                                            //print(string.Format("downloading : {0} to {1}", finalImageUrl, finalSavePath));

                                            {
                                                using (UnityWebRequest www2 = UnityWebRequestTexture.GetTexture(finalImageUrl))
                                                {
                                                    //print(www.url);
                                                    yield return www2.SendWebRequest();

                                                    if (www2.isNetworkError || www2.isHttpError)
                                                    {
                                                        Debug.Log(www2.error);
                                                    }
                                                    else
                                                    {
                                                        Texture2D t2d = ((DownloadHandlerTexture)www2.downloadHandler).texture as Texture2D;
                                                        t2d.Apply();

                                                        File.WriteAllBytes(finalSavePath, t2d.EncodeToPNG());
                                                        DestroyImmediate(t2d, true);
                                                        UpdateDownloadInfoStatic();
                                                    }
                                                    www2.Dispose();

                                                    fileChecked += 1;


                                                }
                                            }  //bolck download image

                                            //UpdateDownloadInfo();
                                        }
                                        break;
                                    }
                                case 4:  //download female shoe
                                    {
                                        if (imgName != "")
                                        {
                                            string finalImageUrl = string.Format(imageUrlFormat, imgName);
                                            string finalSavePath = Path.Combine(dataPathDict["shoeFemaleDataPath"], imgName);
                                            //print(string.Format("downloading : {0} to {1}", finalImageUrl, finalSavePath));

                                            {
                                                using (UnityWebRequest www2 = UnityWebRequestTexture.GetTexture(finalImageUrl))
                                                {
                                                    //print(www.url);
                                                    yield return www2.SendWebRequest();

                                                    if (www2.isNetworkError || www2.isHttpError)
                                                    {
                                                        Debug.Log(www2.error);
                                                    }
                                                    else
                                                    {
                                                        Texture2D t2d = ((DownloadHandlerTexture)www2.downloadHandler).texture as Texture2D;
                                                        t2d.Apply();

                                                        File.WriteAllBytes(finalSavePath, t2d.EncodeToPNG());
                                                        DestroyImmediate(t2d, true);
                                                        UpdateDownloadInfoStatic();
                                                    }
                                                    www2.Dispose();

                                                    fileChecked += 1;


                                                }
                                            }  //bolck download image

                                            //UpdateDownloadInfo();
                                        }
                                        break;
                                    }
                                case 5: //download male wig
                                    {
                                        if (imgName != "")
                                        {
                                            string finalImageUrl = string.Format(imageUrlFormat, imgName);
                                            string finalSavePath = Path.Combine(dataPathDict["wigMaleDataPAth"], imgName);
                                            //print(string.Format("downloading : {0} to {1}", finalImageUrl, finalSavePath));

                                            {
                                                using (UnityWebRequest www2 = UnityWebRequestTexture.GetTexture(finalImageUrl))
                                                {
                                                    //print(www.url);
                                                    yield return www2.SendWebRequest();

                                                    if (www2.isNetworkError || www2.isHttpError)
                                                    {
                                                        Debug.Log(www2.error);
                                                    }
                                                    else
                                                    {
                                                        Texture2D t2d = ((DownloadHandlerTexture)www2.downloadHandler).texture as Texture2D;
                                                        t2d.Apply();

                                                        File.WriteAllBytes(finalSavePath, t2d.EncodeToPNG());
                                                        DestroyImmediate(t2d, true);
                                                        UpdateDownloadInfoStatic();
                                                    }
                                                    www2.Dispose();

                                                    fileChecked += 1;


                                                }
                                            } //bolck download image

                                            //UpdateDownloadInfo();
                                        }
                                        break;
                                    }
                                case 6: //bolck download image
                                    {
                                        if (imgName != "")
                                        {
                                            string finalImageUrl = string.Format(imageUrlFormat, imgName);
                                            string finalSavePath = Path.Combine(dataPathDict["tieMaleDataPath"], imgName);
                                            //print(string.Format("downloading : {0} to {1}", finalImageUrl, finalSavePath));

                                            {
                                                using (UnityWebRequest www2 = UnityWebRequestTexture.GetTexture(finalImageUrl))
                                                {
                                                    //print(www.url);
                                                    yield return www2.SendWebRequest();

                                                    if (www2.isNetworkError || www2.isHttpError)
                                                    {
                                                        Debug.Log(www2.error);
                                                    }
                                                    else
                                                    {
                                                        Texture2D t2d = ((DownloadHandlerTexture)www2.downloadHandler).texture as Texture2D;
                                                        t2d.Apply();

                                                        File.WriteAllBytes(finalSavePath, t2d.EncodeToPNG());
                                                        DestroyImmediate(t2d, true);
                                                        UpdateDownloadInfoStatic();
                                                    }
                                                    www2.Dispose();

                                                    fileChecked += 1;


                                                }
                                            } //block Download Image

                                            //UpdateDownloadInfo();
                                        }
                                        break;
                                    }
                                default:
                                    {
                                        break;
                                    }
                            }
                        }
                    }

                }



            }
        }


    }



    #endregion GOOD



    #region VERYGOOD
    public IEnumerator DownloadMalesData(MiniJsonArray ma)  // download males data
    {
        for (int i = 0; i < ma.Count; i++)
        {
            MiniJsonObject m = ma.Get(i);
            StartCoroutine(DownloadSingleData(m));
        }
        yield return null;
    }



    public IEnumerator DownloadFemalesData(MiniJsonArray fa)  // download females data
    {
        for(int i=0;i<fa.Count;i++)
        {
            MiniJsonObject m= fa.Get(i);
            StartCoroutine(DownloadSingleData(m));
        }
        yield return null;
    }


   



    public IEnumerator DownloadSingleData(MiniJsonObject mo)   //download a single minijson object
    {
        MiniJsonObject typeObject = mo.GetJsonObject("type");
        int wearingCode = typeObject.GetField("id", -1);
        int mfType = typeObject.GetField("dress_type", -1);
        string imgName = mo.GetField("icon", "");


        switch (wearingCode)
        {
            case 1: //download female dress
                {
                    if (imgName != "")
                    {
                        string finalImageUrl = string.Format(imageUrlFormat, imgName);
                        string finalSavePath = Path.Combine(dataPathDict["dressFemaleDataPath"], imgName);
                        //print(string.Format("downloading : {0} to {1}", finalImageUrl, finalSavePath));

                        StartCoroutine(DownloadImage(finalImageUrl, finalSavePath));

                        //UpdateDownloadInfo();
                    }
                    break;
                }
            case 2:  //download female wig
                {
                    if (imgName != "")
                    {
                        string finalImageUrl = string.Format(imageUrlFormat, imgName);
                        string finalSavePath = Path.Combine(dataPathDict["wigFemaleDataPath"], imgName);
                        //print(string.Format("downloading : {0} to {1}", finalImageUrl, finalSavePath));

                        StartCoroutine(DownloadImage(finalImageUrl, finalSavePath));

                        //UpdateDownloadInfo();
                    }
                    break;
                }
            case 3:  //download female ornament
                {
                    if (imgName != "")
                    {
                        string finalImageUrl = string.Format(imageUrlFormat, imgName);
                        string finalSavePath = Path.Combine(dataPathDict["ornamentFemaleDataPath"], imgName);
                        //print(string.Format("downloading : {0} to {1}", finalImageUrl, finalSavePath));

                        StartCoroutine(DownloadImage(finalImageUrl, finalSavePath));

                        //UpdateDownloadInfo();
                    }
                    break;
                }
            case 4:  //download female shoe
                {
                    if (imgName != "")
                    {
                        string finalImageUrl = string.Format(imageUrlFormat, imgName);
                        string finalSavePath = Path.Combine(dataPathDict["shoeFemaleDataPath"], imgName);
                        //print(string.Format("downloading : {0} to {1}", finalImageUrl, finalSavePath));

                        StartCoroutine(DownloadImage(finalImageUrl, finalSavePath));

                        //UpdateDownloadInfo();
                    }
                    break;
                }
            case 5: //download male wig
                {
                    if (imgName != "")
                    {
                        string finalImageUrl = string.Format(imageUrlFormat, imgName);
                        string finalSavePath = Path.Combine(dataPathDict["wigMaleDataPAth"], imgName);
                        //print(string.Format("downloading : {0} to {1}", finalImageUrl, finalSavePath));

                        StartCoroutine(DownloadImage(finalImageUrl, finalSavePath));

                        //UpdateDownloadInfo();
                    }
                    break;
                }
            case 6: // download male tie
                {
                    if (imgName != "")
                    {
                        string finalImageUrl = string.Format(imageUrlFormat, imgName);
                        string finalSavePath = Path.Combine(dataPathDict["tieMaleDataPath"], imgName);
                        //print(string.Format("downloading : {0} to {1}", finalImageUrl, finalSavePath));

                        StartCoroutine(DownloadImage(finalImageUrl, finalSavePath));

                        //UpdateDownloadInfo();
                    }
                    break;
                }
            default:
                {
                    break;
                }
        }

        
        yield return null;
    }

    

    public IEnumerator DownloadImage(string imageUrl,string savePath)  //download and saves single image
    {
        print("Starting Download");
        using (UnityWebRequest www =UnityWebRequestTexture.GetTexture(imageUrl))
        {
            //print(www.url);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Texture2D t2d = ((DownloadHandlerTexture)www.downloadHandler).texture as Texture2D;
                t2d.Apply();
                
                File.WriteAllBytes(savePath, t2d.EncodeToPNG());
                DestroyImmediate(t2d,true);
                //DestroyImmediate(t2d);
                UpdateDownloadInfoStatic();

            }
            
            www.Dispose();
            lock (lockObjectFileChecked)
            {
                fileChecked += 1;
            }

            print("Download Completed");

        }
    }



    #endregion VERYGOOD





    public IEnumerator CreateDirectories()  // creates necessary directories
    {
        foreach(string k in dataPathDict.Keys)
        {
            if(!Directory.Exists(dataPathDict[k]))
            {
                print(string.Format("creating Directory : {0}", dataPathDict[k]));
                Directory.CreateDirectory(dataPathDict[k]);
            }
        }
        yield return null;
    }



    public IEnumerator UpdateDownloadInfo()
    {
        lock(lockObjectFileDownloaded)
        {
            totalFileDownloaded += 1;
            downloadPercent = (100 * totalFileDownloaded) / totalDownloadableFile;
            downloadInfoUIText.text = string.Format(downloadInfoString, downloadPercent, totalDownloadableFile, totalFileDownloaded);
        }
        yield return null;
    }





    public IEnumerator LoadMainScene()
    {
        //yield return new WaitUntil(() => { return (fileChecked == totalDownloadableFile); });
//#if !UNITY_EDITOR
        WWWForm form = new WWWForm();
        string device_id;
        string device_type;
#if UNITY_ANDROID
        device_type = "A";
        device_id = SystemInfo.deviceUniqueIdentifier;

#elif UNITY_IPHONe
        device_type="I";
        device_id = SystemInfo.deviceUniqueIdentifier;
#elif UNITY_EDITOR
        device_type="A";
        device_id = SystemInfo.deviceUniqueIdentifier;
#endif
        form.AddField("device_id", device_id);
        form.AddField("device_type", device_type);


        
        using (UnityWebRequest www = UnityWebRequest.Post("http://demowebz.cu.cc/venusfashion/api/Headings/fileDownloaded", form)) // debug

        {
            //print(www.url);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //InstantiateInfoPopup("No Internet Connection");
                //retryButton.SetActive(true);
                Debug.Log(www.error);
            }
            else
            {
            }
            
        }
//#endif
        yield return new WaitForFixedUpdate();
        PlayerPrefs.SetInt("isNewInstall", 0);
        SceneManager.LoadScene(1);
    }








    public void InstantiateInfoPopup(String message)
    {
        GameObject g = Instantiate<GameObject>(popUp, canvasObject.transform);
        Text t = g.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        t.text = message;

        Button b = g.transform.GetChild(0).GetChild(1).GetComponent<Button>();
        b.onClick.AddListener(() => { Destroy(g); });
    }




}



/*
 {"error_code":0,"data":{"males":[{"id":4,"body_type":"BN","skin_color":"TN","eye_color":"GH","type_id":1,"icon":"BNTNGH-834589_15d0943391059f8223656b9a.jpg","created":"2017-11-20T17:46:38+00:00","modified":"2017-11-20T17:56:39+00:00","type":{"id":1,"dress_type":1,"name":"Dress"}},{"id":6,"body_type":"HG","skin_color":"DK","eye_color":"GH","type_id":2,"icon":"HGDKGH-xxx_15094347965d9f825ac153d3.jpg","created":"2017-11-20T17:46:38+00:00","modified":"2017-11-20T17:56:44+00:00","type":{"id":2,"dress_type":1,"name":"Wig"}}],"females":[{"id":4,"body_type":"BN","skin_color":"TN","eye_color":"GH","type_id":1,"icon":"BNTNGH-834589_15d0943391059f8223656b9a.jpg","created":"2017-11-20T17:46:38+00:00","modified":"2017-11-20T17:56:39+00:00","type":{"id":1,"dress_type":1,"name":"Dress"}},{"id":6,"body_type":"HG","skin_color":"DK","eye_color":"GH","type_id":2,"icon":"HGDKGH-xxx_15094347965d9f825ac153d3.jpg","created":"2017-11-20T17:46:38+00:00","modified":"2017-11-20T17:56:44+00:00","type":{"id":2,"dress_type":1,"name":"Wig"}}]}}
 */
