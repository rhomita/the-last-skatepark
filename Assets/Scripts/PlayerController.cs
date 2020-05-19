using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private GameObject ridingParticles;

    private float speed = 9f;
    private float jumpHeight = 0.5f;
    private Vector3 velocity = Vector3.zero;
    private CharacterController controller;

    private bool isGrounded;
    private bool isRiding = false;
    private Animator skateAnimator;
    private PlayerCombat combat;

    void Start()
    {
        controller = transform.GetComponent<CharacterController>();
        combat = transform.GetComponent<PlayerCombat>();
        skateAnimator = GameManager.instance.GetSkate().GetComponent<Animator>();
    }

    // TODO: Add run
    void Update()
    {
        speed = isRiding ? 18f : 9f;

        CheckGround();
        AddGravity();

        GetInput();
        CalculateJump();
    }

    void FixedUpdate()
    {
        MakeMovement();
    }

    void AddGravity()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -0.5f;
        }
        else
        {
            velocity.y += (Physics.gravity.y * 1.2f) * Time.deltaTime;
        }

        velocity.y = Mathf.Clamp(velocity.y, -10, 5);
    }

    void GetInput()
    {
        if (!isRiding)
        {
            float horizontal = Input.GetAxis("Horizontal");
            velocity.x = horizontal;
        } else
        {
            velocity.x = 0;
        }
        
        float vertical = Input.GetAxis("Vertical");
        velocity.z = vertical;

        ridingParticles.SetActive(isRiding && vertical > 0);
    }

    private void CalculateJump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }
    }

    private void MakeMovement()
    {
        Vector3 forward = transform.forward.normalized;
        Vector3 right = transform.right.normalized;
        forward.y = 0;
        right.y = 0;
        Vector3 movement = forward * velocity.z + right * velocity.x;

        bool isWalking = Vector3.Magnitude(movement) > 0;
        skateAnimator.SetBool("isWalking", combat.HasSkate() && isWalking && !isRiding);

        movement.y = velocity.y;

        controller.Move(movement * speed * Time.fixedDeltaTime);
    }

    private void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundMask);
    }

    public void ToggleRide()
    {
        isRiding = !isRiding;
    }

    public bool IsRiding()
    {
        return isRiding;
    }

}
