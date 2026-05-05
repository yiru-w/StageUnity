using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffetChangeGoodObject : Effet
{
    [SerializeField] private List<ObjectController> goodObjects;

    [SerializeField] private List<ObjectType> badObjects;

    void Start()
    {
        ObjectController objectController = FindObjectOfType<ObjectController>();
        if (objectController != null)
        {
            Instantiate(goodObjects[Random.Range(0, goodObjects.Count)], objectController.transform.position, Quaternion.identity);
            Destroy(objectController.gameObject);
        }
    }

    public override void ActiverEffet()
    {
       
        foreach (ObjectController objectControlle in FindObjectsOfType<ObjectController>())
        {
            if(badObjects.Contains(objectControlle.GetObjectType()))
            {
                Instantiate(goodObjects[Random.Range(0, goodObjects.Count)], objectControlle.transform.position, Quaternion.identity);
                Destroy(objectControlle.gameObject);
            }
        }
       
    }
}
