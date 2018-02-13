using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using DG;
using DG.Tweening;
using UnityEngine.Networking;

public class SelectDressController : MonoBehaviour {

    [SerializeField]
    private GameObject gameControllerObject;
    private GameController gameController;
    [SerializeField]
    private GameObject[] sideMenus;
    [SerializeField]
    private GameObject[] dressButtonObjects;

    [SerializeField]
    private GameObject femaleModelObject;

    [SerializeField]
    private Image dress;
    [SerializeField]
    private Image wig;
    [SerializeField]
    private Image ornament;
    [SerializeField]
    private Image shoe;

    [SerializeField]
    GameObject femaleFace;

    [SerializeField]
    private GameObject dressOptionPanel;

    
    public GameObject[] editButtons;


    public GameObject wigEditPanel;

    public GameObject dressEditPanel;

    public GameObject shoeEditPanel;

    public bool dressDataLoadingComplete = false;
    public bool femaleWigDataLoadingComplete = false;
    public bool ornamentDataLoadingComplete = false;
    public bool shoeDataLoadingComplete = false;

    public string currentDressName = null;
    public string currentWigName = null;
    public string currentOrnamentName = null;
    public string currentShoeName = null;

    public bool isWearingDress=false;
    public bool isWearingWig = false;
    public bool isWearingOrnament = false;
    public bool isWearingShoe = false;




    public GameObject femaleDressContainer;
    public GameObject femaleWigContainer;
    public GameObject femaleOrnamentContainer;
    public GameObject femaleShoeContainer;

    public GameObject dressButtonPrefab;
    public GameObject wigButtonPrefab;
    public GameObject ornamentButtonPrefab;
    public GameObject shoeButtonPrefab;


    public GameObject dressLoadingPanel;
    public GameObject wigLoadingPanel;
    public GameObject ornamentLoadingPanel;
    public GameObject shoeLoadingPanel;

    public GameObject canvasObject;
    public GameObject infoPopupPrefab;

    public GameObject dressColorSlider;
    public GameObject dressBrightnessSlider;
    public GameObject wigColorSlider;
    public GameObject wigBrightnessSlider;
    public GameObject shoeColorSlider;
    public GameObject shoeBrightnessSlider;

    public GameObject selectDressOptionPanel;

    public UIImageColorPicker dressColorPicker;
    public UIImageColorPicker dressBrigtnessPicker;
    public UIImageColorPicker wigColorPicker;
    public UIImageColorPicker wigBrightnessPicker;
    public UIImageColorPicker shoeColorPicker;
    public UIImageColorPicker shoeBrightnessPicker;

    public GameObject selectWearingRoot;

    public bool paidUserStatus=false;


    public int maxCoroutineQueueLength = 10;

    public int dressCoroutineInQueue = 0;
    public int wigCoroutineInQueue = 0;
    public int ornamentCoroutineInQueue = 0;
    public int shoeCoroutineInQueue = 0;


    public GameObject[] wigEditSidePanels;
    public GameObject[] dressEditSidePanels;
    public GameObject[] shoeEditSidePanels;


    public float dressColor = 0.5f;
    public float dressBrightness = .5f;
    public float wigColor = 0.5f;
    public float wigBrightness = .5f;
    public float shoeColor = 0.5f;
    public float shoeBrightness = .5f;

    public Toggle[] wigEditUndoButtons;
    public Toggle[] dressEditUndoButtons;
    public Toggle[] shoeEditUndoButtons;


    public GameObject[] dressEditButtons;
    public GameObject[] wigEditButtons;
    public GameObject[] shoeEditButtons;

    public bool dontShowPopup = false;

    

    public bool IsWearingDress()
    {
        return isWearingDress;
    }
    public bool IsWearingWig()
    {
        return isWearingWig;
    }
    public bool IsWearingShoe()
    {
        return isWearingShoe;
    }

    public bool IsWearingOrnament()
    {
        return isWearingOrnament;
    }

    public void SetIsWearingDress(bool val)
    {
         isWearingDress=val;
    }
    public void SetIsWearingWig(bool val)
    {
        isWearingWig=val;
    }
    public void SetIsWearingShoe(bool val)
    {
        isWearingShoe=val;
    }

    public void SetIsWearingOrnament(bool val)
    {
        isWearingOrnament=val;
    }

    // Use this for initialization
    private void Awake()
    {
        gameController = gameControllerObject.GetComponent<GameController>();
        if (gameController != null)
        {
            paidUserStatus = gameController.IsPaidUser;
        }

        dressColorPicker = dressColorSlider.GetComponent<UIImageColorPicker>();
        dressBrigtnessPicker = dressBrightnessSlider.GetComponent<UIImageColorPicker>();
        wigColorPicker = wigColorSlider.GetComponent<UIImageColorPicker>();
        wigBrightnessPicker = wigBrightnessSlider.GetComponent<UIImageColorPicker>();
        shoeColorPicker = shoeColorSlider.GetComponent<UIImageColorPicker>();
        shoeBrightnessPicker = shoeBrightnessSlider.GetComponent<UIImageColorPicker>();
        //wigColorPicker = wigColorSlider.GetComponent<UIImageColorPicker>();


        editButtons[0].SetActive(false);
        editButtons[1].SetActive(false);

        //StartCoroutine(GetWearingsForSelectedModel(gameController.mainBodyShape,gameController.mainBodyTone,gameController.mainEyeColor));
        //StartCoroutine(GetWearingsForSelectedModel("hourglass", "dark", "green"));
    }
    void Start () {
        
        
        
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        if(isWearingDress)
        {
            DiscardEditDress();
        }
        if(isWearingWig)
        {
            DiscardEditWig();
        }
        if(isWearingShoe)
        {
            DiscardEditShoe();
        }
    }

    public IEnumerator ReloadDresses()
    {
        string sm = gameController.mainBodyShape;
        string sb = gameController.mainBodyTone;
        string se = gameController.mainEyeColor;
        yield return null;
    }

    private void OnEnable()
    {
        OnclickCloseEditDressButton();
        OnclickCloseEditWigButton();
        OnclickCloseEditShoeButton();
        CheckForChanges();
        
    }

	public void CheckForChanges()
    {
		if(gameObject.activeSelf && selectWearingRoot.activeSelf )
        {

            try
            {
                if (gameController == null)
                {
                    gameController = gameControllerObject.GetComponent<GameController>();
                }
				if (gameController.bodyChanged || (paidUserStatus != gameController.IsPaidUser))
                {
                    paidUserStatus = gameController.IsPaidUser;
                    print(string.Format("showing wearings for : bodyShape : {0}  bodyTone:{1}  eyeColor : {2}", gameController.mainBodyShape, gameController.mainBodyTone, gameController.mainEyeColor));

                    StartCoroutine(GetWearingsForSelectedModel(gameController.mainBodyShape, gameController.mainBodyTone, gameController.mainEyeColor));
                    //gameController.bodyChanged = false;
                }
            }
            catch(Exception e)
            {
                print(string.Format("check for change error : {0}", e.Message));
            }
        }
    }

    public void ToggleSideMenuSelectDress(int sideMenuIndex = 0, int showCode = 0,GameObject btnGameObject=null)
    {
        if (btnGameObject != null)
        {
            gameController.SetCurrentActive(sideMenus[sideMenuIndex], btnGameObject,dressOptionPanel);
        }
        if (showCode == 1)
        {

            for (int i = 0; i < sideMenus.Length; i++)
            {
                if (i == sideMenuIndex)
                {
                    sideMenus[i].GetComponent<RectTransform>().DOAnchorPosX(0f, .3f);
                }
                else
                {
                    sideMenus[i].GetComponent<RectTransform>().DOAnchorPosX(-400f, .3f);
                }
            }
        }
        else if (showCode == 2)
        {
            sideMenus[sideMenuIndex].GetComponent<RectTransform>().DOAnchorPosX(-400f, .3f);
        }
        else
        {
            for (int i = 0; i < sideMenus.Length; i++)
            {
                if (i == sideMenuIndex)
                {
                    if (sideMenus[i].GetComponent<RectTransform>().anchoredPosition.x < 0f)
                    {
                        sideMenus[i].GetComponent<RectTransform>().DOAnchorPosX(0f, .3f);
                    }
                    else
                    {
                        sideMenus[i].GetComponent<RectTransform>().DOAnchorPosX(-400f, .3f);
                    }
                }
                else
                {
                    sideMenus[i].GetComponent<RectTransform>().DOAnchorPosX(-400f, .3f);
                }
            }
        }
    }




