#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Warmerise.Map
{
    public class LadderModule : MonoBehaviour
    {
        public MeshRenderer mainPart;
        public MeshRenderer supportRails;
        public LadderHandleGizmo supportExtender;

        //Reference to the main ladder gizmo
        [HideInInspector]
        public LadderGizmo lg;

        public void Initialize()
        {
            if (supportExtender)
            {
                supportExtender.lm = this;
                supportExtender.previousLocalPosition = supportExtender.transform.localPosition;
            }
        }
    }
}
#endif
