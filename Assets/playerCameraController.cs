using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.Cursor;

public class cameraController : MonoBehaviour
{


 
    public GameObject player;
    public Vector3 offset;
    public GameObject npc;
    public GameObject mainCamera;
    public GameObject dialogueCamera;
    public GameObject targetGroup;
    
    
    public GameObject dialogueBox;

    public bool isDialogueActive = false;


    void Start()
    {
        player = this.gameObject;

    }

    void LateUpdate()
    {
        Dialogue();


    }




    private void Dialogue()
    {
        var dialogueManagers = GameObject.FindGameObjectsWithTag("DialogueManager");
        var objectCount = dialogueManagers.Length;
        foreach (var obj in dialogueManagers)
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
                //Cursor.visible = true;
            }
            else
            {
                isDialogueActive = false;
                //Cursor.visible = false;
            }
        }
        if (player != null && !isDialogueActive)
        {


            //transform.position = player.transform.position - player.transform.forward * cameraDistance;
            //transform.LookAt(player.transform.position);
            //transform.position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);

            mainCamera.SetActive(true);
            dialogueCamera.SetActive(false);
            player.GetComponent<playerController>().isLocked = false;


        }
        else if (isDialogueActive)
        {
            //Vector3 headToTransform = closeUpPosition.position - player.GetComponent<playerController>().interactingNPC.transform.position;
            //Vector3 adjustedCloseUpPosition = player.GetComponent<playerController>().interactingNPC.transform.position + headToTransform;

            //transform.position = Vector3.Lerp(transform.position, adjustedCloseUpPosition, followSpeed * Time.deltaTime);
            //transform.LookAt(player.GetComponent<playerController>().interactingNPC.transform.GetChild(0).gameObject.transform);

            mainCamera.SetActive(false);
            dialogueCamera.SetActive(true);
            player.GetComponent<playerController>().isLocked = true;
        }
    }
}
