using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG;
using DG.Tweening;

public class AnimateHint : MonoBehaviour {

    [SerializeField]
    private bool shouldAnimate = false;
    [SerializeField]
    private float HINT_MOVING_SPEED = 3f;
    [SerializeField]
    private float BOUND_SIZE = 20f;
    private float hintTransition = 0.0f;
    [SerializeField]
    private Transform mainParent;
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        Animate();
		
	}
    private void Animate()
    {
        if(shouldAnimate)
        {
            hintTransition += Time.deltaTime * HINT_MOVING_SPEED;
            transform.localPosition = new Vector3((Mathf.Sin(hintTransition) * BOUND_SIZE)+175f, transform.localPosition.y, transform.localPosition.z);
        }
    }
    public void StartAnimating()
    {
        transform.gameObject.SetActive(true);
        shouldAnimate = true;

    }
    public void StopAnimating()
    {
        transform.gameObject.SetActive(false);
        shouldAnimate = false;
        if(transform.parent!=mainParent)
        {
            transform.parent.GetChild(1).gameObject.SetActive(false);
        }
        transform.parent = mainParent;
    }

    public void InitialistHintFor(GameObject parentGameObject)
    {
        parentGameObject.transform.GetChild(1).gameObject.SetActive(true);
        transform.parent = parentGameObject.transform;
        GetComponent<RectTransform>().anchoredPosition3D = new Vector3(200, 0, 0);
        //hintTransition = 100;

    }

    public bool IsShowingHint()
    {
        return shouldAnimate;
    }

    public GameObject HintShowingFor()
    {
        return transform.parent.gameObject;
    }
}
