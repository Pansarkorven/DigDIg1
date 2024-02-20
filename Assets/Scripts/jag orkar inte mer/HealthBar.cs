using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Image FillImage;
    public float CurrentHP;

    public void UpdateHP()
    {
       // FillImage.fillAmount = CurrentHP / 100;
    }

}
