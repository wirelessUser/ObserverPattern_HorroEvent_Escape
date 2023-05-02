using UnityEngine;

public class RatRushEvent : MonoBehaviour
{
    [SerializeField] private Transform rats;
    [SerializeField] private Transform target;
    [SerializeField] private SoundType soundToPlay;

    private float speed = 7.5f;
    private bool rushActive = false;
    private bool reachedTarget = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerView>() != null)
        {
            EventService.Instance.RatRushEvent.InvokeEvent();
            onRatRush();
            GameService.Instance.GetSoundView().PlaySoundEffects(soundToPlay);
            GetComponent<Collider>().enabled = false;
        }
    }

    void Update()
    {
        //Todo - Morning
        if (rushActive)
        {
            if (!reachedTarget)
            {
                rats.position = Vector3.MoveTowards(rats.position, target.position, speed * Time.deltaTime);

                if (rats.position == target.position)
                {
                    reachedTarget = true;
                }
            }
            else
            {
                rushActive = false;
                rats.gameObject.SetActive(false);
            }
        }
    }

    private void onRatRush()
    {
        rats.gameObject.SetActive(true);
        rushActive = true;
    }
}
