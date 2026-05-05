using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    [SerializeField] private ObjectType objectType;

    [SerializeField] private float bottomDestroy;

    [SerializeField] private int health;

    [SerializeField] private int score;

    [SerializeField] private float distanceThreshold = 0.3f;

    public static event Action<ObjectType> OnObjectChangeGoodObject;

    private SingeController player;

    private EventManager eventManager;

    private SpriteRenderer spriteRenderer;

    public void OnDisable()
    {
        Destroy(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        player = FindObjectOfType<SingeController>();

        eventManager = FindObjectOfType<EventManager>();
        if(eventManager != null)
        {
            ChangeGoodObject();
            ApplyEffetMultiplyGainedScore();
            eventManager.OnEffetStart += ApplyEffetMultiplyGainedScore;
            eventManager.OnEffetEnd += RemoveEffetMultiplyGainedScore;
        }
    }

    private void ChangeGoodObject()
    {
        Effet current = eventManager.GetEffetCurrent();
        if (current is EffetChangeGoodObject)
        {
            if (objectType == ObjectType.Bomb || objectType == ObjectType.Ghost)
            {
                OnObjectChangeGoodObject?.Invoke(objectType);
            }
        }
    }

    private void ApplyEffetMultiplyGainedScore()
    {
        Effet current = eventManager.GetEffetCurrent();
        if (current is EffetMultiplyGainedScore effetMultiplyGainedScore)
        {
            score = (int)(score * effetMultiplyGainedScore.GetMultiplyCurrent());
        }
    }

    private void RemoveEffetMultiplyGainedScore()
    {
        Effet current = eventManager.GetEffetCurrent();
        if (current is EffetMultiplyGainedScore effetMultiplyGainedScore)
        {
            score = (int)(score / effetMultiplyGainedScore.GetMultiplyCurrent());
        }
    }


    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.GetPanierPosition().position);
        if (distanceToPlayer <= distanceThreshold)
        {
            ParticleInstancier instancier = FindObjectOfType<ParticleInstancier>();
            if(instancier != null)
            {
                instancier.PlayParticle(transform.position, spriteRenderer.sprite);
            }
            AddHealth();
            AddScore();
            Destroy(gameObject);
        }
        if (transform.position.y < bottomDestroy)
        {
            Destroy(gameObject);
        }
    }

    private void AddHealth()
    {
        HealthManager.Instance.AddHealth(health);
    }

    private void AddScore()
    {
        ScoreManager.Instance.AddScore(score);
    }
    
    public ObjectType GetObjectType()
    {
        return objectType;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceThreshold);
    }

    private void OnDestroy()
    {
        if (eventManager != null)
        {
            eventManager.OnEffetStart -= ApplyEffetMultiplyGainedScore;
            eventManager.OnEffetEnd -= RemoveEffetMultiplyGainedScore;
        }
    }
}
