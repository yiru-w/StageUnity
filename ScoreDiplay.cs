using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDiplay : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        ScoreManager.Instance.OnScoreChanged += ChangeScore;
    }

    private void ChangeScore()
    {
        scoreText.text = "Score: " + ScoreManager.Instance.GetScore();
    }

    void OnDestroy()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged -= ChangeScore;
        }
    }
}
