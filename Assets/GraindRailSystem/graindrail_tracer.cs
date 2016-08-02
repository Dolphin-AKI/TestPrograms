using UnityEngine;
using System.Collections;

public class graindrail_tracer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("onTrigEnter" + col.ToString());
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("onColEnt" + col.collider.ToString());
    }
}
