using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Warmerise.Map
{
    public class KeyObjects : MonoBehaviour
    {
        [MenuItem("Warmerise/Workflow/Create '_AllPoints' Object")]
        static void SpawnKeyObjects()
        {
            if(GameObject.Find("_AllPoints") == null)
            {
                //Create object root
                GameObject keyObjectsRoot = new GameObject("_AllPoints");
                //Move in front of Scene Camera
                Camera sceneCamera = SceneView.lastActiveSceneView.camera;
                keyObjectsRoot.transform.position = sceneCamera.transform.position + sceneCamera.transform.forward * 25;
                //Select root object
                Selection.objects = new Object[] { keyObjectsRoot };

                GameObject controlObject = null;

                //Control objects
                string[] controlObjects = new string[] { "_InvisibleWalls" , "_Snow/Dirt", "_MetalObjects" };
                for(int i = 0; i < controlObjects.Length; i++)
                {
                    controlObject = new GameObject(controlObjects[i]);
                    controlObject.transform.parent = keyObjectsRoot.transform;
                    controlObject.transform.localPosition = Vector3.zero;
                }

                //Trigger damage
                controlObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                controlObject.name = "TriggerDamage";
                controlObject.transform.parent = keyObjectsRoot.transform;
                controlObject.transform.localPosition = new Vector3(0, -7.1f, 0);
                controlObject.transform.localScale = new Vector3(100, 1, 100);
                controlObject.GetComponent<MeshRenderer>().sharedMaterial = Resources.Load("TriggerDamageMaterial") as Material;

                //Armory Spot 1
                controlObject = new GameObject("Armory");
                controlObject.transform.parent = keyObjectsRoot.transform;
                controlObject.transform.localPosition = new Vector3(-3, 0, -6.5f);
                controlObject.AddComponent<ArmoryGizmo>();

                //Armory Spot 2
                controlObject = new GameObject("Armory");
                controlObject.transform.parent = keyObjectsRoot.transform;
                controlObject.transform.localPosition = new Vector3(3, 0, -6.5f);
                controlObject.AddComponent<ArmoryGizmo>();

                //Jet point
                controlObject = new GameObject("JetPoint");
                controlObject.transform.parent = keyObjectsRoot.transform;
                controlObject.transform.localPosition = new Vector3(3, 0, 6.5f);
                controlObject.AddComponent<VehicleGizmo>();

                //Car point
                controlObject = new GameObject("CarPoint");
                controlObject.transform.parent = keyObjectsRoot.transform;
                controlObject.transform.localPosition = new Vector3(-3, 0, 6.5f);
                controlObject.AddComponent<VehicleGizmo>();

                //Welcome camera
                controlObject = new GameObject("MultiplayerMenu");
                controlObject.transform.parent = keyObjectsRoot.transform;
                controlObject.transform.localPosition = new Vector3(0, 5, 0);
                controlObject.AddComponent<MenuGizmo>();

                //Spawn points
                controlObject = new GameObject("SpawnPoints");
                controlObject.transform.parent = keyObjectsRoot.transform;
                controlObject.transform.localPosition = Vector3.zero;
                for(int team = 0; team < 2; team++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        GameObject spawnPoint = new GameObject(team == 0 ? "RedSpawn" : "BlueSpawn");
                        spawnPoint.transform.parent = controlObject.transform;
                        spawnPoint.transform.localPosition = new Vector3((2 + (i * 2)) * (team == 0 ? -1 : 1), 0, 0);
                        spawnPoint.AddComponent<SpawnPointGizmo>();
                    }
                }

                //Spectator camera
                controlObject = new GameObject("SpectatorCamera");
                controlObject.transform.parent = keyObjectsRoot.transform;
                controlObject.transform.localPosition = new Vector3(0, 0, -11.5f);
                controlObject.AddComponent<SpawnPointGizmo>();

                // Register root object for undo.
                Undo.RegisterCreatedObjectUndo(keyObjectsRoot, "Create key objects");
            }
            else
            {
                EditorUtility.DisplayDialog("Create Key Objects", "The object '_AllPoints' is already present in current Scene.", "Ok");
            }
        }

        [MenuItem("Warmerise/Workflow/Create New Ladder")]
        static void CreateNewLadder()
        {
            GameObject _AllPoints = GameObject.Find("_AllPoints");
            if (_AllPoints != null)
            {
                //Create Ladder container object if there none
                Transform laddersContainer = _AllPoints.transform.Find("Ladders");
                if (laddersContainer == null)
                {
                    laddersContainer = (new GameObject("Ladders")).transform;
                    laddersContainer.parent = _AllPoints.transform;
                    laddersContainer.localPosition = Vector3.zero;
                    laddersContainer.localRotation = Quaternion.identity;
                }

                //Create new ladder
                //Create object root
                GameObject ladderObject = new GameObject("MetalLadder");
                ladderObject.transform.parent = laddersContainer;
                //Move in front of Scene Camera
                Camera sceneCamera = SceneView.lastActiveSceneView.camera;
                ladderObject.transform.position = sceneCamera.transform.position + sceneCamera.transform.forward * 25;
                //Select root object
                Selection.objects = new Object[] { ladderObject };

                GameObject topObject = new GameObject("Top");
                topObject.transform.parent = ladderObject.transform;
                topObject.transform.localPosition = new Vector3(0, 12.14f, 0);
                LadderHandleGizmo lhg = topObject.AddComponent<LadderHandleGizmo>();
                lhg.gizmoType = LadderHandleGizmo.GizmoType.Main;

                LadderGizmo lg = ladderObject.AddComponent<LadderGizmo>();
                lg.top = lhg;
                lg.ladderAreaCollider = ladderObject.AddComponent<BoxCollider>();
                GameObject ladderModulePrefab = Resources.Load("LadderModule") as GameObject; //Load ladder module prefab
                lg.ladderModulePrefab = ladderModulePrefab.GetComponent<LadderModule>();
                if (!lg.ladderModulePrefab)
                {
                    Debug.LogError("Ladder module is missing.");
                }
                lg.InitializeStyles();
                lg.RebuildLadder();

                // Register root object for undo.
                Undo.RegisterCreatedObjectUndo(ladderObject, "Create ladder");
            }
            else
            {
                EditorUtility.DisplayDialog("Create New Ladder", "The object '_AllPoints' not found, please initialize it first.", "Ok");
            }
        }

        [MenuItem("Warmerise/Workflow/Create New Door")]
        static void CreateNewDoor()
        {
            GameObject _AllPoints = GameObject.Find("_AllPoints");
            if (_AllPoints != null)
            {
                //Create Door container object if there none
                Transform doorContainer = _AllPoints.transform.Find("Doors");
                if (doorContainer == null)
                {
                    doorContainer = (new GameObject("Doors")).transform;
                    doorContainer.parent = _AllPoints.transform;
                    doorContainer.localPosition = Vector3.zero;
                    doorContainer.localRotation = Quaternion.identity;
                }

                GameObject doorPrefab = Resources.Load("SimpleDoor") as GameObject;

                if (doorPrefab != null)
                {
                    GameObject instantiatedDoor = Instantiate(doorPrefab, _AllPoints.transform);
                    instantiatedDoor.transform.parent = doorContainer;
                    //Move in front of Scene Camera
                    Camera sceneCamera = SceneView.lastActiveSceneView.camera;
                    instantiatedDoor.transform.position = sceneCamera.transform.position + sceneCamera.transform.forward * 10;
                    //Select root object
                    Selection.objects = new Object[] { instantiatedDoor };

                    // Register root object for undo.
                    Undo.RegisterCreatedObjectUndo(instantiatedDoor, "Create door");
                }
                else
                {
                    EditorUtility.DisplayDialog("Create New Door", "Resource with the name 'SimpleDoor' is missing, unable to create a door.", "Ok");
                }
            }
            else
            {
                EditorUtility.DisplayDialog("Create New Door", "The object '_AllPoints' not found, please initialize it first.", "Ok");
            }
        }

        [MenuItem("Warmerise/Workflow/Create New Day Time Water")]
        static void CreateWaterDayTime()
        {
            CreateWater("WaterDayTime", true);
        }

        [MenuItem("Warmerise/Workflow/Create New Night Time Water")]
        static void CreateWaterNightTime()
        {
            CreateWater("WaterNightTime", true);
        }

        static void CreateWater(string waterPlaneName, bool checkWater)
        {
            GameObject waterPrefab = Resources.Load(waterPlaneName) as GameObject;

            if (waterPrefab != null)
            {
                Warmerise.Map.WMWaterScript waterPlane = checkWater ? GameObject.FindObjectOfType<Warmerise.Map.WMWaterScript>() : null;

                if(waterPlane == null || EditorUtility.DisplayDialog("Water Plane Detected",
                "There's already a Water plane in the Scene, placing multiple water planes may negatively impact performance, do you wish to add new water plane anyway?", "Yes", "No"))
                {
                    GameObject instantiatedWater = Instantiate(waterPrefab);
                    instantiatedWater.name = waterPlaneName;
                    //Move in front of Scene Camera
                    Camera sceneCamera = SceneView.lastActiveSceneView.camera;
                    instantiatedWater.transform.position = sceneCamera.transform.position + sceneCamera.transform.forward * 50;
                    //Select root object
                    Selection.objects = new Object[] { instantiatedWater };

                    // Register root object for undo.
                    Undo.RegisterCreatedObjectUndo(instantiatedWater, "Create custom object");
                }
                else
                {
                    if(waterPlane != null)
                    {
                        //Select water plane object
                        Selection.objects = new Object[] { waterPlane.gameObject };
                    }
                }
            }
            else
            {
                EditorUtility.DisplayDialog("Error", "Prefab '" + waterPlaneName + "' does not exist in Resources folder.", "Ok");
            }
        }

        [MenuItem("Warmerise/Workflow/Create Ball Mode Base")]
        static void CreateBallModeBase()
        {
            if(GameObject.Find("/_BallModeBaseMap") == null)
            {
                CreateWater("_BallModeBaseMap", false);
            }
            else
            {
                EditorUtility.DisplayDialog("Ball Mode Base", "Object '_BallModeBaseMap' already exist in the Scene.", "Ok");
            }
           
        }

        [MenuItem("Warmerise/Documentation/Tutorial PDF")]
        static void OpenTutorialPDF()
        {
            Object pdf = AssetDatabase.LoadAssetAtPath("Packages/com.warmerise.map.export/Tutorials/CustomMapTutorial.pdf", typeof(Object));

            if(pdf == null)
            {
                //Try local path
                pdf = AssetDatabase.LoadAssetAtPath("Assets/WarmeriseMapExport/Tutorials/CustomMapTutorial.pdf", typeof(Object));
            }
            string pathToPDF = System.IO.Path.Combine(System.IO.Directory.GetParent(Application.dataPath).FullName, AssetDatabase.GetAssetPath(pdf));
            if (System.IO.File.Exists(pathToPDF))
            {
                System.Diagnostics.Process.Start(pathToPDF);
                Debug.Log("Opening PDF, please wait...");
            }
            else
            {
                Debug.LogError("CustomMapTutorial.pdf does not exist.");
            }
        }
    }
}
