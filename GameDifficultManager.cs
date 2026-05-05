using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDifficultManager : MonoBehaviour
{
    [SerializeField] private float timeDifficultChange = 3f;

    private float timeDifficultChangeCurrent;

    private int niveauDifficult = 1;

    public event Action OnDifficultChange;


    void OnEnable()
    {
        niveauDifficult = 1;
        timeDifficultChangeCurrent = timeDifficultChange;
    }

    // Update is called once per frame
    void Update()
    {
        if (niveauDifficult < 5)
        {
            timeDifficultChangeCurrent -= Time.deltaTime;
            if (timeDifficultChangeCurrent <= 0)
            {
                niveauDifficult++;
                OnDifficultChange?.Invoke();
                timeDifficultChangeCurrent = timeDifficultChange;
            }
        }
    }

    public int GetNiveauDifficulty()
    {
        return niveauDifficult;
    }
}
