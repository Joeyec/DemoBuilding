// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using HoloToolkit.Unity.InputModule;
using UnityEngine;
//using UnityEngine.VR.WSA.Input;
using UnityEngine.XR.WSA.Input;

namespace HoloToolkit.Unity
{
    /// <summary>
    /// GestureManager creates a gesture recognizer and signs up for a tap gesture.
    /// When a tap gesture is detected, GestureManager uses GazeManager to find the game object.
    /// GestureManager then sends a message to that game object.
    /// </summary>
    [RequireComponent(typeof(GazeManager))]
    public partial class GestureManager : Singleton<GestureManager>
    {
        /// <summary>
        /// Key to press in the editor to select the currently gazed hologram
        /// </summary>
        public KeyCode EditorSelectKey = KeyCode.Space;

        /// <summary>
        /// To select even when a hologram is not being gazed at,
        /// set the override focused object.
        /// If its null, then the gazed at object will be selected.
        /// </summary>
        public GameObject OverrideFocusedObject
        {
            get; set;
        }

        /// <summary>
        /// Gets the currently focused object, or null if none.
        /// </summary>
        public GameObject FocusedObject
        {
            get { return focusedObject; }
        }

        public bool IsNavigating { get; private set; }
        public Vector3 NavigationPosition { get; private set; }

        private GestureRecognizer gestureRecognizer;
        private GameObject focusedObject;

        void Start()
        {
            //  创建GestureRecognizer实例  
            gestureRecognizer = new GestureRecognizer();
            //  注册指定的手势类型  
            gestureRecognizer.SetRecognizableGestures(GestureSettings.Tap
                | GestureSettings.NavigationX);
            //  订阅手势事件  
            gestureRecognizer.TappedEvent += GestureRecognizer_TappedEvent;

            //添加Navigation手势事件  
            gestureRecognizer.NavigationStartedEvent += GestureRecognizer_NavigationStartedEvent;
            gestureRecognizer.NavigationUpdatedEvent += GestureRecognizer_NavigationUpdatedEvent;
            gestureRecognizer.NavigationCompletedEvent += GestureRecognizer_NavigationCompletedEvent;
            gestureRecognizer.NavigationCanceledEvent += GestureRecognizer_NavigationCanceledEvent;
            //  开始手势识别  
            gestureRecognizer.StartCapturingGestures();
        }

        private void GestureRecognizer_NavigationCanceledEvent(InteractionSourceKind source, Vector3 normalizedOffset, Ray headRay)
        {
            IsNavigating = false;
        }

        private void GestureRecognizer_NavigationCompletedEvent(InteractionSourceKind source, Vector3 normalizedOffset, Ray headRay)
        {
            IsNavigating = false;
        }

        private void GestureRecognizer_NavigationUpdatedEvent(InteractionSourceKind source, Vector3 normalizedOffset, Ray headRay)
        {
            if (HandsManager.Instance.FocusedGameObject != null)
            {
                IsNavigating = true;
                NavigationPosition = normalizedOffset;
                HandsManager.Instance.FocusedGameObject.SendMessageUpwards("PerformZoomUpdate", normalizedOffset, SendMessageOptions.DontRequireReceiver);
            }
        }

        private void GestureRecognizer_NavigationStartedEvent(InteractionSourceKind source, Vector3 normalizedOffset, Ray headRay)
        {
            if (HandsManager.Instance.FocusedGameObject != null)
            {
                IsNavigating = true;
                NavigationPosition = normalizedOffset;
                HandsManager.Instance.FocusedGameObject.SendMessageUpwards("PerformNavigationStart", normalizedOffset, SendMessageOptions.DontRequireReceiver);
            }
        }

        private void OnTap()
        {
            if (focusedObject != null)
            {
                focusedObject.SendMessage("OnSelect", SendMessageOptions.DontRequireReceiver);
            }
        }

        private void GestureRecognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
        {
            OnTap();
        }

        void LateUpdate()
        {
            GameObject oldFocusedObject = focusedObject;

            if (GazeManager.Instance.HitObject &&
                OverrideFocusedObject == null &&
                GazeManager.Instance.HitInfo.collider != null)
            {
                // If gaze hits a hologram, set the focused object to that game object.
                // Also if the caller has not decided to override the focused object.
                focusedObject = GazeManager.Instance.HitInfo.collider.gameObject;
            }
            else
            {
                // If our gaze doesn't hit a hologram, set the focused object to null or override focused object.
                focusedObject = OverrideFocusedObject;
            }

            //if (focusedObject != oldFocusedObject)
            //{
            //    // If the currently focused object doesn't match the old focused object, cancel the current gesture.
            //    // Start looking for new gestures.  This is to prevent applying gestures from one hologram to another.
            //    gestureRecognizer.CancelGestures();
            //    gestureRecognizer.StartCapturingGestures();
            //}

#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(EditorSelectKey))
            {
                OnTap();
            }
#endif
        }

        void OnDestroy()
        {
            gestureRecognizer.StopCapturingGestures();
            gestureRecognizer.TappedEvent -= GestureRecognizer_TappedEvent;
            gestureRecognizer.NavigationStartedEvent -= GestureRecognizer_NavigationStartedEvent;
            gestureRecognizer.NavigationUpdatedEvent -= GestureRecognizer_NavigationUpdatedEvent;
            gestureRecognizer.NavigationCompletedEvent -= GestureRecognizer_NavigationCompletedEvent;
            gestureRecognizer.NavigationCanceledEvent -= GestureRecognizer_NavigationCanceledEvent;
        }
    }
}