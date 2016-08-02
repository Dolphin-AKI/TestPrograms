

using UnityEngine;
using System.Collections.Generic;


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class graindRail : MonoBehaviour {

    public float size = 1f;
    public int pathLength;
    public List<Vector3> path;
    public int editPathNum;



    private Vector3[] vertics;
    private int[] triangles;
    //public Vector3 pathT;

	// Use this for initialization
	void Start () {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        vertics = new Vector3[path.Count * 4];
        triangles = new int[(path.Count - 1) * 24 + 12];

        int _trianglecnt = 0;
        for(int i = 0; i < path.Count; i++)
        {
            int tr_i = i * 4;

            //次の制御点への方向取得　（ない場合現制御点）
            Vector3 towardNextPath = path[i];
            if (i < path.Count - 1)
            {
                towardNextPath = (path[i + 1] - path[i]).normalized;
            }
            //面法線を次のパスへ向ける回転軸と回転角度を取得
            Vector3 rotaxis = Vector3.Cross(Vector3.back, towardNextPath).normalized;
            float rotAngle = Vector3.Angle(Vector3.back, towardNextPath);


            vertics[tr_i] = new Vector3(size / 1.414f, size / 1.414f, 0);            
            vertics[tr_i] = Quaternion.AngleAxis(rotAngle, rotaxis) * vertics[tr_i]  + path[i];

            vertics[tr_i + 1] = new Vector3(size / 1.414f, -size / 1.414f, 0);
            vertics[tr_i + 1] = Quaternion.AngleAxis(rotAngle, rotaxis) * vertics[tr_i + 1] + path[i];

            vertics[tr_i + 2] = new Vector3(-size / 1.414f, -size / 1.414f, 0);
            vertics[tr_i + 2] = Quaternion.AngleAxis(rotAngle, rotaxis) * vertics[tr_i + 2] + path[i];

            vertics[tr_i + 3] = new Vector3(-size / 1.414f, size / 1.414f, 0);
            vertics[tr_i + 3] = Quaternion.AngleAxis(rotAngle, rotaxis) * vertics[tr_i + 3] + path[i];


            if (i < path.Count - 1)
            {
                for (int j = 0; j < 3; j++)
                {
                    tr_i = i * 4 + j;
                    triangles[_trianglecnt++] = tr_i;                    
                    triangles[_trianglecnt++] = tr_i + 1;
                    triangles[_trianglecnt++] = tr_i + 4;

                    triangles[_trianglecnt++] = tr_i + 1;                                       
                    triangles[_trianglecnt++] = tr_i + 4 + 1;
                    triangles[_trianglecnt++] = tr_i + 4;

#if debug
                    Debug.Log(_trianglecnt);
#endif
                }
                    tr_i += 1;
                    triangles[_trianglecnt++] = tr_i;
                    triangles[_trianglecnt++] = tr_i - 3;  
                    triangles[_trianglecnt++] = tr_i + 4;
                    

                    triangles[_trianglecnt++] = tr_i - 3;                                       
                    triangles[_trianglecnt++] = tr_i + 1;
                    triangles[_trianglecnt++] = tr_i + 4;
            }

           


        }
        //蓋、最後のほう
        triangles[_trianglecnt++] = (path.Count - 1) * 4;        
        triangles[_trianglecnt++] = (path.Count - 1) * 4 + 3;
        triangles[_trianglecnt++] = (path.Count - 1) * 4 + 1;
        triangles[_trianglecnt++] = (path.Count - 1) * 4 + 1;        
        triangles[_trianglecnt++] = (path.Count - 1) * 4 + 3;
        triangles[_trianglecnt++] = (path.Count - 1) * 4 + 2;

        //蓋、最初のほう
        triangles[_trianglecnt++] = 0;        
        triangles[_trianglecnt++] = 3;
        triangles[_trianglecnt++] = 1;
        triangles[_trianglecnt++] = 1;        
        triangles[_trianglecnt++] = 3;
        triangles[_trianglecnt++] = 2;




        mesh.vertices = this.vertics;
        mesh.triangles = this.triangles;

#if debug
        Debug.Log("path:" + path.Count);
        Debug.Log("vertics:" + vertics.Length);
        Debug.Log("triangles:" + triangles.Length);
        string _strd = "";
        for(int i_d = 0; i_d < triangles.Length; i_d++)
        {
            _strd += "[" + triangles[i_d] + "]";
        }
        Debug.Log(_strd);
#endif


        MeshCollider meshcollider = GetComponent<MeshCollider>();
        meshcollider.sharedMesh = mesh;
        //meshcollider.convex = true;
        //meshcollider.isTrigger = true;



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

        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(path[editPathNum], 1);


        for (int i = 0; i < path.Count-1; i++)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(path[i], path[i + 1]);
        }

    }
}
