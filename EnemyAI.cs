using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Transform player;
    public int heath;
    public GameObject deathEffect; // hieu ung die

    public float stopDistance;
    public GameObject prefabBulletEnemy;
    public Transform shootCheck;
    public float fireRate;
    private float nextFire = 0f;
    private bool facingRight;
    private CharacterController2D character; //This script is incomplete...

    void Start()
    {
        character = GetComponent<CharacterController2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
        if (nextFire < Time.time)
        {
            if (Vector2.Distance(transform.position, player.position) < stopDistance)
            {
                Shoot();
                FindObjectOfType<AudioManager>().Play("ShootingEnemy");
            }
        }
        if (transform.position.x < player.position.x)
        {
            Flip();
        }
        else
        {
            Flip();
        }
    }

    void Shoot()
    {
        nextFire = fireRate + Time.time;
        Instantiate(prefabBulletEnemy, shootCheck.position, shootCheck.rotation);
    }

    public void TakeDamage(int edamage) // chiu thiet hai
    {
        heath -= edamage;
        if (heath <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, transform.rotation); // Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
       
    }
    private void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x < player.position.x)
        {
            rotation.y = 180f;
            facingRight = false;
        }
        else
        {
            rotation.y = 0f;
            facingRight = true;
        }
        transform.eulerAngles = rotation; // reset vi tri cua enemy
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        character = collider.GetComponent<CharacterController2D>();
        if (collider.gameObject.tag == "Player")
        {
            if (facingRight == false)
            {
                character.m_Rigidbody2D.velocity = new Vector2(300f, 0f);
                character.effTakeDameYellow.SetActive(true);
            }
            else
            {
                character.m_Rigidbody2D.velocity = new Vector2(-300f, 0f);
                character.effTakeDameYellow.SetActive(true);
            }
        }
    }
}
