using UnityEngine;
using UnityEditor;
using System.Collections;

[@CustomEditor (typeof(graindRail))]
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

        EditorGUI.BeginChangeCheck();
        Vector3 pos = Handles.PositionHandle(gr.pathT, Quaternion.identity);

        if (EditorGUI.EndChangeCheck()){
            Undo.RecordObject(gr, "Move graindrail Path");
            gr.pathT = pos;
            gr.EditorUpdate();
        }
    }
}
