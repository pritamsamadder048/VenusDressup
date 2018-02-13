

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
    public float sceneloadPercent = 0.0f;
    public int fileChecked=0;

    public Text downloadInfoUIText;
    public Text sceneloadInfoUIText;
    [HideInInspector]
    public string downloadInfoString = "Downloading : {0} %\n[{1}/{2}]";
    [HideInInspector]
    public string sceneLoadingInfoString = "Loading Game : {0} %";

    [HideInInspector]
    public string imageUrlFormat = /*"http://demowebz.cu.cc*/"http://demowebz.cu.cc.bh-43.webhostbox.net/venusfashion/img/item_image/{0}";

    public Dictionary<string, string> dataPathDict;
    public string resourceDataPath;

    public int maxTotalCoroutine = 8;
    public int currentTotalCoroutine = 0;


    public bool standAloneDownloader = false;

    public GameObject popUp;
    public GameObject canvasObject;

    public GameObject downloadInfoPanel;
    public GameObject checkForUpdatePanel;
    public GameObject firstInstallPanel;
    public GameObject loadMainScenePanel;
    public GameObject updateAvailablePanel;
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


    public bool DownloadAll = false;


    public bool updateDownloader = false;


    bool sceneIsNotLoaded = true;

    public int httpErrorCount = 0;

    private void Awake()
    {

        firstInstallUrl = /*"http://demowebz.cu.cc*/"http://demowebz.cu.cc.bh-43.webhostbox.net/venusfashion/api/Headings/firstTime";
        testUrl = /*"http://demowebz.cu.cc*/"http://demowebz.cu.cc.bh-43.webhostbox.net/venusfashion/api/Headings/test";
        defaultUrl = /*"http://demowebz.cu.cc*/"http://demowebz.cu.cc.bh-43.webhostbox.net/venusfashion/api/Headings";
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


        dataPathDict["thumb_dressFemaleDataPath"] = Path.Combine(dataPathDict["dressFemaleDataPath"], "thumb");
        dataPathDict["thumb_wigFemaleDataPath"] = Path.Combine(dataPathDict["wigFemaleDataPath"], "thumb");
        dataPathDict["thumb_wigMaleDataPAth"] = Path.Combine(dataPathDict["wigMaleDataPAth"],"thumb");
        dataPathDict["thumb_ornamentFemaleDataPath"] = Path.Combine(dataPathDict["ornamentFemaleDataPath"], "thumb");
        dataPathDict["thumb_shoeFemaleDataPath"] = Path.Combine(dataPathDict["shoeFemaleDataPath"], "thumb");
        dataPathDict["thumb_tieMaleDataPath"] = Path.Combine(dataPathDict["tieMaleDataPath"], "thumb");
        
        dataPathDict["resourceDataPath"] = resourceDataPath;




        totalDownloadableFile = -999;
        totalFileDownloaded = 0;
        downloadPercent = 0.0f;
        imageUrlFormat = /*"http://demowebz.cu.cc*/"http://demowebz.cu.cc.bh-43.webhostbox.net/venusfashion/img/item_image/{0}";
    downloadInfoString = "Downloading : {0} %\n[{2}/{1}]";

        isNewInstall = PlayerPrefs.GetInt("isNewInstall", 1);
        httpErrorCount = 0;
    }
    // Use this for initialization
    void Start () {

        if(!updateDownloader)
        {
            StartCoroutine(CreateDirectories());
            int isNewInstall = PlayerPrefs.GetInt("isNewInstall", 1);
            if(isNewInstall==1)
            {
                ExtractPreloadableData();
            }
            
            //return;
            totalDownloadableFile = -999;
            totalFileDownloaded = 0;
            downloadPercent = 0.0f;
            imageUrlFormat = /*"http://demowebz.cu.cc*/"http://demowebz.cu.cc.bh-43.webhostbox.net/venusfashion/img/item_image/{0}";
            downloadInfoString = "Downloading : {0} %\n[{2}/{1}]";
            dmo = new MiniJsonObject();
            if (standAloneDownloader)
            {
                //if (isNewInstall == 1)
                //{
                //    checkForUpdatePanel.SetActive(false);
                //    firstInstallPanel.SetActive(true);
                //}
                //StartCoroutine(CreateDirectories());
                StartCoroutine(DownloadAllFile0());

            }
        }
        else
        {
            StartCoroutine(CreateDirectories());
            //ExtractPreloadableData();
            //return;
            totalDownloadableFile = -999;
            totalFileDownloaded = 0;
            downloadPercent = 0.0f;
            imageUrlFormat = /*"http://demowebz.cu.cc*/"http://demowebz.cu.cc.bh-43.webhostbox.net/venusfashion/img/item_image/{0}";
            downloadInfoString = "Downloading : {0} %\n[{2}/{1}]";
            dmo = new MiniJsonObject();
            if (standAloneDownloader)
            {
                //if (isNewInstall == 1)
                //{
                //    checkForUpdatePanel.SetActive(false);
                //    firstInstallPanel.SetActive(true);
                //}
                //StartCoroutine(CreateDirectories());
                StartCoroutine(DownloadAllFile02());

            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPrefs.SetInt("isNewInstall", 1);
            isNewInstall = PlayerPrefs.GetInt("isNewInstall", 1);
        }
	}

    
    private void FixedUpdate()
    {
        if(fileChecked==totalDownloadableFile && sceneIsNotLoaded)
        {
            if(httpErrorCount<=0)
            {
                PlayerPrefs.SetInt("NewUpdateAvailable", 0);
            }
            else
            {
                PlayerPrefs.SetInt("NewUpdateAvailable", 1);
            }
            if (updateDownloader)
            {
                if (httpErrorCount <= 0)
                {
                    PlayerPrefs.SetInt("NewUpdateAvailable", 0);
                }
                else
                {
                    PlayerPrefs.SetInt("NewUpdateAvailable", 1);
                }
            }
            if (httpErrorCount <= 0)
            {
                SendFileDownloadedMessageToServer();
            }
            else
            {
                
            }
            
            StartCoroutine(LoadMainSceneAsync());
            sceneIsNotLoaded = false;
        }
    }
    
    public void RetryDownload()
    {
        StartCoroutine(DownloadAllFile0());
        retryButton.SetActive(false);
    }

    
    


    public void ExtractPreloadableData()
    {
        try
        {
            string preloadData = Path.Combine(Application.streamingAssetsPath, "PreparedData");
            //string preloadData = Path.Combine(Application.persistentDataPath, "resources.zip");
            preloadData = Path.Combine(preloadData, "resources/images.zip");
            //preloadData = Path.Combine(preloadData, "resources/images");
            print(string.Format("datapath : {0}", preloadData));
            print(string.Format("preloadable data Exists : {0}", File.Exists(preloadData)));
            string dataextractionPath = Path.Combine(Application.persistentDataPath, "resources/images.zip");
            print(string.Format("extraction path : {0} ", dataextractionPath));
            //Directory.Move(preloadData, dataextractionDir);
            if(File.Exists(dataextractionPath))
            {
                print("Zip File Already Exists. Deleting old Zip File");
                File.Delete(dataextractionPath);
            }
            if (Application.platform == RuntimePlatform.Android)
            {

                WWW loadData = new WWW(preloadData);
                while (!loadData.isDone)
                {
                    continue;
                }
                File.WriteAllBytes(dataextractionPath, loadData.bytes);
            }
            else
            {
                File.Copy(preloadData, dataextractionPath);
            }

            //ZipManager.Decompress(new FileInfo(dataextractionPath));
            //ZipUtil.Unzip(dataextractionPath, Path.Combine(Application.persistentDataPath, "resources/"));
            print(string.Format("Unzipping zip file to : {0} ", Path.Combine(Application.persistentDataPath, "resources/")));
            SharpUnzip.Unzip(dataextractionPath, Path.Combine(Application.persistentDataPath, "resources/"));

            //File.Delete(preloadData);
            File.Delete(dataextractionPath);

        }
        catch (Exception e)
        {
            print(string.Format("error occured while extracting preload data : {0}", e.Message));
        }

    }




    public void CheckForUpdate()
    {
        int isNewInstall = PlayerPrefs.GetInt("isNewInstall", 1);
        string defaultUrl = /*"http://demowebz.cu.cc*/"http://demowebz.cu.cc.bh-43.webhostbox.net/venusfashion/api/Headings";
        WWWForm form = new WWWForm();
        string device_id = "";
        string device_type = "";
#if UNITY_ANDROID
        device_type = "A";
        device_id = SystemInfo.deviceUniqueIdentifier;

#elif UNITY_IPHONE
        device_type="I";
        device_id = SystemInfo.deviceUniqueIdentifier;
#elif UNITY_EDITOR
        device_type = "A";
        device_id = SystemInfo.deviceUniqueIdentifier;
#endif
        form.AddField("device_id", device_id);
        form.AddField("device_type", device_type);

        if (DownloadAll || isNewInstall==1)
        {
            defaultUrl = firstInstallUrl;
        }


    }

#region ZERO
    public IEnumerator DownloadAllFile0()
    {
        httpErrorCount = 0;
        isNewInstall = PlayerPrefs.GetInt("isNewInstall", 1);
        WWWForm form = new WWWForm();
        string device_id="";
        string device_type="";
#if UNITY_ANDROID
        device_type = "A";
        device_id = SystemInfo.deviceUniqueIdentifier;

#elif UNITY_IPHONE
        device_type="I";
        device_id = SystemInfo.deviceUniqueIdentifier;
#elif UNITY_EDITOR
        device_type = "A";
        device_id = SystemInfo.deviceUniqueIdentifier;
#endif
        print(string.Format("Device id : {0}     Device Type : {1} ", device_id, device_type));
        form.AddField("device_id", device_id);
        form.AddField("device_type", device_type);

        if(DownloadAll)
        {
            firstInstallUrl = testUrl;
        }

        if (isNewInstall == 1)
        {
			Debug.Log(firstInstallUrl);
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
                    print(jsonString);




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



                        if(femalesArrayData.Count>0 || malesArrayData .Count>0)
                        {
                            checkForUpdatePanel.SetActive(false);
                            PlayerPrefs.SetInt("NewUpdateAvailable", 1);
                            InstantiateUpdateAvailableInfoPopup(femalesArrayData,malesArrayData);
                        }
                        else
                        {
                            PlayerPrefs.SetInt("NewUpdateAvailable", 0);
                            StartCoroutine(LoadMainSceneAsync());
                        }


                        //if (femalesArrayData.Count > 0)
                        //{
                        //    checkForUpdatePanel.SetActive(false);
                        //    firstInstallPanel.SetActive(false);
                        //    downloadInfoPanel.SetActive(true);
                        //    yield return new WaitForFixedUpdate();
                        //    StartCoroutine(DownloadFemalesData0(femalesArrayData));
                        //}
                        //if (malesArrayData.Count > 0)
                        //{
                        //    checkForUpdatePanel.SetActive(false);
                        //    firstInstallPanel.SetActive(false);
                        //    downloadInfoPanel.SetActive(true);
                        //    yield return new WaitForFixedUpdate();
                        //    StartCoroutine(DownloadMalesData0(malesArrayData));
                        //}

                    }
                    else if(error_code==2)
                    {
                        //LoadMainSceneStatic();
                        PlayerPrefs.SetInt("NewUpdateAvailable", 0);
                        StartCoroutine(LoadMainSceneAsync());
                    }



                }
                www.Dispose();
            }
        }

        else
        {
            if(DownloadAll)
            {
                defaultUrl = firstInstallUrl;
            }
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
                    print("Json String : "+jsonString);




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



                        if (femalesArrayData.Count > 0 || malesArrayData.Count > 0)
                        {
                            checkForUpdatePanel.SetActive(false);
                            PlayerPrefs.SetInt("NewUpdateAvailable", 1);
                            InstantiateUpdateAvailableInfoPopup(femalesArrayData, malesArrayData);
                        }
                        else
                        {
                            PlayerPrefs.SetInt("NewUpdateAvailable", 0);
                            StartCoroutine(LoadMainSceneAsync());
                        }


                        //if (femalesArrayData.Count > 0)
                        //{
                        //    checkForUpdatePanel.SetActive(false);
                        //    firstInstallPanel.SetActive(false);
                        //    downloadInfoPanel.SetActive(true);
                        //    yield return new WaitForFixedUpdate();
                        //    StartCoroutine(DownloadFemalesData0(femalesArrayData));
                        //}
                        //if (malesArrayData.Count > 0)
                        //{
                        //    checkForUpdatePanel.SetActive(false);
                        //    firstInstallPanel.SetActive(false);
                        //    downloadInfoPanel.SetActive(true);
                        //    yield return new WaitForFixedUpdate();
                        //    StartCoroutine(DownloadMalesData0(malesArrayData));
                        //}


                    }
                    else if(error_code==2)
                    {
                        //LoadMainSceneStatic();
                        StartCoroutine("LoadMainSceneAsync");
                    }
                    else
                    {
                        InstantiateInfoPopup("Unable To Fetch Data From Server");
                        retryButton.SetActive(true);
                        Debug.Log(www.error);
                    }



                }
                //www.Dispose();
            }


        }

    }





    public IEnumerator DownloadAllFile02()
    {
        httpErrorCount = 0;
        isNewInstall = PlayerPrefs.GetInt("isNewInstall", 1);
        WWWForm form = new WWWForm();
        string device_id = "";
        string device_type = "";
#if UNITY_ANDROID
        device_type = "A";
        device_id = SystemInfo.deviceUniqueIdentifier;

#elif UNITY_IPHONE
        device_type="I";
        device_id = SystemInfo.deviceUniqueIdentifier;
#elif UNITY_EDITOR
        device_type = "A";
        device_id = SystemInfo.deviceUniqueIdentifier;
#endif
        form.AddField("device_id", device_id);
        form.AddField("device_type", device_type);

        if (DownloadAll)
        {
            firstInstallUrl = testUrl;
        }

        if (isNewInstall == 1)
        {
            Debug.Log(firstInstallUrl);
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
                    Debug.Log("Form upload complete! first time update");

                    string jsonString = www.downloadHandler.text;
                    print(jsonString);




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
                        print(string.Format("first time update total male image : {0}   total Female Image : {1}", malesArrayData.Count, femalesArrayData.Count));



                        if (femalesArrayData.Count > 0 || malesArrayData.Count > 0)
                        {
                            //checkForUpdatePanel.SetActive(false);
                            PlayerPrefs.SetInt("NewUpdateAvailable", 1);
                            //InstantiateUpdateAvailableInfoPopup(femalesArrayData, malesArrayData);
                        }


                        if (femalesArrayData.Count > 0)
                        {
                            checkForUpdatePanel.SetActive(false);
                            firstInstallPanel.SetActive(false);
                            downloadInfoPanel.SetActive(true);
                            yield return new WaitForFixedUpdate();
                            StartCoroutine(DownloadFemalesData0(femalesArrayData));
                        }
                        if (malesArrayData.Count > 0)
                        {
                            checkForUpdatePanel.SetActive(false);
                            firstInstallPanel.SetActive(false);
                            downloadInfoPanel.SetActive(true);
                            yield return new WaitForFixedUpdate();
                            StartCoroutine(DownloadMalesData0(malesArrayData));
                        }

                    }
                    else if (error_code == 2)
                    {
                        //LoadMainSceneStatic();
                        PlayerPrefs.SetInt("NewUpdateAvailable", 0);
                        StartCoroutine(LoadMainSceneAsync());
                    }



                }
                www.Dispose();
            }
        }

        else
        {
            if (DownloadAll)
            {
                defaultUrl = firstInstallUrl;
            }
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
                    Debug.Log("Form upload complete for update!");

                    string jsonString = www.downloadHandler.text;
                    print("Json String : " + jsonString);




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
                        print(string.Format("update total male image : {0}   total Female Image : {1}", malesArrayData.Count, femalesArrayData.Count));


                        if(femalesArrayData.Count > 0 || malesArrayData.Count > 0)
                        {
                            //checkForUpdatePanel.SetActive(false);
                            PlayerPrefs.SetInt("NewUpdateAvailable", 1);
                            //InstantiateUpdateAvailableInfoPopup(femalesArrayData, malesArrayData);
                        }


                        if (femalesArrayData.Count > 0)
                        {
                            checkForUpdatePanel.SetActive(false);
                            firstInstallPanel.SetActive(false);
                            downloadInfoPanel.SetActive(true);
                            yield return new WaitForFixedUpdate();
                            StartCoroutine(DownloadFemalesData0(femalesArrayData));
                        }
                        if (malesArrayData.Count > 0)
                        {
                            checkForUpdatePanel.SetActive(false);
                            firstInstallPanel.SetActive(false);
                            downloadInfoPanel.SetActive(true);
                            yield return new WaitForFixedUpdate();
                            StartCoroutine(DownloadMalesData0(malesArrayData));
                        }


                    }
                    else if (error_code == 2)
                    {
                        //LoadMainSceneStatic();
                        PlayerPrefs.SetInt("NewUpdateAvailable", 0);
                        SendFileDownloadedMessageToServer();
                        StartCoroutine("LoadMainSceneAsync");
                    }
                    else
                    {
                        InstantiateInfoPopup("Unable To Fetch Data From Server");
                        retryButton.SetActive(true);
                        Debug.Log(www.error);
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
                    httpErrorCount += 1;
                }
                else
                {
                    Texture2D t2d = ((DownloadHandlerTexture)www.downloadHandler).texture as Texture2D;
                    t2d.Apply();
                    print("downloaded .. writing data");
                    File.WriteAllBytes(savePath, t2d.EncodeToPNG());
                    print("Writing data completed");
                    yield return new WaitForFixedUpdate();
                    string imageName = imageUrl.Split('/')[imageUrl.Split('/').Length-1];
                    
                    string thumbnailpath = savePath.Replace(imageName, "");
                    thumbnailpath = Path.Combine(thumbnailpath, "thumb");
                    
                    thumbnailpath = Path.Combine(thumbnailpath, imageName);
                    t2d = GameController.ResizeTexture2D(t2d, 140, 214);
                    t2d.Apply();
                    File.WriteAllBytes(thumbnailpath, t2d.EncodeToPNG());
                    yield return new WaitForSeconds(.2f);
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
        string device_id = "";
        string device_type = "";
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
        using (UnityWebRequest www = UnityWebRequest.Post(/*"http://demowebz.cu.cc*/"http://demowebz.cu.cc.bh-43.webhostbox.net/venusfashion/api/Headings", form))  
#else
        using (UnityWebRequest www = UnityWebRequest.Post(/*"http://demowebz.cu.cc*/"http://demowebz.cu.cc.bh-43.webhostbox.net/venusfashion/api/Headings/test", form)) // debug
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
        string device_id = "";
        string device_type = "";
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
        using (UnityWebRequest www = UnityWebRequest.Post(/*"http://demowebz.cu.cc*/"http://demowebz.cu.cc.bh-43.webhostbox.net/venusfashion/api/Headings", form))  
#else                                                                             
        using (UnityWebRequest www = UnityWebRequest.Post(/*"http://demowebz.cu.cc*/"http://demowebz.cu.cc.bh-43.webhostbox.net/venusfashion/api/Headings/test", form)) // debug
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







    public void UpdateSceneloadInfoStatic(float loadpercent)
    {

        
        sceneloadPercent = (float)Math.Round(100f* loadpercent,2);
        
        sceneloadInfoUIText.text = string.Format(sceneLoadingInfoString, sceneloadPercent);


    }

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



    public void SendFileDownloadedMessageToServer()
    {
        WWWForm form = new WWWForm();
        string device_id = "";
        string device_type = "";
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



        using (UnityWebRequest www = UnityWebRequest.Post(/*"http://demowebz.cu.cc*/"http://demowebz.cu.cc.bh-43.webhostbox.net/venusfashion/api/Headings/fileDownloaded", form)) // debug

        {
            //print(www.url);
            www.SendWebRequest();
            print("sent request to inform server that update is downladed");
            while(!www.isDone )
            {
                if(www.isNetworkError || www.isHttpError)
                {
                    break;
                }
                continue;
            }

            if (www.isNetworkError || www.isHttpError)
            {
                //InstantiateInfoPopup("No Internet Connection");
                //retryButton.SetActive(true);
                Debug.Log(www.error);
                return;
            }
            else
            {
                print("informed to server ");
                print("server message : "+www.downloadHandler.text);
            }

        }
        //#endif
        PlayerPrefs.SetInt("isNewInstall", 0);
    }

    public IEnumerator LoadMainScene()
    {
        //yield return new WaitUntil(() => { return (fileChecked == totalDownloadableFile); });
//#if !UNITY_EDITOR
        WWWForm form = new WWWForm();
        string device_id="";
        string device_type="";
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


        
        using (UnityWebRequest www = UnityWebRequest.Post(/*"http://demowebz.cu.cc*/"http://demowebz.cu.cc.bh-43.webhostbox.net/venusfashion/api/Headings/fileDownloaded", form)) // debug

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
        yield return new WaitForEndOfFrame();
        PlayerPrefs.SetInt("isNewInstall", 0);
        //SceneManager.LoadScene(1);
        StartCoroutine("LoadMainSceneAsync");
    }



    public void LoadMainSceneStatic()
    {
        print("Loading main scene static");
        PlayerPrefs.SetInt("isNewInstall", 0);
        SceneManager.LoadScene(1);
    }


    public IEnumerator LoadMainSceneAsync()
    {
        sceneIsNotLoaded = false;
        print("Loading new scene");
        PlayerPrefs.SetInt("isNewInstall", 0);
        checkForUpdatePanel.SetActive(false);
        downloadInfoPanel.SetActive(false);
        firstInstallPanel.SetActive(false);
        loadMainScenePanel.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1,LoadSceneMode.Single);

        while(!asyncLoad.isDone)
        {
            float asyncloadpercent = asyncLoad.progress;
            UpdateSceneloadInfoStatic(asyncloadpercent);
            print(string.Format("Loading Percent : {0}", asyncloadpercent));

            yield return null;
        }
    }






    public void InstantiateInfoPopup(String message)
    {
        GameObject g = Instantiate<GameObject>(popUp, canvasObject.transform);
        Text t = g.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        t.text = message;

        Button b = g.transform.GetChild(0).GetChild(1).GetComponent<Button>();
        b.onClick.AddListener(() => { Destroy(g); });
    }


    public void InstantiateUpdateAvailableInfoPopup(MiniJsonArray femalesArrayData=null,MiniJsonArray malesArrayData=null)
    {
        GameObject g = Instantiate<GameObject>(updateAvailablePanel, canvasObject.transform);

        Button b1 = g.transform.GetChild(2).GetComponent<Button>();
        Button b2 = g.transform.GetChild(3).GetComponent<Button>();
        b1.onClick.AddListener(() => {
            if (femalesArrayData.Count > 0)
            {
                checkForUpdatePanel.SetActive(false);
                firstInstallPanel.SetActive(false);
                downloadInfoPanel.SetActive(true);
                //yield return new WaitForFixedUpdate();
                StartCoroutine(DownloadFemalesData0(femalesArrayData));
            }
            if (malesArrayData.Count > 0)
            {
                checkForUpdatePanel.SetActive(false);
                firstInstallPanel.SetActive(false);
                downloadInfoPanel.SetActive(true);
                //yield return new WaitForFixedUpdate();
                StartCoroutine(DownloadMalesData0(malesArrayData));
            }
            Destroy(g); });
        b2.onClick.AddListener(() => {
            PlayerPrefs.SetInt("NewUpdateAvailable", 1);
            StartCoroutine(LoadMainSceneAsync());
            Destroy(g); });
    }



}



