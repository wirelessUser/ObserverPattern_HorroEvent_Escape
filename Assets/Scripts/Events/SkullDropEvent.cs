using UnityEngine;

public class SkullDropEvent : MonoBehaviour
{
    [SerializeField] private int keysRequiredToTrigger;
    [SerializeField] private Transform skulls;
    [SerializeField] private SoundType soundToPlay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerView>() != null && GameService.Instance.GetPlayerController().KeysEquipped >= keysRequiredToTrigger)
        {
            EventService.Instance.OnSkullDropEvent.InvokeEvent();
            onSkullDrop();
            GameService.Instance.GetSoundView().PlaySoundEffects(soundToPlay);
            GetComponent<Collider>().enabled = false;
        }
    }

    private void onSkullDrop()
    {
        skulls.gameObject.SetActive(true);
    }
}
