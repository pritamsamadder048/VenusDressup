using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MaleController : MonoBehaviour {

    public GameObject[] maleSideMenus;
    public GameController gameController;
    public GameObject maleOptionPanel;
    public GameObject[] editButtons;
    public GameObject[] maleButtonObjects;
    public GameObject[] maleAcceptDiscardSidePanels;
    public GameObject maleNextPrevButtonParent;
    public GameObject maleProjectionParent;
    public string carouselSelectedMaleModel = "male_1";

    public MaleRotationController maleRotationController;


    [SerializeField]
    private GameObject wigEditPanel;
    [SerializeField]
    private GameObject tieEditPanel;

    
    private string currentWigName = null;
    private string currentTieName = null;
    private string currentShoeName = null;

    
    private bool isWearingWig = false;
    private bool isWearingTie = false;
    private bool isWearingShoe = false;

    public string GetCarouselSelectedMaleModel()
    {
        return carouselSelectedMaleModel;
    }
    public void SetCarouselSelectedMaleModel(string val)
    {
        carouselSelectedMaleModel = val;
    }

    public GameObject GetCarouselSelectedaleModelObject()
    {
        return maleRotationController.GetSelectedShape();
    }

    // Use this for initialization
    void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
        maleRotationController.CheckForBodyChange();
	}

    public void ToggleSideMenuMale(int sideMenuIndex = 0, int showCode = 0, GameObject btnGameObject = null)
    {
        if (btnGameObject != null)
        {
            gameController.SetCurrentActive(maleSideMenus[sideMenuIndex], btnGameObject, maleOptionPanel);
        }
        if (showCode == 1)
        {

            for (int i = 0; i < maleSideMenus.Length; i++)
            {
                if (i == sideMenuIndex)
                {
                    maleSideMenus[i].GetComponent<RectTransform>().DOAnchorPosX(0f, .3f);
                }
                else
                {
                    maleSideMenus[i].GetComponent<RectTransform>().DOAnchorPosX(-400f, .3f);
                }
            }
        }
        else if (showCode == 2)
        {
            maleSideMenus[sideMenuIndex].GetComponent<RectTransform>().DOAnchorPosX(-400f, .3f);
        }
        else
        {
            for (int i = 0; i < maleSideMenus.Length; i++)
            {
                if (i == sideMenuIndex)
                {
                    if (maleSideMenus[i].GetComponent<RectTransform>().anchoredPosition.x < 0f)
                    {
                        print(string.Format("Showing menu : {0}", sideMenuIndex));
                        maleSideMenus[i].GetComponent<RectTransform>().DOAnchorPosX(0f, .3f);
                    }
                    else
                    {
                        print(string.Format("hiding menu : {0}", sideMenuIndex));
                        maleSideMenus[i].GetComponent<RectTransform>().DOAnchorPosX(-400f, .3f);
                    }
                }
                else
                {
                    maleSideMenus[i].GetComponent<RectTransform>().DOAnchorPosX(-400f, .3f);
                }
            }
        }
    }

    public void OnClickSelectMale(bool newState)
    {
        gameController.ToggleFace(false,false);
        maleButtonObjects[0].GetComponent<Toggle>().isOn = newState;
        gameController.ToggleOptionSideMenu(2);
        for (int i = 0; i < maleButtonObjects.Length; i++)
        {
            maleButtonObjects[i].transform.GetChild(0).gameObject.SetActive(false);
        }
        maleButtonObjects[0].transform.GetChild(0).gameObject.SetActive(newState);
        


        for (int i = 0; i < maleAcceptDiscardSidePanels.Length; i++)
        {
            maleAcceptDiscardSidePanels[i].SetActive(false);
        }
        maleAcceptDiscardSidePanels[0].SetActive(true);


        if (newState)
        {
            ToggleSideMenuMale(0, 2);
            ToggleSideMenuMale(1, 2);
            editButtons[0].SetActive(false);
            editButtons[1].SetActive(false);
            //maleRotationController.ShowAllShapes(true);
            StartCoroutine(ShowProjectorHideMainMaleModel());
            maleNextPrevButtonParent.SetActive(true);
            //if (dress.transform.parent.gameObject.activeSelf && dress.gameObject.activeSelf && dress.color.a > 0)
            //{
            //    editButtons[0].SetActive(true);
            //}
            //else
            //{
            //    editButtons[0].SetActive(false);
            //}
        }
        else
        {
            
            //editButtons[0].SetActive(false);
        }
    }


    public void OnClickSelectWigsForMale(bool newState)   //select long dress handler
    {
        
        //editButtons[1].SetActive(false);
        maleButtonObjects[1].GetComponent<Toggle>().isOn = newState;
        gameController.ToggleOptionSideMenu(2);
        for (int i = 0; i < maleButtonObjects.Length; i++)
        {
            maleButtonObjects[i].transform.GetChild(0).gameObject.SetActive(false);
        }
        maleButtonObjects[1].transform.GetChild(0).gameObject.SetActive(newState);


        for (int i = 0; i < maleAcceptDiscardSidePanels.Length; i++)
        {
            maleAcceptDiscardSidePanels[i].SetActive(false);
        }
        


        if (newState)
        {
            ToggleSideMenuMale(0, 1, maleButtonObjects[1]);
            //maleRotationController.HideUnSelectedShapes(newState);

            //maleAcceptDiscardSidePanels[1].SetActive(true);
            StartCoroutine(HideProjectorShowMainMaleModel());
            maleNextPrevButtonParent.SetActive(false);
            if (gameController.maleWig.transform.parent.gameObject.activeSelf && gameController.maleWig.gameObject.activeSelf && gameController.maleWig.color.a > 0)
            {
                editButtons[1].SetActive(false);
                editButtons[0].SetActive(true);
            }
            else
            {
                editButtons[0].SetActive(false);
            }
            
        }
        else
        {
            ToggleSideMenuMale(0);
            editButtons[0].SetActive(false);
        }
    }

    public void OnClickSelectTiesForMale(bool newState)   //select long dress handler
    {
        
        //editButtons[1].SetActive(false);
        maleButtonObjects[2].GetComponent<Toggle>().isOn = newState;
        gameController.ToggleOptionSideMenu(2);
        for (int i = 0; i < maleButtonObjects.Length; i++)
        {
            maleButtonObjects[i].transform.GetChild(0).gameObject.SetActive(false);
        }
        maleButtonObjects[2].transform.GetChild(0).gameObject.SetActive(newState);

        for (int i = 0; i < maleAcceptDiscardSidePanels.Length; i++)
        {
            maleAcceptDiscardSidePanels[i].SetActive(false);
        }
        


        if (newState)
        {
            ToggleSideMenuMale(1, 1, maleButtonObjects[1]);
            //maleRotationController.HideUnSelectedShapes(newState);

            //maleAcceptDiscardSidePanels[2].SetActive(true);
            StartCoroutine(HideProjectorShowMainMaleModel());
            maleNextPrevButtonParent.SetActive(false);
            if (gameController.maleTie.transform.parent.gameObject.activeSelf && gameController.maleTie.gameObject.activeSelf && gameController.maleTie.color.a > 0)
            {
                editButtons[0].SetActive(false);
                editButtons[1].SetActive(true);
            }
            else
            {
                editButtons[1].SetActive(false);
            }

            
        }
        else
        {
            ToggleSideMenuMale(1);
            editButtons[1].SetActive(false);
        }
    }


    public IEnumerator HideProjectorShowMainMaleModel()
    {
        gameController.maleProjectionParent.transform.GetChild(0).GetComponent<RawImage>().DOFade(0f, .3f).onComplete+=()=> {
            gameController.ShowMale();
            gameController.maleProjectionParent.SetActive(false);
            if (gameController.IsUsingCustomFace())
            {
                gameController.ToggleFace(true, false);
            }
        };
        
        yield return null;
    }

    public IEnumerator ShowProjectorHideMainMaleModel()
    {
        gameController.HideMale();
        gameController.maleProjectionParent.SetActive(true);
        yield return new WaitForSeconds(.1f);

        gameController.maleProjectionParent.transform.GetChild(0).GetComponent<RawImage>().DOFade(1f, .3f);
        yield return null;
    }





