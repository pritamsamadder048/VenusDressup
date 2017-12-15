using UnityEngine;
using System.Linq;
using System.Collections;

public class PopUpClosingStyle : MonoBehaviour
{
    public Collider2D attachedCollider;
    GameController gc;

    public CloseStyle popupCloseStyle = CloseStyle.None;
    // Use this for initialization
    void Start()
    {
        attachedCollider = GetComponent<Collider2D>();
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        
        //if(popupCloseStyle==CloseStyle.TapToClose)
        //{
        //    CloseThisPopupOnTap();
        //}
    }

    private void OnMouseUpAsButton()
    {
        if (popupCloseStyle == CloseStyle.TapToClose)
        {
            DestroyImmediate(gameObject);
        }
    }

   

    public void CloseThisPopupOnTap()
    {
#if !UNITY_EDITOR
        Touch currentTouch = Input.GetTouch(0);
        if(currentTouch.phase==TouchPhase.Ended)
        {
            
            if(attachedCollider==null)
            {
                attachedCollider = GetComponent<Collider2D>();
                if(attachedCollider==null)
                {
                    return;
                }
            }

            Collider2D hitCollider = Physics2D.OverlapCircle(currentTouch.position, .25f);

            if(hitCollider==null || hitCollider!=attachedCollider)
            {
                return;
            }
            else if(hitCollider==attachedCollider)
            {
                DestroyImmediate(gameObject);
            }

        }
#else
        if (Input.GetMouseButtonUp(0))
        {

            if (attachedCollider == null)
            {
                attachedCollider = GetComponent<Collider2D>();
                if (attachedCollider == null)
                {
                    return;
                }
            }

            Collider2D hitCollider = Physics2D.OverlapBox(Input.mousePosition,Vector2.one*2f,0f);

            print(string.Format("mouse is at : {0}", Input.mousePosition));
            Vector3 formattedPoint = Camera.main.WorldToScreenPoint(Input.mousePosition);
            formattedPoint=new Vector3(Mathf.CeilToInt(formattedPoint.x / gc.touchController.mainCanvas.scaleFactor), Mathf.CeilToInt(formattedPoint.y / gc.touchController.mainCanvas.scaleFactor), 0f);
            print(string.Format("formated mouse is at : {0} ",formattedPoint));

            print(string.Format("current collider is : {0} ", attachedCollider.bounds));
            if (hitCollider == null || hitCollider != attachedCollider)
            {
                return;
            }
            else if (hitCollider == attachedCollider)
            {
                print(string.Format("Hit got for :{0} ", hitCollider.name));
                DestroyImmediate(gameObject);
            }

        }
#endif

    }
}


public enum CloseStyle
{
    None,
    TapToClose,
    TapAndHoldToClose

};