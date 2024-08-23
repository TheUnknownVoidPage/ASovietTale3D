using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    Animator animator;
    public float moveSpeed = 5f; // Speed of the player movement
    public float rotationSpeed = 720f; // Speed of the player rotation


    private Rigidbody rb;
    private CharacterController characterController;
    public Transform cameraTransform;

    public GameObject interactingNPC;

    private Vector3 moveDirection;
    Vector3 desiredMoveDirection;

    private void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    void Update()
    {




        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
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
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        desiredMoveDirection = forward * Input.GetAxis("Vertical") + right * Input.GetAxis("Horizontal");

        Vector3 force = desiredMoveDirection * moveSpeed;
        rb.AddForce(force, ForceMode.VelocityChange);
    }
    void HandleRotation()
    {
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(desiredMoveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void SetInteractingNPC(GameObject gameObject)
    {
        this.interactingNPC = gameObject;
    }

}