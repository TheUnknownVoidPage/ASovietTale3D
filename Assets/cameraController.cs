using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class cameraController : MonoBehaviour
{
    public Transform playerTransform;
    public GameObject player;
    private Vector3 offset;
    private Vector3 dialogOffset;


    private Vector3 angleOffset;
    private Vector3 dialogueAngleOffset;
    public float followSpeed = 10f;
    

    public Transform closeUpPosition; // The close-up camera position near the player and NPC
    public Transform normalPosition; // The close-up camera position near the player and NPC

    public float transitionSpeed = 2f; // Speed of camera transition
    public GameObject dialogueBox; // Reference to the dialogue box

    public bool isDialogueActive = false; // State of the dialogue box


    void Start()
    {



            if (playerTransform == null)
        {
            Debug.LogError("Player Transform is not assigned!");
            return;
        }

        // Calculate the initial offset
        offset = normalPosition.position - playerTransform.position;
        dialogOffset = closeUpPosition.position - playerTransform.position;

    }

    void LateUpdate()
    {

        var dialogueManagers = GameObject.FindGameObjectsWithTag("DialogueManager");
        var objectCount = dialogueManagers.Length;
        foreach(var obj in dialogueManagers)
        {
            if (obj.activeInHierarchy)
            {
                dialogueBox = obj;
            }
        }
        if (dialogueBox != null)
        {
            if (dialogueBox.activeInHierarchy)
            {
                isDialogueActive = true;
            }
            else
            {
                isDialogueActive = false;
            }
        }



        if (playerTransform != null && !isDialogueActive)
        {
            Vector3 targetPosition = playerTransform.position + offset;


            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
            //transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, normalPosition.transform.rotation, followSpeed * Time.deltaTime);
            transform.eulerAngles = normalPosition.transform.eulerAngles;

        }
        else if (isDialogueActive)
        {
            Vector3 headToTransform = closeUpPosition.position - player.GetComponent<playerController>().interactingNPC.transform.position;
            Vector3 adjustedCloseUpPosition = player.GetComponent<playerController>().interactingNPC.transform.position + headToTransform;

            transform.position = Vector3.Lerp(transform.position, adjustedCloseUpPosition, followSpeed * Time.deltaTime);
            transform.LookAt(player.GetComponent<playerController>().interactingNPC.transform.GetChild(0).gameObject.transform);
        }
    }
}
