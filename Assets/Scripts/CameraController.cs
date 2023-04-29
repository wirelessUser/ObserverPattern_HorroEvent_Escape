using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 originalPosition;
    [SerializeField] private float defaultShakeDuration;
    [SerializeField] private float defaultShakeMagnitude;

    private void Start()
    {
        originalPosition = transform.localPosition;
    }

    private void OnEnable()
    {
        EventService.Instance.LightsOffByGhostEvent.AddListener(Shake);
        EventService.Instance.PlayerDeathEvent.AddListener(Shake);
        EventService.Instance.RatRushEvent.AddListener(Shake);
        EventService.Instance.SkullDropEvent.AddListener(Shake);
    }

    private void OnDisable()
    {
        EventService.Instance.LightsOffByGhostEvent.RemoveListener(Shake);
        EventService.Instance.PlayerDeathEvent.RemoveListener(Shake);
        EventService.Instance.RatRushEvent.RemoveListener(Shake);
        EventService.Instance.SkullDropEvent.RemoveListener(Shake);
    }

    public void Shake()
    {
        //Todo -> Improve Coroutine -> Take Ref, NUll check , StopCoroutine
        StartCoroutine(ShakeCoroutine(defaultShakeDuration, defaultShakeMagnitude));
    }

    private IEnumerator ShakeCoroutine(float shakeDuration, float shakeMagnitude)
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            float x = Random.Range(-shakeMagnitude, shakeMagnitude);
            float y = Random.Range(-shakeMagnitude, shakeMagnitude);

            transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y, originalPosition.z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
