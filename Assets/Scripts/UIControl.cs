using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    public Transform camera;
    // Use this for initialization
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        //当前对象始终面向摄像机。
        this.transform.LookAt(camera.position);

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(camera.position - this.transform.position), 0);

        this.transform.position = new Vector3(camera.position.x, camera.position.y, camera.position.z + 10);
    }

}