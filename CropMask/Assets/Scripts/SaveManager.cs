using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SaveManager : MonoBehaviour {


    [SerializeField]
    private GameObject gameControllerObj;
    private GameController gameController;


    public GameObject croppedFaceContainer;
    public GameObject facePrefab;

    public GameObject savedLookContainer;
    public GameObject saveLookPrefab;

    public string faceDataFile = "croppedfaces.dat";

    public string saveLookFile = "wearingsdata.dat";

    List<CroppedFaceData> croppedFaceDatas = new List< CroppedFaceData>();

    List<SaveData> saveDatas = new List<SaveData>();

    public List<GameObject> faces = new List<GameObject>();
    public List<GameObject> saves = new List<GameObject>();

    public PopUpController popupController;

    void Start () {
        gameController = gameControllerObj.GetComponent<GameController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}


#region SAVEDLOOKS
    public IEnumerator ShowSavedLooks()
    {
        saveDatas = SaveData.LoadData(saveLookFile);
        if (saveDatas != null)
        {
            foreach (SaveData s in saveDatas)
            {
                //s.InverseRecheckData();
                GameObject g = Instantiate(saveLookPrefab, savedLookContainer.transform);
                Image img = g.GetComponent<Image>();

                SavedLookProperties slp = g.GetComponent<SavedLookProperties>();
                slp.imageName = s.saveName;
                slp.imageIndex = saveDatas.IndexOf(s);

                slp.InitLookProperties(s);

                saves.Add(g);
                print(string.Format("loading image : {0}", s.saveName));
                StartCoroutine(LoadScreenshotFromDisk(img, s.saveName));
            }
        }

        yield return null;
    }

    public IEnumerator LoadScreenshotFromDisk(Image img, string imgname)
    {
        string dataPath = Application.persistentDataPath;

        if (Application.platform != RuntimePlatform.OSXPlayer)
        {
            dataPath += "/wearingsdata";
        }
        string finalSavePath = Path.Combine(dataPath, "screenshots");
        finalSavePath = Path.Combine(finalSavePath, imgname);
        print(string.Format("Full path to image is : {0} ", finalSavePath));
        if (File.Exists(finalSavePath))
        {
            print("yes screenshot does exist : "+finalSavePath);
            Texture2D t2d = new Texture2D(130, 150);
            t2d.LoadImage(File.ReadAllBytes(finalSavePath));

            img.sprite = Sprite.Create(t2d, new Rect(0f, 0f, t2d.width, t2d.height), new Vector2(0.5f, 0.5f), 100f);
        }
        else
        {
            print("nope file does not exist : "+finalSavePath);
        }

        yield return null;
    }

    public IEnumerator ResetAllSavedLooks()
    {
        foreach (GameObject s in saves)
        {
            Destroy(s);
        }
        StartCoroutine(ShowSavedLooks());
        yield return null;
    }

    public void DeleteSavedLooksData(SaveData s)
    {
        SaveData.DeleteSavedLook(s, saveDatas, saveLookFile);
        StartCoroutine(ResetAllSavedLooks());
    }

#endregion SAVEDLOOKS


#region SAVEDFACES
    public IEnumerator ShowCroppedFaces()
    {
        croppedFaceDatas = CroppedFaceData.LoadData(faceDataFile);
        if(croppedFaceDatas !=null)
        {
            foreach (CroppedFaceData c in croppedFaceDatas)
            {
                GameObject g = Instantiate(facePrefab, croppedFaceContainer.transform);
                Image img = g.GetComponent<Image>();
                CroppedFaceProperties cfp = g.GetComponent<CroppedFaceProperties>();
                cfp.imageName = c.imageName;
                cfp.imageIndex = croppedFaceDatas.IndexOf(c);

                cfp.InitFaceProperties(c);


                faces.Add(g);
                print(string.Format("loading image : {0}", c.imageName));
                StartCoroutine(LoadImageFromDisk(img, c.imageName));
            }
        }

        yield return null;
    }

    public IEnumerator LoadImageFromDisk(Image img,string imgname)
    {
        string dataPath = Application.persistentDataPath;

        if (Application.platform != RuntimePlatform.OSXPlayer)
        {
            dataPath += "/croppedfaces";
        }
        string finalSavePath = Path.Combine(dataPath, "faceimages");
        finalSavePath = Path.Combine(finalSavePath, imgname);
        print(string.Format("Full path to face image is : {0} ", finalSavePath));
        if(File.Exists(finalSavePath))
        {
            print("yes face file does exist : "+finalSavePath);
            Texture2D t2d = new Texture2D(150, 150);
            t2d.LoadImage(File.ReadAllBytes(finalSavePath));

            img.sprite = Sprite.Create(t2d, new Rect(0f, 0f, t2d.width, t2d.height), new Vector2(0.5f, 0.5f), 100f);
        }
        else
        {
            print("nope face file does not exist : "+finalSavePath);
        }

        yield return null;
    }

    public IEnumerator ResetAllCroppedFaces()
    {
        foreach(GameObject g in faces)
        {
            Destroy(g);
        }
        StartCoroutine(ShowCroppedFaces());
        yield return null;
    }


    public void DeleteCroppedFaceData(CroppedFaceData c)
    {
        CroppedFaceData.DeleteCroppedFace(c, croppedFaceDatas, faceDataFile);
        StartCoroutine(ResetAllCroppedFaces());
    }

#endregion SAVEDFACES

}
