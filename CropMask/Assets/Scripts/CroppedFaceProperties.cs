using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class CroppedFaceProperties : MonoBehaviour {

    public string imageName;
    public int imageIndex = -1;
    
    GameController gameController;
    SaveManager saveManager;
    string dataPath;
    string finalSavePath;

    public GameObject face;
    public GameObject face2;
    // Use this for initialization


    public CroppedFaceData cfd;
    public int faceHash;

    public bool isInitialized = false;

    private void Awake()
    {
        
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        saveManager = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManager>();
        if (gameController != null)
        {
            face = gameController.faceRawImage.gameObject;
            face2 = gameController.faceRawImage2.gameObject;
        }
        GetComponent<Button>().onClick.AddListener(UseThisCroppedFace);
        transform.GetChild(0).GetComponent<Button>().onClick.AddListener(DeleteThisCroppedFace);
        transform.GetChild(1).GetComponent<Button>().onClick.AddListener(RemoveThisFaceFromScene);
    }
    void Start () {
        

        
        
    }

    public void InitFaceProperties(CroppedFaceData c)
    {
        dataPath = Application.persistentDataPath;

        if (Application.platform != RuntimePlatform.OSXPlayer)
        {
            dataPath += "/croppedfaces";
        }
        finalSavePath = Path.Combine(dataPath, "faceimages");
        finalSavePath = Path.Combine(finalSavePath, imageName);
        cfd = c;
        faceHash = cfd.saveFaceHash;
        if(face!=null&&face2!=null)
        {
            isInitialized = true;
        }
        else
        {
            isInitialized = false;
        }

        if(gameController.faceHash==faceHash  || gameController.faceHash2==faceHash)
        {
            ShowRemoveButton(true);
        }
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UseThisCroppedFace()
    {
        print("clicked");
        if(isInitialized)
        {
            if (File.Exists(finalSavePath))
            {

                print("yes file does exist");
                Texture2D t2d = new Texture2D(150, 150);
                t2d.LoadImage(File.ReadAllBytes(finalSavePath));

                
                gameController.currentlyUsingFace += 1;

                if(gameController.currentlyUsingFace>2)
                {
                    gameController.currentlyUsingFace = 2;
                    
                }

                if (gameController.IsUsingCustomFace() && gameController.currentlySelectedFace == 1 && !gameController.IsUsingCustomFace2())
                {
                    gameController.currentlyUsingFace = 1;
                }
                else if (gameController.IsUsingCustomFace2() && gameController.currentlySelectedFace == 2 && !gameController.IsUsingCustomFace())
                {
                    gameController.currentlyUsingFace = 1;
                }
                if (gameController.currentlySelectedFace==1)
                {
                    face.GetComponent<RawImage>().texture = t2d as Texture;
                    face.transform.localScale = new Vector3(cfd.scale[0], cfd.scale[1], cfd.scale[2]);
                    face.transform.localEulerAngles = new Vector3(cfd.rotation[0], cfd.rotation[1], cfd.rotation[2]);
                    face.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(cfd.position[0], cfd.position[1], cfd.position[2]);
                    face.GetComponent<RectTransform>().sizeDelta = new Vector2(cfd.sizeDelta[0], cfd.sizeDelta[1]);
                    gameController.UsingCustomFace(true);
                    gameController.ShowFaceImage(true);


                    gameController.isLoadedFace = true;
                    gameController.loadedFaceIndex = imageIndex;
                    gameController.faceHash = faceHash;

                }
                else if(gameController.currentlySelectedFace==2)
                {
                    face2.GetComponent<RawImage>().texture = t2d as Texture;
                    face2.transform.localScale = new Vector3(cfd.scale[0], cfd.scale[1], cfd.scale[2]);
                    face2.transform.localEulerAngles = new Vector3(cfd.rotation[0], cfd.rotation[1], cfd.rotation[2]);
                    face2.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(cfd.position[0], cfd.position[1], cfd.position[2]);
                    face2.GetComponent<RectTransform>().sizeDelta = new Vector2(cfd.sizeDelta[0], cfd.sizeDelta[1]);
                    gameController.UsingCustomFace2(true);
                    gameController.ShowFaceImage2(true);

                    gameController.isLoadedFace2 = true;
                    gameController.loadedFaceIndex2 = imageIndex;
                    gameController.faceHash2 = faceHash;
                    

                }
                
                gameController.ToggleSavedFacesPanel(2);
                gameController.isInHome = false;
                StartCoroutine(gameController.FinalizeImageEdit());
                ShowRemoveButton(true);
            }
            else
            {
                print("nope file does not exist so cant load face image");
            }
        }
    }

    public void DeleteThisCroppedFace()
    {
        print(string.Format("Deleting {0} file", cfd.imageName));
        saveManager.DeleteCroppedFaceData(cfd);
        
    }

    public void RemoveThisFaceFromScene()
    {
        if(gameController.isLoadedFace && (gameController.faceHash==faceHash))
        {
            gameController.RemoveFace1();
            
        }
        if (gameController.isLoadedFace2 && (gameController.faceHash2 == faceHash))
        {
            gameController.RemoveFace2();
           
        }
        ShowRemoveButton(false);
    }

    public void ShowRemoveButton(bool showStatus)
    {
        transform.GetChild(1).gameObject.SetActive(showStatus);
        transform.GetChild(0).gameObject.SetActive(!showStatus);
    }
}
