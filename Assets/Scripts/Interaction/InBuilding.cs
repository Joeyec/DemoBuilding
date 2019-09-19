using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InBuilding : MonoBehaviour
{
    public GameObject root;
    public GameObject camera;
    public Transform Floor1;
    public Transform Floor2;
    public Transform Floor3;
    public Transform Floor4;
    public int FloorIndex = 1;
    public bool isInbuilding = false;
    
    public void StayInBuilding()
    {
        root.transform.position = Vector3.zero;
        root.transform.localScale = Vector3.one;
        camera.transform.position = Floor1.position;
        isInbuilding = true;
        
    }
    public void UpStair()
    {
        if (isInbuilding)
        {
            switch (FloorIndex)
            {
                case 1:
                    camera.transform.position = Floor2.position;
                    FloorIndex += 1;
                    break;
                case 2:
                    camera.transform.position = Floor3.position;
                    FloorIndex += 1;
                    break;
                case 3:
                    camera.transform.position = Floor4.position;
                    FloorIndex += 1;
                    break;
                default:
                    break;
            }
        }


    }
    public void DownStair()
    {
        if (isInbuilding)
        {
            switch (FloorIndex)
            {
                case 4:
                    camera.transform.position = Floor3.position;
                    FloorIndex -= 1;
                    break;
                case 3:
                    camera.transform.position = Floor2.position;
                    FloorIndex -= 1;
                    break;
                case 2:
                    camera.transform.position = Floor1.position;
                    FloorIndex -= 1;
                    break;
                default:
                    break;
            }
        }
    }
    public void ResetPosition()
    {
        root.transform.position = new Vector3(0, -0.2f, 2);
        root.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        camera.transform.position = Vector3.zero;
        camera.transform.localEulerAngles = Vector3.zero;
        isInbuilding = false;
    }
}
