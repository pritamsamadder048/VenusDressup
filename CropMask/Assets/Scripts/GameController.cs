using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using DG;
using DG.Tweening;




public class GameController : MonoBehaviour {

    Canvas canvas;

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

    //	public bool isFrontCam=false;
    //	public RawImage background;
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

    public CroppedFaceProperties cfp1, cfp2;

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

    private void Awake()
    {
        modelHash = new Dictionary<string, string>();
        toneHash = new Dictionary<string, string>();
        eyeHash = new Dictionary<string, string>();

        modelHash["apple"] = "AP";
        modelHash["banana"] = "BN";
        modelHash["busty"] = "BS";
        modelHash["hourglass"] = "HG";
        modelHash["pear"] = "PR";

        toneHash["dark"] = "DK";
        toneHash["tan"] = "TN";
        toneHash["white"] = "WH";
        toneHash["olive"] = "OL";


        eyeHash["black"] = "BK";
        eyeHash["brown"] = "BR";
        eyeHash["blue"] = "BG";
        eyeHash["green"] = "GH";

        bodyChanged = true;

        aml = loadingPanelObject.transform.GetChild(0).GetComponent<AnimateLoading>();

        saveDict = new Dictionary<string, bool>();
        saveDict["face1"] = false;
        saveDict["face2"] = false;

    }


    private void LateUpdate()
    {
        CollectGurbage();
    }

    public void CollectGurbage()
    {
        currentTimer += 1;
        if (currentTimer > gurbageCollectTime)
        {
            GC.Collect();
            Resources.UnloadUnusedAssets();
            currentTimer = 0;
        }
    }

    public bool IsInHome()
    {
        return isInHome;
    }

    public void EnableAllButtons()
    {
        for (int i = 0; i < homeMenuButtonObjects.Length; i++)
        {
            if (i != -1)
            {
                homeMenuButtonObjects[i].GetComponent<Button>().interactable = true;
            }
        }
    }





    public void InstantiateInfoPopup(String message)
    {
        GameObject g = Instantiate<GameObject>(infoPopupPrefab, canvasObject.transform);
        Text t = g.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        t.text = message;

        Button b = g.transform.GetChild(0).GetChild(1).GetComponent<Button>();
        b.onClick.AddListener(() => { Destroy(g); });
    }




    #region FACE
    public void UsingCustomFace(bool customFaceStatus = false)
    {
        isUsingCustomFace = customFaceStatus;
    }

    public bool IsUsingCustomFace()
    {
        return isUsingCustomFace;
    }

    public void UsingCustomFace2(bool customFaceStatus = false)
    {
        isUsingCustomFace2 = customFaceStatus;
    }

    public bool IsUsingCustomFace2()
    {
        return isUsingCustomFace2;
    }


    public void RemoveFace1()
    {
        isLoadedFace = false;
        loadedFaceIndex = -1;
        faceHash = 0;
        UsingCustomFace(false);
        ShowFaceImage(false);
        currentlyUsingFace -= 1;
        if (currentlyUsingFace < 0)
        {
            currentlyUsingFace = 0;
        }
    }
    public void RemoveFace2()
    {
        isLoadedFace2 = false;
        loadedFaceIndex2 = -1;
        faceHash2 = 0;
        UsingCustomFace2(false);
        ShowFaceImage2(false);
        currentlyUsingFace -= 1;
        if (currentlyUsingFace < 0)
        {
            currentlyUsingFace = 0;
        }
    }


    public void ShowFaceImage(bool show = false)
    {
        faceRawImage.gameObject.SetActive(show);
        isShowingFace = show;
    }
    public void ShowFaceImage2(bool show = false)
    {
        faceRawImage2.gameObject.SetActive(show);
        isShowingFace2 = show;
    }


    public void DiscardProcessing()
    {
        currentlyUsingFace -= 1;

        if (currentlyUsingFace < 0)
        {
            currentlyUsingFace = 0;
        }

        imagePanel.SetActive(false);
        homePanel.SetActive(true);
        mainModel.SetActive(true);
        ToggleHomeSideMenu(1);

        touchController.ToggleMask(1);
        processingImage.sprite = new Sprite();
        foreach (GameObject g in maskButtons)
        {
            g.GetComponent<Toggle>().isOn = false;
            g.transform.GetChild(0).gameObject.SetActive(false);
        }
        touchController.DisableCurrentMask();
        sceneEditorControllerObj.SetActive(true);
        isLoadedFace = false;
        loadedFaceIndex = -1;
        faceHash = 0;
    }



