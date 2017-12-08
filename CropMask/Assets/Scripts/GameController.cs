using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;

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

	[SerializeField]
	private GameObject femaleModelImageObject;

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

	[SerializeField]
	private GameObject[] panels;

	[SerializeField]
	private GameObject[] homeMenuButtonObjects;

	[SerializeField]
	private GameObject CameraDownMenuPanel;

	[SerializeField]
	private int currentShapeIndex;

	[SerializeField]
	private GameObject mainModel;

	[SerializeField]
	private Image dress;

	[SerializeField]
	private Image wig;

	[SerializeField]
	private Image shoe;

	[SerializeField]
	private Image ornament;

	[SerializeField]
	private SaveData saveData;

	[SerializeField]
	private List<SaveData> saveDatas;

	[SerializeField]
	private GameObject faceEditControllerObj;

	private FaceEditController faceEditController;

	[SerializeField]
	private GameObject[] maskButtons;

	private bool showingDress = false;

	private bool showingWig = false;

	private bool showingShoe = false;

	private string fullConfigFilePath;

	[SerializeField]
	private bool isFirstTime = true;

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

	[HideInInspector]
	public string mainMaleBodyName = "male_1";

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

	public Texture2D currentDressTexture;

	public Texture2D currentWigTexture;

	public Color currentDressColor = Color.white;

	public Color currentWigColor = Color.white;

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

	public Image backGroundImage;

	public GameObject backGroundPanel;

	public bool isShowingBackGroundPanel = false;

	public string currentBackgroundName = "bg0";

	public DressProperties currentDressProperty;

	public FemaleWigProperties currentFemaleWigProperty;
    public OrnamentProperties currentOrnamentProperty;
    public ShoeProperties currentShoeProperty;

	private bool isPaidUser = false;

	public GameObject popupPanelForPurchase;

	public GameObject eventSystem;

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

	private void Awake()
	{
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
		this.HideAcceptCropButton();
		this.currentShapeIndex = 0;
	}

	private void LateUpdate()
	{
		this.CollectGurbage();
	}

	public void CollectGurbage()
	{
		this.currentTimer++;
		if (this.currentTimer > this.gurbageCollectTime)
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
		}
		else
		{
			this.InstantiateInfoPopup(message);
		}
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
		SmartIAPListener.INSTANCE.Purchase("android.test.purchased", new Action<bool, string>(this.PurchaseCallBack));
	}

	public void InstantiateInfoPopup(string message)
	{
		GameObject g = Instantiate<GameObject>(this.infoPopupPrefab, this.canvasObject.transform);
		Text component = g.transform.GetChild(0).GetChild(0).GetComponent<Text>();
		component.text=message;
		Button component2 = g.transform.GetChild(0).GetChild(1).GetComponent<Button>();
		component2.onClick.AddListener(delegate
		{
			Destroy(g);
		});
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
	}

	public void ShowFaceImage(bool show = false)
	{
		this.faceRawImage.gameObject.SetActive(show);
		this.isShowingFace = show;
	}

	public void ShowFaceImage2(bool show = false)
	{
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
		this.processingImage.sprite=new Sprite();
		GameObject[] array = this.maskButtons;
		for (int i = 0; i < array.Length; i++)
		{
			GameObject gameObject = array[i];
			gameObject.GetComponent<Toggle>().isOn=false;
			gameObject.transform.GetChild(0).gameObject.SetActive(false);
		}
		this.touchController.DisableCurrentMask();
		this.sceneEditorControllerObj.SetActive(true);
	}

	public void SaveProcessedImage()
	{
		this.imagePanel.SetActive(false);
		if (this.currentlySelectedFace == 1)
		{
			this.isLoadedFace = false;
			this.loadedFaceIndex = -1;
			this.faceHash = 0;
		}
		else if (this.currentlySelectedFace == 2)
		{
			this.isLoadedFace2 = false;
			this.loadedFaceIndex2 = -1;
			this.faceHash2 = 0;
		}
		base.StartCoroutine(this.FinalizeImageEdit());
		this.touchController.ToggleMask(1);
		GameObject[] array = this.maskButtons;
		for (int i = 0; i < array.Length; i++)
		{
			GameObject gameObject = array[i];
			gameObject.GetComponent<Toggle>().isOn=(false);
			gameObject.transform.GetChild(0).gameObject.SetActive(false);
		}
		this.touchController.DisableCurrentMask();
	}

	
	public IEnumerator FinalizeImageEdit()
	{
        if (currentlyUsingFace == 1)
        {
            if (currentlySelectedFace == 1)
            {


                UsingCustomFace(true);
                ShowFaceImage(true);
            }
            else if (currentlySelectedFace == 2)
            {

                UsingCustomFace2(true);
                ShowFaceImage2(true);
            }
        }
        else if (currentlyUsingFace == 2)
        {


            UsingCustomFace(true);
            ShowFaceImage(true);

            UsingCustomFace2(true);
            ShowFaceImage2(true);


        }

        faceEditController.ShowFaceEditPAnel();
        yield return null;

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
		this.CheckIfFirstTime();
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			this.Quit();
		}
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
				this.popupController.ShowPopup(2, "Save The Face");
			}
			return;
		case 2:
			ShortcutExtensions46.DOAnchorPosX(this.panels[7].GetComponent<RectTransform>(), -400f, 0.3f, false);
			this.isShowingSavedFacesMenu = false;
			return;
		}
		
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
		base.StartCoroutine(this.BodyShapeSelectionProcess(true));
		this.popupController.HidePopup();
	}

	public void OnPressSelectBodyShapeButton(GameObject g)
	{
		if (this.isFirstTime)
		{
			base.StartCoroutine(this.BodyShapeSelectionProcess(true));
		}
		else
		{
			this.popupController.ShowPopup(3, null);
		}
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

            ornament.DOFade(0f, .3f);
            dress.DOFade(0f, .3f);
            wig.DOFade(0f, .3f);
            shoe.DOFade(0f, .3f);
            femaleModelImageObject.GetComponent<Image>().DOFade(0f, .3f);



            //yield return new WaitForSeconds(.3f);
            panels[5].SetActive(true);
            selectShapeController.SelectThisParticularModel(mainCarouselRotation, mainModelIndex);
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
                femaleModelObject.SetActive(true);
                yield return new WaitForSeconds(.2f);
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

            if (isFirstTime)
            {
                popupController.ShowPopup(0, "NOW, SEE THE BEST\nDRESS STYLE FOR YOUR\nBODY TYPE");
            }
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

	public void OnPressBackGroundbutton(GameObject g)
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
			if (this.currentActiveButton != null)
			{
				Toggle component = this.currentActiveButton.GetComponent<Toggle>();
				if (component != null)
				{
					component.isOn=(true);
				}
			}
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
		this.eventSystem.SetActive(false);
		this.loadingPanelOnly.SetActive(true);
	}

	public void HideLoadingPanelOnly()
	{
		this.loadingPanelOnly.SetActive(false);
		this.eventSystem.SetActive(true);
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
		if (this.IsPaidUser)
		{

            SaveData sd = new SaveData();

            sd.Initialize(mainBodyShape, mainBodyTone, mainEyeColor);
            if (wig.gameObject.activeSelf && wig.transform.parent.gameObject.activeSelf && wig.color.a > 0.5f)
            {
                //sd.wigName = wig.sprite.texture.name;
                sd.femaleWigProperty = currentFemaleWigProperty;
            }
            if (dress.gameObject.activeSelf && dress.transform.parent.gameObject.activeSelf && dress.color.a > 0.5f)
            {
                //sd.dressName = dress.sprite.texture.name;
                sd.dressProperty = currentDressProperty;
            }
            if (ornament.gameObject.activeSelf && ornament.transform.parent.gameObject.activeSelf && ornament.color.a > 0.5f)
            {
                //sd.ornamentName = ornament.sprite.texture.name;
                sd.ornamentProperty = currentOrnamentProperty;
            }
            if (shoe.gameObject.activeSelf && shoe.transform.parent.gameObject.activeSelf && shoe.color.a > 0.5f)
            {
                sd.shoeName = shoe.sprite.texture.name;
                sd.shoeProperty = currentShoeProperty;
            }

            sd.ReCheckData();


            SaveData.SaveWearings("wearingsdata.dat", sd, this);
        }
		else
		{
			this.InstantiateInfoPopupForPurchase();
		}
	}

	public void CallBackFromSaveWearings(string fullPath)
	{
		base.StartCoroutine(this.TakeScreenshotAndSaveTo(fullPath));
	}

	
	public IEnumerator TakeScreenshotAndSaveTo(string fullPath)
	{
        DeactiveCurrentActive(true, true);
        yield return new WaitForSecondsRealtime(1f);
        ScreenCapture.CaptureScreenshot(fullPath);
        yield return new WaitForSecondsRealtime(1f);
        ActiveCurrentActive(true, true);
    }

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
		this.wig.transform.parent.gameObject.SetActive(false);
		if (changeEditButtonState)
		{
			if ((double)this.wig.color.a > 0.5)
			{
				this.wigEditButton.SetActive(this.wig.transform.parent.gameObject.activeSelf);
			}
			else
			{
				this.wigEditButton.SetActive(false);
			}
		}
		else
		{
			this.wigEditButton.SetActive(false);
		}
	}

	public void ToggleDress(bool changeEditButtonState = false)
	{
		this.dress.transform.parent.gameObject.SetActive(false);
		if (changeEditButtonState)
		{
			if ((double)this.dress.color.a > 0.5)
			{
				this.dressEditButton.SetActive(this.wig.transform.parent.gameObject.activeSelf);
			}
			else
			{
				this.dressEditButton.SetActive(false);
			}
		}
		else
		{
			this.dressEditButton.SetActive(false);
		}
	}

	public void ToggleOrnament(bool changeEditButtonState = false)
	{
		this.ornament.transform.parent.gameObject.SetActive(false);
		if (changeEditButtonState)
		{
		}
	}

	public void ToggleShoe(bool changeEditButtonState = false)
	{
		this.shoe.transform.parent.gameObject.SetActive(false);
		if (changeEditButtonState)
		{
		}
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

	public void AcceptBodyShapeChange()
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
			}
			this.mainBodyShape = b;
			this.mainBodyTone = b2;
			this.mainEyeColor = b3;
			this.mainCarouselRotation = this.selectShapeController.GetcarouselSelectedRotation();
			this.mainModelIndex = this.selectShapeController.GetCarouselSelectedModelIndex();
			this.selectShapeController.OnClickSelectEyeButton(true);
			this.ResetWearing();
		}
		catch (UnityException arg)
		{
			MonoBehaviour.print("Error..in body shape accept  " + arg);
		}
	}

	public void AcceptBodyToneChange()
	{
		GameObject selectedShape = this.rotationController.GetSelectedShape();
		try
		{
			string name = selectedShape.GetComponent<SpriteRenderer>().sprite.texture.name;
			this.femaleModelImageObject.GetComponent<Image>().sprite=(selectedShape.GetComponent<SpriteRenderer>().sprite);
			this.ResetWearing();
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
			this.selectShapeController.OnClickSelectShapeButton(true);
		}
		catch
		{
		}
	}

	public void AcceptEyeColorChange()
	{
		GameObject selectedShape = this.rotationController.GetSelectedShape();
		try
		{
			string name = selectedShape.GetComponent<SpriteRenderer>().sprite.texture.name;
			this.femaleModelImageObject.GetComponent<Image>().sprite=(selectedShape.GetComponent<SpriteRenderer>().sprite);
			this.ResetWearing();
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
			this.selectShapeController.OnClickSelectBodyToneButton(true);
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
			this.ToggleSavedFacesPanel(0);
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
			this.ToggleSavedLooksPanel(0);
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
					base.StartCoroutine(this.saveManager.ResetAllCroppedFaces());
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
					base.StartCoroutine(this.saveManager.ResetAllCroppedFaces());
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
		this.maleParent.SetActive(true);
	}

	public void HideMale()
	{
		this.maleImage.gameObject.SetActive(false);
		this.maleParent.SetActive(false);
	}

	public void ToggleMaleWig(bool changeEditButtonState = false)
	{
		this.maleWig.transform.parent.gameObject.SetActive(false);
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
		this.maleTie.transform.parent.gameObject.SetActive(false);
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

	public void AcceptMaleBody()
	{
		string carouselSelectedMaleModel = this.maleController.GetCarouselSelectedMaleModel();
		this.maleImage.sprite=(this.maleController.GetCarouselSelectedaleModelObject().GetComponent<SpriteRenderer>().sprite);
		this.SetMainMaleBodyName(this.maleImage.sprite.texture.name);
		this.isShowingMale = true;
		this.maleController.OnClickSelectWigsForMale(true);
	}
}
