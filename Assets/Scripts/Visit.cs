using HoloToolkit.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visit : MonoBehaviour
{
    public float speed = 2;
    public GameObject root;
    public GameObject camera;
    public GameObject protos;
    public GameObject FloorPoint;
    private Vector3 cameraPosition;
    private Vector3 floor1Position;
    private void Update()
    {
        
    }

    public void VisitAction()
    {
        
        //cameraPosition = transform.parent.Find("MixedRealityCameraParent").transform.position;
        floor1Position = FloorPoint.transform.position;
        if (transform.GetComponent<FixedAngularSize>() != null)
        {
            transform.GetComponent<FixedAngularSize>().enabled = false;
        }
        protos.transform.localScale =new Vector3(1,1,1);
        root.transform.localScale = new Vector3(8, 8, 8);
        camera.transform.position = new Vector3(floor1Position.x,floor1Position.y+1.5f,floor1Position.z);
    }
}
