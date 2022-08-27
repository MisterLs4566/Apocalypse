using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public Transform playerTransform;
    private Vector3 differenceToPlayer, velocity;
    private float angle;
    private CharacterController enemyController;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        enemyController = GetComponent<CharacterController>();  
    }

    // Update is called once per frame
    void Update()
    {
       Rotate();
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
        enemyController.Move(velocity * Time.deltaTime);
    }
}
