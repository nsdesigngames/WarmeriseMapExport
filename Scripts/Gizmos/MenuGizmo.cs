#if UNITY_EDITOR
using UnityEngine;
using System.Collections;

namespace Warmerise.Map
{
    public class MenuGizmo : MonoBehaviour
    {
        Vector3[] drawPoints = { new Vector3(2, 2, 3.94f), new Vector3(-2, 2, 3.94f), new Vector3(-2, -2, 3.94f), new Vector3(2, -2, 3.94f) };

        void OnDrawGizmos()
        {
            if (gameObject.name.StartsWith("MultiplayerMenu"))
            {
                Gizmos.color = Color.white;
                for (int i = 0; i < drawPoints.Length; i++)
                {
                    if (i == drawPoints.Length - 1)
                    {
                        Gizmos.DrawLine(transform.TransformPoint(drawPoints[0]), transform.TransformPoint(drawPoints[drawPoints.Length - 1]));
                    }
                    else
                    {
                        Gizmos.DrawLine(transform.TransformPoint(drawPoints[i]), transform.TransformPoint(drawPoints[i + 1]));
                    }
                    Gizmos.DrawLine(transform.position, transform.TransformPoint(drawPoints[i]));
                }
            }
        }
    }
}
#endif
