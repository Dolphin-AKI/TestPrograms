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

    [SerializeField]
    private float _glowtime = 1.5f;
    public float GlowTime { get { return _glowtime; } set { _glowtime = value; } }

    private float timer = 0;

    private float _amplitude = 0;

    private Renderer _render;

    public Shader _objectsonarShader;

    //shader properties
    int ColorID;
    int SmoothnessID;
    int MetalicID;
    int GlowID;


    private void Awake()
    {
        _render = this.gameObject.GetComponent<Renderer>();
        _render.material.shader = _objectsonarShader;

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

        _render.material.SetColor(ColorID, color);
        _render.material.SetFloat(SmoothnessID, smoothness);
        _render.material.SetFloat(MetalicID, metalic);


        if (Input.GetKeyDown("s"))
        {
            timer = 0;
        }


        if(timer < _glowtime)
        {

            _amplitude = Mathf.Cos( timer / _glowtime * Mathf.PI / 2) / 2;
            
            _render.material.SetFloat(GlowID, _amplitude);

            timer += Time.deltaTime;
        }
        else
        {

        }




		
	}
}
