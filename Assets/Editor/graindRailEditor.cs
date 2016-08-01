using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[@CustomEditor (typeof(graindRail))]
[CanEditMultipleObjects]
public class graindRailEditor : Editor {

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnSceneGUI()
    {
        graindRail gr = target as graindRail;

        
        gr.path[gr.editPathNum] = Handles.PositionHandle(gr.path[gr.editPathNum], Quaternion.identity);

        
    }

    private bool pathListFolding = false;
    public override void OnInspectorGUI()
    {
        graindRail gr = target as graindRail;

        gr.size = EditorGUILayout.FloatField("size", gr.size);
        gr.editPathNum = EditorGUILayout.IntField("edit", gr.editPathNum);

        List<Vector3> path = gr.path;
        int plen = path.Count;
        
        if (pathListFolding = EditorGUILayout.Foldout(pathListFolding, "Path"))
        {
            for(int i = 0; i < plen; i++)
            {
                path[i] = EditorGUILayout.Vector3Field(i.ToString(), path[i]);
            }

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("ADD", GUILayout.Width(50f)))
            {
                Undo.RecordObject(gr, "add path");
                gr.path.Add(Vector3.zero);
            }
            if (GUILayout.Button("Remove", GUILayout.Width(70f)))
            {
                Undo.RecordObject(gr, "remove path");
                gr.path.RemoveAt(gr.editPathNum);
            }
            EditorGUILayout.EndHorizontal();
        }

        
    }
    
}
