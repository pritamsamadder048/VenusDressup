using Lean.Touch;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

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
        HideFaceEditPanel();

        


        if (gameController.currentlyUsingFace == 1)
        {
            if (gameController.currentlySelectedFace == 1)
            {
                gameController.UsingCustomFace(true);
                gameController.ShowFaceImage(true);
            }
            else if (gameController.currentlySelectedFace == 2)
            {
                gameController.UsingCustomFace2(true);
                gameController.ShowFaceImage2(true);
            }
        }
        else if (gameController.currentlyUsingFace == 2)
        {


            gameController.UsingCustomFace(true);
            gameController.ShowFaceImage(true);

            gameController.UsingCustomFace2(true);
            gameController.ShowFaceImage2(true);

        }

        croppedFaceObject.SetActive(true);

        

        if (gameController.currentlySelectedFace == 1)
        {
            if(!gameController.isLoadedFace)
            {
                gameController.saveDict["face1"] = true;
            }
            else
            {
                gameController.saveDict["face1"] = false;
            }
            gameController.currentlySelectedFace = 2;
        }
        else if (gameController.currentlySelectedFace == 2)
        {
            if(!gameController.isLoadedFace2)
            {
                gameController.saveDict["face2"] = true;
            }
            else
            {
                gameController.saveDict["face2"] = false;
            }
            gameController.currentlySelectedFace = 1;
        }

        
        //gameController.faceIsSaved = false;
    }



    public void DiscardFaceEdit()
    {
        //gameController.currentlyUsingFace -= 1;

        //if(gameController.currentlyUsingFace<0)
        //{
        //    gameController.currentlyUsingFace = 0;
        //}

            if (gameController.currentlySelectedFace == 1)
            {
            gameController.saveDict["face1"] = false;
            
            gameController.RemoveFace1();
            }
            else if (gameController.currentlySelectedFace == 2)
            {
            gameController.saveDict["face2"] = false;
            
            gameController.RemoveFace2();
            }
        
        

        HideFaceEditPanel();
        
    }

    public void ShowFaceEditPAnel()
    {
        
        if(gameController.currentlySelectedFace==1)
        {
            faceLeanScale = croppedFace.GetComponent<LeanCustomScale>();
            faceLeanRotate = croppedFace.GetComponent<LeanRotate>();
            croppedFaceController = croppedFace.GetComponent<CroppedFaceController>();
            faceParent = croppedFace.transform.parent.gameObject;
            croppedFaceObject = croppedFace.gameObject;
            if(!gameController.isLoadedFace)
            {
                gameController.saveDict["face1"] = true;
            }
        }
        else if(gameController.currentlySelectedFace==2)
        {
            faceLeanScale = croppedFace2.GetComponent<LeanCustomScale>();
            faceLeanRotate = croppedFace2.GetComponent<LeanRotate>();
            croppedFaceController = croppedFace2.GetComponent<CroppedFaceController>();
            faceParent = croppedFace2.transform.parent.gameObject;
            croppedFaceObject = croppedFace2.gameObject;
            if (!gameController.isLoadedFace2)
            {
                gameController.saveDict["face2"] = true;
            }
        }
       
        gameController.ToggleHomeSideMenu(2);
        wigParent.SetActive(false);
        maleWigParent.SetActive(false);
        homeButton.SetActive(false);
        menuButton.SetActive(false);
        saveButton.SetActive(false);

        faceLeanScale.enabled = true;
        faceLeanRotate.enabled = true;
        croppedFaceController.enabled = true;
        faceParent.GetComponent<UILineRenderer>().enabled = true;
        //croppedFaceBoundary.SetActive(true);
        faceEditPanel.SetActive(true);
        sceneEditorControllerObj.SetActive(false);

    }

    public void HideFaceEditPanel()
    {
        faceLeanScale.enabled = false;
        faceLeanRotate.enabled = false;
        croppedFaceController.enabled = false;
        //croppedFaceBoundary.SetActive(false);


        faceEditPanel.SetActive(false);
        wigParent.SetActive(true);
        maleWigParent.SetActive(true);
        homeButton.SetActive(true);
        menuButton.SetActive(true);
        saveButton.SetActive(true);
        faceParent.GetComponent<UILineRenderer>().enabled = false;
        gameController.GoToHome();
        sceneEditorControllerObj.SetActive(true);
        
    }
}
