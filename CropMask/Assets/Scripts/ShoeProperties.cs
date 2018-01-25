using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;


[Serializable]
public class ShoeProperties : MonoBehaviour , ISerializationCallbackReceiver
{
    [NonSerialized]
    public MiniJsonObject mo;


    public string propertyType = "shoe";

    public int wearingCode;
    public int mfType = 1;
    public string imgName;
    public string lockStatus="";
    public string finalImageUrl;
    public string finalSavePath;
    public string serializedJsonObject = "";
    [NonSerialized]
    public GameController gameController;
    [SerializeField]
    private bool _isInitialized = false;
    [NonSerialized]
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


    public void InitializeShoeProperty(MiniJsonObject m)
    {
        if (gameController == null)
        {
            gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }

        if (rfm == null)
        {
            rfm = GameObject.FindGameObjectWithTag("ResourceFileManager").GetComponent<ResourceFileManager>();
        }
        propertyType = "shoe";

        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(InvokeUseThisShoe);

        mo = m;
        serializedJsonObject = mo.ToString(); // MiniJSON.jsonEncode(mo);

        wearingCode = mo.GetField("type_id", -1);

        imgName = mo.GetField("icon", "");

        lockStatus = mo.GetField("lock_status", "false");

        if (!gameController.IsPaidUser && lockStatus=="true")
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

        _isInitialized = true;
        StartCoroutine(SetImage());

    }

    public void InitializeShoeProperty(string jsonString)
    {
        if(jsonString=="" || jsonString==null)
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
        propertyType = "shoe";

        //GetComponent<Button>().onClick.RemoveAllListeners();
        //GetComponent<Button>().onClick.AddListener(UseThisShoe);

        mo = m;
        serializedJsonObject = mo.ToString(); // MiniJSON.jsonEncode(mo);

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

        _isInitialized = true;
        print("successfully init shoe property");
        //StartCoroutine(SetImage());

    }

    public void InvokeUseThisShoe()
    {
        if(gameController!=null)
        {
            if(_isInitialized)
            {
                gameController.ShowLoadingPanelOnlyTransparent();

                Invoke("UseThisShoe", .2f);
            }
        }
    }

    public void UseThisShoe()
    {
        if (_isInitialized)
        {
            gameController.InstantiateNotInteractablePanel();
            gameController.selectDressController.PutOnShoeDynamically(this);
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
        if (IsInitialized)
        {
            print("trying to load shoe image from : " + finalSavePath);
            string thumbnailpath = finalSavePath.Replace(imgName, "");
            thumbnailpath = Path.Combine(thumbnailpath, "thumb");

            thumbnailpath = Path.Combine(thumbnailpath, imgName);

            if (File.Exists(finalSavePath) && File.Exists(thumbnailpath))
            {
                Texture2D t2d = new Texture2D(10, 10);
                //t2d.LoadImage(File.ReadAllBytes(finalSavePath));
                t2d.LoadImage(File.ReadAllBytes(thumbnailpath));
                t2d.Apply();

                GetComponent<Image>().sprite = Sprite.Create(t2d, new Rect(0, 0, t2d.width, t2d.height), new Vector2(0.5f, 0.5f), 100f);

                gameController.selectDressController.shoeLoadingPanel.SetActive(false);
                //DestroyImmediate(t2d, true);
            }
            else
            {
                Destroy(gameObject);
            }
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
