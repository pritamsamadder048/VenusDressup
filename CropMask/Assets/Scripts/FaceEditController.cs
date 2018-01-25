using System;
using Lean.Touch;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using DG.Tweening;

public class FaceEditController : MonoBehaviour {


    
    public GameObject wigParent;
    public GameObject maleWigParent;
    [SerializeField]
    private GameObject homeButton;
    [SerializeField]
    private GameObject menuButton;
    [SerializeField]
    private GameObject saveButton;
    [SerializeField]
    private GameObject faceEditPanel;
    [SerializeField]
    GameObject gameControllerObj;
    private GameController gameController;

    
    public GameObject croppedFace;
    
    public GameObject croppedFaceBoundary;

    LeanCustomScale faceLeanScale;
    LeanRotate faceLeanRotate;
    CroppedFaceController croppedFaceController;

    public GameObject croppedFace2;

    public GameObject croppedFaceBoundary2;



    public GameObject sceneEditorControllerObj;
    public SceneEditorController sceneEditorController;









    public GameObject croppedFaceObject;



    public GameObject faceParent;


    public GameObject[] faceEditPanelButtons;

    public GameObject[] editSidePanels;

    public GameObject[] faceEditUndoButtons;

    public bool backIsImageProcessingScreen = false;

    public bool isShowingFaceBrightnessPanel = true;
    public bool isShowingFaceColorPanel = false;
    public bool isShowingFaceSaturationPanel = false;
    public bool onlyEditing = false;

