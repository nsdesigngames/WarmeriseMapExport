using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Warmerise.Map
{
    public class DoorGizmo : MonoBehaviour
    {
        public Transform doorHinge;
        public enum OpenDirection { Up, Down, Left, Right };
        public OpenDirection openDirection = OpenDirection.Up;

        OpenDirection previousOpenDirection = OpenDirection.Down;

        static Vector3[] arrowGizmoPoints = {
            new Vector3(0, 1, 0), new Vector3(0, 0, 1), new Vector3(0, 0, 0.25f), new Vector3(0, -2, 0.25f), new Vector3(0, -2, -0.25f), new Vector3(0, 0, -0.25f), new Vector3(0, 0, -1)
        };

        Color mainColor = new Color(1, 1, 0, 0.5f);
        Color selectedColor = new Color(0, 1, 0, 0.5f);

        Vector3 mainGizmoDimensions = new Vector3(2, 0.5f, 1);

        void OnDrawGizmos()
        {
            if (!doorHinge)
                return;

            if(previousOpenDirection != openDirection)
            {
                previousOpenDirection = openDirection;

                if(openDirection == OpenDirection.Up)
                {
                    doorHinge.localPosition = new Vector3(0.25f, 3.45f, 0);
                    doorHinge.localEulerAngles = Vector3.zero;
                }
                else if (openDirection == OpenDirection.Down)
                {
                    doorHinge.localPosition = new Vector3(0.25f, -0.05f, 0);
                    doorHinge.localEulerAngles = new Vector3(180, 0, 0);
                }
                else if (openDirection == OpenDirection.Right)
                {
                    doorHinge.localPosition = new Vector3(0.25f, 1.725f, 1);
                    doorHinge.localEulerAngles = new Vector3(90, 0, 0);
                }
                else if (openDirection == OpenDirection.Left)
                {
                    doorHinge.localPosition = new Vector3(0.25f, 1.725f, -1);
                    doorHinge.localEulerAngles = new Vector3(-90, 0, 0);
                }
            }

            //Draw Gizmos
            Gizmos.color = Color.green;

            for (int i = 0; i < arrowGizmoPoints.Length; i++)
            {
                if (i == arrowGizmoPoints.Length - 1)
                {
                    Gizmos.DrawLine(doorHinge.TransformPoint(arrowGizmoPoints[0]), doorHinge.TransformPoint(arrowGizmoPoints[arrowGizmoPoints.Length - 1]));
                }
                else
                {
                    Gizmos.DrawLine(doorHinge.TransformPoint(arrowGizmoPoints[i]), doorHinge.TransformPoint(arrowGizmoPoints[i + 1]));
                }
            }

            // Convert the local coordinate values into world
            // coordinates for the matrix transformation.
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = Selection.activeGameObject != gameObject ? mainColor : selectedColor;
            Gizmos.DrawCube(Vector3.zero, mainGizmoDimensions);
        }
    }
}
