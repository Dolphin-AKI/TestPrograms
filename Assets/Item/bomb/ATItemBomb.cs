using UnityEngine;
using System.Collections;


//Aride統合後にItemを継承します
public class ATItemBomb : MonoBehaviour {

    public float bombMaxScale = 10;
    public float bombExpSpeed = 1;


    private GameObject bombObject;
    private GameObject Itempanel;
    private Transform bombArea;
    private GameObject bombAreaGO;
    private ParticleSystem explosionFog;
    private ParticleSystem.ShapeModule explosionFog_shape;
    private ParticleSystem.EmissionModule explosionFog_emission;
    private float explosionFog_emissionRate;


    public enum state
    {
        Panel,
        get,
        ready,
        bomthrow,
        explosion
    }
    public state st;
	// Use this for initialization
	void Start () {

        st = state.Panel;

        bombObject = transform.FindChild("bomb").gameObject;
        Itempanel = transform.FindChild("Panel").gameObject;
        bombAreaGO = transform.FindChild("BombArea").gameObject;
        bombArea = transform.FindChild("BombArea").GetComponent<Transform>();
        explosionFog = transform.FindChild("BombArea").GetComponent<ParticleSystem>();
        explosionFog_shape = explosionFog.shape;
        explosionFog_emission = explosionFog.emission;

        bombObject.SetActive(false);
        Itempanel.SetActive(true);

        bombArea.localScale = Vector3.zero;
        //explosionFog_shape.radius = 0;
        //explosionFog_emission.rate = 0.125f;
        //explosionFog_emissionRate = 0.125f;
        if (explosionFog.isPlaying)
        {
            explosionFog.Stop();
        }

        bombAreaGO.SetActive(false);
	
	}
	
	// Update is called once per frame
	void Update () {

        switch (st)
        {
            case state.Panel:
                this.transform.Rotate(0, 0, 1f);


                //if get item
                // -> state 2
                break;

            case state.get:
                Itempanel.SetActive(false);
                bombObject.SetActive(true);

                //ARideに接続　入力を受け取れるようにする

                break;

            case state.ready:
                //入力待ち  ぶん投げる




                break;

            case state.bomthrow:
                //着弾したらどっかーん


                break;

            case state.explosion:

                //後で着弾時の処理に移行
                bombAreaGO.SetActive(true);


                if(bombArea.localScale.x < bombMaxScale)
                {
                    bombArea.localScale += Vector3.one * bombExpSpeed * Time.deltaTime;
                    
                    if (!explosionFog.isPlaying)
                    {
                        explosionFog.Play();
                    }
                }
                else if(bombArea.localScale.x >= bombMaxScale)
                {
                    bombArea.localScale = Vector3.one * bombMaxScale;
                }

                bombArea.Rotate(1, 0, 1);

                break;

        }
	
	}
}
