using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpController : MonoBehaviour {


    public GameController gameController;

    [SerializeField]
    protected GameObject[] popupPanels;

    public GameObject popupWithOkButton;

    public GameObject PrefabBodyshapeSelectionPopup;
    public GameObject bodyshapeSelectionPopup;
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

    public void InstantiateBodyShapeSelectionPopup()
    {
        if(bodyshapeSelectionPopup!=null)
        {
            Destroy(bodyshapeSelectionPopup);


        }

        bodyshapeSelectionPopup = Instantiate<GameObject>(PrefabBodyshapeSelectionPopup,Vector3.zero,Quaternion.identity, gameController.canvasObject.transform);
        bodyshapeSelectionPopup.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
        Button okButton = bodyshapeSelectionPopup.transform.GetChild(0).GetChild(2).GetComponent<Button>();
        Button closeButton = bodyshapeSelectionPopup.transform.GetChild(0).GetChild(1).GetComponent<Button>();

        if(closeButton!=null)
        {
            closeButton.onClick.AddListener(() => {
                Destroy(bodyshapeSelectionPopup);
            });
        }

        if(okButton!=null)
        {
            okButton.onClick.AddListener(() => {
                gameController.selectShapeController.OnClickSelectShapeButton(true);
                Destroy(bodyshapeSelectionPopup);
            });
        }
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
