using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;



[Serializable]
public class DressProperties : MonoBehaviour , ISerializationCallbackReceiver
{
    [NonSerialized]
    public MiniJsonObject mo;
    

    public string propertyType = "dress";

    public int wearingCode;
    public int mfType=1;
    public string imgName;
    public string finalImageUrl;
    public string finalSavePath;
    public string lockStatus = "";
    public string serializedJsonObject = "";
    [SerializeField]
    private bool _isInitialized = false;

    public float[] dressColor = new float[] { 0.5f, 0.5f, 0.5f, 1f };


    [System.NonSerialized]
    public GameController gameController;

    

    [System.NonSerialized]
     public ResourceFileManager rfm;
    // Use this for initialization


    public bool IsInitialized
    {
        get
        {
            return _isInitialized;
        }
        private set
        {
            _isInitialized = value;
        }
    }


    void Awake()
    {

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Clone(DressProperties dp)
    {
        this.dressColor = new float[] { dp.dressColor[0], dp.dressColor[1], dp.dressColor[2], dp.dressColor[3] };
        this.imgName = dp.imgName;
        this.finalImageUrl = dp.finalImageUrl;
        this.wearingCode = dp.wearingCode;
        this.mfType = dp.mfType;
        this.finalSavePath = dp.finalSavePath;
        this.lockStatus = dp.lockStatus;
        this.serializedJsonObject = dp.serializedJsonObject;
        this.gameController = dp.gameController;
        this.mo = dp.mo;

    }

    public void InitializeDressProperty(MiniJsonObject m)
    {
        if (gameController == null)
        {
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }

        if (rfm == null)
        {
            rfm = GameObject.FindGameObjectWithTag("ResourceFileManager").GetComponent<ResourceFileManager>();
        }
        propertyType = "dress";

        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(InvokeUseThisDress);
        
        mo = m;
        serializedJsonObject = mo.ToString(); //MiniJSON.jsonEncode(mo);

        wearingCode = mo.GetField("type_id", -1);
        
        imgName = mo.GetField("icon", "");

        lockStatus = mo.GetField("lock_status", "false");

        if (!gameController.IsPaidUser && lockStatus == "true")
        {
            //GetComponent<Button>().interactable = false;   // may we need to change this..not sure
            transform.GetChild(0).gameObject.SetActive(true);
        }

        switch (wearingCode)
        {
            case 1: //download female dress
                {
                    if (imgName != "")
                    {
                        finalImageUrl = string.Format(rfm.imageUrlFormat, imgName);
                        finalSavePath = Path.Combine(rfm.dataPathDict["dressFemaleDataPath"], imgName);
                        //finalSavePath = Path.Combine(rfm.dataPathDict["thumb_dressFemaleDataPath"], imgName);


                        //StartCoroutine(DownloadImage(finalImageUrl, finalSavePath));

                        //UpdateDownloadInfo();
                    }
                    break;
                }
            case 2:  //download female wig
                {
                    if (imgName != "")
                    {
                        finalImageUrl = string.Format(rfm.imageUrlFormat, imgName);
                        finalSavePath = Path.Combine(rfm.dataPathDict["wigFemaleDataPath"], imgName);
                        //finalSavePath = Path.Combine(rfm.dataPathDict["thumb_wigFemaleDataPath"], imgName);


                        //StartCoroutine(DownloadImage(finalImageUrl, finalSavePath));

                        //UpdateDownloadInfo();
                    }
                    break;
                }
            case 3:  //download female ornament
                {
                    if (imgName != "")
                    {
                        finalImageUrl = string.Format(rfm.imageUrlFormat, imgName);
                        finalSavePath = Path.Combine(rfm.dataPathDict["ornamentFemaleDataPath"], imgName);
                        //finalSavePath = Path.Combine(rfm.dataPathDict["thumb_ornamentFemaleDataPath"], imgName);


                        //StartCoroutine(DownloadImage(finalImageUrl, finalSavePath));

                        //UpdateDownloadInfo();
                    }
                    break;
                }
            case 4:  //download female shoe
                {
                    if (imgName != "")
                    {
                        finalImageUrl = string.Format(rfm.imageUrlFormat, imgName);
                        finalSavePath = Path.Combine(rfm.dataPathDict["shoeFemaleDataPath"], imgName);
                        //finalSavePath = Path.Combine(rfm.dataPathDict["thumb_shoeFemaleDataPath"], imgName);


                        //StartCoroutine(DownloadImage(finalImageUrl, finalSavePath));

                        //UpdateDownloadInfo();
                    }
                    break;
                }
            case 5: //download male wig
                {
                    if (imgName != "")
                    {
                        finalImageUrl = string.Format(rfm.imageUrlFormat, imgName);
                        finalSavePath = Path.Combine(rfm.dataPathDict["wigMaleDataPAth"], imgName);
                        //finalSavePath = Path.Combine(rfm.dataPathDict["thumb_wigMaleDataPAth"], imgName);


                        //StartCoroutine(DownloadImage(finalImageUrl, finalSavePath));

                        //UpdateDownloadInfo();
                    }
                    break;
                }
            case 6: // download male tie
                {
                    if (imgName != "")
                    {
                        finalImageUrl = string.Format(rfm.imageUrlFormat, imgName);
                        finalSavePath = Path.Combine(rfm.dataPathDict["tieMaleDataPath"], imgName);
                        //finalSavePath = Path.Combine(rfm.dataPathDict["thumb_tieMaleDataPath"], imgName);


                        //StartCoroutine(DownloadImage(finalImageUrl, finalSavePath));

                        //UpdateDownloadInfo();
                    }
                    break;
                }
            default:
                {
                    break;
                }
        }
        dressColor = new float[] { 0.5f, 0.5f, 0.5f, 1f };
    _isInitialized = true;
        StartCoroutine(SetImage());
        
    }




    public void InitializeDressProperty(MiniJsonObject m,Color dc)
    {
        if (gameController == null)
        {
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }

        if (rfm == null)
        {
            rfm = GameObject.FindGameObjectWithTag("ResourceFileManager").GetComponent<ResourceFileManager>();
        }
        propertyType = "dress";

        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(InvokeUseThisDress);

        mo = m;
        serializedJsonObject = mo.ToString(); //MiniJSON.jsonEncode(mo);

        wearingCode = mo.GetField("type_id", -1);

        imgName = mo.GetField("icon", "");

        lockStatus = mo.GetField("lock_status", "false");

        if (!gameController.IsPaidUser && lockStatus == "true")
        {
            //GetComponent<Button>().interactable = false;    // may we need to change this..not sure
            transform.GetChild(0).gameObject.SetActive(true);
        }

        switch (wearingCode)
        {
            case 1: //download female dress
                {
                    if (imgName != "")
                    {
                        finalImageUrl = string.Format(rfm.imageUrlFormat, imgName);
                        finalSavePath = Path.Combine(rfm.dataPathDict["dressFemaleDataPath"], imgName);
                        //finalSavePath = Path.Combine(rfm.dataPathDict["thumb_dressFemaleDataPath"], imgName);


                        //StartCoroutine(DownloadImage(finalImageUrl, finalSavePath));

                        //UpdateDownloadInfo();
                    }
                    break;
                }
            case 2:  //download female wig
                {
                    if (imgName != "")
                    {
                        finalImageUrl = string.Format(rfm.imageUrlFormat, imgName);
                        finalSavePath = Path.Combine(rfm.dataPathDict["wigFemaleDataPath"], imgName);
                        //finalSavePath = Path.Combine(rfm.dataPathDict["thumb_wigFemaleDataPath"], imgName);


                        //StartCoroutine(DownloadImage(finalImageUrl, finalSavePath));

                        //UpdateDownloadInfo();
                    }
                    break;
                }
            case 3:  //download female ornament
                {
                    if (imgName != "")
                    {
                        finalImageUrl = string.Format(rfm.imageUrlFormat, imgName);
                        finalSavePath = Path.Combine(rfm.dataPathDict["ornamentFemaleDataPath"], imgName);
                        //finalSavePath = Path.Combine(rfm.dataPathDict["thumb_ornamentFemaleDataPath"], imgName);


                        //StartCoroutine(DownloadImage(finalImageUrl, finalSavePath));

                        //UpdateDownloadInfo();
                    }
                    break;
                }
            case 4:  //download female shoe
                {
                    if (imgName != "")
                    {
                        finalImageUrl = string.Format(rfm.imageUrlFormat, imgName);
                        finalSavePath = Path.Combine(rfm.dataPathDict["shoeFemaleDataPath"], imgName);
                        //finalSavePath = Path.Combine(rfm.dataPathDict["thumb_shoeFemaleDataPath"], imgName);


                        //StartCoroutine(DownloadImage(finalImageUrl, finalSavePath));

                        //UpdateDownloadInfo();
                    }
                    break;
                }
            case 5: //download male wig
                {
                    if (imgName != "")
                    {
                        finalImageUrl = string.Format(rfm.imageUrlFormat, imgName);
                        finalSavePath = Path.Combine(rfm.dataPathDict["wigMaleDataPAth"], imgName);
                        //finalSavePath = Path.Combine(rfm.dataPathDict["thumb_wigMaleDataPAth"], imgName);


                        //StartCoroutine(DownloadImage(finalImageUrl, finalSavePath));

                        //UpdateDownloadInfo();
                    }
                    break;
                }
            case 6: // download male tie
                {
                    if (imgName != "")
                    {
                        finalImageUrl = string.Format(rfm.imageUrlFormat, imgName);
                        finalSavePath = Path.Combine(rfm.dataPathDict["tieMaleDataPath"], imgName);
                        //finalSavePath = Path.Combine(rfm.dataPathDict["thumb_tieMaleDataPath"], imgName);


                        //StartCoroutine(DownloadImage(finalImageUrl, finalSavePath));

                        //UpdateDownloadInfo();
                    }
                    break;
                }
            default:
                {
                    break;
                }
        }
        dressColor = new float[] { dc.r, dc.g, dc.b, dc.a };
        _isInitialized = true;
        StartCoroutine(SetImage());

    }


    public void InitializeDressProperty(string  jsonString)
    {
        if(jsonString=="" || jsonString ==null)
        {
            IsInitialized = false;
            return;
        }
        MiniJsonObject m = new MiniJsonObject(jsonString);
        if (gameController == null)
        {
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }

        if (rfm == null)
        {
            rfm = GameObject.FindGameObjectWithTag("ResourceFileManager").GetComponent<ResourceFileManager>();
        }
        propertyType = "dress";

        //GetComponent<Button>().onClick.RemoveAllListeners();
        //GetComponent<Button>().onClick.AddListener(UseThisDress);

        mo = m;
        serializedJsonObject = mo.ToString(); //MiniJSON.jsonEncode(mo);

        wearingCode = mo.GetField("type_id", -1);

        imgName = mo.GetField("icon", "");

        lockStatus = mo.GetField("lock_status", "false");

        //if (!gameController.IsPaidUser && lockStatus == "true")
        //{
        //    GetComponent<Button>().interactable = false;
        //    transform.GetChild(0).gameObject.SetActive(true);
        //}

        switch (wearingCode)
        {
            case 1: //download female dress
                {
                    if (imgName != "")
                    {
                        finalImageUrl = string.Format(rfm.imageUrlFormat, imgName);
                        finalSavePath = Path.Combine(rfm.dataPathDict["dressFemaleDataPath"], imgName);
                        //finalSavePath = Path.Combine(rfm.dataPathDict["thumb_dressFemaleDataPath"], imgName);


                        //StartCoroutine(DownloadImage(finalImageUrl, finalSavePath));

                        //UpdateDownloadInfo();
                    }
                    break;
                }
            case 2:  //download female wig
                {
                    if (imgName != "")
                    {
                        finalImageUrl = string.Format(rfm.imageUrlFormat, imgName);
                        finalSavePath = Path.Combine(rfm.dataPathDict["wigFemaleDataPath"], imgName);
                        //finalSavePath = Path.Combine(rfm.dataPathDict["thumb_wigFemaleDataPath"], imgName);


                        //StartCoroutine(DownloadImage(finalImageUrl, finalSavePath));

                        //UpdateDownloadInfo();
                    }
                    break;
                }
            case 3:  //download female ornament
                {
                    if (imgName != "")
                    {
                        finalImageUrl = string.Format(rfm.imageUrlFormat, imgName);
                        finalSavePath = Path.Combine(rfm.dataPathDict["ornamentFemaleDataPath"], imgName);
                        //finalSavePath = Path.Combine(rfm.dataPathDict["thumb_ornamentFemaleDataPath"], imgName);


                        //StartCoroutine(DownloadImage(finalImageUrl, finalSavePath));

                        //UpdateDownloadInfo();
                    }
                    break;
                }
            case 4:  //download female shoe
                {
                    if (imgName != "")
                    {
                        finalImageUrl = string.Format(rfm.imageUrlFormat, imgName);
                        finalSavePath = Path.Combine(rfm.dataPathDict["shoeFemaleDataPath"], imgName);
                        //finalSavePath = Path.Combine(rfm.dataPathDict["thumb_shoeFemaleDataPath"], imgName);


                        //StartCoroutine(DownloadImage(finalImageUrl, finalSavePath));

                        //UpdateDownloadInfo();
                    }
                    break;
                }
            case 5: //download male wig
                {
                    if (imgName != "")
                    {
                        finalImageUrl = string.Format(rfm.imageUrlFormat, imgName);
                        finalSavePath = Path.Combine(rfm.dataPathDict["wigMaleDataPAth"], imgName);
                        //finalSavePath = Path.Combine(rfm.dataPathDict["thumb_wigMaleDataPAth"], imgName);


                        //StartCoroutine(DownloadImage(finalImageUrl, finalSavePath));

                        //UpdateDownloadInfo();
                    }
                    break;
                }
            case 6: // download male tie
                {
                    if (imgName != "")
                    {
                        finalImageUrl = string.Format(rfm.imageUrlFormat, imgName);
                        finalSavePath = Path.Combine(rfm.dataPathDict["tieMaleDataPath"], imgName);
                        //finalSavePath = Path.Combine(rfm.dataPathDict["thumb_tieMaleDataPath"], imgName);


                        //StartCoroutine(DownloadImage(finalImageUrl, finalSavePath));

                        //UpdateDownloadInfo();
                    }
                    break;
                }
            default:
                {
                    break;
                }
        }
        dressColor = new float[] { 0.5f, 0.5f, 0.5f, 1f };
        _isInitialized = true;
        print("successfully init dressproperty");

        
        //StartCoroutine(SetImage());

    }

    public void InitializeDressProperty(string jsonString,Color dc)
    {
        if (jsonString == "" || jsonString == null)
        {
            IsInitialized = false;
            return;
        }
        MiniJsonObject m = new MiniJsonObject(jsonString);
        if (gameController == null)
        {
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }

        if (rfm == null)
        {
            rfm = GameObject.FindGameObjectWithTag("ResourceFileManager").GetComponent<ResourceFileManager>();
        }
        propertyType = "dress";

        //GetComponent<Button>().onClick.RemoveAllListeners();
        //GetComponent<Button>().onClick.AddListener(UseThisDress);

        mo = m;
        serializedJsonObject = mo.ToString(); //MiniJSON.jsonEncode(mo);

        wearingCode = mo.GetField("type_id", -1);

        imgName = mo.GetField("icon", "");

        lockStatus = mo.GetField("lock_status", "false");

        //if (!gameController.IsPaidUser && lockStatus == "true")
        //{
        //    GetComponent<Button>().interactable = false;
        //    transform.GetChild(0).gameObject.SetActive(true);
        //}

        switch (wearingCode)
        {
            case 1: //download female dress
                {
                    if (imgName != "")
                    {
                        finalImageUrl = string.Format(rfm.imageUrlFormat, imgName);
                        finalSavePath = Path.Combine(rfm.dataPathDict["dressFemaleDataPath"], imgName);
                        //finalSavePath = Path.Combine(rfm.dataPathDict["thumb_dressFemaleDataPath"], imgName);


                        //StartCoroutine(DownloadImage(finalImageUrl, finalSavePath));

                        //UpdateDownloadInfo();
                    }
                    break;
                }
            case 2:  //download female wig
                {
                    if (imgName != "")
                    {
                        finalImageUrl = string.Format(rfm.imageUrlFormat, imgName);
                        finalSavePath = Path.Combine(rfm.dataPathDict["wigFemaleDataPath"], imgName);
                        //finalSavePath = Path.Combine(rfm.dataPathDict["thumb_wigFemaleDataPath"], imgName);


                        //StartCoroutine(DownloadImage(finalImageUrl, finalSavePath));

                        //UpdateDownloadInfo();
                    }
                    break;
                }
            case 3:  //download female ornament
                {
                    if (imgName != "")
                    {
                        finalImageUrl = string.Format(rfm.imageUrlFormat, imgName);
                        finalSavePath = Path.Combine(rfm.dataPathDict["ornamentFemaleDataPath"], imgName);
                        //finalSavePath = Path.Combine(rfm.dataPathDict["thumb_ornamentFemaleDataPath"], imgName);


                        //StartCoroutine(DownloadImage(finalImageUrl, finalSavePath));

                        //UpdateDownloadInfo();
                    }
                    break;
                }
            case 4:  //download female shoe
                {
                    if (imgName != "")
                    {
                        finalImageUrl = string.Format(rfm.imageUrlFormat, imgName);
                        finalSavePath = Path.Combine(rfm.dataPathDict["shoeFemaleDataPath"], imgName);
                        //finalSavePath = Path.Combine(rfm.dataPathDict["thumb_shoeFemaleDataPath"], imgName);


                        //StartCoroutine(DownloadImage(finalImageUrl, finalSavePath));

                        //UpdateDownloadInfo();
                    }
                    break;
                }
            case 5: //download male wig
                {
                    if (imgName != "")
                    {
                        finalImageUrl = string.Format(rfm.imageUrlFormat, imgName);
                        finalSavePath = Path.Combine(rfm.dataPathDict["wigMaleDataPAth"], imgName);
                        //finalSavePath = Path.Combine(rfm.dataPathDict["thumb_wigMaleDataPAth"], imgName);


                        //StartCoroutine(DownloadImage(finalImageUrl, finalSavePath));

                        //UpdateDownloadInfo();
                    }
                    break;
                }
            case 6: // download male tie
                {
                    if (imgName != "")
                    {
                        finalImageUrl = string.Format(rfm.imageUrlFormat, imgName);
                        finalSavePath = Path.Combine(rfm.dataPathDict["tieMaleDataPath"], imgName);
                        //finalSavePath = Path.Combine(rfm.dataPathDict["thumb_tieMaleDataPath"], imgName);


                        //StartCoroutine(DownloadImage(finalImageUrl, finalSavePath));

                        //UpdateDownloadInfo();
                    }
                    break;
                }
            default:
                {
                    break;
                }
        }

        dressColor = new float[] { dc.r, dc.g, dc.b, dc.a };
        _isInitialized = true;
        print("successfully init dressproperty");
        //StartCoroutine(SetImage());

    }


    public void SetDressColor(Color dc)
    {
        dressColor = new float[] { dc.r, dc.g, dc.b, dc.a };
    }
    public void SetDressColor(float[] dc)
    {
        dressColor = new float[] { dc[0], dc[1] ,dc[2], dc[3] };
    }



    public void InvokeUseThisDress()
    {
        if(gameController!=null)
        {
            if(_isInitialized && gameController.IsPaidUser)
            {
                gameController.ShowLoadingPanelOnlyTransparent();
                Invoke("UseThisDress", .2f);
            }
            else if(!gameController.IsPaidUser)
            {
                gameController.InstantiateInfoPopupForPurchase();
                return;
            }
        }
    }

    public void UseThisDress()
    {
        if (_isInitialized)
        {
            gameController.InstantiateNotInteractablePanel();
            gameController.selectDressController.PutOnLongDressDynamically(this);
            Invoke("DestroyUninteractivePanel", .5f);
            gameController.HideLoadingPanelOnly();
            gameController.HideLoadingPanelOnlyTransparent();
        }
        else
        {
            gameController.HideLoadingPanelOnly();
            gameController.HideLoadingPanelOnlyTransparent();
            return;
        }
        
    }

    public IEnumerator SetImage()
    {
        if (IsInitialized)
        {
            //print("trying to load dress image from : " + finalSavePath);
            string thumbnailpath = finalSavePath.Replace(imgName, "");
            thumbnailpath = Path.Combine(thumbnailpath, "thumb");

            thumbnailpath = Path.Combine(thumbnailpath, imgName);

            if (File.Exists(finalSavePath) && File.Exists(thumbnailpath))
            {
                Texture2D t2d = new Texture2D(10,10);
                //t2d.LoadImage(File.ReadAllBytes(finalSavePath));
                t2d.LoadImage(File.ReadAllBytes(thumbnailpath));
                t2d.Apply();

                GetComponent<Image>().sprite = Sprite.Create(t2d, new Rect(0, 0, t2d.width, t2d.height), new Vector2(0.5f, 0.5f), 100f);

                gameController.selectDressController.dressLoadingPanel.SetActive(false);

                //gameController.selectDressController.dressCoroutineInQueue -= 1;
                //if (gameController.selectDressController.dressCoroutineInQueue < 0)
                //{
                //    gameController.selectDressController.dressCoroutineInQueue = 0;
                //}

                //DestroyImmediate(t2d, true);
            }
            else
            {
                Destroy(gameObject);

                //gameController.selectDressController.dressCoroutineInQueue -= 1;
                //if (gameController.selectDressController.dressCoroutineInQueue < 0)
                //{
                //    gameController.selectDressController.dressCoroutineInQueue = 0;
                //}
            }
            //gameController.selectDressController.dressCoroutineInQueue -= 1;
            //if (gameController.selectDressController.dressCoroutineInQueue < 0)
            //{
            //    gameController.selectDressController.dressCoroutineInQueue = 0;
            //}
        }
        yield return null;
    }

    public void OnBeforeSerialize()
    {
        //throw new NotImplementedException();
    }

    public void OnAfterDeserialize()
    {
        //throw new NotImplementedException();
    }


    public void DestroyUninteractivePanel()
    {
        gameController.DestroyNotInteractablePopupPanel();
    }
}
