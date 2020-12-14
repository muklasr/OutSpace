using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 200f;        // Player Speed
    public float jumpForce = 5000;       // Player jump force
    private float health = 1f;
    private float hForce = 0;            // Horizontal force used to create movement

    private Animator anim;                  // Player animator
    private Rigidbody2D rb2d;                  // Player rigide body 2D
    private bool facingRight = true;    // Is player facing right ?
    private bool isDead = false;        // Is player dead ?
    private bool isJump = false;        // Is player jump ?

    public GameObject fireSplashPrefab;          
    public GameObject bulletPrefab;          // Bullet prefab instantiated at fire
    public Transform gunTip;                // Gun tip ( Where the bullet shoot from)

    public Slider sliderHealth;

    // Use this for initialization
    void Start()
    {

        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("jump0: " + rb2d.velocity.y);
            if (rb2d.velocity.y == 0)
            {
                isJump = true;
                Debug.Log("jump");
                rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * 0.5f);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject tempBullet = Instantiate(bulletPrefab, gunTip.position, gunTip.rotation);
            GameObject tempFireSplash = Instantiate(fireSplashPrefab, gunTip.position, gunTip.rotation);
            StartCoroutine(SplashCoroutine(tempFireSplash));
            if (!facingRight)
            {
                tempBullet.transform.eulerAngles = new Vector3(0, 0, 180f);
                tempFireSplash.transform.eulerAngles = new Vector3(0, 0, 180f);
            }
            else if (!facingRight)
            {
                tempBullet.transform.eulerAngles = new Vector3(0, 0, 90f);
                tempFireSplash.transform.eulerAngles = new Vector3(0, 0, 90f);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {

            anim.SetBool("Run", false);
            hForce = Input.GetAxisRaw("Horizontal");

            // anim.SetFloat("Speed", Mathf.Abs(hForce));

            rb2d.velocity = new Vector2(hForce * speed, rb2d.velocity.y);

            if (hForce > 0 && !facingRight)
            {
                Flip();
            }
            else if (hForce < 0 && facingRight)
            {
                Flip();
            }
            else if (hForce != 0)
            {
                Debug.Log("hForce0: " + hForce);
                anim.SetBool("Run", true);
            }

            if (isJump)
            {
                isJump = false;
                Debug.Log("jump woy ");
                rb2d.AddForce(Vector2.up * jumpForce);
            }

        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void Dead(float health)
    {
        if (health <= 0f)
        {
            Debug.Log("Player Health at 0");
            isDead = true;
            SceneManager.LoadScene("GameOverScene");
            anim.SetTrigger("Die");
        }
    }

    public void setHealth(float h)
    {
        this.health = this.health - h;
        Debug.Log("Player Health at " + this.health);
        Dead(this.health);
        sliderHealth.value = this.health;
    }

    float getHealth()
    {
        return health;
    }


    void DestroyObject()
    {
        Destroy(gameObject);
    }

    IEnumerator SplashCoroutine(GameObject gameObj)
    {

        yield return new WaitForSeconds(0.25f);

        Destroy(gameObj);
    }
}
