using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Networking;
using System.IO;

public class MaleController : MonoBehaviour {


    public bool paidUserStatus = false;


    public GameObject[] maleSideMenus;
    public GameController gameController;
    public GameObject maleOptionPanel;
    public GameObject[] editButtons;
    public GameObject[] maleButtonObjects;
    public GameObject[] maleAcceptDiscardSidePanels;
    public GameObject maleNextPrevButtonParent;
    public GameObject maleProjectionParent;
    public string carouselSelectedMaleModel = "M1";
    public int currentMaleIndex=0;
    public float currentCarouselRotation = 0f;

    public MaleRotationController maleRotationController;



    public GameObject wigEditPanel;
    public GameObject tieEditPanel;

    public GameObject maleWigButtonPrefab;
    public GameObject maleTieButtonPrefab;

    private string currentWigName = null;
    private string currentTieName = null;
    private string currentShoeName = null;

    
    public bool isWearingWig = false;
    public bool isWearingTie = false;
    public bool isWearingShoe = false;

    public GameObject previouslyActiveSidePanel;
    public GameObject previouslyActiveDownPanel;
    public bool previouslyActiveToggle = false;
    public GameObject previouslyActiveButton;


    public GameObject[] maleWigEditSidePanels;
    public GameObject[] maleWigEditButtons;
    public GameObject[] maleWigEditUndoButtons;

    public GameObject[] maleTieEditSidePanels;
    public GameObject[] maleTieEditButtons;
    public GameObject[] maleTieEditUndoButtons;


    public GameObject maleTieColorSlider;
    public GameObject maleTieBrightnessSlider;
    public GameObject maleWigColorSlider;
    public GameObject maleWigBrightnessSlider;
    
    public MaleUIImageColorPicker maleWigColorPicker;
    public MaleUIImageColorPicker maleWigBrightnessPicker;
    public MaleUIImageColorPicker maleTieColorPicker;
    public MaleUIImageColorPicker maleTieBrightnessPicker;



    public MaleWigProperties currentMaleWigProperty;
    public MaleTieProperties currentMaleTieProperty;
    public Color currentMaleWigColor = new Color(0.5f, 0.5f, 0.5f, 0f);
    public Color currentMaleTieColor = new Color(0.5f, 0.5f, 0.5f, 0f);

    public bool maleWigDataLoadingComplete = false;
    public bool maleTieDataLoadingComplete = false;

    public bool dontShowPopup = false;

    public string SelectedMale = "M1";

    public string CurrentlyShowingDressesFor = "";

    public GameObject selectWearingRoot;


    public GameObject maleWigContainer;
    public GameObject maleTieContainer;

    public GameObject maleWigLoadingPanel;
    public GameObject maleTieLoadingPanel;


    public int maxCoroutineQueueLength = 8;
    public int maleWigCoroutineInQueue = 0;
    public int maleTieCoroutineInQueue = 0;

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


    private void OnEnable()
    {
        CheckForChanges();
    }


    private void OnDisable()
    {
        ResetMalePanel(true);
        StopAllCoroutines();
    }

    public void SetCurrentMaleIndexAndRotation()
    {
        currentMaleIndex = maleRotationController.GetSelectedShapeIndex();
        currentCarouselRotation = maleRotationController.currentRotation;
    }

    #region PUTONDYNAMICALLY

