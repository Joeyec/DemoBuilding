using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
public class OneHandControl : MonoBehaviour,INavigationHandler
{
    public float RotationSensitivity = 1;
    public float ScaleSensitivity = 0.005f;
    private float orignalScale = 1f;
    public void OnNavigationCanceled(NavigationEventData eventData)
    {
        
    }

    public void OnNavigationCompleted(NavigationEventData eventData)
    {
        
    }

    public void OnNavigationStarted(NavigationEventData eventData)
    {
       
    }

    
    public void OnNavigationUpdated(NavigationEventData eventData)
    {
        float rotationFactor = eventData.NormalizedOffset.x * RotationSensitivity;
        BoxCollider box = GetComponent<BoxCollider>();
        //transform.Rotate(new Vector3(0, -1 * rotationFactor, 0));
        transform.RotateAround(box.bounds.center, new Vector3(0, -1, 0), rotationFactor);
        //float scaleFactor = eventData.NormalizedOffset.x * ScaleSensitivity;
        //make sure it is not an overflow
        if (Mathf.Abs(eventData.NormalizedOffset.x) < 1
        && Mathf.Abs(eventData.NormalizedOffset.z) < 1)
        {
            float newScaleX = transform.localScale.x * (orignalScale + eventData.NormalizedOffset.y * ScaleSensitivity);
            float newScaleY = transform.localScale.y * (orignalScale + eventData.NormalizedOffset.y * ScaleSensitivity);
            float newScaleZ = transform.localScale.z * (orignalScale + eventData.NormalizedOffset.y * ScaleSensitivity);
            transform.localScale = new Vector3(newScaleX, newScaleY, newScaleZ);
        }
        //mark the event data to be used, why??
        eventData.Use();
    }
    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
