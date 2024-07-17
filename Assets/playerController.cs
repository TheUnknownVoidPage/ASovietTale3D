using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    Animator animator;
    public float moveSpeed = 5f; // Speed of the player movement
    public float rotationSpeed = 720f; // Speed of the player rotation


    private CharacterController characterController;
    public Transform cameraTransform;

    public GameObject interactingNPC;

    private Vector3 moveDirection;
    Vector3 desiredMoveDirection;

    private void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }
    void Update()
    {




        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");



        moveDirection = new Vector3(moveX, 0f, moveZ);

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

        characterController.SimpleMove(desiredMoveDirection * moveSpeed);
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