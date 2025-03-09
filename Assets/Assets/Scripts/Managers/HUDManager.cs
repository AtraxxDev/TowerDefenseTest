using UnityEngine;
using UnityEngine.UIElements;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;

    private void Start()
    {
        GameManager.Instance.OnPhaseChanged += OnPhaseChanged;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPhaseChanged -= OnPhaseChanged;
    }



    private void OnPhaseChanged(GamePhase newPhase)
    {
        // Ocultar todos los paneles
        foreach (var panel in panels)
        {
            panel.SetActive(false);
        }

        switch (newPhase)
        {
            case GamePhase.Planning:
                panels[0].SetActive(true);  // Activa el HUD del Building en la fase de planificación
                panels[3].SetActive(false);
                break;
            case GamePhase.Wave:
                panels[0].SetActive(false);  // Activa el HUD del Building en la fase de planificación
                panels[3].SetActive(true);
                break;
            case GamePhase.GameOver:
                panels[1].SetActive(true);  
                break;
            case GamePhase.Winning:
                panels[2].SetActive(true); 
                break;
        }
    }

}
