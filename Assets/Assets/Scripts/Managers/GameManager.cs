using System;
using UnityEngine;

public enum GamePhase
{
    Planning, 
    Wave,
    GameOver,
    Winning
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GamePhase currentPhase;


    public event Action<GamePhase> OnPhaseChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        currentPhase = GamePhase.Planning;
        OnPhaseChanged?.Invoke(currentPhase);
    }

    private void Update()
    {
        // Dependiendo de la fase actual, se pueden habilitar o deshabilitar acciones
        HandleGamePhase();
    }


    private void HandleGamePhase()
    {
        switch (currentPhase)
        {
            case GamePhase.Planning:
                // En la fase de planificación, se puede construir
                //placementSystem.EnablePlacement();
                break;
            case GamePhase.Wave:
                // En la fase de ola, no se puede construir
                //placementSystem.DisablePlacement();
                break;
        }
    }

    [ContextMenu("Event Wave")]
    public void StartWave()
    {
        currentPhase = GamePhase.Wave;
        OnPhaseChanged?.Invoke(currentPhase);
    }
    [ContextMenu("Event Planning")]

    public void EndWave()
    {
        currentPhase = GamePhase.Planning;
        OnPhaseChanged?.Invoke(currentPhase);
    }

    public void IsGameOver()
    {
        currentPhase = GamePhase.GameOver;
        OnPhaseChanged?.Invoke(currentPhase);
    }

    public void IsWinning()
    {
        currentPhase = GamePhase.Winning;
        OnPhaseChanged?.Invoke(currentPhase);
    }

}
