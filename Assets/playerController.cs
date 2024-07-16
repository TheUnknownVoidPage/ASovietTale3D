using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    Animator animator;
    public float moveSpeed = 5f; // Speed of the player movement
    public float rotationSpeed = 720f; // Speed of the player rotation

    public GameObject interactingNPC;

    private Vector3 moveDirection;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (interactingNPC != null)
        {
            print(interactingNPC.name);
        }
        
        

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
        if (moveX != 0 && moveZ != 0)
        {
            transform.position += moveDirection.normalized * moveSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
        
    }
    void HandleRotation()
    {
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void SetInteractingNPC(GameObject gameObject)
    {
        this.interactingNPC = gameObject;
    }

}
