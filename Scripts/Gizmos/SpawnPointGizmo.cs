#if UNITY_EDITOR
using UnityEngine;
using System.Collections;

namespace Warmerise.Map
{
    public class SpawnPointGizmo : MonoBehaviour
    {
        void OnDrawGizmos()
        {
            // Draw spawn point gizmo  
            if (!gameObject.name.StartsWith("RedSpawn") && !gameObject.name.StartsWith("BlueSpawn") && !gameObject.name.StartsWith("SpectatorCamera"))
            {
                Gizmos.color = Color.black;
            }
            else
            {
                if (gameObject.name.StartsWith("RedSpawn"))
                {
                    Gizmos.color = new Color(255 / 255.0f, 63 / 255.0f, 63 / 255.0f, 1);
                }
                if (gameObject.name.StartsWith("BlueSpawn"))
                {
                    Gizmos.color = new Color(79 / 255.0f, 96 / 255.0f, 225 / 255.0f, 1);
                }
                if (gameObject.name.StartsWith("SpectatorCamera"))
                {
                    Gizmos.color = Color.white;
                }
            }

            Vector3 startPoint = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);

            Gizmos.DrawSphere(startPoint, 1);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(startPoint, startPoint + transform.forward * 2);
        }
    }
}
#endif
