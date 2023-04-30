using UnityEngine;

public class Potion : MonoBehaviour, IInteractable
{
    [SerializeField] private SoundService soundService;
    public void Interact()
    {
        GameService.Instance.GetInstructionView().HideInstruction();
        EventService.Instance.PotionDrinkEvent.InvokeEvent();
        soundService.PlaySoundEffects(SoundType.DrinkPotion);
        gameObject.SetActive(false);
    }
}
