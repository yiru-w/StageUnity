using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameDifficulyDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TextMeshProUGUI;

    private GameDifficultManager gameDifficultManager;

    void Start()
    {
        gameDifficultManager = FindObjectOfType<GameDifficultManager>();
        if (gameDifficultManager != null)
        {
            gameDifficultManager.OnDifficultChange += UpdateDifficultyDisplay;
        }
    }

    private void UpdateDifficultyDisplay()
    {
        TextMeshProUGUI.text = $"Difficulty : {gameDifficultManager.GetNiveauDifficulty()}";
    }

    private void OnDestroy()
    {
        if (gameDifficultManager != null)
        {
            gameDifficultManager.OnDifficultChange -= UpdateDifficultyDisplay;
        }
    }
}
