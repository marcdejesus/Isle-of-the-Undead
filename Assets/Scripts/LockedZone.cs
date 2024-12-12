using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedZone : MonoBehaviour
{
    private Collider zoneCollider;
    private bool playerEntered = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get the collider component
        zoneCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This method is called when another collider enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the player
        if (other.CompareTag("Player"))
        {
            // Set the flag to indicate the player has entered
            playerEntered = true;
        }
    }

    // This method is called when another collider exits the trigger
    private void OnTriggerExit(Collider other)
    {
        // Check if the collider belongs to the player and the player had entered before
        if (other.CompareTag("Player") && playerEntered)
        {
            // Disable the trigger collider
            zoneCollider.isTrigger = false;
            // Reset the flag
            playerEntered = false;
        }
    }
}
