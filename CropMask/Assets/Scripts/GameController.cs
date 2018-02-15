using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class GameController : MonoBehaviour
{
	private Canvas canvas;

	[SerializeField]
	private GameObject cameraPanel;

	[SerializeField]
	private GameObject mainPanel;

	[SerializeField]
	private GameObject imagePanel;

	[SerializeField]
	private GameObject hint;

	[SerializeField]
	private GameObject MenubarObject;

	[SerializeField]
	private GameObject sideMenuPanel;

	private bool isShowingSideMenu = true;

	[SerializeField]
	private GameObject OptionSideMenuPanel;

	private bool isShowingOptionSideMenu = false;

	private bool isShowingCameraDowneMenu = false;

	[SerializeField]
	private GameObject homePanel;

	[SerializeField]
	private GameObject femaleModelObject;


	public GameObject femaleModelImageObject;

	public bool isInHome = true;

	public Image processingImage;

	public RawImage faceRawImage;

	public RawImage faceRawImage2;

	[SerializeField]
	private GameObject acceptCropButtonObject;

	[SerializeField]
	private GameObject currentActivePanel;

	[SerializeField]
	private GameObject currentActiveButton;

	public GameObject currentActiveDownPanel;

	public bool isUsingCustomFace = false;

	public bool isUsingCustomFace2 = false;

	[SerializeField]
	private GameObject galleryControllerObject;

	[SerializeField]
	private GameObject cameraControllerObject;

	[SerializeField]
	private GameObject touchControllerObject;

	[SerializeField]
	private GameObject selectDressControllerObject;

	public Gallery galleryController;

	public CameraController cameraController;

	public TouchController touchController;

	public SelectDressController selectDressController;

	private AnimateHint animateHint;

	[SerializeField]
	private GameObject loadingPanelObject;

	public AnimateLoading aml;

	public GameObject loadingPanelOnly;
    public GameObject loadingPanelOnlyTransparent;


    public GameObject[] panels;


	public GameObject[] homeMenuButtonObjects;

    public GameObject[] optionButtons;

    [SerializeField]
	private GameObject CameraDownMenuPanel;

	[SerializeField]
	private int currentShapeIndex;

	public GameObject mainModel;


	public Image dress;


	public Image wig;

	public Image shoe;

	public Image ornament;



	[SerializeField]
	private SaveData saveData;

	[SerializeField]
	private List<SaveData> saveDatas;

	[SerializeField]
	private GameObject faceEditControllerObj;

	public FaceEditController faceEditController;

	[SerializeField]
	private GameObject[] maskButtons;

	private bool showingDress = false;

	private bool showingWig = false;

	private bool showingShoe = false;

	private string fullConfigFilePath;

	
	public bool isFirstTime = true;

	[SerializeField]
	private int selectedBodyShape = 0;

	[SerializeField]
	private int selectedEyeColor = 0;

	[SerializeField]
	private int selectedBodyTone = 0;

	public GameObject sceneEditorControllerObj;

	public SceneEditorController sceneEditorController;

	[SerializeField]
	private GameObject wigEditButton;

	[SerializeField]
	private GameObject dressEditButton;

	[SerializeField]
	private GameObject popupControllerObject;

	public PopUpController popupController;

	[SerializeField]
	private GameObject projectionImage;

	[SerializeField]
	private GameObject selectShapeControllerObject;

	public SelectShapeController selectShapeController;

	[SerializeField]
	private GameObject rotationControllerObject;

	private RotationController rotationController;

	public bool isShowingSavedFacesMenu = false;

	public bool isShowingSavedLooksMenu = false;

	public string mainBodyShape = "apple";

	public string mainBodyTone = "white";

	public string mainEyeColor = "black";

	public bool bodyChanged = true;

	public float mainCarouselRotation = 0f;

	public int mainModelIndex = 0;

    public Image backGroundImage;

    public GameObject backGroundPanel;

    public bool isShowingBackGroundPanel = false;

    public string currentBackgroundName = "bg0";

	public string mainMaleBodyName = "M1";

	public bool isScreenSpaceCamera = false;

	public SaveManager saveManager;

	public bool isShowingFace = false;

	public bool isLoadedFace = false;

	public int loadedFaceIndex = -1;

	public int faceHash = 0;

	public bool isShowingFace2 = false;

	public bool isLoadedFace2 = false;

	public int loadedFaceIndex2 = -1;

	public int faceHash2 = 0;

	public GameObject saveButton;

	public MaleController maleController;

	public GameObject maleProjectionParent;

	public GameObject maleParent;

	public Image maleImage;

	public GameObject maleWigParent;

	public Image maleWig;

	public GameObject maleTieParent;

	public Image maleTie;

	public GameObject maleShoeParent;

	public Image maleShoe;

	public bool isShowingMale = false;

	public Dictionary<string, string> modelHash;

	public Dictionary<string, string> toneHash;

	public Dictionary<string, string> eyeHash;

	public MiniJsonArray currentDressList;

	public MiniJsonArray currentFemaleWigList;

	public MiniJsonArray currentOrnamentList;

	public MiniJsonArray currentShoeList;

    public MiniJsonArray currentMaleWigList;
    public MiniJsonArray currentMaleTieList;

    public Texture2D currentDressTexture;

	public Texture2D currentWigTexture;

    public Texture2D currentShoeTexture;


    public Texture2D currentMaleWigTexture;
    public Texture2D currentMaleTieTexture;

    public Color currentDressColor = new Color(0.5f, 0.5f, 0.5f, 0f);

    public Color currentWigColor = new Color(0.5f,0.5f,0.5f,0f);

    public Color currentShoeColor = new Color(0.5f, 0.5f, 0.5f, 0f);

    public Color currentMaleWigColor = new Color(0.5f, 0.5f, 0.5f, 0f);
    public Color currentMaleTieColor = new Color(0.5f, 0.5f, 0.5f, 0f);

    public int currentlyUsingFace = 0;

	public CroppedFaceProperties cfp1;

	public CroppedFaceProperties cfp2;

	public int currentlySelectedFace = 1;

	public Dictionary<string, bool> saveDict;

	public int currentTimer = 0;

	public int gurbageCollectTime = 500;

	public GameObject bodyshapeDownPanel;

	public GameObject previousActiveSidePanel;

	public GameObject previousActiveDownPanel;

	public GameObject previousActiveButton;

	public GameObject previousActiveRootPanel;

	public GameObject infoPopupPrefab;

	public GameObject canvasObject;

	

	public DressProperties currentDressProperty;

	public FemaleWigProperties currentFemaleWigProperty;
    public OrnamentProperties currentOrnamentProperty;
    public ShoeProperties currentShoeProperty;

    public MaleWigProperties currentMaleWigProperty;
    public MaleTieProperties currentMaleTieProperty;

    public GameObject NotInteractablePanelPrefab;
    public GameObject NotInteractablePanelObject;

    public bool autoAcceptChange = false;

	[SerializeField]
	private bool isPaidUser = false;

	public GameObject popupPanelForPurchase;

	public GameObject eventSystem;


    public bool shouldShowDressResetWarning = false;


    public bool femaleModelAndAllAppearingsAreShown = true;
    public bool maleModelIsZoomed=false;
    public bool femaleModelIsZoomed=false;


    public GameObject maleFemlaeSelectionPopup;
    public Toggle[] maleFemaleSelectionToggle;

    public Image tmpDress;
    public Image tmpWig;
    public Image tmpOrnament;
    public Image tmpShoe;
    public Image tmpMaleWig;
    public Image tmpMaleTie;



    public int temporarilyHiddenFace = -1;


    public Texture2D temporalFaceImage;
    public Texture2D temporeaFaceImage2;


    public FaceColorController faceColorController;
    public FaceColorController faceBrightnessController;
    public FaceColorController faceSaturationController;

    public GameObject hideMaleButton;

    public BackGroundProperty currentBackgroundProperty;

    public int previousFaceIndex;
    public Texture2D previousImagetexture;
    public Vector3 previousScale, previousPosition, previousRotation;
    public Vector2 previousSizeDelta;
    public Color previousColor;
    public bool previouslyUsingFace1 = false;
    public bool previouslyUsingFace2 = false;
    public int previousFaceHash=-999;
    public int previousLoadedFaceIndex = -1;
    public bool previouslyLoaded = false;


    public Color currentBodyToneColor = new Color(0.5f, 0.5f, 0.5f,1f);



    



    //public MaleWigProperties maleWigProperty;
    //public MaleTieProperties maleTieProperty;

    public Camera MAINCAMERA;



    public bool IsPaidUser
	{
		get
		{
			return this.isPaidUser;
		}
		private set
		{
			this.isPaidUser = value;
		}
	}






    public float currentFaceBrightness = 0.5f;
    public float currentFaceColor = 0.5f;
    public float currentFaceSaturation = 0.5f;

    public float currentFaceBrightness2 = 0.5f;
    public float currentFaceColor2 = 0.5f;
    public float currentFaceSaturation2 = 0.5f;



    public CroppedFaceProperties loadedCroppedFaceProperty;
    public CroppedFaceProperties loadedCroppedFaceProperty2;
    public CroppedFaceProperties tempCroppedFaceProperty;

    private void Awake()
	{
        Input.simulateMouseWithTouches = true;


		this.modelHash = new Dictionary<string, string>();
		this.toneHash = new Dictionary<string, string>();
		this.eyeHash = new Dictionary<string, string>();
		this.modelHash["apple"] = "AP";
		this.modelHash["banana"] = "BN";
		this.modelHash["busty"] = "BS";
		this.modelHash["hourglass"] = "HG";
		this.modelHash["pear"] = "PR";
		this.toneHash["dark"] = "DK";
		this.toneHash["tan"] = "TN";
		this.toneHash["white"] = "WH";
		this.toneHash["olive"] = "OL";
		this.eyeHash["black"] = "BK";
		this.eyeHash["brown"] = "BR";
		this.eyeHash["blue"] = "BG";
		this.eyeHash["green"] = "GH";
		this.bodyChanged = true;
		this.aml = this.loadingPanelObject.transform.GetChild(0).GetComponent<AnimateLoading>();
		this.saveDict = new Dictionary<string, bool>();
		this.saveDict["face1"] = false;
		this.saveDict["face2"] = false;




        this.isShowingSideMenu = true;
        this.galleryController = this.galleryControllerObject.GetComponent<Gallery>();
        this.cameraController = this.cameraControllerObject.GetComponent<CameraController>();
        this.touchController = this.touchControllerObject.GetComponent<TouchController>();
        this.selectDressController = this.selectDressControllerObject.GetComponent<SelectDressController>();
        this.sceneEditorController = this.sceneEditorControllerObj.GetComponent<SceneEditorController>();
        this.rotationController = this.rotationControllerObject.GetComponent<RotationController>();
        this.faceEditController = this.faceEditControllerObj.GetComponent<FaceEditController>();
        this.popupController = this.popupControllerObject.GetComponent<PopUpController>();
        this.selectShapeController = this.selectShapeControllerObject.GetComponent<SelectShapeController>();

        currentBackgroundProperty = new BackGroundProperty();
        currentBackgroundProperty.InitBackgroundProperty("bg0");

        print(string.Format("Background is : {0}  path is {1}", currentBackgroundProperty.backGroundName, currentBackgroundProperty.backGroundPath));
    }

	private void Start()
	{



        this.SetPreviousActiveRootPanel(this.homePanel);
        this.SetPreviousActive(this.sideMenuPanel);
        this.IsPaidUser = (PlayerPrefs.GetInt("isPaidUser", 0) == 1);
		this.canvas = GameObject.FindGameObjectWithTag("canvas").GetComponent<Canvas>();
		if (this.canvas != null)
		{
			if (this.canvas.renderMode == RenderMode.ScreenSpaceCamera)
			{
				this.isScreenSpaceCamera = true;
			}
			else if (this.canvas.renderMode == RenderMode.ScreenSpaceOverlay)
			{
				this.isScreenSpaceCamera = false;
			}
		}
		this.animateHint = this.hint.GetComponent<AnimateHint>();

        if (!Application.isMobilePlatform)
        {
            PlayerPrefs.SetInt("selectedBodyShape", 0);
            PlayerPrefs.SetInt("selectedEyeColor", 0);
            PlayerPrefs.SetInt("selectedBodyTone", 0);
        }
        Screen.sleepTimeout=SleepTimeout.SystemSetting;

		//this.isShowingSideMenu = true;
		//this.galleryController = this.galleryControllerObject.GetComponent<Gallery>();
		//this.cameraController = this.cameraControllerObject.GetComponent<CameraController>();
		//this.touchController = this.touchControllerObject.GetComponent<TouchController>();
		//this.selectDressController = this.selectDressControllerObject.GetComponent<SelectDressController>();
		//this.sceneEditorController = this.sceneEditorControllerObj.GetComponent<SceneEditorController>();
		//this.rotationController = this.rotationControllerObject.GetComponent<RotationController>();
		//this.faceEditController = this.faceEditControllerObj.GetComponent<FaceEditController>();
		//this.popupController = this.popupControllerObject.GetComponent<PopUpController>();
		//this.selectShapeController = this.selectShapeControllerObject.GetComponent<SelectShapeController>();
		this.HideAcceptCropButton();
		this.currentShapeIndex = 0;

        CheckIfFirstTime();

        
    }

	private void LateUpdate()
	{
		this.CollectGurbage();
	}

	public void CollectGurbage(bool rightNow=false)
	{
		if(!rightNow)
        {
            this.currentTimer++;
            if (this.currentTimer > this.gurbageCollectTime)
            {
                GC.Collect();
                Resources.UnloadUnusedAssets();
                this.currentTimer = 0;
            }
        }
        else
        {
            GC.Collect();
            Resources.UnloadUnusedAssets();
            this.currentTimer = 0;
        }
	}

	public void PaidUserDetected(string sku = "")
	{
		PlayerPrefs.SetInt("isPaidUser", 1);
		this.IsPaidUser = (PlayerPrefs.GetInt("isPaidUser", 0) == 1);
		MonoBehaviour.print(string.Format("user is paid : {0}", sku));
	}

	public void UndoPurchase(string sku = "")
	{
		PlayerPrefs.SetInt("isPaidUser", 0);
		this.IsPaidUser = (PlayerPrefs.GetInt("isPaidUser", 0) == 1);
	}

	public void PurchaseCallBack(bool purchaseStatus, string message)
	{
		if (purchaseStatus)
		{
			this.InstantiateInfoPopup("All Features Unlocked");
			this.PaidUserDetected("");
			this.selectDressController.CheckForChanges();
            this.maleController.CheckForChanges();
		}
		else
		{
			this.InstantiateInfoPopup(message);
			this.InstantiateInfoPopup("Demo All Features Unlocked");
            this.PaidUserDetected("");
            this.selectDressController.CheckForChanges();
            this.maleController.CheckForChanges();
        }
	}

    public void DownloadUpdate()
    {
        SceneManager.LoadScene(2);
    }

	public bool IsInHome()
	{
		return this.isInHome;
	}

	public void EnableAllButtons()
	{
		for (int i = 0; i < this.homeMenuButtonObjects.Length; i++)
		{
			if (i != -1)
			{
				this.homeMenuButtonObjects[i].GetComponent<Button>().interactable=true;
			}
		}
	}

	public void PurchaseFullVersionApp()
	{
        //SmartIAPListener.INSTANCE.Purchase("android.test.purchased", new Action<bool, string>(this.PurchaseCallBack));
        PurchaseCallBack(true, "Purchase Completed");

    }




    public void InstantiateInfoPopup(String message, CloseStyle closeStyle = CloseStyle.None)
    {
        GameObject g = Instantiate<GameObject>(infoPopupPrefab, canvasObject.transform);
        if (closeStyle != CloseStyle.None)
        {
            PopUpClosingStyle pcs = g.GetComponent<PopUpClosingStyle>();
            if (pcs != null)
            {
                pcs.popupCloseStyle = closeStyle;
            }
        }
        Text t = g.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        t.text = message;

        Button b = g.transform.GetChild(0).GetChild(1).GetComponent<Button>();
        b.onClick.AddListener(() => { Destroy(g); });
        Button o= g.transform.GetChild(0).GetChild(2).GetComponent<Button>();
        o.onClick.AddListener(() => { Destroy(g); });
        //print("info popup instantiate");
    }

    public void InstantiateInfoPopupForPurchase()
	{
		if (!this.IsPaidUser)
		{
			try
			{
				GameObject g = Instantiate<GameObject>(this.popupPanelForPurchase, this.canvasObject.transform);
				Button component = g.transform.GetChild(0).GetChild(1).GetComponent<Button>();
				if (component != null)
				{
					component.onClick.AddListener(delegate
					{
						Destroy(g);
					});
				}
				Button component2 = g.transform.GetChild(0).GetChild(2).GetComponent<Button>();
				if (component2 != null)
				{
					component2.onClick.AddListener(delegate
					{
						this.PurchaseFullVersionApp();
						Destroy(g);
					});
				}
			}
			catch
			{
			}
		}
	}


    public void SetFemaleBodyTone()
    {
        currentBodyToneColor = this.femaleModelImageObject.GetComponent<Image>().color;
    }

	public void UsingCustomFace(bool customFaceStatus = false)
	{
		this.isUsingCustomFace = customFaceStatus;
	}

	public bool IsUsingCustomFace()
	{
		return this.isUsingCustomFace;
	}

	public void UsingCustomFace2(bool customFaceStatus = false)
	{
		this.isUsingCustomFace2 = customFaceStatus;
	}

	public bool IsUsingCustomFace2()
	{
		return this.isUsingCustomFace2;
	}

	public void RemoveFace1()
	{
		this.isLoadedFace = false;
		this.loadedFaceIndex = -1;
		this.faceHash = 0;
		this.UsingCustomFace(false);
		this.ShowFaceImage(false);
		this.currentlyUsingFace--;
		if (this.currentlyUsingFace < 0)
		{
			this.currentlyUsingFace = 0;
		}
        saveDict["face1"] = false;
        previouslyUsingFace1 = false;
	}

	public void RemoveFace2()
	{
		this.isLoadedFace2 = false;
		this.loadedFaceIndex2 = -1;
		this.faceHash2 = 0;
		this.UsingCustomFace2(false);
		this.ShowFaceImage2(false);
		this.currentlyUsingFace--;
		if (this.currentlyUsingFace < 0)
		{
			this.currentlyUsingFace = 0;
		}
        saveDict["face2"] = false;
        previouslyUsingFace2 = false;
    }

	public void ShowFaceImage(bool show = false)
	{
        this.faceRawImage.transform.parent.gameObject.SetActive(show);
		this.faceRawImage.gameObject.SetActive(show);
		this.isShowingFace = show;
	}

	public void ShowFaceImage2(bool show = false)
	{
        this.faceRawImage2.transform.parent.gameObject.SetActive(show);
        this.faceRawImage2.gameObject.SetActive(show);
		this.isShowingFace2 = show;
	}

	public void DiscardProcessing()
	{
		this.currentlyUsingFace--;
		if (this.currentlyUsingFace < 0)
		{
			this.currentlyUsingFace = 0;
		}
		this.imagePanel.SetActive(false);
		this.homePanel.SetActive(true);
		this.mainModel.SetActive(true);
		this.ToggleHomeSideMenu(1);
		this.touchController.ToggleMask(1);
        DestroyImmediate(this.processingImage.sprite);
        CollectGurbage(true);
        GameObject[] array = this.maskButtons;
		for (int i = 0; i < array.Length; i++)
		{
			GameObject gameObject = array[i];
			gameObject.GetComponent<Toggle>().isOn=false;
			gameObject.transform.GetChild(0).gameObject.SetActive(false);
		}

        faceRawImage.transform.parent.GetComponent<UILineRenderer>().enabled = false;
        faceRawImage2.transform.parent.GetComponent<UILineRenderer>().enabled = false;

        this.touchController.DisableCurrentMask();
		this.sceneEditorControllerObj.SetActive(true);

	}

    public void ActiveSceneEditorController()
    {
        sceneEditorController.gameObject.SetActive(true);
        sceneEditorController.enabled = true;
    }

    public void DeActiveSceneEditorController()
    {
        sceneEditorController.gameObject.SetActive(false);
        sceneEditorController.enabled = false;
    }

    public void SaveProcessedImage()
	{
        //touchController.ApplyMaskImageToFace();
        touchController.ApplyMaskImageTemp();
        this.imagePanel.SetActive(false);
        
		//if (this.currentlySelectedFace == 1)
		//{
		//	this.isLoadedFace = false;
		//	this.loadedFaceIndex = -1;
		//	this.faceHash = 0;
		//}
		//else if (this.currentlySelectedFace == 2)
		//{
		//	this.isLoadedFace2 = false;
		//	this.loadedFaceIndex2 = -1;
		//	this.faceHash2 = 0;
		//}

        base.StopCoroutine(this.FinalizeImageEdit(true));
        base.StartCoroutine(this.FinalizeImageEdit(true));
		this.touchController.ToggleMask(1);
		GameObject[] array = this.maskButtons;
		for (int i = 0; i < array.Length; i++)
		{
			GameObject gameObject = array[i];
			gameObject.GetComponent<Toggle>().isOn=(false);
			gameObject.transform.GetChild(0).gameObject.SetActive(false);
		}
		this.touchController.DisableCurrentMask();

        //DestroyImmediate(processingImage.sprite);
        CollectGurbage(true);
	}

	
	public IEnumerator FinalizeImageEdit(bool backIsImageProcessingScreen=false)
	{
        
        faceEditController.ShowFaceEditPAnel(backIsImageProcessingScreen);
        yield return null;

    }


    public IEnumerator FinalizeImageEdit(Texture2D faceTexture, Vector3 scale,Vector3 rotation,int imageIndex,int faceHash,Color col,bool backIsImageProcessingScreen = false)
    {

        faceEditController.ShowFaceEditPAnel(faceTexture,scale,rotation,imageIndex,faceHash,col, backIsImageProcessingScreen);
        yield return null;

    }

    public void CheckForWhichModelToZoom()
    {
        
        if (currentlySelectedFace == 1 && isShowingMale)
        {
            if (isShowingFace2)
            {
                if (faceRawImage2.transform.parent.parent == femaleModelImageObject.transform)
                {
                    ZoomInMaleModel();
                    return;
                }
                else if(faceRawImage2.transform.parent.parent == maleImage.transform)
                {
                    ZoomInFemaleModel();
                    return;
                }
            }
            else
            {
                ZoomInFemaleModel();
                return;
            }
        }
        else if (currentlySelectedFace == 2 && isShowingMale)
        {
            if (isShowingFace)
            {
                if (faceRawImage.transform.parent.parent == femaleModelImageObject.transform)
                {
                    ZoomInMaleModel();
                    return;
                }
                else if (faceRawImage.transform.parent.parent == maleImage.transform)
                {
                    ZoomInFemaleModel();
                    return;
                }
            }
            else
            {
                ZoomInFemaleModel();
                return;
            }
        }
        else if(!isShowingMale||currentlyUsingFace==1)
        {
            ZoomInFemaleModel();
            return;
        }
       
        
    }

    public void ToggleZoomModel()
    {
        if(maleModelIsZoomed)
        {
            if(currentlySelectedFace==1)
            {
                if(isShowingFace2)
                {
                    if(faceRawImage2.transform.parent.parent==femaleModelImageObject.transform)
                    {
                        InstantiateInfoPopup("Only one face can be placed on a single model");
                        return;
                    }
                }
            }
            else if(currentlySelectedFace==2)
            {
                if(isShowingFace)
                {
                    if(faceRawImage.transform.parent.parent==femaleModelImageObject.transform)
                    {
                        InstantiateInfoPopup("Only one face can be placed on a single model");
                        return;
                    }
                }
            }
            ZoomInFemaleModel();
        }

        else if(femaleModelIsZoomed && isShowingMale)
        {
            if (currentlySelectedFace == 1)
            {
                if (isShowingFace2)
                {
                    if (faceRawImage2.transform.parent.parent == maleImage.transform)
                    {
                        InstantiateInfoPopup("Only one face can be placed on a single model");
                        return;
                    }
                }
            }
            else if (currentlySelectedFace == 2)
            {
                if (isShowingFace)
                {
                    if (faceRawImage.transform.parent.parent == maleImage.transform)
                    {
                        InstantiateInfoPopup("Only one face can be placed on a single model");
                        return;
                    }
                }
            }
            ZoomInMaleModel();
        }
        else
        {
            return;
        }
    }

    public void ZoomInFemaleModel(bool changeCroppedFaceParent = false)
    {
        //print("child number : " + femaleModelObject.transform.GetSiblingIndex());
        ZoomOutMaleModel();
        if(changeCroppedFaceParent)
        {
            if (currentlySelectedFace == 1)
            {
                faceRawImage.transform.parent.SetParent(femaleModelImageObject.transform);
                faceRawImage.transform.parent.SetSiblingIndex(2);
            }
            else if (currentlySelectedFace == 2)
            {
                faceRawImage2.transform.parent.SetParent(femaleModelImageObject.transform);
                if (femaleModelImageObject.transform.GetSiblingIndex() == 6)
                {
                    faceRawImage2.transform.parent.SetSiblingIndex(3);
                }
                else if (femaleModelImageObject.transform.GetSiblingIndex() == 5)
                {
                    faceRawImage2.transform.parent.SetSiblingIndex(2);
                }
            }
        }
        femaleModelObject.transform.localScale = Vector3.one * 2.5f;
        femaleModelObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(-200f, -1230f);
        print("female Model is zoomed in");
        femaleModelIsZoomed = true;
    }
    public void ZoomOutFemaleModel()
    {
        femaleModelObject.transform.localScale = Vector3.one;
        femaleModelObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(125.87f, 610f);
        print("female Model is zoomed out");
        femaleModelIsZoomed = false;
    }

    public void ZoomInMaleModel(bool changeCroppedFaceParent=false)
    {
        //print("child number : " + femaleModelObject.transform.GetSiblingIndex());
        ZoomOutFemaleModel();
        if(changeCroppedFaceParent)
        {
            if (currentlySelectedFace == 1)
            {
                faceRawImage.transform.parent.SetParent(maleImage.transform);
                faceRawImage.transform.parent.SetSiblingIndex(2);
            }
            else if (currentlySelectedFace == 2)
            {
                faceRawImage2.transform.parent.SetParent(maleImage.transform);
                faceRawImage2.transform.parent.SetSiblingIndex(2);
            }
        }
        maleParent.transform.localScale = Vector3.one * 2.5f;
        maleParent.GetComponent<RectTransform>().anchoredPosition = new Vector2(-200f, -1230f);
        maleParent.transform.SetSiblingIndex(4);
        print("male Model is zoomed in");
        maleModelIsZoomed = true;
    }
    public void ZoomOutMaleModel()
    {
        maleParent.transform.localScale = Vector3.one;
        maleParent.GetComponent<RectTransform>().anchoredPosition = new Vector2(-80f, 645f);
        maleParent.transform.SetSiblingIndex(2);
        print("male Model is zoomed out");
        maleModelIsZoomed = false;
    }

    protected void CheckIfFirstTime()
	{
		this.selectedBodyShape = PlayerPrefs.GetInt("selectedBodyShape");
		this.selectedEyeColor = PlayerPrefs.GetInt("selectedEyeColor");
		this.selectedBodyTone = PlayerPrefs.GetInt("selectedBodyTone");
		if (this.selectedBodyShape <= 0 || this.selectedEyeColor <= 0 || this.selectedBodyTone <= 0)
		{
			this.isFirstTime = true;
			for (int i = 0; i < this.homeMenuButtonObjects.Length; i++)
			{
				this.homeMenuButtonObjects[i].GetComponent<Button>().interactable=(false);
			}
			this.animateHint.InitialistHintFor(this.homeMenuButtonObjects[2]);
			this.animateHint.StartAnimating();
			this.homeMenuButtonObjects[2].GetComponent<Button>().interactable=(true);
		}
		else
		{
			this.EnableAllButtons();
			this.isFirstTime = false;
		}
	}

	private void Update()
	{
        
		//this.CheckIfFirstTime();
		if (Input.GetKeyDown (KeyCode.Escape)) {
			this.Quit ();
		}
#if UNITY_EDITOR
        else if (Input.GetKeyDown (KeyCode.Space))
        {
			PlayerPrefs.SetInt("isPaidUser", 0);
			this.IsPaidUser = (PlayerPrefs.GetInt("isPaidUser", 0) == 1);
		}
#endif
        if (!this.isInHome)
		{
            
			this.saveButton.GetComponent<Button>().interactable=(false);
		}
		else
		{
			this.saveButton.GetComponent<Button>().interactable=(true);
		}
	}

	public void GoToHome()
	{
        ZoomOutFemaleModel();
        ZoomOutMaleModel();
        panels[0].SetActive(true);
        if(!isShowingMale)
        {
            HideMale();
        }
        if (!this.isInHome)
		{
			if (this.panels[5].activeSelf)
			{
				MonoBehaviour.print("yes in it");
				base.StartCoroutine(this.BodyShapeSelectionProcess(false));
			}
			else
			{
				if (!this.isInHome)
				{
					for (int i = 3; i < this.panels.Length; i++)
					{
						this.panels[i].SetActive(false);
					}
					this.homePanel.SetActive(true);
					this.ToggleHomeSideMenu(1);
				}
				else
				{
					this.ToggleHomeSideMenu(0);
				}
				this.maleProjectionParent.SetActive(false);
				if (this.isShowingMale)
				{
					this.ShowMale();
				}
				this.SetPreviousActiveRootPanel(this.homePanel);
                this.SetPreviousActive(this.sideMenuPanel);
            }
		}
		else
		{
			this.ToggleHomeSideMenu(0);
			this.SetPreviousActiveRootPanel(this.homePanel);
            this.SetPreviousActive(this.sideMenuPanel);
        }
		if (this.isUsingCustomFace)
		{
			this.ShowFaceImage(true);
		}
		else
		{
			this.ShowFaceImage(false);
		}
		if (this.isUsingCustomFace2)
		{
			this.ShowFaceImage2(true);
		}
		else
		{
			this.ShowFaceImage2(false);
		}

        faceRawImage.transform.parent.GetComponent<UILineRenderer>().enabled = false;
        faceRawImage2.transform.parent.GetComponent<UILineRenderer>().enabled = false;

        sceneEditorController.enabled = true;
        sceneEditorControllerObj.SetActive(true);

        
    }

	public void ToggleHomeSideMenu(int showCode = 0)
	{
		if (this.isShowingCameraDowneMenu)
		{
			this.ToggleCameraDownMenu(2);
		}
		if (this.isShowingBackGroundPanel)
		{
			this.ToggleBackGroundSideMenu(2);
		}
		if (this.isShowingSavedFacesMenu)
		{
			this.ToggleSavedFacesPanel(2);
		}
		if (this.isShowingSavedLooksMenu)
		{
			this.ToggleSavedLooksPanel(2);
		}
		if (showCode == 1)
		{
			this.ToggleOptionSideMenu(2);
			ShortcutExtensions46.DOAnchorPosX(this.sideMenuPanel.GetComponent<RectTransform>(), 0f, 0.3f, false);
			this.isInHome = true;
			this.isShowingSideMenu = true;
		}
		else if (showCode == 2)
		{
			if (!this.isScreenSpaceCamera)
			{
				ShortcutExtensions46.DOAnchorPosX(this.sideMenuPanel.GetComponent<RectTransform>(), -400f, 0.3f, false);
			}
			else
			{
				ShortcutExtensions46.DOAnchorPosX(this.sideMenuPanel.GetComponent<RectTransform>(), -400f, 0.3f, false);
			}
			this.isShowingSideMenu = false;
		}
		else if (!this.isShowingSideMenu)
		{
			this.ToggleOptionSideMenu(2);
			if (!this.isScreenSpaceCamera)
			{
				ShortcutExtensions46.DOAnchorPosX(this.sideMenuPanel.GetComponent<RectTransform>(), 0f, 0.3f, false);
			}
			else
			{
				ShortcutExtensions46.DOAnchorPosX(this.sideMenuPanel.GetComponent<RectTransform>(), 0f, 0.3f, false);
			}
			this.isInHome = true;
			this.isShowingSideMenu = true;
		}
		else
		{
			this.ToggleOptionSideMenu(2);
			if (!this.isScreenSpaceCamera)
			{
				ShortcutExtensions46.DOAnchorPosX(this.sideMenuPanel.GetComponent<RectTransform>(), -400f, 0.3f, false);
			}
			else
			{
				ShortcutExtensions46.DOAnchorPosX(this.sideMenuPanel.GetComponent<RectTransform>(), -400f, 0.3f, false);
			}
			this.isShowingSideMenu = false;
		}
		if (!this.sceneEditorControllerObj.activeSelf)
		{
			this.sceneEditorControllerObj.SetActive(true);
		}
		if (this.sceneEditorController.IsShownigobjectPanel())
		{
			this.sceneEditorController.HideObjectsInSceePanel();
		}
	}

	public void ToggleOptionSideMenu(int showCode = 0)
	{
		if (this.isShowingSavedFacesMenu)
		{
			this.ToggleSavedFacesPanel(2);
		}
		if (this.isShowingSavedLooksMenu)
		{
			this.ToggleSavedLooksPanel(2);
		}
		if (this.isShowingBackGroundPanel)
		{
			this.ToggleBackGroundSideMenu(2);
		}
		if (this.isShowingSideMenu)
		{
			this.ToggleHomeSideMenu(2);
		}
		if (this.animateHint.IsShowingHint())
		{
			this.animateHint.StopAnimating();
		}
		if (showCode == 1)
		{
			this.DeactiveCurrentActive(false, false);
            int st= PlayerPrefs.GetInt("NewUpdateAvailable", 0);
            if(st==1)
            {
                optionButtons[3].GetComponent<Button>().interactable = true;

            }
            else
            {
                optionButtons[3].GetComponent<Button>().interactable = false;
            }
            if (!this.isScreenSpaceCamera)
			{
				ShortcutExtensions46.DOAnchorPosX(this.OptionSideMenuPanel.GetComponent<RectTransform>(), 0f, 0.3f, false);
			}
			else
			{
				ShortcutExtensions46.DOAnchorPosX(this.OptionSideMenuPanel.GetComponent<RectTransform>(), 0f, 0.3f, false);
			}
			this.isShowingOptionSideMenu = true;
		}
		else if (showCode == 2)
		{
			if (!this.isScreenSpaceCamera)
			{
				ShortcutExtensions46.DOAnchorPosX(this.OptionSideMenuPanel.GetComponent<RectTransform>(), -400f, 0.3f, false);
			}
			else
			{
				ShortcutExtensions46.DOAnchorPosX(this.OptionSideMenuPanel.GetComponent<RectTransform>(), -400f, 0.3f, false);
			}
			this.isShowingOptionSideMenu = false;
		}
		else if (!this.isShowingOptionSideMenu)
		{
            
			this.DeactiveCurrentActive(false, false);
            //GoToHome();
            int st = PlayerPrefs.GetInt("NewUpdateAvailable", 0);
            if (st == 1)
            {
                optionButtons[3].GetComponent<Button>().interactable = true;

            }
            else
            {
                optionButtons[3].GetComponent<Button>().interactable = false;
            }

            if (!this.isScreenSpaceCamera)
			{
				ShortcutExtensions46.DOAnchorPosX(this.OptionSideMenuPanel.GetComponent<RectTransform>(), 0f, 0.3f, false);
			}
			else
			{
				ShortcutExtensions46.DOAnchorPosX(this.OptionSideMenuPanel.GetComponent<RectTransform>(), 0f, 0.3f, false);
			}

			this.isShowingOptionSideMenu = true;
		}
		else
		{
			if (!this.isScreenSpaceCamera)
			{
				ShortcutExtensions46.DOAnchorPosX(this.OptionSideMenuPanel.GetComponent<RectTransform>(), -400f, 0.3f, false);
			}
			else
			{
				ShortcutExtensions46.DOAnchorPosX(this.OptionSideMenuPanel.GetComponent<RectTransform>(), -400f, 0.3f, false);
			}
			this.isShowingOptionSideMenu = false;
		}
		if (this.isShowingOptionSideMenu)
		{
			this.MenubarObject.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
		}
		else
		{
			this.MenubarObject.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
		}
		if (!this.sceneEditorControllerObj.activeSelf)
		{
			this.sceneEditorControllerObj.SetActive(true);
		}
		if (this.sceneEditorController.IsShownigobjectPanel())
		{
			this.sceneEditorController.HideObjectsInSceePanel();
		}

        int tst = PlayerPrefs.GetInt("NewUpdateAvailable", 0);
        if (tst == 1)
        {
            optionButtons[3].GetComponent<Button>().interactable = true;

        }
        else
        {
            optionButtons[3].GetComponent<Button>().interactable = false;
        }
    }

	public void ToggleBackGroundSideMenu(int showCode = 0)
	{
		this.panels[10].SetActive(true);
		if (this.isShowingCameraDowneMenu)
		{
			this.ToggleCameraDownMenu(2);
		}
		if (this.isShowingSavedFacesMenu)
		{
			this.ToggleSavedFacesPanel(2);
		}
		if (this.isShowingSavedLooksMenu)
		{
			this.ToggleSavedLooksPanel(2);
		}
		if (showCode == 1)
		{
			this.ToggleOptionSideMenu(2);
			this.ToggleHomeSideMenu(2);
			ShortcutExtensions46.DOAnchorPosX(this.backGroundPanel.GetComponent<RectTransform>(), 0f, 0.3f, false);
			this.isShowingBackGroundPanel = true;
		}
		else if (showCode == 2)
		{
			if (!this.isScreenSpaceCamera)
			{
				ShortcutExtensions46.DOAnchorPosX(this.backGroundPanel.GetComponent<RectTransform>(), -800f, 0.3f, false);
			}
			else
			{
				ShortcutExtensions46.DOAnchorPosX(this.backGroundPanel.GetComponent<RectTransform>(), -800f, 0.3f, false);
			}
			this.isShowingBackGroundPanel = false;
		}
		else if (!this.isShowingBackGroundPanel)
		{
			this.ToggleOptionSideMenu(2);
			this.ToggleHomeSideMenu(2);
			if (!this.isScreenSpaceCamera)
			{
				ShortcutExtensions46.DOAnchorPosX(this.backGroundPanel.GetComponent<RectTransform>(), 0f, 0.3f, false);
			}
			else
			{
				ShortcutExtensions46.DOAnchorPosX(this.backGroundPanel.GetComponent<RectTransform>(), 0f, 0.3f, false);
			}
			this.isShowingBackGroundPanel = true;
		}
		else
		{
			this.ToggleOptionSideMenu(2);
			this.ToggleHomeSideMenu(2);
			if (!this.isScreenSpaceCamera)
			{
				ShortcutExtensions46.DOAnchorPosX(this.backGroundPanel.GetComponent<RectTransform>(), -800f, 0.3f, false);
			}
			else
			{
				ShortcutExtensions46.DOAnchorPosX(this.backGroundPanel.GetComponent<RectTransform>(), -800f, 0.3f, false);
			}
			this.isShowingBackGroundPanel = false;
		}
		if (!this.sceneEditorControllerObj.activeSelf)
		{
			this.sceneEditorControllerObj.SetActive(true);
		}
		if (this.sceneEditorController.IsShownigobjectPanel())
		{
			this.sceneEditorController.HideObjectsInSceePanel();
		}

        Invoke("HideLoadingPanelOnly", .1f);
	}

	public void ToggleCameraDownMenu(int showCode = 0)
	{
		if (this.animateHint.IsShowingHint())
		{
			this.animateHint.StopAnimating();
		}
		if (showCode == 1)
		{
			ShortcutExtensions46.DOAnchorPosY(this.CameraDownMenuPanel.GetComponent<RectTransform>(), 0f, 0.3f, false);
			this.isShowingCameraDowneMenu = true;
		}
		else if (showCode == 2)
		{
			ShortcutExtensions46.DOAnchorPosY(this.CameraDownMenuPanel.GetComponent<RectTransform>(), -400f, 0.3f, false);
			this.isShowingCameraDowneMenu = false;
		}
		else if (!this.isShowingCameraDowneMenu)
		{
			MonoBehaviour.print("on");
			ShortcutExtensions46.DOAnchorPosY(this.CameraDownMenuPanel.GetComponent<RectTransform>(), 0f, 0.3f, false);
			this.isShowingCameraDowneMenu = true;
		}
		else
		{
			MonoBehaviour.print("off");
			ShortcutExtensions46.DOAnchorPosY(this.CameraDownMenuPanel.GetComponent<RectTransform>(), -400f, 0.3f, false);
			this.isShowingCameraDowneMenu = false;
		}
	}

    public void ToggleSavedFacesPanel0()
    {
        ToggleSavedFacesPanel(0);
    }
    public void ToggleSavedFacesPanel1()
    {
        ToggleSavedFacesPanel(1);
    }
    public void ToggleSavedFacesPanel2()
    {
        ToggleSavedFacesPanel(2);
    }

    public void ToggleSavedFacesPanel(int showStatus = 0)
	{
		switch (showStatus)
		{
		case 0:
            default:
			this.panels[7].SetActive(true);
			if (!this.isShowingSavedFacesMenu)
			{
				if (this.isShowingCameraDowneMenu)
				{
					this.ToggleCameraDownMenu(2);
				}
				if (this.isShowingOptionSideMenu)
				{
					this.ToggleOptionSideMenu(2);
				}
				if (this.isShowingSideMenu)
				{
					this.ToggleHomeSideMenu(2);
				}
				if (this.isShowingSavedLooksMenu)
				{
					this.ToggleSavedLooksPanel(2);
				}
				ShortcutExtensions46.DOAnchorPosX(this.panels[7].GetComponent<RectTransform>(), 0f, 0.3f, false);
				this.isShowingSavedFacesMenu = true;
				base.StartCoroutine(this.saveManager.ResetAllCroppedFaces());
				if (this.saveDict["face1"] || this.saveDict["face2"])
				{
					this.popupController.ShowPopup(2, "Save The Face");
				}
			}
			else
			{
				if (this.isShowingCameraDowneMenu)
				{
					this.ToggleCameraDownMenu(2);
				}
				if (this.isShowingOptionSideMenu)
				{
					this.ToggleOptionSideMenu(2);
				}
				if (this.isShowingSideMenu)
				{
					this.ToggleHomeSideMenu(2);
				}
				ShortcutExtensions46.DOAnchorPosX(this.panels[7].GetComponent<RectTransform>(), -400f, 0.3f, false);
				this.isShowingSavedFacesMenu = false;
			}
			return;
		case 1:
			if (this.isShowingCameraDowneMenu)
			{
				this.ToggleCameraDownMenu(2);
			}
			if (this.isShowingOptionSideMenu)
			{
				this.ToggleOptionSideMenu(2);
			}
			if (this.isShowingSideMenu)
			{
				this.ToggleHomeSideMenu(2);
			}
			ShortcutExtensions46.DOAnchorPosX(this.panels[7].GetComponent<RectTransform>(), 0f, 0.3f, false);
			this.isShowingSavedFacesMenu = true;
			base.StartCoroutine(this.saveManager.ResetAllCroppedFaces());
			if (this.saveDict["face1"] || this.saveDict["face2"])
			{
                    HideLoadingPanelOnly();
                    this.popupController.ShowPopup(2, "Save The Face");
			}
                
			return;
		case 2:
			ShortcutExtensions46.DOAnchorPosX(this.panels[7].GetComponent<RectTransform>(), -400f, 0.3f, false);
			this.isShowingSavedFacesMenu = false;
                HideLoadingPanelOnly();
			return;
		}
		
	}

    public void ToggleSavedLooksPanel0()
    {
        ToggleSavedLooksPanel(0);
    }

    public void ToggleSavedLooksPanel1()
    {
        ToggleSavedLooksPanel(1);
    }

    public void ToggleSavedLooksPanel2()
    {
        ToggleSavedLooksPanel(2);
    }
    public void ToggleSavedLooksPanel(int showStatus = 0)
	{
		switch (showStatus)
		{
		case 0:
		default:
			this.panels[8].SetActive(true);
			if (!this.isShowingSavedLooksMenu)
			{
				MonoBehaviour.print("toggling saved look panels");
				if (this.isShowingCameraDowneMenu)
				{
					this.ToggleCameraDownMenu(2);
				}
				if (this.isShowingOptionSideMenu)
				{
					this.ToggleOptionSideMenu(2);
				}
				if (this.isShowingSideMenu)
				{
					this.ToggleHomeSideMenu(2);
				}
				if (this.isShowingSavedFacesMenu)
				{
					this.ToggleSavedFacesPanel(2);
				}
				ShortcutExtensions46.DOAnchorPosX(this.panels[8].GetComponent<RectTransform>(), 0f, 0.3f, false);
				this.isShowingSavedLooksMenu = true;
				base.StartCoroutine(this.saveManager.ResetAllSavedLooks());
			}
			else
			{
				if (this.isShowingCameraDowneMenu)
				{
					this.ToggleCameraDownMenu(2);
				}
				if (this.isShowingOptionSideMenu)
				{
					this.ToggleOptionSideMenu(2);
				}
				if (this.isShowingSideMenu)
				{
					this.ToggleHomeSideMenu(2);
				}
				if (this.isShowingSavedFacesMenu)
				{
					this.ToggleSavedFacesPanel(2);
				}
				ShortcutExtensions46.DOAnchorPosX(this.panels[8].GetComponent<RectTransform>(), -400f, 0.3f, false);
				this.isShowingSavedLooksMenu = false;
                    HideLoadingPanelOnly();
			}
			return;
		case 1:
			if (this.isShowingCameraDowneMenu)
			{
				this.ToggleCameraDownMenu(2);
			}
			if (this.isShowingOptionSideMenu)
			{
				this.ToggleOptionSideMenu(2);
			}
			if (this.isShowingSideMenu)
			{
				this.ToggleHomeSideMenu(2);
			}
			if (this.isShowingSavedFacesMenu)
			{
				this.ToggleSavedFacesPanel(2);
			}
			ShortcutExtensions46.DOAnchorPosX(this.panels[8].GetComponent<RectTransform>(), 0f, 0.3f, false);
			this.isShowingSavedLooksMenu = true;
			base.StartCoroutine(this.saveManager.ResetAllSavedLooks());
			return;
		case 2:
			ShortcutExtensions46.DOAnchorPosX(this.panels[8].GetComponent<RectTransform>(), -400f, 0.3f, false);
			this.isShowingSavedLooksMenu = false;
			return;
		}

        
	}

	public void OnPressCameraButton(GameObject g)
	{
		this.ToggleCameraDownMenu(0);
	}

	public void ConfirmBodyShapeSelection(GameObject g)
	{
        //base.StartCoroutine(this.BodyShapeSelectionProcess(true));
        //ResetWearing();
        selectShapeController.OnClickSelectShapeButton(true);
		this.popupController.HidePopup();
	}

	public void OnPressSelectBodyShapeButton(bool mainButton=false)
	{
        autoAcceptChange = !mainButton;
		if(!isFirstTime &&(selectDressController.isWearingDress || selectDressController.isWearingWig || selectDressController.isWearingOrnament || selectDressController.isWearingShoe))
        {
            shouldShowDressResetWarning = true;
        }
        else
        {
            shouldShowDressResetWarning = false;
        }
        PlayerPrefs.SetInt("selectedBodyShape", 1);
        PlayerPrefs.SetInt("selectedEyeColor", 1);
        PlayerPrefs.SetInt("selectedBodyTone", 1);
        
        base.StartCoroutine(this.BodyShapeSelectionProcess(true));

    }


	private IEnumerator BodyShapeSelectionProcess(bool shouldHideMainModelImage)
	{
        if (shouldHideMainModelImage)
        {

            print("Pressed shape button");
            DeactiveCurrentActive(true);
            SetPreviousActive(currentActivePanel, currentActiveDownPanel);
            yield return new WaitForSeconds(.3f);
            femaleModelObject.SetActive(false);
            maleParent.SetActive(false);
            if (animateHint.IsShowingHint())
            {
                animateHint.StopAnimating();
            }
            ToggleHomeSideMenu(2);
            isInHome = false;

            for (int i = 2; i < panels.Length; i++)
            {
                panels[i].SetActive(false);
            }
            projectionImage.GetComponent<RawImage>().DOFade(0f, 0f);
            yield return new WaitForSeconds(.3f);


            HideFemaleModelAndAppearings();


            //yield return new WaitForSeconds(.3f);
            panels[5].SetActive(true);
            selectShapeController.SelectThisParticularModel(mainCarouselRotation, mainModelIndex);
            if(!isFirstTime)
            {
                selectShapeController.OnClickSelectEyeButton(true);
            }
            projectionImage.GetComponent<RawImage>().DOFade(1f, 0.3f);
            bodyshapeDownPanel.GetComponent<RectTransform>().DOAnchorPosY(0f, .3f);


        }
        else
        {
            bodyshapeDownPanel.GetComponent<RectTransform>().DOAnchorPosY(-400f, .3f);
            //ActivePreviousActive(true);
            if (!femaleModelObject.activeSelf || femaleModelImageObject.GetComponent<Image>().color.a < 0.5)
            {
                if (animateHint.IsShowingHint())
                {
                    animateHint.StopAnimating();
                }
                femaleModelImageObject.GetComponent<Image>().DOFade(0f, 0f);
                projectionImage.GetComponent<RawImage>().DOFade(0f, 0.3f);
                
                yield return new WaitForSeconds(.2f);
                ShowFemaleModelAndAppearings();
            }
            else
            {
                if (animateHint.IsShowingHint())
                {
                    animateHint.StopAnimating();
                }
            }

            if (isShowingMale)
            {
                ShowMale();
            }
            else
            {
                HideMale();
            }

            panels[5].SetActive(false);

            if (sideMenuPanel == previousActiveSidePanel || previousActiveRootPanel == homePanel)
            {
                this.SetPreviousActiveRootPanel(this.homePanel);
                this.SetPreviousActive(this.sideMenuPanel);
                ToggleHomeSideMenu(1);
            }
            else
            {

                selectDressController.editButtons[0].SetActive(false);
                selectDressController.editButtons[1].SetActive(false);
                if (previousActiveRootPanel ==panels[6])
                {
                    if (previousActiveSidePanel.name == "LongDressSidePanel")
                    {
                        //if (dress.transform.parent.gameObject.activeSelf && dress.gameObject.activeSelf && dress.color.a > 0.5f)
                        if(selectDressController.isWearingDress)
                        {
                            selectDressController.editButtons[0].SetActive(true);
                            selectDressController.editButtons[1].SetActive(false);
                        }
                        else
                        {
                            selectDressController.editButtons[0].SetActive(false);
                            selectDressController.editButtons[1].SetActive(false);
                        }
                    }
                    else if (previousActiveSidePanel.name == "WigSidePAnel")
                    {
                        //if (wig.transform.parent.gameObject.activeSelf && wig.gameObject.activeSelf && wig.color.a > 0.5f)
                        if(selectDressController.isWearingWig)
                        {
                            selectDressController.editButtons[1].SetActive(true);
                            selectDressController.editButtons[0].SetActive(false);
                        }
                        else
                        {
                            selectDressController.editButtons[0].SetActive(false);
                            selectDressController.editButtons[1].SetActive(false);
                        }
                    }
                }
                
            }
            //if (!isInHome)
            //{
            //    for (int i = 3; i < panels.Length; i++)
            //    {
            //        panels[i].SetActive(false);
            //    }
            //    homePanel.SetActive(true);
            //    ToggleHomeSideMenu(1);
            //    isInHome = true;
            //}
            //else
            //{
            //    ToggleHomeSideMenu();
            //    isInHome = true;
            //}


            if (!sceneEditorControllerObj.activeSelf)
            {
                sceneEditorControllerObj.SetActive(true);
            }
            if (sceneEditorController.IsShownigobjectPanel())
            {
                sceneEditorController.HideObjectsInSceePanel();
            }


            //StartCoroutine(selectShapeController.ResetAllModels(true));
            if (maleProjectionParent.transform.GetChild(0).GetComponent<RawImage>().color.a > 0.5f)
            {
                maleProjectionParent.transform.GetChild(0).GetComponent<RawImage>().DOFade(0f, .3f);
                maleProjectionParent.SetActive(false);
            }
            yield return new WaitForSeconds(.3f);
            if(sideMenuPanel!=previousActiveSidePanel && previousActiveRootPanel!=homePanel)
            {
                ActivePreviousActive(true);
            }
            yield return null;

            
            if (isFirstTime && (PlayerPrefs.GetInt("selectedEyeColor",0)==1) && (PlayerPrefs.GetInt("selectedBodyTone",0)==1) && (PlayerPrefs.GetInt("selectedBodyShape",0) ==1))
            {
                popupController.ShowPopup(0, "NOW, SEE THE BEST\nDRESS STYLE FOR YOUR\nBODY TYPE");
            }
            CheckIfFirstTime();
        }
    }


    public void HideFemaleModelAndAppearings()
    {
        if(femaleModelAndAllAppearingsAreShown)
        {
            ornament.DOFade(0f, .3f);
            dress.DOFade(0f, .3f);
            wig.DOFade(0f, .3f);
            shoe.DOFade(0f, .3f);
            femaleModelImageObject.GetComponent<Image>().DOFade(0f, .3f);
            femaleModelObject.SetActive(false);
            femaleModelAndAllAppearingsAreShown = false;
        }
    }

    public void ShowFemaleModelAndAppearings()
    {
        if(!femaleModelAndAllAppearingsAreShown)
        {
            femaleModelObject.SetActive(true);
            femaleModelImageObject.GetComponent<Image>().DOFade(1f, .3f);
            if (selectDressController.IsWearingDress())
            {
                dress.DOFade(1f, .3f);
            }
            if (selectDressController.IsWearingWig())
            {
                wig.DOFade(1f, .3f);
            }
            if (selectDressController.IsWearingOrnament())
            {
                ornament.DOFade(1f, .3f);
            }
            if (selectDressController.IsWearingShoe())
            {
                shoe.DOFade(1f, .3f);
            }
            femaleModelAndAllAppearingsAreShown = true;
        }
    }

	public void OnPressDressForHerButton(GameObject g)
	{
		this.SetPreviousActiveRootPanel(this.panels[6]);
		if (this.animateHint.IsShowingHint())
		{
			this.animateHint.StopAnimating();
		}
		this.ToggleHomeSideMenu(2);
		this.isInHome = false;
		for (int i = 2; i < this.panels.Length; i++)
		{
			this.panels[i].SetActive(false);
		}
		this.selectDressController.OnClickSelectLongDressButton(true);
		this.panels[6].SetActive(true);
	}

	public void OnPressForHimButton(GameObject g)
	{
		if (this.animateHint.IsShowingHint())
		{
			this.animateHint.StopAnimating();
		}
		this.ToggleHomeSideMenu(2);
		this.isInHome = false;
		for (int i = 2; i < this.panels.Length; i++)
		{
			this.panels[i].SetActive(false);
		}
		this.panels[9].SetActive(true);
		this.maleController.OnClickSelectMale(true);
		this.maleProjectionParent.SetActive(true);
		ShortcutExtensions46.DOFade(this.maleProjectionParent.transform.GetChild(0).GetComponent<RawImage>(), 1f, 0.3f);
	}

    public void OnPressForHimButton()
    {
        if (this.animateHint.IsShowingHint())
        {
            this.animateHint.StopAnimating();
        }
        this.ToggleHomeSideMenu(2);
        this.isInHome = false;
        for (int i = 2; i < this.panels.Length; i++)
        {
            this.panels[i].SetActive(false);
        }
        this.panels[9].SetActive(true);
        this.maleProjectionParent.SetActive(false);
        //ShortcutExtensions46.DOFade(this.maleProjectionParent.transform.GetChild(0).GetComponent<RawImage>(), 1f, 0.3f);
    }

    public void OnPressBackGroundbutton(GameObject g)
	{
        ShowLoadingPanelOnly();
        Invoke("InvokeOnPressBackGroundbutton", .01f);
	}

    public void InvokeOnPressBackGroundbutton()
    {
        if (this.animateHint.IsShowingHint())
        {
            this.animateHint.StopAnimating();
        }
        this.ToggleBackGroundSideMenu(0);
    }


    public void OnPresWigsForHerButton(GameObject g)
	{
		if (this.animateHint.IsShowingHint())
		{
			this.animateHint.StopAnimating();
		}
		if (!this.showingWig)
		{
			ShortcutExtensions46.DOFade(this.wig.gameObject.GetComponent<Image>(), 1f, 0.5f);
			this.showingWig = true;
		}
		else
		{
			ShortcutExtensions46.DOFade(this.wig.gameObject.GetComponent<Image>(), 0f, 0.5f);
			this.showingWig = false;
		}
	}

	public void OnPresShoesForHerButton(GameObject g)
	{
		if (this.animateHint.IsShowingHint())
		{
			this.animateHint.StopAnimating();
		}
		if (!this.showingShoe)
		{
			ShortcutExtensions46.DOFade(this.shoe.gameObject.GetComponent<Image>(), 1f, 0.5f);
			this.showingShoe = true;
		}
		else
		{
			ShortcutExtensions46.DOFade(this.shoe.gameObject.GetComponent<Image>(), 0f, 0.5f);
			this.showingShoe = false;
		}
	}

	public void OnPressGalleryButton(GameObject g)
	{
		if (this.animateHint.IsShowingHint())
		{
			this.animateHint.StopAnimating();
		}
		this.ToggleHomeSideMenu(2);
		this.isInHome = false;
		this.galleryController.OnOpenGallery();
		this.sceneEditorControllerObj.SetActive(false);
	}

	public void SetCurrentActive(GameObject activePanel = null, GameObject activeButton = null, GameObject activeDownPanel = null)
	{
		this.currentActiveButton = activeButton;
		this.currentActivePanel = activePanel;
		this.currentActiveDownPanel = activeDownPanel;
	}

	public void SetPreviousActive(GameObject activePanel = null, GameObject activeDownPanel = null, GameObject activeButton = null)
	{
		this.previousActiveButton = activeButton;
		this.previousActiveSidePanel = activePanel;
		this.previousActiveDownPanel = activeDownPanel;
	}

	public void SetPreviousActiveRootPanel(GameObject g)
	{
		this.previousActiveRootPanel = g;
	}

	public void ActiveCurrentActive(bool toggleDownPanelToo = false, bool reallyActiveCurrentActive = false)
	{
		if (this.isInHome)
		{
			this.ToggleHomeSideMenu(1);
		}
		else
		{
			
			if (this.currentActivePanel != null)
			{
				ShortcutExtensions46.DOAnchorPosX(this.currentActivePanel.GetComponent<RectTransform>(), 0f, 0.3f, false);
			}
			if (toggleDownPanelToo)
			{
				if (this.currentActiveDownPanel != null)
				{
					ShortcutExtensions46.DOAnchorPosY(this.currentActiveDownPanel.GetComponent<RectTransform>(), 0f, 0.3f, false);
				}
			}
		}
		if (reallyActiveCurrentActive)
		{
			this.panels[0].SetActive(true);
            if (this.currentActiveButton != null)
            {
                Toggle component = this.currentActiveButton.GetComponent<Toggle>();
                if (component != null)
                {
                    component.isOn = (true);
                }
            }
        }
	}

	public void ActivePreviousActive(bool toggleDownPanelToo = false, bool reallyActiveCurrentActive = false)
	{
		this.previousActiveRootPanel.SetActive(true);
		if (this.isInHome)
		{
			this.ToggleHomeSideMenu(1);
		}
		else
		{
			if (this.previousActiveButton != null)
			{
				Toggle component = this.currentActiveButton.GetComponent<Toggle>();
				if (component != null)
				{
					component.isOn=(true);
				}
			}
			if (this.previousActiveSidePanel != null)
			{
				ShortcutExtensions46.DOAnchorPosX(this.previousActiveSidePanel.GetComponent<RectTransform>(), 0f, 0.3f, false);
			}
			if (toggleDownPanelToo)
			{
				if (this.previousActiveDownPanel != null)
				{
					ShortcutExtensions46.DOAnchorPosY(this.previousActiveDownPanel.GetComponent<RectTransform>(), 0f, 0.3f, false);
				}
			}
		}
		if (reallyActiveCurrentActive)
		{
			this.panels[0].SetActive(true);
		}
	}

	public void DeactiveCurrentActive(bool toggleDownPanelToo = false, bool reallyHideEverything = false)
	{
		if (this.isInHome)
		{
			this.ToggleHomeSideMenu(2);
		}
		else
		{
			if (this.currentActiveButton != null)
			{
				Toggle component = this.currentActiveButton.GetComponent<Toggle>();
				if (component != null)
				{
					component.isOn=(false);
				}
			}
			if (this.currentActivePanel != null)
			{
				ShortcutExtensions46.DOAnchorPosX(this.currentActivePanel.GetComponent<RectTransform>(), -400f, 0.3f, false);
			}
			if (toggleDownPanelToo || reallyHideEverything)
			{
				if (this.currentActiveDownPanel != null)
				{
					ShortcutExtensions46.DOAnchorPosY(this.currentActiveDownPanel.GetComponent<RectTransform>(), -600f, 0.3f, false);
				}
			}
		}
		if (reallyHideEverything)
		{
			this.panels[0].SetActive(false);
		}
	}

	public void ChangeShape(int index)
	{
	}

	public void ChangeShape(GameObject newShape)
	{
	}

	public void Quit()
	{
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
			{
				EditorApplication.isPlaying=(false);
			}
#endif
        Application.Quit();
	}

	public void ShowLoading()
	{
		this.loadingPanelObject.SetActive(true);
		this.aml.StartRotatingStatic();
	}

	public void HideLoading()
	{
		this.loadingPanelObject.SetActive(false);
		this.aml.StopRotating();
	}

	public void ShowLoadingPanelOnly()
	{
        //this.eventSystem.SetActive(false);
        
		this.loadingPanelOnly.SetActive(true);
        print("loading panel only shown at : " + Time.deltaTime.ToString());
    }

	public void HideLoadingPanelOnly()
	{
		this.loadingPanelOnly.SetActive(false);
        print("loading panel only hide at : " + Time.deltaTime.ToString());
        //this.eventSystem.SetActive(true);
    }


    public void ShowLoadingPanelOnlyTransparent()
    {
        //this.eventSystem.SetActive(false);

        this.loadingPanelOnlyTransparent.SetActive(true);
        print("loading panel only transparent shown at : " + Time.deltaTime.ToString());
    }

    public void HideLoadingPanelOnlyTransparent()
    {
        this.loadingPanelOnlyTransparent.SetActive(false);
        print("loading panel only transparent hide at : " + Time.deltaTime.ToString());
        //this.eventSystem.SetActive(true);
    }

    public void ResetCroppedFace()
	{
		Color[] pixels = Enumerable.Repeat<Color>(Color.clear, this.faceRawImage.texture.width * this.faceRawImage.texture.height).ToArray<Color>();
		((Texture2D)this.faceRawImage.texture).SetPixels(pixels);
		((Texture2D)this.faceRawImage.texture).Apply();
		this.faceRawImage.gameObject.SetActive(false);
	}

	public void ChangeBodyToneColor(Color toneColor)
	{
		Image component = this.femaleModelImageObject.GetComponent<Image>();
		component.color=(toneColor);
	}

	public void ShowAcceptCropButton()
	{
		this.acceptCropButtonObject.SetActive(true);
	}

	public void HideAcceptCropButton()
	{
		this.acceptCropButtonObject.SetActive(false);
	}

	public void SaveDataToDisk(bool newSave = true)
	{
        print("Saving data to disk");
		if (this.IsPaidUser)
		{

            SaveData sd = new SaveData();
            print("New Save Data Created");
            DeActiveSceneEditorController();

            sd.Initialize(mainModelIndex,mainCarouselRotation,mainBodyShape, mainBodyTone, mainEyeColor,this);
            if (wig.gameObject.activeSelf && wig.transform.parent.gameObject.activeSelf && wig.color.a > 0.5f && selectDressController.isWearingWig)
            {
                //sd.wigName = wig.sprite.texture.name;
                print("wig property set");
                //currentFemaleWigProperty.SetFemaleWigColor(wig.color);

                sd.femaleWigProperty = new FemaleWigProperties();
                sd.femaleWigProperty.Clone(currentFemaleWigProperty);
                sd.femaleWigProperty.SetFemaleWigColor(wig.color);
            }
            if (dress.gameObject.activeSelf && dress.transform.parent.gameObject.activeSelf && dress.color.a > 0.5f  && selectDressController.isWearingDress)
            {
                //sd.dressName = dress.sprite.texture.name;
                
                print(currentDressProperty.imgName);
                //currentDressProperty.SetDressColor(dress.color);
                sd.dressProperty = new DressProperties();
                print(string.Format("Previous dressproperty color : {0} {1} {2} {3}", currentDressProperty.dressColor[0], currentDressProperty.dressColor[1], currentDressProperty.dressColor[2], currentDressProperty.dressColor[3]));
                sd.dressProperty.Clone( currentDressProperty);
                
                sd.dressProperty.SetDressColor(dress.color);
                print(string.Format("next dressproperty color : {0} {1} {2} {3}", currentDressProperty.dressColor[0], currentDressProperty.dressColor[1], currentDressProperty.dressColor[2], currentDressProperty.dressColor[3]));
                print("Current dress color is : " + currentDressColor.ToString());
                print("dress property set dress color is : "+sd.dressProperty.dressColor[0]+ " "+ sd.dressProperty.dressColor[1]+" " + sd.dressProperty.dressColor[2] + " "+ sd.dressProperty.dressColor[3]);

            }
            if (ornament.gameObject.activeSelf && ornament.transform.parent.gameObject.activeSelf && ornament.color.a > 0.5f && selectDressController.isWearingOrnament)
            {
                //sd.ornamentName = ornament.sprite.texture.name;
                print("ornament property set");
                sd.ornamentProperty = currentOrnamentProperty;
            }
            if (shoe.gameObject.activeSelf && shoe.transform.parent.gameObject.activeSelf && shoe.color.a > 0.5f && selectDressController.isWearingShoe)
            {
                //sd.shoeName = shoe.sprite.texture.name;
                print("shoe property set");
                sd.shoeProperty= currentShoeProperty;
            }

            print("Recheck started from gamecontroller");
            sd.ReCheckData(this);
            //print("After dress property set dress color is : " + sd.dressProperty.dressColor[0] + " " + sd.dressProperty.dressColor[1] + " " + sd.dressProperty.dressColor[2] + " " + sd.dressProperty.dressColor[3]);
            print("Recheck completed");
           int savestatus= SaveData.SaveWearings("wearingsdata.dat", sd, this);
            print("save status : " + savestatus);
            if(savestatus==0)
            {
                InstantiateInfoPopup("Maximum Save Data Reached");
                ActiveSceneEditorController();
            }
        }
		else
		{
			this.InstantiateInfoPopupForPurchase();
		}
	}

	public void CallBackFromSaveWearings(string fullPath)
	{
        sceneEditorController.gameObject.SetActive(false);
		base.StartCoroutine(this.TakeScreenshotAndSaveTo(fullPath));
	}

	
	public IEnumerator TakeScreenshotAndSaveTo(string fullPath)
	{
        
        DeactiveCurrentActive(true, true);
        ToggleOptionSideMenu(2);
        print(string.Format("Full Path : {0} ", fullPath));
        

        yield return new WaitForSecondsRealtime(.5f);
        ScreenCapture.CaptureScreenshot(fullPath);
        //yield return new WaitForSecondsRealtime(1f);
        //yield return new WaitForEndOfFrame();
        Texture2D ttx2d = new Texture2D(320, 480);
        if(Application.isMobilePlatform)
        {
            fullPath = Path.Combine(Application.persistentDataPath, fullPath);
        }

        print("full path now is : " + fullPath);
        int maxWait = 30;
        int currentWaitTime = 0;
        while(!File.Exists(fullPath))
        {
            yield return new WaitForSecondsRealtime(.3f);
            currentWaitTime += 1;
            if(currentWaitTime>maxWait)
            {
                break;
            }
        }

        if(!File.Exists(fullPath))
        {
            InstantiateInfoPopup("Could not Save Data");
        }
        else
        {
            yield return new WaitForSecondsRealtime(1f);
            ttx2d.LoadImage(File.ReadAllBytes(fullPath));
            ttx2d.Apply();
            ttx2d = cameraController.ResizeTexture2D(ttx2d, 320, 480);
            ttx2d.Apply();
            File.WriteAllBytes(fullPath, ttx2d.EncodeToJPG(50));
            //ttx2d = RTImage(MAINCAMERA, 320, 480);
            //ttx2d.Apply();
            //File.WriteAllBytes(fullPath, ttx2d.EncodeToJPG(50));

            //RenderTexture rt = MAINCAMERA.activeTexture;
            //rt.Create();

            //ScreenCapture.CaptureScreenshot()
        }

        yield return new WaitForSecondsRealtime(.5f);
        //Destroy(ttx2d, 2f);
        //ActiveCurrentActive(true, true);
        GoToHome();
    }


    public static Texture2D ResizeTexture2D(Texture2D texture, int newWidth, int newHeight)
    {
        float warpFactor = 1.0F;
        //		Texture2D destTex = new Texture2D(Screen.width, Screen.height);
        Texture2D destTex = new Texture2D(newWidth, newHeight);
        print("resize texture size : " + destTex.width + "   " + destTex.height);
        //		print ("Screen size : " + Screen.width + "   " + Screen.height);
        Color[] destPix = new Color[destTex.width * destTex.height];
        try
        {
            int y = 0;
            while (y < destTex.height)
            {
                int x = 0;
                while (x < destTex.width)
                {
                    float xFrac = x * 1.0F / (destTex.width - 1);
                    float yFrac = y * 1.0F / (destTex.height - 1);
                    float warpXFrac = Mathf.Pow(xFrac, warpFactor);
                    float warpYFrac = Mathf.Pow(yFrac, warpFactor);
                    destPix[y * destTex.width + x] = texture.GetPixelBilinear(warpXFrac, warpYFrac);
                    x++;
                }
                y++;
            }
            destTex.SetPixels(destPix);
            destTex.Apply();

        }
        catch
        {
            destTex = null;
        }

        //destImage.sprite = Sprite.Create(destTex, new Rect(0, 0, destTex.width, destTex.height), new Vector2(.5f, .5f), 100);

        return destTex;
    }



    /*
    Texture2D RTImage(Camera cam)
    {
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = cam.targetTexture;
        cam.Render();
        Texture2D image = new Texture2D(cam.targetTexture.width, cam.targetTexture.height);
        image.ReadPixels(new Rect(0, 0, cam.targetTexture.width, cam.targetTexture.height), 0, 0);
        image.Apply();
        RenderTexture.active = currentRT;
        return image;
    }

    Texture2D RTImage(Camera cam ,int newWidth,int newHeight)
    {
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = cam.targetTexture;
        cam.Render();
        Texture2D image = new Texture2D(cam.targetTexture.width, cam.targetTexture.height);
        image.ReadPixels(new Rect(0, 0, cam.targetTexture.width, cam.targetTexture.height), 0, 0);
        image.Apply();
        image = cameraController.ResizeTexture2D(image,newWidth,newHeight);
        image.Apply();
        RenderTexture.active = currentRT;
        return image;
    }

    */

    public void ToggleGameObject(GameObject togglableGameObject)
	{
		togglableGameObject.SetActive(!togglableGameObject.activeSelf);
	}

	public void ToggleGameObject(GameObject togglableGameObject, bool newState)
	{
		togglableGameObject.SetActive(newState);
	}

	public void HideGameObject(GameObject togglableGameObject)
	{
		togglableGameObject.SetActive(false);
	}

	public void DeActiveCroppedFace()
	{
		this.ToggleFace(false, true);
	}

	public void ToggleFace(bool newStatus = false, bool changeCustomFaceShowingStatus = false)
	{
		if (this.isUsingCustomFace)
		{
            this.ShowFaceImage(newStatus);
        }
		if (this.isUsingCustomFace2)
		{
			this.ShowFaceImage2(newStatus);
		}
		if (changeCustomFaceShowingStatus)
		{
			this.isUsingCustomFace = newStatus;
			this.isUsingCustomFace2 = newStatus;
		}
	}



	public void ToggleWig(bool changeEditButtonState = false)
	{
        selectDressController.RemoveWig();
        //selectDressController.isWearingWig = false;
	}

	public void ToggleDress(bool changeEditButtonState = false)
	{
        selectDressController.RemoveDress();
        //selectDressController.isWearingDress = false;

	}

	public void ToggleOrnament(bool changeEditButtonState = false)
	{
        selectDressController.RemoveOrnament();
        //selectDressController.isWearingOrnament = false;
	}

	public void ToggleShoe(bool changeEditButtonState = false)
	{
        selectDressController.RemoveShoe();
        //selectDressController.isWearingShoe = false;
	}

	public string GetMainBodyShape()
	{
		return this.mainBodyShape;
	}

	public string GetMainBodyTone()
	{
		return this.mainBodyTone;
	}

	public string GetMainEyeColor()
	{
		return this.mainEyeColor;
	}



    public void InstantiateNotInteractablePanel()
    {
        DestroyNotInteractablePopupPanel();
        print("Instantiating not interactable prefab");
        eventSystem.SetActive(false);
        NotInteractablePanelObject = Instantiate<GameObject>(NotInteractablePanelPrefab, canvas.transform);
    }

    public void DestroyNotInteractablePopupPanel()
    {
        if (NotInteractablePanelObject!=null)
        {
            print("Destroying not interactable prefab");
            
            DestroyImmediate(NotInteractablePanelObject);

        }
        eventSystem.SetActive(true);
    }



	public void AcceptBodyShapeChange(bool autoAccept = false)
	{
		GameObject selectedShape = this.rotationController.GetSelectedShape();
		try
		{
			string name = selectedShape.GetComponent<SpriteRenderer>().sprite.texture.name;
			this.femaleModelImageObject.GetComponent<Image>().sprite=(selectedShape.GetComponent<SpriteRenderer>().sprite);
			string b = name.Split(new char[]
			{
				'_'
			})[0];
			string b2 = name.Split(new char[]
			{
				'_'
			})[1];
			string b3 = name.Split(new char[]
			{
				'_'
			})[2];
			if (this.mainBodyShape != b || this.mainBodyTone != b2 || this.mainEyeColor != b3)
			{
				this.bodyChanged = true;
                ResetWearing();
                selectShapeController.ResetToDefaultColor();
            }
			this.mainBodyShape = b;
			this.mainBodyTone = b2;
			this.mainEyeColor = b3;
			this.mainCarouselRotation = this.selectShapeController.GetcarouselSelectedRotation();
			this.mainModelIndex = this.selectShapeController.GetCarouselSelectedModelIndex();
            
            

            if (!autoAccept)
            {
                this.selectShapeController.OnClickSelectEyeButton(true);
                return;
            }
            else
            {
                return;
            }
            if (!autoAcceptChange)
            {
                this.selectShapeController.OnClickSelectEyeButton(true);
                return;
            }

        }
		catch (UnityException arg)
		{
			MonoBehaviour.print("Error..in body shape accept  " + arg);
		}
	}

	public void AcceptBodyToneChange(bool autoAccept = false)
	{
		GameObject selectedShape = this.rotationController.GetSelectedShape();
		try
		{
			string name = selectedShape.GetComponent<SpriteRenderer>().sprite.texture.name;
			this.femaleModelImageObject.GetComponent<Image>().sprite=(selectedShape.GetComponent<SpriteRenderer>().sprite);
			//this.ResetWearing();
			string b = name.Split(new char[]
			{
				'_'
			})[0];
			string b2 = name.Split(new char[]
			{
				'_'
			})[1];
			string b3 = name.Split(new char[]
			{
				'_'
			})[2];
			if (this.mainBodyShape != b || this.mainBodyTone != b2 || this.mainEyeColor != b3)
			{
				this.bodyChanged = true;
			}
			this.mainBodyShape = b;
			this.mainBodyTone = b2;
			this.mainEyeColor = b3;
			this.mainCarouselRotation = this.selectShapeController.GetcarouselSelectedRotation();
			this.mainModelIndex = this.selectShapeController.GetCarouselSelectedModelIndex();
            if(!autoAccept)
            {
                this.selectShapeController.OnClickSelectShapeButton(true);
                return;
            }
            else 
            {
                return;
            }
            if(!autoAcceptChange)
            {
                this.selectShapeController.OnClickSelectShapeButton(true);
                return;
            }
			
		}
		catch
		{
		}
	}

	public void AcceptEyeColorChange(bool autoAccept=false)
	{
		GameObject selectedShape = this.rotationController.GetSelectedShape();
		try
		{
			string name = selectedShape.GetComponent<SpriteRenderer>().sprite.texture.name;
			this.femaleModelImageObject.GetComponent<Image>().sprite=(selectedShape.GetComponent<SpriteRenderer>().sprite);
			//this.ResetWearing();
			string b = name.Split(new char[]
			{
				'_'
			})[0];
			string b2 = name.Split(new char[]
			{
				'_'
			})[1];
			string b3 = name.Split(new char[]
			{
				'_'
			})[2];
			if (this.mainBodyShape != b || this.mainBodyTone != b2 || this.mainEyeColor != b3)
			{
				this.bodyChanged = true;
			}
			this.mainBodyShape = b;
			this.mainBodyTone = b2;
			this.mainEyeColor = b3;
			this.mainCarouselRotation = this.selectShapeController.GetcarouselSelectedRotation();
			this.mainModelIndex = this.selectShapeController.GetCarouselSelectedModelIndex();
			
            if (!autoAccept)
            {
                this.selectShapeController.OnClickSelectBodyToneButton(true);
                return;
            }
            else if (autoAccept)
            {
                return;
            }
            if (!autoAcceptChange)
            {
                this.selectShapeController.OnClickSelectBodyToneButton(true);
                return;
            }
        }
		catch
		{
		}
	}

	public void ResetWearing()
	{
		this.selectDressController.SetIsWearingDress(false);
		this.selectDressController.SetIsWearingWig(false);
		this.selectDressController.SetIsWearingOrnament(false);
		this.selectDressController.SetIsWearingShoe(false);
	}

	public void OnClickSavedFacesButton()
	{
		if (this.IsPaidUser)
		{
            ShowLoadingPanelOnly();
            Invoke("ToggleSavedFacesPanel0", .2f);
		}
		else
		{
			this.InstantiateInfoPopupForPurchase();
		}
	}

	public void OnClickSavedLooksButton()
	{
		if (this.IsPaidUser)
		{
            ShowLoadingPanelOnly();
			Invoke("ToggleSavedLooksPanel0", .3f);
		}
		else
		{
			this.InstantiateInfoPopupForPurchase();
		}
	}

	public void SaveCroppedFace()
	{
		this.popupController.HidePopup();
		if (this.saveDict["face1"])
		{
			CroppedFaceData croppedFaceData = new CroppedFaceData();
			croppedFaceData.Initialize(this.faceRawImage);
			int num = CroppedFaceData.SaveCroppedFace("croppedfaces.dat", croppedFaceData, this.faceRawImage);
			if (num != 0)
			{
				if (num == 1)
				{
					this.saveDict["face1"] = false;
					this.isLoadedFace = true;
					this.faceHash = croppedFaceData.saveFaceHash;
                    
					//base.StartCoroutine(this.saveManager.ResetAllCroppedFaces());
				}
			}
			else
			{
				this.popupController.ShowPopup(1, "You can save 5 faces only!");
			}
		}
		if (this.saveDict["face2"])
		{
			CroppedFaceData croppedFaceData2 = new CroppedFaceData();
			croppedFaceData2.Initialize(this.faceRawImage2);
			int num2 = CroppedFaceData.SaveCroppedFace("croppedfaces.dat", croppedFaceData2, this.faceRawImage2);
			if (num2 != 0)
			{
				if (num2 == 1)
				{
					this.saveDict["face2"] = false;
					this.isLoadedFace2 = true;
					this.faceHash2 = croppedFaceData2.saveFaceHash;
					//base.StartCoroutine(this.saveManager.ResetAllCroppedFaces());
				}
			}
			else
			{
				this.popupController.ShowPopup(1, "You can save 5 faces only!");
			}
		}
		base.StartCoroutine(this.saveManager.ResetAllCroppedFaces());
	}

	public void DeactiveMale()
	{
		this.HideMale();
	}

	public void ShowMale()
	{
		this.maleImage.gameObject.SetActive(true);
        //isShowingMale = true;
		this.maleParent.SetActive(true);
	}

	public void HideMale()
	{
		this.maleImage.gameObject.SetActive(false);
		this.maleParent.SetActive(false);

	}

    public void RemoveMale()
    {
        HideMale();
        hideMaleButton.SetActive(false);
        if (faceRawImage.transform.parent.parent == maleImage.transform)
        {
            RemoveFace1();

        }
        else if(faceRawImage2.transform.parent.parent == maleImage.transform)
        {
            RemoveFace2();

        }

        maleController.RemoveMaleWig();
        maleController.RemoveMaleTie();

        isShowingMale = false;
    }

	public void ToggleMaleWig(bool changeEditButtonState = false)
	{
        //this.maleWig.transform.parent.gameObject.SetActive(false);
        maleController.RemoveMaleWig();
		if (changeEditButtonState)
		{
			if ((double)this.maleWig.color.a > 0.5)
			{
				this.maleController.editButtons[0].SetActive(this.maleWig.transform.parent.gameObject.activeSelf);
			}
			else
			{
				this.maleController.editButtons[0].SetActive(false);
			}
		}
		else
		{
			this.maleController.editButtons[0].SetActive(false);
		}
	}

	public void ToggleMaleTie(bool changeEditButtonState = false)
	{
        //this.maleTie.transform.parent.gameObject.SetActive(false);
        maleController.RemoveMaleTie();
        if (changeEditButtonState)
		{
			if ((double)this.maleTie.color.a > 0.5)
			{
				this.maleController.editButtons[1].SetActive(this.maleTie.transform.parent.gameObject.activeSelf);
			}
			else
			{
				this.maleController.editButtons[1].SetActive(false);
			}
		}
		else
		{
			this.maleController.editButtons[1].SetActive(false);
		}
	}

	public string GetMainMaleBodyName()
	{
		return this.mainMaleBodyName;
	}

	public void SetMainMaleBodyName(string val)
	{
		this.mainMaleBodyName = val;
	}

	public void AcceptMaleBody(bool goToNextPage=true)
	{
		string carouselSelectedMaleModel = this.maleController.GetCarouselSelectedMaleModel();
		this.maleImage.sprite=(this.maleController.GetCarouselSelectedaleModelObject().GetComponent<SpriteRenderer>().sprite);

        if(mainMaleBodyName!= this.maleImage.sprite.texture.name)
        {
            maleController.RemoveMaleWig();
            maleController.RemoveMaleTie();
        }
		this.SetMainMaleBodyName(this.maleImage.sprite.texture.name);
		this.isShowingMale = true;
        maleController.SelectedMale = mainMaleBodyName;
        maleController.SetCurrentMaleIndexAndRotation();
        hideMaleButton.SetActive(true);
		if(goToNextPage)
        {
            this.maleController.OnClickSelectWigsForMale(true);
        }
	}



    public void ChangeToGrayScale(Image targetImage)
    {
        Color[] pixels = targetImage.sprite.texture.GetPixels();
        for (int i = 0; i < pixels.Length; i++)
        {
            if (pixels[i].a > 0.5f)
            {
                pixels[i] = new Color(pixels[i].grayscale, pixels[i].grayscale, pixels[i].grayscale, pixels[i].a);
            }
        }
        Texture2D texture2D = new Texture2D(targetImage.sprite.texture.width, targetImage.sprite.texture.height);
        texture2D.SetPixels(pixels);
        texture2D.Apply();
        targetImage.sprite = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f), 100f);
    }

    public void ChangeToGrayScale(RawImage targetImage)
    {
        Color[] pixels = ((Texture2D)(targetImage.texture)).GetPixels();
        for (int i = 0; i < pixels.Length; i++)
        {
            if (pixels[i].a > 0.5f)
            {
                pixels[i] = new Color(pixels[i].grayscale, pixels[i].grayscale, pixels[i].grayscale, pixels[i].a);
            }
        }
        //Texture2D texture2D = new Texture2D(targetImage.texture.width, targetImage.texture.height);
        //texture2D.SetPixels(pixels);
        //texture2D.Apply();
        ((Texture2D)(targetImage.texture)).SetPixels(pixels);
        ((Texture2D)(targetImage.texture)).Apply();
    }

    public void ChangeToGrayScale(Texture2D targetImage)
    {
        Color[] pixels = targetImage.GetPixels();
        for (int i = 0; i < pixels.Length; i++)
        {
            if (pixels[i].a > 0.5f)
            {
                pixels[i] = new Color(pixels[i].grayscale, pixels[i].grayscale, pixels[i].grayscale, pixels[i].a);
            }
        }
        //Texture2D texture2D = new Texture2D(targetImage.texture.width, targetImage.texture.height);
        //texture2D.SetPixels(pixels);
        //texture2D.Apply();
        targetImage.SetPixels(pixels);
        targetImage.Apply();
    }




    #region MALEFEMALETOGGLESCENE
    public void ShowMaleFemaleSelectionPopup()
    {
        if(isShowingMale)
        {
            GameObject g = Instantiate<GameObject>(maleFemlaeSelectionPopup, canvas.transform);
            maleFemaleSelectionToggle = new Toggle[2];
            maleFemaleSelectionToggle[0] = g.transform.GetChild(0).GetChild(0).GetComponent<Toggle>();
            maleFemaleSelectionToggle[1] = g.transform.GetChild(0).GetChild(1).GetComponent<Toggle>();

            maleFemaleSelectionToggle[0].onValueChanged.RemoveAllListeners();
            maleFemaleSelectionToggle[0].onValueChanged.AddListener( this.OnFemaleSelectionToggleChange);

            maleFemaleSelectionToggle[1].onValueChanged.RemoveAllListeners();
            maleFemaleSelectionToggle[1].onValueChanged.AddListener(this.OnMaleSelectionToggleChange);


            g.transform.GetChild(0).GetChild(2).GetComponent<Button>().onClick.AddListener(() =>
            {
                faceEditController.DiscardFaceEdit();
                DestroyImmediate(g);
            });

            g.transform.GetChild(0).GetChild(3).GetComponent<Button>().onClick.AddListener(()=>
            {



                if(!maleModelIsZoomed && ! femaleModelIsZoomed)
                {
                    //InstantiateInfoPopup("Please Select A Model To Use The Face On");
                    //faceEditController.DiscardFaceEdit();
                    //DestroyImmediate(g);
                    return;
                }
                if(temporarilyHiddenFace==1)
                {
                    if(maleModelIsZoomed)
                    {
                        
                        UseOnlySingleFaceOnAModel(false,true);
                        ZoomInMaleModel(true);
                    }
                    else if(femaleModelIsZoomed)
                    {
                        
                        UseOnlySingleFaceOnAModel(true,true);
                        ZoomInFemaleModel(true);
                    }
                    saveDict["face1"] = false;
                    RemoveFace1();
                }
                else if(temporarilyHiddenFace==2)
                {
                    if (maleModelIsZoomed)
                    {
                        
                        UseOnlySingleFaceOnAModel(false,true);
                        ZoomInMaleModel(true);
                    }
                    else if (femaleModelIsZoomed)
                    {
                        
                        UseOnlySingleFaceOnAModel(true,true);
                        ZoomInFemaleModel(true);
                    }
                    saveDict["face2"] = false;
                    RemoveFace2();
                }
                else if(temporarilyHiddenFace==-1)
                {
                    if (maleModelIsZoomed)
                    {
                        print("currently using face  on male");
                        
                        UseOnlySingleFaceOnAModel(false,true);
                        ZoomInMaleModel(true);
                    }
                    else if (femaleModelIsZoomed)
                    {
                        print("currently using face on female");
                        
                        UseOnlySingleFaceOnAModel(true,true);
                        ZoomInFemaleModel(true);
                        
                    }
                    if (currentlySelectedFace == 1)
                    {
                        //saveDict["face1"] = false;
                        //RemoveFace1();
                        faceColorController.SetTarget(faceRawImage);
                        faceBrightnessController.SetTarget(faceRawImage);
                        faceSaturationController.SetTarget(faceRawImage);
                    }
                    else if (currentlySelectedFace == 2)
                    {
                        //saveDict["face2"] = false;
                        //RemoveFace2();
                        faceColorController.SetTarget(faceRawImage2);
                        faceBrightnessController.SetTarget(faceRawImage2);
                        faceSaturationController.SetTarget(faceRawImage2);
                    }
                }
                faceEditController.ActivateTouchOnCroppedFace();
                temporarilyHiddenFace = -1;

                //print("Save dicet : " + saveDict);

                if (faceEditController.backIsImageProcessingScreen)
                {
                    if (currentlySelectedFace == 1)
                    {
                        faceRawImage.color = new Color(0.5f, 0.5f, 0.5f);
                        faceRawImage.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                        currentFaceColor = 0.5f;
                        currentFaceBrightness = 0.5f;
                        currentFaceSaturation = 0.5f;
                    }
                    else if (currentlySelectedFace == 2)
                    {
                        faceRawImage2.color = new Color(0.5f, 0.5f, 0.5f);
                        faceRawImage2.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                        currentFaceColor2 = 0.5f;
                        currentFaceBrightness2 = 0.5f;
                        currentFaceSaturation2 = 0.5f;
                    }
                }

                DestroyImmediate(g);

            });
        }
        else
        {
            //touchController.ApplyMaskImageTemp();
            faceEditController.ActivateTouchOnCroppedFace();
            UseOnlySingleFaceOnAModel(true,true);
            //OnFemaleSelectionToggleChange(true);
            ZoomInFemaleModel();
            faceColorController.SetTarget(faceRawImage);
            faceBrightnessController.SetTarget(faceRawImage);
            faceSaturationController.SetTarget(faceRawImage);
            //print("Save dicet : " + saveDict);

            if (faceEditController.backIsImageProcessingScreen)
            {
                if (currentlySelectedFace == 1)
                {
                    faceRawImage.color = new Color(0.5f, 0.5f, 0.5f);
                    faceRawImage.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                    currentFaceColor = 0.5f;
                    currentFaceBrightness = 0.5f;
                    currentFaceSaturation = 0.5f;
                }
                else if (currentlySelectedFace == 2)
                {
                    faceRawImage2.color = new Color(0.5f, 0.5f, 0.5f);
                    faceRawImage2.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                    currentFaceColor2 = 0.5f;
                    currentFaceBrightness2 = 0.5f;
                    currentFaceSaturation2 = 0.5f;
                }
            }


        }

        

        //if(faceEditController.backIsImageProcessingScreen)
        //{
        //    if(currentlySelectedFace==1)
        //    {
        //        faceRawImage.color = new Color(0.5f, 0.5f, 0.5f);

        //        currentFaceColor = 0.5f;
        //        currentFaceBrightness = 0.5f;
        //        currentFaceSaturation = 0.5f;
        //    }
        //    else if(currentlySelectedFace==2)
        //    {
        //        faceRawImage2.color = new Color(0.5f, 0.5f, 0.5f);

        //        currentFaceColor2 = 0.5f;
        //        currentFaceBrightness2 = 0.5f;
        //        currentFaceSaturation2 = 0.5f;
        //    }
        //}
        
    }

    public void ShowMaleFemaleSelectionPopup(Texture2D faceTexture,Vector3 scale,Vector3 rotation, int imageIndex, int faceHash,Color col, bool backIsImageProcessingScreen=false)
    {
        if (isShowingMale)
        {
            GameObject g = Instantiate<GameObject>(maleFemlaeSelectionPopup, canvas.transform);
            maleFemaleSelectionToggle = new Toggle[2];
            maleFemaleSelectionToggle[0] = g.transform.GetChild(0).GetChild(0).GetComponent<Toggle>();
            maleFemaleSelectionToggle[1] = g.transform.GetChild(0).GetChild(1).GetComponent<Toggle>();

            maleFemaleSelectionToggle[0].onValueChanged.RemoveAllListeners();
            maleFemaleSelectionToggle[0].onValueChanged.AddListener(this.OnFemaleSelectionToggleChange);

            maleFemaleSelectionToggle[1].onValueChanged.RemoveAllListeners();
            maleFemaleSelectionToggle[1].onValueChanged.AddListener(this.OnMaleSelectionToggleChange);


            g.transform.GetChild(0).GetChild(2).GetComponent<Button>().onClick.AddListener(() =>
            {
                faceEditController.DiscardFaceEdit();
                DestroyImmediate(g);
            });

            g.transform.GetChild(0).GetChild(3).GetComponent<Button>().onClick.AddListener(() =>
            {



                if (!maleModelIsZoomed && !femaleModelIsZoomed)
                {
                    //InstantiateInfoPopup("Please Select A Model To Use The Face On");
                    //faceEditController.DiscardFaceEdit();
                    //DestroyImmediate(g);
                    return;
                }
                if (temporarilyHiddenFace == 1)
                {
                    if (maleModelIsZoomed)
                    {

                        UseOnlySingleFaceOnAModel(faceTexture,false);
                        ZoomInMaleModel(true);
                    }
                    else if (femaleModelIsZoomed)
                    {

                        UseOnlySingleFaceOnAModel(faceTexture);
                        ZoomInFemaleModel(true);
                    }
                    saveDict["face1"] = false;
                    RemoveFace1();
                }
                else if (temporarilyHiddenFace == 2)
                {
                    if (maleModelIsZoomed)
                    {

                        UseOnlySingleFaceOnAModel(faceTexture, false);
                        ZoomInMaleModel(true);
                    }
                    else if (femaleModelIsZoomed)
                    {

                        UseOnlySingleFaceOnAModel(faceTexture);
                        ZoomInFemaleModel(true);
                    }
                    saveDict["face2"] = false;
                    RemoveFace2();
                }
                else if (temporarilyHiddenFace == -1)
                {
                    if (maleModelIsZoomed)
                    {
                        print("currently using face  on male");

                        UseOnlySingleFaceOnAModel(faceTexture, false);
                        ZoomInMaleModel(true);
                    }
                    else if (femaleModelIsZoomed)
                    {
                        print("currently using face on female");

                        UseOnlySingleFaceOnAModel(faceTexture);
                        ZoomInFemaleModel(true);

                    }
                    if (currentlySelectedFace == 1)
                    {
                        //saveDict["face1"] = false;
                        //RemoveFace1();
                        faceColorController.SetTarget(faceRawImage);
                        faceBrightnessController.SetTarget(faceRawImage);
                        faceSaturationController.SetTarget(faceRawImage);
                    }
                    else if (currentlySelectedFace == 2)
                    {
                        //saveDict["face2"] = false;
                        //RemoveFace2();
                        faceColorController.SetTarget(faceRawImage2);
                        faceBrightnessController.SetTarget(faceRawImage2);
                        faceSaturationController.SetTarget(faceRawImage2);
                    }
                }
                faceEditController.ActivateTouchOnCroppedFace();
                temporarilyHiddenFace = -1;

                
                

                if (currentlySelectedFace == 1)
                {
                    faceRawImage.transform.localEulerAngles = rotation;
                    faceRawImage.transform.localScale = scale;
                    faceRawImage.color = col;

                    currentFaceBrightness = col.b;
                    currentFaceColor = col.r;
                    currentFaceSaturation = col.g;

                    tempCroppedFaceProperty.Copy(ref loadedCroppedFaceProperty);

                    print("ChabgingScaleOfFace1");
                    isLoadedFace = true;
                    this.faceHash = faceHash;
                    this.loadedFaceIndex = imageIndex;
                    saveDict["face2"] = false;
                }
                else if (currentlySelectedFace == 2)
                {
                    faceRawImage2.transform.localEulerAngles = rotation;
                    faceRawImage2.transform.localScale = scale;
                    faceRawImage2.color = col;

                    tempCroppedFaceProperty.Copy(ref loadedCroppedFaceProperty2);

                    currentFaceBrightness2 = col.b;
                    currentFaceColor2 = col.r;
                    currentFaceSaturation2 = col.g;

                    print("ChangingScaleOfFace1");
                    isLoadedFace2 = true;
                    this.faceHash2 = faceHash;
                    this.loadedFaceIndex2 = imageIndex;
                    saveDict["face2"] = false;
                }

                if(isLoadedFace)
                {
                    saveDict["face1"] = false;
                }
                if(isLoadedFace2)
                {
                    saveDict["face2"] = false;
                }

                DestroyImmediate(g);

            });
        }
        else
        {
            //touchController.ApplyMaskImageTemp();
            faceEditController.ActivateTouchOnCroppedFace();
            UseOnlySingleFaceOnAModel(faceTexture);
            if (currentlySelectedFace == 1)
            {
                faceRawImage.transform.localEulerAngles = rotation;
                faceRawImage.transform.localScale = scale;
                faceRawImage.color = col;

                currentFaceBrightness = col.b;
                currentFaceColor = col.r;
                currentFaceSaturation = col.g;


                tempCroppedFaceProperty.Copy(ref loadedCroppedFaceProperty);

                print("loaded face property : " + loadedCroppedFaceProperty.cfd.imageName);

                print("ChangingScaleOfFace1");
                isLoadedFace = true;
                this.faceHash = faceHash;
                this.loadedFaceIndex = imageIndex;
            }
            else if (currentlySelectedFace == 2)
            {
                faceRawImage2.transform.localEulerAngles = rotation;
                faceRawImage2.transform.localScale = scale;
                faceRawImage2.color = col;

                currentFaceBrightness2 = col.b;
                currentFaceColor2 = col.r;
                currentFaceSaturation2 = col.g;

                tempCroppedFaceProperty.Copy(ref loadedCroppedFaceProperty2);

                print("ChangingScaleOfFace2");
                isLoadedFace2 = true;
                this.faceHash2 = faceHash;
                this.loadedFaceIndex2 = imageIndex;

               
            }
            //OnFemaleSelectionToggleChange(true);
            ZoomInFemaleModel();
            faceColorController.SetTarget(faceRawImage);
            faceBrightnessController.SetTarget(faceRawImage);
            faceSaturationController.SetTarget(faceRawImage);
            print("Save dicet : " + saveDict);
        }



        //if (faceEditController.backIsImageProcessingScreen)
        //{
        //    if (currentlySelectedFace == 1)
        //    {
        //        faceRawImage.color = new Color(0.5f, 0.5f, 0.5f);
        //    }
        //    else if (currentlySelectedFace == 2)
        //    {
        //        faceRawImage2.color = new Color(0.5f, 0.5f, 0.5f);
        //    }
        //}

        


    }


    public void SetPreviousFaceDetail(int currentSelectedIndex)
    {
        if(currentSelectedIndex==1 && previouslyUsingFace1)
        {
            previousFaceIndex = 1;
            if(isLoadedFace)
            {
                previousFaceHash = faceHash;
                previousLoadedFaceIndex = loadedFaceIndex;
                previouslyLoaded = isLoadedFace;
            }
            else
            {
                previousFaceHash = -999;
                previousLoadedFaceIndex = -1;
                previouslyLoaded = false;
            }
            previousColor = faceRawImage.color;
            DestroyImmediate(previousImagetexture, true);
            previousImagetexture = new Texture2D(faceRawImage.texture.width, faceRawImage.texture.height);
            previousImagetexture.SetPixels(((Texture2D)(faceRawImage.texture)).GetPixels());
            previousImagetexture.Apply();
            previousPosition = faceRawImage.rectTransform.anchoredPosition3D;
            previousScale = faceRawImage.transform.localScale;
            previousRotation = faceRawImage.transform.localEulerAngles;
            previousSizeDelta = faceRawImage.rectTransform.sizeDelta;
        }
        else if (currentSelectedIndex == 2 && previouslyUsingFace2)
        {
            if (isLoadedFace2)
            {
                previousFaceHash = faceHash2;
                previousLoadedFaceIndex = loadedFaceIndex2;
                previouslyLoaded = isLoadedFace2;
            }
            else
            {
                previousFaceHash = -999;
                previousLoadedFaceIndex = -1;
                previouslyLoaded = false;
            }

            previousFaceIndex = 2;
            previousColor = faceRawImage2.color;
            DestroyImmediate(previousImagetexture, true);
            previousImagetexture = new Texture2D(faceRawImage2.texture.width, faceRawImage2.texture.height);
            previousImagetexture.SetPixels(((Texture2D)(faceRawImage2.texture)).GetPixels());
            previousImagetexture.Apply();
            previousPosition = faceRawImage2.rectTransform.anchoredPosition3D;
            previousScale = faceRawImage2.transform.localScale;
            previousRotation = faceRawImage2.transform.localEulerAngles;
            previousSizeDelta = faceRawImage2.rectTransform.sizeDelta;
        }
    }

    public void OnFemaleSelectionToggleChange(bool newState)
    {
        if(newState)
        {
            //UseOnlySingleFaceOnAModel(true);
            ZoomInFemaleModel();
        }
        else
        {
            ZoomOutFemaleModel();
        }
    }

    public void OnMaleSelectionToggleChange(bool newState)
    {
        if (newState)
        {
            //UseOnlySingleFaceOnAModel(false);
            ZoomInMaleModel();
        }
        else
        {
            ZoomOutMaleModel();
        }
    }



    public void UseOnlySingleFaceOnAModel(bool femaleToZoom=true,bool useImageFromTempImage=false)
    {
        if (femaleToZoom)
        {
            if (currentlySelectedFace == 1)
            {
                if(useImageFromTempImage)
                {
                    if(faceRawImage.transform.parent.parent==femaleModelImageObject.transform)
                    {
                        print("faceRawImage.transform.parent.parent==femaleModelImageObject.transform");
                        SetPreviousFaceDetail(1);
                        DestroyImmediate(faceRawImage.texture);
                        faceRawImage.texture = new Texture2D(temporalFaceImage.width, temporalFaceImage.height) as Texture;
                        (faceRawImage.texture as Texture2D).SetPixels(temporalFaceImage.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage.texture as Texture2D).Apply();
                        faceRawImage.transform.parent.gameObject.SetActive(true);

                        faceRawImage.rectTransform.sizeDelta = new Vector2(temporalFaceImage.width, temporalFaceImage.height);
                        faceRawImage.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage.rectTransform.sizeDelta / 2f;
                        faceRawImage.gameObject.SetActive(true);
                        DestroyImmediate(temporalFaceImage);
                        currentlySelectedFace = 1;
                        saveDict["face1"] = true;
                        UsingCustomFace(true);
                        ShowFaceImage(true);
                        return;
                    }
                    else if(faceRawImage2.transform.parent.parent== femaleModelImageObject.transform)
                    {
                        print("faceRawImage2.transform.parent.parent== femaleModelImageObject.transform");
                        SetPreviousFaceDetail(2);
                        DestroyImmediate(faceRawImage2.texture);
                        faceRawImage2.texture = new Texture2D(temporalFaceImage.width, temporalFaceImage.height) as Texture;
                        (faceRawImage2.texture as Texture2D).SetPixels(temporalFaceImage.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage2.texture as Texture2D).Apply();
                        faceRawImage2.transform.parent.gameObject.SetActive(true);
                        faceRawImage2.rectTransform.sizeDelta = new Vector2(temporalFaceImage.width, temporalFaceImage.height);
                        faceRawImage2.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage2.rectTransform.sizeDelta / 2f;
                        faceRawImage2.gameObject.SetActive(true);
                        DestroyImmediate(temporalFaceImage);
                        currentlySelectedFace = 2;
                        saveDict["face2"] = true;
                        UsingCustomFace2(true);
                        ShowFaceImage2(true);
                        return;
                    }
                    else if (faceRawImage.transform.parent.parent == maleImage.transform)
                    {
                        print("faceRawImage.transform.parent.parent == maleImage.transform");
                        SetPreviousFaceDetail(2);
                        DestroyImmediate(faceRawImage2.texture);
                        faceRawImage2.texture = new Texture2D(temporalFaceImage.width, temporalFaceImage.height) as Texture;
                        (faceRawImage2.texture as Texture2D).SetPixels(temporalFaceImage.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage2.texture as Texture2D).Apply();
                        faceRawImage2.transform.parent.gameObject.SetActive(true);
                        faceRawImage2.rectTransform.sizeDelta = new Vector2(temporalFaceImage.width, temporalFaceImage.height);
                        faceRawImage2.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage2.rectTransform.sizeDelta / 2f;
                        faceRawImage2.gameObject.SetActive(true);
                        DestroyImmediate(temporalFaceImage);
                        currentlySelectedFace = 2;
                        saveDict["face2"] = true;
                        UsingCustomFace2(true);
                        ShowFaceImage2(true);
                        return;
                    }

                    else if (faceRawImage2.transform.parent.parent == maleImage.transform)
                    {
                        print("faceRawImage2.transform.parent.parent == maleImage.transform");
                        SetPreviousFaceDetail(1);
                        DestroyImmediate(faceRawImage.texture);
                        faceRawImage.texture = new Texture2D(temporalFaceImage.width, temporalFaceImage.height) as Texture;
                        (faceRawImage.texture as Texture2D).SetPixels(temporalFaceImage.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage.texture as Texture2D).Apply();
                        faceRawImage.transform.parent.gameObject.SetActive(true);

                        faceRawImage.rectTransform.sizeDelta = new Vector2(temporalFaceImage.width, temporalFaceImage.height);
                        faceRawImage.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage.rectTransform.sizeDelta / 2f;
                        faceRawImage.gameObject.SetActive(true);
                        DestroyImmediate(temporalFaceImage);
                        currentlySelectedFace = 1;
                        saveDict["face1"] = true;
                        UsingCustomFace(true);
                        ShowFaceImage(true);
                        return;
                        
                    }
                    else
                    {
                        print("using face 1 on female");
                        SetPreviousFaceDetail(1);
                        DestroyImmediate(faceRawImage.texture);
                        faceRawImage.texture = new Texture2D(temporalFaceImage.width, temporalFaceImage.height) as Texture;
                        (faceRawImage.texture as Texture2D).SetPixels(temporalFaceImage.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage.texture as Texture2D).Apply();
                        faceRawImage.transform.parent.gameObject.SetActive(true);

                        faceRawImage.rectTransform.sizeDelta = new Vector2(temporalFaceImage.width, temporalFaceImage.height);
                        faceRawImage.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage.rectTransform.sizeDelta / 2f;
                        faceRawImage.gameObject.SetActive(true);
                        DestroyImmediate(temporalFaceImage);
                        currentlySelectedFace = 1;
                        saveDict["face1"] = true;
                        UsingCustomFace(true);
                        ShowFaceImage(true);
                        return;
                    }
                }
                //if (isShowingFace2)
                //{
                //    if (faceRawImage2.transform.parent.parent == femaleModelImageObject.transform)
                //    {
                //        //InstantiateInfoPopup("Only one face can be placed on a single model");
                //        //return;
                        
                //        temporarilyHiddenFace = 2;
                //        faceRawImage2.transform.parent.gameObject.SetActive(false);
                //    }
                //    else if (faceRawImage2.transform.parent.parent == maleImage.transform)
                //    {
                //        if(temporarilyHiddenFace==2)
                //        {
                            
                //            faceRawImage2.transform.parent.gameObject.SetActive(true);
                //            temporarilyHiddenFace = -1;
                //        }
                //    }
                //}

                return;
                
            }
            else if (currentlySelectedFace == 2)
            {

                if (useImageFromTempImage)
                {
                    if (faceRawImage.transform.parent.parent == femaleModelImageObject.transform)
                    {
                        print("faceRawImage.transform.parent.parent == femaleModelImageObject.transform");
                        SetPreviousFaceDetail(1);
                        DestroyImmediate(faceRawImage.texture);
                        faceRawImage.texture = new Texture2D(temporeaFaceImage2.width, temporeaFaceImage2.height) as Texture;
                        (faceRawImage.texture as Texture2D).SetPixels(temporeaFaceImage2.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage.texture as Texture2D).Apply();
                        faceRawImage.transform.parent.gameObject.SetActive(true);

                        faceRawImage.rectTransform.sizeDelta = new Vector2(temporeaFaceImage2.width, temporeaFaceImage2.height);
                        faceRawImage.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage.rectTransform.sizeDelta / 2f;
                        faceRawImage.gameObject.SetActive(true);
                        DestroyImmediate(temporeaFaceImage2);
                        currentlySelectedFace = 1;
                        saveDict["face1"] = true;
                        UsingCustomFace(true);
                        ShowFaceImage(true);
                        return;
                    }
                    else if (faceRawImage2.transform.parent.parent == femaleModelImageObject.transform)
                    {
                        print("faceRawImage2.transform.parent.parent == femaleModelImageObject.transform");
                        SetPreviousFaceDetail(2);
                        DestroyImmediate(faceRawImage2.texture);
                        faceRawImage2.texture = new Texture2D(temporeaFaceImage2.width, temporeaFaceImage2.height) as Texture;
                        (faceRawImage2.texture as Texture2D).SetPixels(temporeaFaceImage2.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage2.texture as Texture2D).Apply();
                        faceRawImage2.transform.parent.gameObject.SetActive(true);
                        faceRawImage2.rectTransform.sizeDelta = new Vector2(temporeaFaceImage2.width, temporeaFaceImage2.height);
                        faceRawImage2.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage2.rectTransform.sizeDelta / 2f;
                        faceRawImage2.gameObject.SetActive(true);
                        DestroyImmediate(temporeaFaceImage2);
                        currentlySelectedFace = 2;
                        saveDict["face2"] = true;
                        UsingCustomFace2(true);
                        ShowFaceImage2(true);
                        return;
                    }
                    else if (faceRawImage.transform.parent.parent == maleImage.transform)
                    {
                        print("faceRawImage.transform.parent.parent == maleImage.transform");
                        SetPreviousFaceDetail(2);
                        DestroyImmediate(faceRawImage2.texture);
                        faceRawImage2.texture = new Texture2D(temporalFaceImage.width, temporalFaceImage.height) as Texture;
                        (faceRawImage2.texture as Texture2D).SetPixels(temporalFaceImage.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage2.texture as Texture2D).Apply();
                        faceRawImage2.transform.parent.gameObject.SetActive(true);
                        faceRawImage2.rectTransform.sizeDelta = new Vector2(temporalFaceImage.width, temporalFaceImage.height);
                        faceRawImage2.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage2.rectTransform.sizeDelta / 2f;
                        faceRawImage2.gameObject.SetActive(true);
                        DestroyImmediate(temporalFaceImage);
                        currentlySelectedFace = 2;
                        saveDict["face2"] = true;
                        UsingCustomFace2(true);
                        ShowFaceImage2(true);
                        return;
                    }

                    else if (faceRawImage2.transform.parent.parent == maleImage.transform)
                    {
                        print("faceRawImage2.transform.parent.parent == maleImage.transform");
                        SetPreviousFaceDetail(1);
                        DestroyImmediate(faceRawImage.texture);
                        faceRawImage.texture = new Texture2D(temporalFaceImage.width, temporalFaceImage.height) as Texture;
                        (faceRawImage.texture as Texture2D).SetPixels(temporalFaceImage.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage.texture as Texture2D).Apply();
                        faceRawImage.transform.parent.gameObject.SetActive(true);

                        faceRawImage.rectTransform.sizeDelta = new Vector2(temporalFaceImage.width, temporalFaceImage.height);
                        faceRawImage.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage.rectTransform.sizeDelta / 2f;
                        faceRawImage.gameObject.SetActive(true);
                        DestroyImmediate(temporalFaceImage);
                        currentlySelectedFace = 1;
                        saveDict["face1"] = true;
                        UsingCustomFace(true);
                        ShowFaceImage(true);
                        return;

                    }
                    else
                    {
                        print("using face 2 on female");
                        SetPreviousFaceDetail(2);
                        DestroyImmediate(faceRawImage2.texture);
                        faceRawImage2.texture = new Texture2D(temporalFaceImage.width, temporalFaceImage.height) as Texture;
                        (faceRawImage2.texture as Texture2D).SetPixels(temporalFaceImage.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage2.texture as Texture2D).Apply();
                        faceRawImage2.transform.parent.gameObject.SetActive(true);
                        faceRawImage2.rectTransform.sizeDelta = new Vector2(temporalFaceImage.width, temporalFaceImage.height);
                        faceRawImage2.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage2.rectTransform.sizeDelta / 2f;
                        faceRawImage2.gameObject.SetActive(true);
                        DestroyImmediate(temporalFaceImage);
                        currentlySelectedFace = 2;
                        saveDict["face2"] = true;
                        UsingCustomFace2(true);
                        ShowFaceImage2(true);
                        return;
                    }
                }

                //if (isShowingFace)
                //{
                //    if (faceRawImage.transform.parent.parent == femaleModelImageObject.transform)
                //    {
                //        temporarilyHiddenFace = 1;
                //        faceRawImage.transform.parent.gameObject.SetActive(false);
                //    }
                //    else if (faceRawImage.transform.parent.parent == maleImage.transform)
                //    {
                //        if (temporarilyHiddenFace == 1)
                //        {
                //            faceRawImage.transform.parent.gameObject.SetActive(true);
                //            temporarilyHiddenFace = -1;
                //        }
                //    }
                //}
            }
            //ZoomInFemaleModel();
        }

        else if (!femaleToZoom && isShowingMale)
        {
            if (currentlySelectedFace == 1)
            {
                if (useImageFromTempImage)
                {
                    if (faceRawImage.transform.parent.parent == maleImage.transform)
                    {
                        print("faceRawImage.transform.parent.parent == maleImage.transform");
                        SetPreviousFaceDetail(1);
                        DestroyImmediate(faceRawImage.texture);
                        faceRawImage.texture = new Texture2D(temporalFaceImage.width, temporalFaceImage.height) as Texture;
                        (faceRawImage.texture as Texture2D).SetPixels(temporalFaceImage.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage.texture as Texture2D).Apply();
                        faceRawImage.transform.parent.gameObject.SetActive(true);

                        faceRawImage.rectTransform.sizeDelta = new Vector2(temporalFaceImage.width, temporalFaceImage.height);
                        faceRawImage.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage.rectTransform.sizeDelta / 2f;
                        faceRawImage.gameObject.SetActive(true);
                        DestroyImmediate(temporalFaceImage);
                        currentlySelectedFace = 1;
                        saveDict["face1"] = true;
                        UsingCustomFace(true);
                        ShowFaceImage(true);
                        return;
                    }
                    else if (faceRawImage2.transform.parent.parent == maleImage.transform)
                    {
                        print("faceRawImage2.transform.parent.parent == maleImage.transform");
                        SetPreviousFaceDetail(2);
                        DestroyImmediate(faceRawImage2.texture);
                        faceRawImage2.texture = new Texture2D(temporalFaceImage.width, temporalFaceImage.height) as Texture;
                        (faceRawImage2.texture as Texture2D).SetPixels(temporalFaceImage.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage2.texture as Texture2D).Apply();
                        faceRawImage2.transform.parent.gameObject.SetActive(true);
                        faceRawImage2.rectTransform.sizeDelta = new Vector2(temporalFaceImage.width, temporalFaceImage.height);
                        faceRawImage2.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage2.rectTransform.sizeDelta / 2f;
                        faceRawImage2.gameObject.SetActive(true);
                        DestroyImmediate(temporalFaceImage);
                        currentlySelectedFace = 2;
                        saveDict["face2"] = true;
                        UsingCustomFace2(true);
                        ShowFaceImage2(true);
                        return;
                    }
                    else if (faceRawImage.transform.parent.parent == femaleModelImageObject.transform)
                    {
                        print("faceRawImage.transform.parent.parent == femaleModelImageObject.transform");
                        SetPreviousFaceDetail(2);
                        DestroyImmediate(faceRawImage2.texture);
                        faceRawImage2.texture = new Texture2D(temporalFaceImage.width, temporalFaceImage.height) as Texture;
                        (faceRawImage2.texture as Texture2D).SetPixels(temporalFaceImage.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage2.texture as Texture2D).Apply();
                        faceRawImage2.transform.parent.gameObject.SetActive(true);
                        faceRawImage2.rectTransform.sizeDelta = new Vector2(temporalFaceImage.width, temporalFaceImage.height);
                        faceRawImage2.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage2.rectTransform.sizeDelta / 2f;
                        faceRawImage2.gameObject.SetActive(true);
                        DestroyImmediate(temporalFaceImage);
                        currentlySelectedFace = 2;
                        saveDict["face2"] = true;
                        UsingCustomFace2(true);
                        ShowFaceImage2(true);
                        return;
                    }
                
                    else if (faceRawImage2.transform.parent.parent == femaleModelImageObject.transform)
                    {
                        print("faceRawImage2.transform.parent.parent == femaleModelImageObject.transform");
                        SetPreviousFaceDetail(1);
                        DestroyImmediate(faceRawImage.texture);
                        faceRawImage.texture = new Texture2D(temporalFaceImage.width, temporalFaceImage.height) as Texture;
                        (faceRawImage.texture as Texture2D).SetPixels(temporalFaceImage.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage.texture as Texture2D).Apply();
                        faceRawImage.transform.parent.gameObject.SetActive(true);

                        faceRawImage.rectTransform.sizeDelta = new Vector2(temporalFaceImage.width, temporalFaceImage.height);
                        faceRawImage.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage.rectTransform.sizeDelta / 2f;
                        faceRawImage.gameObject.SetActive(true);
                        DestroyImmediate(temporalFaceImage);
                        currentlySelectedFace = 1;
                        saveDict["face1"] = true;
                        UsingCustomFace(true);
                        ShowFaceImage(true);
                        return;
                    }
                    else
                    {
                        print("currently selected face 1 male zoom else");
                        SetPreviousFaceDetail(1);
                        DestroyImmediate(faceRawImage.texture);
                        faceRawImage.texture = new Texture2D(temporalFaceImage.width, temporalFaceImage.height) as Texture;
                        (faceRawImage.texture as Texture2D).SetPixels(temporalFaceImage.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage.texture as Texture2D).Apply();
                        faceRawImage.transform.parent.gameObject.SetActive(true);

                        faceRawImage.rectTransform.sizeDelta = new Vector2(temporalFaceImage.width, temporalFaceImage.height);
                        faceRawImage.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage.rectTransform.sizeDelta / 2f;
                        faceRawImage.gameObject.SetActive(true);
                        DestroyImmediate(temporalFaceImage);
                        currentlySelectedFace = 1;
                        saveDict["face1"] = true;
                        return;
                    }
                }

                //if (isShowingFace2)
                //{
                //    if (faceRawImage2.transform.parent.parent == maleImage.transform)// && temporarilyHiddenFace!=2)
                //    {
                //        temporarilyHiddenFace = 2;
                //        faceRawImage2.transform.parent.gameObject.SetActive(false);
                //    }
                //    else if(faceRawImage2.transform.parent.parent == femaleModelImageObject.transform)
                //    {
                //        if(temporarilyHiddenFace==2)
                //        {
                //            faceRawImage2.transform.parent.gameObject.SetActive(true);
                //            temporarilyHiddenFace = -1;
                //        }
                //    }
                //}
            }
            else if (currentlySelectedFace == 2)
            {
                if (useImageFromTempImage)
                {
                    if (faceRawImage.transform.parent.parent == maleImage.transform)
                    {
                        print("faceRawImage.transform.parent.parent == maleImage.transform");
                        SetPreviousFaceDetail(1);
                        DestroyImmediate(faceRawImage.texture);
                        faceRawImage.texture = new Texture2D(temporeaFaceImage2.width, temporeaFaceImage2.height) as Texture;
                        (faceRawImage.texture as Texture2D).SetPixels(temporeaFaceImage2.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage.texture as Texture2D).Apply();
                        faceRawImage.transform.parent.gameObject.SetActive(true);

                        faceRawImage.rectTransform.sizeDelta = new Vector2(temporeaFaceImage2.width, temporeaFaceImage2.height);
                        faceRawImage.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage.rectTransform.sizeDelta / 2f;
                        faceRawImage.gameObject.SetActive(true);
                        DestroyImmediate(temporeaFaceImage2);
                        currentlySelectedFace = 1;
                        saveDict["face1"] = true;
                        UsingCustomFace(true);
                        ShowFaceImage(true);

                        return;
                    }
                    else if (faceRawImage2.transform.parent.parent == maleImage.transform)
                    {
                        print("faceRawImage2.transform.parent.parent == maleImage.transform");
                        SetPreviousFaceDetail(2);
                        DestroyImmediate(faceRawImage2.texture);
                        faceRawImage2.texture = new Texture2D(temporeaFaceImage2.width, temporeaFaceImage2.height) as Texture;
                        (faceRawImage2.texture as Texture2D).SetPixels(temporeaFaceImage2.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage2.texture as Texture2D).Apply();
                        faceRawImage2.transform.parent.gameObject.SetActive(true);
                        faceRawImage2.rectTransform.sizeDelta = new Vector2(temporeaFaceImage2.width, temporeaFaceImage2.height);
                        faceRawImage2.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage2.rectTransform.sizeDelta / 2f;
                        faceRawImage2.gameObject.SetActive(true);
                        DestroyImmediate(temporeaFaceImage2);
                        currentlySelectedFace = 2;
                        saveDict["face2"] = true;
                        UsingCustomFace2(true);
                        ShowFaceImage2(true);
                        return;
                    }
                    else if(faceRawImage.transform.parent.parent == femaleModelImageObject.transform)
                    {
                        print("faceRawImage.transform.parent.parent == femaleModelImageObject.transform");
                        SetPreviousFaceDetail(2);
                        DestroyImmediate(faceRawImage2.texture);
                        faceRawImage2.texture = new Texture2D(temporeaFaceImage2.width, temporeaFaceImage2.height) as Texture;
                        (faceRawImage2.texture as Texture2D).SetPixels(temporeaFaceImage2.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage2.texture as Texture2D).Apply();
                        faceRawImage2.transform.parent.gameObject.SetActive(true);
                        faceRawImage2.rectTransform.sizeDelta = new Vector2(temporeaFaceImage2.width, temporeaFaceImage2.height);
                        faceRawImage2.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage2.rectTransform.sizeDelta / 2f;
                        faceRawImage2.gameObject.SetActive(true);
                        DestroyImmediate(temporeaFaceImage2);
                        currentlySelectedFace = 2;
                        saveDict["face2"] = true;
                        UsingCustomFace2(true);
                        ShowFaceImage2(true);
                        return;
                    }
                    else if(faceRawImage2.transform.parent.parent == femaleModelImageObject.transform)
                    {
                        print("faceRawImage2.transform.parent.parent == femaleModelImageObject.transform");
                        SetPreviousFaceDetail(1);
                        DestroyImmediate(faceRawImage.texture);
                        faceRawImage.texture = new Texture2D(temporeaFaceImage2.width, temporeaFaceImage2.height) as Texture;
                        (faceRawImage.texture as Texture2D).SetPixels(temporeaFaceImage2.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage.texture as Texture2D).Apply();
                        faceRawImage.transform.parent.gameObject.SetActive(true);

                        faceRawImage.rectTransform.sizeDelta = new Vector2(temporeaFaceImage2.width, temporeaFaceImage2.height);
                        faceRawImage.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage.rectTransform.sizeDelta / 2f;
                        faceRawImage.gameObject.SetActive(true);
                        DestroyImmediate(temporeaFaceImage2);
                        currentlySelectedFace = 1;
                        saveDict["face1"] = true;
                        UsingCustomFace(true);
                        ShowFaceImage(true);
                        return;
                    }
                    else
                    {
                        print("currently selected face 2 male zoom else");
                        SetPreviousFaceDetail(2);
                        DestroyImmediate(faceRawImage2.texture);
                        faceRawImage2.texture = new Texture2D(temporeaFaceImage2.width, temporeaFaceImage2.height) as Texture;
                        (faceRawImage2.texture as Texture2D).SetPixels(temporeaFaceImage2.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage2.texture as Texture2D).Apply();
                        faceRawImage2.transform.parent.gameObject.SetActive(true);
                        faceRawImage2.rectTransform.sizeDelta = new Vector2(temporeaFaceImage2.width, temporeaFaceImage2.height);
                        faceRawImage2.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage2.rectTransform.sizeDelta / 2f;
                        faceRawImage2.gameObject.SetActive(true);
                        DestroyImmediate(temporeaFaceImage2);
                        currentlySelectedFace = 2;
                        saveDict["face2"] = true;
                        UsingCustomFace2(true);
                        ShowFaceImage2(true);
                        return;
                    }
                }

                //if (isShowingFace)
                //{
                //    if (faceRawImage.transform.parent.parent == maleImage.transform)
                //    {
                //        temporarilyHiddenFace = 1;
                //        faceRawImage.transform.parent.gameObject.SetActive(false);
                        
                //    }
                //    else if (faceRawImage.transform.parent.parent == femaleModelImageObject.transform)
                //    {
                //        if (temporarilyHiddenFace == 1)
                //        {
                //            faceRawImage.transform.parent.gameObject.SetActive(true);
                //            temporarilyHiddenFace = -1;
                //        }
                //    }
                //}
            }
            //ZoomInMaleModel();
        }
    }













    public void UseOnlySingleFaceOnAModel(Texture2D faceTexture,bool femaleToZoom = true, bool useImageFromTempImage = false)
    {
        if (femaleToZoom)
        {
            if (currentlySelectedFace == 1)
            {
                if (!useImageFromTempImage)
                {
                    if (faceRawImage.transform.parent.parent == femaleModelImageObject.transform)
                    {
                        print("faceRawImage.transform.parent.parent==femaleModelImageObject.transform");
                        SetPreviousFaceDetail(1);
                        DestroyImmediate(faceRawImage.texture);
                        faceRawImage.texture = new Texture2D(faceTexture.width, faceTexture.height) as Texture;
                        (faceRawImage.texture as Texture2D).SetPixels(faceTexture.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage.texture as Texture2D).Apply();
                        faceRawImage.transform.parent.gameObject.SetActive(true);

                        faceRawImage.rectTransform.sizeDelta = new Vector2(faceTexture.width, faceTexture.height);
                        faceRawImage.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage.rectTransform.sizeDelta / 2f;
                        faceRawImage.gameObject.SetActive(true);
                        DestroyImmediate(faceTexture);
                        currentlySelectedFace = 1;
                        saveDict["face1"] = false;
                        UsingCustomFace(true);
                        ShowFaceImage(true);
                        return;
                    }
                    else if (faceRawImage2.transform.parent.parent == femaleModelImageObject.transform)
                    {
                        print("faceRawImage2.transform.parent.parent== femaleModelImageObject.transform");
                        SetPreviousFaceDetail(2);
                        DestroyImmediate(faceRawImage2.texture);
                        faceRawImage2.texture = new Texture2D(faceTexture.width, faceTexture.height) as Texture;
                        (faceRawImage2.texture as Texture2D).SetPixels(faceTexture.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage2.texture as Texture2D).Apply();
                        faceRawImage2.transform.parent.gameObject.SetActive(true);
                        faceRawImage2.rectTransform.sizeDelta = new Vector2(faceTexture.width, faceTexture.height);
                        faceRawImage2.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage2.rectTransform.sizeDelta / 2f;
                        faceRawImage2.gameObject.SetActive(true);
                        DestroyImmediate(faceTexture);
                        currentlySelectedFace = 2;
                        saveDict["face2"] = false;
                        UsingCustomFace2(true);
                        ShowFaceImage2(true);
                        return;
                    }
                    else if (faceRawImage.transform.parent.parent == maleImage.transform)
                    {
                        print("faceRawImage.transform.parent.parent == maleImage.transform");
                        SetPreviousFaceDetail(2);
                        DestroyImmediate(faceRawImage2.texture);
                        faceRawImage2.texture = new Texture2D(faceTexture.width, faceTexture.height) as Texture;
                        (faceRawImage2.texture as Texture2D).SetPixels(faceTexture.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage2.texture as Texture2D).Apply();
                        faceRawImage2.transform.parent.gameObject.SetActive(true);
                        faceRawImage2.rectTransform.sizeDelta = new Vector2(faceTexture.width, faceTexture.height);
                        faceRawImage2.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage2.rectTransform.sizeDelta / 2f;
                        faceRawImage2.gameObject.SetActive(true);
                        DestroyImmediate(faceTexture);
                        currentlySelectedFace = 2;
                        saveDict["face2"] = false;
                        UsingCustomFace2(true);
                        ShowFaceImage2(true);
                        return;
                    }

                    else if (faceRawImage2.transform.parent.parent == maleImage.transform)
                    {
                        print("faceRawImage2.transform.parent.parent == maleImage.transform");
                        SetPreviousFaceDetail(1);
                        DestroyImmediate(faceRawImage.texture);
                        faceRawImage.texture = new Texture2D(faceTexture.width, faceTexture.height) as Texture;
                        (faceRawImage.texture as Texture2D).SetPixels(faceTexture.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage.texture as Texture2D).Apply();
                        faceRawImage.transform.parent.gameObject.SetActive(true);

                        faceRawImage.rectTransform.sizeDelta = new Vector2(faceTexture.width, faceTexture.height);
                        faceRawImage.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage.rectTransform.sizeDelta / 2f;
                        faceRawImage.gameObject.SetActive(true);
                        DestroyImmediate(faceTexture);
                        currentlySelectedFace = 1;
                        saveDict["face1"] = false;
                        UsingCustomFace(true);
                        ShowFaceImage(true);
                        return;

                    }
                    else
                    {
                        print("using face 1 on female");
                        SetPreviousFaceDetail(1);
                        DestroyImmediate(faceRawImage.texture);
                        faceRawImage.texture = new Texture2D(faceTexture.width, faceTexture.height) as Texture;
                        (faceRawImage.texture as Texture2D).SetPixels(faceTexture.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage.texture as Texture2D).Apply();
                        faceRawImage.transform.parent.gameObject.SetActive(true);

                        faceRawImage.rectTransform.sizeDelta = new Vector2(faceTexture.width, faceTexture.height);
                        faceRawImage.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage.rectTransform.sizeDelta / 2f;
                        faceRawImage.gameObject.SetActive(true);
                        DestroyImmediate(faceTexture);
                        currentlySelectedFace = 1;
                        saveDict["face1"] = false;
                        UsingCustomFace(true);
                        ShowFaceImage(true);
                        return;
                    }
                }
                //if (isShowingFace2)
                //{
                //    if (faceRawImage2.transform.parent.parent == femaleModelImageObject.transform)
                //    {
                //        //InstantiateInfoPopup("Only one face can be placed on a single model");
                //        //return;

                //        temporarilyHiddenFace = 2;
                //        faceRawImage2.transform.parent.gameObject.SetActive(false);
                //    }
                //    else if (faceRawImage2.transform.parent.parent == maleImage.transform)
                //    {
                //        if(temporarilyHiddenFace==2)
                //        {

                //            faceRawImage2.transform.parent.gameObject.SetActive(true);
                //            temporarilyHiddenFace = -1;
                //        }
                //    }
                //}

                return;

            }
            else if (currentlySelectedFace == 2)
            {

                if (!useImageFromTempImage)
                {
                    if (faceRawImage.transform.parent.parent == femaleModelImageObject.transform)
                    {
                        print("faceRawImage.transform.parent.parent == femaleModelImageObject.transform");
                        SetPreviousFaceDetail(1);
                        DestroyImmediate(faceRawImage.texture);
                        faceRawImage.texture = new Texture2D(faceTexture.width, faceTexture.height) as Texture;
                        (faceRawImage.texture as Texture2D).SetPixels(faceTexture.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage.texture as Texture2D).Apply();
                        faceRawImage.transform.parent.gameObject.SetActive(true);

                        faceRawImage.rectTransform.sizeDelta = new Vector2(faceTexture.width, faceTexture.height);
                        faceRawImage.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage.rectTransform.sizeDelta / 2f;
                        faceRawImage.gameObject.SetActive(true);
                        DestroyImmediate(faceTexture);
                        currentlySelectedFace = 1;
                        saveDict["face1"] = true;
                        UsingCustomFace(true);
                        ShowFaceImage(true);
                        return;
                    }
                    else if (faceRawImage2.transform.parent.parent == femaleModelImageObject.transform)
                    {
                        print("faceRawImage2.transform.parent.parent == femaleModelImageObject.transform");
                        SetPreviousFaceDetail(2);
                        DestroyImmediate(faceRawImage2.texture);
                        faceRawImage2.texture = new Texture2D(faceTexture.width, faceTexture.height) as Texture;
                        (faceRawImage2.texture as Texture2D).SetPixels(faceTexture.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage2.texture as Texture2D).Apply();
                        faceRawImage2.transform.parent.gameObject.SetActive(true);
                        faceRawImage2.rectTransform.sizeDelta = new Vector2(faceTexture.width, faceTexture.height);
                        faceRawImage2.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage2.rectTransform.sizeDelta / 2f;
                        faceRawImage2.gameObject.SetActive(true);
                        DestroyImmediate(faceTexture);
                        currentlySelectedFace = 2;
                        saveDict["face2"] = true;
                        UsingCustomFace2(true);
                        ShowFaceImage2(true);
                        return;
                    }
                    else if (faceRawImage.transform.parent.parent == maleImage.transform)
                    {
                        print("faceRawImage.transform.parent.parent == maleImage.transform");
                        SetPreviousFaceDetail(2);
                        DestroyImmediate(faceRawImage2.texture);
                        faceRawImage2.texture = new Texture2D(faceTexture.width, faceTexture.height) as Texture;
                        (faceRawImage2.texture as Texture2D).SetPixels(faceTexture.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage2.texture as Texture2D).Apply();
                        faceRawImage2.transform.parent.gameObject.SetActive(true);
                        faceRawImage2.rectTransform.sizeDelta = new Vector2(faceTexture.width, faceTexture.height);
                        faceRawImage2.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage2.rectTransform.sizeDelta / 2f;
                        faceRawImage2.gameObject.SetActive(true);
                        DestroyImmediate(faceTexture);
                        currentlySelectedFace = 2;
                        saveDict["face2"] = true;
                        UsingCustomFace2(true);
                        ShowFaceImage2(true);
                        return;
                    }

                    else if (faceRawImage2.transform.parent.parent == maleImage.transform)
                    {
                        print("faceRawImage2.transform.parent.parent == maleImage.transform");
                        SetPreviousFaceDetail(1);
                        DestroyImmediate(faceRawImage.texture);
                        faceRawImage.texture = new Texture2D(faceTexture.width, faceTexture.height) as Texture;
                        (faceRawImage.texture as Texture2D).SetPixels(faceTexture.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage.texture as Texture2D).Apply();
                        faceRawImage.transform.parent.gameObject.SetActive(true);

                        faceRawImage.rectTransform.sizeDelta = new Vector2(faceTexture.width, faceTexture.height);
                        faceRawImage.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage.rectTransform.sizeDelta / 2f;
                        faceRawImage.gameObject.SetActive(true);
                        DestroyImmediate(faceTexture);
                        currentlySelectedFace = 1;
                        saveDict["face1"] = true;
                        UsingCustomFace(true);
                        ShowFaceImage(true);
                        return;

                    }
                    else
                    {
                        print("using face 2 on female");
                        SetPreviousFaceDetail(2);
                        DestroyImmediate(faceRawImage2.texture);
                        faceRawImage2.texture = new Texture2D(faceTexture.width, faceTexture.height) as Texture;
                        (faceRawImage2.texture as Texture2D).SetPixels(faceTexture.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage2.texture as Texture2D).Apply();
                        faceRawImage2.transform.parent.gameObject.SetActive(true);
                        faceRawImage2.rectTransform.sizeDelta = new Vector2(faceTexture.width, faceTexture.height);
                        faceRawImage2.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage2.rectTransform.sizeDelta / 2f;
                        faceRawImage2.gameObject.SetActive(true);
                        DestroyImmediate(faceTexture);
                        currentlySelectedFace = 2;
                        saveDict["face2"] = true;
                        UsingCustomFace2(true);
                        ShowFaceImage2(true);
                        return;
                    }
                }

                //if (isShowingFace)
                //{
                //    if (faceRawImage.transform.parent.parent == femaleModelImageObject.transform)
                //    {
                //        temporarilyHiddenFace = 1;
                //        faceRawImage.transform.parent.gameObject.SetActive(false);
                //    }
                //    else if (faceRawImage.transform.parent.parent == maleImage.transform)
                //    {
                //        if (temporarilyHiddenFace == 1)
                //        {
                //            faceRawImage.transform.parent.gameObject.SetActive(true);
                //            temporarilyHiddenFace = -1;
                //        }
                //    }
                //}
            }
            //ZoomInFemaleModel();
        }

        else if (!femaleToZoom && isShowingMale)
        {
            if (currentlySelectedFace == 1)
            {
                if (!useImageFromTempImage)
                {
                    if (faceRawImage.transform.parent.parent == maleImage.transform)
                    {
                        print("faceRawImage.transform.parent.parent == maleImage.transform");
                        SetPreviousFaceDetail(1);
                        DestroyImmediate(faceRawImage.texture);
                        faceRawImage.texture = new Texture2D(faceTexture.width, faceTexture.height) as Texture;
                        (faceRawImage.texture as Texture2D).SetPixels(faceTexture.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage.texture as Texture2D).Apply();
                        faceRawImage.transform.parent.gameObject.SetActive(true);

                        faceRawImage.rectTransform.sizeDelta = new Vector2(faceTexture.width, faceTexture.height);
                        faceRawImage.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage.rectTransform.sizeDelta / 2f;
                        faceRawImage.gameObject.SetActive(true);
                        DestroyImmediate(faceTexture);
                        currentlySelectedFace = 1;
                        saveDict["face1"] = true;
                        UsingCustomFace(true);
                        ShowFaceImage(true);
                        return;
                    }
                    else if (faceRawImage2.transform.parent.parent == maleImage.transform)
                    {
                        print("faceRawImage2.transform.parent.parent == maleImage.transform");
                        SetPreviousFaceDetail(2);
                        DestroyImmediate(faceRawImage2.texture);
                        faceRawImage2.texture = new Texture2D(faceTexture.width, faceTexture.height) as Texture;
                        (faceRawImage2.texture as Texture2D).SetPixels(faceTexture.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage2.texture as Texture2D).Apply();
                        faceRawImage2.transform.parent.gameObject.SetActive(true);
                        faceRawImage2.rectTransform.sizeDelta = new Vector2(faceTexture.width, faceTexture.height);
                        faceRawImage2.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage2.rectTransform.sizeDelta / 2f;
                        faceRawImage2.gameObject.SetActive(true);
                        DestroyImmediate(faceTexture);
                        currentlySelectedFace = 2;
                        saveDict["face2"] = true;
                        UsingCustomFace2(true);
                        ShowFaceImage2(true);
                        return;
                    }
                    else if (faceRawImage.transform.parent.parent == femaleModelImageObject.transform)
                    {
                        print("faceRawImage.transform.parent.parent == femaleModelImageObject.transform");
                        SetPreviousFaceDetail(2);
                        DestroyImmediate(faceRawImage2.texture);
                        faceRawImage2.texture = new Texture2D(faceTexture.width, faceTexture.height) as Texture;
                        (faceRawImage2.texture as Texture2D).SetPixels(faceTexture.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage2.texture as Texture2D).Apply();
                        faceRawImage2.transform.parent.gameObject.SetActive(true);
                        faceRawImage2.rectTransform.sizeDelta = new Vector2(faceTexture.width, faceTexture.height);
                        faceRawImage2.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage2.rectTransform.sizeDelta / 2f;
                        faceRawImage2.gameObject.SetActive(true);
                        DestroyImmediate(faceTexture);
                        currentlySelectedFace = 2;
                        saveDict["face2"] = true;
                        UsingCustomFace2(true);
                        ShowFaceImage2(true);
                        return;
                    }

                    else if (faceRawImage2.transform.parent.parent == femaleModelImageObject.transform)
                    {
                        print("faceRawImage2.transform.parent.parent == femaleModelImageObject.transform");
                        SetPreviousFaceDetail(1);
                        DestroyImmediate(faceRawImage.texture);
                        faceRawImage.texture = new Texture2D(faceTexture.width, faceTexture.height) as Texture;
                        (faceRawImage.texture as Texture2D).SetPixels(faceTexture.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage.texture as Texture2D).Apply();
                        faceRawImage.transform.parent.gameObject.SetActive(true);

                        faceRawImage.rectTransform.sizeDelta = new Vector2(faceTexture.width, faceTexture.height);
                        faceRawImage.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage.rectTransform.sizeDelta / 2f;
                        faceRawImage.gameObject.SetActive(true);
                        DestroyImmediate(faceTexture);
                        currentlySelectedFace = 1;
                        saveDict["face1"] = true;
                        UsingCustomFace(true);
                        ShowFaceImage(true);
                        return;
                    }
                    else
                    {
                        print("currently selected face 1 male zoom else");
                        SetPreviousFaceDetail(1);
                        DestroyImmediate(faceRawImage.texture);
                        faceRawImage.texture = new Texture2D(faceTexture.width, faceTexture.height) as Texture;
                        (faceRawImage.texture as Texture2D).SetPixels(faceTexture.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage.texture as Texture2D).Apply();
                        faceRawImage.transform.parent.gameObject.SetActive(true);

                        faceRawImage.rectTransform.sizeDelta = new Vector2(faceTexture.width, faceTexture.height);
                        faceRawImage.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage.rectTransform.sizeDelta / 2f;
                        faceRawImage.gameObject.SetActive(true);
                        DestroyImmediate(faceTexture);
                        currentlySelectedFace = 1;
                        saveDict["face1"] = true;
                        return;
                    }
                }

                //if (isShowingFace2)
                //{
                //    if (faceRawImage2.transform.parent.parent == maleImage.transform)// && temporarilyHiddenFace!=2)
                //    {
                //        temporarilyHiddenFace = 2;
                //        faceRawImage2.transform.parent.gameObject.SetActive(false);
                //    }
                //    else if(faceRawImage2.transform.parent.parent == femaleModelImageObject.transform)
                //    {
                //        if(temporarilyHiddenFace==2)
                //        {
                //            faceRawImage2.transform.parent.gameObject.SetActive(true);
                //            temporarilyHiddenFace = -1;
                //        }
                //    }
                //}
            }
            else if (currentlySelectedFace == 2)
            {
                if (!useImageFromTempImage)
                {
                    if (faceRawImage.transform.parent.parent == maleImage.transform)
                    {
                        print("faceRawImage.transform.parent.parent == maleImage.transform");
                        SetPreviousFaceDetail(1);
                        DestroyImmediate(faceRawImage.texture);
                        faceRawImage.texture = new Texture2D(faceTexture.width, faceTexture.height) as Texture;
                        (faceRawImage.texture as Texture2D).SetPixels(faceTexture.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage.texture as Texture2D).Apply();
                        faceRawImage.transform.parent.gameObject.SetActive(true);

                        faceRawImage.rectTransform.sizeDelta = new Vector2(faceTexture.width, faceTexture.height);
                        faceRawImage.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage.rectTransform.sizeDelta / 2f;
                        faceRawImage.gameObject.SetActive(true);
                        DestroyImmediate(faceTexture);
                        currentlySelectedFace = 1;
                        saveDict["face1"] = true;
                        UsingCustomFace(true);
                        ShowFaceImage(true);

                        return;
                    }
                    else if (faceRawImage2.transform.parent.parent == maleImage.transform)
                    {
                        print("faceRawImage2.transform.parent.parent == maleImage.transform");
                        SetPreviousFaceDetail(2);
                        DestroyImmediate(faceRawImage2.texture);
                        faceRawImage2.texture = new Texture2D(faceTexture.width, faceTexture.height) as Texture;
                        (faceRawImage2.texture as Texture2D).SetPixels(faceTexture.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage2.texture as Texture2D).Apply();
                        faceRawImage2.transform.parent.gameObject.SetActive(true);
                        faceRawImage2.rectTransform.sizeDelta = new Vector2(faceTexture.width, faceTexture.height);
                        faceRawImage2.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage2.rectTransform.sizeDelta / 2f;
                        faceRawImage2.gameObject.SetActive(true);
                        DestroyImmediate(faceTexture);
                        currentlySelectedFace = 2;
                        saveDict["face2"] = true;
                        UsingCustomFace2(true);
                        ShowFaceImage2(true);
                        return;
                    }
                    else if (faceRawImage.transform.parent.parent == femaleModelImageObject.transform)
                    {
                        print("faceRawImage.transform.parent.parent == femaleModelImageObject.transform");
                        SetPreviousFaceDetail(2);
                        DestroyImmediate(faceRawImage2.texture);
                        faceRawImage2.texture = new Texture2D(faceTexture.width, faceTexture.height) as Texture;
                        (faceRawImage2.texture as Texture2D).SetPixels(faceTexture.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage2.texture as Texture2D).Apply();
                        faceRawImage2.transform.parent.gameObject.SetActive(true);
                        faceRawImage2.rectTransform.sizeDelta = new Vector2(faceTexture.width, faceTexture.height);
                        faceRawImage2.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage2.rectTransform.sizeDelta / 2f;
                        faceRawImage2.gameObject.SetActive(true);
                        DestroyImmediate(faceTexture);
                        currentlySelectedFace = 2;
                        saveDict["face2"] = true;
                        UsingCustomFace2(true);
                        ShowFaceImage2(true);
                        return;
                    }
                    else if (faceRawImage2.transform.parent.parent == femaleModelImageObject.transform)
                    {
                        print("faceRawImage2.transform.parent.parent == femaleModelImageObject.transform");
                        SetPreviousFaceDetail(1);
                        DestroyImmediate(faceRawImage.texture);
                        faceRawImage.texture = new Texture2D(faceTexture.width, faceTexture.height) as Texture;
                        (faceRawImage.texture as Texture2D).SetPixels(faceTexture.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage.texture as Texture2D).Apply();
                        faceRawImage.transform.parent.gameObject.SetActive(true);

                        faceRawImage.rectTransform.sizeDelta = new Vector2(faceTexture.width, faceTexture.height);
                        faceRawImage.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage.rectTransform.sizeDelta / 2f;
                        faceRawImage.gameObject.SetActive(true);
                        DestroyImmediate(faceTexture);
                        currentlySelectedFace = 1;
                        saveDict["face1"] = true;
                        UsingCustomFace(true);
                        ShowFaceImage(true);
                        return;
                    }
                    else
                    {
                        print("currently selected face 2 male zoom else");
                        SetPreviousFaceDetail(2);
                        DestroyImmediate(faceRawImage2.texture);
                        faceRawImage2.texture = new Texture2D(faceTexture.width, faceTexture.height) as Texture;
                        (faceRawImage2.texture as Texture2D).SetPixels(faceTexture.GetPixels());  // = (Texture)combinedImage;
                        (faceRawImage2.texture as Texture2D).Apply();
                        faceRawImage2.transform.parent.gameObject.SetActive(true);
                        faceRawImage2.rectTransform.sizeDelta = new Vector2(faceTexture.width, faceTexture.height);
                        faceRawImage2.gameObject.GetComponent<BoxCollider2D>().size = faceRawImage2.rectTransform.sizeDelta / 2f;
                        faceRawImage2.gameObject.SetActive(true);
                        DestroyImmediate(faceTexture);
                        currentlySelectedFace = 2;
                        saveDict["face2"] = true;
                        UsingCustomFace2(true);
                        ShowFaceImage2(true);
                        return;
                    }
                }

                //if (isShowingFace)
                //{
                //    if (faceRawImage.transform.parent.parent == maleImage.transform)
                //    {
                //        temporarilyHiddenFace = 1;
                //        faceRawImage.transform.parent.gameObject.SetActive(false);

                //    }
                //    else if (faceRawImage.transform.parent.parent == femaleModelImageObject.transform)
                //    {
                //        if (temporarilyHiddenFace == 1)
                //        {
                //            faceRawImage.transform.parent.gameObject.SetActive(true);
                //            temporarilyHiddenFace = -1;
                //        }
                //    }
                //}
            }
            //ZoomInMaleModel();
        }
    }
    #endregion MALEFEMALETOGGLESCENE




    #region SHARING


    public void OnClickShareButton()
    {
        GoToHome();
    }

    public void Share()
    {
        GoToHome();

    }


    public IEnumerator TakeScreenshotForShare(string fullPath,Action callBack)
    {

        DeactiveCurrentActive(true, true);
        print(string.Format("Full Path : {0} ", fullPath));


        yield return new WaitForSecondsRealtime(.5f);
        ScreenCapture.CaptureScreenshot(fullPath);
        //yield return new WaitForSecondsRealtime(1f);
        //yield return new WaitForEndOfFrame();
        Texture2D ttx2d = new Texture2D(320, 480);
        if (Application.isMobilePlatform)
        {
            fullPath = Path.Combine(Application.persistentDataPath, fullPath);
        }

        print("full path now is : " + fullPath);
        int maxWait = 30;
        int currentWaitTime = 0;
        while (!File.Exists(fullPath))
        {
            yield return new WaitForSecondsRealtime(.3f);
            currentWaitTime += 1;
            if (currentWaitTime > maxWait)
            {
                break;
            }
        }

        if (!File.Exists(fullPath))
        {
            InstantiateInfoPopup("Could not Save Screenshot");

        }
        else
        {
            yield return new WaitForSecondsRealtime(1f);
            ttx2d.LoadImage(File.ReadAllBytes(fullPath));
            ttx2d.Apply();
            ttx2d = cameraController.ResizeTexture2D(ttx2d, 600, 800);
            ttx2d.Apply();
            //File.WriteAllBytes(fullPath, ttx2d.EncodeToJPG(50));
            //ttx2d = RTImage(MAINCAMERA, 320, 480);
            //ttx2d.Apply();
            //File.WriteAllBytes(fullPath, ttx2d.EncodeToJPG(50));

            //RenderTexture rt = MAINCAMERA.activeTexture;
            //rt.Create();

            //ScreenCapture.CaptureScreenshot()
        }

        yield return new WaitForSecondsRealtime(.5f);
        //Destroy(ttx2d, 2f);
        //ActiveCurrentActive(true, true);
        GoToHome();
    }


    #endregion


}
