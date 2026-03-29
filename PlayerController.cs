using System.Collections;
using UnityEngine;
using TMPro; // TextMeshPro

public class PlayerController : MonoBehaviour
{
    [Header("Movimento lateral")]
    public float speed = 5f;
    private float originalSpeed;

    [Header("Limites laterais")]
    public float minX = -6f;
    public float maxX = 6f;

    [Header("Avanço automático")]
    public float forwardSpeed = 2f;
    private float currentY;

    [Header("Estado")]
    private bool isSlowed = false;

    [Header("Pontuação")]
    public int score = 0;           // pontuação atual
    public TMP_Text scoreText;      // referência ao texto na tela (TextMeshPro)

    void Start()
    {
        originalSpeed = speed;
        currentY = transform.position.y;

        UpdateScoreUI(); // inicializa a UI da pontuação
    }

    void Update()
    {
        // Movimento lateral
        float moveX = Input.GetAxis("Horizontal");

        // Avanço automático
        currentY += forwardSpeed * Time.deltaTime;

        // Nova posição
        Vector3 newPosition = new Vector3(
            transform.position.x + moveX * speed * Time.deltaTime,
            currentY,
            transform.position.z
        );

        // Limita dentro da tela
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);

        transform.position = newPosition;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Trash"))
        {
            Debug.Log("Pegou lixo! Objeto: " + other.name);
            Destroy(other.gameObject);
            AddScore(10); // soma 10 pontos
        }
        else if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Bateu no obstáculo! Objeto: " + other.name);
            AddScore(-5); // tira 5 pontos
            ApplyHitEffect();
        }
        else
        {
            Debug.Log("Colidiu com algo que não é Trash nem Obstacle: " + other.name + ", Tag: " + other.tag);
        }
    }

    void ApplyHitEffect()
    {
        if (isSlowed) return;

        // Recuo visual
        currentY -= 1.5f;

        StartCoroutine(SlowEffect());
    }

    IEnumerator SlowEffect()
    {
        isSlowed = true;

        speed = originalSpeed * 0.1f; // reduz velocidade temporariamente

        yield return new WaitForSeconds(2f);

        speed = originalSpeed;

        isSlowed = false;
    }

    void AddScore(int points)
    {
        score += points;
        if (score < 0) score = 0; // evita pontuação negativa
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score.ToString();
    }
}