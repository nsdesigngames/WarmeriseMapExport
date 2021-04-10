#if UNITY_EDITOR
using UnityEngine;
using System.Collections;

namespace Warmerise.Map
{
    public class VehicleGizmo : MonoBehaviour
    {
        Vector3[] jetGizmoPoints = { new Vector3(-1.59f, 0, 0), new Vector3(-1.59f, 0, 1.2f), new Vector3(-0.59f, 0, 2.31f), new Vector3(0.6f, 0, 2.31f), new Vector3(1.59f, 0, 1.2f),
        new Vector3(1.59f, 0, 0), new Vector3(1.1f, 0, -1.2f), new Vector3(1.04f, 0, -1.97f), new Vector3(0.8f, 0, -3), new Vector3(-0.8f, 0, -3), new Vector3(-1.04f, 0, -1.97f),
        new Vector3(-1.1f, 0, -1.2f)
        };
        Vector3 jetTopPointStart = new Vector3(0, 0, 2.31f);

        Vector3[] carGizmoPoints = { new Vector3(-1.1f, 0, 0), new Vector3(-1.35f, 0, 1.53f), new Vector3(-0.6f, 0, 2.65f), new Vector3(0.6f, 0, 2.65f), new Vector3(1.35f, 0, 1.53f),
        new Vector3(1.1f, 0, 0), new Vector3(1.1f, 0, -1.2f), new Vector3(0.8f, 0, -1.97f), new Vector3(0.26f, 0, -3), new Vector3(-0.26f, 0, -3), new Vector3(-0.8f, 0, -1.97f),
        new Vector3(-1.1f, 0, -1.2f)
        };
        Vector3 carTopPointStart = new Vector3(0, 0, 2.65f);

        bool isJetShip = false;

        void OnDrawGizmos()
        {
            if (gameObject.name.StartsWith("JetPoint") || gameObject.name.StartsWith("CarPoint"))
            {
                isJetShip = !gameObject.name.StartsWith("CarPoint");
                if (isJetShip)
                {
                    Gizmos.color = Color.cyan;
                }
                else
                {
                    Gizmos.color = Color.magenta;
                }

                if (isJetShip)
                {
                    //Jet Gizmos
                    for (int i = 0; i < jetGizmoPoints.Length; i++)
                    {
                        if (i == jetGizmoPoints.Length - 1)
                        {
                            Gizmos.DrawLine(transform.TransformPoint(jetGizmoPoints[0]), transform.TransformPoint(jetGizmoPoints[jetGizmoPoints.Length - 1]));
                        }
                        else
                        {
                            Gizmos.DrawLine(transform.TransformPoint(jetGizmoPoints[i]), transform.TransformPoint(jetGizmoPoints[i + 1]));
                        }
                    }

                    Gizmos.DrawLine(transform.TransformPoint(jetTopPointStart), transform.position);
                    Gizmos.DrawLine(transform.position, transform.position + transform.up * 2.9f);
                    Gizmos.DrawLine(transform.TransformPoint(jetTopPointStart), transform.position + transform.up * 2.9f);
                }
                else
                {
                    //Car Gizmos
                    for (int i = 0; i < carGizmoPoints.Length; i++)
                    {
                        if (i == carGizmoPoints.Length - 1)
                        {
                            Gizmos.DrawLine(transform.TransformPoint(carGizmoPoints[0]), transform.TransformPoint(carGizmoPoints[carGizmoPoints.Length - 1]));
                        }
                        else
                        {
                            Gizmos.DrawLine(transform.TransformPoint(carGizmoPoints[i]), transform.TransformPoint(carGizmoPoints[i + 1]));
                        }
                    }

                    Gizmos.DrawLine(transform.TransformPoint(carTopPointStart), transform.position);
                    Gizmos.DrawLine(transform.position, transform.position + transform.up * 1.7f);
                    Gizmos.DrawLine(transform.TransformPoint(carTopPointStart), transform.position + transform.up * 1.7f);
                }
            }
        }
    }
}
#endif
