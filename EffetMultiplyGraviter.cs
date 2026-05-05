using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffetMultiplyGraviter : Effet
{
    [SerializeField] private float accelerationMultiplyMin = 1.1f;

    [SerializeField] private float accelerationMultiplyMax = 1.3f;

    [SerializeField] private float slowMultiplyMin = 0.7f;

    [SerializeField] private float slowMultiplyMax = 0.9f;

    private float multiplyCurrent;

    public override void ActiverEffet()
    {
        bool isAccelerating = Random.value > 0.5f;
        if (isAccelerating)
        {
            multiplyCurrent = Random.Range(accelerationMultiplyMin, accelerationMultiplyMax);
        }
        else
        {
            multiplyCurrent = Random.Range(slowMultiplyMin, slowMultiplyMax);
        }
    }

    public float GetMultiplyCurrent()
    {
        return multiplyCurrent;
    }
}
