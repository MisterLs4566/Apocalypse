using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{

    public Transform playerTransform;
    private Vector2 differenceToPlayer, velocity;
    private float angle;
    private Rigidbody2D zombieRb;
    public float speed;

    void Start()
    {

        zombieRb = GetComponent<Rigidbody2D>();  

    }

    void Update()
    {

       Rotate();

    }

    void FixedUpdate()
    {

        Move();

    }

    void Rotate()
    {

        differenceToPlayer.x = playerTransform.position.x - transform.position.x;
        differenceToPlayer.y = playerTransform.position.y - transform.position.y;
        angle = Mathf.Atan2(differenceToPlayer.x, differenceToPlayer.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);

    }

    void Move()
    {

        velocity = transform.up * speed;
        zombieRb.velocity = velocity;

    }

}
