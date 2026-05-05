using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject gameOverScreen;

    [SerializeField] private Button buttonRestart;

    [SerializeField] private GameObject gameContainer;
    private void GameOver()
    {
        FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
            .OfType<IDestroyGame>()
            .ToList()
            .ForEach(x => x.DestroyGame());
        gameOverScreen.SetActive(true);
    }


    private void OnEnable()
    {
        gameOverScreen.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        HealthManager.Instance.OnHealthChanged += () =>
        {
            if (HealthManager.Instance.GetHealth() <= 0)
            {
                GameOver();   
            }
        };

        buttonRestart.onClick.AddListener(() =>
        {
            gameContainer.SetActive(false);
            gameContainer.SetActive(true);
        });
    }
}
