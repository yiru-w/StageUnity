using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpwan : MonoBehaviour, IDestroyGame
{
    [SerializeField] private List<ObjectController> listObjects;

    [SerializeField] private Transform left;

    [SerializeField] private Transform right;
    
    private float betweenSpawnTime;

    private float count;

    private bool isSpawning = true;

    private GameDifficultManager gameDifficultManager;

    public void DestroyGame()
    {
        isSpawning = false;
    }

    private void OnEnable()
    {
        isSpawning = true;
        count = betweenSpawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        gameDifficultManager = FindObjectOfType<GameDifficultManager>();
        if (gameDifficultManager != null)
        {
            RefreshBetweenSpawnTime();
        }

        if (isSpawning)
        {
            if (count >= 0)
            {
                count -= Time.deltaTime;
                if (count <= 0)
                {
                    CreateObjetRandom();
                    count = betweenSpawnTime;
                }
            }
        }
    }

    private void RefreshBetweenSpawnTime()
    {
        betweenSpawnTime = 1.7f - 0.3f * gameDifficultManager.GetNiveauDifficulty();
    }

    // Generate a random position between left and right
    private void CreateObjetRandom()
    {
       
        var range = Random.Range(0, listObjects.Count);
        var ObjectControl = listObjects[range];

        var randomX = Random.Range(left.position.x, right.position.x);
        var vector2 = new Vector3(randomX, transform.position.y);

        var obj = Instantiate(ObjectControl,gameObject.transform);
        obj.transform.position = vector2;
    }
}