#region MALEWEARINGS
    public bool IsWearingWig()
    {
        return isWearingWig;
    }
    public bool IsWearingShoe()
    {
        return isWearingShoe;
    }

    public bool IsWearingTie()
    {
        return isWearingTie;
    }

    
    public void SetIsWearingWig(bool val)
    {
        isWearingWig = val;
    }
    public void SetIsWearingShoe(bool val)
    {
        isWearingShoe = val;
    }

    public void SetIsWearingTie(bool val)
    {
        isWearingTie = val;
    }



    public void OnClickWig()
    {
        PutOnWig("male", gameController.GetMainMaleBodyName(), "wig_00");
    }



    public void PutOnWig(string gender, string modelName, string wigName)
    {


        if (gender.ToLower() == "male")
        {
            if (!gameController.maleWig.transform.parent.gameObject.activeSelf && gameController.maleWig.color.a > 0.5f && wigName == currentWigName)
            {
                gameController.maleWig.transform.parent.gameObject.SetActive(true);
                editButtons[1].SetActive(false);
                editButtons[0].SetActive(true);
                return;
            }
            else if (!gameController.maleWig.transform.parent.gameObject.activeSelf && gameController.maleWig.color.a <= 0.5f && wigName == currentWigName)
            {
                gameController.maleWig.transform.parent.gameObject.SetActive(true);
                gameController.maleWig.DOFade(1f, .5f);
                print("else if");
                isWearingWig = true;
                editButtons[1].SetActive(false);
                editButtons[0].SetActive(true);
                return;
            }

            if (isWearingWig && wigName == currentWigName)
            {
                gameController.maleWig.gameObject.GetComponent<Image>().DOFade(0f, .5f);
                isWearingWig = false;
                editButtons[0].SetActive(false);
                return;
            }
            else if (!isWearingWig && wigName == currentWigName)
            {
                gameController.maleWig.gameObject.GetComponent<Image>().DOFade(1f, .5f);
                isWearingWig = true;
                editButtons[0].SetActive(true);
                return;
            }
            else if (isWearingWig && wigName != currentWigName)
            {
                gameController.maleWig.gameObject.GetComponent<Image>().DOFade(0f, .5f);
                isWearingWig = false;
                editButtons[0].SetActive(true);
            }
            else
            {
                editButtons[0].SetActive(true);
            }

            if (!gameController.maleWig.gameObject.activeSelf)
            {
                gameController.maleWig.gameObject.SetActive(true);
            }
            if (!gameController.maleWig.transform.parent.gameObject.activeSelf)
            {
                gameController.maleWig.transform.parent.gameObject.SetActive(true);
            }
            print("loading male wig");
            gameController.maleWig.gameObject.GetComponent<Image>().DOFade(0f, .8f);
            //yield return new WaitForSeconds(.1f);


            string loadPath = "images/wigs/male/" + modelName + "/" + wigName;
            print("trying to load : " + loadPath);
            Texture2D tempTex = Resources.Load(loadPath) as Texture2D;
            gameController.maleWig.sprite = Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f), 100f);
            isWearingWig = true;
            currentWigName = wigName;
            gameController.maleWig.gameObject.GetComponent<Image>().DOFade(0f, 0f);
            print("image assigned");



            gameController.maleWig.gameObject.GetComponent<Image>().DOFade(1f, 1f);
            print("last fade");





        }
    }


    public void OnClickTie()
    {
        PutOnTie("male", gameController.GetMainMaleBodyName(), "tie_00");
    }
    public void PutOnTie(string gender, string modelName, string tieName)
    {


        if (gender.ToLower() == "male")
        {
            if (!gameController.maleTie.transform.parent.gameObject.activeSelf && gameController.maleTie.color.a > 0.5f && tieName == currentTieName)
            {
                gameController.maleTie.transform.parent.gameObject.SetActive(true);
                editButtons[0].SetActive(false);
                editButtons[1].SetActive(true);
                return;
            }
            else if (!gameController.maleTie.transform.parent.gameObject.activeSelf && gameController.maleTie.color.a <= 0.5f && tieName == currentTieName)
            {
                gameController.maleTie.transform.parent.gameObject.SetActive(true);
                gameController.maleTie.DOFade(1f, .5f);
                print("else if");
                isWearingTie = true;
                editButtons[0].SetActive(false);
                editButtons[1].SetActive(true);
                return;
            }

            if (isWearingTie && tieName == currentTieName)
            {
                gameController.maleTie.gameObject.GetComponent<Image>().DOFade(0f, .5f);
                isWearingTie = false;
                editButtons[1].SetActive(false);
                return;
            }
            else if (!isWearingTie && tieName == currentTieName)
            {
                gameController.maleTie.gameObject.GetComponent<Image>().DOFade(1f, .5f);
                isWearingTie = true;
                editButtons[1].SetActive(true);
                return;
            }
            else if (isWearingTie && tieName != currentTieName)
            {
                gameController.maleTie.gameObject.GetComponent<Image>().DOFade(0f, .5f);
                isWearingTie = false;
                editButtons[1].SetActive(true);
            }
            else
            {
                editButtons[1].SetActive(true);
            }

            if (!gameController.maleTie.gameObject.activeSelf)
            {
                gameController.maleTie.gameObject.SetActive(true);
            }
            if (!gameController.maleTie.transform.parent.gameObject.activeSelf)
            {
                gameController.maleTie.transform.parent.gameObject.SetActive(true);
            }
            print("loading male tie");
            gameController.maleTie.gameObject.GetComponent<Image>().DOFade(0f, .8f);
            //yield return new WaitForSeconds(.1f);


            string loadPath = "images/ties/" + modelName + "/" + tieName;
            print("trying to load : " + loadPath);
            Texture2D tempTex = Resources.Load(loadPath) as Texture2D;
            gameController.maleTie.sprite = Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f), 100f);
            isWearingTie = true;
            currentTieName = tieName;
            gameController.maleTie.gameObject.GetComponent<Image>().DOFade(0f, 0f);
            print("image assigned");



            gameController.maleTie.gameObject.GetComponent<Image>().DOFade(1f, 1f);
            print("last fade");





        }
    }
    #endregion MALEDRESS
}
