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

    public bool faceInUse = false;

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
            GetComponent<Image>().color = new Color(c.faceColor[0], c.faceColor[1], c.faceColor[2], c.faceColor[3]);
        }
        else
        {
            isInitialized = false;
        }

        if(gameController.faceHash==faceHash  || gameController.faceHash2==faceHash)
        {
            faceInUse = true;
            ShowRemoveButton(true);

            if(gameController.faceHash == faceHash)
            {
                gameController.loadedFaceIndex = imageIndex;
            }
            else if(gameController.faceHash2==faceHash)
            {
                gameController.loadedFaceIndex2 = imageIndex;
            }
        }
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void Copy(ref CroppedFaceProperties cp)
    {
        cp = new CroppedFaceProperties
        {
            imageName = this.imageName,
            imageIndex = this.imageIndex,
            dataPath = this.dataPath,
            finalSavePath = this.finalSavePath,
            faceHash = this.faceHash
        };
        cfd.Copy(ref cp.cfd);
    }

    public void UseThisCroppedFace()
    {
        print("clicked");
        if(isInitialized && !faceInUse)
        {
            if (File.Exists(finalSavePath))
            {

                if(gameController.isShowingMale)
                {
                    print("yes file does exist");
                    Texture2D t2d = new Texture2D(10, 10);
                    t2d.LoadImage(File.ReadAllBytes(finalSavePath));

                    this.Copy(ref gameController.tempCroppedFaceProperty);
                    

                    gameController.ToggleSavedFacesPanel(2);
                    gameController.isInHome = false;
                    Vector3 scale = new Vector3(cfd.scale[0], cfd.scale[1], cfd.scale[2]);
                    Vector3 rotation = new Vector3(cfd.rotation[0], cfd.rotation[1], cfd.rotation[2]);
                    Color col = new Color(cfd.faceColor[0], cfd.faceColor[1], cfd.faceColor[2], cfd.faceColor[3]);
                    StartCoroutine(gameController.FinalizeImageEdit(t2d, scale, rotation, imageIndex, faceHash, col, false));
                    ShowRemoveButton(true);
                }
                else
                {
                    Texture2D t2d = new Texture2D(150, 150);
                    t2d.LoadImage(File.ReadAllBytes(finalSavePath));

                    


                    //gameController.isLoadedFace = true;
                    //gameController.loadedFaceIndex = imageIndex;
                    //gameController.faceHash = faceHash;

                    //this.Copy(gameController.loadedCroppedFaceProperty);
                    this.Copy(ref gameController.tempCroppedFaceProperty);

                    gameController.ToggleSavedFacesPanel(2);
                    gameController.isInHome = false;
                    //gameController.currentlySelectedFace = 1;
                    //gameController.currentlyUsingFace = 1;
                    Vector3 scale = new Vector3(cfd.scale[0], cfd.scale[1], cfd.scale[2]);
                    Vector3 rotation= new Vector3(cfd.rotation[0], cfd.rotation[1], cfd.rotation[2]);
                    Color col = new Color(cfd.faceColor[0], cfd.faceColor[1], cfd.faceColor[2], cfd.faceColor[3]);
                    StartCoroutine(gameController.FinalizeImageEdit(t2d,scale,rotation,imageIndex,faceHash,col,false));
                    ShowRemoveButton(true);
                }
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
        faceInUse = false;
    }

    public void ShowRemoveButton(bool showStatus)
    {
        transform.GetChild(1).gameObject.SetActive(showStatus);
        transform.GetChild(0).gameObject.SetActive(!showStatus);
    }
}
