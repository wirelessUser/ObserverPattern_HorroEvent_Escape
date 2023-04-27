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
        EventManager.Instance.OnLightsOffByGhost += Shake;
        EventManager.OnRatRush += Shake;
        EventManager.OnSkullDrop += Shake;
        EventManager.OnPlayerDeath += Shake;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnLightsOffByGhost -= Shake;
        EventManager.OnRatRush -= Shake;
        EventManager.OnSkullDrop -= Shake;
        EventManager.OnPlayerDeath -= Shake;
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
