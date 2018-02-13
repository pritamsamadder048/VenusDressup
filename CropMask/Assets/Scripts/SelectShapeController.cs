using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG;
using DG.Tweening;

public class SelectShapeController : MonoBehaviour {

    [SerializeField]
    private GameObject gameControllerObject;
    public GameController gameController;
    [SerializeField]
    private GameObject[] sideMenus;
    [SerializeField]
    private GameObject[] shapeButtonObjects;
    [SerializeField]
    private GameObject rotationControllerObject;
    public RotationController rotationController;

    [SerializeField]
    private GameObject previousButtonObject;
    [SerializeField]
    private GameObject nextButtonObject;
    
    
    [SerializeField]
    private GameObject acceptDiscardPanelForBodyShape;
    [SerializeField]
    private GameObject acceptDiscardPanelForEyeColor;
    [SerializeField]
    private GameObject acceptDiscardPanelForBodyTone;

    [SerializeField]
    private GameObject selectedFemaleModel;


    public string carouselSelectedBodyShape = "apple";

    public string carouselSelectedBodyTone = "white";

    public string carouselSelectedEyeColor = "black";

    public float carouselSelectedRotation = 0f;

    public RawImage faceCloseup;
    //public SpriteRenderer femaleSprite;


    public GameObject bodyToneColorSlider;
    public BodyToneColorPicker bodyToneColorPicker;

    public float GetcarouselSelectedRotation()
    {
        return carouselSelectedRotation;
    }

    public void SetcarouselSelectedRotation(float val)
    {
        carouselSelectedRotation = val;
    }
    public int GetCarouselSelectedModelIndex()
    {
        return rotationController.GetSelectedShapeIndex();
    }
    public string GetCarouselSelectedBodyTone()
    {
        return carouselSelectedBodyTone;
    }
    public string GetCarouselSelectedBodyShape()
    {
        return carouselSelectedBodyShape;
    }
    public string GetCarouselSelectedEyeColor()
    {
        return carouselSelectedEyeColor;
    }
    public void SetCarouselSelectedBodyTone(string val)
    {
        carouselSelectedBodyTone=val;
    }
    public void SetCarouselSelectedBodyShape(string val)
    {
        carouselSelectedBodyShape=val;
    }
    public void SetCarouselSelectedEyeColor(string val)
    {
        carouselSelectedEyeColor = val;
    }

    public bool GetUnselectedIsHidden()
    {
        return rotationController.unselectedIsHidden;
    }

    public void SetCarouselSelectedBodyShapeAndTone(string name)
    {
        SetCarouselSelectedBodyShape(name.Split('_')[0]);
        SetCarouselSelectedBodyTone(name.Split('_')[1]);
        SetCarouselSelectedEyeColor(name.Split('_')[2]);
    }

    private void Awake()
    {
        rotationController = rotationControllerObject.GetComponent<RotationController>();
    }
    // Use this for initialization
    void Start () {
        gameController = gameControllerObject.GetComponent<GameController>();
        
        //rotationController.TransparentUnSelected();
        rotationControllerObject.SetActive(true);
        //rotationController.HideUnSelectedShapes(true);
        OnClickSelectShapeButton(true);

    }
	

