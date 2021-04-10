#if UNITY_EDITOR
using UnityEngine;
using System.Collections;

namespace Warmerise.Map
{
    public class ArmoryGizmo : MonoBehaviour
    {
        Vector3[] positions = new Vector3[30];

        void OnDrawGizmos()
        {
            if (Application.isEditor)
            {
                Vector3 centrePos = transform.position;
                for (int pointNum = 0; pointNum < positions.Length; pointNum++)
                {
                    // "i" now represents the progress around the circle from 0-1
                    // we multiply by 1.0 to ensure we get a fraction as a result.
                    float i = (float)(pointNum * 2) / positions.Length;

                    // get the angle for this step (in radians, not degrees)
                    float angle = i * Mathf.PI * 2;

                    // the X & Y position for this angle are calculated using Sin & Cos
                    float x = Mathf.Sin(angle) * 2.3f;
                    float z = Mathf.Cos(angle) * 2.3f;

                    Vector3 pos = new Vector3(x, 0, z) + centrePos;
                    positions[pointNum] = pos;
                }

                Gizmos.color = Color.yellow;
                for (int i = 0; i < positions.Length; i++)
                {
                    if (i == positions.Length - 1)
                    {
                        Gizmos.DrawLine(positions[0], positions[positions.Length - 1]);
                    }
                    else
                    {
                        Gizmos.DrawLine(positions[i], positions[i + 1]);
                    }
                }
                Gizmos.DrawLine(transform.position, new Vector3(centrePos.x, centrePos.y + 3, centrePos.z));
            }
        }
    }
}
#endif
