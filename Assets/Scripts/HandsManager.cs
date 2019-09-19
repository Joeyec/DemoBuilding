// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using HoloToolkit.Unity.InputModule;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

namespace HoloToolkit.Unity
{
    /// <summary>
    /// HandsManager determines if the hand is currently detected or not.
    /// </summary>
    public partial class HandsManager : Singleton<HandsManager>
    {
        /// <summary>
        /// HandDetected tracks the hand detected state.
        /// Returns true if the list of tracked hands is not empty.
        /// </summary>
        public bool HandDetected
        {
            get { return trackedHands.Count > 0; }
        }

        private HashSet<uint> trackedHands = new HashSet<uint>();

        public GameObject FocusedGameObject { get; private set; }

        void Awake()
        {
            InteractionManager.InteractionSourceDetectedLegacy+= InteractionManager_SourceDetected;
            
            InteractionManager.InteractionSourceLostLegacy += InteractionManager_SourceLost;
            //来源被按下  
            InteractionManager.InteractionSourcePressedLegacy += InteractionManager_SourcePressed;
            //被释放  
            InteractionManager.InteractionSourceReleasedLegacy+= InteractionManager_SourceReleased;

            FocusedGameObject = null;
        }

        //手势释放时，将被关注的物体置空  
        private void InteractionManager_SourceReleased(InteractionSourceState state)
        {
            FocusedGameObject = null;
        }
        //识别到手指按下时，将凝视射线关注的物体置为当前手势操作的对象  
        private void InteractionManager_SourcePressed(InteractionSourceState state)
        {
            if (GazeManager.Instance.HitObject!= null)
            {
                FocusedGameObject = GazeManager.Instance.HitObject;
            }
        }

        private void InteractionManager_SourceDetected(InteractionSourceState state)
        {
            // Check to see that the source is a hand.
            if (state.source.kind != InteractionSourceKind.Hand)
            {
                return;
            }

            trackedHands.Add(state.source.id);
        }

        private void InteractionManager_SourceLost(InteractionSourceState state)
        {
            // Check to see that the source is a hand.
            if (state.source.kind != InteractionSourceKind.Hand)
            {
                return;
            }

            if (trackedHands.Contains(state.source.id))
            {
                trackedHands.Remove(state.source.id);
            }
            FocusedGameObject = null;
        }

        void OnDestroy()
        {
            InteractionManager.InteractionSourceDetectedLegacy-= InteractionManager_SourceDetected;
            InteractionManager.InteractionSourceLostLegacy-= InteractionManager_SourceLost;

            InteractionManager.InteractionSourceReleasedLegacy-= InteractionManager_SourceReleased;
            InteractionManager.InteractionSourcePressedLegacy-= InteractionManager_SourcePressed;
        }
    }
}