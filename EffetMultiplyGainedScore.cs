using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffetMultiplyGainedScore : Effet
{
    [SerializeField] private float multiply = 2f;


    public override void ActiverEffet(){}

    public float GetMultiplyCurrent()
    {
        return multiply;
    }
}
