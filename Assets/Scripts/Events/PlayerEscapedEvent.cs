using UnityEngine;

public class PlayerEscapedEvent : MonoBehaviour
{
    [SerializeField] private SoundService soundService;
    [SerializeField] private SoundType soundToPlay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerView>() != null)
        {
            soundService.PlaySoundEffects(soundToPlay);
            EventService.Instance.PlayerEscapedEvent.InvokeEvent();
        }
    }
}
