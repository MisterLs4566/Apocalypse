using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed, runSpeed, hearts;
    private float currentSpeed, angle;
    private float horizontalInput, verticalInput;
    private CharacterController playerC;
    private Vector3 velocity, mousePos, playerPos;
    private Vector2 velocity2d;
    private Animator playerAnimator;
    private bool isShooting;
    private string weapon;

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = speed;
        isShooting = false;
        weapon = "";

        playerC = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movementInput();
        Move();
        Rotate();
        Shoot();
        Animate();
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
            velocity.x = horizontalInput * (currentSpeed/1.2f);
            velocity.y = verticalInput * (currentSpeed/1.2f);
        }
        else
        {
            velocity.x = horizontalInput * currentSpeed;
            velocity.y = verticalInput * currentSpeed;
        }
    }

    void Move()
    {
        playerC.Move(velocity * Time.deltaTime);
    }

    void Rotate()
    {
        velocity2d = new Vector2(velocity.x, velocity.y);
        angle = Mathf.Atan2(velocity2d.x, velocity2d.y) * Mathf.Rad2Deg;

        if((velocity2d.x != 0 || velocity2d.y !=0) && isShooting == false)
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

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer == 6)
        {
            Destroy(other.gameObject);
        }
    }
}
