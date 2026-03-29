using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GameManager centraliza o estado do jogo
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum Phase { Morning, Afternoon, Night }
    public enum Difficulty { Easy, Medium, Hard }

    [Header("Configurações globais")]
    public Difficulty currentDifficulty = Difficulty.Easy;
    public Phase currentPhase = Phase.Morning;

    [Tooltip("Duração das fases em segundos")]
    public PhaseDuration[] phaseDurations;

    [System.Serializable]
    public struct PhaseDuration
    {
        public Phase phase;
        public float duration;
    }

    [Header("Pontuação")]
    public int score = 0;
    public int highScore = 0;

    [Header("Progressão")]
    public int trashCollected = 0;
    public int trashToNextLevel;

    void UpdateDifficultyGoals()
    {
        switch (currentDifficulty)
        {
            case Difficulty.Easy:
                trashToNextLevel = 8;
                break;

            case Difficulty.Medium:
                trashToNextLevel = 12;
                break;

            case Difficulty.Hard:
                trashToNextLevel = 16;
                break;
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        UpdateDifficultyGoals();

        StartPhase(currentPhase);
    }

    public void StartPhase(Phase phase)
    {
        currentPhase = phase;
        TransitionController.Instance?.SetPhase(phase);
    }

    public void AdvancePhase()
    {
        currentPhase = currentPhase switch
        {
            Phase.Morning => Phase.Afternoon,
            Phase.Afternoon => Phase.Night,
            Phase.Night => Phase.Morning,
            _ => Phase.Morning
        };
        StartPhase(currentPhase);
    }

    public void SetDifficulty(Difficulty diff)
    {
        currentDifficulty = diff;
    }

    // 🔥 AQUI ESTÁ A MÁGICA
    public void CollectTrash(int amount)
    {
        trashCollected += amount;

        // Atualiza score também
        AddScore(amount);

        Debug.Log($"Lixo coletado: {trashCollected}/{trashToNextLevel}");

        if (trashCollected >= trashToNextLevel)
        {
            NextLevel();
        }
    }

    // 🚀 PROGRESSÃO DE FASE
    void NextLevel()
    {
        trashCollected = 0;
        trashToNextLevel += 5; // aumenta meta

        // aumenta dificuldade até o máximo
        if (currentDifficulty != Difficulty.Hard)
        {
            currentDifficulty++;
        }

        AdvancePhase(); // muda fase do dia também

        Debug.Log("SUBIU DE NÍVEL 🚀");
    }

    public void AddScore(int amount)
    {
        score += amount;

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        UIManager.Instance?.UpdateScore(score, highScore);
    }

    public float GetCurrentPhaseDuration()
    {
        foreach (var d in phaseDurations)
        {
            if (d.phase == currentPhase) return d.duration;
        }
        return 60f;
    }

    public Phase GetPhase() => currentPhase;
}