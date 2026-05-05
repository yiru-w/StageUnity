using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    [SerializeField] private int healthMax = 50;

    private int health;
    public static HealthManager Instance { get; private set; }

    public event Action OnHealthChanged;

    private void OnEnable()
    {
        health = healthMax;
        OnHealthChanged?.Invoke();
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

        health = healthMax;
    }

    public void AddHealth(int amount)
    {
        if(health + amount <= 0)
        {
            health = 0;
            OnHealthChanged?.Invoke();
        }
        else if (health + amount < healthMax)
        {
            health += amount;
            OnHealthChanged?.Invoke();
        }
    }

   

    public int GetHealth()
    {
        return health;
    }

    public int GetHealthMax()
    {
        return healthMax;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnDestroy()
    {
        Instance = null;
        OnHealthChanged = null;
    }
}