    // Use this for initialization
    void Start () {
        gameController = gameControllerObj.GetComponent<GameController>();

        
        sceneEditorController = sceneEditorControllerObj.GetComponent<SceneEditorController>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void AcceptFaceEdit()
    {
        if(backIsImageProcessingScreen)
        {
            if (gameController.currentlySelectedFace == 1)
            {
                gameController.saveDict["face1"] = true;
                gameController.isLoadedFace = false;
                gameController.loadedFaceIndex = -1;
                gameController.faceHash = 0;

                gameController.currentFaceBrightness = gameController.faceRawImage.color.b;
                gameController.currentFaceColor = gameController.faceRawImage.color.r;
                gameController.currentFaceSaturation = gameController.faceRawImage.color.g;
                gameController.previouslyUsingFace1 = true;

            }
            else if (!gameController.isShowingMale)
            {
                gameController.currentlySelectedFace = 1;
                gameController.saveDict["face1"] = true;
                gameController.isLoadedFace = false;
                gameController.loadedFaceIndex = -1;
                gameController.faceHash = 0;

                gameController.currentFaceBrightness = gameController.faceRawImage.color.b;
                gameController.currentFaceColor = gameController.faceRawImage.color.r;
                gameController.currentFaceSaturation = gameController.faceRawImage.color.g;
                gameController.previouslyUsingFace1 = true;

            }
            else if (gameController.currentlySelectedFace == 2)
            {
                gameController.saveDict["face2"] = true;
                gameController.isLoadedFace2 = false;
                gameController.loadedFaceIndex2 = -1;
                gameController.faceHash2 = 0;

                gameController.currentFaceBrightness2 = gameController.faceRawImage2.color.b;
                gameController.currentFaceColor2 = gameController.faceRawImage2.color.r;
                gameController.currentFaceSaturation2 = gameController.faceRawImage2.color.g;
                gameController.previouslyUsingFace2 = true;


            }
        }
        else
        {
            if (gameController.currentlySelectedFace == 1)
            {
                
                gameController.previouslyUsingFace1 = true;

            }
            else if (!gameController.isShowingMale)
            {
                
                gameController.previouslyUsingFace1 = true;

            }
            else if (gameController.currentlySelectedFace == 2)
            {
                
                gameController.previouslyUsingFace2 = true;


            }
        }

        
        backIsImageProcessingScreen = false;
        HideFaceEditPanel();




        





        DestroyImmediate(gameController.processingImage.sprite);
        gameController.CollectGurbage(true);
        
    }



    public void DeleteFace()
    {
        if(gameController.currentlySelectedFace==1)
        {
            gameController.RemoveFace1();
            
        }
        else if(gameController.currentlySelectedFace==2)
        {
            gameController.RemoveFace2();
        }
        DiscardFaceEdit();
    }


    public void DiscardFaceEdit()
    {
        //gameController.currentlyUsingFace -= 1;

        //if(gameController.currentlyUsingFace<0)
        //{
        //    gameController.currentlyUsingFace = 0;
        //}

        if(!onlyEditing)
        {
            if (gameController.currentlySelectedFace == 1)
            {
                if(!gameController.previouslyUsingFace1)
                {
                    gameController.saveDict["face1"] = false;

                    gameController.RemoveFace1();
                }
                else
                {
                    if(gameController.previousFaceIndex==1)
                    {
                        gameController.faceRawImage.texture = new Texture2D(gameController.previousImagetexture.width, gameController.previousImagetexture.height) as Texture;
                        (gameController.faceRawImage.texture as Texture2D).SetPixels(gameController.previousImagetexture.GetPixels());
                        (gameController.faceRawImage.texture as Texture2D).Apply();
                        gameController.faceRawImage.color = gameController.previousColor;
                        gameController.faceRawImage.transform.localScale = gameController.previousScale;
                        gameController.faceRawImage.rectTransform.anchoredPosition3D = gameController.previousPosition;
                        gameController.faceRawImage.transform.localEulerAngles = gameController.previousRotation;
                        gameController.faceRawImage.rectTransform.sizeDelta = gameController.previousSizeDelta;
                        if(gameController.previouslyLoaded)
                        {
                            gameController.isLoadedFace = gameController.previouslyLoaded;
                            gameController.loadedFaceIndex = gameController.previousLoadedFaceIndex;
                            gameController.faceHash = gameController.previousFaceHash;
                        }
                        gameController.saveDict["face1"] = false;
                    }
                    //else if(gameController.previousFaceIndex == 2)
                    //{
                        
                    //}
                }
            }
            else if (gameController.currentlySelectedFace == 2)
            {
                if(!gameController.previouslyUsingFace2)
                {
                    gameController.saveDict["face2"] = false;

                    gameController.RemoveFace2();
                }
                else
                {
                    if (gameController.previousFaceIndex == 2)
                    {
                        gameController.faceRawImage2.texture = new Texture2D(gameController.previousImagetexture.width, gameController.previousImagetexture.height) as Texture;
                        (gameController.faceRawImage2.texture as Texture2D).SetPixels(gameController.previousImagetexture.GetPixels());
                        (gameController.faceRawImage2.texture as Texture2D).Apply();
                        gameController.faceRawImage2.color = gameController.previousColor;
                        gameController.faceRawImage2.transform.localScale = gameController.previousScale;
                        gameController.faceRawImage2.rectTransform.anchoredPosition3D = gameController.previousPosition;
                        gameController.faceRawImage2.transform.localEulerAngles = gameController.previousRotation;
                        gameController.faceRawImage2.rectTransform.sizeDelta = gameController.previousSizeDelta;

                        if (gameController.previouslyLoaded)
                        {
                            gameController.isLoadedFace2 = gameController.previouslyLoaded;
                            gameController.loadedFaceIndex2 = gameController.previousLoadedFaceIndex;
                            gameController.faceHash2 = gameController.previousFaceHash;
                        }
                        gameController.saveDict["face2"] = false;
                    }
                }
            }
            if (!gameController.isShowingMale)
            {
               if(!gameController.previouslyUsingFace1)
                {
                    gameController.saveDict["face1"] = false;

                    gameController.RemoveFace1();
                }
                else
                {
                    if (gameController.previousFaceIndex == 1)
                    {
                        gameController.faceRawImage.texture = new Texture2D(gameController.previousImagetexture.width, gameController.previousImagetexture.height) as Texture;
                        (gameController.faceRawImage.texture as Texture2D).SetPixels(gameController.previousImagetexture.GetPixels());
                        (gameController.faceRawImage.texture as Texture2D).Apply();
                        gameController.faceRawImage.color = gameController.previousColor;
                        gameController.faceRawImage.transform.localScale = gameController.previousScale;
                        gameController.faceRawImage.rectTransform.anchoredPosition3D = gameController.previousPosition;
                        gameController.faceRawImage.transform.localEulerAngles = gameController.previousRotation;
                        gameController.faceRawImage.rectTransform.sizeDelta = gameController.previousSizeDelta;

                        if (gameController.previouslyLoaded)
                        {
                            gameController.isLoadedFace = gameController.previouslyLoaded;
                            gameController.loadedFaceIndex = gameController.previousLoadedFaceIndex;
                            gameController.faceHash = gameController.previousFaceHash;
                        }
                        gameController.saveDict["face1"] = false;
                    }
                    //else if(gameController.previousFaceIndex == 2)
                    //{

                    //}
                }
            }
        }
        else
        {
            UndoFaceBrightnessEdit();
            UndoFaceColorEdit();
            UndoFaceSaturationEdit();
        }

        
        print(string.Format("before gameController.saveDict[face1] : {0} gameController.saveDict[face2] : {1}", gameController.saveDict["face1"], gameController.saveDict["face2"]));
        
        HideFaceEditPanel(true & !onlyEditing);
        onlyEditing = false;
        print(string.Format("after gameController.saveDict[face1] : {0} gameController.saveDict[face2] : {1}", gameController.saveDict["face1"], gameController.saveDict["face2"]));
    }

    public void ActivateTouchOnCroppedFace()
    {
        if (gameController.currentlySelectedFace == 1)
        {
            faceLeanScale = croppedFace.GetComponent<LeanCustomScale>();
            faceLeanRotate = croppedFace.GetComponent<LeanRotate>();
            croppedFaceController = croppedFace.GetComponent<CroppedFaceController>();
            faceParent = croppedFace.transform.parent.gameObject;
            croppedFaceObject = croppedFace.gameObject;
            if (!gameController.isLoadedFace)
            {
                //gameController.saveDict["face1"] = true;
            }
        }
        else if (gameController.currentlySelectedFace == 2)
        {
            faceLeanScale = croppedFace2.GetComponent<LeanCustomScale>();
            faceLeanRotate = croppedFace2.GetComponent<LeanRotate>();
            croppedFaceController = croppedFace2.GetComponent<CroppedFaceController>();
            faceParent = croppedFace2.transform.parent.gameObject;
            croppedFaceObject = croppedFace2.gameObject;
            if (!gameController.isLoadedFace2)
            {
                //gameController.saveDict["face2"] = true;
            }
        }


        faceLeanScale.enabled = true;
        faceLeanRotate.enabled = true;
        croppedFaceController.enabled = true;
        faceParent.GetComponent<UILineRenderer>().enabled = true;
    }

    public void ActivateTouchOnEditingCroppedFace()
    {
        if (gameController.currentlySelectedFace == 1)
        {
            faceLeanScale = croppedFace.GetComponent<LeanCustomScale>();
            faceLeanRotate = croppedFace.GetComponent<LeanRotate>();
            croppedFaceController = croppedFace.GetComponent<CroppedFaceController>();
            faceParent = croppedFace.transform.parent.gameObject;
            croppedFaceObject = croppedFace.gameObject;
            //if (!gameController.isLoadedFace)
            //{
            //    gameController.saveDict["face1"] = true;
            //}
        }
        else if (gameController.currentlySelectedFace == 2)
        {
            faceLeanScale = croppedFace2.GetComponent<LeanCustomScale>();
            faceLeanRotate = croppedFace2.GetComponent<LeanRotate>();
            croppedFaceController = croppedFace2.GetComponent<CroppedFaceController>();
            faceParent = croppedFace2.transform.parent.gameObject;
            croppedFaceObject = croppedFace2.gameObject;
            //if (!gameController.isLoadedFace2)
            //{
            //    gameController.saveDict["face2"] = true;
            //}
        }


        faceLeanScale.enabled = true;
        faceLeanRotate.enabled = true;
        croppedFaceController.enabled = true;
        faceParent.GetComponent<UILineRenderer>().enabled = true;
    }

    public void ShowFaceEditPAnel(bool backIsImageProcessingScreen=false)
    {
        this.backIsImageProcessingScreen = backIsImageProcessingScreen;
        gameController.ShowMaleFemaleSelectionPopup();
        
       
        gameController.ToggleHomeSideMenu(2);
        wigParent.SetActive(false);
        maleWigParent.SetActive(false);
        homeButton.SetActive(false);
        menuButton.SetActive(false);
        saveButton.SetActive(false);

        
        if (gameController.isShowingMale)
        {
            //faceEditPanelButtons[2].SetActive(true);
            
        }
        else
        {
            //faceEditPanelButtons[2].SetActive(false);
        }
        //faceEditPanelButtons[2].SetActive(false); // hide toggle button

        faceEditPanel.SetActive(true);
        sceneEditorControllerObj.SetActive(false);

    }

    public void EditSelectedFace(int selectedFace)
    {
        this.backIsImageProcessingScreen = false;
        onlyEditing = true;
        gameController.ToggleHomeSideMenu(2);
        wigParent.SetActive(false);
        maleWigParent.SetActive(false);
        homeButton.SetActive(false);
        menuButton.SetActive(false);
        saveButton.SetActive(false);


        if (gameController.isShowingMale)
        {
            gameController.currentlySelectedFace = selectedFace;
            if (selectedFace==1)
            {
                if (gameController.faceRawImage.transform.parent.parent == gameController.femaleModelImageObject.transform)
                {

                    gameController.ZoomInFemaleModel();


                    
                    gameController.faceColorController.SetTarget(gameController.faceRawImage);
                    gameController.faceBrightnessController.SetTarget(gameController.faceRawImage);
                    gameController.faceSaturationController.SetTarget(gameController.faceRawImage);
                    

                }
                else if(gameController.faceRawImage.transform.parent.parent == gameController.maleImage.transform)
                {
                    gameController.ZoomInMaleModel();



                    gameController.faceColorController.SetTarget(gameController.faceRawImage);
                    gameController.faceBrightnessController.SetTarget(gameController.faceRawImage);
                    gameController.faceSaturationController.SetTarget(gameController.faceRawImage);
                }
            }
            else if(selectedFace==2)
            {
                if (gameController.faceRawImage2.transform.parent.parent == gameController.femaleModelImageObject.transform)
                {

                    gameController.ZoomInFemaleModel();



                    gameController.faceColorController.SetTarget(gameController.faceRawImage2);
                    gameController.faceBrightnessController.SetTarget(gameController.faceRawImage2);
                    gameController.faceSaturationController.SetTarget(gameController.faceRawImage2);


                }
                else if (gameController.faceRawImage2.transform.parent.parent == gameController.maleImage.transform)
                {
                    gameController.ZoomInMaleModel();



                    gameController.faceColorController.SetTarget(gameController.faceRawImage2);
                    gameController.faceBrightnessController.SetTarget(gameController.faceRawImage2);
                    gameController.faceSaturationController.SetTarget(gameController.faceRawImage2);
                }
            }

        }
        else
        {
            gameController.currentlySelectedFace = selectedFace;
            gameController.ZoomInFemaleModel();
            

            if(selectedFace==1)
            {
                gameController.faceColorController.SetTarget(gameController.faceRawImage);
                gameController.faceBrightnessController.SetTarget(gameController.faceRawImage);
                gameController.faceSaturationController.SetTarget(gameController.faceRawImage);
            }

            else if(selectedFace==2)
            {
                gameController.faceColorController.SetTarget(gameController.faceRawImage2);
                gameController.faceBrightnessController.SetTarget(gameController.faceRawImage2);
                gameController.faceSaturationController.SetTarget(gameController.faceRawImage2);
            }
        }


        faceEditPanel.SetActive(true);
        sceneEditorControllerObj.SetActive(false);
        ActivateTouchOnEditingCroppedFace();
    }




    public void ShowFaceEditPAnel(Texture2D faceTexture, Vector3 scale, Vector3 rotation, int imageIndex, int faceHash,Color col, bool backIsImageProcessingScreen)
    {
        this.backIsImageProcessingScreen = backIsImageProcessingScreen;
        gameController.ShowMaleFemaleSelectionPopup(faceTexture,scale,rotation,imageIndex,faceHash,col, backIsImageProcessingScreen);


        gameController.ToggleHomeSideMenu(2);
        wigParent.SetActive(false);
        maleWigParent.SetActive(false);
        homeButton.SetActive(false);
        menuButton.SetActive(false);
        saveButton.SetActive(false);


        if (gameController.isShowingMale)
        {
            //faceEditPanelButtons[2].SetActive(true);

        }
        else
        {
            //faceEditPanelButtons[2].SetActive(false);
        }
        //faceEditPanelButtons[2].SetActive(false); // hide toggle button

        faceEditPanel.SetActive(true);
        sceneEditorControllerObj.SetActive(false);
    }

    public void HideFaceEditPanel(bool discarding=false)
    {
        ActivateTouchOnCroppedFace();
        faceLeanScale.enabled = false;
        faceLeanRotate.enabled = false;
        croppedFaceController.enabled = false;
        //croppedFaceBoundary.SetActive(false);


        faceEditPanel.SetActive(false);

        if(!backIsImageProcessingScreen)
        {
            wigParent.SetActive(true);
            maleWigParent.SetActive(true);
            homeButton.SetActive(true);
            menuButton.SetActive(true);
            saveButton.SetActive(true);
            faceParent.GetComponent<UILineRenderer>().enabled = false;
            
            gameController.ZoomOutFemaleModel();
            gameController.ZoomOutMaleModel();
            gameController.GoToHome();
            sceneEditorControllerObj.SetActive(true);
        }

        else
        {
            wigParent.SetActive(true);
            maleWigParent.SetActive(true);
            homeButton.SetActive(true);
            menuButton.SetActive(true);
            saveButton.SetActive(true);
            faceParent.GetComponent<UILineRenderer>().enabled = false;

            gameController.ZoomOutFemaleModel();
            gameController.ZoomOutMaleModel();
            gameController.galleryController.GoToImageCrop();
        }
        
        if(discarding)
        {
            //if (gameController.currentlySelectedFace == 1)
            //{
            //    gameController.saveDict["face1"] = false;

            //    gameController.RemoveFace1();
            //}
            //else if (gameController.currentlySelectedFace == 2)
            //{
            //    gameController.saveDict["face2"] = false;

            //    gameController.RemoveFace2();
            //}
            //if (!gameController.isShowingMale)
            //{
            //    gameController.saveDict["face1"] = false;

            //    gameController.RemoveFace1();
            //}
        }
    }



    public void ToggleFaceColorPanel(int showStatus=0)
    {
        switch (showStatus)
        {
            case 0:
            default:
                {
                    if (isShowingFaceColorPanel)
                    {
                        editSidePanels[1].GetComponent<RectTransform>().DOAnchorPosX(-800f, .3f);
                        faceEditPanelButtons[3].transform.GetChild(0).gameObject.SetActive(false);
                        faceEditUndoButtons[1].SetActive(false);
                        isShowingFaceColorPanel = false;
                    }
                    else
                    {
                        ToggleFaceBrightnessPanel(2);
                        ToggleFaceSaturationPanel(2);
                        editSidePanels[1].GetComponent<RectTransform>().DOAnchorPosX(0f, .3f);
                        faceEditPanelButtons[3].transform.GetChild(0).gameObject.SetActive(true);
                        faceEditUndoButtons[1].SetActive(true);
                        isShowingFaceColorPanel = true;
                    }
                    break;
                }
            case 1:
                {
                    ToggleFaceBrightnessPanel(2);
                    ToggleFaceSaturationPanel(2);

                    editSidePanels[1].GetComponent<RectTransform>().DOAnchorPosX(0f, .3f);
                    faceEditPanelButtons[3].transform.GetChild(0).gameObject.SetActive(true);
                    faceEditUndoButtons[1].SetActive(true);
                    isShowingFaceColorPanel = true;
                    break;
                }
            case 2:
                {
                    editSidePanels[1].GetComponent<RectTransform>().DOAnchorPosX(-800f, .3f);
                    faceEditPanelButtons[3].transform.GetChild(0).gameObject.SetActive(false);
                    faceEditUndoButtons[1].SetActive(false);
                    isShowingFaceColorPanel = false;
                    break;
                }
        }
    }




    public void ToggleFaceSaturationPanel(int showStatus = 0)
    {
        switch (showStatus)
        {
            case 0:
            default:
                {
                    if (isShowingFaceSaturationPanel)
                    {
                        editSidePanels[2].GetComponent<RectTransform>().DOAnchorPosX(-800f, .3f);
                        faceEditPanelButtons[4].transform.GetChild(0).gameObject.SetActive(false);
                        faceEditUndoButtons[2].SetActive(false);
                        isShowingFaceSaturationPanel = false;
                    }
                    else
                    {
                        ToggleFaceBrightnessPanel(2);
                        ToggleFaceColorPanel(2);
                        editSidePanels[2].GetComponent<RectTransform>().DOAnchorPosX(0f, .3f);
                        faceEditPanelButtons[4].transform.GetChild(0).gameObject.SetActive(true);
                        faceEditUndoButtons[2].SetActive(true);
                        isShowingFaceSaturationPanel = true;
                    }
                    break;
                }
            case 1:
                {
                    ToggleFaceBrightnessPanel(2);
                    ToggleFaceColorPanel(2);

                    editSidePanels[2].GetComponent<RectTransform>().DOAnchorPosX(0f, .3f);
                    faceEditPanelButtons[4].transform.GetChild(0).gameObject.SetActive(true);
                    faceEditUndoButtons[2].SetActive(true);
                    isShowingFaceSaturationPanel = true;
                    break;
                }
            case 2:
                {
                    editSidePanels[2].GetComponent<RectTransform>().DOAnchorPosX(-800f, .3f);
                    faceEditPanelButtons[4].transform.GetChild(0).gameObject.SetActive(false);
                    faceEditUndoButtons[2].SetActive(false);
                    isShowingFaceSaturationPanel = false;
                    break;
                }
        }
    }





    public void ToggleFaceBrightnessPanel(int showStatus = 0)
    {
        switch (showStatus)
        {
            case 0:
            default:
                {
                    if(isShowingFaceBrightnessPanel)
                    {
                        editSidePanels[0].GetComponent<RectTransform>().DOAnchorPosX(-800f, .3f);
                        faceEditPanelButtons[2].transform.GetChild(0).gameObject.SetActive(false);
                        faceEditUndoButtons[0].SetActive(false);
                        isShowingFaceBrightnessPanel = false;
                    }
                    else
                    {
                        ToggleFaceColorPanel(2);
                        ToggleFaceSaturationPanel(2);

                        editSidePanels[0].GetComponent<RectTransform>().DOAnchorPosX(0f, .3f);
                        faceEditPanelButtons[2].transform.GetChild(0).gameObject.SetActive(true);
                        faceEditUndoButtons[0].SetActive(true);
                        isShowingFaceBrightnessPanel = true;
                    }
                    break;
                }
            case 1:
                {
                    ToggleFaceColorPanel(2);
                    ToggleFaceSaturationPanel(2);

                    editSidePanels[0].GetComponent<RectTransform>().DOAnchorPosX(0f,.3f);
                    faceEditPanelButtons[2].transform.GetChild(0).gameObject.SetActive(true);
                    faceEditUndoButtons[0].SetActive(true);
                    isShowingFaceBrightnessPanel = true;
                    break;
                }
            case 2:
                {
                    editSidePanels[0].GetComponent<RectTransform>().DOAnchorPosX(-800f, .3f);
                    faceEditPanelButtons[2].transform.GetChild(0).gameObject.SetActive(false);
                    faceEditUndoButtons[0].SetActive(false);
                    isShowingFaceBrightnessPanel = false;
                    break;
                }
        }
    }

    public void OnClickFaceColorButton(bool newState)
    {
        if(newState)
        {
            ToggleFaceSaturationPanel(2);
            ToggleFaceBrightnessPanel(2);
            ToggleFaceColorPanel(1);
        }
        else
        {
            ToggleFaceColorPanel(2);
        }
    }

    public void OnClickFaceSaturationButton(bool newState)
    {
        if (newState)
        {
            ToggleFaceBrightnessPanel(2);
            ToggleFaceColorPanel(2);
            ToggleFaceSaturationPanel(1);
        }
        else
        {
            ToggleFaceSaturationPanel(2);
        }
    }

    public void OnClickFaceBrightnessButton(bool newState)
    {
        if (newState)
        {
            ToggleFaceColorPanel(2);
            ToggleFaceSaturationPanel(2);
            ToggleFaceBrightnessPanel(1);
        }
        else
        {
            ToggleFaceBrightnessPanel(2);
        }
    }





    public void UndoFaceBrightnessEdit()
    {
        if (gameController.currentlySelectedFace == 1)
        {
            gameController.faceRawImage.color = new Color(gameController.faceRawImage.color.r,  gameController.faceRawImage.color.g, gameController.currentFaceBrightness);

            gameController.faceBrightnessController.DeactivateColorSlider();
            gameController.faceBrightnessController.mainSlider.value = gameController.currentFaceBrightness;
            gameController.faceBrightnessController.ActivateColorSlider(false);
        }
        else if (gameController.currentlySelectedFace == 2)
        {
            gameController.faceRawImage2.color = new Color(gameController.faceRawImage2.color.r, gameController.faceRawImage2.color.g, gameController.currentFaceBrightness2);

            gameController.faceBrightnessController.DeactivateColorSlider();
            gameController.faceBrightnessController.mainSlider.value = gameController.currentFaceBrightness2;
            gameController.faceBrightnessController.ActivateColorSlider(false);
        }

        
    }



    public void UndoFaceColorEdit()
    {
        if(gameController.currentlySelectedFace==1)
        {
            gameController.faceRawImage.color = new Color(gameController.currentFaceColor, gameController.faceRawImage.color.g, gameController.faceRawImage.color.b);

            gameController.faceColorController.DeactivateColorSlider();
            gameController.faceColorController.mainSlider.value = gameController.currentFaceColor;
            gameController.faceColorController.ActivateColorSlider(false);
        }
        else if(gameController.currentlySelectedFace==2)
        {
            gameController.faceRawImage2.color = new Color(gameController.currentFaceColor2, gameController.faceRawImage2.color.g, gameController.faceRawImage2.color.b);

            gameController.faceColorController.DeactivateColorSlider();
            gameController.faceColorController.mainSlider.value = gameController.currentFaceColor2;
            gameController.faceColorController.ActivateColorSlider(false);
        }

        
    }


    public void UndoFaceSaturationEdit()
    {
        if (gameController.currentlySelectedFace == 1)
        {
            gameController.faceRawImage.color = new Color(gameController.faceRawImage.color.r, gameController.currentFaceSaturation, gameController.faceRawImage.color.b);

            gameController.faceSaturationController.DeactivateColorSlider();
            gameController.faceSaturationController.mainSlider.value = gameController.currentFaceSaturation;
            gameController.faceSaturationController.ActivateColorSlider(false);
        }
        else if (gameController.currentlySelectedFace == 2)
        {
            gameController.faceRawImage2.color = new Color(gameController.faceRawImage2.color.r, gameController.currentFaceSaturation2, gameController.faceRawImage2.color.b);

            gameController.faceSaturationController.DeactivateColorSlider();
            gameController.faceSaturationController.mainSlider.value = gameController.currentFaceSaturation;
            gameController.faceSaturationController.ActivateColorSlider(false);
        }

        
    }




    public void ResetDefaultFace()
    {
        if(gameController.currentlySelectedFace==1)
        {
            if(gameController.isLoadedFace)
            {
                gameController.currentFaceBrightness = gameController.loadedCroppedFaceProperty.cfd.faceColor[2];
                gameController.currentFaceColor = gameController.loadedCroppedFaceProperty.cfd.faceColor[1];
                gameController.currentFaceSaturation = gameController.loadedCroppedFaceProperty.cfd.faceColor[1];

                UndoFaceBrightnessEdit();
                UndoFaceColorEdit();
                UndoFaceSaturationEdit();
                UndoMirrorFaceImage();

            }
            else
            {
                gameController.currentFaceBrightness = 0.5f;
                gameController.currentFaceColor = 0.5f;
                gameController.currentFaceSaturation = 0.5f;

                UndoFaceBrightnessEdit();
                UndoFaceColorEdit();
                UndoFaceSaturationEdit();
                UndoMirrorFaceImage();
            }
        }
        else if(gameController.currentlySelectedFace==2)
        {
            if (gameController.isLoadedFace2)
            {
                gameController.currentFaceBrightness2 = gameController.loadedCroppedFaceProperty2.cfd.faceColor[2];
                gameController.currentFaceColor2 = gameController.loadedCroppedFaceProperty2.cfd.faceColor[1];
                gameController.currentFaceSaturation2 = gameController.loadedCroppedFaceProperty2.cfd.faceColor[1];

                UndoFaceBrightnessEdit();
                UndoFaceColorEdit();
                UndoFaceSaturationEdit();
                UndoMirrorFaceImage();
            }
            else
            {
                gameController.currentFaceBrightness2 = 0.5f;
                gameController.currentFaceColor2 = 0.5f;
                gameController.currentFaceSaturation2 = 0.5f;

                UndoFaceBrightnessEdit();
                UndoFaceColorEdit();
                UndoFaceSaturationEdit();
                UndoMirrorFaceImage();
            }
        }
    }




    public void MirrorFaceImage()
    {
        if(gameController.currentlySelectedFace ==1)
        {
            gameController.faceRawImage.transform.localScale = new Vector3(gameController.faceRawImage.transform.localScale.x * -1, gameController.faceRawImage.transform.localScale.y, gameController.faceRawImage.transform.localScale.z);
        }
        else if(gameController.currentlySelectedFace==2)
        {
            gameController.faceRawImage2.transform.localScale = new Vector3(gameController.faceRawImage2.transform.localScale.x * -1, gameController.faceRawImage2.transform.localScale.y, gameController.faceRawImage2.transform.localScale.z);
        }
    }

    public void UndoMirrorFaceImage()
    {
        if (gameController.currentlySelectedFace == 1)
        {
            gameController.faceRawImage.transform.localScale = new Vector3(Mathf.Abs(gameController.faceRawImage.transform.localScale.x), gameController.faceRawImage.transform.localScale.y, gameController.faceRawImage.transform.localScale.z);
        }
        else if (gameController.currentlySelectedFace == 2)
        {
            gameController.faceRawImage2.transform.localScale = new Vector3(Mathf.Abs(gameController.faceRawImage2.transform.localScale.x), gameController.faceRawImage2.transform.localScale.y, gameController.faceRawImage2.transform.localScale.z);
        }
    }
}
