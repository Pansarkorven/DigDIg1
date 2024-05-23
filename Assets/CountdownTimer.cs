using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    public float countdownTime = 5f;
    public GameObject Svärd;

    private float currentTime;

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            if (Svärd != null && !Svärd.activeSelf)
            {
                Svärd.SetActive(true);
            }
        }
    }
}
