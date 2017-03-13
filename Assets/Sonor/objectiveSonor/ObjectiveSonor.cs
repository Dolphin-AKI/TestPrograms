using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveSonor : MonoBehaviour {

    public enum ObjectType
    {
        none,
        enemy,
        item,
        friend,
    }
    public ObjectType Type = ObjectType.none;
    public Color color;
    public float smoothness;
    public float metalic;

    private float _glowtime = 1.5f;
    public float GlowTime { get { return _glowtime; } set { _glowtime = value; } }

    private float timer = 0;

    private float _amplitude = 0;

    [SerializeField]
    Shader shader;

    //shader properties
    int ColorID;
    int SmoothnessID;
    int MetalicID;
    int GlowID;


    private void Awake()
    {
        ColorID = Shader.PropertyToID("_Color");
        SmoothnessID = Shader.PropertyToID("_Glossiness");
        MetalicID = Shader.PropertyToID("_Metallic");
        GlowID = Shader.PropertyToID("_Glow");
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Shader.SetGlobalColor(ColorID, color);
        Shader.SetGlobalFloat(SmoothnessID, smoothness);
        Shader.SetGlobalFloat(MetalicID, metalic);


        if (Input.GetKeyDown("s"))
        {
            timer = 0;
        }


        if(timer < _glowtime)
        {

            _amplitude = Mathf.Cos( timer / _glowtime * Mathf.PI / 2);
            
            Shader.SetGlobalFloat(GlowID, _amplitude);

            timer += Time.deltaTime;
        }
        else
        {

        }




		
	}
}
