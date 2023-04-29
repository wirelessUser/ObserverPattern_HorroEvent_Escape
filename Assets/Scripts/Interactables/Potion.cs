using UnityEngine;

public class Potion : MonoBehaviour, I_Interactable
{
    [SerializeField] private SoundService soundService;
    public void Interact()
    {
        UIManager.Instance.ShowInteractInstructions(false);
        EventService.Instance.PotionDrinkEvent.InvokeEvent();
        soundService.PlaySoundEffects(SoundType.DrinkPotion);
        gameObject.SetActive(false);
    }
}
