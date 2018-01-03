using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpController : MonoBehaviour {


    public GameController gameController;

    [SerializeField]
    protected GameObject[] popupPanels;

    public GameObject popupWithOkButton;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowPopup(int index,string message=null)
    {
        foreach (GameObject g in popupPanels )
        {
            g.SetActive(false);

        }

        GameObject p = popupPanels[index];
        Text m = p.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>();
        if(message!=null)
        {
            m.text = message;
        }

        p.SetActive(true);
    }

    public void ResetAllSavedFaceStatic()
    {
        foreach (GameObject g in popupPanels)
        {
            g.SetActive(false);

        }
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        StartCoroutine(gameController.saveManager.ResetAllCroppedFaces());
    }

    public void HidePopup()
    {
        foreach(GameObject g in popupPanels)
        {
            g.SetActive(false);

        }
        
    }


    public void DestroyPopup(GameObject g)
    {
        DestroyImmediate(g);
    }
   
}
