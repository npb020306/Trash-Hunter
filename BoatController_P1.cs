using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Movimento básico do barquinho usando teclado (WASD ou setas).
[RequireComponent(typeof(Rigidbody2D))]
public class BoatController_P1 : MonoBehaviour
{
    [Header("Configurações de movimento")]
    public float speed = 4f;

    [Header("Efeito de dano")]
    public float slowSpeed = 2f;
    public float slowDuration = 2f;

    private float originalSpeed;
    private bool isSlowed = false;

    private Rigidbody2D rb;
    private Vector2 inputDir;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalSpeed = speed;
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        inputDir = new Vector2(h, v).normalized;
    }

    void FixedUpdate()
    {
        // 🚤 movimento livre (frente, trás e lados)
        rb.velocity = inputDir * speed;
    }

    void LateUpdate()
    {
        // 📱 limita o barco dentro da tela
        Vector3 pos = transform.position;

        float camHeight = Camera.main.orthographicSize;
        float camWidth = camHeight * Camera.main.aspect;

        float minX = -camWidth + 0.5f;
        float maxX = camWidth - 0.5f;

        float minY = -camHeight + 0.5f;
        float maxY = camHeight - 0.5f;

        // limita só pros lados
        pos.x = Mathf.Clamp(pos.x, minX, maxX);

        // NÃO limita o eixo Y
        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            if (!isSlowed)
            {
                StartCoroutine(SlowEffect());
                GameManager.Instance.AddScore(-5);
            }
        }
    }

    IEnumerator SlowEffect()
    {
        isSlowed = true;

        speed = slowSpeed;

        yield return new WaitForSeconds(slowDuration);

        speed = originalSpeed;

        isSlowed = false;
    }
}