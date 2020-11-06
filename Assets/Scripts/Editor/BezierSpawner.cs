using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CustomBezier))]
public class BezierSpawner : Editor
{
    private void OnSceneGUI()
    {
        var tgt = (CustomBezier)target;
        tgt.pointA.position = Handles.DoPositionHandle(tgt.pointA.position, tgt.pointA.rotation);
        tgt.pointB.position = Handles.DoPositionHandle(tgt.pointB.position, tgt.pointB.rotation);
        tgt.tangentA.position = Handles.DoPositionHandle(tgt.tangentA.position, tgt.tangentA.rotation);
        tgt.tangentB.position = Handles.DoPositionHandle(tgt.tangentB.position, tgt.tangentB.rotation);

        Handles.DrawLine(tgt.pointA.position, tgt.tangentA.position);
        Handles.DrawLine(tgt.pointB.position, tgt.tangentB.position);

        Handles.DrawBezier(tgt.pointA.position, tgt.pointB.position, tgt.tangentA.position, tgt.tangentB.position, Color.red, EditorGUIUtility.whiteTexture, 2);
        tgt.points = Handles.MakeBezierPoints(tgt.pointA.position, tgt.pointB.position, tgt.tangentA.position, tgt.tangentB.position, tgt.EnemiesAmount);

        foreach (var point in tgt.points)
        {
            Handles.DrawWireCube(point, Vector3.one * 1f);
        }

    }
}