    public void OnClickSelectLongDressButton(bool newState)   //select long dress handler
    {
        
        
        if (gameController==null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        gameController.ToggleOptionSideMenu(2);
        for (int i = 0; i < dressButtonObjects.Length; i++)
        {
            dressButtonObjects[i].transform.GetChild(0).gameObject.SetActive(false);
        }
        dressButtonObjects[0].transform.GetChild(0).gameObject.SetActive(newState);

        if(newState)
        {
            editButtons[1].SetActive(false);
            editButtons[2].SetActive(false);
            ToggleSideMenuSelectDress(0, 1, dressButtonObjects[0]);
            //if (dress.transform.parent.gameObject.activeSelf && dress.gameObject.activeSelf && dress.color.a > 0)
            //{
            //    editButtons[0].SetActive(true);
            //}
            print("new state is true");
            dressButtonObjects[0].GetComponent<Toggle>().isOn = true;
            if (isWearingDress)
            {
                print("Dress is weared");
                editButtons[0].SetActive(true);
            }
            else
            {
                print("no dress");
                editButtons[0].SetActive(false);
            }
        }
        else
        {
            ToggleSideMenuSelectDress(0,2);
            editButtons[0].SetActive(false);
        }
    }

    //public void OnClickSelectShortDressButton(bool newState)   //select short dress handler
    //{


    //    gameController.ToggleOptionSideMenu(2);
    //    for (int i = 0; i < dressButtonObjects.Length; i++)
    //    {
    //        dressButtonObjects[i].transform.GetChild(0).gameObject.SetActive(false);
    //    }
    //    dressButtonObjects[1].transform.GetChild(0).gameObject.SetActive(newState);

    //    if (newState)
    //    {
    //        ToggleSideMenuSelectDress(1, 1, dressButtonObjects[1]);
    //    }
    //    else
    //    {
    //        ToggleSideMenuSelectDress(1);
    //    }
    //}

    public void OnClickSelectWigButton(bool newState)    //select wig handler
    {

        
        gameController.ToggleOptionSideMenu(2);
        for (int i = 0; i < dressButtonObjects.Length; i++)
        {
            dressButtonObjects[i].transform.GetChild(0).gameObject.SetActive(false);
        }
        dressButtonObjects[1].transform.GetChild(0).gameObject.SetActive(newState);

        if (newState )
        {
            editButtons[0].SetActive(false);
            editButtons[2].SetActive(false);
            ToggleSideMenuSelectDress(1, 1, dressButtonObjects[1]);
            
            if (wig.transform.parent.gameObject.activeSelf && wig.gameObject.activeSelf && wig.color.a>0)
            {
                  editButtons[1].SetActive(true);
            }
            else
            {
                editButtons[1].SetActive(false);
            }
        }
        else
        {
            ToggleSideMenuSelectDress(1,2);
            editButtons[1].SetActive(false);
        }

        
            
        
    }


    public void OnClickSelectOrnamentButton(bool newState)    //select ornament handler
    {

        

        gameController.ToggleOptionSideMenu(2);
        for (int i = 0; i < dressButtonObjects.Length; i++)
        {
            dressButtonObjects[i].transform.GetChild(0).gameObject.SetActive(false);
        }
        dressButtonObjects[2].transform.GetChild(0).gameObject.SetActive(newState);

        if (newState)
        {
            editButtons[0].SetActive(false);
            editButtons[1].SetActive(false);
            editButtons[2].SetActive(false);
            ToggleSideMenuSelectDress(2, 1, dressButtonObjects[2]);
        }
        else
        {
            ToggleSideMenuSelectDress(2);
        }
    }

    public void OnClickSelectShoeButton(bool newState)    //select shoe handler
    {


        gameController.ToggleOptionSideMenu(2);
        for (int i = 0; i < dressButtonObjects.Length; i++)
        {
            dressButtonObjects[i].transform.GetChild(0).gameObject.SetActive(false);
        }
        dressButtonObjects[3].transform.GetChild(0).gameObject.SetActive(newState);

        if (newState)
        {
            ToggleSideMenuSelectDress(3, 1, dressButtonObjects[3]);
            editButtons[0].SetActive(false);
            editButtons[1].SetActive(false);
            if(isWearingShoe)
            {
                editButtons[2].SetActive(true);
            }
        }
        else
        {
            editButtons[2].SetActive(false);
            ToggleSideMenuSelectDress(3);
        }
    }

    public void OnClickDress()
    {
        PutOnLongDress("female", "model_001", "longdress_1");
    }
    public void OnClickDress3()
    {
        PutOnLongDress("female", "model_001", "longdress_4");
    }
    public void OnclickWig()
    {
        PutOnWig("female", "model_001", "wig_1");
    }
    public void OnclickOrnament()
    {
        PutOnOrnament("female", "model_001", "ornament_1");
    }
    public void OnclickShoe()
    {
        PutOnShoe("female", "model_001", "shoe_1");
    }

    public void PutOnLongDress(string gender, string modelName, string dressName)
    {
        if (gender.ToLower() == "female")
        {
            if (!dress.transform.parent.gameObject.activeSelf && dress.color.a > 0.5f && dressName == currentDressName)
            {
                dress.transform.parent.gameObject.SetActive(true);
                editButtons[1].SetActive(false);
                editButtons[0].SetActive(true);
                return;
            }
            else if (!dress.transform.parent.gameObject.activeSelf && dress.color.a <= 0.5f && dressName == currentDressName)
            {
                dress.transform.parent.gameObject.SetActive(true);
                dress.DOFade(1f, .5f);
                //print("else if");
                isWearingDress = true;
                editButtons[1].SetActive(false);
                editButtons[0].SetActive(true);
                return;
            }

            if (isWearingDress && dressName==currentDressName)
            {
                dress.gameObject.GetComponent<Image>().DOFade(0f, .5f);
                isWearingDress = false;
                editButtons[0].SetActive(false);
                return;
            }
            else if(!isWearingDress && dressName==currentDressName)
            {
                dress.gameObject.GetComponent<Image>().DOFade(1f, .5f);
                isWearingDress = true;
                editButtons[0].SetActive(true);
                return;
            }
            else if (isWearingDress && dressName !=currentDressName)
            {
                dress.gameObject.GetComponent<Image>().DOFade(0f, .5f);
                isWearingDress = false;
                editButtons[0].SetActive(true);
            }
            else
            {
                editButtons[0].SetActive(true);
            }
            if (!dress.gameObject.activeSelf)
            {
                dress.gameObject.SetActive(true);
            }
            if (!dress.transform.parent.gameObject.activeSelf)
            {
                dress.transform.parent.gameObject.SetActive(true);
            }
            print("loading dress");
            dress.gameObject.GetComponent<Image>().DOFade(0f, .8f);
            //yield return new WaitForSeconds(.1f);



            Texture2D tempTex = Resources.Load("images/longdresses/female/" +modelName+"/images/"+ dressName) as Texture2D;
            dress.sprite = Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f), 100f);
            isWearingDress = true;
            currentDressName = dressName;
            dress.gameObject.GetComponent<Image>().DOFade(0f, 0f);
            print("image assigned");



            dress.gameObject.GetComponent<Image>().DOFade(1f, 1f);
            print("last fade");


        }
    }

    public void InitControllers()
    {
        if(gameController==null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
    }

    public void RemoveDress()
    {
        InitControllers();
        print("Remove Dress");
        dress.gameObject.GetComponent<Image>().DOFade(0f, .5f);
        isWearingDress = false;
        currentDressName = "";
        gameController.currentDressProperty = null;
        
        editButtons[0].SetActive(false);
        return;
    }

    public void DeleteDress()
    {
        OnclickCloseEditDressButton();
        RemoveDress();
        
    }

    public void DeleteWig()
    {
        OnclickCloseEditWigButton();
        RemoveWig();

    }

    public void DeleteShoe()
    {
        OnclickCloseEditShoeButton();
        RemoveShoe();

    }


    public void PutOnLongDressDynamically(DressProperties dp,bool resetDress=false)  //for dynamically loading dress
    {
        gameController = gameControllerObject.GetComponent<GameController>();
        string dressName = dp.imgName;

        print("Dp Color = " + dp.dressColor[0] + " " + dp.dressColor[1]);
        if(!resetDress)
        {

            if (!dress.transform.parent.gameObject.activeSelf && dress.color.a > 0.5f && dressName == currentDressName)
            {
                print("!dress.transform.parent.gameObject.activeSelf && dress.color.a > 0.5f && dressName == currentDressName");
                dress.transform.parent.gameObject.SetActive(true);
                isWearingDress = true;
                editButtons[1].SetActive(false);
                editButtons[0].SetActive(true);
                return;
            }
            else if (!dress.transform.parent.gameObject.activeSelf && dress.color.a <= 0.5f && dressName == currentDressName)
            {
                print("!dress.transform.parent.gameObject.activeSelf && dress.color.a <= 0.5f && dressName == currentDressName");

                dress.transform.parent.gameObject.SetActive(true);
                dress.DOFade(1f, .5f);
                //print("else if");
                isWearingDress = true;
                editButtons[1].SetActive(false);
                editButtons[0].SetActive(true);
                return;
            }

            if (isWearingDress && dressName == currentDressName)
            {
                print("if (isWearingDress && dressName == currentDressName)");
                dress.gameObject.GetComponent<Image>().DOFade(0f, .5f);
                isWearingDress = false;
                editButtons[0].SetActive(false);
                return;
            }
            else if (!isWearingDress && dressName == currentDressName)
            {
                print("else if (!isWearingDress && dressName == currentDressName)");
                dress.gameObject.GetComponent<Image>().DOFade(1f, .5f);
                isWearingDress = true;
                editButtons[0].SetActive(true);
                return;
            }
            else if (isWearingDress && dressName != currentDressName)
            {
                print("else if (isWearingDress && dressName != currentDressName)");
                //dress.gameObject.GetComponent<Image>().DOFade(0f, 1f);
                //isWearingDress = false;
                //editButtons[0].SetActive(true);
            }
            else
            {
                //editButtons[0].SetActive(true);
            }
            if (!dress.gameObject.activeSelf)
            {
                dress.gameObject.SetActive(true);
            }
            if (!dress.transform.parent.gameObject.activeSelf)
            {
                dress.transform.parent.gameObject.SetActive(true);
            }
            print("loading dress");
            //dress.gameObject.GetComponent<Image>().DOFade(0f, .8f);
            //yield return new WaitForSeconds(.1f);

        }

        else
        {
            print("Reset Dress");
            //dress.gameObject.GetComponent<Image>().DOFade(0f, .5f);
            if (!dress.gameObject.activeSelf)
            {
                dress.gameObject.SetActive(true);
            }
            if (!dress.transform.parent.gameObject.activeSelf)
            {
                dress.transform.parent.gameObject.SetActive(true);
            }
        }

        Texture2D tempTex = new Texture2D(10,10);
        

        if (File.Exists(dp.finalSavePath))
        {
            
            tempTex.LoadImage(File.ReadAllBytes(dp.finalSavePath));
            tempTex.Apply();

            if(isWearingDress)
            {
                DestroyImmediate(gameController.tmpDress.sprite, true);
                gameController.tmpDress.sprite = dress.sprite;
                gameController.tmpDress.color = new Color(dress.color.r, dress.color.g, dress.color.b, 1f);
            }
            else
            {
                DestroyImmediate(gameController.tmpDress.sprite, true);
                gameController.tmpDress.sprite = dress.sprite;
                //gameController.tmpDress.color = new Color(dress.color.r, dress.color.g, dress.color.b, 1f);
            }
            gameController.tmpDress.gameObject.SetActive(true);
            dress.gameObject.SetActive(false);

// change the dress at background
            {
                dress.sprite = Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f), 100f);
                dress.color = new Color(dp.dressColor[0], dp.dressColor[1], dp.dressColor[2], 0f); //new Color(0.5f, .5f, .5f, 0f);
                dress.gameObject.SetActive(true);
                gameController.currentDressColor = new Color(dp.dressColor[0], dp.dressColor[1], dp.dressColor[2], 1); //new Color(0.5f, .5f, .5f, 0f);
                print("Dress Color now is : " + gameController.currentDressColor.ToString());
                isWearingDress = true;
                editButtons[0].SetActive(true);
                currentDressName = dressName;
            }

            float fadeDuration = .8f;
            if (!isWearingDress)
            {
                fadeDuration = 0f;
            }

            print("fade duration : " + fadeDuration);
            gameController.tmpDress.DOFade(0f, fadeDuration).SetEase(Ease.OutSine).onComplete+= delegate
            {
                gameController.tmpDress.gameObject.SetActive(false);
                DestroyImmediate(gameController.tmpDress.sprite,true);
                
            };
            dress.gameObject.GetComponent<Image>().DOFade(1f,.8f).SetEase(Ease.InSine).onComplete += delegate
            {
                print("last fade");

                gameController.currentDressTexture = new Texture2D(1, 1);
                gameController.currentDressTexture.LoadImage(File.ReadAllBytes(dp.finalSavePath));
                gameController.currentDressTexture.Apply();
                gameController.currentDressProperty = new DressProperties();
                gameController.currentDressProperty = dp;
                //gameController.currentDressColor = new Color(0.5f, .5f, .5f, 1f); //Color.white;
                print("Dress Color : " + dress.gameObject.GetComponent<Image>().color.ToString());
                gameController.HideLoadingPanelOnly();
                gameController.HideLoadingPanelOnlyTransparent();
            };

            //dress.gameObject.GetComponent<Image>().DOFade(0f, fadeDuration).onComplete += delegate 
            //{
            //    //dress.sprite = Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f), 100f);
            //    //dress.color = new Color(dp.dressColor[0], dp.dressColor[1], dp.dressColor[2], 0f); //new Color(0.5f, .5f, .5f, 0f);
            //    //gameController.currentDressColor = new Color(dp.dressColor[0], dp.dressColor[1], dp.dressColor[2], 1); //new Color(0.5f, .5f, .5f, 0f);
            //    //print("Dress Color now is : " + gameController.currentDressColor.ToString());
            //    //isWearingDress = true;
            //    //editButtons[0].SetActive(true);
            //    //currentDressName = dressName;

            //    //dress.gameObject.GetComponent<Image>().DOFade(0f, 0f);
            //    //print("image assigned");


            //    //dress.gameObject.GetComponent<Image>().DOFade(1f, .5f).onComplete += delegate
            //    //  {
            //    //      print("last fade");

            //    //      gameController.currentDressTexture = new Texture2D(1, 1);
            //    //      gameController.currentDressTexture.LoadImage(File.ReadAllBytes(dp.finalSavePath));
            //    //      gameController.currentDressTexture.Apply();
            //    //      gameController.currentDressProperty = new DressProperties();
            //    //      gameController.currentDressProperty = dp;
            //    //    //gameController.currentDressColor = new Color(0.5f, .5f, .5f, 1f); //Color.white;
            //    //    print("Dress Color : " + dress.gameObject.GetComponent<Image>().color.ToString());
            //    //      gameController.HideLoadingPanelOnly();
            //    //      gameController.HideLoadingPanelOnlyTransparent();
            //    //  };
                
            //};


            
             //Color.white;
            
            

            



            
        }
        else
        {
            gameController.HideLoadingPanelOnly();
            gameController.HideLoadingPanelOnlyTransparent();
            gameController.InstantiateInfoPopup("Could not Load Dress");
            DestroyImmediate(gameController.tmpDress.sprite);
            gameController.tmpDress.gameObject.SetActive(false);
        }



    }


    public void PutOnWig(string gender, string modelName, string wigName)
    {


        if (gender.ToLower() == "female")
        {
            if (!wig.transform.parent.gameObject.activeSelf && wig.color.a>0.5f && wigName == currentWigName)
            {
                wig.transform.parent.gameObject.SetActive(true);
                editButtons[0].SetActive(false);
                editButtons[1].SetActive(true);
                return;
            }
            else if (!wig.transform.parent.gameObject.activeSelf && wig.color.a <= 0.5f && wigName == currentWigName)
            {
                wig.transform.parent.gameObject.SetActive(true);
                wig.DOFade(1f, .5f);
                print("else if");
                isWearingWig = true;
                editButtons[0].SetActive(false);
                editButtons[1].SetActive(true);
                return;
            }
            
            if (isWearingWig && wigName == currentWigName)
            {
                wig.gameObject.GetComponent<Image>().DOFade(0f, .5f);
                isWearingWig = false;
                editButtons[1].SetActive(false);
                return;
            }
            else if (!isWearingWig && wigName == currentWigName)
            {
                wig.gameObject.GetComponent<Image>().DOFade(1f, .5f);
                isWearingWig = true;
                editButtons[1].SetActive(true);
                return;
            }
            else if(isWearingWig&&wigName!=currentWigName)
            {
                wig.gameObject.GetComponent<Image>().DOFade(0f, .5f);
                isWearingWig = false;
                editButtons[1].SetActive(true);
            }
            else
            {
                editButtons[1].SetActive(true);
            }

            if (!wig.gameObject.activeSelf)
            {
                wig.gameObject.SetActive(true);
            }
            if (!wig.transform.parent.gameObject.activeSelf)
            {
                wig.transform.parent.gameObject.SetActive(true);
            }
            print("loading wig");
            wig.gameObject.GetComponent<Image>().DOFade(0f, .8f);
            //yield return new WaitForSeconds(.1f);



            Texture2D tempTex = Resources.Load("images/wigs/female/" + modelName + "/images/" + wigName) as Texture2D;
            wig.sprite = Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f), 100f);
            isWearingWig = true;
            currentWigName = wigName;
            wig.gameObject.GetComponent<Image>().DOFade(0f, 0f);
            print("image assigned");



            wig.gameObject.GetComponent<Image>().DOFade(1f, 1f);
            print("last fade");


            


        }
    }



    public void RemoveWig()
    {
        wig.gameObject.GetComponent<Image>().DOFade(0f, .5f);
        isWearingWig = false;
        currentWigName = "";
        gameController.currentFemaleWigProperty = null;
        editButtons[1].SetActive(false);
        gameController.ZoomOutFemaleModel();
        return;
    }

    public void PutOnWigDynamically(FemaleWigProperties fwp,bool resetWig=false)
    {
        gameController = gameControllerObject.GetComponent<GameController>();

        string wigName = fwp.imgName;

        if(!resetWig)
        {
            if (!wig.transform.parent.gameObject.activeSelf && wig.color.a > 0.5f && wigName == currentWigName)
            {
                print(string.Format("if (!wig.transform.parent.gameObject.activeSelf && wig.color.a > 0.5f && {0} == {1})",wigName,currentWigName));
                wig.transform.parent.gameObject.SetActive(true);
                editButtons[0].SetActive(false);
                editButtons[2].SetActive(false);
                editButtons[1].SetActive(true);
                isWearingWig = true;
                return;
            }
            else if (!wig.transform.parent.gameObject.activeSelf && wig.color.a <= 0.5f && wigName == currentWigName)
            {
                print(string.Format("else if (!wig.transform.parent.gameObject.activeSelf && wig.color.a <= 0.5f && {0} == {1})",wigName,currentWigName));
                wig.transform.parent.gameObject.SetActive(true);
                wig.DOFade(1f, .5f);
                print("else if");
                editButtons[0].SetActive(false);
                editButtons[2].SetActive(false);
                editButtons[1].SetActive(true);
                isWearingWig = true;
                
                return;
            }

            if (isWearingWig && wigName == currentWigName)
            {
                print(string.Format("if (isWearingWig && {0} == {1})",wigName,currentWigName));
                wig.gameObject.GetComponent<Image>().DOFade(0f, .5f);
                isWearingWig = false;
                editButtons[1].SetActive(false);
                return;
            }
            else if (!isWearingWig && wigName == currentWigName)
            {
                print(string.Format("else if (!isWearingWig && {0} == {1})",wigName,currentWigName));
                wig.gameObject.GetComponent<Image>().DOFade(1f, .5f);
                isWearingWig = true;
                editButtons[1].SetActive(true);
                return;
            }
            else if (isWearingWig && wigName != currentWigName)
            {
                print(string.Format("else if (isWearingWig && {0} != {1})",wigName,currentWigName));
                //wig.gameObject.GetComponent<Image>().DOFade(0f, .5f);
                //isWearingWig = false;
                //editButtons[1].SetActive(true);
            }
            else
            {
                //editButtons[1].SetActive(true);
            }

            if (!wig.gameObject.activeSelf)
            {
                wig.gameObject.SetActive(true);
            }
            if (!wig.transform.parent.gameObject.activeSelf)
            {
                wig.transform.parent.gameObject.SetActive(true);
            }
            print("loading wig");
            //wig.gameObject.GetComponent<Image>().DOFade(0f, .8f);
            //yield return new WaitForSeconds(.1f);


        }
        else
        {
            print("resetting wig");
            //wig.gameObject.GetComponent<Image>().DOFade(0f, .5f);
            if (!wig.gameObject.activeSelf)
            {
                wig.gameObject.SetActive(true);
            }
            if (!wig.transform.parent.gameObject.activeSelf)
            {
                wig.transform.parent.gameObject.SetActive(true);
            }
        }



        //if (File.Exists(fwp.finalSavePath))
        //{
        //    Texture2D tempTex = new Texture2D(10,10);
        //    tempTex.LoadImage(File.ReadAllBytes(fwp.finalSavePath));
        //    tempTex.Apply();
        //    wig.sprite = Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f), 100f);
        //    wig.color = new Color(0.5f, .5f, .5f, 1f); //Color.white;
        //    gameController.currentWigColor = new Color(0.5f, .5f, .5f, 1f); //Color.white;
        //    isWearingWig = true;
        //    editButtons[1].SetActive(true);
        //    currentWigName = wigName;
        //    wig.gameObject.GetComponent<Image>().DOFade(0f, 0f);
        //    print("image assigned");



        //    wig.gameObject.GetComponent<Image>().DOFade(1f, 1f);
        //    print("last fade");

        //    gameController.currentWigTexture = new Texture2D(1, 1);
        //    gameController.currentWigTexture.LoadImage(File.ReadAllBytes(fwp.finalSavePath));
        //    gameController.currentWigTexture.Apply();
        //    gameController.currentFemaleWigProperty = new FemaleWigProperties();
        //    gameController.currentFemaleWigProperty = fwp;
        //    gameController.currentWigColor = new Color(0.5f, .5f, .5f, 1f); //Color.white;
        //}

        if (File.Exists(fwp.finalSavePath))
        {

            if (isWearingWig)
            {
                DestroyImmediate(gameController.tmpWig.sprite, true);
                gameController.tmpWig.sprite = wig.sprite;
                gameController.tmpWig.color = new Color(wig.color.r, wig.color.g, wig.color.b, 1f);
            }
            else
            {
                DestroyImmediate(gameController.tmpWig.sprite, true);
                gameController.tmpWig.sprite = wig.sprite;
                //gameController.tmpDress.color = new Color(dress.color.r, dress.color.g, dress.color.b, 1f);
            }
            gameController.tmpWig.gameObject.SetActive(true);
            wig.gameObject.SetActive(false);


            float fadeDuration = 1f;
            if (!isWearingWig)
            {
                fadeDuration = 0f;
            }
            Texture2D tempTex = new Texture2D(10, 10);
            tempTex.LoadImage(File.ReadAllBytes(fwp.finalSavePath));
            tempTex.Apply();

            {
                wig.sprite = Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f), 100f);
                wig.color = new Color(fwp.wigColor[0], fwp.wigColor[1], fwp.wigColor[2], 0f); //new Color(0.5f, .5f, .5f, 0f); //Color.white;
                wig.gameObject.SetActive(true);
                gameController.currentWigColor = new Color(fwp.wigColor[0], fwp.wigColor[1], fwp.wigColor[2], 1f); //Color.white;
                
                isWearingWig = true;
                editButtons[1].SetActive(true);
                currentWigName = wigName;
            }


            gameController.tmpWig.DOFade(0f, fadeDuration).SetEase(Ease.OutSine).onComplete += delegate
            {
                gameController.tmpWig.gameObject.SetActive(false);
                DestroyImmediate(gameController.tmpWig.sprite, true);
                
            };

            wig.DOFade(1f, .8f).SetEase(Ease.InSine).onComplete += delegate
            {
                print("last fade");

                gameController.currentWigTexture = new Texture2D(1, 1);
                gameController.currentWigTexture.LoadImage(File.ReadAllBytes(fwp.finalSavePath));
                gameController.currentWigTexture.Apply();
                gameController.currentFemaleWigProperty = new FemaleWigProperties();
                gameController.currentFemaleWigProperty = fwp;
                //gameController.currentWigColor = new Color(0.5f, .5f, .5f, 1f); //Color.white;
            };

            //wig.gameObject.GetComponent<Image>().DOFade(0f, fadeDuration).onComplete += delegate
            //      {
            //          //wig.sprite = Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f), 100f);
            //          //wig.color = new Color(fwp.wigColor[0], fwp.wigColor[1], fwp.wigColor[2],0f); //new Color(0.5f, .5f, .5f, 0f); //Color.white;
            //          //gameController.currentWigColor = new Color(fwp.wigColor[0], fwp.wigColor[1], fwp.wigColor[2], 1f); //Color.white;
            //          //isWearingWig = true;
            //          //editButtons[1].SetActive(true);
            //          //currentWigName = wigName;
            //          //wig.gameObject.GetComponent<Image>().DOFade(0f, 0f);
            //          //print("image assigned");



            //          //wig.gameObject.GetComponent<Image>().DOFade(1f, .5f).onComplete+=delegate
            //          //{
            //          //    print("last fade");

            //          //    gameController.currentWigTexture = new Texture2D(1, 1);
            //          //    gameController.currentWigTexture.LoadImage(File.ReadAllBytes(fwp.finalSavePath));
            //          //    gameController.currentWigTexture.Apply();
            //          //    gameController.currentFemaleWigProperty = new FemaleWigProperties();
            //          //    gameController.currentFemaleWigProperty = fwp;
            //          //    //gameController.currentWigColor = new Color(0.5f, .5f, .5f, 1f); //Color.white;
            //          //};

            //      };
        }
        else
        {
            gameController.HideLoadingPanelOnly();
            gameController.HideLoadingPanelOnlyTransparent();
            gameController.InstantiateInfoPopup("Could not Load Wig");
            DestroyImmediate(gameController.tmpWig.sprite);
            gameController.tmpWig.gameObject.SetActive(false);
        }




    }




    
    public void PutOnOrnament(string gender, string modelName, string ornamentName)
    {
        if (gender.ToLower() == "female")
        {
            if (!ornament.transform.parent.gameObject.activeSelf && ornament.color.a > 0.5f && ornamentName == currentOrnamentName)
            {
                ornament.transform.parent.gameObject.SetActive(true);
                //editButtons[0].SetActive(false);
                //editButtons[1].SetActive(true);
                return;
            }
            else if (!ornament.transform.parent.gameObject.activeSelf && ornament.color.a <= 0.5f && ornamentName == currentWigName)
            {
                ornament.transform.parent.gameObject.SetActive(true);
                ornament.DOFade(1f, .5f);
                //print("else if");
                isWearingOrnament = true;
                //editButtons[0].SetActive(false);
                //editButtons[1].SetActive(true);
                return;
            }
            if (isWearingOrnament && ornamentName == currentOrnamentName)
            {
                ornament.gameObject.GetComponent<Image>().DOFade(0f, .5f);
                isWearingOrnament = false;
                return;
            }
            else if (!isWearingOrnament && ornamentName == currentOrnamentName)
            {
                ornament.gameObject.GetComponent<Image>().DOFade(1f, .5f);
                isWearingOrnament = true;
                return;
            }
            else if(isWearingOrnament && ornamentName!=currentOrnamentName)
            {
                ornament.gameObject.GetComponent<Image>().DOFade(0f, .5f);
                isWearingOrnament = false;
            }
            if (!ornament.gameObject.activeSelf)
            {
                ornament.gameObject.SetActive(true);
            }
            if (!ornament.transform.parent.gameObject.activeSelf)
            {
                ornament.transform.parent.gameObject.SetActive(true);
            }
            print("loading ornament");
            ornament.gameObject.GetComponent<Image>().DOFade(0f, .8f);
            //yield return new WaitForSeconds(.1f);



            Texture2D tempTex = Resources.Load("images/ornaments/female/" + modelName + "/images/" + ornamentName) as Texture2D;
            ornament.sprite = Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f), 100f);
            isWearingOrnament = true;
            currentOrnamentName = ornamentName;
            ornament.gameObject.GetComponent<Image>().DOFade(0f, 0f);
            print("image assigned");



            ornament.gameObject.GetComponent<Image>().DOFade(1f, 1f);
            print("last fade");



            


        }
    }

    public void RemoveOrnament()
    {
        ornament.gameObject.GetComponent<Image>().DOFade(0f, .5f);
        isWearingOrnament = false;
        currentOrnamentName = "";
        gameController.currentOrnamentProperty = null;

        return;
    }

    public void PutOnOrnamentDynamically(OrnamentProperties op)
    {
        gameController = gameControllerObject.GetComponent<GameController>();
        string ornamentName = op.imgName;
        
            if (!ornament.transform.parent.gameObject.activeSelf && ornament.color.a > 0.5f && ornamentName == currentOrnamentName)
            {
                ornament.transform.parent.gameObject.SetActive(true);
                //editButtons[0].SetActive(false);
                //editButtons[1].SetActive(true);
                return;
            }
            else if (!ornament.transform.parent.gameObject.activeSelf && ornament.color.a <= 0.5f && ornamentName == currentWigName)
            {
                ornament.transform.parent.gameObject.SetActive(true);
                ornament.DOFade(1f, .5f);
                //print("else if");
                isWearingOrnament = true;
                //editButtons[0].SetActive(false);
                //editButtons[1].SetActive(true);
                return;
            }
            if (isWearingOrnament && ornamentName == currentOrnamentName)
            {
                ornament.gameObject.GetComponent<Image>().DOFade(0f, .5f);
                isWearingOrnament = false;
                return;
            }
            else if (!isWearingOrnament && ornamentName == currentOrnamentName)
            {
                ornament.gameObject.GetComponent<Image>().DOFade(1f, .5f);
                isWearingOrnament = true;
                return;
            }
            else if (isWearingOrnament && ornamentName != currentOrnamentName)
            {
                //ornament.gameObject.GetComponent<Image>().DOFade(0f, .5f);
                //isWearingOrnament = false;
            }
            if (!ornament.gameObject.activeSelf)
            {
                ornament.gameObject.SetActive(true);
            }
            if (!ornament.transform.parent.gameObject.activeSelf)
            {
                ornament.transform.parent.gameObject.SetActive(true);
            }
            print("loading ornament");
            //ornament.gameObject.GetComponent<Image>().DOFade(0f, .8f);
            //yield return new WaitForSeconds(.1f);



        if(File.Exists(op.finalSavePath))
        {
            Texture2D tempTex = new Texture2D(10, 10);
            tempTex.LoadImage(File.ReadAllBytes(op.finalSavePath));
            tempTex.Apply();



            if (isWearingOrnament)
            {
                DestroyImmediate(gameController.tmpOrnament.sprite, true);
                gameController.tmpOrnament.sprite = ornament.sprite;
                gameController.tmpOrnament.color = new Color(ornament.color.r, ornament.color.g, ornament.color.b, 1f);
            }
            else
            {
                DestroyImmediate(gameController.tmpOrnament.sprite, true);
                gameController.tmpOrnament.sprite = ornament.sprite;
                //gameController.tmpDress.color = new Color(dress.color.r, dress.color.g, dress.color.b, 1f);
            }
            gameController.tmpOrnament.gameObject.SetActive(true);
            ornament.gameObject.SetActive(false);

            float fadeDuration = .5f;
            if (!isWearingOrnament)
            {
                fadeDuration = 0f;
            }


            {
                ornament.sprite = Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f), 100f);
                ornament.gameObject.SetActive(true);
                isWearingOrnament = true;
                currentOrnamentName = ornamentName;
                //ornament.gameObject.GetComponent<Image>().DOFade(0f, 0f);
                print("image assigned");
            }

            gameController.tmpOrnament.DOFade(0f, fadeDuration).SetEase(Ease.OutSine).onComplete += delegate
            {
                gameController.tmpOrnament.gameObject.SetActive(false);
                DestroyImmediate(gameController.tmpOrnament.sprite, true);

            };


            ornament.gameObject.GetComponent<Image>().DOFade(1f, .8f).SetDelay(.2f).SetEase(Ease.InSine).onComplete += delegate
            {
                print("last fade");



                gameController.currentOrnamentProperty = new OrnamentProperties();
                gameController.currentOrnamentProperty = op;
            };

            //ornament.gameObject.GetComponent<Image>().DOFade(0f, fadeDuration).onComplete += delegate
            // {
            //    // ornament.sprite = Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f), 100f);
            //    // isWearingOrnament = true;
            //    // currentOrnamentName = ornamentName;
            //    ////ornament.gameObject.GetComponent<Image>().DOFade(0f, 0f);
            //    //print("image assigned");



            //     ornament.gameObject.GetComponent<Image>().DOFade(1f, .5f).onComplete += delegate
            //       {
            //           print("last fade");



            //           gameController.currentOrnamentProperty = new OrnamentProperties();
            //           gameController.currentOrnamentProperty = op;
            //       };

            // };
        }
        else
        {
            gameController.HideLoadingPanelOnly();
            gameController.HideLoadingPanelOnlyTransparent();
            gameController.InstantiateInfoPopup("Could not Load Ornament");
            DestroyImmediate(gameController.tmpOrnament.sprite);
            gameController.tmpOrnament.gameObject.SetActive(false);
        }


        
    }


    public void PutOnShoe(string gender, string modelName, string shoeName)
    {
        if (gender.ToLower() == "female")
        {
            if (!shoe.transform.parent.gameObject.activeSelf && shoe.color.a > 0.5f && shoeName == currentShoeName)
            {
                shoe.transform.parent.gameObject.SetActive(true);
                //editButtons[0].SetActive(false);
                //editButtons[1].SetActive(true);
                return;
            }
            else if (!shoe.transform.parent.gameObject.activeSelf && shoe.color.a <= 0.5f && shoeName == currentShoeName)
            {
                shoe.transform.parent.gameObject.SetActive(true);
                shoe.DOFade(1f, .5f);
                //print("else if");
                isWearingShoe = true;
                //editButtons[0].SetActive(false);
                //editButtons[1].SetActive(true);
                return;
            }
            if (isWearingShoe && shoeName == currentShoeName)
            {
                shoe.gameObject.GetComponent<Image>().DOFade(0f, .5f);
                isWearingShoe = false;
                return;
            }
            else if (!isWearingShoe && shoeName == currentShoeName)
            {
                shoe.gameObject.GetComponent<Image>().DOFade(1f, .5f);
                isWearingShoe = true;
                return;
            }
            else if(isWearingShoe&&shoeName!=currentShoeName)
            {
                shoe.gameObject.GetComponent<Image>().DOFade(0f, .5f);
                isWearingShoe = false;
            }
            if (!shoe.gameObject.activeSelf)
            {
                shoe.gameObject.SetActive(true);
            }
            if (!shoe.transform.parent.gameObject.activeSelf)
            {
                shoe.transform.parent.gameObject.SetActive(true);
            }
            print("loading shoe");
            shoe.gameObject.GetComponent<Image>().DOFade(0f, .8f);
            //yield return new WaitForSeconds(.1f);



            Texture2D tempTex = Resources.Load("images/shoes/female/" + modelName + "/images/" + shoeName) as Texture2D;
            shoe.sprite = Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f), 100f);
            isWearingShoe = true;
            currentShoeName = shoeName;
            shoe.gameObject.GetComponent<Image>().DOFade(0f, 0f);
            print("image assigned");



            shoe.gameObject.GetComponent<Image>().DOFade(1f, 1f);
            print("last fade");


        }
    }

    public void RemoveShoe()
    {
        shoe.gameObject.GetComponent<Image>().DOFade(0f, .5f);
        isWearingShoe = false;
        currentShoeName = "";
        gameController.currentShoeProperty = null;
        editButtons[2].SetActive(false);
        return;
    }

    public void PutOnShoeDynamically(ShoeProperties sp,bool resetShoe=false)
    {
        gameController = gameControllerObject.GetComponent<GameController>();
        string shoeName = sp.imgName;
        if(!resetShoe)
        {
            if (!shoe.transform.parent.gameObject.activeSelf && shoe.color.a > 0.5f && shoeName == currentShoeName)
            {
                shoe.transform.parent.gameObject.SetActive(true);
                editButtons[0].SetActive(false);
                editButtons[1].SetActive(false);
                editButtons[2].SetActive(true);
                isWearingShoe = true;
                return;
            }
            else if (!shoe.transform.parent.gameObject.activeSelf && shoe.color.a <= 0.5f && shoeName == currentShoeName)
            {
                shoe.transform.parent.gameObject.SetActive(true);
                shoe.DOFade(1f, .5f);
                //print("else if");
                shoe.transform.parent.gameObject.SetActive(true);
                editButtons[0].SetActive(false);
                editButtons[1].SetActive(false);
                editButtons[2].SetActive(true);
                isWearingShoe = true;
                
                return;
            }
            if (isWearingShoe && shoeName == currentShoeName)
            {
                editButtons[0].SetActive(false);
                editButtons[1].SetActive(false);
                editButtons[2].SetActive(false);
                shoe.gameObject.GetComponent<Image>().DOFade(0f, .5f);
                isWearingShoe = false;
                return;
            }
            else if (!isWearingShoe && shoeName == currentShoeName)
            {
                editButtons[0].SetActive(false);
                editButtons[1].SetActive(false);
                editButtons[2].SetActive(true);
                shoe.gameObject.GetComponent<Image>().DOFade(1f, .5f);
                isWearingShoe = true;
                return;
            }
            else if (isWearingShoe && shoeName != currentShoeName)
            {
                //shoe.gameObject.GetComponent<Image>().DOFade(0f, .5f);
                //isWearingShoe = false;
            }
            if (!shoe.gameObject.activeSelf)
            {
                shoe.gameObject.SetActive(true);
            }
            if (!shoe.transform.parent.gameObject.activeSelf)
            {
                shoe.transform.parent.gameObject.SetActive(true);
            }
            print("loading shoe");
        }
        else
        {
            print("resetting shoe");
            //wig.gameObject.GetComponent<Image>().DOFade(0f, .5f);
            if (!shoe.gameObject.activeSelf)
            {
                shoe.gameObject.SetActive(true);
            }
            if (!shoe.transform.parent.gameObject.activeSelf)
            {
                shoe.transform.parent.gameObject.SetActive(true);
            }
        }
            
            //shoe.gameObject.GetComponent<Image>().DOFade(0f, .8f);
            //yield return new WaitForSeconds(.1f);



       //if(File.Exists(sp.finalSavePath))
       // {
       //     Texture2D tempTex = new Texture2D(10, 10);
       //     tempTex.LoadImage(File.ReadAllBytes(sp.finalSavePath));
       //     tempTex.Apply();
       //     if (isWearingShoe)
       //     {
       //         DestroyImmediate(gameController.tmpShoe.sprite, true);
       //         gameController.tmpShoe.sprite = shoe.sprite;
       //         gameController.tmpShoe.color = new Color(shoe.color.r, shoe.color.g, shoe.color.b, 1f);
       //     }
       //     else
       //     {
       //         DestroyImmediate(gameController.tmpShoe.sprite, true);
       //         gameController.tmpShoe.sprite = shoe.sprite;
       //         //gameController.tmpDress.color = new Color(dress.color.r, dress.color.g, dress.color.b, 1f);
       //     }
       //     gameController.tmpShoe.gameObject.SetActive(true);
       //     shoe.gameObject.SetActive(false);

       //     float fadeDuration = .5f;
       //     if (!isWearingShoe)
       //     {
       //         fadeDuration = 0f;
       //     }


       //     {
       //         shoe.sprite = Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f), 100f);
       //         shoe.gameObject.SetActive(true);
       //         isWearingShoe = true;
       //         currentShoeName = shoeName;
       //         //ornament.gameObject.GetComponent<Image>().DOFade(0f, 0f);
       //         print("image assigned");
       //     }

       //     gameController.tmpShoe.DOFade(0f, fadeDuration).SetEase(Ease.OutSine).onComplete += delegate
       //     {
       //         gameController.tmpShoe.gameObject.SetActive(false);
       //         DestroyImmediate(gameController.tmpShoe.sprite, true);

       //     };


       //     shoe.gameObject.GetComponent<Image>().DOFade(1f, .8f).SetDelay(.2f).SetEase(Ease.InSine).onComplete += delegate
       //     {
       //         print("last fade");



       //         gameController.currentShoeProperty = new ShoeProperties();
       //         gameController.currentShoeProperty = sp;
       //     };

       // }

       //else
       // {
       //     gameController.HideLoadingPanelOnly();
       //     gameController.HideLoadingPanelOnlyTransparent();
       //     gameController.InstantiateInfoPopup("Could not Load Shoe");
       //     DestroyImmediate(gameController.tmpShoe.sprite);
       //     gameController.tmpShoe.gameObject.SetActive(false);
       // }









        if (File.Exists(sp.finalSavePath))
        {

            if (isWearingShoe)
            {
                Destroy(gameController.tmpShoe.sprite);
                gameController.tmpShoe.sprite = shoe.sprite;
                gameController.tmpShoe.color = new Color(shoe.color.r, shoe.color.g, shoe.color.b, 1f);
            }
            else
            {
                Destroy(gameController.tmpShoe.sprite);
                gameController.tmpShoe.sprite = shoe.sprite;
                //gameController.tmpDress.color = new Color(dress.color.r, dress.color.g, dress.color.b, 1f);
            }
            gameController.tmpShoe.gameObject.SetActive(true);
            shoe.gameObject.SetActive(false);


            float fadeDuration = 1f;
            if (!isWearingShoe)
            {
                fadeDuration = 0f;
            }
            Texture2D tempTex = new Texture2D(10, 10);
            tempTex.LoadImage(File.ReadAllBytes(sp.finalSavePath));
            tempTex.Apply();

            {
                shoe.sprite = Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f), 100f);
                shoe.color = new Color(sp.shoeColor[0], sp.shoeColor[1], sp.shoeColor[2], 0f); //new Color(0.5f, .5f, .5f, 0f); //Color.white;
                shoe.gameObject.SetActive(true);
                gameController.currentShoeColor = new Color(sp.shoeColor[0], sp.shoeColor[1], sp.shoeColor[2], 1f); //Color.white;

                isWearingShoe = true;
                editButtons[2].SetActive(true);
                currentShoeName = shoeName;
            }


            gameController.tmpShoe.DOFade(0f, fadeDuration).SetEase(Ease.OutSine).onComplete += delegate
            {
                gameController.tmpShoe.gameObject.SetActive(false);
                DestroyImmediate(gameController.tmpShoe.sprite);

            };

            shoe.DOFade(1f, .8f).SetEase(Ease.InSine).onComplete += delegate
            {
                print("last fade");

                gameController.currentShoeTexture = new Texture2D(1, 1);
                gameController.currentShoeTexture.LoadImage(File.ReadAllBytes(sp.finalSavePath));
                gameController.currentShoeTexture.Apply();
                gameController.currentShoeProperty = new ShoeProperties();
                gameController.currentShoeProperty = sp;
                //gameController.currentWigColor = new Color(0.5f, .5f, .5f, 1f); //Color.white;
            };

            //wig.gameObject.GetComponent<Image>().DOFade(0f, fadeDuration).onComplete += delegate
            //      {
            //          //wig.sprite = Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f), 100f);
            //          //wig.color = new Color(fwp.wigColor[0], fwp.wigColor[1], fwp.wigColor[2],0f); //new Color(0.5f, .5f, .5f, 0f); //Color.white;
            //          //gameController.currentWigColor = new Color(fwp.wigColor[0], fwp.wigColor[1], fwp.wigColor[2], 1f); //Color.white;
            //          //isWearingWig = true;
            //          //editButtons[1].SetActive(true);
            //          //currentWigName = wigName;
            //          //wig.gameObject.GetComponent<Image>().DOFade(0f, 0f);
            //          //print("image assigned");



            //          //wig.gameObject.GetComponent<Image>().DOFade(1f, .5f).onComplete+=delegate
            //          //{
            //          //    print("last fade");

            //          //    gameController.currentWigTexture = new Texture2D(1, 1);
            //          //    gameController.currentWigTexture.LoadImage(File.ReadAllBytes(fwp.finalSavePath));
            //          //    gameController.currentWigTexture.Apply();
            //          //    gameController.currentFemaleWigProperty = new FemaleWigProperties();
            //          //    gameController.currentFemaleWigProperty = fwp;
            //          //    //gameController.currentWigColor = new Color(0.5f, .5f, .5f, 1f); //Color.white;
            //          //};

            //      };
        }
        else
        {
            gameController.HideLoadingPanelOnly();
            gameController.HideLoadingPanelOnlyTransparent();
            gameController.InstantiateInfoPopup("Could not Load shoe");
            DestroyImmediate(gameController.tmpShoe.sprite);
            gameController.tmpShoe.gameObject.SetActive(false);
        }
    }


    public SaveData GetCurrentWearings()
    {
        SaveData currentWearings = new SaveData();
        
        return currentWearings;
    }

    public void ActiveWigEditSliders()
    {
        wigColorSlider.SetActive(true);
        wigColorPicker.ActivateColorSlider();
        wigBrightnessSlider.SetActive(true);
        wigBrightnessPicker.ActivateColorSlider();
    }

    public void DeactiveWigEditSliders()
    {

        ToggleEditSidePanels(wigEditSidePanels[0], 2);
        ToggleEditSidePanels(wigEditSidePanels[1], 2);

        wigColorPicker.DeactivateColorSlider();
        wigColorSlider.SetActive(false);
        wigBrightnessPicker.DeactivateColorSlider();
        wigBrightnessSlider.SetActive(false);
    }




    public void OnClickEditWigButton(bool newState)
    {
        if(newState)
        {
            ActiveWigEditSliders();
            gameController.sceneEditorController.enabled = false;
            editButtons[1].GetComponent<Toggle>().isOn = false;
            //ToggleSideMenuSelectDress(5, 1);
            StartCoroutine(ToggleEditPanel(wigEditPanel));
            wigEditUndoButtons[1].gameObject.SetActive(false);
            wigEditUndoButtons[0].gameObject.SetActive(true);
            ToggleSideMenuSelectDress(5, 1);
            gameController.ZoomInFemaleModel();
        }

        

    }


    public void ActiveDressEditSliders()
    {
        dressColorSlider.SetActive(true);
        dressColorPicker.ActivateColorSlider();
        dressBrightnessSlider.SetActive(true);
        dressBrigtnessPicker.ActivateColorSlider();
    }

    public void DeactiveDressEditSliders()
    {
        ToggleEditSidePanels(dressEditSidePanels[0], 2);
        ToggleEditSidePanels(dressEditSidePanels[1], 2);

        dressColorPicker.DeactivateColorSlider();
        dressColorSlider.SetActive(false);
        dressBrigtnessPicker.DeactivateColorSlider();
        dressBrightnessSlider.SetActive(false);
    }

    public void OnClickEditDressButton(bool newState)
    {
        if (newState)
        {
            ActiveDressEditSliders();
            gameController.sceneEditorController.enabled = false;
            editButtons[0].GetComponent<Toggle>().isOn = false;
            //ToggleSideMenuSelectDress(4, 1);
            StartCoroutine(ToggleEditPanel(dressEditPanel));
            
            dressEditUndoButtons[1].gameObject.SetActive(false);
            dressEditUndoButtons[0].gameObject.SetActive(true);
            ToggleSideMenuSelectDress(4, 1);


        }



    }







    public void ActiveShoeEditSliders()
    {
        shoeColorSlider.SetActive(true);
        shoeColorPicker.ActivateColorSlider();
        shoeBrightnessSlider.SetActive(true);
        shoeBrightnessPicker.ActivateColorSlider();
    }

    public void DeactiveShoeEditSliders()
    {

        ToggleEditSidePanels(shoeEditSidePanels[0], 2);
        ToggleEditSidePanels(shoeEditSidePanels[1], 2);

        shoeColorPicker.DeactivateColorSlider();
        shoeColorSlider.SetActive(false);
        shoeBrightnessPicker.DeactivateColorSlider();
        shoeBrightnessSlider.SetActive(false);
    }




    public void OnClickEditShoeButton(bool newState)
    {
        if (newState)
        {
            ActiveShoeEditSliders();
            gameController.sceneEditorController.enabled = false;
            editButtons[2].GetComponent<Toggle>().isOn = false;
            //ToggleSideMenuSelectDress(5, 1);
            StartCoroutine(ToggleEditPanel(shoeEditPanel));
            shoeEditUndoButtons[1].gameObject.SetActive(false);
            shoeEditUndoButtons[0].gameObject.SetActive(true);
            ToggleSideMenuSelectDress(8, 1);
            //gameController.ZoomInFemaleModel();
        }



    }


    private IEnumerator ToggleEditPanel(GameObject editPanel=null,bool show=true)
    {
        if(show)
        {
            gameController.DeactiveCurrentActive(true);
            yield return new WaitForSeconds(0.1f);
            editPanel.GetComponent<RectTransform>().DOAnchorPosY(0f, 0.3f);
            selectDressOptionPanel.GetComponent<RectTransform>().DOAnchorPosY(-800f, 0.3f);
            print("Show dress edit panel if");
        }
        else
        {
            
            editPanel.GetComponent<RectTransform>().DOAnchorPosY(-800f, 0.3f);
            selectDressOptionPanel.GetComponent<RectTransform>().DOAnchorPosY(0f, 0.3f);
            yield return new WaitForSeconds(0.1f);
            gameController.ActiveCurrentActive(true);
            print("Show dress edit panel else");
        }
    }

