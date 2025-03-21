using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMove : MonoBehaviour
{
    // VARIABLES
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;
    [SerializeField] private bool isGrounded;

    public float moveX;
    public float moveY;
    public float mouseX;
    public float mouseY;
    public bool isStopPanel;
    private bool runMode = false;

    // Signifies ground layer
    [SerializeField] private LayerMask groundMask;

    // Vector3s for direction and movement
    private Vector3 velocity;
    public Vector3 vHorizontalMouse;
    private Vector3 moveDirection;

    /*
    private bool isJumping;
    private float jumpTimer;
    */

    // COMPONENTS
    private CharacterController controller;
    private Animator anim;

    // METHODS
    // Mouse rotation
    private void rotatePlayer()
    {
        float mouseX = Input.GetAxis("Mouse X");

        Vector3 vHorizontalMouse = Vector3.up * mouseX;

        transform.Rotate(vHorizontalMouse);
    }

    // Character movement
    private void move()
    {
        // Run mode on or off
        if (Input.GetKeyDown(KeyCode.LeftShift) && !runMode)
        {
            runMode = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && runMode)
        {
            runMode = false;
        }

        // Mouse input
        float moveZ = Input.GetAxis("Vertical");
        float moveX = Input.GetAxis("Horizontal");

        // Normalizes direction vector
        moveDirection = new Vector3(moveX, 0, moveZ).normalized;
        moveDirection = transform.TransformDirection(moveDirection);

        // Bool that checks if the player is grounded by creating a sphere & if it is in contact with the ground layer
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0f)
        {
            if ((moveDirection != Vector3.zero && !runMode) || (moveDirection != Vector3.zero && runMode))
            {
                // Side Walk R
                if (Input.GetKey(KeyCode.D))
                {
                    sideWalkR();
                }
                // Side Walk L
                else if (Input.GetKey(KeyCode.A))
                {
                    sideWalkL();
                }
                // Back Walk
                else if (Input.GetKey(KeyCode.S))
                {
                    backWalk();
                }
            }
            // Front Walk
            if (moveDirection != Vector3.zero && !runMode && Input.GetKey(KeyCode.W))
            {
                walk();
            }
            // Run
            else if (moveDirection != Vector3.zero && runMode && Input.GetKey(KeyCode.W))
            {
                run();
            }
            // Idle
            else if (moveDirection == Vector3.zero)
            {
                idle();
            }
            // Jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //jump();
            }

            moveDirection *= moveSpeed;
        }

        controller.Move(moveDirection * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // To stop var from overflowing
        // Putting a limit a threshold
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        /*
         * Optional feature
         * 
        if (isJumping)
        {
            jumpTimer += Time.deltaTime;
            if (jumpTimer > 1.5f)
            {
                jumpTimer = 0f;
                anim.SetBool("toJump", false);
                isJumping = false;
            }
        }
        */

    }

    // ALL MOVEMENT TYPES
    private void idle()
    {
        anim.SetFloat("Blend", 0f);
    }

    private void walk()
    {
        anim.SetFloat("Blend", 0.2f);
        moveSpeed = walkSpeed;
    }

    private void sideWalkR()
    {
        moveSpeed = 0.5f * walkSpeed;
        anim.SetFloat("Blend", 0.8f);
    }

    private void sideWalkL()
    {
        moveSpeed = 0.5f * walkSpeed;
        anim.SetFloat("Blend", 0.6f);
    }

    private void backWalk()
    {
        moveSpeed = 0.25f * walkSpeed;
        anim.SetFloat("Blend", 1f);
    }

    private void run()
    {
        moveSpeed = 5 * walkSpeed;
        anim.SetFloat("Blend", 0.4f);
    }

    /*
     * Optional feature
     * 
    private void jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        anim.SetBool("toJump", true);
        isJumping = true;
    }
    */

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        // Stops 3D character controller for panel navigation
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isStopPanel = false;
    }

    void Update()
    {
        if (!isStopPanel)
        {
            move();
            rotatePlayer();
        }

        //Debug.Log(Cursor.visible);
        //Debug.Log("MouseX" + vHorizontalMouse);
    }

    
}
