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

    [SerializeField]
    private GameObject wigEditPanel;
    [SerializeField]
    private GameObject dressEditPanel;

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
    public GameObject wigColorSlider;

    public GameObject selectDressOptionPanel;

    public UIImageColorPicker dressColorPicker;
    public UIImageColorPicker wigColorPicker;

    public GameObject selectWearingRoot;

    public bool paidUserStatus=false;


    public int maxCoroutineQueueLength = 10;

    public int dressCoroutineInQueue = 0;
    public int wigCoroutineInQueue = 0;
    public int ornamentCoroutineInQueue = 0;
    public int shoeCoroutineInQueue = 0;

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
    void Start () {
        
        gameController = gameControllerObject.GetComponent<GameController>();
        if(gameController!=null)
        {
            paidUserStatus = gameController.IsPaidUser;
        }
        dressColorPicker = dressColorSlider.GetComponent<UIImageColorPicker>();
        wigColorPicker = wigColorSlider.GetComponent<UIImageColorPicker>();
        //wigColorPicker = wigColorSlider.GetComponent<UIImageColorPicker>();


        editButtons[0].SetActive(false);
        editButtons[1].SetActive(false);

        //StartCoroutine(GetWearingsForSelectedModel(gameController.mainBodyShape,gameController.mainBodyTone,gameController.mainEyeColor));
        //StartCoroutine(GetWearingsForSelectedModel("hourglass", "dark", "green"));
    }
	
	// Update is called once per frame
	void Update () {
		
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
                    gameController.bodyChanged = false;
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
        
        editButtons[1].SetActive(false);
        if(gameController==null)
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
            ToggleSideMenuSelectDress(0, 1, dressButtonObjects[0]);
            if (dress.transform.parent.gameObject.activeSelf && dress.gameObject.activeSelf && dress.color.a > 0)
            {
                editButtons[0].SetActive(true);
            }
            else
            {
                editButtons[0].SetActive(false);
            }
        }
        else
        {
            ToggleSideMenuSelectDress(0);
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

        editButtons[0].SetActive(false);
        gameController.ToggleOptionSideMenu(2);
        for (int i = 0; i < dressButtonObjects.Length; i++)
        {
            dressButtonObjects[i].transform.GetChild(0).gameObject.SetActive(false);
        }
        dressButtonObjects[1].transform.GetChild(0).gameObject.SetActive(newState);

        if (newState )
        {
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
            ToggleSideMenuSelectDress(1);
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
        }
        else
        {
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

    public void PutOnLongDressDynamically(DressProperties dp,bool resetDress=false)  //for dynamically loading dress
    {
        gameController = gameControllerObject.GetComponent<GameController>();
        string dressName = dp.imgName;


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

        }

        else
        {
            print("Reset Wig");
            dress.gameObject.GetComponent<Image>().DOFade(0f, .8f);
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
        if(File.Exists(dp.finalSavePath))
        {
            tempTex.LoadImage(File.ReadAllBytes(dp.finalSavePath));
            tempTex.Apply();
           
            dress.sprite = Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f), 100f);
            dress.color = Color.white;
            gameController.currentDressColor = Color.white;
            isWearingDress = true;
            editButtons[0].SetActive(true);
            currentDressName = dressName;
            
            dress.gameObject.GetComponent<Image>().DOFade(0f, 0f);
            print("image assigned");



            dress.gameObject.GetComponent<Image>().DOFade(1f, 1f);
            print("last fade");

            gameController.currentDressTexture = new Texture2D(1, 1);
            gameController.currentDressTexture.LoadImage(File.ReadAllBytes(dp.finalSavePath));
            gameController.currentDressTexture.Apply();
            gameController.currentDressProperty = new DressProperties();
            gameController.currentDressProperty = dp;
            gameController.currentDressColor = Color.white;
        }



        
    }

    public void PutOnShortDress(string gender, string modelName, string dressName)
    {
        if (gender.ToLower() == "female")
        {
            
            if (!dress.gameObject.activeSelf)
            {
                dress.gameObject.SetActive(true);
            }
            print("loading dress");
            dress.gameObject.GetComponent<Image>().DOFade(0f, .8f);
            //yield return new WaitForSeconds(.1f);



            Texture2D tempTex = Resources.Load("images/shortdresses/female/" + modelName + "/images/" + dressName) as Texture2D;
            dress.sprite = Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f), 100f);
            dress.gameObject.GetComponent<Image>().DOFade(0f, 0f);
            print("image assigned");



            dress.gameObject.GetComponent<Image>().DOFade(1f, 1f);
            print("last fade");


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
                isWearingWig = true;
                editButtons[0].SetActive(false);
                editButtons[1].SetActive(true);
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


        }
        else
        {
            print("resetting wig");
            wig.gameObject.GetComponent<Image>().DOFade(0f, .8f);
            if (!wig.gameObject.activeSelf)
            {
                wig.gameObject.SetActive(true);
            }
            if (!wig.transform.parent.gameObject.activeSelf)
            {
                wig.transform.parent.gameObject.SetActive(true);
            }
        }



        if (File.Exists(fwp.finalSavePath))
        {
            Texture2D tempTex = new Texture2D(10,10);
            tempTex.LoadImage(File.ReadAllBytes(fwp.finalSavePath));
            tempTex.Apply();
            wig.sprite = Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f), 100f);
            wig.color = Color.white;
            gameController.currentWigColor = Color.white;
            isWearingWig = true;
            editButtons[1].SetActive(true);
            currentWigName = wigName;
            wig.gameObject.GetComponent<Image>().DOFade(0f, 0f);
            print("image assigned");



            wig.gameObject.GetComponent<Image>().DOFade(1f, 1f);
            print("last fade");

            gameController.currentWigTexture = new Texture2D(1, 1);
            gameController.currentWigTexture.LoadImage(File.ReadAllBytes(fwp.finalSavePath));
            gameController.currentWigTexture.Apply();
            gameController.currentFemaleWigProperty = new FemaleWigProperties();
            gameController.currentFemaleWigProperty = fwp;
            gameController.currentWigColor = Color.white;
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



        if(File.Exists(op.finalSavePath))
        {
            Texture2D tempTex = new Texture2D(10, 10);
            tempTex.LoadImage(File.ReadAllBytes(op.finalSavePath));
            tempTex.Apply();
            ornament.sprite = Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f), 100f);
            isWearingOrnament = true;
            currentOrnamentName = ornamentName;
            ornament.gameObject.GetComponent<Image>().DOFade(0f, 0f);
            print("image assigned");



            ornament.gameObject.GetComponent<Image>().DOFade(1f, 1f);
            print("last fade");



            gameController.currentOrnamentProperty = new OrnamentProperties();
            gameController.currentOrnamentProperty = op;
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


    public void PutOnShoeDynamically(ShoeProperties sp)
    {
        gameController = gameControllerObject.GetComponent<GameController>();
        string shoeName = sp.imgName;
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
            else if (isWearingShoe && shoeName != currentShoeName)
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



       if(File.Exists(sp.finalSavePath))
        {
            Texture2D tempTex = new Texture2D(10, 10);
            tempTex.LoadImage(File.ReadAllBytes(sp.finalSavePath));
            tempTex.Apply();
            shoe.sprite = Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f), 100f);
            isWearingShoe = true;
            currentShoeName = shoeName;
            shoe.gameObject.GetComponent<Image>().DOFade(0f, 0f);
            print("image assigned");



            shoe.gameObject.GetComponent<Image>().DOFade(1f, 1f);
            print("last fade");


            gameController.currentShoeProperty = new ShoeProperties();
            gameController.currentShoeProperty = sp;
        }


        
    }


    public SaveData GetCurrentWearings()
    {
        SaveData currentWearings = new SaveData();
        
        return currentWearings;
    }



    public void OnClickEditWigButton(bool newState)
    {
        if(newState)
        {
            wigColorSlider.SetActive(true);
            wigColorPicker.ActivateColorSlider();
            gameController.sceneEditorController.enabled = false;
            editButtons[1].GetComponent<Toggle>().isOn = false;
            StartCoroutine(ToggleEditPanel(wigEditPanel));
            ToggleSideMenuSelectDress(5, 1);
        }

        

    }

    public void OnClickEditDressButton(bool newState)
    {
        if (newState)
        {
            dressColorSlider.SetActive(true);
            dressColorPicker.ActivateColorSlider();
            gameController.sceneEditorController.enabled = false;
            editButtons[0].GetComponent<Toggle>().isOn = false;
            StartCoroutine(ToggleEditPanel(dressEditPanel));
            ToggleSideMenuSelectDress(4, 1);
            
            
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
        }
        else
        {
            
            editPanel.GetComponent<RectTransform>().DOAnchorPosY(-800f, 0.3f);
            selectDressOptionPanel.GetComponent<RectTransform>().DOAnchorPosY(0f, 0.3f);
            yield return new WaitForSeconds(0.1f);
            gameController.ActiveCurrentActive(true);
        }
    }

