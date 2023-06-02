using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartWeightProvider : MonoBehaviour
{
    // Peso do Collider (gameObject)
    public float weigth;
    private bool isGraviting;


    void Start()
    {
        isGraviting = true;
    }


    void Update()
    {
    }

    public float GetWeigth()
    {
        if (isGraviting)
        {
            return weigth;
        }

        return 0;
    }

    public void SetGraviting(bool graviting)
    {
        isGraviting = graviting;
    }
}
