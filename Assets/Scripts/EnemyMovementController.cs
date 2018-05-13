using System.Collections;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour {

    [SerializeField]
    public float speed;

    public float frequency = 10.0f;  
    public float magnitude = 1f;   

    private Vector3 axis;
    private Vector3 position;


    // Use this for initialization
    void Start () {

        position = transform.position;
        axis = transform.right;

    }
	
	// Update is called once per frame
	void Update () {

        position -= transform.up * Time.deltaTime * speed;
        transform.position = position + axis * Mathf.Sin(Time.time * frequency) * magnitude;

    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "PlayerBullets")
        {
            GameController.Score += 100;
            Destroy(gameObject);
        }
    }

}
