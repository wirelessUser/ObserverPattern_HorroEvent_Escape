using UnityEngine;

public class PotionView : MonoBehaviour, IInteractable
{
    private int potionEffect = 20;
    public void Interact()
    {
        GameService.Instance.GetInstructionView().HideInstruction();
        GameService.Instance.GetSoundView().PlaySoundEffects(SoundType.DrinkPotion);
        EventService.Instance.PotionDrinkEvent.InvokeEvent(potionEffect);
        gameObject.SetActive(false);
    }
}
