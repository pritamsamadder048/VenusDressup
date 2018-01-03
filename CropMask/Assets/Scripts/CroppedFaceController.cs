using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using Lean.Touch;



public class CroppedFaceController : MonoBehaviour {


    bool isShowingFace = false;
    private BoxCollider2D cropImageCollider;
    [SerializeField]
    private bool isTouching = false;
    private Ray ray;
    //[SerializeField]
    //private RawImage cropImg;
    // Use this for initialization

    //LineRenderer buttomLine;
    //LineRenderer topLine;
    //LineRenderer leftLine;
    //LineRenderer rightLine;
    public float minX, maxX, minY, maxY;


    RectTransform rt;

    UILineRenderer rectangleLine;

    RawImage rwimg;

    public GameController gameController;
    public GameObject buttomleft, buttomright, topright, topleft;
    private Vector2 lastTouchPosition=Vector2.zero;

    public bool notOverSlider = true;
    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    void Start () {
        //buttomLine = new LineRenderer();
        //topLine = new LineRenderer();
        //leftLine = new LineRenderer();
        //rightLine = new LineRenderer();
		cropImageCollider= gameObject.GetComponent<BoxCollider2D>();
        rectangleLine = GetComponentInParent<UILineRenderer>();

        rt = GetComponent<RectTransform>();
        rwimg = GetComponent<RawImage>();
    }
	
	// Update is called once per frame
	void Update () {
        DragMask();
        DrawLine();
    }

    private void OnDisable()
    {
        notOverSlider = true;
    }
    private void OnEnable()
    {
        notOverSlider = true;
    }

    protected virtual void LateUpdate()
    {
        // This will move the current transform based on a finger drag gesture

        

    }

    public void DrawLine()
    {
        
        minX =transform.localPosition.x- (((rwimg.rectTransform.sizeDelta.x / 2)*(transform.localScale.x))+(rectangleLine.LineThickness));
        maxX = transform.localPosition.x + (((rwimg.rectTransform.sizeDelta.x / 2) * (transform.localScale.x)) + (rectangleLine.LineThickness));

        minY = transform.localPosition.y - (((rwimg.rectTransform.sizeDelta.y / 2) * (transform.localScale.y)) + (rectangleLine.LineThickness));
        maxY = transform.localPosition.y + (((rwimg.rectTransform.sizeDelta.y / 2) * (transform.localScale.y)) + (rectangleLine.LineThickness));
        Vector2[] linePoints = { new Vector2(minX, minY), new Vector2(maxX, minY),new Vector2(maxX,maxY),new Vector2(minX,maxY), new Vector2(minX, minY) };
        
        rectangleLine.Points = linePoints;
        //buttomleft.transform.position = linePoints[0];
        //buttomright.transform.position = linePoints[1];
        //topright.transform.position = linePoints[2];
        //topleft.transform.position = linePoints[3];
    }

    private void OnGUI()
    {
        //GUI.Label(gameObject.GetComponent<RawImage>().rectTransform.rect,Color.red);
        
        
    }

    

    public void DragMask()
    {
        
        Vector2 newPos;
        if (gameObject.activeSelf)
        {
            //Vector3 mousePos = gameController.MAINCAMERA.WorldToScreenPoint(Input.mousePosition);
            //if (Physics2D.OverlapCircle(mousePos, 2f) == gameController.faceCollider2d)
            //{
            //    print("Colided");
            //}
            //else
            //{
            //    print(string.Format("Mouse position : {0}", mousePos.ToString()));
            //}
            //			print ("mask enabled");
            //			print ("mouse press count : " + Input.GetKey (KeyCode.Mouse0));
            if (Application.isMobilePlatform)
            {
                if (Input.touchCount == 1 )
                {
                    //				print ("touching only one");
                    isTouching = true;
                }
                else 
                {
                    //				print ("not touching now");
                    isTouching = false;
                    lastTouchPosition = Vector2.zero;
                }
                
            }
            else
            {
                if ((Input.GetKey(KeyCode.Mouse0))&&((!Input.GetKey(KeyCode.LeftControl) && ! Input.GetKey(KeyCode.RightControl))))
                {
                    //				print ("touching only one");
                    isTouching = true;
                }
                else
                {
                    //				print ("not touching now");
                    isTouching = false;
                    lastTouchPosition = Vector2.zero;
                }
                
            }
        }
        else
        {
            if (isTouching)
            {
                isTouching = false;
                lastTouchPosition = Vector2.zero;
            }

            return;
        }
        if (gameObject.activeSelf && isTouching && notOverSlider)
        {


            if (Application.isMobilePlatform)
            {
                //ray = new Ray(Input.GetTouch(0).position, Vector3.forward);
                newPos = Input.GetTouch(0).position;

            }
            else
            {

                //ray = new Ray(Input.mousePosition, Vector3.forward);
                newPos = Input.mousePosition;
                
                //				print (Input.mousePosition);
            }

            if (lastTouchPosition == Vector2.zero)
            {
                lastTouchPosition = newPos;
            }
            //Debug.DrawRay(ray.origin, ray.direction * 100, Color.green);

            Vector2 deltaMovement = newPos - lastTouchPosition;

            //print(string.Format("delta movement : {0}", deltaMovement));

            if(deltaMovement!=Vector2.zero)
            {
                transform.position += new Vector3(deltaMovement.x,deltaMovement.y,0);
            }
            lastTouchPosition = newPos;
            //if (cropImageCollider == Physics2D.OverlapPoint(ray.origin))
            //{
            //    //			
            //    //print("Yes");
            //    if (ray.origin != transform.position)
            //    {


            //        //					maskImg.transform.position =new Vector3( Input.mousePosition.x-50f,Input.mousePosition.y-50f,1f);
            //        transform.position = newPos;


            //    }
            //    //				print ("hit");
            //}
            //else
            //{
            //    //				print ("not hitting : ");
            //    //				maskImg.transform.SetParent (maskImgParent.transform);
            //}

        }
        else
        {
            //			maskImg.transform.SetParent (maskImgParent.transform);
        }


    }
}
