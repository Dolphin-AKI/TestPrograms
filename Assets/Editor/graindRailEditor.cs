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

        EditorGUI.BeginChangeCheck();
        gr.editPathNum = EditorGUILayout.IntSlider("edit",gr.editPathNum,0,gr.path.Count - 1);
        if(gr.editPathNum >= gr.path.Count)
        {
            gr.editPathNum = gr.path.Count - 1;
        }

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Before", GUILayout.Width(60f)))
        {
            gr.editPathNum--;
            

        }
        if (GUILayout.Button("Next", GUILayout.Width(60f)))
        {
            gr.editPathNum++;
            
            
        }

        EditorGUILayout.EndHorizontal();

        List<Vector3> path = gr.path;
        int plen = path.Count;
        
        if (pathListFolding = EditorGUILayout.Foldout(pathListFolding, "Path"))
        {
            for(int i = 0; i < plen; i++)
            {
                path[i] = EditorGUILayout.Vector3Field(i.ToString(), path[i]);
            }            
        }

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("ADD", GUILayout.Width(50f)))
        {
            Undo.RecordObject(gr, "add path");
            gr.path.Insert(gr.editPathNum, gr.path[gr.editPathNum]);
        }
        if (GUILayout.Button("Remove", GUILayout.Width(70f)))
        {
            Undo.RecordObject(gr, "remove path");
            gr.path.RemoveAt(gr.editPathNum);
        }
        EditorGUILayout.EndHorizontal();



        if (gr.editPathNum >= gr.path.Count)
        {
            gr.editPathNum = gr.path.Count - 1;
        }
        else if (gr.editPathNum < 0)
        {
            gr.editPathNum = 0;
        }

        if (EditorGUI.EndChangeCheck())
        {
            SceneView.RepaintAll();
        }

    }
    
}
