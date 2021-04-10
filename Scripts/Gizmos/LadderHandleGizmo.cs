#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Warmerise.Map
{
    public class LadderHandleGizmo : MonoBehaviour
    {
        public enum GizmoType { Main, Secondary }
        public GizmoType gizmoType = GizmoType.Main;

        Color mainColor = new Color(1, 1, 0, 0.5f);
        Color secondaryColor = new Color(0, 1, 1, 0.5f);
        Color selectedColor = new Color(0, 1, 0, 0.5f);

        Vector3 mainGizmoDimensions = new Vector3(2, 0.5f, 1);
        Vector3 secondaryGizmoDimensions = new Vector3(0.74f, 0.34f, 0.34f);

        //Track position change
        [HideInInspector]
        public LadderModule lm;
        [HideInInspector]
        public Vector3 previousLocalPosition;

        void OnDrawGizmos()
        {
            // Convert the local coordinate values into world
            // coordinates for the matrix transformation.
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = Selection.activeGameObject != gameObject ? (gizmoType == GizmoType.Main ? mainColor : secondaryColor) : selectedColor;
            if(gizmoType == GizmoType.Main)
            {
                Gizmos.DrawCube(Vector3.zero, mainGizmoDimensions);
            }
            else
            {
                Gizmos.DrawCube(Vector3.zero, secondaryGizmoDimensions);

                if(previousLocalPosition != transform.localPosition)
                {
                    previousLocalPosition = transform.localPosition;

                    if(lm && lm.lg)
                    {
                        //Rebuild ladder
                        lm.lg.RebuildLadder();
                    }
                }
            }
        }
    }
}
#endif
