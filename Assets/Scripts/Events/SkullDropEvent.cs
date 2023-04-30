using UnityEngine;

public class SkullDropEvent : MonoBehaviour
{
    [SerializeField] private int keysRequiredToTrigger;
    [SerializeField] private Transform Skulls;
    [SerializeField] private SoundService soundService;
    [SerializeField] private SoundType soundToPlay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerView>() != null && GameService.Instance.GetPlayerController().GetKeys() >= keysRequiredToTrigger)
        {
            EventService.Instance.SkullDropEvent.InvokeEvent();
            OnSkullDrop();
            soundService.PlaySoundEffects(soundToPlay);
            GetComponent<Collider>().enabled = false;
        }
    }

    private void OnSkullDrop()
    {
        Skulls.gameObject.SetActive(true);
    }
}