    public void SaveProcessedImage()
    {
        imagePanel.SetActive(false);
        //homePanel.SetActive(true);
        if (currentlySelectedFace == 1)
        {
            isLoadedFace = false;
            loadedFaceIndex = -1;
            faceHash = 0;


        }
        else if (currentlySelectedFace == 2)
        {
            isLoadedFace2 = false;
            loadedFaceIndex2 = -1;
            faceHash2 = 0;

        }
        StartCoroutine(FinalizeImageEdit());
        touchController.ToggleMask(1);
        foreach (GameObject g in maskButtons)
        {
            g.GetComponent<Toggle>().isOn = false;
            g.transform.GetChild(0).gameObject.SetActive(false);
        }
        touchController.DisableCurrentMask();


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





    #endregion FACE

    void Start() {


        canvas = GameObject.FindGameObjectWithTag("canvas").GetComponent<Canvas>();

        if (canvas != null)
        {
            if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
            {
                isScreenSpaceCamera = true;
            }
            else if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
            {
                isScreenSpaceCamera = false;
            }
        }
        animateHint = hint.GetComponent<AnimateHint>();


        if (!Application.isMobilePlatform)
        {
            PlayerPrefs.SetInt("selectedBodyShape", 0);
            PlayerPrefs.SetInt("selectedEyeColor", 0);
            PlayerPrefs.SetInt("selectedBodyTone", 0);
        }



        Screen.sleepTimeout = SleepTimeout.SystemSetting;
        isShowingSideMenu = true;
        galleryController = galleryControllerObject.GetComponent<Gallery>();
        cameraController = cameraControllerObject.GetComponent<CameraController>();
        touchController = touchControllerObject.GetComponent<TouchController>();
        selectDressController = selectDressControllerObject.GetComponent<SelectDressController>();
        sceneEditorController = sceneEditorControllerObj.GetComponent<SceneEditorController>();
        rotationController = rotationControllerObject.GetComponent<RotationController>();
        faceEditController = faceEditControllerObj.GetComponent<FaceEditController>();
        popupController = popupControllerObject.GetComponent<PopUpController>();
        selectShapeController = selectShapeControllerObject.GetComponent<SelectShapeController>();

        HideAcceptCropButton();


        currentShapeIndex = 0;
        //InstantiateInfoPopup(string.Format("w:{0} h:{1}", Screen.width, Screen.height));


    }


    protected void CheckIfFirstTime()
    {
        selectedBodyShape = PlayerPrefs.GetInt("selectedBodyShape");
        selectedEyeColor = PlayerPrefs.GetInt("selectedEyeColor");
        selectedBodyTone = PlayerPrefs.GetInt("selectedBodyTone");

        if (selectedBodyShape <= 0 || selectedEyeColor <= 0 || selectedBodyTone <= 0)
        {
            isFirstTime = true;
            for (int i = 0; i < homeMenuButtonObjects.Length; i++)
            {
                homeMenuButtonObjects[i].GetComponent<Button>().interactable = false;
            }
            animateHint.InitialistHintFor(homeMenuButtonObjects[2]);
            animateHint.StartAnimating();

            homeMenuButtonObjects[2].GetComponent<Button>().interactable = true;
        }
        else
        {
            EnableAllButtons();
            isFirstTime = false;
        }
    }

    private void Update()
    {
        CheckIfFirstTime();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
        if (!isInHome)
        {
            saveButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            saveButton.GetComponent<Button>().interactable = true;
        }
    }



    public void GoToHome()
    {
        if (!isInHome)
        {
            if(panels[5].activeSelf)
            {
                print("yes in it");
                StartCoroutine(BodyShapeSelectionProcess(false));
                isInHome = false;
            }
            else
            {
                if (!isInHome)
                {
                    for (int i = 3; i < panels.Length; i++)
                    {
                        panels[i].SetActive(false);
                    }
                    homePanel.SetActive(true);
                    ToggleHomeSideMenu(1);
                    isInHome = true;
                }
                else
                {
                    ToggleHomeSideMenu();
                    isInHome = true;
                }
                
                maleProjectionParent.SetActive(false);
                if(isShowingMale)
                {
                    ShowMale();
                }
                
                SetPreviousActiveRootPanel(homePanel);
            }


        }
        else
        {
            ToggleHomeSideMenu();
            isInHome = true;
            SetPreviousActiveRootPanel(homePanel);
        }



        if (isUsingCustomFace)
        {
            ShowFaceImage(true);
        }
        else
        {
            ShowFaceImage(false);
        }
        if (isUsingCustomFace2)
        {
            ShowFaceImage2(true);
        }
        else
        {
            ShowFaceImage2(false);
        }

        //if (animateHint.IsShowingHint())
        //{
        //    animateHint.StopAnimating();
        //}
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

        //femaleModelObject.SetActive(true);

        //if (!sceneEditorControllerObj.activeSelf)
        //{
        //    sceneEditorControllerObj.SetActive(true);
        //}
        //if (sceneEditorController.IsShownigobjectPanel())
        //{
        //    sceneEditorController.HideObjectsInSceePanel();
        //}


    }

    public void ToggleHomeSideMenu(int showCode = 0)
    {
        if (isShowingCameraDowneMenu)
        {
            ToggleCameraDownMenu(2);


        }
        if (isShowingBackGroundPanel)
        {
            ToggleBackGroundSideMenu(2);
        }

        if (isShowingSavedFacesMenu)
        {
            ToggleSavedFacesPanel(2);
        }
        if (isShowingSavedLooksMenu)
        {
            ToggleSavedLooksPanel(2);
        }
        if (showCode == 1)
        {
            ToggleOptionSideMenu(2);
            
                sideMenuPanel.GetComponent<RectTransform>().DOAnchorPosX(0f, .3f);
            isShowingSideMenu = true;
        }
        else if (showCode == 2)
        {
            if (!isScreenSpaceCamera)
            {
                sideMenuPanel.GetComponent<RectTransform>().DOAnchorPosX(-400f, .3f);
            }
            else
            {
                sideMenuPanel.GetComponent<RectTransform>().DOAnchorPosX(-400f, .3f);
            }
            isShowingSideMenu = false;
        }
        else
        {
            if (!isShowingSideMenu)
            {
                ToggleOptionSideMenu(2);
                if (!isScreenSpaceCamera)
                {
                    sideMenuPanel.GetComponent<RectTransform>().DOAnchorPosX(0f, .3f);
                }
                else
                {
                    sideMenuPanel.GetComponent<RectTransform>().DOAnchorPosX(0f, .3f);
                }
                isShowingSideMenu = true;
            }
            else
            {
                ToggleOptionSideMenu(2);
                if (!isScreenSpaceCamera)
                {
                    sideMenuPanel.GetComponent<RectTransform>().DOAnchorPosX(-400f, .3f);
                }
                else
                {
                    sideMenuPanel.GetComponent<RectTransform>().DOAnchorPosX(-400f, .3f);
                }
                isShowingSideMenu = false;
            }
        }

        if (!sceneEditorControllerObj.activeSelf)
        {
            sceneEditorControllerObj.SetActive(true);
        }
        if (sceneEditorController.IsShownigobjectPanel())
        {
            sceneEditorController.HideObjectsInSceePanel();
        }
    }



    public void ToggleOptionSideMenu(int showCode = 0)
    {
        if (isShowingSavedFacesMenu)
        {
            ToggleSavedFacesPanel(2);
        }
        if (isShowingSavedLooksMenu)
        {
            ToggleSavedLooksPanel(2);
        }
        if(isShowingBackGroundPanel)
        {
            ToggleBackGroundSideMenu(2);
        }
        if (animateHint.IsShowingHint())
        {
            animateHint.StopAnimating();
        }

        if (showCode == 1)
        {
            DeactiveCurrentActive();
            if (!isScreenSpaceCamera)
            {
                OptionSideMenuPanel.GetComponent<RectTransform>().DOAnchorPosX(0f, .3f);
            }
            else
            {
                OptionSideMenuPanel.GetComponent<RectTransform>().DOAnchorPosX(0f, .3f);
            }
            isShowingOptionSideMenu = true;
        }
        else if (showCode == 2)
        {
            if (!isScreenSpaceCamera)
            {
                OptionSideMenuPanel.GetComponent<RectTransform>().DOAnchorPosX(-400f, .3f);
            }
            else
            {
                OptionSideMenuPanel.GetComponent<RectTransform>().DOAnchorPosX(-400f, .3f);
            }
            isShowingOptionSideMenu = false;
        }
        else
        {
            if (!isShowingOptionSideMenu)
            {
                DeactiveCurrentActive();
                if (!isScreenSpaceCamera)
                {
                    OptionSideMenuPanel.GetComponent<RectTransform>().DOAnchorPosX(0f, .3f);
                }
                else
                {
                    OptionSideMenuPanel.GetComponent<RectTransform>().DOAnchorPosX(0f, .3f);
                }
                isShowingOptionSideMenu = true;
            }
            else
            {
                if (!isScreenSpaceCamera)
                {
                    OptionSideMenuPanel.GetComponent<RectTransform>().DOAnchorPosX(-400f, .3f);
                }
                else
                {
                    OptionSideMenuPanel.GetComponent<RectTransform>().DOAnchorPosX(-400f, .3f);
                }
                isShowingOptionSideMenu = false;
            }
        }

        if (isShowingOptionSideMenu)
        {

            MenubarObject.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            MenubarObject.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
        }

        if (!sceneEditorControllerObj.activeSelf)
        {
            sceneEditorControllerObj.SetActive(true);
        }
        if (sceneEditorController.IsShownigobjectPanel())
        {
            sceneEditorController.HideObjectsInSceePanel();
        }
    }

    public void ToggleBackGroundSideMenu(int showCode = 0)
    {
        panels[10].SetActive(true);
        if (isShowingCameraDowneMenu)
        {
            ToggleCameraDownMenu(2);


        }

        if (isShowingSavedFacesMenu)
        {
            ToggleSavedFacesPanel(2);
        }
        if (isShowingSavedLooksMenu)
        {
            ToggleSavedLooksPanel(2);
        }
        if (showCode == 1)
        {
            ToggleOptionSideMenu(2);
            ToggleHomeSideMenu(2);
            backGroundPanel.GetComponent<RectTransform>().DOAnchorPosX(0f, .3f);
            isShowingBackGroundPanel = true;
        }
        else if (showCode == 2)
        {
            if (!isScreenSpaceCamera)
            {
                backGroundPanel.GetComponent<RectTransform>().DOAnchorPosX(-800f, .3f);
            }
            else
            {
                backGroundPanel.GetComponent<RectTransform>().DOAnchorPosX(-800f, .3f);
            }
            isShowingBackGroundPanel = false;
        }
        else
        {
            if (!isShowingBackGroundPanel)
            {
                ToggleOptionSideMenu(2);
                ToggleHomeSideMenu(2);
                if (!isScreenSpaceCamera)
                {
                    backGroundPanel.GetComponent<RectTransform>().DOAnchorPosX(0f, .3f);
                }
                else
                {
                    backGroundPanel.GetComponent<RectTransform>().DOAnchorPosX(0f, .3f);
                }
                isShowingBackGroundPanel = true;
            }
            else
            {
                ToggleOptionSideMenu(2);
                ToggleHomeSideMenu(2);
                if (!isScreenSpaceCamera)
                {
                    backGroundPanel.GetComponent<RectTransform>().DOAnchorPosX(-800f, .3f);
                }
                else
                {
                    backGroundPanel.GetComponent<RectTransform>().DOAnchorPosX(-800f, .3f);
                }
                isShowingBackGroundPanel = false;
            }
        }

        if (!sceneEditorControllerObj.activeSelf)
        {
            sceneEditorControllerObj.SetActive(true);
        }
        if (sceneEditorController.IsShownigobjectPanel())
        {
            sceneEditorController.HideObjectsInSceePanel();
        }
    }


    public void ToggleCameraDownMenu(int showCode = 0)
    {
        if (animateHint.IsShowingHint())
        {
            animateHint.StopAnimating();
        }

        if (showCode == 1)
        {
            //DeactiveCurrentActive();
            CameraDownMenuPanel.GetComponent<RectTransform>().DOAnchorPosY(0f, .3f);
            isShowingCameraDowneMenu = true;
        }
        else if (showCode == 2)
        {
            CameraDownMenuPanel.GetComponent<RectTransform>().DOAnchorPosY(-400f, .3f);
            isShowingCameraDowneMenu = false;
        }
        else
        {
            if (!isShowingCameraDowneMenu)
            {
                print("on");
                //DeactiveCurrentActive();
                CameraDownMenuPanel.GetComponent<RectTransform>().DOAnchorPosY(0f, .3f);
                isShowingCameraDowneMenu = true;
            }
            else
            {
                print("off");
                CameraDownMenuPanel.GetComponent<RectTransform>().DOAnchorPosY(-400f, .3f);
                isShowingCameraDowneMenu = false;
            }
        }

       
    }

    public void ToggleSavedFacesPanel(int showStatus = 0)
    {



        switch (showStatus)
        {
            case 0:
            default:
                {
                    panels[7].SetActive(true);
                    if (!isShowingSavedFacesMenu)
                    {
                        if (isShowingCameraDowneMenu)
                        {
                            ToggleCameraDownMenu(2);
                        }
                        if (isShowingOptionSideMenu)
                        {
                            ToggleOptionSideMenu(2);
                        }
                        if (isShowingSideMenu)
                        {
                            ToggleHomeSideMenu(2);
                        }
                        if (isShowingSavedLooksMenu)
                        {
                            ToggleSavedLooksPanel(2);
                        }
                        panels[7].GetComponent<RectTransform>().DOAnchorPosX(0f, .3f);
                        isShowingSavedFacesMenu = true;
                        StartCoroutine(saveManager.ResetAllCroppedFaces());
                        if (saveDict["face1"] || saveDict["face2"])
                        {
                            popupController.ShowPopup(2, "Save The Face");
                        }
                    }
                    else
                    {
                        if (isShowingCameraDowneMenu)
                        {
                            ToggleCameraDownMenu(2);
                        }
                        if (isShowingOptionSideMenu)
                        {
                            ToggleOptionSideMenu(2);
                        }
                        if (isShowingSideMenu)
                        {
                            ToggleHomeSideMenu(2);
                        }
                        panels[7].GetComponent<RectTransform>().DOAnchorPosX(-400f, .3f);
                        isShowingSavedFacesMenu = false;
                    }
                    break;
                }
            case 1:
                {
                    if (isShowingCameraDowneMenu)
                    {
                        ToggleCameraDownMenu(2);
                    }
                    if (isShowingOptionSideMenu)
                    {
                        ToggleOptionSideMenu(2);
                    }
                    if (isShowingSideMenu)
                    {
                        ToggleHomeSideMenu(2);
                    }
                    panels[7].GetComponent<RectTransform>().DOAnchorPosX(0f, .3f);
                    isShowingSavedFacesMenu = true;
                    StartCoroutine(saveManager.ResetAllCroppedFaces());
                    if (saveDict["face1"] || saveDict["face2"])
                    {
                        popupController.ShowPopup(2, "Save The Face");
                    }
                    break;
                }
            case 2:
                {
                    panels[7].GetComponent<RectTransform>().DOAnchorPosX(-400f, .3f);
                    isShowingSavedFacesMenu = false;
                    break;
                }

        }


    }


    public void ToggleSavedLooksPanel(int showStatus = 0)
    {



        switch (showStatus)
        {
            case 0:
            default:
                {
                    panels[8].SetActive(true);
                    if (!isShowingSavedLooksMenu)
                    {
                        print("toggling saved look panels");
                        if (isShowingCameraDowneMenu)
                        {
                            ToggleCameraDownMenu(2);
                        }
                        if (isShowingOptionSideMenu)
                        {
                            ToggleOptionSideMenu(2);
                        }
                        if (isShowingSideMenu)
                        {
                            ToggleHomeSideMenu(2);
                        }
                        if (isShowingSavedFacesMenu)
                        {
                            ToggleSavedFacesPanel(2);
                        }
                        panels[8].GetComponent<RectTransform>().DOAnchorPosX(0f, .3f);
                        isShowingSavedLooksMenu = true;
                        StartCoroutine(saveManager.ResetAllSavedLooks());
                        //saveManager.ResetAllCroppedFaces();
                        //if (!faceIsSaved)
                        //{
                        //    popupController.ShowPopup(2, "Save The Face");
                        //}
                    }
                    else
                    {
                        if (isShowingCameraDowneMenu)
                        {
                            ToggleCameraDownMenu(2);
                        }
                        if (isShowingOptionSideMenu)
                        {
                            ToggleOptionSideMenu(2);
                        }
                        if (isShowingSideMenu)
                        {
                            ToggleHomeSideMenu(2);
                        }
                        if (isShowingSavedFacesMenu)
                        {
                            ToggleSavedFacesPanel(2);
                        }
                        panels[8].GetComponent<RectTransform>().DOAnchorPosX(-400f, .3f);
                        isShowingSavedLooksMenu = false;
                    }
                    break;
                }
            case 1:
                {
                    if (isShowingCameraDowneMenu)
                    {
                        ToggleCameraDownMenu(2);
                    }
                    if (isShowingOptionSideMenu)
                    {
                        ToggleOptionSideMenu(2);
                    }
                    if (isShowingSideMenu)
                    {
                        ToggleHomeSideMenu(2);
                    }
                    if (isShowingSavedFacesMenu)
                    {
                        ToggleSavedFacesPanel(2);
                    }
                    panels[8].GetComponent<RectTransform>().DOAnchorPosX(0f, .3f);
                    isShowingSavedLooksMenu = true;
                    StartCoroutine(saveManager.ResetAllSavedLooks());
                    //saveManager.ResetAllCroppedFaces();
                    //if (!faceIsSaved)
                    //{
                    //    popupController.ShowPopup(2, "Save The Face");
                    //}
                    break;
                }
            case 2:
                {
                    panels[8].GetComponent<RectTransform>().DOAnchorPosX(-400f, .3f);
                    isShowingSavedLooksMenu = false;
                    break;
                }

        }


    }

    public void OnPressCameraButton(GameObject g)
    {
        //g.transform.GetChild(1).gameObject.SetActive(false);

        ToggleCameraDownMenu();

        //femaleModelObject.SetActive(false);
        //if (animateHint.IsShowingHint())
        //{
        //    animateHint.StopAnimating();
        //}
        //ToggleHomeSideMenu(2);
        //isInHome = false;
        //cameraController.OpenCamera();
    }

    public void ConfirmBodyShapeSelection(GameObject g)
    {
        StartCoroutine(BodyShapeSelectionProcess(true));
        popupController.HidePopup();
    }
    public void OnPressSelectBodyShapeButton(GameObject g)
    {
        //g.transform.GetChild(1).gameObject.SetActive(false);
        if (isFirstTime)
        {
            StartCoroutine(BodyShapeSelectionProcess(true));
        }
        else
        {
            popupController.ShowPopup(3);
        }

        //print("Pressed shape button");
        //femaleModelObject.SetActive(false);
        //if (animateHint.IsShowingHint())
        //{
        //    animateHint.StopAnimating();
        //}
        //ToggleHomeSideMenu(2);
        //isInHome = false;

        //for (int i = 2; i < panels.Length; i++)
        //{
        //    panels[i].SetActive(false);
        //}
        //panels[5].SetActive(true);
        //if (isFirstTime)
        //{
        //    popupController.ShowPopup(0, "NOW, SEE THE BEST\nDRESS STYLE FOR YOUR\nBODY TYPE");
        //}

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

            if (sideMenuPanel==previousActiveSidePanel || previousActiveRootPanel==homePanel)
            {
                
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
            ActivePreviousActive(true);
            yield return null;

            if (isFirstTime)
            {
                popupController.ShowPopup(0, "NOW, SEE THE BEST\nDRESS STYLE FOR YOUR\nBODY TYPE");
            }
        }
    }
    public void OnPressDressForHerButton(GameObject g)
    {
        //femaleModelObject.SetActive(true);
        //g.transform.GetChild(1).gameObject.SetActive(false);
        SetPreviousActiveRootPanel(panels[6]);
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
        selectDressController.OnClickSelectLongDressButton(true);
        panels[6].SetActive(true);
    }

    public void OnPressForHimButton(GameObject g)
    {
        //femaleModelObject.SetActive(true);
        //g.transform.GetChild(1).gameObject.SetActive(false);
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
        panels[9].SetActive(true);
        maleController.OnClickSelectMale(true);
        maleProjectionParent.SetActive(true);
        maleProjectionParent.transform.GetChild(0).GetComponent<RawImage>().DOFade(1f, .3f);
    }

    public void OnPressBackGroundbutton(GameObject g)
    {
        if (animateHint.IsShowingHint())
        {
            animateHint.StopAnimating();
        }
        ToggleBackGroundSideMenu(0);

    }

    public void OnPresWigsForHerButton(GameObject g)
    {
        //g.transform.GetChild(1).gameObject.SetActive(false);
        if (animateHint.IsShowingHint())
        {
            animateHint.StopAnimating();
        }
        //ToggleHomeSideMenu(2);
        //isInHome = false;

        //StartCoroutine(PutOnDress("female", "dress_1"));
        //PutOnWig("female", "wig_1");
        //dress.gameObject.SetActive(!dress.gameObject.activeSelf);


        if (!showingWig)
        {
            wig.gameObject.GetComponent<Image>().DOFade(1f, .5f);
            showingWig = true;
        }
        else
        {
            wig.gameObject.GetComponent<Image>().DOFade(0f, .5f);
            showingWig = false;
        }
    }


    public void OnPresShoesForHerButton(GameObject g)
    {
        //g.transform.GetChild(1).gameObject.SetActive(false);
        if (animateHint.IsShowingHint())
        {
            animateHint.StopAnimating();
        }
        //ToggleHomeSideMenu(2);
        //isInHome = false;

        //StartCoroutine(PutOnDress("female", "dress_1"));
        //PutOnShoe("female", "shoe_1");
        //dress.gameObject.SetActive(!dress.gameObject.activeSelf);



        if (!showingShoe)
        {
            shoe.gameObject.GetComponent<Image>().DOFade(1f, .5f);
            showingShoe = true;
        }
        else
        {
            shoe.gameObject.GetComponent<Image>().DOFade(0f, .5f);
            showingShoe = false;
        }
    }
    public void OnPressGalleryButton(GameObject g)
    {
        //g.transform.GetChild(1).gameObject.SetActive(false);
        if (animateHint.IsShowingHint())
        {
            animateHint.StopAnimating();
        }
        ToggleHomeSideMenu(2);
        isInHome = false;
        galleryController.OnOpenGallery();
        sceneEditorControllerObj.SetActive(false);
    }





    public void SetCurrentActive(GameObject activePanel = null, GameObject activeButton = null, GameObject activeDownPanel = null)
    {
        currentActiveButton = activeButton;
        currentActivePanel = activePanel;
        currentActiveDownPanel = activeDownPanel;

    }

    public void SetPreviousActive(GameObject activePanel = null, GameObject activeDownPanel = null, GameObject activeButton = null)
    {
        previousActiveButton = activeButton;
        previousActiveSidePanel = activePanel;
        previousActiveDownPanel = activeDownPanel;

    }
    public void SetPreviousActiveRootPanel(GameObject g)
    {
        previousActiveRootPanel = g;
    }

    public void ActiveCurrentActive(bool toggleDownPanelToo = false , bool reallyActiveCurrentActive=false)
    {

        if (isInHome)
        {
            ToggleHomeSideMenu(1);
        }
        else
        {
            if (currentActiveButton != null)
            {
                Toggle curToggle = currentActiveButton.GetComponent<Toggle>();
                if (curToggle != null)
                {
                    curToggle.isOn = true;
                }
            }
            if (currentActivePanel != null)
            {

                currentActivePanel.GetComponent<RectTransform>().DOAnchorPosX(0f, .3f);
            }
            if (toggleDownPanelToo)
            {
                if (currentActiveDownPanel != null)
                {
                    currentActiveDownPanel.GetComponent<RectTransform>().DOAnchorPosY(0f, .3f);
                }
            }
        }
        if(reallyActiveCurrentActive)
        {
            panels[0].SetActive(true);
        }

    }


    public void ActivePreviousActive(bool toggleDownPanelToo = false, bool reallyActiveCurrentActive = false)
    {
        previousActiveRootPanel.SetActive(true);
        if (isInHome)
        {
            ToggleHomeSideMenu(1);
        }
        else
        {
            if (previousActiveButton != null)
            {
                Toggle curToggle = currentActiveButton.GetComponent<Toggle>();
                if (curToggle != null)
                {
                    curToggle.isOn = true;
                }
            }
            if (previousActiveSidePanel != null)
            {

                previousActiveSidePanel.GetComponent<RectTransform>().DOAnchorPosX(0f, .3f);
            }
            if (toggleDownPanelToo)
            {
                if (previousActiveDownPanel != null)
                {
                    previousActiveDownPanel.GetComponent<RectTransform>().DOAnchorPosY(0f, .3f);
                }
            }
        }
        if (reallyActiveCurrentActive)
        {
            panels[0].SetActive(true);
        }

    }

    public void DeactiveCurrentActive(bool toggleDownPanelToo = false, bool reallyHideEverything=false)
    {

        if (isInHome)
        {
            ToggleHomeSideMenu(2);
        }
        else
        {
            if (currentActiveButton != null)
            {
                Toggle curToggle = currentActiveButton.GetComponent<Toggle>();
                if (curToggle != null)
                {
                    curToggle.isOn = false;
                }
            }
            if (currentActivePanel != null)
            {

                currentActivePanel.GetComponent<RectTransform>().DOAnchorPosX(-400f, .3f);
            }
            if (toggleDownPanelToo || reallyHideEverything)
            {
                if (currentActiveDownPanel != null)
                {
                    currentActiveDownPanel.GetComponent<RectTransform>().DOAnchorPosY(-600f, .3f);
                }
            }
        }
        if(reallyHideEverything)
        {
            panels[0].SetActive(false);
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
        if (Application.isEditor)
        {
            if (UnityEditor.EditorApplication.isPlaying)
            {
                UnityEditor.EditorApplication.isPlaying = false;
            }
        }
#endif

        Application.Quit();
    }








    public void ShowLoading() //show loading screen
    {
        loadingPanelObject.SetActive(true);
        aml.StartRotatingStatic();
    }
    public void HideLoading() //hide loading screen
    {
        loadingPanelObject.SetActive(false);
        aml.StopRotating();
    }



    public void ResetCroppedFace()  ///reset the cropped face selected
    {
        //print("clicked reset");
        Color[] fc = Enumerable.Repeat(Color.clear, faceRawImage.texture.width * faceRawImage.texture.height).ToArray();
        ((Texture2D)faceRawImage.texture).SetPixels(fc);
        ((Texture2D)faceRawImage.texture).Apply();
        faceRawImage.gameObject.SetActive(false);

    }

    public void ChangeBodyToneColor(Color toneColor)
    {
        Image selectedFemaleShapeTex = femaleModelImageObject.GetComponent<Image>();
        selectedFemaleShapeTex.color = toneColor;
    }

    public void ShowAcceptCropButton()  //show the accept button on crop panel
    {
        acceptCropButtonObject.SetActive(true);
    }
    public void HideAcceptCropButton()  //hide the accept button on crop panel
    {
        acceptCropButtonObject.SetActive(false);
    }



    public void SaveDataToDisk(bool newSave = true)
    {
        SaveData sd = new SaveData();

        sd.Initialize(mainBodyShape, mainBodyTone,mainEyeColor);
        if(wig.gameObject.activeSelf && wig.transform.parent.gameObject.activeSelf&&wig.color.a>0.5f)
        {
            sd.wigName = wig.sprite.texture.name;
        }
        if (dress.gameObject.activeSelf && dress.transform.parent.gameObject.activeSelf && dress.color.a > 0.5f)
        {
            sd.dressName = dress.sprite.texture.name;
        }
        if (ornament.gameObject.activeSelf && ornament.transform.parent.gameObject.activeSelf && ornament.color.a > 0.5f)
        {
            sd.ornamentName = ornament.sprite.texture.name;
        }
        if (shoe.gameObject.activeSelf && shoe.transform.parent.gameObject.activeSelf && shoe.color.a > 0.5f)
        {
            sd.shoeName = shoe.sprite.texture.name;
            
        }

        
        SaveData.SaveWearings("wearingsdata.dat", sd,this);
        

    }

    public void CallBackFromSaveWearings(string fullPath)
    {
        StartCoroutine(TakeScreenshotAndSaveTo(fullPath));
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
        ToggleFace(false, true);
    }
    public void ToggleFace(bool newStatus=false,bool changeCustomFaceShowingStatus=false)
    {
        if(isUsingCustomFace)
        {
            ShowFaceImage(newStatus);
        }
        if(isUsingCustomFace2)
        {
            ShowFaceImage2(newStatus);
        }
        if(changeCustomFaceShowingStatus)
        {
            isUsingCustomFace = newStatus;
            isUsingCustomFace2 = newStatus;
        }
    }

    public void ToggleWig(bool changeEditButtonState = false)
    {
        //wig.transform.parent.gameObject.SetActive(!wig.transform.parent.gameObject.activeSelf);
        wig.transform.parent.gameObject.SetActive(false);
        if (changeEditButtonState)
        {
            if (wig.color.a > 0.5)
            {
                wigEditButton.SetActive(wig.transform.parent.gameObject.activeSelf);
            }
            else
            {
                wigEditButton.SetActive(false);
            }
        }
        else
        {
            wigEditButton.SetActive(false);
        }
    }

    public void ToggleDress(bool changeEditButtonState = false)
    {
        //dress.transform.parent.gameObject.SetActive(!dress.transform.parent.gameObject.activeSelf);
        dress.transform.parent.gameObject.SetActive(false);
        if (changeEditButtonState)
        {
            if (dress.color.a > 0.5)
            {
                dressEditButton.SetActive(wig.transform.parent.gameObject.activeSelf);
            }
            else
            {
                dressEditButton.SetActive(false);
            }
        }
        else
        {
            dressEditButton.SetActive(false);
        }
    }
    public void ToggleOrnament(bool changeEditButtonState = false)
    {
        //ornament.transform.parent.gameObject.SetActive(!ornament.transform.parent.gameObject.activeSelf);
        ornament.transform.parent.gameObject.SetActive(false);
        if (changeEditButtonState)
        {
            //if (dress.color.a > 0.5)
            //{
            //    wigEditButton.SetActive(wig.transform.parent.gameObject.activeSelf);
            //}
            //else
            //{
            //    wigEditButton.SetActive(false);
            //}
        }
        else
        {
            //wigEditButton.SetActive(false);
        }
    }

    public void ToggleShoe(bool changeEditButtonState = false)
    {
        //shoe.transform.parent.gameObject.SetActive(!shoe.transform.parent.gameObject.activeSelf);
        shoe.transform.parent.gameObject.SetActive(false);
        if (changeEditButtonState)
        {
            //if (dress.color.a > 0.5)
            //{
            //    wigEditButton.SetActive(wig.transform.parent.gameObject.activeSelf);
            //}
            //else
            //{
            //    wigEditButton.SetActive(false);
            //}
        }
        else
        {
            //wigEditButton.SetActive(false);
        }
    }

    public string GetMainBodyShape()
    {
        return mainBodyShape;
    }

    public string GetMainBodyTone()
    {
        return mainBodyTone;
    }
    public string GetMainEyeColor()
    {
        return mainEyeColor;
    }


    public void AcceptBodyShapeChange()
    {

        GameObject to = rotationController.GetSelectedShape();
        try
        {
            string name = to.GetComponent<SpriteRenderer>().sprite.texture.name;
            femaleModelImageObject.GetComponent<Image>().sprite = to.GetComponent<SpriteRenderer>().sprite;
            string newShape= name.Split('_')[0]; 
            string newTone= name.Split('_')[1]; 
            string newEye= name.Split('_')[2];
            if(mainBodyShape!=newShape || mainBodyTone!=newTone || mainEyeColor!=newEye)
            {
                bodyChanged = true;
            }
            mainBodyShape = newShape;
            mainBodyTone = newTone;
            mainEyeColor = newEye;
            mainCarouselRotation = selectShapeController.GetcarouselSelectedRotation();
            mainModelIndex = selectShapeController.GetCarouselSelectedModelIndex();
            selectShapeController.OnClickSelectEyeButton(true);
            ResetWearing();
        }
        catch (UnityException e)
        {
            print("Error..in body shape accept  " + e);
        }
    }
    public void AcceptBodyToneChange()
    {

        GameObject to = rotationController.GetSelectedShape();
        try
        {
            string name = to.GetComponent<SpriteRenderer>().sprite.texture.name;
            femaleModelImageObject.GetComponent<Image>().sprite = to.GetComponent<SpriteRenderer>().sprite;
            //if(mainBodyShape!=name.Split('_')[0])
            //{
            //    ResetWearing();
            //}
            ResetWearing();

            string newShape = name.Split('_')[0];
            string newTone = name.Split('_')[1];
            string newEye = name.Split('_')[2];
            if (mainBodyShape != newShape || mainBodyTone != newTone || mainEyeColor != newEye)
            {
                bodyChanged = true;
            }
            mainBodyShape = newShape;
            mainBodyTone = newTone;
            mainEyeColor = newEye;
            
            mainCarouselRotation = selectShapeController.GetcarouselSelectedRotation();
            mainModelIndex = selectShapeController.GetCarouselSelectedModelIndex();
            selectShapeController.OnClickSelectShapeButton(true);
        }
        catch
        {

        }
    }

    public void AcceptEyeColorChange()
    {

        GameObject to = rotationController.GetSelectedShape();
        try
        {
            string name = to.GetComponent<SpriteRenderer>().sprite.texture.name;
            femaleModelImageObject.GetComponent<Image>().sprite = to.GetComponent<SpriteRenderer>().sprite;
            //if (mainBodyShape != name.Split('_')[0])
            //{
            //    ResetWearing();
            //}
            ResetWearing();

            string newShape = name.Split('_')[0];
            string newTone = name.Split('_')[1];
            string newEye = name.Split('_')[2];
            if (mainBodyShape != newShape || mainBodyTone != newTone || mainEyeColor != newEye)
            {
                bodyChanged = true;
            }
            mainBodyShape = newShape;
            mainBodyTone = newTone;
            mainEyeColor = newEye;
            mainCarouselRotation = selectShapeController.GetcarouselSelectedRotation();
            mainModelIndex = selectShapeController.GetCarouselSelectedModelIndex();
            selectShapeController.OnClickSelectBodyToneButton(true);
        }
        catch
        {

        }
    }

    public void ResetWearing()
    {
        selectDressController.SetIsWearingDress(false);
        selectDressController.SetIsWearingWig(false);
        selectDressController.SetIsWearingOrnament(false);
        selectDressController.SetIsWearingShoe(false);
    }

    

    public void OnClickSavedFacesButton()
    {
        ToggleSavedFacesPanel(0);
    }

    
    public void OnClickSavedLooksButton()
    {
        ToggleSavedLooksPanel(0);
    }


    public void SaveCroppedFace()
    {
        popupController.HidePopup();
        if (saveDict["face1"])
        {
            CroppedFaceData cfd = new CroppedFaceData();
            cfd.Initialize(faceRawImage);
            int status= CroppedFaceData.SaveCroppedFace("croppedfaces.dat", cfd,faceRawImage);
            

             switch(status)
            {
                case 0:
                    {
                        popupController.ShowPopup(1, "You can save 5 faces only!");
                        break;
                    }
                case 1:
                    {
                        saveDict["face1"] = false;
                        isLoadedFace = true;
                        faceHash = cfd.saveFaceHash;
                                    StartCoroutine(saveManager.ResetAllCroppedFaces());
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            
        }
        if (saveDict["face2"])
        {
            CroppedFaceData cfd = new CroppedFaceData();
            cfd.Initialize(faceRawImage2);
            int status = CroppedFaceData.SaveCroppedFace("croppedfaces.dat", cfd, faceRawImage2);


            switch (status)
            {
                case 0:
                    {
                        popupController.ShowPopup(1, "You can save 5 faces only!");
                        break;
                    }
                case 1:
                    {
                        saveDict["face2"] = false;
                        isLoadedFace2 = true;
                        faceHash2 = cfd.saveFaceHash;
                        StartCoroutine(saveManager.ResetAllCroppedFaces());
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

           
        }

        StartCoroutine(saveManager.ResetAllCroppedFaces());
    }








    #region MALE


    public void DeactiveMale()
    {
        HideMale();
    }

    public void ShowMale()
    {
        
        maleImage.gameObject.SetActive(true);
        maleParent.SetActive(true);
        //maleImage.DOFade(1f, .3f);
    }

    public void HideMale()
    {


        maleImage.gameObject.SetActive(false);
        maleParent.SetActive(false);

    }
    public void ToggleMaleWig(bool changeEditButtonState = false)
    {
        //wig.transform.parent.gameObject.SetActive(!wig.transform.parent.gameObject.activeSelf);
        maleWig.transform.parent.gameObject.SetActive(false);
        if (changeEditButtonState)
        {
            if (maleWig.color.a > 0.5)
            {
                maleController.editButtons[0].SetActive(maleWig.transform.parent.gameObject.activeSelf);
            }
            else
            {
                maleController.editButtons[0].SetActive(false);
            }
        }
        else
        {
            maleController.editButtons[0].SetActive(false);
        }
    }

    public void ToggleMaleTie(bool changeEditButtonState = false)
    {
        //wig.transform.parent.gameObject.SetActive(!wig.transform.parent.gameObject.activeSelf);
        maleTie.transform.parent.gameObject.SetActive(false);
        if (changeEditButtonState)
        {
            if (maleTie.color.a > 0.5)
            {
                maleController.editButtons[1].SetActive(maleTie.transform.parent.gameObject.activeSelf);
            }
            else
            {
                maleController.editButtons[1].SetActive(false);
            }
        }
        else
        {
            maleController.editButtons[1].SetActive(false);
        }
    }


    public string GetMainMaleBodyName()
    {
        return mainMaleBodyName;
    }
    public void SetMainMaleBodyName(string val)
    {
        mainMaleBodyName = val;
    }

    public void AcceptMaleBody()
    {
        string cmn = maleController.GetCarouselSelectedMaleModel();
        maleImage.sprite = maleController.GetCarouselSelectedaleModelObject().GetComponent<SpriteRenderer>().sprite;
        SetMainMaleBodyName(maleImage.sprite.texture.name);
        isShowingMale = true;
        maleController.OnClickSelectWigsForMale(true);
    }
    
    #endregion MALE

















}














/*
<? xml version="1.0" encoding="utf-8"?>
<manifest xmlns:android="http://schemas.android.com/apk/res/android" package="com.example.camerashots" android:theme="@android:style/Theme.NoTitleBar" android:versionName="1.0" android:versionCode="1" android:installLocation="preferExternal">
  <supports-screens
        android:smallScreens="true"
        android:normalScreens="true"
        android:largeScreens="true"
        android:xlargeScreens="true"
        android:anyDensity="true"/>
  <application android:icon="@drawable/app_icon" android:label="@string/app_name" android:debuggable="false">
    <activity android:name="com.unity3d.player.UnityPlayerNativeActivity" android:label="@string/app_name" android:screenOrientation="portrait" android:launchMode="singleTask" android:configChanges="mcc|mnc|locale|touchscreen|keyboard|keyboardHidden|navigation|orientation|screenLayout|uiMode|screenSize|smallestScreenSize|fontScale">
      <intent-filter>
        <action android:name="android.intent.action.MAIN" />
        <category android:name="android.intent.category.LAUNCHER" />
      </intent-filter>
      <meta-data android:name="unityplayer.UnityActivity" android:value="true" />
      <meta-data android:name="unityplayer.ForwardNativeEventsToDalvik" android:value="false" />
    </activity>
<activity
    android:name="com.astricstore.camerashots.CameraShotActivity"
    android:configChanges="orientation|keyboardHidden|screenSize">
</activity>
<activity
    android:name="eu.janmuller.android.simplecropimage.CropImage"
    android:configChanges="orientation|keyboardHidden|screenSize">
    </activity>
    
  <activity android:name= "com.radikallabs.androidgallery.Gallery"></activity>
    
  </application>
  <uses-sdk android:minSdkVersion="9" android:targetSdkVersion="18" />
  <uses-feature android:glEsVersion="0x00020000" />
  <uses-permission android:name="android.permission.INTERNET" />
  <uses-permission android:name="android.permission.READ_EXTERNAL_STORAGE" />
	<uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />
	<uses-permission android:name="android.permission.WRITE_INTERNAL_STORAGE" /> 
	<uses-permission android:name="android.permission.READ_INTERNAL_STORAGE" /> 
  <uses-permission android:name="android.permission.CAMERA" />
	<uses-feature android:name="android.hardware.camera" android:required="false" />
	<uses-feature android:name="android.hardware.camera.front" android:required="false" />
</manifest>

    */







/*
 * 
 * <?xml version="1.0" encoding="utf-8"?>
<manifest xmlns:android="http://schemas.android.com/apk/res/android" package="com.astricstore.imagevideocontactpicker" android:theme="@android:style/Theme.NoTitleBar" android:versionName="1.0" android:versionCode="1" android:installLocation="preferExternal">
<supports-screens
    android:smallScreens="true"
    android:normalScreens="true"
    android:largeScreens="true"
    android:xlargeScreens="true"
    android:anyDensity="true"/>

<application android:icon="@drawable/app_icon" android:label="@string/app_name" android:debuggable="false">

<activity android:name="com.unity3d.player.UnityPlayerNativeActivity" android:label="@string/app_name" android:screenOrientation="portrait" android:launchMode="singleTask" android:configChanges="mcc|mnc|locale|touchscreen|keyboard|keyboardHidden|navigation|orientation|screenLayout|uiMode|screenSize|smallestScreenSize|fontScale">
  <intent-filter>
    <action android:name="android.intent.action.MAIN" />
    <category android:name="android.intent.category.LAUNCHER" />
  </intent-filter>

  <meta-data android:name="unityplayer.UnityActivity" android:value="true" />
  <meta-data android:name="unityplayer.ForwardNativeEventsToDalvik" android:value="false" />
</activity>

<activity 
android:name="com.astricstore.imagevideocontactpicker.AndroidPickerActivity"
android:configChanges="orientation|keyboardHidden|screenSize">
</activity>

<activity
android:name="com.astricstore.camerashots.CameraShotActivity"
android:configChanges="orientation|keyboardHidden|screenSize">
</activity>

 <activity 
android:name="eu.janmuller.android.simplecropimage.CropImage"
android:configChanges="orientation|keyboardHidden|screenSize">
</activity>

<activity android:name= "com.radikallabs.androidgallery.Gallery"></activity>
</application>
<uses-sdk android:minSdkVersion="9" android:targetSdkVersion="18" />
<uses-feature android:glEsVersion="0x00020000" />
<uses-permission android:name="android.permission.INTERNET" />
<uses-permission android:name="android.permission.READ_CONTACTS"/>
<uses-permission android:name="android.permission.READ_EXTERNAL_STORAGE" />
<uses-permission android:name="android.permission.WRITE_EXTERNAL_STORAGE" />
<uses-permission android:name="android.permission.WRITE_INTERNAL_STORAGE" />
<uses-permission android:name="android.permission.READ_INTERNAL_STORAGE" />
<uses-permission android:name="android.permission.CAMERA" />
<uses-feature android:name="android.hardware.camera" android:required="false" />
<uses-feature android:name="android.hardware.camera.front" android:required="false" />
</manifest>


    */