    public void InitControllers()
    {
        if(rotationController==null)
        {
            rotationController = rotationControllerObject.GetComponent<RotationController>();
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
    public void SelectThisParticularModel(float rotation,int index=0)
    {
        InitControllers();
        rotationController.SelectThisModel(rotation, index);

        

        if(GetUnselectedIsHidden())
        {
            print("Executing if");
            rotationController.ShowAllShapes(true);
            rotationController.HideUnSelectedShapes(true);
        }
        else
        {
            print("Executing else");
            rotationController.ShowAllShapes(true);
        }

    }


    public void SetBodyToneColor(Color color)
    {
        gameController.femaleModelImageObject.GetComponent<Image>().color = color;
        gameController.currentBodyToneColor = color;
    }

    public void SetBodyToneColor(float[] color)
    {
        gameController.femaleModelImageObject.GetComponent<Image>().color = new Color(color[0], color[1], color[2], color[3]);
        gameController.currentBodyToneColor= new Color(color[0], color[1], color[2], color[3]);
    }


    public void ResetToDefaultColor()
    {
        gameController.femaleModelImageObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
    }


    public void PreLoadOnClickSelectShapeButton(bool newState)
    {
        if (gameController == null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        shapeButtonObjects[0].GetComponent<Toggle>().isOn = newState;

        if (newState)
        {
            if (!gameController.isFirstTime)
            {
                if (gameController.shouldShowDressResetWarning && newState)
                {
                    if (gameController.selectDressController.isWearingDress || gameController.selectDressController.isWearingWig || gameController.selectDressController.isWearingOrnament || gameController.selectDressController.isWearingShoe)
                    {
                        gameController.popupController.InstantiateBodyShapeSelectionPopup();
                        gameController.shouldShowDressResetWarning = false;
                        //OnClickSelectBodyToneButton(true);
                    }
                    else
                    {
                        OnClickSelectShapeButton(newState);

                    }

                }
                else if(newState)
                {
                    OnClickSelectShapeButton(newState);
                    
                }
            }
            else
            {
                OnClickSelectShapeButton(newState);
            }
            

        }

        //shapeButtonObjects[0].GetComponent<Toggle>().isOn = newState;
    }
    public void OnClickSelectShapeButton(bool newState)
    {
        if(gameController==null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        shapeButtonObjects[0].GetComponent<Toggle>().isOn = newState;

        if(newState)
        {
     //       if (!gameController.isFirstTime)
     //       {
     //           if (gameController.shouldShowDressResetWarning && newState)
     //           {
					//if (gameController.selectDressController.isWearingDress || gameController.selectDressController.isWearingWig || gameController.selectDressController.isWearingOrnament || gameController.selectDressController.isWearingShoe) {
					//	gameController.popupController.ShowPopup(3, null);
					//	gameController.shouldShowDressResetWarning = false;
					//	//OnClickSelectBodyToneButton(true);
					//}

     //           }
     //       }
            gameController.ToggleOptionSideMenu(2);
            ToggleSideMenuSelectShape(0, 2);
            ToggleSideMenuSelectShape(1, 2);
            previousButtonObject.SetActive(true);
            nextButtonObject.SetActive(true);
            rotationControllerObject.SetActive(true);
            gameController.ZoomOutFemaleModel();
            gameController.DeActiveSceneEditorController();

            if (gameController.autoAcceptChange)
            {
                gameController.HideFemaleModelAndAppearings();
                acceptDiscardPanelForBodyTone.SetActive(false);
                acceptDiscardPanelForEyeColor.SetActive(false);
                acceptDiscardPanelForBodyShape.SetActive(false);
            }
            else
            {
                gameController.HideFemaleModelAndAppearings();
                acceptDiscardPanelForBodyTone.SetActive(false);
                acceptDiscardPanelForEyeColor.SetActive(false);
                acceptDiscardPanelForBodyShape.SetActive(true);
            }

            rotationController.ShowAllShapes(true);
            for (int i = 0; i < shapeButtonObjects.Length; i++)
            {
                shapeButtonObjects[i].transform.GetChild(0).gameObject.SetActive(false);
            }
            shapeButtonObjects[0].transform.GetChild(0).gameObject.SetActive(true);
            //PlayerPrefs.SetInt("selectedBodyShape", 1);

//			shapeButtonObjects[1].GetComponent<Toggle>().isOn = false;
//			shapeButtonObjects[2].GetComponent<Toggle>().isOn = false;

			shapeButtonObjects[1].GetComponent<Toggle>().isOn = false;
			shapeButtonObjects[2].GetComponent<Toggle>().isOn = false;
            
        }
        else
        {
            gameController.ActiveSceneEditorController();
        }
		shapeButtonObjects[0].GetComponent<Toggle>().isOn = newState;

    }

    public void OnClickSelectBodyToneButton(bool newState)
    {

        if (gameController == null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }

       
        shapeButtonObjects[1].GetComponent<Toggle>().isOn = newState;
        
        previousButtonObject.SetActive(false);
        nextButtonObject.SetActive(false);
        gameController.ToggleOptionSideMenu(2);
        ToggleSideMenuSelectShape(1, 2);
        ToggleSideMenuSelectShape(0, 0, shapeButtonObjects[1]);


        if(newState)
        {
            if (gameController.isUsingCustomFace)
            {
                gameController.faceRawImage.transform.parent.gameObject.SetActive(true);
                //gameController.faceRawImage2.transform.parent.gameObject.SetActive(true);
            }
            else
            {
                gameController.faceRawImage.transform.parent.gameObject.SetActive(false);
                //gameController.faceRawImage2.transform.parent.gameObject.SetActive(true);
            }
            if(gameController.isUsingCustomFace2)
            {
                //gameController.faceRawImage.transform.parent.gameObject.SetActive(true);
                gameController.faceRawImage2.transform.parent.gameObject.SetActive(true);
            }
            else
            {
                //gameController.faceRawImage.transform.parent.gameObject.SetActive(true);
                gameController.faceRawImage2.transform.parent.gameObject.SetActive(false);
            }

            //			shapeButtonObjects[0].GetComponent<Toggle>().isOn = false;
            //			shapeButtonObjects[1].GetComponent<Toggle>().isOn = false;


            gameController.ZoomInFemaleModel();
            bodyToneColorPicker.ActivateColorSlider(false);
			shapeButtonObjects[0].GetComponent<Toggle>().isOn = false;
			shapeButtonObjects[2].GetComponent<Toggle>().isOn = false;

        }
        else
        {
            bodyToneColorPicker.DeactivateColorSlider();
        }
		shapeButtonObjects[1].GetComponent<Toggle>().isOn = newState;

        if (gameController.autoAcceptChange)
        {
            rotationController.ShowAllShapes(false);
            gameController.ShowFemaleModelAndAppearings();
        }
        else
        {
            //rotationController.ShowAllShapes(true);
            //rotationController.HideUnSelectedShapes(true);

            rotationController.ShowAllShapes(false);
            gameController.ShowFemaleModelAndAppearings();

        }

        rotationControllerObject.SetActive(false);
        for (int i = 0; i < shapeButtonObjects.Length; i++)
        {
            shapeButtonObjects[i].transform.GetChild(0).gameObject.SetActive(false);
        }
        shapeButtonObjects[1].transform.GetChild(0).gameObject.SetActive(true);
        
        //PlayerPrefs.SetInt("selectedBodyTone", 1);
        acceptDiscardPanelForBodyShape.SetActive(false);
        acceptDiscardPanelForEyeColor.SetActive(false);
        //acceptDiscardPanelForBodyTone.SetActive(true);
        acceptDiscardPanelForBodyTone.SetActive(!gameController.autoAcceptChange);

    }

    public void OnClickSelectEyeButton(bool newState)
    {
        if (gameController == null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        print("New state is " + newState);
        if (newState)
        {
            gameController.faceRawImage.transform.parent.gameObject.SetActive(false);
            gameController.faceRawImage2.transform.parent.gameObject.SetActive(false);

            //femaleSprite.sprite = rotationController.GetSelectedShape().GetComponent<SpriteRenderer>().sprite;
            ToggleSideMenuSelectShape(0, 2);
            ToggleSideMenuSelectShape(1, 1, shapeButtonObjects[2]);
            if(!gameController.femaleModelAndAllAppearingsAreShown)
            {
                gameController.ShowFemaleModelAndAppearings();
            }
            gameController.ZoomInFemaleModel();

			shapeButtonObjects[0].GetComponent<Toggle>().isOn = false;
			shapeButtonObjects[1].GetComponent<Toggle>().isOn = false;

        }
        else
        {
            ToggleSideMenuSelectShape(1, 2, shapeButtonObjects[2]);
            //gameController.ZoomOutFemaleModel();
        }

        //faceCloseup.gameObject.SetActive(newState);

        shapeButtonObjects[2].GetComponent<Toggle>().isOn = newState;

        previousButtonObject.SetActive(false);
        nextButtonObject.SetActive(false);
        gameController.ToggleOptionSideMenu(2);
        


        if (gameController.autoAcceptChange)
        {
            rotationController.ShowAllShapes(false);
            //gameController.ShowFemaleModelAndAppearings();
            //gameController.HideFemaleModelAndAppearings();
            //gameController.ZoomInFemaleModel();
        }
        else
        {
            //rotationController.ShowAllShapes(false);
            //rotationController.HideUnSelectedShapes(true);

            rotationController.ShowAllShapes(false);
            //gameController.ShowFemaleModelAndAppearings();
            //gameController.HideFemaleModelAndAppearings();
            //gameController.ZoomInFemaleModel();
        }

        rotationControllerObject.SetActive(false);
        for (int i = 0; i < shapeButtonObjects.Length; i++)
        {
            shapeButtonObjects[i].transform.GetChild(0).gameObject.SetActive(false);
        }
        shapeButtonObjects[2].transform.GetChild(0).gameObject.SetActive(true);
        //PlayerPrefs.SetInt("selectedEyeColor", 1);
        acceptDiscardPanelForBodyShape.SetActive(false);
        acceptDiscardPanelForBodyTone.SetActive(false);
        //acceptDiscardPanelForEyeColor.SetActive(true);
        acceptDiscardPanelForEyeColor.SetActive(!gameController.autoAcceptChange);
    }
    public void ToggleSideMenuSelectShape(int sideMenuIndex=0,int showCode = 0,GameObject btnGameObject=null)
    {
        if(btnGameObject!=null)
        {
            gameController.SetCurrentActive(sideMenus[sideMenuIndex], btnGameObject);
        }
        if (showCode == 1)
        {
            
            for(int i=0;i<sideMenus.Length;i++)
            {
                if(i==sideMenuIndex)
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
                    if(sideMenus[i].GetComponent<RectTransform>().anchoredPosition.x<0f)
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


    public void OnAcceptChanges()
    {
        gameController.ChangeShape(rotationController.GetSelectedShape());
    }


    public void ChangeBodyTone(string toneColor)
    {
        
        
        
        try
        {
            SetModel(rotationController.GetSelectedShape(), toneColor,GetCarouselSelectedEyeColor());
            if (gameController.autoAcceptChange)
            {
                gameController.AcceptBodyToneChange(true);
            }
            else
            {
                gameController.AcceptBodyToneChange(true);
            }
        }
        catch
        {

        }
        //Texture2D tmptex = Resources.Load()
        
        

        

        //gameController.EnableAllButtons();
    }

    public void ChangeEyeColor(string eyeColor)
    {



        try
        {
            SetModel(rotationController.GetSelectedShape(), GetCarouselSelectedBodyTone(),eyeColor);
            if(gameController.autoAcceptChange)
            {
                gameController.AcceptEyeColorChange(true);
            }
            else
            {
                gameController.AcceptEyeColorChange(true);
            }

            //femaleSprite.sprite = rotationController.GetSelectedShape().GetComponent<SpriteRenderer>().sprite;
        }
        catch
        {
            print("hey error");
        }
        //Texture2D tmptex = Resources.Load()





        //gameController.EnableAllButtons();
    }

    public void SetModel(GameObject model,string toneColor,string eyeColor="black")
    {
        try
        {
            SpriteRenderer shapeTex = model.GetComponent<SpriteRenderer>();
            string modelName = shapeTex.sprite.texture.name.Split('_')[0];
            string fileName = "images/models/female/" + modelName + "/" +toneColor+"/"+ modelName + "_" + toneColor+"_"+eyeColor;
            print(fileName);
            Texture2D tmptex = Resources.Load(fileName) as Texture2D;
            shapeTex.sprite = Sprite.Create(tmptex, new Rect(0, 0, tmptex.width, tmptex.height), new Vector2(0.5f, 0.5f), 100f);
            rotationController.CheckForBodyToneChange();
        }
        catch (System.Exception e)
        {
            print("error setting model : "+e);
        }
    }

    public IEnumerator ResetAllModels(bool reallyAll = false)
    {
        for (int i = 0; i < rotationController.shapes.Length; i++)
        {
            if (i != rotationController.GetSelectedShapeIndex() || reallyAll)
            {
                SetModel(rotationController.shapes[i], rotationController.bodyToneColors[0]);
            }
        }
        yield return null;
    }

    public IEnumerator ResetModel(GameObject model)
    {
        
                SetModel(model, rotationController.bodyToneColors[0]);
           
        yield return null;
    }
}
