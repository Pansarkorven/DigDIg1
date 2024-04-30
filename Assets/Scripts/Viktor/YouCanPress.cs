using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouCanPress : MonoBehaviour
{
    [SerializeField] LayerMask Interactibles;
    [SerializeField] LayerMask OneWaysPlatfroms;
    [SerializeField] Transform InRangeOfUse;
    [SerializeField] float UseRange = 0.5f;
    [SerializeField] private GameObject TxtPrefab1;
    [SerializeField] private GameObject TxtPrefab2;

    void Update()
    {
        CanUse();
        OneWays();
    }

    public void CanUse()
    {
        Collider2D[] UseStuff = Physics2D.OverlapCircleAll(InRangeOfUse.position, UseRange, Interactibles);

        if (UseStuff.Length > 0)
        {
            if (TxtPrefab1 != null)
            {
                TxtPrefab1.SetActive(true);
            }
        }
        else
        {
            if (TxtPrefab1 != null)
            {
                TxtPrefab1.SetActive(false);
            }
        }
    }
    public void OneWays()
    {
        Collider2D[] UseStuff = Physics2D.OverlapCircleAll(InRangeOfUse.position, UseRange, OneWaysPlatfroms);

        if (UseStuff.Length > 0)
        {
            if (TxtPrefab2 != null)
            {
                TxtPrefab2.SetActive(true);
            }
        }
        else
        {
            if (TxtPrefab2 != null)
            {
                TxtPrefab2.SetActive(false);
            }
        }
    }
}