#region DRESSWIGSHOECOLOR

    public void OnclickAcceptDressColorChangeButton(GameObject editPanel)
    {
        StartCoroutine(ToggleEditPanel(editPanel, false));
        ToggleSideMenuSelectDress(4, 2);
        gameController.currentDressColor= dress.color;
        Color[] dcs = dress.sprite.texture.GetPixels();
        gameController.currentDressTexture.SetPixels(dcs);
        gameController.currentDressTexture.Apply();

        DeactiveDressEditSliders();
        gameController.sceneEditorController.enabled = true;
    }

    public void OnclickCloseEditDressButton(GameObject editPanel)
    {
        StartCoroutine(ToggleEditPanel(editPanel, false));
        ToggleSideMenuSelectDress(4, 2);
        ToggleSideMenuSelectDress(6, 2);

        DiscardEditDress();
        DeactiveDressEditSliders();


        dressEditButtons[0].GetComponent<Toggle>().isOn = false;
        dressEditButtons[0].transform.GetChild(0).gameObject.SetActive(false);
        dressEditButtons[1].GetComponent<Toggle>().isOn = false;
        dressEditButtons[1].transform.GetChild(0).gameObject.SetActive(false);


        editButtons[1].SetActive(false);

        if (isWearingDress)
        {
            editButtons[0].SetActive(true);
        }
        else
        {
            editButtons[0].SetActive(false);
        }
        gameController.sceneEditorController.enabled = true;
        //ToggleSideMenuSelectDress(5, 2);
    }
    public void OnclickCloseEditDressButton()
    {
        StartCoroutine(ToggleEditPanel(dressEditPanel, false));
        ToggleSideMenuSelectDress(4, 2);
        ToggleSideMenuSelectDress(6, 2);

        //DiscardEditDress();
        //DeactiveDressEditSliders();


        dressEditButtons[0].GetComponent<Toggle>().isOn = false;
        dressEditButtons[0].transform.GetChild(0).gameObject.SetActive(false);
        dressEditButtons[1].GetComponent<Toggle>().isOn = false;
        dressEditButtons[1].transform.GetChild(0).gameObject.SetActive(false);


        editButtons[1].SetActive(false);
        editButtons[2].SetActive(false);

        if (isWearingDress)
        {
            editButtons[0].SetActive(true);
        }
        else
        {
            editButtons[0].SetActive(false);
        }



        gameController.sceneEditorController.enabled = true;
        //ToggleSideMenuSelectDress(5, 2);
    }

    public void DiscardEditDress()
    {
        DiscardEditDressColor();
        DiscardEditDressBrightness();
    }

    public void DiscardEditDressColor()
    {
        print("In discard edit dress color");
        Color c = gameController.currentDressColor;
        
        c.b = dress.color.b;
        print("current dress color : " + c.ToString());
        dress.color = c;
        //dress.sprite = Sprite.Create(gameController.currentDressTexture, new Rect(0, 0, gameController.currentDressTexture.width, gameController.currentDressTexture.height), new Vector2(0.5f, 0.5f), 100f);
        dressColorPicker.DeactivateColorSlider();
        dressColorPicker.mainSlider.value = 0.5f;
        dressColorPicker.ActivateColorSlider();
        //dressBrigtnessPicker.DeactivateColorSlider();
        //dressBrigtnessPicker.mainSlider.value = 0.5f;
        //dressBrigtnessPicker.ActivateColorSlider();
    }
    public void DiscardEditDressBrightness()
    {
        Color c = gameController.currentDressColor;
        c.r = dress.color.r;
        print("current dress brightness : " + c.ToString());
        dress.color = c;
        //dress.sprite = Sprite.Create(gameController.currentDressTexture, new Rect(0, 0, gameController.currentDressTexture.width, gameController.currentDressTexture.height), new Vector2(0.5f, 0.5f), 100f);
        //dressColorPicker.DeactivateColorSlider();
        //dressColorPicker.mainSlider.value = 0.5f;
        //dressColorPicker.ActivateColorSlider();
        dressBrigtnessPicker.DeactivateColorSlider();
        dressBrigtnessPicker.mainSlider.value = 0.5f;
        dressBrigtnessPicker.ActivateColorSlider();
    }
    public void ResetDefaultDress()
    {
        PutOnLongDressDynamically(gameController.currentDressProperty,true);
        //PutOnLongDressDynamically(gameController.currentDressProperty,true);
        dressColorPicker.DeactivateColorSlider();
        dressColorPicker.mainSlider.value = 0.5f;
        dressColorPicker.ActivateColorSlider();
        dressBrigtnessPicker.DeactivateColorSlider();
        dressBrigtnessPicker.mainSlider.value = 0.5f;
        dressBrigtnessPicker.ActivateColorSlider();

    }

    

   

    public void OnclickAcceptWigColorChangeButton(GameObject editPanel)
    {
        StartCoroutine(ToggleEditPanel(editPanel, false));
        ToggleSideMenuSelectDress(5, 2);
        gameController.currentWigColor = wig.color;
        Color[] wcs = wig.sprite.texture.GetPixels();
        gameController.currentWigTexture.SetPixels(wcs);
        gameController.currentWigTexture.Apply();

        DeactiveWigEditSliders();
        gameController.sceneEditorController.enabled = true;
        gameController.ZoomOutFemaleModel();
    }

    public void OnclickCloseEditWigButton(GameObject editPanel)
    {
        StartCoroutine(ToggleEditPanel(editPanel, false));
        ToggleSideMenuSelectDress(5, 2);
        ToggleSideMenuSelectDress(7, 2);
        gameController.ZoomOutFemaleModel();
        DiscardEditWig();
        DeactiveWigEditSliders();

        wigEditButtons[0].GetComponent<Toggle>().isOn = false;
        wigEditButtons[0].transform.GetChild(0).gameObject.SetActive(false);
        wigEditButtons[1].GetComponent<Toggle>().isOn = false;
        wigEditButtons[1].transform.GetChild(0).gameObject.SetActive(false);


        editButtons[0].SetActive(false);
        editButtons[2].SetActive(false);

        if (isWearingWig)
        {
            editButtons[1].SetActive(true);
        }
        else
        {
            editButtons[1].SetActive(false);
        }


        gameController.sceneEditorController.enabled = true;
    }

    public void OnclickCloseEditWigButton()
    {
        StartCoroutine(ToggleEditPanel(wigEditPanel, false));
        ToggleSideMenuSelectDress(5, 2);
        ToggleSideMenuSelectDress(7, 2);
        //DiscardEditWig();
        DeactiveWigEditSliders();


        wigEditButtons[0].GetComponent<Toggle>().isOn = false;
        wigEditButtons[0].transform.GetChild(0).gameObject.SetActive(false);
        wigEditButtons[1].GetComponent<Toggle>().isOn = false;
        wigEditButtons[1].transform.GetChild(0).gameObject.SetActive(false);


        gameController.sceneEditorController.enabled = true;
    }

    public void DiscardEditWig()
    {
        //gameController.ZoomOutFemaleModel();
        DiscardEditWigColor();
        DiscardEditWigBrightness();
    }

    public void DiscardEditWigColor()
    {
        Color c= gameController.currentWigColor;
        c.b = wig.color.b;
        wig.color = c;
        //wig.sprite = Sprite.Create(gameController.currentWigTexture, new Rect(0, 0, gameController.currentWigTexture.width, gameController.currentWigTexture.height), new Vector2(0.5f, 0.5f), 100f);
        wigColorPicker.DeactivateColorSlider();
        wigColorPicker.mainSlider.value = 0.5f;
        wigColorPicker.ActivateColorSlider();
        //wigBrightnessPicker.DeactivateColorSlider();
        //wigBrightnessPicker.mainSlider.value = 0.5f;
        //wigBrightnessPicker.ActivateColorSlider();
    }
    public void DiscardEditWigBrightness()
    {
        Color c = gameController.currentWigColor;
        c.r = wig.color.r;
        wig.color = c;
        //wig.sprite = Sprite.Create(gameController.currentWigTexture, new Rect(0, 0, gameController.currentWigTexture.width, gameController.currentWigTexture.height), new Vector2(0.5f, 0.5f), 100f);
        //wigColorPicker.DeactivateColorSlider();
        //wigColorPicker.mainSlider.value = 0.5f;
        //wigColorPicker.ActivateColorSlider();
        wigBrightnessPicker.DeactivateColorSlider();
        wigBrightnessPicker.mainSlider.value = 0.5f;
        wigBrightnessPicker.ActivateColorSlider();
    }

    public void ResetDefaultWig()
    {
        PutOnWigDynamically(gameController.currentFemaleWigProperty,true);
        wigColorPicker.DeactivateColorSlider();
        wigColorPicker.mainSlider.value = 0.5f;
        wigColorPicker.ActivateColorSlider();
        wigBrightnessPicker.DeactivateColorSlider();
        wigBrightnessPicker.mainSlider.value = 0.5f;
        wigBrightnessPicker.ActivateColorSlider();
        //gameController.ZoomOutFemaleModel();
    }

    

    public void ResetDefaultWigBrightness()
    {
        PutOnWigDynamically(gameController.currentFemaleWigProperty, true);
        wigBrightnessPicker.DeactivateColorSlider();
        wigBrightnessPicker.mainSlider.value = 0.5f;
        wigBrightnessPicker.ActivateColorSlider();
        wigBrightnessPicker.DeactivateColorSlider();
        wigBrightnessPicker.mainSlider.value = 0.5f;
        wigBrightnessPicker.ActivateColorSlider();
        //gameController.ZoomOutFemaleModel();
    }







