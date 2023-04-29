using UnityEngine;

public class PlayerEscapedEvent : MonoBehaviour
{
    [SerializeField] private SoundType soundToPlay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            SoundManager.Instance.PlaySoundEffects(soundToPlay);
            EventService.Instance.PlayerEscapedEvent.InvokeEvent();
        }
    }
}
