using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class specialEnemy3D : MonoBehaviour
{

    [SerializeField]
    private float speed;

    private Material dissolveMaterial;


    // Use this for initialization
    void Start()
    {
        dissolveMaterial = this.gameObject.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-speed * Time.deltaTime, 0f, 0f);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "PlayerBullets")
        {
            Destroy(col.gameObject);
            StartCoroutine(dissolveShader());
            GameController.Score += 100;
        }
    }

    IEnumerator dissolveShader()
    {
        float time = 0.0f;

        while (time < 0.3f){
            time += Time.deltaTime;
            dissolveMaterial.SetFloat("_DissolveFactor", time);
            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }
}
