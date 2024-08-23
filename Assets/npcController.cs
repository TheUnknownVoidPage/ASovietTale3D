using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class npcController : MonoBehaviour
{

    public Transform player;
    public Transform npcHead;
    Quaternion originalHeadPos;
    Quaternion originalPos;
    private float rotationSpeed = 5f;

    public GameObject dialogueManagerPrefab;
    GameObject dialogueManager;
    public float interactionDistance = 0.5f;
    private bool chatBoxOpen = false;

    public string characterName;
    public string chapter;
    public string sequence;


    // Start is called before the first frame update
    void Start()
    {
        originalHeadPos = npcHead.transform.rotation;
        originalPos = transform.rotation;
        dialogueManager = Instantiate(dialogueManagerPrefab);
        dialogueManager.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
        DialogueManagement();
    }


    void LookAtPlayer()
    {
        var distanceToPlayer = (player.position - transform.position).magnitude;
        var directionToPlayer = (player.position - transform.position).normalized;

        //print("Direction: " + directionToPlayer);
        //print("Distance: " + distanceToPlayer);

        if (distanceToPlayer < 5f)
        {

                // Calculate the direction to the player on the XZ plane
                Vector3 directionOnXZ = new Vector3(directionToPlayer.x, 0, directionToPlayer.z);
            // Calculate the target rotation based on the direction
            Quaternion targetRotation = Quaternion.LookRotation(directionOnXZ);

            // Get the angle difference between the current and target rotation
            float angleDifference = Quaternion.Angle(originalHeadPos, targetRotation);
            //print("Angle Difference:" + angleDifference);
            // Check if the angle difference is within the limit
            if (angleDifference <= 70f && !chatBoxOpen)
            {
                // Smoothly rotate the head towards the player on the Y axis
                npcHead.transform.rotation = Quaternion.Slerp(npcHead.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
            else if(chatBoxOpen)
            {
                npcHead.transform.rotation = Quaternion.Slerp(npcHead.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
            
            else
            {

                npcHead.transform.rotation = Quaternion.Slerp(npcHead.transform.rotation, originalHeadPos, Time.deltaTime * rotationSpeed);
            }
            if (chatBoxOpen)
            {

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, originalPos, Time.deltaTime * rotationSpeed);
            }
        }
    }
    void DialogueManagement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if ((Vector3.Distance(transform.position, player.position) <= interactionDistance))// && (horizontal == 0 && vertical == 0))
        {
            // Check if the 'E' key is pressed
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!chatBoxOpen)
                {
                    player.GetComponent<playerController>().SetInteractingNPC(gameObject);
                    dialogueManager.SetActive(true);
                    dialogueManager.GetComponent<DialogueManager>().LoadDialogueData();
                    dialogueManager.GetComponent<DialogueManager>().StartDialogue(characterName, chapter, sequence);
                    chatBoxOpen = true;
                }
                else
                {
                    if (chatBoxOpen)
                    {

                        if (dialogueManager.GetComponent<DialogueManager>().isTyping == true)
                        {
                            dialogueManager.GetComponent<DialogueManager>().ShowFullText();
                        }
                        else
                        {
                            dialogueManager.GetComponent<DialogueManager>().AdvanceDialogue();
                        }
                    }
                }
                if (!dialogueManager.activeSelf) chatBoxOpen = false;
            }
        }
        else
        {
            dialogueManager.SetActive(false);
            chatBoxOpen = false;
        }
    }


}
