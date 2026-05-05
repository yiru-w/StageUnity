using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventDisplay : MonoBehaviour
{
    [SerializeField] private GameObject eventDisplayContainer;

    [SerializeField] private float timeToDisplay = 1f;

    private bool isDisplaying = false;

    private float displayTimer;

    private EventManager eventManager;


    private void OnEnable()
    {
        isDisplaying = false;
        displayTimer = timeToDisplay;
        eventDisplayContainer.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {   
        eventManager = FindObjectOfType<EventManager>();
        if(eventManager != null)
        {
            eventManager.OnEffetStart += DisplayEvent;
        }
    }

    private void DisplayEvent()
    {
        Effet current = eventManager.GetEffetCurrent();
        eventDisplayContainer.gameObject.SetActive(true);
        isDisplaying = true;
        if(current is EffetMultiplyGraviter)
        {
            eventDisplayContainer.GetComponentInChildren<TextMeshProUGUI>().text = "Nouvel ¨¦v¨¦nement : Vitesse de chute multipli¨¦e par " + ((EffetMultiplyGraviter)current).GetMultiplyCurrent().ToString("0.00") + " !";
        }
        else if(current is EffetChangeGoodObject)
        {
            eventDisplayContainer.GetComponentInChildren<TextMeshProUGUI>().text = "Nouvel ¨¦v¨¦nement : Les objets mauvais deviennent bons !";
        }
        else if(current is EffetMultiplyGainedScore)
        {
            eventDisplayContainer.GetComponentInChildren<TextMeshProUGUI>().text = "Nouvel ¨¦v¨¦nement : Score multipli¨¦ par " + ((EffetMultiplyGainedScore)current).GetMultiplyCurrent().ToString("0.0") + " !";
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (isDisplaying)
        {
            timeToDisplay -= Time.deltaTime;
            if (timeToDisplay <= 0f)
            {
                eventDisplayContainer.gameObject.SetActive(false);
                isDisplaying = false;
                timeToDisplay = 1f; // Reset for the next event
            }
        }
    }

    private void OnDestroy()
    {
        if(eventManager != null)
        {
            eventManager.OnEffetStart -= DisplayEvent;
        }
    }
}
