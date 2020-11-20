using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpawnEnemy))]
public class SpawnControlInspector : Editor
{
    CustomBezier bezierInfo;
    GameObject enemyType;
    private void OnEnable()
    {
        SpawnEnemy temp = (SpawnEnemy)target;
        bezierInfo = temp.gameObject.GetComponent<CustomBezier>();
    }
    public override void OnInspectorGUI()
    {
        if (bezierInfo.EnemiesAmount < 1)
        {
            bezierInfo.EnemiesAmount = 1;
        }
        enemyType = (GameObject)EditorGUILayout.ObjectField("Enemy", enemyType, typeof(GameObject), false);
        bezierInfo.EnemiesAmount = Mathf.Max(1, EditorGUILayout.IntField("EnemiesAmount", bezierInfo.EnemiesAmount));
        if (enemyType == null)
        {
            EditorGUILayout.HelpBox("You need a reference object to generate", MessageType.Warning);
            GUI.enabled = false;
        }

        if (GUILayout.Button("CheckSpawns"))
        {
            CheckSpawn();
        }
    }

    public void CheckSpawn()
    {
        for (int i = bezierInfo.points.Count - 1; i > 0; i--)
        {
            if (i - 1 >= 0)
            {
                if (bezierInfo.points[i] == bezierInfo.points[i - 1])
                {
                    bezierInfo.points.RemoveAt(i);
                }
            }
        }
        foreach (var point in bezierInfo.points)
        {
            GameObject instance = Instantiate(enemyType, (new Vector3(point.x, point.y, point.z)), Quaternion.identity);
        }
    }
}
