using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofHide : MonoBehaviour
{
    public GameObject Roof;
    public bool isHide = false;
    private void Awake()
    {
        Roof.SetActive(!isHide);
    }
    public void roofHide()
    {
        isHide = !isHide;
        Debug.Log(isHide);
        
       
        Roof.SetActive(!isHide);
        
    }
}