    public void PutOnMaleWigDynamically(MaleWigProperties dp, bool resetDress = false)  //for dynamically loading Male Wig
    {
        
        string wigName = dp.imgName;

        print("Dp Color = " + dp.wigColor[0] + " " + dp.wigColor[1]);
        if (!resetDress)
        {

            if (!gameController.maleWig.transform.parent.gameObject.activeSelf && gameController.maleWig.color.a > 0.5f && wigName == currentWigName)
            {
                print("!malewig.transform.parent.gameObject.activeSelf && malewig.color.a > 0.5f && wigName == currentDressName");
                gameController.maleWig.transform.parent.gameObject.SetActive(true);
                isWearingWig = true;
                editButtons[1].SetActive(false);
                editButtons[0].SetActive(true);
                return;
            }
            else if (!gameController.maleWig.transform.parent.gameObject.activeSelf && gameController.maleWig.color.a <= 0.5f && wigName == currentWigName)
            {
                print("!gameController.maleWig.transform.parent.gameObject.activeSelf && gameController.maleWig.color.a <= 0.5f && wigName == currentWigName");

                gameController.maleWig.transform.parent.gameObject.SetActive(true);
                gameController.maleWig.DOFade(1f, .5f);
                //print("else if");
                isWearingWig = true;
                editButtons[1].SetActive(false);
                editButtons[0].SetActive(true);
                return;
            }

            if (isWearingWig && wigName == currentWigName)
            {
                print("if (isWearingDress && dressName == currentDressName)");
                gameController.maleWig.gameObject.GetComponent<Image>().DOFade(0f, .5f);
                isWearingWig = false;
                editButtons[0].SetActive(false);
                return;
            }
            else if (!isWearingWig && wigName == currentWigName)
            {
                print("else if (!isWearingDress && dressName == currentDressName)");
                gameController.maleWig.gameObject.GetComponent<Image>().DOFade(1f, .5f);
                isWearingWig = true;
                editButtons[0].SetActive(true);
                return;
            }
            else if (isWearingWig && wigName != currentWigName)
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
            if (!gameController.maleWig.gameObject.activeSelf)
            {
                gameController.maleWig.gameObject.SetActive(true);
            }
            if (!gameController.maleWig.transform.parent.gameObject.activeSelf)
            {
                gameController.maleWig.transform.parent.gameObject.SetActive(true);
            }
            print("loading male wig");
            //dress.gameObject.GetComponent<Image>().DOFade(0f, .8f);
            //yield return new WaitForSeconds(.1f);

        }

        else
        {
            print("Reset Dress");
            //dress.gameObject.GetComponent<Image>().DOFade(0f, .5f);
            if (!gameController.maleWig.gameObject.activeSelf)
            {
                gameController.maleWig.gameObject.SetActive(true);
            }
            if (!gameController.maleWig.transform.parent.gameObject.activeSelf)
            {
                gameController.maleWig.transform.parent.gameObject.SetActive(true);
            }
        }

        Texture2D tempTex = new Texture2D(10, 10);


        if (File.Exists(dp.finalSavePath))
        {

            tempTex.LoadImage(File.ReadAllBytes(dp.finalSavePath));
            tempTex.Apply();

            if (isWearingWig)
            {
                DestroyImmediate(gameController.tmpMaleWig.sprite);
                gameController.tmpMaleWig.sprite = gameController.maleWig.sprite;
                gameController.tmpMaleWig.color = new Color(gameController.maleWig.color.r, gameController.maleWig.color.g, gameController.maleWig.color.b, 1f);
            }
            else
            {
                DestroyImmediate(gameController.tmpMaleWig.sprite);
                gameController.tmpMaleWig.sprite = gameController.maleWig.sprite;
                //gameController.tmpDress.color = new Color(dress.color.r, dress.color.g, dress.color.b, 1f);
            }
            gameController.tmpMaleWig.gameObject.SetActive(true);
            gameController.maleWig.gameObject.SetActive(false);

            // change the dress at background
            {
                gameController.maleWig.sprite = Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f), 100f);
                gameController.maleWig.color = new Color(dp.wigColor[0], dp.wigColor[1], dp.wigColor[2], 0f); //new Color(0.5f, .5f, .5f, 0f);
                gameController.maleWig.gameObject.SetActive(true);
                this.currentMaleWigColor= gameController.currentMaleWigColor = new Color(dp.wigColor[0], dp.wigColor[1], dp.wigColor[2], 1); //new Color(0.5f, .5f, .5f, 0f);
                print("male wig Color now is : " + gameController.currentMaleWigColor.ToString());
                isWearingWig = true;
                editButtons[0].SetActive(true);
                currentWigName = wigName;
            }

            float fadeDuration = .8f;
            if (!isWearingWig)
            {
                fadeDuration = 0f;
            }

