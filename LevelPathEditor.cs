using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelPath))]
public class LevelPathEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        LevelPath lp = (LevelPath)target;
        lp.shape2D = (Mesh2D)EditorGUILayout.ObjectField("Mesh2d", lp.shape2D, typeof(Mesh2D), true);
        lp.segments = EditorGUILayout.IntField("Segments", lp.segments);
        lp.length = EditorGUILayout.IntField("Length Per Segment", lp.length);
        lp.obstacesPerSegment = EditorGUILayout.IntField("Obstacles Per Segment", lp.obstacesPerSegment);
        lp.lightsPerSegment = EditorGUILayout.IntField("Lights Per Segment", lp.lightsPerSegment);
        lp.obstacle = (GameObject)EditorGUILayout.ObjectField("Obstacle Prefab", lp.obstacle, typeof(Object), true);
        lp.light = (GameObject)EditorGUILayout.ObjectField("Light Prefab", lp.light, typeof(Object), true);
        if (GUILayout.Button("Generate Level"))
        {
            lp.GenerateLevel();
        }
        if (GUILayout.Button("Destroy Level"))
        {
            lp.DestroyLevel();
        }
    }
}
