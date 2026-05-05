using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;

    public static ScoreManager Instance { get; private set; }

    public event Action OnScoreChanged;

    public void OnEnable()
    {
        score = 0;
        OnScoreChanged?.Invoke();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        OnScoreChanged?.Invoke();
    }

    public int GetScore()
    {
        return score;
    }

    private void OnDestroy()
    {
        Instance = null;
        OnScoreChanged = null;
    }
}
