using System.Collections;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    private Vector3 originalPosition;
    [SerializeField] private float m_DefaultShakeDuration;
    [SerializeField] private float m_DefaultShakeMagnitude;

    private void Start()
    {
        originalPosition = transform.localPosition;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            Shake(m_DefaultShakeDuration, m_DefaultShakeMagnitude);
        }
    }

    public void Shake(float shakeDuration, float shakeMagnitude)
    {
        StartCoroutine(ShakeCoroutine(shakeDuration, shakeMagnitude));
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
