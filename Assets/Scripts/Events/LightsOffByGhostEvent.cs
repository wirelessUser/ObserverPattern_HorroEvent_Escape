using UnityEngine;

public class LightsOffByGhostEvent : MonoBehaviour
{
    [SerializeField] private int keysRequiredToTrigger;
    [SerializeField] private SoundType soundToPlay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null && PlayerController.Instance.KeysEquipped == keysRequiredToTrigger)
        {
            EventService.Instance.LightsOffByGhostEvent.InvokeEvent();
            SoundManager.Instance.PlaySoundEffects(soundToPlay, false);
            GetComponent<Collider>().enabled = false;
        }
    }
}