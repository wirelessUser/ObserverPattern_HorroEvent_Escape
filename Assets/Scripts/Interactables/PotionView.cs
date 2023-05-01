using UnityEngine;

public class PotionView : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        GameService.Instance.GetInstructionView().HideInstruction();
        GameService.Instance.GetSoundView().PlaySoundEffects(SoundType.DrinkPotion);
        EventService.Instance.PotionDrinkEvent.InvokeEvent();
        gameObject.SetActive(false);
    }
}
