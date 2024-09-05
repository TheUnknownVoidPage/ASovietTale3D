using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChangeScript : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private string sceneToGoTo;
    [SerializeField] private float distanceToPlayer;
    [SerializeField] private float interactionDistance = 2f;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = (player.position - gameObject.transform.position).magnitude;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding is the player (you can tag the player as "Player")
        if (other.GetComponent<CharacterController>() != null)
        {
            print("test");
            if (other.CompareTag("Player"))
            {
                // Load the specified scene
                SceneManager.LoadScene(sceneToGoTo);
            }
        }
    }
}
