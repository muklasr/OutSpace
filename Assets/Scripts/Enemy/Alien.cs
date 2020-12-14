using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Alien : EnemyController
{
    private Animator anim;

    public int health = 100;
    private int staticHealth;

    private bool isDead = false;
    public float timeRemaining = 10;


    public GameObject bloodPrefab;          // Blood splash prefab
    public PlayerController playerController;

    //private GameController gc;

    // Use this for initialization
    void Start()
    {
        staticHealth = health;
        anim = GetComponent<Animator>();
        //  gc = FindObjectOfType<GameController>();
    }

    protected override void Update()
    {
        if (!isDead)
        {
            if (target)
            {
                anim.SetBool("Idle", true);
                base.Update();

                if (Mathf.Abs(targetDistance) < viewDistance)
                {
                    if (Mathf.Abs(targetDistance) > 50)
                    {
                        anim.SetBool("Run", true);
                        anim.SetBool("Idle", false);
                        anim.SetBool("Bite", false);
                        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
                        Debug.Log("Run");
                    }
                    else
                    {
                        anim.SetBool("Bite", true);
                        anim.SetBool("Idle", false);
                        playerController.setHealth(0.005f);
                    }
                }
            }
        } else if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            DestroyObject();
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "Bullet")
        {
            float countdown = 2;
            GameObject tempBlood = Instantiate(bloodPrefab, collision.transform.position, collision.transform.rotation);
            countdown -= Time.deltaTime;
            StartCoroutine(BloodCoroutine(tempBlood));
            health -= 10;
            Dead(health);
        }
    }
    void Dead(int health)
    {
        if (health <= 0)
        {
            if(staticHealth == 500)
            {
                SceneManager.LoadScene("WinScene");
            }
            Debug.Log("Enemy Health at 0");
            isDead = true;
            anim.SetBool("Idle", false);
            anim.SetBool("Run", false);
            anim.SetBool("Bite", false);
            anim.SetBool("Die", true);
            //gc.SendMessage("IncreaseScore", 100f, SendMessageOptions.DontRequireReceiver);
            Debug.Log("Enemy dead");
        }
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }

    IEnumerator BloodCoroutine(GameObject gameObj)
    {

        //yield on a new YieldInstruction that waits for 1 seconds.
        yield return new WaitForSeconds(1);

        //After we have waited 1 seconds print the time again.
        Destroy(gameObj);
    }
}
