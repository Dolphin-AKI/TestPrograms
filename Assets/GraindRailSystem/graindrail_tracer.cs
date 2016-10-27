using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class graindrail_tracer : MonoBehaviour {

    public float speed = 0;
    private Rigidbody rgbd;

    //レールからのオフセット値
    public Vector3 railOffset = new Vector3(0, 0.5f, 0);
    //接続しているグラインドレール
    private graindRail ConnectedGraindRail = null;
    private bool OnGraindRail = false;
    //現在のノード
    private int node;
    //次ノードのポジション
    private Vector3 nextNodepos;
    //進行方向（逆行かどうか）
    private bool reverse = false;

	// Use this for initialization
	void Start () {

        rgbd = this.GetComponent<Rigidbody>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        



        if (OnGraindRail)
        { 

            this.transform.forward = (nextNodepos - this.transform.position).normalized;
            this.transform.position += this.transform.forward * speed * Time.deltaTime;

            if((nextNodepos - this.transform.position).magnitude < 1.5f)
            {
                getNextNode();
            }
        }

    }


    private void getNextNode()
    {
        

        if (reverse)
        {
            node -= 1;
            if(node < 0)
            {
                TakeOffRail();
                return;
            }
        }
        else
        {
            node += 1;
            if(node >= ConnectedGraindRail.path.Count - 1)
            {
                TakeOffRail();
                return;
            }
        }


            nextNodepos = ConnectedGraindRail.getNextNodePosition(node, reverse) + railOffset;
        
        
    }



    //こっちが反応してる
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "GraindRail")
        {

            ConnectedGraindRail = col.gameObject.GetComponent<graindRail>();
            OnGraindRail = true;

            node = ConnectedGraindRail.NearPath(this.transform.position);
            reverse = ConnectedGraindRail.RailForward(node, this.transform.forward);
            nextNodepos = ConnectedGraindRail.getNextNodePosition(node, reverse) + railOffset;

            rgbd.isKinematic = true;
        }
    }

    /// <summary>
    /// レールから離脱
    /// </summary>
    private void TakeOffRail()
    {
        ConnectedGraindRail = null;
        OnGraindRail = false;


        rgbd.isKinematic = false;

        rgbd.velocity = this.transform.forward * speed;
    }
}
