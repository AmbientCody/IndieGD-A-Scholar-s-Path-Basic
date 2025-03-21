using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMove6 : MonoBehaviour
{
    [SerializeField] private float fmSpeed = 4000f;
    [SerializeField] private float runX = 2f;
    [SerializeField] private float smSpeed = 2000f;
    [SerializeField] private float rSpeed = 1000f;
    [SerializeField] private float gravity = 10f;
    [SerializeField] private float vVelocity;
    private bool shouldJump = false;

    CharacterController charCon;
    Animator anim;

    void Start()
    {
        charCon = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Movement
        Vector3 vertical = (transform.TransformDirection(Vector3.forward) * Input.GetAxis("Vertical")).normalized;

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.S) && charCon.isGrounded)
        {
            // Run
            vertical *= fmSpeed * runX * Time.deltaTime;
            anim.SetFloat("Blend", 0.4f);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            // Walk
            vertical *= fmSpeed * Time.deltaTime;
            anim.SetFloat("Blend", 0.2f);
        }
        else
        {
            // Idle
            anim.SetFloat("Blend", 0f);
        }

        // Side walk
        if (Input.GetKey(KeyCode.A))
        {
            anim.SetFloat("Blend", 0.6f);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            anim.SetFloat("Blend", 0.8f);
        }

        // Walk back
        if (Input.GetKey(KeyCode.S))
        {
            anim.SetFloat("Blend", 1f);
        }

        Vector3 horizontal = (transform.TransformDirection(Vector3.right) * Input.GetAxis("Horizontal")).normalized;
        horizontal *= smSpeed * Time.deltaTime;

        // Rotation
        float rAngle = Input.GetAxis("Mouse X") * rSpeed * Time.deltaTime;
        Vector3 target = new Vector3(0, rAngle, 0);
        transform.Rotate(target);
        

        // Grounded
        if (charCon.isGrounded)
        {
            // Jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                vVelocity = 5f;
            }
            anim.SetBool("toJump", true);
        }

        // Ungrounded
        else if (!charCon.isGrounded)
        {
            vVelocity -= gravity * Time.deltaTime;

            // Jump animation trigger
            if (shouldJump)
            {
                anim.SetBool("toJump", false);
            }
        }

        Vector3 moveVector = Vector3.zero;
        moveVector += horizontal + vertical;
        moveVector.y = vVelocity;
        charCon.Move(moveVector * Time.deltaTime);

        // Check for jumping
        if (moveVector.y > 0)
        {
            shouldJump = true;
        }
        else if (moveVector.y < 0)
        {
            shouldJump = false;
        }

        Debug.Log("MoveVector.y -> " + moveVector.y);
    }
}