/*
 {"error_code":0,"data":{"males":[{"id":4,"body_type":"BN","skin_color":"TN","eye_color":"GH","type_id":1,"icon":"BNTNGH-834589_15d0943391059f8223656b9a.jpg","created":"2017-11-20T17:46:38+00:00","modified":"2017-11-20T17:56:39+00:00","type":{"id":1,"dress_type":1,"name":"Dress"}},{"id":6,"body_type":"HG","skin_color":"DK","eye_color":"GH","type_id":2,"icon":"HGDKGH-xxx_15094347965d9f825ac153d3.jpg","created":"2017-11-20T17:46:38+00:00","modified":"2017-11-20T17:56:44+00:00","type":{"id":2,"dress_type":1,"name":"Wig"}}],"females":[{"id":4,"body_type":"BN","skin_color":"TN","eye_color":"GH","type_id":1,"icon":"BNTNGH-834589_15d0943391059f8223656b9a.jpg","created":"2017-11-20T17:46:38+00:00","modified":"2017-11-20T17:56:39+00:00","type":{"id":1,"dress_type":1,"name":"Dress"}},{"id":6,"body_type":"HG","skin_color":"DK","eye_color":"GH","type_id":2,"icon":"HGDKGH-xxx_15094347965d9f825ac153d3.jpg","created":"2017-11-20T17:46:38+00:00","modified":"2017-11-20T17:56:44+00:00","type":{"id":2,"dress_type":1,"name":"Wig"}}]}}
 */
