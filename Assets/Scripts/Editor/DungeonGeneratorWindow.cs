using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DungeonGeneratorWindow : EditorWindow
{
    public enum ColliderType
    {
        Box,
        Sphere,
        Capsule,
    }
    ColliderType colliderSelector;
    Vector3 SpawnPosition;
    GameObject managerDungeon;
    GameObject spawner;
    GameObject wall;
    bool boolManager;
    bool boolSpawner;
    bool boolWall;
    bool colliderAdd;
    bool layerAdd;
    bool audioSourceAdd;
    LayerMask layerType;


    [MenuItem("Window/DungeonGenerator")]
    public static void GetWindow()
    {
        GetWindow<DungeonGeneratorWindow>();
    }
    private void OnGUI()
    {
        EditorGUILayout.LabelField("DungeonGenerator");

        if (boolManager = EditorGUILayout.Foldout(boolManager, "ManagerCreate"))
        {
            Manager();
        }
        if (boolSpawner = EditorGUILayout.Foldout(boolSpawner, "SpawnCreate"))
        {
            Spawner();
        }
        if (boolWall = EditorGUILayout.Foldout(boolWall, "WallCreate"))
        {
            Wall();
        }
    }
    private void Manager()
    {
        managerDungeon = (GameObject)EditorGUILayout.ObjectField("ManagerObject", managerDungeon, typeof(GameObject), false);

        layerAdd = EditorGUILayout.Toggle("Layer assign", layerAdd);
        if (layerAdd)
            layerType = EditorGUILayout.LayerField("Layer assign", layerType);


        /*if (managerDungeon == null)
        {
            EditorGUILayout.HelpBox("Please, insert an entity you consider manager for continue", MessageType.Warning);
            GUI.enabled = false;
        }*/
        SpawnPosition = EditorGUILayout.Vector3Field("SpawnPosition", SpawnPosition);
        if (GUILayout.Button("CreateManagerDungeon"))
        {
            managerDungeon = Instantiate(managerDungeon, SpawnPosition, Quaternion.identity);
            managerDungeon.layer = layerType;
        }
    }

    private void Spawner()
    {
        spawner = (GameObject)EditorGUILayout.ObjectField("Spawner", spawner, typeof(GameObject), false);
        if (GUILayout.Button("Create Spawner"))
        {
            spawner = Instantiate(spawner, SpawnPosition, Quaternion.identity);
            spawner.layer = layerType;
        }
    }
    private void Wall()
    {
        wall = (GameObject)EditorGUILayout.ObjectField("WallObject", wall, typeof(GameObject), false);
        if (wall == null)
        {
            EditorGUILayout.HelpBox("Please, insert an Prefab you consider wall for continue", MessageType.Warning);
            GUI.enabled = false;
        }


        audioSourceAdd = EditorGUILayout.Toggle("AudioSource Add", audioSourceAdd);
        colliderAdd = EditorGUILayout.Toggle("Collider Add", colliderAdd);
        layerAdd = EditorGUILayout.Toggle("Layer assign", layerAdd);
        if (layerAdd)
            layerType = EditorGUILayout.LayerField("Layer assign", layerType);
        if (colliderAdd)
            colliderSelector = (ColliderType)EditorGUILayout.EnumPopup("Collider Selected", colliderSelector);

        SpawnPosition = EditorGUILayout.Vector3Field("SpawnPosition", SpawnPosition);

        if (GUILayout.Button("Create Wall"))
        {
            GameObject instance = Instantiate(wall, SpawnPosition, Quaternion.identity);
            if (colliderAdd)
            {
                switch (colliderSelector)
                {
                    case ColliderType.Box:
                        instance.AddComponent<BoxCollider>();
                        break;
                    case ColliderType.Sphere:
                        instance.AddComponent<SphereCollider>();
                        break;
                    case ColliderType.Capsule:
                        instance.AddComponent<CapsuleCollider>();
                        break;
                }
            }
            if (audioSourceAdd)
                instance.AddComponent<AudioSource>();
        }
    }
}
