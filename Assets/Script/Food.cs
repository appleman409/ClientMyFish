using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    [SerializeField] private float food = 7200;
    [SerializeField] private bool hastarget = false;

    public float getfood()
    {
        return food;
    }

    public void settarget()
    {
        hastarget = true;
    }

    public bool getTarget()
    {
        return hastarget;
    }
    
}
