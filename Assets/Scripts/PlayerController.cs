using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float speed;

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private GameObject gettingHitAnimation;

    [SerializeField]
    private Transform bulletParent;

    public AudioClip badImpact;
    public AudioClip impact;
    public AudioClip shoot;
    AudioSource audioSource;

    public GameController changeVariable;

    public GameObject GameControllerSet;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);

        if (Input.GetKey("a"))
        {
            Debug.Log("Press a.");
            transform.position += new Vector3(-speed * Time.deltaTime, 0f, 0f);
        }
        if (Input.GetKey("s"))
        {
            transform.position += new Vector3(0f, -speed * Time.deltaTime, 0f);
            Debug.Log("Press s.");
        }
        if (Input.GetKey("d"))
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
            Debug.Log("Press d.");
        }

        if (Input.GetKey("w"))
        {
            transform.position += new Vector3(0f, speed * Time.deltaTime, 0f);
            Debug.Log("Press w.");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {

            BulletPool.Spawn(bullet, bulletParent.position, bulletParent.rotation);
//            GameObject obj = Instantiate(bullet);
            audioSource.PlayOneShot(shoot, 0.7F);
//            obj.transform.position = bulletParent.position;
            Debug.Log("Shoot");
        }

    }

    void OnTriggerEnter(Collider col)
    {

        if ((col.tag == "Enemies" ) || (col.tag == "EnemyBullets") || (col.tag == "Boss"))
        {
            audioSource.PlayOneShot(impact, 0.7F);
            changeVariable.CurrentLives--;
            changeVariable.Lives.text = changeVariable.CurrentLives.ToString();
            GotHitAnim();

            if (changeVariable.CurrentLives <= 0)
            {
                GameControllerSet.GetComponent<GameController>().SetGameController(GameController.GameControllerState.GameOver);

                gameObject.SetActive(false);
            }



            //Destroy(gameObject);
        }

        //       if (col.tag == "Boss")
        //       {
        //       audioSource.PlayOneShot(badImpact, 0.7F);
        //    Camera.main.transform.localPosition.x = Random.insideUnitSphere.x * shakeAmount;
        //        changeVariable.CurrentLives-= 2;
        //       GotHitAnim();
        //    }
    }

    void GotHitAnim()
    {
        GameObject explosion = (GameObject)Instantiate(gettingHitAnimation);
        explosion.transform.position = transform.position;
    }


}