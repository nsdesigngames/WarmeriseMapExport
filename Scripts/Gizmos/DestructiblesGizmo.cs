using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Warmerise.Map
{

    public class DestructiblesGizmo : MonoBehaviour
    {
        Color mainColor = new Color(1, 1, 0, 0.5f);
        Color selectedColor = new Color(0, 1, 0, 0.5f);
        Vector3 mainGizmoDimensions = new Vector3(1f, 0.5f, 1f);

        void OnDrawGizmos()
        {
            // Convert the local coordinate values into world
            // coordinates for the matrix transformation.
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = Selection.activeGameObject != gameObject ? mainColor : selectedColor;
            Gizmos.DrawCube(new Vector3(0, mainGizmoDimensions.y * 0.5f, 0), mainGizmoDimensions);
        }
    }
}