            print("fade duration : " + fadeDuration);
            gameController.tmpMaleWig.DOFade(0f, fadeDuration).SetEase(Ease.OutSine).onComplete += delegate
            {
                gameController.tmpMaleWig.gameObject.SetActive(false);
                DestroyImmediate(gameController.tmpMaleWig.sprite);

            };
            gameController.maleWig.gameObject.GetComponent<Image>().DOFade(1f, .8f).SetEase(Ease.InSine).onComplete += delegate
            {
                print("last fade");

                gameController.currentMaleWigTexture = new Texture2D(1, 1);
                gameController.currentMaleWigTexture.LoadImage(File.ReadAllBytes(dp.finalSavePath));
                gameController.currentMaleWigTexture.Apply();
                gameController.currentMaleWigProperty = new MaleWigProperties();
                gameController.currentMaleWigProperty = dp;
                //gameController.currentDressColor = new Color(0.5f, .5f, .5f, 1f); //Color.white;
                print("male Wig Color : " + gameController.maleWig.gameObject.GetComponent<Image>().color.ToString());
                gameController.HideLoadingPanelOnly();
                gameController.HideLoadingPanelOnlyTransparent();
            };

            








        }
        else
        {
            gameController.HideLoadingPanelOnly();
            gameController.HideLoadingPanelOnlyTransparent();
            gameController.InstantiateInfoPopup("Could not Load malewig");
            DestroyImmediate(gameController.tmpMaleWig.sprite);
            gameController.tmpMaleWig.gameObject.SetActive(false);
        }



    }




    public void PutOnMaleTieDynamically(MaleTieProperties dp, bool resetDress = false)  //for dynamically loading Male Wig
    {

        string tieName = dp.imgName;

        print("Dp Color = " + dp.tieColor[0] + " " + dp.tieColor[1]);
        if (!resetDress)
        {

            if (!gameController.maleTie.transform.parent.gameObject.activeSelf && gameController.maleTie.color.a > 0.5f && tieName == currentTieName)
            {
                print("!malewig.transform.parent.gameObject.activeSelf && malewig.color.a > 0.5f && wigName == currentDressName");
                gameController.maleTie.transform.parent.gameObject.SetActive(true);
                isWearingTie = true;
                editButtons[0].SetActive(false);
                editButtons[1].SetActive(true);
                return;
            }
            else if (!gameController.maleTie.transform.parent.gameObject.activeSelf && gameController.maleTie.color.a <= 0.5f && tieName == currentTieName)
            {
                print("!gameController.maleWig.transform.parent.gameObject.activeSelf && gameController.maleWig.color.a <= 0.5f && wigName == currentWigName");

                gameController.maleTie.transform.parent.gameObject.SetActive(true);
                gameController.maleTie.DOFade(1f, .5f);
                //print("else if");
                isWearingTie = true;
                editButtons[0].SetActive(false);
                editButtons[1].SetActive(true);
                return;
            }

            if (isWearingTie && tieName == currentTieName)
            {
                print("if (isWearingDress && dressName == currentDressName)");
                gameController.maleTie.gameObject.GetComponent<Image>().DOFade(0f, .5f);
                isWearingTie = false;
                editButtons[1].SetActive(false);
                return;
            }
            else if (!isWearingTie && tieName == currentTieName)
            {
                print("else if (!isWearingDress && dressName == currentDressName)");
                gameController.maleTie.gameObject.GetComponent<Image>().DOFade(1f, .5f);
                isWearingTie = true;
                editButtons[1].SetActive(true);
                return;
            }
            else if (isWearingTie && tieName != currentTieName)
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
            if (!gameController.maleTie.gameObject.activeSelf)
            {
                gameController.maleTie.gameObject.SetActive(true);
            }
            if (!gameController.maleTie.transform.parent.gameObject.activeSelf)
            {
                gameController.maleTie.transform.parent.gameObject.SetActive(true);
            }
            print("loading male tie");
            //dress.gameObject.GetComponent<Image>().DOFade(0f, .8f);
            //yield return new WaitForSeconds(.1f);

        }

        else
        {
            print("Reset Dress");
            //dress.gameObject.GetComponent<Image>().DOFade(0f, .5f);
            if (!gameController.maleTie.gameObject.activeSelf)
            {
                gameController.maleTie.gameObject.SetActive(true);
            }
            if (!gameController.maleTie.transform.parent.gameObject.activeSelf)
            {
                gameController.maleTie.transform.parent.gameObject.SetActive(true);
            }
        }

        Texture2D tempTex = new Texture2D(10, 10);


        if (File.Exists(dp.finalSavePath))
        {

            tempTex.LoadImage(File.ReadAllBytes(dp.finalSavePath));
            tempTex.Apply();

            if (isWearingTie)
            {
                DestroyImmediate(gameController.tmpMaleTie.sprite);
                gameController.tmpMaleTie.sprite = gameController.maleTie.sprite;
                gameController.tmpMaleTie.color = new Color(gameController.maleTie.color.r, gameController.maleTie.color.g, gameController.maleTie.color.b, 1f);
            }
            else
            {
                DestroyImmediate(gameController.tmpMaleTie.sprite);
                gameController.tmpMaleTie.sprite = gameController.maleTie.sprite;
                //gameController.tmpDress.color = new Color(dress.color.r, dress.color.g, dress.color.b, 1f);
            }
            gameController.tmpMaleTie.gameObject.SetActive(true);
            gameController.tmpMaleTie.gameObject.SetActive(false);

            // change the dress at background
            {
                gameController.maleTie.sprite = Sprite.Create(tempTex, new Rect(0, 0, tempTex.width, tempTex.height), new Vector2(0.5f, 0.5f), 100f);
                gameController.maleTie.color = new Color(dp.tieColor[0], dp.tieColor[1], dp.tieColor[2], 0f); //new Color(0.5f, .5f, .5f, 0f);
                gameController.maleTie.gameObject.SetActive(true);
                this.currentMaleTieColor = gameController.currentMaleTieColor = new Color(dp.tieColor[0], dp.tieColor[1], dp.tieColor[2], 1); //new Color(0.5f, .5f, .5f, 0f);
                print("male tie Color now is : " + gameController.currentMaleTieColor.ToString());
                isWearingTie = true;
                editButtons[1].SetActive(true);
                currentTieName = tieName;
            }

            float fadeDuration = .8f;
            if (!isWearingTie)
            {
                fadeDuration = 0f;
            }

            print("fade duration : " + fadeDuration);
            gameController.tmpMaleTie.DOFade(0f, fadeDuration).SetEase(Ease.OutSine).onComplete += delegate
            {
                gameController.tmpMaleTie.gameObject.SetActive(false);
                DestroyImmediate(gameController.tmpMaleTie.sprite, true);

            };
            gameController.maleTie.gameObject.GetComponent<Image>().DOFade(1f, .8f).SetEase(Ease.InSine).onComplete += delegate
            {
                print("last fade");

                gameController.currentMaleTieTexture = new Texture2D(1, 1);
                gameController.currentMaleTieTexture.LoadImage(File.ReadAllBytes(dp.finalSavePath));
                gameController.currentMaleTieTexture.Apply();
                gameController.currentMaleTieProperty = new MaleTieProperties();
                gameController.currentMaleTieProperty = dp;
                //gameController.currentDressColor = new Color(0.5f, .5f, .5f, 1f); //Color.white;
                print("male tie Color : " + gameController.maleTie.gameObject.GetComponent<Image>().color.ToString());
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
            gameController.InstantiateInfoPopup("Could not Load male tie");
            DestroyImmediate(gameController.tmpMaleTie.sprite);
            gameController.tmpMaleTie.gameObject.SetActive(false);
        }



    }


    #endregion PUTONDYNAMICALLY


    #region DYNAMICSETTINGDRESSES
    public void CheckForChanges()
    {
        if (gameObject.activeSelf && selectWearingRoot.activeSelf)
        {

            try
            {
                if(SelectedMale!=CurrentlyShowingDressesFor || (paidUserStatus != gameController.IsPaidUser))
                {
                    paidUserStatus = gameController.IsPaidUser;
                    Debug.Log("Chacges in male occured");
                    StopCoroutine("GetWearingsForSelectedMaleModel");
                    StartCoroutine(GetWearingsForSelectedMaleModel(SelectedMale));
                    CurrentlyShowingDressesFor = SelectedMale;
                }
            }
            catch (System.Exception e)
            {
                print(string.Format("check for change error : {0}", e.Message));
            }
        }
    }

    public IEnumerator GetWearingsForSelectedMaleModel(string maleName)
    {


        maleWigDataLoadingComplete = false;
        maleTieDataLoadingComplete = false;


        StartCoroutine(ResetAllWearings());


        WWWForm form = new WWWForm();
        string device_id="";
        string device_type="A";

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

        form.AddField("body_type", maleName);
        //form.AddField("model_type", "M");

        using (UnityWebRequest www = UnityWebRequest.Post("http://demowebz.cu.cc/venusfashion/api/Headings/maleDresses", form))
        {
            //print(www.url);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                gameController.InstantiateInfoPopup("No Internet Connection", CloseStyle.TapToClose);
            }
            else
            {
                Debug.Log("Male Form upload complete!");

                string jsonString = www.downloadHandler.text;
                print(string.Format(" male  wearings for {0}  : {1}",maleName,jsonString));




                MiniJsonObject dmo = new MiniJsonObject(jsonString);

                int error_code = dmo.GetField("error_code", -1);
                print(error_code);
                if (error_code == 0)
                {
                    //MiniJsonObject rObject = dmo.GetJsonObject("data");
                    //MiniJsonArray rObject = dmo.GetJsonArray("data");
                    //MiniJsonArray rData = rObject.GetJsonArray("full_match");
                    MiniJsonArray rData = dmo.GetJsonArray("data");
                    
                    //MiniJsonArray toneMatch = rObject.GetJsonArray("skin_color");
                    //MiniJsonArray bodyMatch = rObject.GetJsonArray("body_type");

                    string notAvailableMessage = "No {0} are Available";
                    string thesearenotavailable = "";


                    #region PREFETCHARRAY
                    #region PREFETCHARRAYMALEWIG
                    MiniJsonObject maleWigObject = rData.Get(0);
                    MiniJsonArray maleWigArray = maleWigObject.GetJsonArray("males");
                    //MiniJsonArray maleWigArray = rObject.GetJsonArray("wigs");
                    gameController.currentMaleWigList = maleWigArray;
                    maleWigDataLoadingComplete = true;
                    #endregion PREFETCHARRAYDRESS

                    #region PREFETCHARRAYWIG

                    MiniJsonObject maleTieObject = rData.Get(1);
                    MiniJsonArray maleTieArray = maleTieObject.GetJsonArray("males");
                    //MiniJsonArray maleTieArray = rObject.GetJsonArray("Ties");
                    gameController.currentMaleTieList = maleTieArray;
                    maleTieDataLoadingComplete = true;
                    #endregion PREFETCHARRAYWIG

                   


                    if (maleWigArray.Count <= 0)
                    {
                        thesearenotavailable += "Wig";
                    }
                    if (maleTieArray.Count <= 0)
                    {
                        if (thesearenotavailable != "")
                        {
                            thesearenotavailable += " and ";
                        }
                        thesearenotavailable += "Tie";
                    }
                    

                    if ((thesearenotavailable != "" || thesearenotavailable.Length >= 3) && (!dontShowPopup))
                    {
                        string formattedMessage = string.Format(notAvailableMessage, thesearenotavailable);
                        gameController.InstantiateInfoPopup(formattedMessage, CloseStyle.TapToClose);
                    }


                    #endregion PREFETCHARRAY




                    #region GETMALETIELIST

                    StopCoroutine("ResetMaleWigs");
                    StartCoroutine(ResetMaleWigs(gameController.currentMaleWigList));

                    print(string.Format("Total Wig for this Male : {0}", gameController.currentMaleWigList.Count));

                    #endregion GETMALETIELIST



                    #region GETMALETIELIST
                    // get the list of wigs

                    StopCoroutine("ResetMaleTies");
                    StartCoroutine(ResetMaleTies(gameController.currentMaleTieList));

                    print(string.Format("Total Ties for this male : {0}", gameController.currentMaleTieList.Count));

                    #endregion GETMALETIELIST



                   




                }





            }
            www.Dispose();

        }
        print("still succes");
        dontShowPopup = false;
        yield return null;
    }

        public IEnumerator ResetAllWearings()
    {
        StopCoroutine("DeleteAllMaleWigs");
        StopCoroutine("DeleteAllMaleTies");

        StartCoroutine(DeleteAllMaleWigs());
        StartCoroutine(DeleteAllMaleTies());
        yield return null;
    }



    public IEnumerator DeleteAllMaleWigs()
    {
        print("Destroying male wigs");
        Transform[] ts = maleWigContainer.GetComponentsInChildren<Transform>();
        foreach (Transform t in ts)
        {
            if (t.gameObject != maleWigContainer)
            {
                Destroy(t.gameObject);
            }
        }
        maleWigLoadingPanel.SetActive(true);
        maleWigLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StartRotatingStatic();
        yield return null;
    }

    public IEnumerator DeleteAllMaleTies()
    {
        print("Destroying male ties");
        Transform[] ts = maleTieContainer.GetComponentsInChildren<Transform>();
        foreach (Transform t in ts)
        {
            if (t.gameObject != maleTieContainer)
            {
                Destroy(t.gameObject);
            }
        }
        maleTieLoadingPanel.SetActive(true);
        maleTieLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StartRotatingStatic();
        yield return null;
    }



    public IEnumerator ResetMaleWigs(MiniJsonArray fa)
    {
        if (fa.Count > 1)
        {
            for (int i = 0; i < fa.Count; i++)
            {
                maleWigCoroutineInQueue += 1;


                GameObject d = Instantiate(maleWigButtonPrefab, Vector3.zero, Quaternion.identity, maleWigContainer.transform);
                //print("loaded main: " + i);
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


                d.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;

                if (maleWigLoadingPanel.activeSelf)
                {
                    maleWigLoadingPanel.SetActive(false);
                    maleWigLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();
                }


                //yield return new WaitForSeconds(.01f);
                if (maleWigCoroutineInQueue > maxCoroutineQueueLength)
                {
                    maleWigCoroutineInQueue = 0;
                    yield return new WaitForSeconds(.1f);
                }
                StartCoroutine(LoadIconOnMaleWigButton(d, fa.Get(i)));
                if (!gameController.IsPaidUser)
                {
                    d.GetComponent<Button>().interactable = false;
                    d.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
            maleWigLoadingPanel.SetActive(false);
            maleWigLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();
        }
        else if (fa.Count == 1)
        {
            for (int i = 0; i < fa.Count; i++)
            {
                maleWigCoroutineInQueue += 1;



                GameObject d = Instantiate(maleWigButtonPrefab, Vector3.zero, Quaternion.identity, maleWigContainer.transform);

                //if (i == 0)
                //{
                //    d.transform.GetChild(1).gameObject.SetActive(true);
                //    d.transform.GetChild(4).gameObject.SetActive(true);
                //    d.transform.GetChild(7).gameObject.SetActive(true);
                //    d.transform.GetChild(8).gameObject.SetActive(true);
                //}



                d.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;


                //yield return new WaitForSeconds(.01f);
                if (maleWigCoroutineInQueue > maxCoroutineQueueLength)
                {
                    maleWigCoroutineInQueue = 0;
                    yield return new WaitForSeconds(.1f);
                }
                StartCoroutine(LoadIconOnMaleWigButton(d, fa.Get(i)));
                if (!gameController.IsPaidUser)
                {
                    d.GetComponent<Button>().interactable = false;
                    d.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
            maleWigLoadingPanel.SetActive(false);
            maleWigLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();
        }
        else
        {
            maleWigLoadingPanel.SetActive(false);
            maleWigLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();

        }

        maleWigCoroutineInQueue = 0;

        //if (partialDressArray != null)
        //{
        //    if (partialDressArray.Count > 0)
        //    {
        //        StartCoroutine(ResetPartiallyMatchingDresses(partialDressArray));
        //    }
        //}



        yield return null;
    }

    public IEnumerator ResetMaleTies(MiniJsonArray fa)
    {

        if (fa.Count > 1)
        {
            for (int i = 0; i < fa.Count; i++)
            {
                maleTieCoroutineInQueue += 1;


                GameObject d = Instantiate(maleTieButtonPrefab, Vector3.zero, Quaternion.identity, maleTieContainer.transform);
                //print("loaded main: " + i);
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


                d.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;

                if (maleTieLoadingPanel.activeSelf)
                {
                    maleTieLoadingPanel.SetActive(false);
                    maleTieLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();
                }


                //yield return new WaitForSeconds(.01f);
                if (maleTieCoroutineInQueue > maxCoroutineQueueLength)
                {
                    maleTieCoroutineInQueue = 0;
                    yield return new WaitForSeconds(.1f);
                }
                StartCoroutine(LoadIconOnMaleTieButton(d, fa.Get(i)));
                if (!gameController.IsPaidUser)
                {
                    d.GetComponent<Button>().interactable = false;
                    d.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
            maleTieLoadingPanel.SetActive(false);
            maleTieLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();
        }
        else if (fa.Count == 1)
        {
            for (int i = 0; i < fa.Count; i++)
            {
                maleTieCoroutineInQueue += 1;



                GameObject d = Instantiate(maleTieButtonPrefab, Vector3.zero, Quaternion.identity, maleTieContainer.transform);

                //if (i == 0)
                //{
                //    d.transform.GetChild(1).gameObject.SetActive(true);
                //    d.transform.GetChild(4).gameObject.SetActive(true);
                //    d.transform.GetChild(7).gameObject.SetActive(true);
                //    d.transform.GetChild(8).gameObject.SetActive(true);
                //}



                d.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;


                //yield return new WaitForSeconds(.01f);
                if (maleTieCoroutineInQueue > maxCoroutineQueueLength)
                {
                    maleTieCoroutineInQueue = 0;
                    yield return new WaitForSeconds(.1f);
                }
                StartCoroutine(LoadIconOnMaleTieButton(d, fa.Get(i)));
                if (!gameController.IsPaidUser)
                {
                    d.GetComponent<Button>().interactable = false;
                    d.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
            maleTieLoadingPanel.SetActive(false);
            maleTieLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();
        }
        else
        {
            maleTieLoadingPanel.SetActive(false);
            maleTieLoadingPanel.transform.GetChild(0).GetComponent<AnimateLoading>().StopRotating();

        }

        maleTieCoroutineInQueue = 0;

        //if (partialDressArray != null)
        //{
        //    if (partialDressArray.Count > 0)
        //    {
        //        StartCoroutine(ResetPartiallyMatchingDresses(partialDressArray));
        //    }
        //}



        yield return null;
    }

    #endregion DYNAMICSETTINGDRESSES


    public IEnumerator LoadIconOnMaleWigButton(GameObject d, MiniJsonObject m)
    {
        //print("Started work : " + Time.deltaTime);
        MaleWigProperties mp = d.GetComponent<MaleWigProperties>();
        mp.InitializeWigProperty(m);
        //print("Completed work : "+Time.deltaTime);

        //dressCoroutineInQueue -= 1;
        //if (dressCoroutineInQueue < 0)
        //{
        //    dressCoroutineInQueue = 0;
        //}
        yield return null;
    }

    public IEnumerator LoadIconOnMaleTieButton(GameObject d, MiniJsonObject m)
    {
        //print("Started work : " + Time.deltaTime);
        MaleTieProperties mp = d.GetComponent<MaleTieProperties>();
        mp.InitializeTieProperty(m);
        //print("Completed work : "+Time.deltaTime);

        //dressCoroutineInQueue -= 1;
        //if (dressCoroutineInQueue < 0)
        //{
        //    dressCoroutineInQueue = 0;
        //}
        yield return null;
    }


    public void ResetMalePanel(bool reallyResetAllButtons = false)
    {
        DeactivateAllEditPanels();

        if(reallyResetAllButtons)
        {
            maleButtonObjects[0].GetComponent<Toggle>().isOn = false;
            maleButtonObjects[1].GetComponent<Toggle>().isOn = false;
            maleButtonObjects[2].GetComponent<Toggle>().isOn = false;

        }
    }
    public void SetPreviouslyActive(GameObject sidePanel=null,GameObject downPanel=null,GameObject button=null) //set previously active panels and button
    {
        previouslyActiveSidePanel = sidePanel;
        previouslyActiveDownPanel = downPanel;
        previouslyActiveButton = button;

        if(previouslyActiveButton!=null)
        {
            Toggle t = previouslyActiveButton.GetComponent<Toggle>();
            if(t!=null)
            {
                previouslyActiveToggle = true;
            }
            else
            {
                previouslyActiveToggle = false;
            }
        }
    }

    public void ActivatePreviouslyActives()  // Activate previously actiated panels and buttons
    {
        if (previouslyActiveSidePanel != null)
        {
            previouslyActiveSidePanel.GetComponent<RectTransform>().DOAnchorPosX(0f, .3f);
        }
        if (previouslyActiveDownPanel != null)
        {
            previouslyActiveDownPanel.GetComponent<RectTransform>().DOAnchorPosY(0f, .3f);
        }
        if (previouslyActiveButton != null && previouslyActiveToggle)
        {
            previouslyActiveButton.GetComponent<Toggle>().isOn = true;
        }
    }

    public void DeActivatePreviouslyActives()  // DeActivate previously actiated panels and buttons
    {
        if(previouslyActiveSidePanel!=null)
        {
            previouslyActiveSidePanel.GetComponent<RectTransform>().DOAnchorPosX(-800f, .3f);
        }
        if(previouslyActiveDownPanel!=null)
        {
            previouslyActiveDownPanel.GetComponent<RectTransform>().DOAnchorPosY(-800f, .3f);
        }
        //if(previouslyActiveButton!=null && previouslyActiveToggle)
        //{
        //    previouslyActiveButton.GetComponent<Toggle>().isOn =false;
        //}
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
        //maleButtonObjects[0].GetComponent<Toggle>().isOn = newState;
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
            SetPreviouslyActive();
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
        //maleButtonObjects[1].GetComponent<Toggle>().isOn = newState;
        CheckForChanges();
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

            SetPreviouslyActive(maleSideMenus[0], maleOptionPanel, maleButtonObjects[1]);
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
        //maleButtonObjects[2].GetComponent<Toggle>().isOn = newState;
        CheckForChanges();
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
            ToggleSideMenuMale(1, 1, maleButtonObjects[2]);
            //maleRotationController.HideUnSelectedShapes(newState);

            //maleAcceptDiscardSidePanels[2].SetActive(true);

            SetPreviouslyActive(maleSideMenus[1], maleOptionPanel, maleButtonObjects[2]);
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



    public void ToggleMaleOptionPanel(int showCode)
    {
        switch(showCode)
        {
            case 0:
            default:
                {
                    if(maleOptionPanel.GetComponent<RectTransform>().anchoredPosition.y<=-350f)
                    {
                        maleOptionPanel.GetComponent<RectTransform>().DOAnchorPosY(0f, .2f);
                    }
                    else
                    {
                        maleOptionPanel.GetComponent<RectTransform>().DOAnchorPosY(-8000f, .2f);
                    }
                    break;
                }
            case 1:
                {
                    maleOptionPanel.GetComponent<RectTransform>().DOAnchorPosY(0f, .2f);
                    break;
                }
            case 2:
                {
                    maleOptionPanel.GetComponent<RectTransform>().DOAnchorPosY(-8000f, .2f);
                    break;
                }
        }
    }
    


    #region EDIT
    public void ToggleEditSidePanels(GameObject panel, int toggleState = 0)
    {
        switch (toggleState)
        {
            case 0:
            default:
                {
                    if (panel.GetComponent<RectTransform>().anchoredPosition.x < -5)
                    {
                        panel.GetComponent<RectTransform>().DOAnchorPosX(0f, .2f);
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


    public void DeactivateWigEditPanels()
    {
        ToggleMaleWigColorPanel(false);
        ToggleMaleWigBrightnessPanel(false);
    }

    public void DeactivateTieEditPanels()
    {
        ToggleMaleTieColorPanel(false);
        ToggleMaleTieBrightnessPanel(false);
    }

    public void DeactiveEditDownPanels()
    {
        wigEditPanel.GetComponent<RectTransform>().DOAnchorPosY(-800f, .3f);
        tieEditPanel.GetComponent<RectTransform>().DOAnchorPosY(-800f, .3f);
        ToggleMaleOptionPanel(1);
        ActivatePreviouslyActives();

    }

    public void DeactivateAllEditPanels()
    {
        DeactivateWigEditPanels();
        DeactivateTieEditPanels();
        DeactiveEditDownPanels();
    }




    #region EDITWIG

    public void CancelEditWig()
    {
        gameController.maleWig.color = new Color(currentMaleWigColor.r, currentMaleWigColor.g, currentMaleWigColor.b,1f);
        DeactivateAllEditPanels();
        gameController.ActiveSceneEditorController();
    }


    public void DiscardEditMaleWig()
    {
        DiscardEditMaleWigBrightness();
        DiscardEditMaleWigColor();
    }

    public void DiscardEditMaleWigBrightness()
    {
        Color c = gameController.currentMaleWigColor;
        c.r = gameController.maleWig.color.r;
        gameController.maleWig.color = c;

        maleWigBrightnessPicker.DeactivateColorSlider();
        maleWigBrightnessPicker.mainSlider.value = c.b;
        maleWigBrightnessPicker.ActivateColorSlider(false);
    }

    public void DiscardEditMaleWigColor()
    {
        Color c = gameController.currentMaleWigColor;
        c.b = gameController.maleWig.color.b;
        gameController.maleWig.color = c;

        maleWigColorPicker.DeactivateColorSlider();
        maleWigColorPicker.mainSlider.value = c.r;
        maleWigColorPicker.ActivateColorSlider(false);
    }

    public void AcceptEditWig()
    {
        gameController.currentMaleWigColor= currentMaleWigColor = gameController.maleWig.color;
        DeactivateAllEditPanels();
        gameController.ActiveSceneEditorController();
    }

    public void ActiveMaleWigEditSliders(bool setAsMaleWig=false)
    {
        if(setAsMaleWig)
        {
            Color c = gameController.currentMaleWigColor;

            maleWigColorSlider.SetActive(true);
            maleWigColorPicker.DeactivateColorSlider();
            maleWigColorPicker.mainSlider.value = c.r;
            maleWigColorPicker.ActivateColorSlider(false);

            maleWigBrightnessSlider.SetActive(true);
            maleWigBrightnessPicker.DeactivateColorSlider();
            maleWigBrightnessPicker.mainSlider.value = c.b;
            maleWigBrightnessPicker.ActivateColorSlider(false);
        }
        else
        {
            maleWigColorSlider.SetActive(true);
            maleWigColorPicker.ActivateColorSlider(false);
            maleWigBrightnessSlider.SetActive(true);
            maleWigBrightnessPicker.ActivateColorSlider(false);
        }
    }

    public void DeactiveMaleWigEditSliders()
    {

        ToggleEditSidePanels(maleWigEditSidePanels[0], 2);
        ToggleEditSidePanels(maleWigEditSidePanels[1], 2);

        maleWigColorPicker.DeactivateColorSlider();
        maleWigColorSlider.SetActive(false);
        maleWigBrightnessPicker.DeactivateColorSlider();
        maleWigBrightnessSlider.SetActive(false);
    }
    public void ToggleMaleWigColorPanel(bool newState)
    {
        if (newState)
        {
            maleWigEditButtons[0].GetComponent<Toggle>().isOn = true;
            maleWigEditButtons[1].GetComponent<Toggle>().isOn = false;

            ToggleEditSidePanels(maleWigEditSidePanels[1], 2);
            ToggleEditSidePanels(maleWigEditSidePanels[0], 1);

            maleWigEditUndoButtons[1].gameObject.SetActive(false);
            maleWigEditUndoButtons[0].gameObject.SetActive(true);

            maleWigEditButtons[1].transform.GetChild(0).gameObject.SetActive(false);
            maleWigEditButtons[0].transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            ToggleEditSidePanels(maleWigEditSidePanels[0], 2);
            maleWigEditButtons[0].transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    public void ToggleMaleWigBrightnessPanel(bool newState)
    {

        if (newState)
        {
            maleWigEditButtons[1].GetComponent<Toggle>().isOn = true;
            maleWigEditButtons[0].GetComponent<Toggle>().isOn = false;

            ToggleEditSidePanels(maleWigEditSidePanels[0], 2);
            ToggleEditSidePanels(maleWigEditSidePanels[1], 1);

            maleWigEditUndoButtons[0].gameObject.SetActive(false);
            maleWigEditUndoButtons[1].gameObject.SetActive(true);

            maleWigEditButtons[0].transform.GetChild(0).gameObject.SetActive(false);
            maleWigEditButtons[1].transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            ToggleEditSidePanels(maleWigEditSidePanels[1], 2);
            maleWigEditButtons[1].transform.GetChild(0).gameObject.SetActive(false);
        }

    }

    public void OnClickEditMaleWigButton()
    {
        gameController.sceneEditorController.enabled = false;
        DeActivatePreviouslyActives();
        ActiveMaleWigEditSliders(true);
        wigEditPanel.GetComponent<RectTransform>().DOAnchorPosY(0f, .3f);
        ToggleMaleWigColorPanel(true);
    }



    #endregion EDITWIG



    #region EDITTIE

    public void CancelEditTie()
    {
        gameController.maleTie.color = new Color(currentMaleTieColor.r, currentMaleTieColor.g, currentMaleTieColor.b,1f);
        DeactivateAllEditPanels();
        gameController.ActiveSceneEditorController();
    }

    public void AcceptEditTie()
    {
        gameController.currentMaleTieColor=currentMaleTieColor= gameController.maleTie.color;
        DeactivateAllEditPanels();
        gameController.ActiveSceneEditorController();
    }



    public void DiscardEditMaleTie()
    {
        DiscardEditMaleWigBrightness();
        DiscardEditMaleWigColor();
    }

    public void DiscardEditMaleTieBrightness()
    {
        Color c = gameController.currentMaleTieColor;
        c.r = gameController.maleTie.color.r;
        gameController.maleTie.color = c;

        maleTieBrightnessPicker.DeactivateColorSlider();
        maleTieBrightnessPicker.mainSlider.value = c.b;
        maleTieBrightnessPicker.ActivateColorSlider(false);
    }

    public void DiscardEditMaleTieColor()
    {
        Color c = gameController.currentMaleTieColor;
        c.b = gameController.maleTie.color.b;
        gameController.maleTie.color = c;

        maleTieColorPicker.DeactivateColorSlider();
        maleTieColorPicker.mainSlider.value = c.r;
        maleTieColorPicker.ActivateColorSlider(false);
    }

    public void ActiveMaleTieEditSliders(bool setAsMale=false)
    {
        if(setAsMale)
        {
            Color c = gameController.currentMaleTieColor;

            maleTieColorSlider.SetActive(true);
            maleTieColorPicker.DeactivateColorSlider();
            maleTieColorPicker.mainSlider.value = c.r;
            maleTieColorPicker.ActivateColorSlider(false);

            maleTieBrightnessSlider.SetActive(true);
            maleTieBrightnessPicker.DeactivateColorSlider();
            maleTieBrightnessPicker.mainSlider.value = c.b;
            maleTieBrightnessPicker.ActivateColorSlider(false);
        }
        else
        {
            maleTieColorSlider.SetActive(true);
            maleTieColorPicker.ActivateColorSlider(false);
            maleTieBrightnessSlider.SetActive(true);
            maleTieBrightnessPicker.ActivateColorSlider(false);
        }
    }

    public void DeactiveMaleTieEditSliders()
    {

        ToggleEditSidePanels(maleTieEditSidePanels[0], 2);
        ToggleEditSidePanels(maleTieEditSidePanels[1], 2);

        maleTieColorPicker.DeactivateColorSlider();
        maleTieColorSlider.SetActive(false);
        maleTieBrightnessPicker.DeactivateColorSlider();
        maleTieBrightnessSlider.SetActive(false);
    }
    public void ToggleMaleTieColorPanel(bool newState)
    {
        if (newState)
        {
            maleTieEditButtons[0].GetComponent<Toggle>().isOn = true;
            maleTieEditButtons[1].GetComponent<Toggle>().isOn = false;


            ToggleEditSidePanels(maleTieEditSidePanels[1], 2);
            ToggleEditSidePanels(maleTieEditSidePanels[0], 1);

            maleTieEditUndoButtons[1].gameObject.SetActive(false);
            maleTieEditUndoButtons[0].gameObject.SetActive(true);

            maleTieEditButtons[1].transform.GetChild(0).gameObject.SetActive(false);
            maleTieEditButtons[0].transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            ToggleEditSidePanels(maleTieEditSidePanels[0], 2);
            maleTieEditButtons[0].transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    public void ToggleMaleTieBrightnessPanel(bool newState)
    {

        if (newState)
        {
            maleTieEditButtons[1].GetComponent<Toggle>().isOn = true;
            maleTieEditButtons[0].GetComponent<Toggle>().isOn = false;


            ToggleEditSidePanels(maleTieEditSidePanels[0], 2);
            ToggleEditSidePanels(maleTieEditSidePanels[1], 1);

            maleTieEditUndoButtons[0].gameObject.SetActive(false);
            maleTieEditUndoButtons[1].gameObject.SetActive(true);

            maleTieEditButtons[0].transform.GetChild(0).gameObject.SetActive(false);
            maleTieEditButtons[1].transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            ToggleEditSidePanels(maleTieEditSidePanels[1], 2);
            maleTieEditButtons[1].transform.GetChild(0).gameObject.SetActive(false);
        }

    }

    public void OnClickEditMaleTieButton()
    {
        gameController.sceneEditorController.enabled = false;
        DeActivatePreviouslyActives();
        ActiveMaleTieEditSliders(true);
        tieEditPanel.GetComponent<RectTransform>().DOAnchorPosY(0f, .3f);
        ToggleMaleTieColorPanel(true);
    }



    #endregion EDITTIE


    #endregion EDIT

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



    public void RemoveMaleWig()
    {
        gameController.maleWig.gameObject.GetComponent<Image>().DOFade(0f, .5f);
        isWearingWig = false;
        currentWigName = "";

        gameController.currentMaleWigProperty = null;
        editButtons[0].SetActive(false);
        return;
    }

    public void RemoveMaleTie()
    {
        gameController.maleTie.gameObject.GetComponent<Image>().DOFade(0f, .5f);
        isWearingTie = false;
        currentTieName = "";
        gameController.currentMaleTieProperty = null;
        editButtons[1].SetActive(false);
        return;
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
            gameController.maleWig.color = new Color(0.5f, 0.5f, 0.5f, 1f);


            gameController.maleWig.gameObject.GetComponent<Image>().DOFade(1f, 1f);
            print("last fade");

            currentMaleWigColor = new Color(0.5f, 0.5f, 0.5f, 1f);



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
    #endregion MALEWEARINGS







    public void SelectThisParticularMaleModel(float rotation, int index = 0,bool hideUnselected=false)
    {

        maleRotationController.SelectThisMaleModel(rotation, index);



        if (hideUnselected)
        {
            print("Executing if");
            maleRotationController.ShowAllShapes(true);
            maleRotationController.HideUnSelectedShapes(true);
        }
        else
        {
            print("Executing else");
            maleRotationController.ShowAllShapes(true);
        }

    }


    public void DeleteMaleWig()
    {
        CancelEditWig();
        RemoveMaleWig();
    }

    public void DeleteMaleTie()
    {
        CancelEditTie();
        RemoveMaleTie();
    }
}
