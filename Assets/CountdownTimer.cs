using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    public float countdownTime = 5f;
    public GameObject Sv�rd;

    private float currentTime;

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            if (Sv�rd != null && !Sv�rd.activeSelf)
            {
                Sv�rd.SetActive(true);
            }
        }
    }
}
