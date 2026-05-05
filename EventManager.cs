using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    
    [SerializeField] private List<Effet> effets = new List<Effet>();

    [SerializeField] private float timeBetweenEffetsMin = 15f;

    [SerializeField] private float timeBetweenEffetsMax = 20f;

    [SerializeField] private float dureeEffet = 7f;

    private float timerBetweenEffets;

    private float timerDureeEffet;

    private Effet effetCurrent;

    public event Action OnEffetStart;

    public event Action OnEffetEnd;


    private void OnEnable()
    {
        timerBetweenEffets = UnityEngine.Random.Range(timeBetweenEffetsMin, timeBetweenEffetsMax);
        timerDureeEffet = dureeEffet;
    }

    // Update is called once per frame
    void Update()
    {
        if (effetCurrent == null)
        {
            timerBetweenEffets -= Time.deltaTime;
            if (timerBetweenEffets <= 0f)
            {
                StartEffet();
            }
        }
        else
        {
            timerDureeEffet -= Time.deltaTime;
            if (timerDureeEffet <= 0f)
            {
                StopEffet();
            }
        }
    }

    public Effet GetEffetCurrent()
    {
        return effetCurrent;
    }
    public float GetDureeEffet()
    {
        return dureeEffet;
    }

    private void StartEffet()
    {
        if (effets == null || effets.Count == 0) return;

        int indexEffet = UnityEngine.Random.Range(0, effets.Count);
        effetCurrent = effets[indexEffet];

        effetCurrent.ActiverEffet();
        OnEffetStart?.Invoke();

        timerDureeEffet = dureeEffet;
    }

    private void StopEffet()
    {
        effetCurrent = null;
        OnEffetEnd?.Invoke();

        timerBetweenEffets = UnityEngine.Random.Range(timeBetweenEffetsMin, timeBetweenEffetsMax);
    }
}
