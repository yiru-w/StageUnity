using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;


public class HealthDisplay : MonoBehaviour
{

    [SerializeField] private Slider healthSlider;


    // Start is called before the first frame update
    void Start()
    {
        HealthManager healthManager = HealthManager.Instance;
        healthSlider.maxValue = healthManager.GetHealthMax();
        healthManager.OnHealthChanged += UpdateHealthDisplay;
    }

    private void UpdateHealthDisplay()
    {
        healthSlider.value = HealthManager.Instance.GetHealth();
    }

    void OnDestroy()
    {
        if (HealthManager.Instance != null)
        {
            HealthManager.Instance.OnHealthChanged -= UpdateHealthDisplay;
        }
    }
}
