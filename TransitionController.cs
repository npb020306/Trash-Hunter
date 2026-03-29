using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionController : MonoBehaviour
{
    // Singleton simples
    public static TransitionController Instance { get; private set; }

    [Header("Definições de iluminação por fase")]
    public PhaseLighting morning;
    public PhaseLighting afternoon;
    public PhaseLighting night;

    [System.Serializable]
    public class PhaseLighting
    {
        public Color ambientColor;
        public float fogDensity;
        public Color cameraBackgroundColor;
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
        // Inicia com a fase atual do GameManager
        SetPhase(GameManager.Instance.GetPhase());
    }

    public void SetPhase(GameManager.Phase phase)
    {
        // Aplica configuração básica; você pode suavizar com uma cor de transição
        PhaseLighting pl = GetPhaseLighting(phase);

        // Iluminação ambiente
        RenderSettings.ambientLight = pl.ambientColor;

        // Névoa (visibilidade)
        RenderSettings.fog = true;
        RenderSettings.fogDensity = pl.fogDensity;

        // Cor de fundo da câmera
        if (Camera.main != null)
        {
            Camera.main.backgroundColor = pl.cameraBackgroundColor;
        }
    }

    private PhaseLighting GetPhaseLighting(GameManager.Phase phase)
    {
        return phase switch
        {
            GameManager.Phase.Morning => morning,
            GameManager.Phase.Afternoon => afternoon,
            GameManager.Phase.Night => night,
            _ => morning
        };
    }

    // Permite chamar uma transição suave no futuro (com Lerp)
}