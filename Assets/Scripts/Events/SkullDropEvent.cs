using UnityEngine;

public class SkullDropEvent : MonoBehaviour
{
    [SerializeField] private int keysRequiredToTrigger;
    [SerializeField] private SoundType soundToPlay;
    [SerializeField] private Transform Skulls;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null && PlayerController.Instance.KeysEquipped >= keysRequiredToTrigger)
        {
            EventService.Instance.SkullDropEvent.InvokeEvent();
            OnSkullDrop();
            SoundManager.Instance.PlaySoundEffects(soundToPlay, false);
            GetComponent<Collider>().enabled = false;
        }
    }

    private void OnSkullDrop()
    {
        Skulls.gameObject.SetActive(true);
    }
}
