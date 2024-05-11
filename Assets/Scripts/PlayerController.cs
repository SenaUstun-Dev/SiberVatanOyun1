using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float movementSpeed = 5.0f;
    public float rotationSpeed = 1.0f;
    private Animator animator;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        var movementDirection = new Vector3(horizontal, 0, vertical);

        animator.SetBool("isRunning", movementDirection != Vector3.zero);
        //animator.SetBool("isRunning", rb.velocity != Vector3.zero); alterntif yol

        if (movementDirection == Vector3.zero)
        {
            rb.velocity = Vector3.zero;

            return;
        }

        rb.velocity = movementDirection * movementSpeed ;

        var rotationDirection = Quaternion.LookRotation(movementDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationDirection, rotationSpeed * Time.deltaTime);
    }
}
