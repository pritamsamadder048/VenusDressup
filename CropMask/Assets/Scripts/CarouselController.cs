using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CarouselController : MonoBehaviour {


    [SerializeField]
    private GameObject mainPanelObject;
    [SerializeField]
    private GameObject scrollPanelObject;
    private RectTransform panel;
    [SerializeField]
    private GameObject[] objects;
    [SerializeField] private RectTransform center;

    [SerializeField]
    private float[] distance;
    [SerializeField]
    private float[] distanceReposition;
    [SerializeField]
    private int startObject = 1;
    [SerializeField]
    private int selectedObject = 0;
    [SerializeField]
    private float maxDistance = 250f;
    [SerializeField]
    private float error_margin = 10f;
    [SerializeField]
    private float positioningSpeed = 10f;
    [SerializeField]
    private float scalingingSpeed = 5f;
    [SerializeField]
    private bool dragging = false;

    private ScrollRect scrollRect;
    [SerializeField]
    private int objectDistance;
    [SerializeField]
    private int minObjectNum;
    [SerializeField]
    private int objectLength;
    [SerializeField]
    private bool targetNearestObject = true;
	// Use this for initialization
	void Start () {
        //maxDistance = (Screen.width <= 600) ? Screen.width / 2f : 300f;
        maxDistance = ((Screen.width / 800f) * 300f) + 10f;
        panel = scrollPanelObject.GetComponent<RectTransform>();
        scrollRect = mainPanelObject.GetComponent<ScrollRect>();
        objectLength = objects.Length;
        distance = new float[objectLength];
        distanceReposition = new float[objectLength];
        objectDistance = (int)Mathf.Abs(objects[1].GetComponent<RectTransform>().anchoredPosition.x - objects[0].GetComponent<RectTransform>().anchoredPosition.x);
        if(selectedObject>0 && selectedObject<=objectLength)
        {
            panel.anchoredPosition = new Vector2((startObject - 1) * -objectDistance, panel.anchoredPosition.y);
        }


    }

    
    // Update is called once per frame
    void Update () {
		for(int i=0;i< objects.Length;i++)
        {
            distanceReposition[i] = center.GetComponent<RectTransform>().position.x - objects[i].GetComponent<RectTransform>().position.x;
            distance[i] = Mathf.Abs(distanceReposition[i]);

            if((distanceReposition[i]>=(maxDistance)))// && !dragging)
            {
                float curX = objects[i].GetComponent<RectTransform>().anchoredPosition.x;
                float curY = objects[i].GetComponent<RectTransform>().anchoredPosition.y;

                Vector2 newAnchoredPosition = new Vector2(curX + (objectLength * objectDistance), curY);
                
                objects[i].GetComponent<RectTransform>().anchoredPosition = newAnchoredPosition;
                //print(string.Format("Changing position to negative for i = {0}  because distance is {1}", i, distanceReposition[i]));
            }
            else if ((distanceReposition[i] < (maxDistance)*-1f))// && !dragging)
            {
                float curX = objects[i].GetComponent<RectTransform>().anchoredPosition.x;
                float curY = objects[i].GetComponent<RectTransform>().anchoredPosition.y;

                Vector2 newAnchoredPosition = new Vector2(curX - (objectLength * objectDistance), curY);
                
                objects[i].GetComponent<RectTransform>().anchoredPosition = newAnchoredPosition;
                //print(string.Format("Changing position to positive for i = {0} because distance is {1}", i, distanceReposition[i]));
            }
        }

        float minDistance = Mathf.Min(distance);
        if (targetNearestObject)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                if (minDistance == distance[i])
                {
                    minObjectNum = i;
                    selectedObject = minObjectNum;
                    break;
                }
            }
        }
        //print(string.Format("minimagenum : {0} image distance : {1}", minImageNum, imageDistance));
        LerpToObjectScale();
        if (!dragging)
        {
            //LerpToImage(minImageNum * -imageDistance);
            LerpToObject(-objects[minObjectNum].GetComponent<RectTransform>().anchoredPosition.x);
            
            
        }
	}


    private void LerpToObject(float position)
    {
        
        float newX = Mathf.Lerp(panel.anchoredPosition.x, position, Time.deltaTime * positioningSpeed);
        //print(string.Format("anchored pos :{0} , new Position : {1} , new lerp position : {3}", panel.anchoredPosition.x, position, position, newX));
        //if(Mathf.Abs(position-newX)<5f)
        //{
        //    newX = position;
        //}
        if ((Mathf.Abs(newX)>=Mathf.Abs(position)-1f) && (Mathf.Abs(newX)<= Mathf.Abs(position)+1f))
        {
            //selectedObject = minObjectNum;
        }
        Vector2 newPosition = new Vector2(newX, panel.anchoredPosition.y);
        panel.anchoredPosition = newPosition;
    }

    private void LerpToObjectScale()
    {
        for (int i = 0; i < objectLength; i++)
        {
            if (i == minObjectNum)
            {
                //objects[i].transform.localScale = new Vector3(1f, 1f, 1f);
                objects[i].transform.localScale = Vector3.Lerp(objects[i].transform.localScale, Vector3.one, Time.deltaTime * scalingingSpeed);
                objects[i].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                //print("Selected color : " + objects[i].GetComponent<Image>().color);
            }
            else
            {
                //objects[i].transform.localScale = new Vector3(.8f, .8f, 1f);
                objects[i].transform.localScale = Vector3.Lerp(objects[i].transform.localScale, Vector3.one/2, Time.deltaTime * scalingingSpeed);
                //objects[i].GetComponent<Image>().color = new Color(139f/255f, 139f/255f, 139f/255f, 205f/255f);
                objects[i].GetComponent<Image>().color = new Color(100f / 255f, 100f / 255f, 100f / 255f, 255f / 255f);
                //print("UnSelected color : " + objects[i].GetComponent<Image>().color);
            }
        }
    }

    public void StartDraggingCarousel()
    {
        dragging = true;
        targetNearestObject = true;
    }

    public void EndDraggingCarousel()
    {
        dragging = false;
    }
    public void GoToObject(int objectIndex)  //starting from 0
    {
        targetNearestObject = false;
        if(objectIndex>=objectLength)
        {
            objectIndex = objectLength-1;
        }
        else if(objectIndex<=0)
        {
            objectIndex = 0;
        }

        selectedObject = minObjectNum = objectIndex;
        //selectedObject = minObjectNum;
    }

    public void GoToNextObject()
    {
        int objectToSelect = minObjectNum;
        objectToSelect += 1;
        if (objectToSelect >= objectLength)
        {
            objectToSelect = 0;
        }
        //else if (objectToSelect < 0)
        //{
        //    objectToSelect = objectLength-1;
        //}
        //print("selecting previous object : " + objectToSelect);
        GoToObject(objectToSelect);
    }

    public void GoToPreviousObject()
    {
        int objectToSelect = minObjectNum;
        objectToSelect -= 1;
        if (objectToSelect < 0)
        {
            objectToSelect = objectLength-1;
        }
        //else if (objectToSelect >= objectLength)
        //{
        //    objectToSelect = 0;
        //}
        //print("selecting previous object : " + objectToSelect);
        GoToObject(objectToSelect);
    }
    public void HideUnSelectedObjects(bool alsoDisableScrolling=false)
    {
        for(int i=0;i<objects.Length;i++)
        {
            if(i!=selectedObject)
            {
                objects[i].SetActive(false);
            }
        }
        if(alsoDisableScrolling)
        {
            DisableScrolling();
        }
    }
    public void ShowAllObjects(bool alsoEnableScrolling = false)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(true);
        }
        if(alsoEnableScrolling)
        {
            EnableScrolling();
        }
    }

    public void DisableScrolling()
    {
        if(scrollRect==null)
        {
            scrollRect = mainPanelObject.GetComponent<ScrollRect>();
        }
        scrollRect.enabled = false;
    }
    public void EnableScrolling()
    {
        if (scrollRect == null)
        {
            scrollRect = mainPanelObject.GetComponent<ScrollRect>();
        }
        scrollRect.enabled = true;
    }
}
