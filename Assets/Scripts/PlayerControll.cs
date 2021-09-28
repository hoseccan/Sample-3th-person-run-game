using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    private Transform cam;
    private Rigidbody rb;
    private Animator anim;
    private float previousInputforce;
    public bool isJump;

    public float jumpForceY;
    public float jumpForceZ;
    public float movingSpeed;

    void Awake()
    {
        cam = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (isJump && Mathf.Abs(rb.velocity.y) > 0.05)
        {
            rb.AddForce(transform.forward * jumpForceZ);
        }
        else if (Mathf.Abs(rb.velocity.y) < 0.05) isJump = false;
        Move();
    }
    void Move()
    {
        float H = CrossPlatformInputManager.GetAxis("Horizontal");
        float Y = CrossPlatformInputManager.GetAxis("Vertical");

        Vector3 direction = transform.position + (cam.transform.right * H) + (cam.transform.forward * Y);
        Vector3 convertedDirection = new Vector3(direction.x, transform.position.y, direction.z);
        transform.LookAt(convertedDirection);

        float inputforce = Vector3.Magnitude(new Vector3(H, 0, Y));
        if (inputforce == 0 && previousInputforce > 0.7)
        {
            Jump();
        }
        previousInputforce = inputforce;
        if (inputforce < 0.3f)
        {
            anim.SetBool("IsRun", false);
            anim.SetBool("IsWalk", false);
            return;
        }
        if (inputforce < 0.7f)
        {
            rb.AddForce(transform.forward * movingSpeed / 2 * 1000f);
            anim.SetBool("IsRun", false);
            anim.SetBool("IsWalk", true);
            return;
        }
        if (inputforce > 0.7f)
        {
            rb.AddForce(transform.forward * movingSpeed * 1000f);
            anim.SetBool("IsRun", true);
            anim.SetBool("IsWalk", false);
            return;
        }
    }
    void Jump()
    {
        isJump = true;
        rb.AddForce(jumpForceY * Vector3.up * 100);
        anim.SetTrigger("IsJump");
    }
}
