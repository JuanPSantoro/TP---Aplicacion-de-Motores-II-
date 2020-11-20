using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CustomBezier))]
public class BezierSpawner : Editor
{
    CustomBezier tgt;
    private void OnEnable()
    {
        tgt = (CustomBezier)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        AddPoint(tgt);
        RemoveLastPoint(tgt);
    }
    private void OnSceneGUI()
    {

        tgt.points = new List<Vector3>();
        Draw(tgt);
        for (int i = 0; i < tgt.listPoints.Count; i++)
        {
            if (tgt.EnemiesAmount < 1)
            {
                break;
            }
            if (i < tgt.listPoints.Count - 1)
            {
                Handles.DrawBezier(tgt.listPoints[i].position, tgt.listPoints[i + 1].position, tgt.listTgt[i].position, tgt.listTgt[i + 1].position, Color.red, EditorGUIUtility.whiteTexture, 2);
                Vector3[] temp = Handles.MakeBezierPoints(tgt.listPoints[i].position, tgt.listPoints[i + 1].position, tgt.listTgt[i].position, tgt.listTgt[i + 1].position, tgt.EnemiesAmount);
                foreach (var point in temp)
                {
                    tgt.points.Add(point);
                    Handles.DrawWireCube(point, Vector3.one * 1f);
                }
            }
        }
    }
    void AddPoint(CustomBezier tgt)
    {
        if (GUILayout.Button("Add point"))
        {
            tgt.listPoints.Add(new GameObject("Point" + tgt.listPoints.Count.ToString()).transform);
            tgt.listTgt.Add(new GameObject("Tangent" + tgt.listTgt.Count.ToString()).transform);
            tgt.listPoints[tgt.listPoints.Count - 1].transform.position = tgt.listPoints[tgt.listPoints.Count - 2].transform.position + new Vector3(20, 0, 0);
            tgt.listTgt[tgt.listTgt.Count - 1].transform.position = tgt.listTgt[tgt.listTgt.Count - 2].transform.position + new Vector3(20, 0, 0);

            foreach (var item in tgt.listPoints)
            {
                item.SetParent(tgt.current.transform);
            }
            foreach (var item in tgt.listTgt)
            {
                item.SetParent(tgt.current.transform);
            }
        }

    }
    void RemoveLastPoint(CustomBezier tgt)
    {
        if (tgt.listPoints.Count > 2 || tgt.listTgt.Count > 2)
        {
            if (GUILayout.Button("Remove last point"))
            {
                int index = tgt.listPoints.Count - 1;
                DestroyImmediate(tgt.listPoints[index].gameObject);
                DestroyImmediate(tgt.listTgt[index].gameObject);
                tgt.listPoints.RemoveAt(index);
                tgt.listTgt.RemoveAt(index);
            }
        }
    }
    void Draw(CustomBezier tgt)
    {
        for (int i = 0; i < tgt.listPoints.Count; i++)
        {
            tgt.listPoints[i].position = Handles.DoPositionHandle(tgt.listPoints[i].transform.position, tgt.listPoints[i].rotation);
            tgt.listTgt[i].position = Handles.DoPositionHandle(tgt.listTgt[i].transform.position, tgt.listTgt[i].rotation);
            Handles.DrawLine(tgt.listPoints[i].position, tgt.listTgt[i].position);
        }
    }
}
