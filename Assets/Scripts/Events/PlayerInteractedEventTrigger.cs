using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractedEventTrigger : EventTrigger
{

    /*  private void Update()
      {
          if (Input.GetKeyDown(KeyCode.E))
              OnPlayerInteracted?.Invoke();
      }*/
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Interactable>() != null)
        {
            // PlayerInteractedEventTrigger.OnPlayerInteracted += InteractedWithKey;
            UIManager.OnPlayerNearInteractable?.Invoke();

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Player Interacted");
                OnPlayerInteracted?.Invoke();
                other.GetComponent<Interactable>().Interact();
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Interactable>() != null)
        {
            // PlayerInteractedEventTrigger.OnPlayerInteracted -= InteractedWithKey;
            UIManager.OnPlayerNotNearInteractable?.Invoke();
        }
    }
}
