using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetup : MonoBehaviour
{
    public float RefferenceWidth = 1080f;
    public float RefferenceHeight = 1920;
    // Use this for initialization
    void Start()
    {
        //Camera.main.aspect = 9f / 16f;

        float targetAspectRatio = 9f / 16f;
        float targetWidth = (float)Screen.width;
        float targetHeight = (float)Screen.width / targetAspectRatio;

        Debug.Log(string.Format("targetWidth {0} , targetHeight {1}", targetWidth, targetHeight));

        if ((targetHeight > (float)Screen.height  ) && (Screen.width>RefferenceWidth))
        {
            targetHeight = (float)Screen.height;
            //targetHeight = Screen.height - 100f;
            targetWidth = (float)Screen.height * targetAspectRatio;
        }

        if(Screen.width>RefferenceWidth || Screen.height > RefferenceHeight)
        {
            Debug.Log(string.Format("targetWidth {0} , targetHeight {1}", targetWidth, targetHeight));

            float leftX = (Screen.width - targetWidth) / 2f;
            float leftY = (Screen.height - targetHeight) / 2f;

            Debug.Log(string.Format("leftX {0} , leftY {1}", leftX, leftY));

            //Rect pixelRect = new Rect(leftX, leftY, targetWidth, targetHeight);
            Rect pixelRect = new Rect(leftX, 0, targetWidth, Screen.height);
            Camera.main.pixelRect = pixelRect;
        }
    }
}