/// <summary>
///  Shoe edit
/// </summary>


    public void OnclickAcceptShoeColorChangeButton(GameObject editPanel)
    {
        StartCoroutine(ToggleEditPanel(editPanel, false));
        ToggleSideMenuSelectDress(8, 2);
        ToggleSideMenuSelectDress(9, 2);
        gameController.currentShoeColor = shoe.color;
        Color[] scs = shoe.sprite.texture.GetPixels();
        gameController.currentShoeTexture.SetPixels(scs);
        gameController.currentShoeTexture.Apply();

        DeactiveShoeEditSliders();
        gameController.sceneEditorController.enabled = true;
        if(isWearingShoe)
        {
            editButtons[2].SetActive(true);
        }
        else
        {
            editButtons[2].SetActive(false);
        }
        //gameController.ZoomOutFemaleModel();
    }

    public void OnclickCloseEditShoeButton(GameObject editPanel)
    {
        StartCoroutine(ToggleEditPanel(editPanel, false));
        ToggleSideMenuSelectDress(8, 2);
        ToggleSideMenuSelectDress(9, 2);
        //gameController.ZoomOutFemaleModel();
        DiscardEditShoe();
        DeactiveShoeEditSliders();

        shoeEditButtons[0].GetComponent<Toggle>().isOn = false;
        shoeEditButtons[0].transform.GetChild(0).gameObject.SetActive(false);
        shoeEditButtons[1].GetComponent<Toggle>().isOn = false;
        shoeEditButtons[1].transform.GetChild(0).gameObject.SetActive(false);


        editButtons[0].SetActive(false);
        editButtons[1].SetActive(false);

        if (isWearingShoe)
        {
            editButtons[2].SetActive(true);
        }
        else
        {
            editButtons[2].SetActive(false);
        }


        gameController.sceneEditorController.enabled = true;
    }

    public void OnclickCloseEditShoeButton()
    {
        StartCoroutine(ToggleEditPanel(shoeEditPanel, false));
        ToggleSideMenuSelectDress(8, 2);
        ToggleSideMenuSelectDress(9, 2);
        //DiscardEditWig();
        DeactiveShoeEditSliders();


        shoeEditButtons[0].GetComponent<Toggle>().isOn = false;
        shoeEditButtons[0].transform.GetChild(0).gameObject.SetActive(false);
        shoeEditButtons[1].GetComponent<Toggle>().isOn = false;
        shoeEditButtons[1].transform.GetChild(0).gameObject.SetActive(false);


        gameController.sceneEditorController.enabled = true;
    }

    public void DiscardEditShoe()
    {
        //gameController.ZoomOutFemaleModel();
        DiscardEditShoeColor();
        DiscardEditShoeBrightness();
    }

    public void DiscardEditShoeColor()
    {
        Color c = gameController.currentShoeColor;
        c.b = shoe.color.b;
        shoe.color = c;
        //wig.sprite = Sprite.Create(gameController.currentWigTexture, new Rect(0, 0, gameController.currentWigTexture.width, gameController.currentWigTexture.height), new Vector2(0.5f, 0.5f), 100f);
        shoeColorPicker.DeactivateColorSlider();
        shoeColorPicker.mainSlider.value = c.r;
        shoeColorPicker.ActivateColorSlider();
        //wigBrightnessPicker.DeactivateColorSlider();
        //wigBrightnessPicker.mainSlider.value = 0.5f;
        //wigBrightnessPicker.ActivateColorSlider();
    }
    public void DiscardEditShoeBrightness()
    {
        Color c = gameController.currentShoeColor;
        c.r = shoe.color.r;
        shoe.color = c;
        //wig.sprite = Sprite.Create(gameController.currentWigTexture, new Rect(0, 0, gameController.currentWigTexture.width, gameController.currentWigTexture.height), new Vector2(0.5f, 0.5f), 100f);
        //wigColorPicker.DeactivateColorSlider();
        //wigColorPicker.mainSlider.value = 0.5f;
        //wigColorPicker.ActivateColorSlider();
        shoeBrightnessPicker.DeactivateColorSlider();
        shoeBrightnessPicker.mainSlider.value = c.b;
        shoeBrightnessPicker.ActivateColorSlider();
    }

    public void ResetDefaultShoe()
    {
        PutOnShoeDynamically(gameController.currentShoeProperty, true);
        shoeColorPicker.DeactivateColorSlider();
        shoeColorPicker.mainSlider.value = 0.5f;
        shoeColorPicker.ActivateColorSlider();
        shoeBrightnessPicker.DeactivateColorSlider();
        shoeBrightnessPicker.mainSlider.value = 0.5f;
        shoeBrightnessPicker.ActivateColorSlider();
        //gameController.ZoomOutFemaleModel();
    }



    public void ResetDefaultShoeBrightness()
    {
        PutOnShoeDynamically(gameController.currentShoeProperty, true);
        shoeBrightnessPicker.DeactivateColorSlider();
        shoeBrightnessPicker.mainSlider.value = 0.5f;
        shoeBrightnessPicker.ActivateColorSlider();
        shoeBrightnessPicker.DeactivateColorSlider();
        shoeBrightnessPicker.mainSlider.value = 0.5f;
        shoeBrightnessPicker.ActivateColorSlider();
        //gameController.ZoomOutFemaleModel();
    }





    #endregion DRESSWIGSHOECOLOR


    public void InstantiateInfoPopup(String message,CloseStyle closeStyle=CloseStyle.None)
    {
        GameObject g = Instantiate<GameObject>(infoPopupPrefab, canvasObject.transform);
        if(closeStyle!=CloseStyle.None)
        {
            PopUpClosingStyle pcs = g.GetComponent<PopUpClosingStyle>();
            if(pcs!=null)
            {
                pcs.popupCloseStyle = closeStyle;
            }
        }
        Text t = g.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        t.text = message;

        Button b= g.transform.GetChild(0).GetChild(1).GetComponent<Button>();
        b.onClick.AddListener(() => { Destroy(g); });
    }




    #region DYNAMICWEARINGS


