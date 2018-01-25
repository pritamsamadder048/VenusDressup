using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;


[Serializable]
public class FemaleWigProperties : MonoBehaviour , ISerializationCallbackReceiver
{
    [NonSerialized]
    public MiniJsonObject mo;

    public string propertyType = "wig";

    public int wearingCode;
    public int mfType = 1;
    public string imgName;
    public string finalImageUrl;
    public string finalSavePath;
    public string lockStatus = "";
    public string serializedJsonObject = "";
    [NonSerialized]
    public GameController gameController;

    [SerializeField]
    private bool _isInitialized = false;

    public float[] wigColor = new float[] { 0.5f, 0.5f, 0.5f, 1f };

    [NonSerialized]
    ResourceFileManager rfm;
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
        propertyType = "wig";

        //GetComponent<Button>().onClick.AddListener(UseThisWig);
        rfm = GameObject.FindGameObjectWithTag("ResourceFileManager").GetComponent<ResourceFileManager>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Clone(FemaleWigProperties fwp)
    {
        this.wigColor = new float[] { fwp.wigColor[0], fwp.wigColor[1], fwp.wigColor[2], fwp.wigColor[3] };
        this.imgName = fwp.imgName;
        this.finalImageUrl = fwp.finalImageUrl;
        this.wearingCode = fwp.wearingCode;
        this.mfType = fwp.mfType;
        this.finalSavePath = fwp.finalSavePath;
        this.lockStatus = fwp.lockStatus;
        this.serializedJsonObject = fwp.serializedJsonObject;
        this.gameController = fwp.gameController;
        this.mo = fwp.mo;
    }

    public void InitializeWigProperty(MiniJsonObject m)
    {
        if (gameController == null)
        {
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }

        if (rfm == null)
        {
            rfm = GameObject.FindGameObjectWithTag("ResourceFileManager").GetComponent<ResourceFileManager>();
        }

        propertyType = "wig";
        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(InvokeUseThisWig);

        mo = m;
        serializedJsonObject = mo.ToString(); //MiniJSON.jsonEncode(m);


        wearingCode = mo.GetField("type_id", -1);

        imgName = mo.GetField("icon", "");

        lockStatus = mo.GetField("lock_status", "false");

        if (!gameController.IsPaidUser && lockStatus == "true")
        {
            GetComponent<Button>().interactable = false;
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
        wigColor = new float[] { 0.5f, 0.5f, 0.5f, 1f };
        _isInitialized = true;
        StartCoroutine(SetImage());
    }


    public void InitializeWigProperty(MiniJsonObject m , Color wc)
    {
        if (gameController == null)
        {
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }

        if (rfm == null)
        {
            rfm = GameObject.FindGameObjectWithTag("ResourceFileManager").GetComponent<ResourceFileManager>();
        }

        propertyType = "wig";
        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(InvokeUseThisWig);

        mo = m;
        serializedJsonObject = mo.ToString(); //MiniJSON.jsonEncode(m);


        wearingCode = mo.GetField("type_id", -1);

        imgName = mo.GetField("icon", "");

        lockStatus = mo.GetField("lock_status", "false");

        if (!gameController.IsPaidUser && lockStatus == "true")
        {
            GetComponent<Button>().interactable = false;
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
        wigColor = new float[] { wc.r, wc.g, wc.b, wc.a };
        
        _isInitialized = true;
        StartCoroutine(SetImage());
    }


    public void InitializeWigProperty( string jsonString)
    {
        if(jsonString =="" || jsonString==null)
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

        propertyType = "wig";

        //GetComponent<Button>().onClick.RemoveAllListeners();
        //GetComponent<Button>().onClick.AddListener(UseThisWig);

        mo = m;
        serializedJsonObject = mo.ToString(); //MiniJSON.jsonEncode(m);


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
        wigColor = new float[] { 0.5f, 0.5f, 0.5f, 1f };
        _isInitialized = true;
        print("successfully init female wig property");
        //StartCoroutine(SetImage());
    }


    public void InitializeWigProperty(string jsonString ,Color wc)
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

        propertyType = "wig";

        //GetComponent<Button>().onClick.RemoveAllListeners();
        //GetComponent<Button>().onClick.AddListener(UseThisWig);

        mo = m;
        serializedJsonObject = mo.ToString(); //MiniJSON.jsonEncode(m);


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
        wigColor = new float[] { wc.r, wc.g, wc.b, wc.a };
        _isInitialized = true;
        print("successfully init female wig property");
        //StartCoroutine(SetImage());
    }


    public void SetFemaleWigColor(Color col)
    {
        wigColor = new float[] { col.r, col.g, col.b, col.a };
    }
    public void SetFemaleWigColor(float[] col)
    {
        wigColor = new float[] { col[0], col[1], col[2], col[3] };
    }

    public void InvokeUseThisWig()
    {
        if(gameController!=null)
        {
            if(_isInitialized)
            {
                gameController.ShowLoadingPanelOnlyTransparent();
                Invoke("UseThisWig", .2f);
            }
        }
    }

    public void UseThisWig()
    {
        if (_isInitialized)
        {
            gameController.InstantiateNotInteractablePanel();
            gameController.selectDressController.PutOnWigDynamically(this);
            Invoke("DestroyUninteractivePanel", .5f);
            gameController.HideLoadingPanelOnlyTransparent();
        }
        else
        {
            gameController.HideLoadingPanelOnlyTransparent();
            return;
        }
    }

    public IEnumerator SetImage()
    {
        //print("trying to load wig image from : " + finalSavePath);
        if (IsInitialized)
        {
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

                gameController.selectDressController.wigLoadingPanel.SetActive(false);


                //gameController.selectDressController.wigCoroutineInQueue -= 1;
                //if (gameController.selectDressController.wigCoroutineInQueue < 0)
                //{
                //    gameController.selectDressController.wigCoroutineInQueue = 0;
                //}
                //DestroyImmediate(t2d, true);
            }
            else
            {
                Destroy(gameObject);


                //gameController.selectDressController.wigCoroutineInQueue -= 1;
                //if (gameController.selectDressController.wigCoroutineInQueue < 0)
                //{
                //    gameController.selectDressController.wigCoroutineInQueue = 0;
                //}
            }

            //gameController.selectDressController.wigCoroutineInQueue -= 1;
            //if (gameController.selectDressController.wigCoroutineInQueue < 0)
            //{
            //    gameController.selectDressController.wigCoroutineInQueue = 0;
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
