using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingeController : MonoBehaviour, IDestroyGame
{

    private Vector3 positionInitialise;

    [SerializeField] private Transform maxRangeLeft;

    [SerializeField] private Transform maxRangeRight;

    [SerializeField] private Transform panierPosition;  

    private bool isMoving = true;

    public void OnEnable()
    {
        isMoving = true;
        transform.position = positionInitialise;
    }

    public void DestroyGame()
    {
        isMoving = false;
    }

    public Transform GetPanierPosition()
    {
        return panierPosition;
    }

    public void Awake()
    {
        positionInitialise = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector2(Mathf.Clamp(mousePosition.x, maxRangeLeft.position.x, maxRangeRight.position.x), transform.position.y);
            }
        }
    }
}
