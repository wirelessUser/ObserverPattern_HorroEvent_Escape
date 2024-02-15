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
            OnSkullDrop();
            GameService.Instance.GetSoundView().PlaySoundEffects(soundToPlay);
            EventService.Instance.OnSkullDrop.InvokeEvent();
            GetComponent<Collider>().enabled = false;
        }
    }

    private void OnSkullDrop()
    {
        skulls.gameObject.SetActive(true);
    }
}
