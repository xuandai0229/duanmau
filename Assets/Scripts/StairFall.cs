using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float fallDelay = 0.5f; // Thời gian chờ trước khi rơi
    [SerializeField] private float destroyDelay = 2f; // Thời gian bị hủy
    [SerializeField] private float shakeIntensity = 0.1f; // Độ rung
    [SerializeField] private float shakeDuration = 0.3f; // Thời gian rung

    private Rigidbody2D rb;
    private Vector3 originalPosition;
    private bool isFalling = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true; // Ban đầu cố định
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; // Ngăn chặn xoay
        originalPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isFalling)
        {
            isFalling = true;
            StartCoroutine(FallSequence());
        }
    }

    IEnumerator FallSequence()
    {
        yield return StartCoroutine(ShakeEffect()); // Hiệu ứng rung trước khi rơi

        yield return new WaitForSeconds(fallDelay);

        rb.isKinematic = false;
        rb.gravityScale = 2f; // Làm rơi nhanh hơn
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; // Đảm bảo rơi thẳng

        Destroy(gameObject, destroyDelay);
    }

    IEnumerator ShakeEffect()
    {
        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration)
        {
            float xOffset = Random.Range(-shakeIntensity, shakeIntensity);
            transform.position = originalPosition + new Vector3(xOffset, 0, 0);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPosition;
    }
}
