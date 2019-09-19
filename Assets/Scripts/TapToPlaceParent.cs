using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;

public class TapToPlaceParent : MonoBehaviour
{
    public GameObject camera;
    public GameObject root;
    bool placing = false;

    // Called by GazeGestureManager when the user performs a Select gesture
    public void OnSelect()
    {
        // On each Select gesture, toggle whether the user is in placing mode.
        placing = !placing;
        root.GetComponent<Tagalong>().enabled = placing;
        root.GetComponent<Billboard>().enabled = placing;
        if (root.GetComponent<FixedAngularSize>() != null)
            root.GetComponent<FixedAngularSize>().enabled = placing;
                // If the user is in placing mode, display the spatial mapping mesh.
        if (placing)
        {
            SpatialMappingManager.Instance.DrawVisualMeshes = true;
        }
        // If the user is not in placing mode, hide the spatial mapping mesh.
        else
        {
            SpatialMappingManager.Instance.DrawVisualMeshes = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If the user is in placing mode,
        // update the placement to match the user's gaze.

        if (placing)
        {
            // Do a raycast into the world that will only hit the Spatial Mapping mesh.
            var headPosition = camera.transform.position;
            var gazeDirection = camera.transform.forward;

            RaycastHit hitInfo;
            if (Physics.Raycast(headPosition, gazeDirection, out hitInfo,
                30.0f, SpatialMappingManager.Instance.PhysicsLayer))
            {
                // Move this object's parent object to
                // where the raycast hit the Spatial Mapping mesh.
                this.transform.position = hitInfo.point;

                // Rotate this object's parent object to face the user.
                Quaternion toQuat = camera.transform.localRotation;
                toQuat.x = 0;
                toQuat.z = 0;
                this.transform.parent.rotation = toQuat;
               
            }
        }
    }
}