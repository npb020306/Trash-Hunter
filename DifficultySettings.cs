using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DifficultyParameters
{
    public int trashPerSecond;      // quantidade de lixo gerado por segundo (aprox.)
    public float obstacleSpawnEvery; // intervalo entre spawns de obstáculos
    public float fogDensity;         // densidade de névoa/fos
    public Color ambientColor;       // cor ambiente para a dificuldade
}

public class DifficultySettings : MonoBehaviour
{
    // Valores por dificuldade (você pode expor isso no Inspector para ajustar rapidamente)
    public DifficultyParameters Easy;
    public DifficultyParameters Medium;
    public DifficultyParameters Hard;

    public DifficultyParameters GetParameters(GameManager.Difficulty diff)
    {
        return diff switch
        {
            GameManager.Difficulty.Easy => Easy,
            GameManager.Difficulty.Medium => Medium,
            GameManager.Difficulty.Hard => Hard,
            _ => Easy
        };
    }

    // Ex.: use como GameManager.Instance.GetCurrentDifficultyParameters() se implementar o acesso
}