#region DRESSWIGCOLOR

    public void OnclickAcceptDressColorChangeButton(GameObject editPanel)
    {
        StartCoroutine(ToggleEditPanel(editPanel, false));
        ToggleSideMenuSelectDress(4, 2);
        gameController.currentDressColor= dress.color;
        Color[] dcs = dress.sprite.texture.GetPixels();
        gameController.currentDressTexture.SetPixels(dcs);
        gameController.currentDressTexture.Apply();
        dressColorPicker.DeactivateColorSlider();
        dressColorSlider.SetActive(false);

        gameController.sceneEditorController.enabled = true;
    }

    public void OnclickCloseEditDressButton(GameObject editPanel)
    {
        StartCoroutine(ToggleEditPanel(editPanel, false));
        ToggleSideMenuSelectDress(4, 2);

        DiscardEditDress();
        dressColorPicker.DeactivateColorSlider();
        dressColorSlider.SetActive(false);
        
        gameController.sceneEditorController.enabled = true;
        //ToggleSideMenuSelectDress(5, 2);
    }

    public void DiscardEditDress()
    {
        dress.color = gameController.currentDressColor;
        dress.sprite = Sprite.Create(gameController.currentDressTexture, new Rect(0, 0, gameController.currentDressTexture.width, gameController.currentDressTexture.height), new Vector2(0.5f, 0.5f), 100f);
        dressColorPicker.DeactivateColorSlider();
        dressColorPicker.mainSlider.value = 0f;
        dressColorPicker.ActivateColorSlider();
    }
    public void ResetDefaultDress()
    {
        PutOnLongDressDynamically(gameController.currentDressProperty,true);
        //PutOnLongDressDynamically(gameController.currentDressProperty,true);
        dressColorPicker.DeactivateColorSlider();
        dressColorPicker.mainSlider.value = 0f;
        dressColorPicker.ActivateColorSlider();


    }



    public void OnclickAcceptWigColorChangeButton(GameObject editPanel)
    {
        StartCoroutine(ToggleEditPanel(editPanel, false));
        ToggleSideMenuSelectDress(5, 2);
        gameController.currentWigColor = wig.color;
        Color[] wcs = wig.sprite.texture.GetPixels();
        gameController.currentWigTexture.SetPixels(wcs);
        gameController.currentWigTexture.Apply();
        wigColorPicker.DeactivateColorSlider();
        wigColorSlider.SetActive(false);

        gameController.sceneEditorController.enabled = true;
    }

    public void OnclickCloseEditWigButton(GameObject editPanel)
    {
        StartCoroutine(ToggleEditPanel(editPanel, false));
        ToggleSideMenuSelectDress(5, 2);
        DiscardEditWig();
        wigColorPicker.DeactivateColorSlider();
        wigColorSlider.SetActive(false);

        gameController.sceneEditorController.enabled = true;
    }

    public void DiscardEditWig()
    {
        wig.color = gameController.currentWigColor;
        wig.sprite = Sprite.Create(gameController.currentWigTexture, new Rect(0, 0, gameController.currentWigTexture.width, gameController.currentWigTexture.height), new Vector2(0.5f, 0.5f), 100f);
        wigColorPicker.DeactivateColorSlider();
        wigColorPicker.mainSlider.value = 0f;
        wigColorPicker.ActivateColorSlider();
    }

    public void ResetDefaultWig()
    {
        PutOnWigDynamically(gameController.currentFemaleWigProperty,true);
        wigColorPicker.DeactivateColorSlider();
        wigColorPicker.mainSlider.value = 0f;
        wigColorPicker.ActivateColorSlider();
    }

    #endregion DRESSWIGCOLOR


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
        print("started.. main");
        if(fa.Count>1)
        {
            for (int i = 0; i < fa.Count; i++)
            {
                dressCoroutineInQueue += 1;

                yield return new WaitUntil(() =>
                {
                    return dressCoroutineInQueue < maxCoroutineQueueLength;
                });

                GameObject d = Instantiate(dressButtonPrefab, Vector3.zero, Quaternion.identity, femaleDressContainer.transform);
                print("loaded main: " + i);
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
                
                StartCoroutine(LoadIconOnDressButton(d, fa.Get(i)));
                if(!gameController.IsPaidUser)
                {
                    d.GetComponent<Button>().interactable = false;
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

                yield return new WaitUntil(() =>
                {
                    return dressCoroutineInQueue < maxCoroutineQueueLength;
                });

                GameObject d = Instantiate(dressButtonPrefab, Vector3.zero, Quaternion.identity, femaleDressContainer.transform);

                if (i == 0)
                {
                    d.transform.GetChild(1).gameObject.SetActive(true);
                    d.transform.GetChild(4).gameObject.SetActive(true);
                    d.transform.GetChild(7).gameObject.SetActive(true);
                    d.transform.GetChild(8).gameObject.SetActive(true);
                }
                


                d.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
                StartCoroutine(LoadIconOnDressButton(d, fa.Get(i)));
                if (!gameController.IsPaidUser)
                {
                    d.GetComponent<Button>().interactable = false;
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
            InstantiateInfoPopup("No Dresses Available",CloseStyle.TapToClose);
        }

        if(partialDressArray !=null)
        {
            if(partialDressArray.Count>0)
            {
                StartCoroutine(ResetPartiallyMatchingDresses(partialDressArray));
            }
        }
        yield return null;
    }


    public IEnumerator ResetPartiallyMatchingDresses(MiniJsonArray fa)
    {
        print("started partial");
        if (fa.Count > 1)
        {
            for (int i = 0; i < fa.Count; i++)
            {
                dressCoroutineInQueue += 1;

                yield return new WaitUntil(() =>
                {
                    return dressCoroutineInQueue < maxCoroutineQueueLength;
                });


                GameObject d = Instantiate(dressButtonPrefab, Vector3.zero, Quaternion.identity, femaleDressContainer.transform);
                print("loaded partial: " + i);
                //{

                //if (i == 0)
                //{
                //    d.transform.GetChild(3).gameObject.SetActive(true);
                //    d.transform.GetChild(6).gameObject.SetActive(true);
                //    d.transform.GetChild(7).gameObject.SetActive(true);
                //}
                //else if (i == (fa.Count - 1))
                //{
                //    d.transform.GetChild(2).gameObject.SetActive(true);
                //    d.transform.GetChild(5).gameObject.SetActive(true);
                //    d.transform.GetChild(8).gameObject.SetActive(true);
                //}
                //else
                //{
                //    d.transform.GetChild(3).gameObject.SetActive(true);
                //    d.transform.GetChild(6).gameObject.SetActive(true);
                //    //d.transform.GetChild(7).gameObject.SetActive(true);
                //}
                //}

                d.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
                StartCoroutine(LoadIconOnDressButton(d, fa.Get(i)));
                if (!gameController.IsPaidUser)
                {
                    d.GetComponent<Button>().interactable = false;
                    d.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
            //dressLoadingPanel.SetActive(false);
            //dressLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();
        }
        else if (fa.Count == 1)
        {
            dressCoroutineInQueue += 1;

            yield return new WaitUntil(() =>
            {
                return dressCoroutineInQueue < maxCoroutineQueueLength;
            });

            for (int i = 0; i < fa.Count; i++)
            {
                GameObject d = Instantiate(dressButtonPrefab, Vector3.zero, Quaternion.identity, femaleDressContainer.transform);

                //if (i == 0)
                //{
                //    d.transform.GetChild(1).gameObject.SetActive(true);
                //    d.transform.GetChild(4).gameObject.SetActive(true);
                //    d.transform.GetChild(7).gameObject.SetActive(true);
                //    d.transform.GetChild(8).gameObject.SetActive(true);
                //}



                d.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
                StartCoroutine(LoadIconOnDressButton(d, fa.Get(i)));
                if (!gameController.IsPaidUser)
                {
                    d.GetComponent<Button>().interactable = false;
                    d.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
            //dressLoadingPanel.SetActive(false);
            //dressLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();
        }
        else
        {
            dressLoadingPanel.SetActive(false);
            dressLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();
            //InstantiateInfoPopup("No Dresses Available");
        }
        yield return null;
    }



    public IEnumerator LoadIconOnDressButton(GameObject d, MiniJsonObject m)
    {
        DressProperties dp = d.GetComponent<DressProperties>();
        dp.InitializeDressProperty(m);

        dressCoroutineInQueue -= 1;
        if(dressCoroutineInQueue<0)
        {
            dressCoroutineInQueue = 0;
        }
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

                yield return new WaitUntil(() =>
                {
                    return wigCoroutineInQueue < maxCoroutineQueueLength;
                });

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

                yield return new WaitUntil(() =>
                {
                    return wigCoroutineInQueue < maxCoroutineQueueLength;
                });

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
                StartCoroutine(LoadIconOnWigButton(w, fa.Get(i)));

            }
            wigLoadingPanel.SetActive(false);
            wigLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();
        }
        else
        {
            wigLoadingPanel.SetActive(false);
            wigLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();
            InstantiateInfoPopup("No Wigs Available",CloseStyle.TapToClose);
        }

        if(partialWigArray!=null)
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

                yield return new WaitUntil(() =>
                {
                    return wigCoroutineInQueue < maxCoroutineQueueLength;
                });

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

                yield return new WaitUntil(() =>
                {
                    return wigCoroutineInQueue < maxCoroutineQueueLength;
                });

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
        yield return null;
    }

    public IEnumerator LoadIconOnWigButton(GameObject w, MiniJsonObject m)
    {
        FemaleWigProperties fwp = w.GetComponent<FemaleWigProperties>();
        fwp.InitializeWigProperty(m);

        wigCoroutineInQueue -= 1;
        if (wigCoroutineInQueue < 0)
        {
            wigCoroutineInQueue = 0;
        }

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

                yield return new WaitUntil(() =>
                {
                    return ornamentCoroutineInQueue < maxCoroutineQueueLength;
                });

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

                yield return new WaitUntil(() =>
                {
                    return ornamentCoroutineInQueue < maxCoroutineQueueLength;
                });


                GameObject o = Instantiate(ornamentButtonPrefab, Vector3.zero, Quaternion.identity, femaleOrnamentContainer.transform);


                if (i == 0)
                {
                    o.transform.GetChild(1).gameObject.SetActive(true);
                    o.transform.GetChild(4).gameObject.SetActive(true);
                    o.transform.GetChild(7).gameObject.SetActive(true);
                    o.transform.GetChild(8).gameObject.SetActive(true);
                }
                



                o.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
                StartCoroutine(LoadIconOnOrnamentButton(o, fa.Get(i)));

            }
            ornamentLoadingPanel.SetActive(false);
            ornamentLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();
        }
        else
        {
            ornamentLoadingPanel.SetActive(false);
            ornamentLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();
            InstantiateInfoPopup("No Ornaments Available",CloseStyle.TapToClose);
        }

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

                yield return new WaitUntil(() =>
                {
                    return ornamentCoroutineInQueue < maxCoroutineQueueLength;
                });


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

                yield return new WaitUntil(() =>
                {
                    return ornamentCoroutineInQueue < maxCoroutineQueueLength;
                });


                GameObject o = Instantiate(ornamentButtonPrefab, Vector3.zero, Quaternion.identity, femaleOrnamentContainer.transform);


                //if (i == 0)
                //{
                //    o.transform.GetChild(1).gameObject.SetActive(true);
                //    o.transform.GetChild(4).gameObject.SetActive(true);
                //    o.transform.GetChild(7).gameObject.SetActive(true);
                //    o.transform.GetChild(8).gameObject.SetActive(true);
                //}




                o.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
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
        yield return null;
    }

    public IEnumerator LoadIconOnOrnamentButton(GameObject o, MiniJsonObject m)
    {
        OrnamentProperties op = o.GetComponent<OrnamentProperties>();
        op.InitializeOrnamentProperty(m);

        ornamentCoroutineInQueue -= 1;
        if(ornamentCoroutineInQueue<0)
        {
            ornamentCoroutineInQueue = 0;
        }
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

                yield return new WaitUntil(() =>
                {
                    return shoeCoroutineInQueue < maxCoroutineQueueLength;
                });

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

                yield return new WaitUntil(() =>
                {
                    return shoeCoroutineInQueue < maxCoroutineQueueLength;
                });

                GameObject s = Instantiate(shoeButtonPrefab, Vector3.zero, Quaternion.identity, femaleShoeContainer.transform);

                if (i == 0)
                {
                    s.transform.GetChild(1).gameObject.SetActive(true);
                    s.transform.GetChild(4).gameObject.SetActive(true);
                    s.transform.GetChild(7).gameObject.SetActive(true);
                    s.transform.GetChild(8).gameObject.SetActive(true);
                }
                


                s.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
                StartCoroutine(LoadIconOnShoeButton(s, fa.Get(i)));

            }
            shoeLoadingPanel.SetActive(false);
            shoeLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();
        }
        else
        {
            shoeLoadingPanel.SetActive(false);
            shoeLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();
            InstantiateInfoPopup("No Shoes Available",CloseStyle.TapToClose);
        }

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

                yield return new WaitUntil(() =>
                {
                    return shoeCoroutineInQueue < maxCoroutineQueueLength;
                });

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

                yield return new WaitUntil(() =>
                {
                    return shoeCoroutineInQueue < maxCoroutineQueueLength;
                });

                GameObject s = Instantiate(shoeButtonPrefab, Vector3.zero, Quaternion.identity, femaleShoeContainer.transform);

                //if (i == 0)
                //{
                //    s.transform.GetChild(1).gameObject.SetActive(true);
                //    s.transform.GetChild(4).gameObject.SetActive(true);
                //    s.transform.GetChild(7).gameObject.SetActive(true);
                //    s.transform.GetChild(8).gameObject.SetActive(true);
                //}



                s.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
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
        yield return null;
    }

    public IEnumerator LoadIconOnShoeButton(GameObject s, MiniJsonObject m)
    {
        ShoeProperties sp = s.GetComponent<ShoeProperties>();
        sp.InitializeShoeProperty(m);

        shoeCoroutineInQueue -= 1;
        if(shoeCoroutineInQueue<0)
        {
            shoeCoroutineInQueue = 0;
        }
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

        //using (UnityWebRequest www = UnityWebRequest.Post("http://demowebz.cu.cc/venusfashion/api/Headings/getWearings", form))
        using (UnityWebRequest www = UnityWebRequest.Post("http://demowebz.cu.cc/venusfashion/api/Headings/getWearingsTwo", form))
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

                    

                    #region GETDRESSLIST
                    // get the list of dress
                    MiniJsonObject dressObject = rData.Get(0);
                    MiniJsonArray dressArray = dressObject.GetJsonArray("females");
                    gameController.currentDressList = dressArray;
                    dressDataLoadingComplete = true;

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
                    MiniJsonObject femaleWigObject = rData.Get(1);
                    MiniJsonArray femaleWigArray = femaleWigObject.GetJsonArray("females");
                    gameController.currentFemaleWigList = femaleWigArray;
                    femaleWigDataLoadingComplete = true;


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
                    MiniJsonObject ornamentObject = rData.Get(2);
                    MiniJsonArray ornamentArray = ornamentObject.GetJsonArray("females");
                    gameController.currentOrnamentList = ornamentArray;
                    ornamentDataLoadingComplete = true;

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
                    MiniJsonObject shoeObject = rData.Get(3);
                    MiniJsonArray shoeArray = shoeObject.GetJsonArray("females");
                    gameController.currentShoeList = shoeArray;
                    shoeDataLoadingComplete = true;


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

        using (UnityWebRequest www = UnityWebRequest.Post("http://demowebz.cu.cc/venusfashion/api/Headings/getWearings", form))
        //using (UnityWebRequest www = UnityWebRequest.Post("http://demowebz.cu.cc/venusfashion/api/Headings/getWearingsTwo", form))
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

}
