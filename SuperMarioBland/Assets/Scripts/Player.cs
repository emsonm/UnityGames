using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float jumpHeight;

    public float bulletSpeed;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public bool canDoubleJump;

    public GameObject bulletSpawnPoint;
    public GameObject bullet;

    public AudioSource jumpSound;
    public AudioSource coinSound;

    private bool grounded;
    private bool doubleJumped;
    private Rigidbody2D body;    

    private bool canShoot = true;
    public float canShootTimerMax = 10f;
    public float canShootTimer = 10f;

    public int Score = 0;

    public Vector2 currentPosition;

    GameObject mainCamera;



    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        mainCamera = (GameObject)GameObject.FindWithTag("MainCamera");
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround.value);
    }

    void Jump()
    {
        jumpSound.Play();
        body.velocity = new Vector2(0, jumpHeight);
    }

    // Update is called once per frame
    void Update()
    {
        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10f);

        if (grounded)
        {
            doubleJumped = false;
        }

        PlayerJump();

        PlayerMovement();

        PlayerShoot();

        currentPosition = transform.position;        
    }

    /// <summary>
    /// Shoots if needed, otherwise sets gun position.
    /// </summary>
    private void PlayerShoot()
    {
        //get gun position

        //should we shoot?

        var dt = Time.deltaTime;

        canShootTimer = canShootTimer - (1f * dt);        
        if (canShootTimer < 0)
        {
            canShoot = true;
            canShootTimer = 0;
        }

        if (canShoot && Input.GetMouseButtonDown(0))
        {
            Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            Vector2 spawnPoint = new Vector2(bulletSpawnPoint.transform.position.x, bulletSpawnPoint.transform.position.y);
            Vector2 direction = target - spawnPoint;
            direction.Normalize();

            Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

            var projectile = (GameObject)Instantiate(bullet, spawnPoint, rotation);

            var bulletBody = projectile.GetComponent<Rigidbody2D>();
            bulletBody.velocity = direction * bulletSpeed;

            canShoot = false;
            canShootTimer = canShootTimerMax;
        }
    }

    /// <summary>
    /// Jumps if needed
    /// </summary>
    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            var canJump = false;

            if (grounded)
            {
                canJump = true;
            }
            else if (canDoubleJump && !doubleJumped)
            {
                canJump = true;
                doubleJumped = true;
            }

            if (canJump)
            {
                Jump();
            }
        }
    }

    /// <summary>
    /// Moves if needed
    /// </summary>
    private void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            body.velocity = new Vector2(moveSpeed, body.velocity.y);
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            body.velocity = new Vector2(-moveSpeed, body.velocity.y);
        }
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        //Debug.Log(target.gameObject.name + " collision");
        if (target.gameObject.tag == "Collectible")
        {
            coinSound.Play();

            Destroy(target.gameObject);
            CoinLogic.CoinCount--;
            Score++;
        }
    }
}
