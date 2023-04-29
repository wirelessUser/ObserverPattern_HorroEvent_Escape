using UnityEngine;

public class Potion : MonoBehaviour, I_Interactable
{
    public void Interact()
    {
        UIManager.Instance.ShowInteractInstructions(false);
        EventService.Instance.PotionDrinkEvent.InvokeEvent();
        SoundManager.Instance.PlaySoundEffects(SoundType.DrinkPotion, false);
        gameObject.SetActive(false);
    }
}