#region RESETDRESSES
    public IEnumerator ResetDresses(MiniJsonArray fa,MiniJsonArray partialDressArray=null)
    {
        //print("started.. main");
        if(fa.Count>1)
        {
            for (int i = 0; i < fa.Count; i++)
            {
                dressCoroutineInQueue += 1;

                
                GameObject d = Instantiate(dressButtonPrefab, Vector3.zero, Quaternion.identity, femaleDressContainer.transform);
                //print("loaded main: " + i);
                if (i == 0)
                {
                    d.transform.GetChild(3).gameObject.SetActive(true);
                    d.transform.GetChild(6).gameObject.SetActive(true);
                    d.transform.GetChild(7).gameObject.SetActive(true);
                }
                else if (i == (fa.Count - 1))
                {
                    d.transform.GetChild(2).gameObject.SetActive(true);
                    d.transform.GetChild(5).gameObject.SetActive(true);
                    d.transform.GetChild(8).gameObject.SetActive(true);
                }
                else
                {
                    d.transform.GetChild(3).gameObject.SetActive(true);
                    d.transform.GetChild(6).gameObject.SetActive(true);
                    //d.transform.GetChild(7).gameObject.SetActive(true);
                }


                d.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;

                if(dressLoadingPanel.activeSelf)
                {
                    dressLoadingPanel.SetActive(false);
                    dressLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();
                }


                //yield return new WaitForSeconds(.01f);
                if(dressCoroutineInQueue>maxCoroutineQueueLength)
                {
                    dressCoroutineInQueue = 0;
                    yield return new WaitForSeconds(.1f);
                }
                StartCoroutine(LoadIconOnDressButton(d, fa.Get(i)));
                if(!gameController.IsPaidUser)
                {
                    //d.GetComponent<Button>().interactable = false;    // may we need to change this..not sure
                    d.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
            dressLoadingPanel.SetActive(false);
            dressLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();
        }
        else if(fa.Count==1)
        {
            for (int i = 0; i < fa.Count; i++)
            {
                dressCoroutineInQueue += 1;

                

                GameObject d = Instantiate(dressButtonPrefab, Vector3.zero, Quaternion.identity, femaleDressContainer.transform);

                if (i == 0)
                {
                    d.transform.GetChild(1).gameObject.SetActive(true);
                    d.transform.GetChild(4).gameObject.SetActive(true);
                    d.transform.GetChild(7).gameObject.SetActive(true);
                    d.transform.GetChild(8).gameObject.SetActive(true);
                }
                


                d.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;


                //yield return new WaitForSeconds(.01f);
                if (dressCoroutineInQueue > maxCoroutineQueueLength)
                {
                    dressCoroutineInQueue = 0;
                    yield return new WaitForSeconds(.1f);
                }
                StartCoroutine(LoadIconOnDressButton(d, fa.Get(i)));
                if (!gameController.IsPaidUser)
                {
                    //d.GetComponent<Button>().interactable = false;    // may we need to change this..not sure
                    d.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
            dressLoadingPanel.SetActive(false);
            dressLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();
        }
        else
        {
            dressLoadingPanel.SetActive(false);
            dressLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();
            
        }

        dressCoroutineInQueue = 0;
        
        if (partialDressArray !=null)
        {
            print("partial data count : " + partialDressArray.Count);
            if (partialDressArray.Count>0)
            {
                print("starting resetting partial dress data");
                StartCoroutine(ResetPartiallyMatchingDresses(partialDressArray));
            }
        }

        

        yield return null;
    }


  


    public IEnumerator ResetPartiallyMatchingDresses(MiniJsonArray fa)
    {
        //print("started partial");
        if (fa.Count > 1)
        {
            for (int i = 0; i < fa.Count; i++)
            {
                dressCoroutineInQueue += 1;

                
                GameObject d = Instantiate(dressButtonPrefab, Vector3.zero, Quaternion.identity, femaleDressContainer.transform);
                

                d.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;


                //yield return new WaitForSeconds(.01f);
                if (dressCoroutineInQueue > maxCoroutineQueueLength)
                {
                    dressCoroutineInQueue = 0;
                    yield return new WaitForSeconds(.1f);
                }
                print("Loading icon on partial dress button");
                StartCoroutine(LoadIconOnDressButton(d, fa.Get(i)));
                if (!gameController.IsPaidUser)
                {
                    //d.GetComponent<Button>().interactable = false;   // may we need to change this..not sure
                    d.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
            
        }
        else if (fa.Count == 1)
        {
            
            for (int i = 0; i < fa.Count; i++)
            {

                dressCoroutineInQueue += 1;

                

                GameObject d = Instantiate(dressButtonPrefab, Vector3.zero, Quaternion.identity, femaleDressContainer.transform);

               



                d.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;



                //yield return new WaitForSeconds(.01f);
                if (dressCoroutineInQueue > maxCoroutineQueueLength)
                {
                    dressCoroutineInQueue = 0;
                    yield return new WaitForSeconds(.1f);
                }
                StartCoroutine(LoadIconOnDressButton(d, fa.Get(i)));
                if (!gameController.IsPaidUser)
                {
                    //d.GetComponent<Button>().interactable = false;   // may we need to change this..not sure
                    d.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
            
        }
        else
        {
            dressLoadingPanel.SetActive(false);
            dressLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();
            //InstantiateInfoPopup("No Dresses Available");
        }

        dressCoroutineInQueue = 0;
        yield return null;
    }
    

    


    public IEnumerator LoadIconOnDressButton(GameObject d, MiniJsonObject m)
    {
        //print("Started work : " + Time.deltaTime);
        DressProperties dp = d.GetComponent<DressProperties>();
        dp.InitializeDressProperty(m);
        //print("Completed work : "+Time.deltaTime);

        //dressCoroutineInQueue -= 1;
        //if (dressCoroutineInQueue < 0)
        //{
        //    dressCoroutineInQueue = 0;
        //}
        yield return null;
    }
    #endregion RESETDRESSES

#region RESETWIGS
    public IEnumerator ResetWigs(MiniJsonArray fa,MiniJsonArray partialWigArray=null)
    {
        if(fa.Count>1)
        {
            for (int i = 0; i < fa.Count; i++)
            {
                wigCoroutineInQueue += 1;

                //yield return new WaitUntil(() =>
                //{
                //    return wigCoroutineInQueue < maxCoroutineQueueLength;
                //});

                GameObject w = Instantiate(wigButtonPrefab, Vector3.zero, Quaternion.identity, femaleWigContainer.transform);
                {
                    if(i==0)
                    {
                        w.transform.GetChild(3).gameObject.SetActive(true);
                        w.transform.GetChild(6).gameObject.SetActive(true);
                        w.transform.GetChild(7).gameObject.SetActive(true);
                    }
                    else if(i==(fa.Count-1))
                    {
                        w.transform.GetChild(2).gameObject.SetActive(true);
                        w.transform.GetChild(5).gameObject.SetActive(true);
                        w.transform.GetChild(8).gameObject.SetActive(true);
                    }
                    else
                    {
                        w.transform.GetChild(3).gameObject.SetActive(true);
                        w.transform.GetChild(6).gameObject.SetActive(true);
                        //w.transform.GetChild(7).gameObject.SetActive(true);
                    }
                }
                w.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;


                if (wigCoroutineInQueue > maxCoroutineQueueLength)
                {
                    wigCoroutineInQueue = 0;
                    yield return new WaitForSeconds(.1f);
                }


                StartCoroutine(LoadIconOnWigButton(w, fa.Get(i)));
                
            }
            wigLoadingPanel.SetActive(false);
            wigLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();
        }
        else if(fa.Count==1)
        {
            for (int i = 0; i < fa.Count; i++)
            {
                wigCoroutineInQueue += 1;

                //yield return new WaitUntil(() =>
                //{
                //    return wigCoroutineInQueue < maxCoroutineQueueLength;
                //});

                GameObject w = Instantiate(wigButtonPrefab, Vector3.zero, Quaternion.identity, femaleWigContainer.transform);
                {
                    if (i == 0)
                    {
                        w.transform.GetChild(1).gameObject.SetActive(true);
                        w.transform.GetChild(4).gameObject.SetActive(true);
                        w.transform.GetChild(7).gameObject.SetActive(true);
                        w.transform.GetChild(8).gameObject.SetActive(true);
                    }
                   
                }
                w.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;

                if (wigCoroutineInQueue > maxCoroutineQueueLength)
                {
                    wigCoroutineInQueue = 0;
                    yield return new WaitForSeconds(.1f);
                }

                StartCoroutine(LoadIconOnWigButton(w, fa.Get(i)));

            }
            wigLoadingPanel.SetActive(false);
            wigLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();
        }
        else
        {
            wigLoadingPanel.SetActive(false);
            wigLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();
            
        }

        wigCoroutineInQueue = 0;


        if (partialWigArray!=null)
        {
            if(partialWigArray.Count>0)
            {
                StartCoroutine(ResetPartiallyMatchingWigs(partialWigArray));
            }
        }
        yield return null;
    }



    public IEnumerator ResetPartiallyMatchingWigs(MiniJsonArray fa)
    {
        if (fa.Count > 1)
        {

            for (int i = 0; i < fa.Count; i++)
            {

                wigCoroutineInQueue += 1;

                //yield return new WaitUntil(() =>
                //{
                //    return wigCoroutineInQueue < maxCoroutineQueueLength;
                //});

                GameObject w = Instantiate(wigButtonPrefab, Vector3.zero, Quaternion.identity, femaleWigContainer.transform);
                //{
                //    if (i == 0)
                //    {
                //        w.transform.GetChild(3).gameObject.SetActive(true);
                //        w.transform.GetChild(6).gameObject.SetActive(true);
                //        w.transform.GetChild(7).gameObject.SetActive(true);
                //    }
                //    else if (i == (fa.Count - 1))
                //    {
                //        w.transform.GetChild(2).gameObject.SetActive(true);
                //        w.transform.GetChild(5).gameObject.SetActive(true);
                //        w.transform.GetChild(8).gameObject.SetActive(true);
                //    }
                //    else
                //    {
                //        w.transform.GetChild(3).gameObject.SetActive(true);
                //        w.transform.GetChild(6).gameObject.SetActive(true);
                //        //w.transform.GetChild(7).gameObject.SetActive(true);
                //    }
                //}
                w.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;

                if (wigCoroutineInQueue > maxCoroutineQueueLength)
                {
                    wigCoroutineInQueue = 0;
                    yield return new WaitForSeconds(.1f);
                }

                StartCoroutine(LoadIconOnWigButton(w, fa.Get(i)));

            }
            wigLoadingPanel.SetActive(false);
            wigLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();
        }
        else if (fa.Count == 1)
        {
            for (int i = 0; i < fa.Count; i++)
            {
                wigCoroutineInQueue += 1;

                //yield return new WaitUntil(() =>
                //{
                //    return wigCoroutineInQueue < maxCoroutineQueueLength;
                //});

                GameObject w = Instantiate(wigButtonPrefab, Vector3.zero, Quaternion.identity, femaleWigContainer.transform);
                //{
                //    if (i == 0)
                //    {
                //        w.transform.GetChild(1).gameObject.SetActive(true);
                //        w.transform.GetChild(4).gameObject.SetActive(true);
                //        w.transform.GetChild(7).gameObject.SetActive(true);
                //        w.transform.GetChild(8).gameObject.SetActive(true);
                //    }

                //}
                w.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;

                if (wigCoroutineInQueue > maxCoroutineQueueLength)
                {
                    wigCoroutineInQueue = 0;
                    yield return new WaitForSeconds(.1f);
                }

                StartCoroutine(LoadIconOnWigButton(w, fa.Get(i)));

            }
            wigLoadingPanel.SetActive(false);
            wigLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();
        }
        else
        {
            wigLoadingPanel.SetActive(false);
            wigLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();
            //InstantiateInfoPopup("No Wigs Available");
        }


        wigCoroutineInQueue = 0;

        yield return null;
    }

    public IEnumerator LoadIconOnWigButton(GameObject w, MiniJsonObject m)
    {
        //yield return new WaitUntil(() => {
        //    return wigCoroutineInQueue < maxCoroutineQueueLength;
        //});

        FemaleWigProperties fwp = w.GetComponent<FemaleWigProperties>();
        fwp.InitializeWigProperty(m);

        //wigCoroutineInQueue -= 1;
        //if (wigCoroutineInQueue < 0)
        //{
        //    wigCoroutineInQueue = 0;
        //}

        yield return null;
    }
    #endregion RESETWIGS


#region RESETORNAMENTS
    public IEnumerator ResetOrnaments(MiniJsonArray fa ,MiniJsonArray partialOrnamentArray=null)
    {
        if(fa.Count>1)
        {
            for (int i = 0; i < fa.Count; i++)
            {
                ornamentCoroutineInQueue += 1;

                //yield return new WaitUntil(() =>
                //{
                //    return ornamentCoroutineInQueue < maxCoroutineQueueLength;
                //});

                GameObject o = Instantiate(ornamentButtonPrefab, Vector3.zero, Quaternion.identity, femaleOrnamentContainer.transform);


                if (i == 0)
                {
                    o.transform.GetChild(3).gameObject.SetActive(true);
                    o.transform.GetChild(6).gameObject.SetActive(true);
                    o.transform.GetChild(7).gameObject.SetActive(true);
                }
                else if (i == (fa.Count - 1))
                {
                    o.transform.GetChild(2).gameObject.SetActive(true);
                    o.transform.GetChild(5).gameObject.SetActive(true);
                    o.transform.GetChild(8).gameObject.SetActive(true);
                }
                else
                {
                    o.transform.GetChild(3).gameObject.SetActive(true);
                    o.transform.GetChild(6).gameObject.SetActive(true);
                    //o.transform.GetChild(7).gameObject.SetActive(true);
                }



                o.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;

                if (ornamentCoroutineInQueue > maxCoroutineQueueLength)
                {
                    ornamentCoroutineInQueue = 0;
                    yield return new WaitForSeconds(.1f);
                }

                StartCoroutine(LoadIconOnOrnamentButton(o, fa.Get(i)));
                
            }
            ornamentLoadingPanel.SetActive(false);
            ornamentLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();
        }
        else if(fa.Count==1)
        {
            for (int i = 0; i < fa.Count; i++)
            {
                ornamentCoroutineInQueue += 1;

                //yield return new WaitUntil(() =>
                //{
                //    return ornamentCoroutineInQueue < maxCoroutineQueueLength;
                //});


                GameObject o = Instantiate(ornamentButtonPrefab, Vector3.zero, Quaternion.identity, femaleOrnamentContainer.transform);


                if (i == 0)
                {
                    o.transform.GetChild(1).gameObject.SetActive(true);
                    o.transform.GetChild(4).gameObject.SetActive(true);
                    o.transform.GetChild(7).gameObject.SetActive(true);
                    o.transform.GetChild(8).gameObject.SetActive(true);
                }
                



                o.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;

                if (ornamentCoroutineInQueue > maxCoroutineQueueLength)
                {
                    ornamentCoroutineInQueue = 0;
                    yield return new WaitForSeconds(.1f);
                }

                StartCoroutine(LoadIconOnOrnamentButton(o, fa.Get(i)));

            }
            ornamentLoadingPanel.SetActive(false);
            ornamentLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();
        }
        else
        {
            ornamentLoadingPanel.SetActive(false);
            ornamentLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();
            
        }

        ornamentCoroutineInQueue = 0;

        if(partialOrnamentArray!=null)
        {
            if(partialOrnamentArray.Count>0)
            {
                StartCoroutine(ResetPartiallyMatchingOrnaments(partialOrnamentArray));
            }
        }
        yield return null;
    }


    public IEnumerator ResetPartiallyMatchingOrnaments(MiniJsonArray fa)
    {
        if (fa.Count > 1)
        {
            for (int i = 0; i < fa.Count; i++)
            {
                ornamentCoroutineInQueue += 1;

                //yield return new WaitUntil(() =>
                //{
                //    return ornamentCoroutineInQueue < maxCoroutineQueueLength;
                //});


                GameObject o = Instantiate(ornamentButtonPrefab, Vector3.zero, Quaternion.identity, femaleOrnamentContainer.transform);


                //if (i == 0)
                //{
                //    o.transform.GetChild(3).gameObject.SetActive(true);
                //    o.transform.GetChild(6).gameObject.SetActive(true);
                //    o.transform.GetChild(7).gameObject.SetActive(true);
                //}
                //else if (i == (fa.Count - 1))
                //{
                //    o.transform.GetChild(2).gameObject.SetActive(true);
                //    o.transform.GetChild(5).gameObject.SetActive(true);
                //    o.transform.GetChild(8).gameObject.SetActive(true);
                //}
                //else
                //{
                //    o.transform.GetChild(3).gameObject.SetActive(true);
                //    o.transform.GetChild(6).gameObject.SetActive(true);
                //    //o.transform.GetChild(7).gameObject.SetActive(true);
                //}



                o.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;

                if (ornamentCoroutineInQueue > maxCoroutineQueueLength)
                {
                    ornamentCoroutineInQueue = 0;
                    yield return new WaitForSeconds(.1f);
                }


                StartCoroutine(LoadIconOnOrnamentButton(o, fa.Get(i)));

            }
            ornamentLoadingPanel.SetActive(false);
            ornamentLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();
        }
        else if (fa.Count == 1)
        {
            for (int i = 0; i < fa.Count; i++)
            {
                ornamentCoroutineInQueue += 1;

                //yield return new WaitUntil(() =>
                //{
                //    return ornamentCoroutineInQueue < maxCoroutineQueueLength;
                //});


                GameObject o = Instantiate(ornamentButtonPrefab, Vector3.zero, Quaternion.identity, femaleOrnamentContainer.transform);


                //if (i == 0)
                //{
                //    o.transform.GetChild(1).gameObject.SetActive(true);
                //    o.transform.GetChild(4).gameObject.SetActive(true);
                //    o.transform.GetChild(7).gameObject.SetActive(true);
                //    o.transform.GetChild(8).gameObject.SetActive(true);
                //}




                o.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;

                if (ornamentCoroutineInQueue > maxCoroutineQueueLength)
                {
                    ornamentCoroutineInQueue = 0;
                    yield return new WaitForSeconds(.1f);
                }

                StartCoroutine(LoadIconOnOrnamentButton(o, fa.Get(i)));

            }
            ornamentLoadingPanel.SetActive(false);
            ornamentLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();
        }
        else
        {
            ornamentLoadingPanel.SetActive(false);
            ornamentLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();
            //InstantiateInfoPopup("No Ornaments Available");
        }

        ornamentCoroutineInQueue = 0;

        yield return null;
    }

    public IEnumerator LoadIconOnOrnamentButton(GameObject o, MiniJsonObject m)
    {
        OrnamentProperties op = o.GetComponent<OrnamentProperties>();
        op.InitializeOrnamentProperty(m);

        //ornamentCoroutineInQueue -= 1;
        //if(ornamentCoroutineInQueue<0)
        //{
        //    ornamentCoroutineInQueue = 0;
        //}

        yield return null;
    }
    #endregion RESETORNAMENTS

#region RESETSHOES
    public IEnumerator ResetShoes(MiniJsonArray fa,MiniJsonArray partialShoeArray=null)
    {
        if(fa.Count>1)
        {
            for (int i = 0; i < fa.Count; i++)
            {
                shoeCoroutineInQueue += 1;

                //yield return new WaitUntil(() =>
                //{
                //    return shoeCoroutineInQueue < maxCoroutineQueueLength;
                //});

                GameObject s = Instantiate(shoeButtonPrefab, Vector3.zero, Quaternion.identity, femaleShoeContainer.transform);

                if (i == 0)
                {
                    s.transform.GetChild(3).gameObject.SetActive(true);
                    s.transform.GetChild(6).gameObject.SetActive(true);
                    s.transform.GetChild(7).gameObject.SetActive(true);
                }
                else if (i == (fa.Count - 1))
                {
                    s.transform.GetChild(2).gameObject.SetActive(true);
                    s.transform.GetChild(5).gameObject.SetActive(true);
                    s.transform.GetChild(8).gameObject.SetActive(true);
                }
                else
                {
                    s.transform.GetChild(3).gameObject.SetActive(true);
                    s.transform.GetChild(6).gameObject.SetActive(true);
                    //s.transform.GetChild(7).gameObject.SetActive(true);
                }


                s.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;

                if (shoeCoroutineInQueue > maxCoroutineQueueLength)
                {
                    shoeCoroutineInQueue = 0;
                    yield return new WaitForSeconds(.1f);
                }

                StartCoroutine(LoadIconOnShoeButton(s, fa.Get(i)));
                
            }
            shoeLoadingPanel.SetActive(false);
            shoeLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();
        }
        else if(fa.Count==1)
        {
            for (int i = 0; i < fa.Count; i++)
            {
                shoeCoroutineInQueue += 1;

                //yield return new WaitUntil(() =>
                //{
                //    return shoeCoroutineInQueue < maxCoroutineQueueLength;
                //});

                GameObject s = Instantiate(shoeButtonPrefab, Vector3.zero, Quaternion.identity, femaleShoeContainer.transform);

                if (i == 0)
                {
                    s.transform.GetChild(1).gameObject.SetActive(true);
                    s.transform.GetChild(4).gameObject.SetActive(true);
                    s.transform.GetChild(7).gameObject.SetActive(true);
                    s.transform.GetChild(8).gameObject.SetActive(true);
                }
                


                s.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;

                if (shoeCoroutineInQueue > maxCoroutineQueueLength)
                {
                    shoeCoroutineInQueue = 0;
                    yield return new WaitForSeconds(.1f);
                }

                StartCoroutine(LoadIconOnShoeButton(s, fa.Get(i)));

            }
            shoeLoadingPanel.SetActive(false);
            shoeLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();
        }
        else
        {
            shoeLoadingPanel.SetActive(false);
            shoeLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();
            
        }

        shoeCoroutineInQueue = 0;

        if(partialShoeArray!=null)
        {
            if(partialShoeArray.Count>0)
            {
                StartCoroutine(ResetPartiallyMatchingShoes(partialShoeArray));
            }
        }
        yield return null;
    }


    public IEnumerator ResetPartiallyMatchingShoes(MiniJsonArray fa)
    {
        if (fa.Count > 1)
        {
            for (int i = 0; i < fa.Count; i++)
            {
                shoeCoroutineInQueue += 1;

                //yield return new WaitUntil(() =>
                //{
                //    return shoeCoroutineInQueue < maxCoroutineQueueLength;
                //});

                GameObject s = Instantiate(shoeButtonPrefab, Vector3.zero, Quaternion.identity, femaleShoeContainer.transform);

                //if (i == 0)
                //{
                //    s.transform.GetChild(3).gameObject.SetActive(true);
                //    s.transform.GetChild(6).gameObject.SetActive(true);
                //    s.transform.GetChild(7).gameObject.SetActive(true);
                //}
                //else if (i == (fa.Count - 1))
                //{
                //    s.transform.GetChild(2).gameObject.SetActive(true);
                //    s.transform.GetChild(5).gameObject.SetActive(true);
                //    s.transform.GetChild(8).gameObject.SetActive(true);
                //}
                //else
                //{
                //    s.transform.GetChild(3).gameObject.SetActive(true);
                //    s.transform.GetChild(6).gameObject.SetActive(true);
                //    //s.transform.GetChild(7).gameObject.SetActive(true);
                //}


                s.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;

                if (shoeCoroutineInQueue > maxCoroutineQueueLength)
                {
                    shoeCoroutineInQueue = 0;
                    yield return new WaitForSeconds(.1f);
                }

                StartCoroutine(LoadIconOnShoeButton(s, fa.Get(i)));

            }
            shoeLoadingPanel.SetActive(false);
            shoeLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();
        }
        else if (fa.Count == 1)
        {
            for (int i = 0; i < fa.Count; i++)
            {
                shoeCoroutineInQueue += 1;

                //yield return new WaitUntil(() =>
                //{
                //    return shoeCoroutineInQueue < maxCoroutineQueueLength;
                //});

                GameObject s = Instantiate(shoeButtonPrefab, Vector3.zero, Quaternion.identity, femaleShoeContainer.transform);

                //if (i == 0)
                //{
                //    s.transform.GetChild(1).gameObject.SetActive(true);
                //    s.transform.GetChild(4).gameObject.SetActive(true);
                //    s.transform.GetChild(7).gameObject.SetActive(true);
                //    s.transform.GetChild(8).gameObject.SetActive(true);
                //}



                s.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;

                if (shoeCoroutineInQueue > maxCoroutineQueueLength)
                {
                    shoeCoroutineInQueue = 0;
                    yield return new WaitForSeconds(.1f);
                }

                StartCoroutine(LoadIconOnShoeButton(s, fa.Get(i)));

            }
            shoeLoadingPanel.SetActive(false);
            shoeLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();
        }
        else
        {
            shoeLoadingPanel.SetActive(false);
            shoeLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();
            //InstantiateInfoPopup("No Shoes Available");
        }

        shoeCoroutineInQueue = 0;
        yield return null;
    }

    public IEnumerator LoadIconOnShoeButton(GameObject s, MiniJsonObject m)
    {
        ShoeProperties sp = s.GetComponent<ShoeProperties>();
        sp.InitializeShoeProperty(m);

        //shoeCoroutineInQueue -= 1;
        //if(shoeCoroutineInQueue<0)
        //{
        //    shoeCoroutineInQueue = 0;
        //}

        yield return null;
    }
    #endregion RESETSHOES


    public IEnumerator GetWearingsForSelectedModel(string body, string tone, string eye)
    {

        dressDataLoadingComplete = false;
        femaleWigDataLoadingComplete = false;
        ornamentDataLoadingComplete = false;
        shoeDataLoadingComplete = false;

        StartCoroutine(ResetAllWearings());
        WWWForm form = new WWWForm();
        string device_id="A";
        string device_type= SystemInfo.deviceUniqueIdentifier;

#if UNITY_EDITOR
        device_type = "A";
        device_id = SystemInfo.deviceUniqueIdentifier;
#elif UNITY_ANDROID
        device_type = "A";
        device_id = SystemInfo.deviceUniqueIdentifier;

#elif UNITY_IPHONE
        device_type="I";
        device_id = SystemInfo.deviceUniqueIdentifier;

#endif
        form.AddField("device_id", device_id);
        form.AddField("device_type", device_type);

        form.AddField("body_type", gameController.modelHash[body]);
        form.AddField("skin_color", gameController.toneHash[tone]);
        form.AddField("eye_color", gameController.eyeHash[eye]);
        form.AddField("model_type", "F");

        //using (UnityWebRequest www = UnityWebRequest.Post(/*"http://demowebz.cu.cc*/"http://demowebz.cu.cc.bh-43.webhostbox.net/venusfashion/api/Headings/getWearings", form))
        using (UnityWebRequest www = UnityWebRequest.Post(/*"http://demowebz.cu.cc*/"http://demowebz.cu.cc.bh-43.webhostbox.net/venusfashion/api/Headings/getWearingsTwo", form))
        {
            //print(www.url);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                InstantiateInfoPopup("No Internet Connection",CloseStyle.TapToClose);
            }
            else
            {
                Debug.Log("Form upload complete!");

                string jsonString = www.downloadHandler.text;
                print("wearings : " + jsonString);




                MiniJsonObject dmo = new MiniJsonObject(jsonString);

                int error_code = dmo.GetField("error_code", -1);
                print(error_code);
                if (error_code == 0)
                {
                    MiniJsonObject rObject = dmo.GetJsonObject("data");
                    MiniJsonArray rData = rObject.GetJsonArray("full_match");

                    MiniJsonArray toneMatch = rObject.GetJsonArray("skin_color");
                    MiniJsonArray bodyMatch = rObject.GetJsonArray("body_type");

                    string notAvailableMessage = "No {0} Available";
                    string thesearenotavailable = "";


                    #region PREFETCHARRAY
                    #region PREFETCHARRAYDRESS
                    MiniJsonObject dressObject = rData.Get(0);
                    MiniJsonArray dressArray = dressObject.GetJsonArray("females");
                    gameController.currentDressList = dressArray;
                    dressDataLoadingComplete = true;
                    #endregion PREFETCHARRAYDRESS

                    #region PREFETCHARRAYWIG

                    MiniJsonObject femaleWigObject = rData.Get(1);
                    MiniJsonArray femaleWigArray = femaleWigObject.GetJsonArray("females");
                    gameController.currentFemaleWigList = femaleWigArray;
                    femaleWigDataLoadingComplete = true;
                    #endregion PREFETCHARRAYWIG

                    #region PREFETCHARRAYORNAMENT

                    MiniJsonObject ornamentObject = rData.Get(2);
                    MiniJsonArray ornamentArray = ornamentObject.GetJsonArray("females");
                    gameController.currentOrnamentList = ornamentArray;
                    ornamentDataLoadingComplete = true;

                    #endregion PREFETCHARRAYORNAMENT

                    #region PREFETCHARRAYSHOE

                    MiniJsonObject shoeObject = rData.Get(3);
                    MiniJsonArray shoeArray = shoeObject.GetJsonArray("females");
                    gameController.currentShoeList = shoeArray;
                    shoeDataLoadingComplete = true;

                    #endregion PREFETCHARRAYSHOE


                    if(dressArray.Count<=0)
                    {
                        thesearenotavailable += "Dress";
                    }
                    if(femaleWigArray.Count<=0)
                    {
                        if(thesearenotavailable!="")
                        {
                            thesearenotavailable += ",";
                        }
                        thesearenotavailable += "Wig";
                    }
                    if(ornamentArray.Count<=0)
                    {
                        if (thesearenotavailable != "")
                        {
                            thesearenotavailable += ",";
                        }
                        thesearenotavailable += "Ornament";
                    }
                    if (shoeArray.Count <= 0)
                    {
                        if (thesearenotavailable != "")
                        {
                            thesearenotavailable += ",";
                        }
                        thesearenotavailable += "Shoe";
                    }

                    if((thesearenotavailable!="" || thesearenotavailable.Length>=3)&&(!dontShowPopup))
                    {
                        string formattedMessage = string.Format(notAvailableMessage, thesearenotavailable);
                        InstantiateInfoPopup(formattedMessage, CloseStyle.TapToClose);
                    }
                    
                    //InstantiateInfoPopup("No Wigs Available", CloseStyle.TapToClose);
                    //InstantiateInfoPopup("No Ornaments Available", CloseStyle.TapToClose);
                    //InstantiateInfoPopup("No Shoes Available", CloseStyle.TapToClose);

                    #endregion PREFETCHARRAY




                    #region GETDRESSLIST
                    // get the list of dress


                    MiniJsonObject toneMatchDressObject = toneMatch.Get(0);
                    MiniJsonObject bodyMatchDressObject = bodyMatch.Get(0);

                    MiniJsonArray toneMatchDressArray = toneMatchDressObject.GetJsonArray("females");
                    MiniJsonArray bodyMatchDressArray = bodyMatchDressObject.GetJsonArray("females");

                    MiniJsonArray partialMatchDressArray = new MiniJsonArray(toneMatchDressArray.GetArrayList());
                    partialMatchDressArray.GetArrayList().InsertRange(partialMatchDressArray.Count, bodyMatchDressArray.GetArrayList());

                    StartCoroutine(ResetDresses(gameController.currentDressList,partialMatchDressArray));

                    print(string.Format("Total dresses for this combination : {0}", gameController.currentDressList.Count));
                    #endregion GETDRESSLIST



                    #region GETFEMALEWIGLIST
                    // get the list of wigs
                    


                    MiniJsonObject toneMatchWigObject = toneMatch.Get(1);
                    MiniJsonObject bodyMatchWigObject = bodyMatch.Get(1);

                    MiniJsonArray toneMatchWigArray = toneMatchWigObject.GetJsonArray("females");
                    MiniJsonArray bodyMatchWigArray = bodyMatchWigObject.GetJsonArray("females");

                    MiniJsonArray partialMatchWigArray = new MiniJsonArray(toneMatchWigArray.GetArrayList());
                    partialMatchWigArray.GetArrayList().InsertRange(partialMatchWigArray.Count, bodyMatchWigArray.GetArrayList());

                    StartCoroutine(ResetWigs(gameController.currentFemaleWigList,partialMatchWigArray));
                    
                    print(string.Format("Total wigs for this combination : {0}", gameController.currentFemaleWigList.Count));
                    #endregion GETFEMALEWIGLIST



                    #region GETORNAMENTLIST
                    // get the list of ornament
                    

                    MiniJsonObject toneMatchOrnamentObject = toneMatch.Get(2);
                    MiniJsonObject bodyMatchOrnamentObject = bodyMatch.Get(2);

                    MiniJsonArray toneMatchOrnamentArray = toneMatchOrnamentObject.GetJsonArray("females");
                    MiniJsonArray bodyMatchOrnamentArray = bodyMatchOrnamentObject.GetJsonArray("females");

                    MiniJsonArray partialMatchOrnamentArray = new MiniJsonArray(toneMatchOrnamentArray.GetArrayList());
                    partialMatchOrnamentArray.GetArrayList().InsertRange(partialMatchOrnamentArray.Count, bodyMatchOrnamentArray.GetArrayList());


                    StartCoroutine(ResetOrnaments(gameController.currentOrnamentList,partialMatchOrnamentArray));
                    print(string.Format("Total ornamens for this combination : {0}", gameController.currentOrnamentList.Count));

                    #endregion GETORNAMENTLIST




                    #region GETSHOELIST
                    // get the list of shoe
                    

                    MiniJsonObject toneMatchShoeObject = toneMatch.Get(3);
                    MiniJsonObject bodyMatchShoeObject = bodyMatch.Get(3);

                    MiniJsonArray toneMatchShoeArray = toneMatchShoeObject.GetJsonArray("females");
                    MiniJsonArray bodyMatchShoeArray = bodyMatchShoeObject.GetJsonArray("females");

                    MiniJsonArray partialMatchShoeArray = new MiniJsonArray(toneMatchShoeArray.GetArrayList());
                    partialMatchShoeArray.GetArrayList().InsertRange(partialMatchShoeArray.Count, bodyMatchShoeArray.GetArrayList());

                    StartCoroutine(ResetShoes(gameController.currentShoeList,partialMatchShoeArray));
                    print(string.Format("Total shoes for this combination : {0}", gameController.currentShoeList.Count));

                    #endregion GETSHOELST




                }





            }
            www.Dispose();

        }
        gameController.bodyChanged = false;
        dontShowPopup = false;
    }

    public IEnumerator GetWearingsForSelectedModel_Old(string body, string tone, string eye)
    {
        dressDataLoadingComplete = false;
        femaleWigDataLoadingComplete = false;
        ornamentDataLoadingComplete = false;
        shoeDataLoadingComplete = false;

        StartCoroutine(ResetAllWearings());
        WWWForm form = new WWWForm();
        string device_id;
        string device_type;

#if UNITY_EDITOR
        device_type = "A";
        device_id = SystemInfo.deviceUniqueIdentifier;
#elif UNITY_ANDROID
        device_type = "A";
        device_id = SystemInfo.deviceUniqueIdentifier;

#elif UNITY_IPHONE
        device_type="I";
        device_id = SystemInfo.deviceUniqueIdentifier;

#endif
        //form.AddField("device_id", device_id);
        //form.AddField("device_type", device_type);

        form.AddField("body_type", gameController.modelHash[body]);
        form.AddField("skin_color", gameController.toneHash[tone]);
        form.AddField("eye_color", gameController.eyeHash[eye]);
        form.AddField("model_type", "F");

        using (UnityWebRequest www = UnityWebRequest.Post(/*"http://demowebz.cu.cc*/"http://demowebz.cu.cc.bh-43.webhostbox.net/venusfashion/api/Headings/getWearings", form))
        //using (UnityWebRequest www = UnityWebRequest.Post(/*"http://demowebz.cu.cc*/"http://demowebz.cu.cc.bh-43.webhostbox.net/venusfashion/api/Headings/getWearingsTwo", form))
        {
            //print(www.url);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError )
            {
                Debug.Log(www.error);
                InstantiateInfoPopup("No Internet Connection");
            }
            else
            {
                Debug.Log("Form upload complete!");

                string jsonString = www.downloadHandler.text;
                print("wearings : "+jsonString);




                MiniJsonObject dmo = new MiniJsonObject(jsonString);

                int error_code = dmo.GetField("error_code", -1);
                print(error_code);
                if (error_code == 0)
                {
                    MiniJsonArray rData = dmo.GetJsonArray("data");


                    #region GETDRESSLIST
                    // get the list of dress
                    MiniJsonObject dressObject = rData.Get(0);
                    MiniJsonArray dressArray = dressObject.GetJsonArray("females");
                    gameController.currentDressList = dressArray;
                    dressDataLoadingComplete = true;
                    StartCoroutine(ResetDresses(gameController.currentDressList));
                    print(string.Format("Total dresses for this combination : {0}", gameController.currentDressList.Count));
                    #endregion GETDRESSLIST



                    #region GETFEMALEWIGLIST
                    // get the list of wigs
                    MiniJsonObject femaleWigObject = rData.Get(1);
                    MiniJsonArray femaleWigArray = femaleWigObject.GetJsonArray("females");
                    gameController.currentFemaleWigList = femaleWigArray;
                    femaleWigDataLoadingComplete = true;
                    StartCoroutine(ResetWigs(gameController.currentFemaleWigList));
                    print(string.Format("Total wigs for this combination : {0}", gameController.currentFemaleWigList.Count));
                    #endregion GETFEMALEWIGLIST



                    #region GETORNAMENTLIST
                    // get the list of ornament
                    MiniJsonObject ornamentObject = rData.Get(2);
                    MiniJsonArray ornamentArray = ornamentObject.GetJsonArray("females");
                    gameController.currentOrnamentList = ornamentArray;
                    ornamentDataLoadingComplete = true;
                    StartCoroutine(ResetOrnaments(gameController.currentOrnamentList));
                    print(string.Format("Total ornamens for this combination : {0}", gameController.currentOrnamentList.Count));

                    #endregion GETORNAMENTLIST




                    #region GETSHOELIST
                    // get the list of shoe
                    MiniJsonObject shoeObject = rData.Get(3);
                    MiniJsonArray shoeArray = shoeObject.GetJsonArray("females");
                    gameController.currentShoeList = shoeArray;
                    shoeDataLoadingComplete = true;
                    StartCoroutine(ResetShoes(gameController.currentShoeList));
                    print(string.Format("Total shoes for this combination : {0}", gameController.currentShoeList.Count));

                    #endregion GETSHOELST

                    


                }

                



            }
            www.Dispose();
            
        }
    }


    public IEnumerator ResetAllWearings()
    {
        StopCoroutine("DeleteAllDresses");
        StopCoroutine("DeleteAllFemaleWigs");
        StopCoroutine("DeleteAllOrnaments");
        StopCoroutine("DeleteAllShoes");

        StartCoroutine(DeleteAllDresses());
        StartCoroutine(DeleteAllFemaleWigs());
        StartCoroutine(DeleteAllOrnaments());
        StartCoroutine(DeleteAllShoes());
        yield return null;
    }

    public IEnumerator DeleteAllDresses()
    {
        print("Destroying dresses");
        Transform[] ts = femaleDressContainer.GetComponentsInChildren<Transform>();
        foreach(Transform t in ts)
        {
            if (t.gameObject != femaleDressContainer)
            {
                Destroy(t.gameObject);
            }
        }
        dressLoadingPanel.SetActive(true);
        dressLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StartRotatingStatic();
        
        yield return null;
    }
    public IEnumerator DeleteAllFemaleWigs()
    {
        print("Destroying female wigs");
        Transform[] ts = femaleWigContainer.GetComponentsInChildren<Transform>();
        foreach (Transform t in ts)
        {
            if (t.gameObject != femaleWigContainer)
            {
                Destroy(t.gameObject);
            }
        }
        wigLoadingPanel.SetActive(true);
        wigLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StartRotatingStatic();
        yield return null;
    }

    public IEnumerator DeleteAllOrnaments()
    {
        print("Destroying ornaments");
        Transform[] ts = femaleOrnamentContainer.GetComponentsInChildren<Transform>();
        foreach (Transform t in ts)
        {
            if (t.gameObject != femaleOrnamentContainer)
            {
                Destroy(t.gameObject);
            }
        }
        ornamentLoadingPanel.SetActive(true);
        ornamentLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StartRotatingStatic();
        yield return null;
    }

    public IEnumerator DeleteAllShoes()
    {
        print("Destroying shoes");
        Transform[] ts = femaleShoeContainer.GetComponentsInChildren<Transform>();
        foreach (Transform t in ts)
        {
            if(t.gameObject!=femaleShoeContainer)
            {
                Destroy(t.gameObject);
            }
        }
        shoeLoadingPanel.SetActive(true);
        shoeLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StartRotatingStatic();
        yield return null;
    }

    #endregion DYNAMICWEARINGS





    #region EDITPANELTOGGLING


    public void ToggleEditSidePanels(GameObject panel,int toggleState=0)
    {
        switch(toggleState)
        {
            case 0:
            default:
                {
                    if(panel.GetComponent<RectTransform>().anchoredPosition.x<-5)
                    {
                        panel.GetComponent<RectTransform>().DOAnchorPosX(0f,.2f);
                    }
                    else
                    {
                        panel.GetComponent<RectTransform>().DOAnchorPosX(-800f, .2f);
                    }
                    break;
                }
            case 1:
                {
                    panel.GetComponent<RectTransform>().DOAnchorPosX(0f, .2f);
                    break;
                }
            case 2:
                {
                    panel.GetComponent<RectTransform>().DOAnchorPosX(-800f, .2f);
                    break;
                }
        }
    }


    public void ToggleDressColorPanel(bool newState)
    {
        if(newState)
        {
            ToggleEditSidePanels(dressEditSidePanels[1], 2);
            ToggleEditSidePanels(dressEditSidePanels[0], 1);

            dressEditUndoButtons[1].gameObject.SetActive(false);
            dressEditUndoButtons[0].gameObject.SetActive(true);
            dressEditButtons[1].transform.GetChild(0).gameObject.SetActive(false);
            dressEditButtons[0].transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            ToggleEditSidePanels(dressEditSidePanels[0], 2);
            dressEditButtons[0].transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    public void ToggleDressBrightnessPanel(bool newState)
    {
        
        if (newState)
        {
            ToggleEditSidePanels(dressEditSidePanels[0], 2);
            ToggleEditSidePanels(dressEditSidePanels[1], 1);

            dressEditUndoButtons[0].gameObject.SetActive(false);
            dressEditUndoButtons[1].gameObject.SetActive(true);
            dressEditButtons[0].transform.GetChild(0).gameObject.SetActive(false);
            dressEditButtons[1].transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            ToggleEditSidePanels(dressEditSidePanels[1], 2);
            dressEditButtons[1].transform.GetChild(0).gameObject.SetActive(false);
        }
        
    }



    public void ToggleWigColorPanel(bool newState)
    {
        if (newState)
        {
            ToggleEditSidePanels(wigEditSidePanels[1], 2);
            ToggleEditSidePanels(wigEditSidePanels[0], 1);

            wigEditUndoButtons[1].gameObject.SetActive(false);
            wigEditUndoButtons[0].gameObject.SetActive(true);

            wigEditButtons[1].transform.GetChild(0).gameObject.SetActive(false);
            wigEditButtons[0].transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            ToggleEditSidePanels(wigEditSidePanels[0], 2);
            wigEditButtons[0].transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    public void ToggleWigBrightnessPanel(bool newState)
    {
        
            if (newState)
            {
                ToggleEditSidePanels(wigEditSidePanels[0], 2);
                ToggleEditSidePanels(wigEditSidePanels[1], 1);

                wigEditUndoButtons[0].gameObject.SetActive(false);
                wigEditUndoButtons[1].gameObject.SetActive(true);

            wigEditButtons[0].transform.GetChild(0).gameObject.SetActive(false);
            wigEditButtons[1].transform.GetChild(0).gameObject.SetActive(true);
        }
            else
            {
                ToggleEditSidePanels(wigEditSidePanels[1], 2);
            wigEditButtons[1].transform.GetChild(0).gameObject.SetActive(false);
        }
        
    }





    public void ToggleShoeColorPanel(bool newState)
    {
        if (newState)
        {
            shoeEditButtons[1].GetComponent<Toggle>().isOn = false;
            shoeEditButtons[1].transform.GetChild(0).gameObject.SetActive(false);
            shoeEditButtons[0].GetComponent<Toggle>().isOn = true;
            shoeEditButtons[0].transform.GetChild(0).gameObject.SetActive(true);

            ToggleEditSidePanels(shoeEditSidePanels[1], 2);
            ToggleEditSidePanels(shoeEditSidePanels[0], 1);

            shoeEditUndoButtons[1].gameObject.SetActive(false);
            shoeEditUndoButtons[0].gameObject.SetActive(true);

            shoeEditButtons[1].transform.GetChild(0).gameObject.SetActive(false);
            shoeEditButtons[0].transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            ToggleEditSidePanels(shoeEditSidePanels[0], 2);
            shoeEditButtons[0].transform.GetChild(0).gameObject.SetActive(false);
            shoeEditButtons[0].GetComponent<Toggle>().isOn = false;
        }
    }
    public void ToggleShoeBrightnessPanel(bool newState)
    {

        if (newState)
        {

            shoeEditButtons[0].GetComponent<Toggle>().isOn = false;
            shoeEditButtons[0].transform.GetChild(0).gameObject.SetActive(false);
            shoeEditButtons[1].GetComponent<Toggle>().isOn = true;
            shoeEditButtons[1].transform.GetChild(0).gameObject.SetActive(true);


            ToggleEditSidePanels(shoeEditSidePanels[0], 2);
            ToggleEditSidePanels(shoeEditSidePanels[1], 1);

            shoeEditUndoButtons[0].gameObject.SetActive(false);
            shoeEditUndoButtons[1].gameObject.SetActive(true);

            shoeEditButtons[0].transform.GetChild(0).gameObject.SetActive(false);
            shoeEditButtons[1].transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            ToggleEditSidePanels(shoeEditSidePanels[1], 2);
            shoeEditButtons[1].transform.GetChild(0).gameObject.SetActive(false);
        }

    }


    #endregion EDITPANELTOGGLING

}
