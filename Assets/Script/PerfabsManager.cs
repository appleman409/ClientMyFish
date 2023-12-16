using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfabsManager : MonoBehaviour
{
    public static PerfabsManager instance;
    [Header("Perfabs Object")]
    [SerializeField] private GameObject ScenceCreateAcc;

    public void LoadScenceCreateAcc()
    {
        Instantiate(ScenceCreateAcc, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
