#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Warmerise.Map
{
    public class LadderGizmo : MonoBehaviour
    {
        public bool disableRendering = false;
        public LadderHandleGizmo top;
        public LadderModule ladderModulePrefab;
        public BoxCollider ladderAreaCollider;

        [HideInInspector]
        public List<LadderModule> ladderModules = new List<LadderModule>();
        float previousTopPositionY = 0;
        Color mainColor = new Color(1, 1, 0, 0.5f);
        Color selectedColor = new Color(0, 1, 0, 0.5f);
        Vector3 mainGizmoDimensions = new Vector3(3, 0.5f, 4);

        [HideInInspector]
        public GUIStyle handleTextStyle = new GUIStyle();

        bool previousDisableRendering;

        public void InitializeStyles()
        {
            handleTextStyle.normal.textColor = Color.black;
            handleTextStyle.fontSize = 12;
            handleTextStyle.alignment = TextAnchor.MiddleCenter;
            handleTextStyle.fontStyle = FontStyle.Bold;
            previousDisableRendering = disableRendering;
        }

        void OnDrawGizmos()
        {
            if (!top)
                return;

            if(previousTopPositionY != top.transform.localPosition.y)
            {
                previousTopPositionY = top.transform.localPosition.y;

                RebuildLadder();
            }

            Handles.Label(transform.position + transform.forward + transform.up, "Climb Side", handleTextStyle);

            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, ladderAreaCollider.size.y, 0));

            // Convert the local coordinate values into world
            // coordinates for the matrix transformation.
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = Selection.activeGameObject != gameObject ? mainColor : selectedColor;
            Gizmos.DrawCube(new Vector3(0, mainGizmoDimensions.y * 0.5f, 0), mainGizmoDimensions);

            if(previousDisableRendering != disableRendering)
            {
                previousDisableRendering = disableRendering;

                RebuildLadder();
            }
        }

        public void RebuildLadder()
        {
            if (!ladderModulePrefab || !ladderAreaCollider)
                return;

            //Build ladder
            Bounds bounds = ladderModulePrefab.mainPart.bounds;
            int elements = Mathf.FloorToInt(top.transform.localPosition.y / bounds.size.y);
            bool upwards = elements > 0;
            elements = Mathf.Abs(elements);

            //Update support rails
            if (!disableRendering)
            {
                //Check if any elements need to be rebuilt
                for (int i = 0; i < ladderModules.Count; i++)
                {
                    if (ladderModules[i] == null)
                    {
                        ladderModules[i] = BuildModule(upwards, bounds, i);
                    }
                    else if (ladderModules[i].mainPart == null)
                    {
                        //Regenerate
                        DestroyImmediate(ladderModules[i].gameObject);
                        ladderModules[i] = BuildModule(upwards, bounds, i);
                    }
                }

                if (ladderModules.Count > elements)
                {
                    //Remove elements
                    for (int i = ladderModules.Count - 1; i >= elements; i--)
                    {
                        DestroyImmediate(ladderModules[i].gameObject);
                        ladderModules.RemoveAt(i);
                    }
                }
                else if (ladderModules.Count < elements)
                {
                    //Add elements
                    int elementsToCreate = elements - ladderModules.Count;
                    for (int i = 0; i < elementsToCreate; i++)
                    {
                        ladderModules.Add(BuildModule(upwards, bounds, ladderModules.Count));
                    }
                }

                for (int i = 0; i < ladderModules.Count; i++)
                {
                    if (ladderModules[i].supportExtender != null && ladderModules[i].supportRails != null)
                    {
                        if (Mathf.Abs(ladderModules[i].supportExtender.previousLocalPosition.z) > bounds.size.z * 2)
                        {
                            ladderModules[i].supportRails.enabled = true;
                            Vector3 localScale = ladderModules[i].supportRails.transform.localScale;
                            localScale.y = -ladderModules[i].supportExtender.previousLocalPosition.z;
                            ladderModules[i].supportRails.transform.localScale = localScale;
                        }
                        else
                        {
                            //Disable support rail
                            ladderModules[i].supportRails.enabled = false;
                        }
                    }
                }
            }
            else
            {
                //Destroy ladder modules
                for (int i = 0; i < ladderModules.Count; i++)
                {
                    if (ladderModules[i] != null)
                    {
                        DestroyImmediate(ladderModules[i].gameObject);
                    }
                }
                ladderModules.Clear();
            }

            ladderAreaCollider.center = new Vector3(0, (elements * bounds.size.y) * 0.5f * (upwards ? 1 : -1), (bounds.size.z * 0.5f) + 0.105f);
            ladderAreaCollider.size = new Vector3(!disableRendering ? bounds.size.x : ladderAreaCollider.size.x, ladderAreaCollider.center.y * 2, 0.211f);
        }

        LadderModule BuildModule(bool upwards, Bounds bounds, int index)
        {
            LadderModule lm = Instantiate(ladderModulePrefab, transform);
            lm.transform.localRotation = Quaternion.identity;
            lm.transform.localPosition = new Vector3(0, index * bounds.size.y * (upwards ? 1 : -1) + (upwards ? 0 : -bounds.size.y), 0);
            lm.lg = this;
            lm.Initialize();
            return lm;
        }
    }
}
#endif
