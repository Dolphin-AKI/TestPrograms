using UnityEngine;
using System.Collections.Generic;

public class graindRail : MonoBehaviour {

    public int pathLength;
    public List<Vector3> path;
    public int editPathNum;

    
    //public Vector3 pathT;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void EditorUpdate()
    {
        
    }

    private void OnDrawGizmos()
    {

        foreach(Vector3 pos in path)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(pos, 1);
        }

        for(int i = 0; i < path.Count-1; i++)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(path[i], path[i + 1]);
        }

    }
}
