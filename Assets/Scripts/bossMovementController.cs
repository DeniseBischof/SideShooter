using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossMovementController : MonoBehaviour
{

    [SerializeField]
    private GameObject enemyBullet;

    [SerializeField]
    private Transform enemyBulletParent;
    public GameController changeLevel;

    public GameObject BossFront;

    public float bulletSpawnSpeed = 0.5f;

    float timeLeft = 2.0f;

    public Transform Player;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;

        transform.position = new Vector3(6, Player.position.y, 0);

        if (timeLeft < 0)
        {
            GameObject obj = Instantiate(enemyBullet);
            obj.transform.position = enemyBulletParent.position;

            timeLeft = bulletSpawnSpeed;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "PlayerBullets")
        {

            GameController.Score += 100;
            GameController.BossLives--;
        }
    }

}