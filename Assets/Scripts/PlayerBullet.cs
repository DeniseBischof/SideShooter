using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{

    [SerializeField]
     float bulletSpeed;

    [SerializeField]
    private GameObject Animation;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(bulletSpeed * Time.deltaTime, 0f, 0f);

    }

    void OnBecameInvisible()
    {
        BulletPool.Despawn(gameObject);
//        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider col)
    {
        if ((col.tag == "Enemies") || (col.tag == "Boss"))
        {
            AnimationMeth();
            BulletPool.Despawn(gameObject);
//          Destroy(gameObject);
        }

        //       if (col.tag == "BossFront")
        //       {
        //
        //            Destroy(gameObject);
        //       }

        //       if (col.tag == "BossBack")
        //       {

        //       Destroy(gameObject);
        //   }
    }

    void AnimationMeth()
    {
        GameObject gotHit = (GameObject)Instantiate(Animation);
        gotHit.transform.position = transform.position;
    }
}
