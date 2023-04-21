using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 originalPosition;
    [SerializeField] private float defaultShakeDuration; // do not use m_ naming convention , its used in C++ not in C#
    [SerializeField] private float defaultShakeMagnitude;

    private void Start()
    {
        originalPosition = transform.localPosition;
    }

    private void OnEnable()
    {
        LightsOffEventTrigger.OnLightsOff += Shake;
        RatRushEventTrigger.OnRatRush += Shake;
        SkullDropEventTrigger.OnSkullDrop += Shake;
        PlayerSanity.OnPlayerDeath += Shake;
    }

    private void OnDisable()
    {
        LightsOffEventTrigger.OnLightsOff -= Shake;
        RatRushEventTrigger.OnRatRush -= Shake;
        SkullDropEventTrigger.OnSkullDrop -= Shake;
        PlayerSanity.OnPlayerDeath -= Shake;
    }

    public void Shake()
    {
        StartCoroutine(ShakeCoroutine(defaultShakeDuration, defaultShakeMagnitude));
    }

    private IEnumerator ShakeCoroutine(float shakeDuration, float shakeMagnitude)
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            float x = Random.Range(-shakeMagnitude, shakeMagnitude);
            float y = Random.Range(-shakeMagnitude, shakeMagnitude);

            transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y /*+ y*/, originalPosition.z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
