using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed, runSpeed, hearts, knockback;
    private float currentSpeed, angle;
    private float horizontalInput, verticalInput;
    private float maxHearts, knockbackVelocity;
    private Animator playerAnimator;
    private Rigidbody2D playerRb;
    private Vector3 mousePos;
    private Vector3 velocity, playerPos;
    private bool isShooting, isGettingKnockback;
    private string weapon;
    private bool receiveDamage;
    private Vector3 playerDirection;

    
    void Start()
    {

        maxHearts = hearts;
        currentSpeed = speed;
        isShooting = false;
        receiveDamage = true;
        isGettingKnockback = false;
        weapon = "Gun";

        playerAnimator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody2D>();

    }

    
    void Update()
    {

        if (receiveDamage == true)
        {

            movementInput();

        }
        else
        {

            horizontalInput = 0;
            verticalInput = 0;

        }

        //Move();

        if (receiveDamage == true)
        {

            Rotate();

        }

        Shoot();
        Animate();
        Knockback();
    }

    void FixedUpdate()
    {
        Move();
    }

    void movementInput()
    {

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetButton("Fire2"))
        {

            currentSpeed = runSpeed;
            playerAnimator.speed = 2;

        }
        else
        {

            currentSpeed = speed;
            playerAnimator.speed = 0.75f;

        }

        if (horizontalInput != 0 && verticalInput != 0)
        {

            velocity.x = horizontalInput * (currentSpeed / 1.2f);
            velocity.y = verticalInput * (currentSpeed / 1.2f);

        }
        else
        {

            velocity.x = horizontalInput * currentSpeed;
            velocity.y = verticalInput * currentSpeed;

        }
    }

    void Move()
    {

        //playerRb.AddForce(velocity * Time.deltaTime, ForceMode2D.Impulse);
        playerRb.velocity = velocity;

    }

    void Rotate()
    {

        angle = Mathf.Atan2(velocity.x, velocity.y) * Mathf.Rad2Deg;

        if((velocity.x != 0 || velocity.y !=0) && isShooting == false)
        {

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);

        }

    }

    void Shoot()
    {

        if(Input.GetButtonDown("Fire1"))
        {

            if (weapon != "")
            {

                isShooting = true;
                mousePos = Input.mousePosition;
                mousePos.z = 10;
                playerPos = Camera.main.WorldToScreenPoint(transform.position);

                mousePos.x = mousePos.x - playerPos.x;
                mousePos.y = mousePos.y - playerPos.y;

                angle = Mathf.Atan2(mousePos.x, mousePos.y) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);

            }

        }
        else
        {

            isShooting = false;

        }

    }

    void Animate()
    {

        if(horizontalInput != 0 || verticalInput != 0)
        {

            playerAnimator.SetBool("isRunning", true);

        }
        else 
        {

            playerAnimator.SetBool("isRunning", false);

        }

    }

    void Knockback()
    {

        if (knockbackVelocity != 0)
        {

            if(knockbackVelocity < 0)
            {

                if(isGettingKnockback == false)
                {

                    StartCoroutine(waitDamage());

                }

            }
            else
            {

                knockbackVelocity = 0f;

            }

        }

    }

    IEnumerator getDamage()
    {

        playerAnimator.SetBool("getDamage", true);
        playerAnimator.SetBool("isRunning", false);
        hearts -= 10;
        receiveDamage = false;
        knockbackVelocity = knockback;
        yield return new WaitForSeconds(1);
        receiveDamage = true;
        playerAnimator.SetBool("getDamage", false);

    }

    IEnumerator waitDamage()
    {

        isGettingKnockback = true;
        yield return new WaitForSeconds(0.05f);
        velocity += playerDirection * -knockbackVelocity;
        knockbackVelocity *= 0.9f;
        isGettingKnockback = false;

    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if(other.gameObject.layer == 6)
        {

            if(other.gameObject.CompareTag("Food"))
            {

                hearts += 25;

            }

            Destroy(other.gameObject);

        }

        if (other.gameObject.CompareTag("Zombie"))
        {

            if(receiveDamage == true)
            {

                playerDirection = gameObject.transform.up;
                velocity = new Vector2(0, 0);
                StartCoroutine(getDamage());

            }

        }

        if (hearts > maxHearts)
        {

            hearts = maxHearts;

        }
        else if (hearts <= 0)
        {

            Destroy(gameObject);

        }

    }

}
