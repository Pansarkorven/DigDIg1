using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAreaTxt : MonoBehaviour
{
    [SerializeField] LayerMask TutorialArea1;
    [SerializeField] LayerMask TutorialArea2;
    [SerializeField] LayerMask TutorialArea3;
    [SerializeField] LayerMask TutorialArea4;

    [SerializeField] Transform InRangeOfUse;
    [SerializeField] float UseRange = 0.5f;
    [SerializeField] private GameObject TxtPrefabNr1;
    [SerializeField] private GameObject TxtPrefabNr2;
    [SerializeField] private GameObject TxtPrefabNr3;
    [SerializeField] private GameObject TxtPrefabNr4;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TutorialThing1();
        TutorialThing2();
        TutorialThing3();
        TutorialThing4();
    }
  

    public void TutorialThing1()
    {
        Collider2D[] Tutorail1 = Physics2D.OverlapCircleAll(InRangeOfUse.position, UseRange, TutorialArea1);

        if (Tutorail1.Length > 0)
        {
            if (TxtPrefabNr1 != null)
            {
                TxtPrefabNr1.SetActive(true);
            }
        }
        else
        {
            if (TxtPrefabNr1 != null)
            {
                TxtPrefabNr1.SetActive(false);
            }
        }
    }
    public void TutorialThing2()
    {
        Collider2D[] Tutorail2 = Physics2D.OverlapCircleAll(InRangeOfUse.position, UseRange, TutorialArea2);

        if (Tutorail2.Length > 0)
        {
            if (TxtPrefabNr2 != null)
            {
                TxtPrefabNr2.SetActive(true);
            }
        }
        else
        {
            if (TxtPrefabNr2 != null)
            {
                TxtPrefabNr2.SetActive(false);
            }
        }
    }
    public void TutorialThing3()
    {
        Collider2D[] Tutorail3 = Physics2D.OverlapCircleAll(InRangeOfUse.position, UseRange, TutorialArea3);

        if (Tutorail3.Length > 0)
        {
            if (TxtPrefabNr3 != null)
            {
                TxtPrefabNr3.SetActive(true);
            }
        }
        else
        {
            if (TxtPrefabNr3 != null)
            {
                TxtPrefabNr3.SetActive(false);
            }
        }
    }
    public void TutorialThing4()
    {
        Collider2D[] Tutorail4 = Physics2D.OverlapCircleAll(InRangeOfUse.position, UseRange, TutorialArea4);

        if (Tutorail4.Length > 0)
        {
            if (TxtPrefabNr4 != null)
            {
                TxtPrefabNr4.SetActive(true);
            }
        }
        else
        {
            if (TxtPrefabNr4 != null)
            {
                TxtPrefabNr4.SetActive(false);
            }
        }
    }
}

