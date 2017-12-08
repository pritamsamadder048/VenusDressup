using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG;
using DG.Tweening;


public class RotationController : MonoBehaviour {

    [SerializeField]
    private GameObject RotatorParent;

    [SerializeField]
    private int selectedShape;

    public int currentSelectedShape;
    [SerializeField]
    private int backLeftShape;
    public int currentBackLeftShape;
    [SerializeField]
    private int backRightShape;
    public int currentBackRightShape;

    
    public GameObject[] shapes;

    [SerializeField]
    private float currentRotation = 0f;

    private int totalShapes;

    [SerializeField]
    private GameObject selectShapeControllerObject;
    public SelectShapeController selectShapeController;

    public string[] bodyToneColors = { "white", "tan", "olive", "dark" };
    public string[] eyeColors = { "black", "blue", "brown", "green" };
    public string[] bodyShapes = { "apple", "banana", "busty", "hourglass", "pear" };

    public bool unselectedIsHidden=false;

    private void Awake()
    {
        selectShapeController = selectShapeControllerObject.GetComponent<SelectShapeController>();
    }

    // Use this for initialization
    void Start () {
        selectedShape = 0;
        totalShapes = shapes.Length;
        
	}
	
	// Update is called once per frame
	void Update () {



        GetSelectedShapeIndex();
        GetBackLeftShape();
        GetBackRightShape();

    }

    public int GetBackLeftShape()
    {
        backLeftShape = selectedShape + 3;
        if (backLeftShape >= shapes.Length)
        {
            backLeftShape = backLeftShape - shapes.Length;
        }
        return backLeftShape;
    }
    public int GetBackRightShape()
    {
        backRightShape = selectedShape + 2;
        if (backRightShape >= shapes.Length)
        {
            backRightShape = backRightShape - shapes.Length;
        }
        return backRightShape;
    }

    private void SelectNextShape()
    {
        //selectShapeController.ChangeBodyTone();
        currentRotation += 72f;
        if(currentRotation>360)
        {
            currentRotation = 72f;
        }

        
        RotatorParent.transform.DOLocalRotate(new Vector3(0f, currentRotation, 0f), .5f);

        TransparentUnSelected();
        selectShapeController.SetCarouselSelectedBodyShapeAndTone(GetSelectedShape().GetComponent<SpriteRenderer>().sprite.texture.name);
        selectShapeController.SetcarouselSelectedRotation(currentRotation);


        print(string.Format("current selected is : {0}", GetSelectedShape().GetComponent<SpriteRenderer>().sprite.texture.name));

        print(string.Format("previous shape index is : {0}", GetNextShapeBeforeIndex(GetSelectedShapeIndex())));
        //StartCoroutine(selectShapeController.ResetModel(shapes[GetNextShapeBeforeIndex(GetSelectedShapeIndex())]));


    }

    private void SelectPreviousShape()
    {
        //selectShapeController.ChangeBodyTone();
        currentRotation -= 72f;
        if (currentRotation < -360)
        {
            currentRotation = -72f;
        }
        RotatorParent.transform.DOLocalRotate(new Vector3(0f, currentRotation, 0f), .5f);

        TransparentUnSelected();
        selectShapeController.SetcarouselSelectedRotation(currentRotation);
        
        selectShapeController.SetCarouselSelectedBodyShapeAndTone(GetSelectedShape().GetComponent<SpriteRenderer>().sprite.texture.name);
        
        
        

        print(string.Format("current selected is : {0}", GetSelectedShape().GetComponent<SpriteRenderer>().sprite.texture.name));

        print(string.Format("next shape index is : {0}", GetNextShapeAfterIndex(GetSelectedShapeIndex())));
        //StartCoroutine(selectShapeController.ResetModel(shapes[GetNextShapeAfterIndex(GetSelectedShapeIndex())]));
    }
    public void SelectThisModel(float rotation,int index=0)
    {
        currentRotation = rotation;

        print("current rotation is : " + currentRotation);
        RotatorParent.transform.localEulerAngles=new Vector3(0f, currentRotation, 0f);
        GetSelectedShapeIndex();
        TransparentUnSelected();
        selectShapeController.SetcarouselSelectedRotation(currentRotation);
        selectShapeController.SetCarouselSelectedBodyShapeAndTone(GetSelectedShape().GetComponent<SpriteRenderer>().sprite.texture.name);
        

    }
    public void TransparentUnSelected()
    {
        int curShape = GetSelectedShapeIndex();
        int brShape = GetBackRightShape();
        int blShape = GetBackLeftShape();

        for (int i = 0; i < shapes.Length; i++)
        {
            if (i == brShape || i == blShape)
            {
                SpriteRenderer sr = shapes[i].GetComponent<SpriteRenderer>();
                //print(sr);
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, .7f);
            }
            else if(i!=GetSelectedShapeIndex())
            {
                SpriteRenderer sr = shapes[i].GetComponent<SpriteRenderer>();

                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, .9f);
            }
            else
            {
                SpriteRenderer sr = shapes[i].GetComponent<SpriteRenderer>();

                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
            }
        }
    }

    public void OnClickNextShapeButton()
    {

        SelectNextShape();
    }

    public void OnClickPreviousShapeButton()
    {
        SelectPreviousShape();
    }


    public int GetSelectedShapeIndex()
    {
        selectedShape = ((int)currentRotation / 72);
        if (selectedShape >= shapes.Length)
        {
            selectedShape = 0;

            print("in get selected shape index if");
        }
        else if (selectedShape < 0)
        {
            selectedShape = (shapes.Length) - Mathf.Abs(selectedShape);
            print("in get selected shape index else");
        }
        return selectedShape;
    }

    public int GetNextShapeAfterIndex(int index )
    {
        int nextIndex = GetSelectedShapeIndex() + 1;
        if (nextIndex >= shapes.Length)
        {
            nextIndex = 0;


        }
        else if (nextIndex < 0)
        {
            nextIndex = (shapes.Length) - Mathf.Abs(nextIndex);
        }

        return nextIndex;
    }

    public int GetNextShapeBeforeIndex(int index)
    {
        int nextIndex = GetSelectedShapeIndex() - 1;
        if (nextIndex >= shapes.Length)
        {
            nextIndex = 0;


        }
        else if (nextIndex < 0)
        {
            nextIndex = (shapes.Length) - Mathf.Abs(nextIndex);
        }

        return nextIndex;
    }

    public GameObject GetSelectedShape()
    {
        return shapes[selectedShape];
    }

    public void HideUnSelectedShapes(bool showStatus)
    {
        print("current selected is : " + selectedShape);
        for(int i=0;i<shapes.Length;i++)
        {
            if(i!=selectedShape)
            {
                shapes[i].SetActive(!showStatus);
                print("hiding :" + i.ToString());
            }
        }
        unselectedIsHidden = true;
    }

    public void ShowAllShapes(bool showStatus)
    {
        for (int i = 0; i < shapes.Length; i++)
        {
            shapes[i].SetActive(showStatus);
        }
        unselectedIsHidden = !showStatus;
    }

    public void CheckForBodyToneChange()
    {
        selectShapeController.SetCarouselSelectedBodyShapeAndTone(GetSelectedShape().GetComponent<SpriteRenderer>().sprite.texture.name);
    }

    
}
