using UnityEngine;

public class Potion : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        GameService.Instance.GetInstructionView().HideInstruction();
        GameService.Instance.GetSoundView().PlaySoundEffects(SoundType.DrinkPotion);
        EventService.Instance.PotionDrinkEvent.InvokeEvent();
        gameObject.SetActive(false);
    }
}
