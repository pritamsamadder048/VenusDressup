using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour {
    public Transform cam_Test;
   //public Transform mainCamTransform;
    void Update()
    {
        transform.LookAt(cam_Test.transform.position, Vector3.up);
    }
}

