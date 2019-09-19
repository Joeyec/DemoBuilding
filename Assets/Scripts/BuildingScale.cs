using UnityEngine;
using System.Collections;
using HoloToolkit.Unity;

public class BuildingScale: MonoBehaviour
{

    private Vector3 navigationPreviousPosition;
    public float MaxScale = 2f;
    public float MinScale = 0.1f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void PerformNavigationStart(Vector3 position)
    {
        //设置初始位置  
        navigationPreviousPosition = position;
    }

    void PerformZoomUpdate(Vector3 position)
    {
        if (GestureManager.Instance.IsNavigating && HandsManager.Instance.FocusedGameObject == gameObject)
        {
            Vector3 deltaScale = Vector3.zero;
            float ScaleValue = 0.01f;
            //设置每一帧缩放的大小
            if (position.x < 0)
            {
                ScaleValue = -1 * ScaleValue;
            }
            //当缩放超出设置的最大，最小范围时直接返回
            if (transform.localScale.x >= MaxScale && ScaleValue > 0)
            {
                return;
            }
            else if (transform.localScale.x <= MinScale && ScaleValue < 0)
            {
                return;
            }
            //根据比例计算每个方向上的缩放大小
            deltaScale.x = ScaleValue;
            deltaScale.y = (transform.localScale.y / transform.localScale.x) * ScaleValue;
            deltaScale.z = (transform.localScale.z / transform.localScale.x) * ScaleValue;
            transform.localScale += deltaScale;
        }
    }
}