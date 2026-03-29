using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Referências UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI phaseText;
    public TextMeshProUGUI difficultyText;

    [Header("Feedback visual")]
    public TextMeshProUGUI feedbackText; // texto que aparece no meio da tela

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        UpdateScore(GameManager.Instance.score, GameManager.Instance.highScore);
        UpdatePhase(GameManager.Instance.GetPhase());
        UpdateDifficulty(GameManager.Instance.currentDifficulty);

        if (feedbackText != null)
            feedbackText.gameObject.SetActive(false);
    }

    public void UpdateScore(int score, int highScore)
    {
        if (scoreText != null) scoreText.text = "Score: " + score;
        if (highScoreText != null) highScoreText.text = "Recorde: " + highScore;
    }

    public void UpdatePhase(GameManager.Phase phase)
    {
        if (phaseText != null) phaseText.text = "Fase: " + phase.ToString();
    }

    public void UpdateDifficulty(GameManager.Difficulty diff)
    {
        if (difficultyText != null) difficultyText.text = "Dificuldade: " + diff.ToString();
    }

    // 🔥 MOSTRA TEXTO TEMPORÁRIO NA TELA
    public void ShowFeedback(string message)
    {
        if (feedbackText != null)
        {
            StopAllCoroutines(); // evita sobreposição bugada
            StartCoroutine(ShowFeedbackRoutine(message));
        }
    }

    IEnumerator ShowFeedbackRoutine(string message)
    {
        feedbackText.gameObject.SetActive(true);
        feedbackText.text = message;

        yield return new WaitForSeconds(1.5f);

        feedbackText.gameObject.SetActive(false);
    }
}