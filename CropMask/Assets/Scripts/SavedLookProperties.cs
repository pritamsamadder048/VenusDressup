using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SavedLookProperties : MonoBehaviour {


    public string imageName;
    public int imageIndex = -1;

    GameController gameController;
    SaveManager saveManager;
    string dataPath;
    string finalSavePath;

    //public GameObject look;

    public SaveData sd;

    private void Awake()
    {

        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        saveManager = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManager>();
        if (gameController != null)
        {
            
        }
        GetComponent<Button>().onClick.AddListener(LoadSavedLook);
        transform.GetChild(0).GetComponent<Button>().onClick.AddListener(DeleteThisSavedLook);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void InitLookProperties(SaveData s)
    {
        dataPath = Application.persistentDataPath;

        if (Application.platform != RuntimePlatform.OSXPlayer)
        {
            dataPath += "/wearingsdata";
        }
        finalSavePath = Path.Combine(dataPath, "screenshots");
        finalSavePath = Path.Combine(finalSavePath, imageName);
        sd = s;
        

    }

    public void LoadSavedLook()
    {
        print("using look : "+finalSavePath);
    }


    public void DeleteThisSavedLook()
    {
        print(string.Format("Deleting {0} file", sd.saveName));
        saveManager.DeleteSavedLooksData(sd);

    }
}
