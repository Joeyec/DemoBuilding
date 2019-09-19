using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHide : MonoBehaviour
{
    private bool isHide=true;
    public GameObject ActionUI;
    public GameObject Lable;
    private void Awake()
    {
        ActionUI.SetActive(!isHide);
        Lable.SetActive(!isHide);
    }
    public void UIMenuHide()
    {
        isHide = !isHide;
        ActionUI.SetActive(!isHide);
        Lable.SetActive(!isHide);
    }

}
