using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class cameraController : MonoBehaviour
{
    public Transform playerTransform;
    public GameObject player;
    public Vector3 offset;

    public float followSpeed = 10f;
    public float turnSpeed = 4.0f;


    public Transform closeUpPosition;
    public Transform normalPosition;

    public float transitionSpeed = 2f;
    public GameObject dialogueBox;

    public bool isDialogueActive = false;


    void Start()
    {



            if (playerTransform == null)
        {
            Debug.LogError("Player Transform is not assigned!");
            return;
        }

        offset = new Vector3(5, 1.5f, -3);

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
            
            transform.position = playerTransform.position + offset;
            transform.LookAt(playerTransform.position);
            if (Input.GetMouseButton(1))
            {
                offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
                
            }
            else
            {

            }

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
