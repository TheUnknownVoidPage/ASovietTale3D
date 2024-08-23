using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    Animator animator;

    public float moveSpeed = 5f; // Speed of the player movement
    public float rotationSpeed = 720f; // Speed of the player rotation
    private float gravity = 9.81f;

    private float verticalVelocity;

    public bool isLocked;


    [SerializeField] private Transform camera;
    [SerializeField] private float turningSpeed = 200f;
    private CharacterController characterController;
    //public Transform cameraTransform;

    public GameObject interactingNPC;

    private Vector3 moveDirection;



    private float moveInput;
    private float turnInput;


    



    private void Start()
    {
        isLocked = false;
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

    }
    void Update()
    {
        if (!isLocked)
        {
            InputManagement();
            Movement();
            HandleAnimation();
        }

    }
    private void Movement()
    {
        GroundMovement();
        Turn();
    }
    private void GroundMovement()
    {
        Vector3 move = new Vector3(turnInput, 0, moveInput);
        move = camera.transform.TransformDirection(move);

        move *= moveSpeed;

        move.y = VerticalForceCalculation();
        
        characterController.Move(move * Time.deltaTime);
    }

    private void Turn()
    {
        if (Mathf.Abs(turnInput) > 0 || Mathf.Abs(moveInput) > 0)
        {
            // Calculate the movement direction based on input and the camera's forward direction
            Vector3 targetDirection = new Vector3(turnInput, 0, moveInput);
            targetDirection = camera.transform.TransformDirection(targetDirection);
            targetDirection.y = 0;  // Ensure no vertical movement

            // If the direction is significant, rotate the player towards it
            if (targetDirection.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turningSpeed);
            }
        }
    }

    private float VerticalForceCalculation()
    {
        if(characterController.isGrounded)
        {
            verticalVelocity = -1f;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        return verticalVelocity;
    }

    private void InputManagement()
    {
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }
    void HandleAnimation()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");



        moveDirection = new Vector3(moveX, 0f, moveZ);

        print(moveDirection);

        if (moveDirection != Vector3.zero)
        {
            animator.SetBool("movement", true);

        }
        else
        {
            animator.SetBool("movement", false);
        }

    
    }

    public void SetInteractingNPC(GameObject gameObject)
    {
        this.interactingNPC = gameObject;
    }

}