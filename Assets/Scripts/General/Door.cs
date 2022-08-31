using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    private Animator animator;
    
    void Start()
    {

        animator = GetComponent<Animator>();

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        animator.SetBool("Open Door", true);
        StartCoroutine(Wait());

    }

    IEnumerator Wait()
    {

        yield return new WaitForSeconds(1);
        animator.SetBool("Open Door", false);

    }

}
