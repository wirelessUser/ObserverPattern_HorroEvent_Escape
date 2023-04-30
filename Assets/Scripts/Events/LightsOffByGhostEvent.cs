using UnityEngine;

public class LightsOffByGhostEvent : MonoBehaviour
{
    [SerializeField] private int keysRequiredToTrigger;
    [SerializeField] private SoundService soundService;
    [SerializeField] private SoundType soundToPlay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerView>() != null && GameService.Instance.GetPlayerController().GetKeys() == keysRequiredToTrigger)
        {
            EventService.Instance.LightsOffByGhostEvent.InvokeEvent();
            soundService.PlaySoundEffects(soundToPlay);
            GetComponent<Collider>().enabled = false;
        }
    }
}