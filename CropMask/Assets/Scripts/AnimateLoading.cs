using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG;
using DG.Tweening;
public class AnimateLoading : MonoBehaviour {

    // Use this for initialization
    private float currentRotation = 0f;
    [SerializeField]
    private float rotatingSpeed = 100f;
    public bool shouldRotate = false;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(shouldRotate)
        {
            DoRotating();
        }
	}


    public IEnumerator StartRotating()
    {
        if(!shouldRotate)
        {
            shouldRotate = true;
            while (shouldRotate == true)
            {
                DoRotating();
                yield return new WaitForEndOfFrame();
            }
        }
        yield return null;
    }

    public void StartRotatingStatic()
    {
        if (!shouldRotate)
        {
            shouldRotate = true;
            
        }
        
    }

    public void StopRotating()
    {
        shouldRotate = false;
        StopCoroutine(StartRotating());
    }

    private void DoRotating()
    {
        currentRotation = currentRotation + (rotatingSpeed * Time.deltaTime * -1);
        if (currentRotation < -360)
        {
            currentRotation = 0f;
        }
        else if (currentRotation >= 360)
        {
            currentRotation = 0f;
        }
        transform.eulerAngles = new Vector3(0, 0, currentRotation);
    }
}
