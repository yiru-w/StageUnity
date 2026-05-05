using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraviterLineaire : MonoBehaviour, IDestroyGame
{
    private float currentFallSpeed;

    private float baseFallSpeed;

    private GameDifficultManager gameDifficultManager;

    private EventManager eventManager;

    private bool isFalling = true;

    public void DestroyGame()
    {
        isFalling = false;
    }

    private void OnEnable()
    {
        isFalling = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameDifficultManager = FindObjectOfType<GameDifficultManager>();
        if (gameDifficultManager != null)
        {
            RefreshFallSpeed(); // Initial fall speed based on difficulty
            gameDifficultManager.OnDifficultChange += RefreshFallSpeed;
        }

        eventManager = FindObjectOfType<EventManager>();
        if (eventManager != null)
        {
            ApplyEffet(); // Apply any current effect at the start
            eventManager.OnEffetStart += ApplyEffet;
            eventManager.OnEffetEnd += RemoveEffet; 
        }
    }


    private void RefreshFallSpeed()
    {
        baseFallSpeed = gameDifficultManager.GetNiveauDifficulty() * 2f;

        // If there's an active effect, apply it to the new base fall speed
        if (eventManager != null && eventManager.GetEffetCurrent() is EffetMultiplyGraviter effetMultiple)
        {
            currentFallSpeed = baseFallSpeed * effetMultiple.GetMultiplyCurrent();
        }
        else
        {
            currentFallSpeed = baseFallSpeed;
        }
    }

    private void ApplyEffet()
    {
        Effet current = eventManager.GetEffetCurrent();
        if (current is EffetMultiplyGraviter effetMultiple)
        {
            currentFallSpeed = baseFallSpeed * effetMultiple.GetMultiplyCurrent();
        }
    }

    private void RemoveEffet()
    {
        currentFallSpeed = baseFallSpeed;
    }

        // Update is called once per frame
    void Update()
    {
        if (isFalling) 
        { 
            transform.Translate(Vector3.down * currentFallSpeed * Time.deltaTime);
        }
    }

    private void OnDestroy()
    {
        if (gameDifficultManager != null) gameDifficultManager.OnDifficultChange -= RefreshFallSpeed;
        if (eventManager != null)
        {
            eventManager.OnEffetStart -= ApplyEffet;
            eventManager.OnEffetEnd -= RemoveEffet;
        }
    }
}
