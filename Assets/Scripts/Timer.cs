using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public float startTime = 10f; // jumlah detik awal
    private float currentTime;
    public TextMeshProUGUI timerText;
    public bool timerRunning = true;

    void Start()
    {
        currentTime = startTime;
    }

    void Update()
    {
        if (timerRunning)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                currentTime = 0;
                timerRunning = false;
                UpdateTimerDisplay();
                TimerEnd();
                return;
            }

            UpdateTimerDisplay();
        }
    }

    void UpdateTimerDisplay()
    {
        int seconds = Mathf.CeilToInt(currentTime);
        timerText.text = seconds.ToString("00");
    }

    void TimerEnd()
    {
        timerText.text = "00";
        Debug.Log("Timer selesai!");
        // Tambahkan aksi lain jika perlu
    }
}
