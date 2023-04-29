using UnityEngine;

public class Key : MonoBehaviour, I_Interactable
{
    //TODO -> Key Counter SHould be in Player Scriptable Object
    private static int keysEquipped = 0;
    [SerializeField] private SoundService soundService;

    public void Interact()
    {
        Debug.Log("Key Picked Up");
        UIManager.Instance.ShowInteractInstructions(false);
        
        keysEquipped++;
        EventService.Instance.KeyPickedUpEvent.InvokeEvent(keysEquipped);
        soundService.PlaySoundEffects(SoundType.KeyPickUp);
        gameObject.SetActive(false);
    }
}
