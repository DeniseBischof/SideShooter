using System.Collections;
using UnityEngine;

public class bossBullet : MonoBehaviour
{

    [SerializeField]
    public float bulletSpeed;




    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        transform.position += new Vector3(-bulletSpeed * Time.deltaTime, 0f, 0f);

    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider col)
    {

    }
}

