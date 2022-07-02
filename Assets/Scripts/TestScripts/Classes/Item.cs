using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private float DegratationTime;
    private bool IsInorganic, reusable;
    private int EnvImpact;
    private string InfoData;

    public Item(float degradationTime, bool isInorganic, int envImpact, bool reusable, string infoData)
    {
        DegratationTime = degradationTime;
        IsInorganic = isInorganic;
        EnvImpact = envImpact;
        InfoData = infoData;

    }

    //Hola
}

