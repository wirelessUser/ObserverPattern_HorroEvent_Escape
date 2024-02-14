using UnityEngine;

public class LightsOffByGhostEvent : MonoBehaviour
{
    [SerializeField] private int KeyForGhostTrigger;
    [SerializeField] private SoundType soundToPlay;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerView>() !=null && KeyForGhostTrigger == GameService.Instance.GetPlayerController().KeysEquipped)
        {
            EventService.Instance.OnLightsOffByGhostEvent.InvokeEvent();
            this.GetComponent<Collider>().enabled = false;
            GameService.Instance.GetSoundView().PlaySoundEffects(soundToPlay);
        }
    }
}