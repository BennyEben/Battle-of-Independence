using UnityEngine;
using TMPro;
using Ilumisoft.HealthSystem; // <-- DITAMBAHKAN: Untuk mengakses komponen Health
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public float startTime = 10f;
    private float currentTime;
    public TextMeshProUGUI timerText;
    public bool timerRunning = true;

    // Tarik GameObject Player dan Enemy dari Hierarchy ke slot ini di Inspector
    public GameObject player;
    public GameObject enemy;

    // Variabel untuk menyimpan komponen Health dari player dan enemy
    private Health playerHealth; // <-- DIUBAH: Dari HealthSystem menjadi Health
    private Health enemyHealth;  // <-- DIUBAH: Dari HealthSystem menjadi Health

    void Start()
    {
        currentTime = startTime;

        // Mengambil komponen health saat permainan dimulai
        if (player != null)
        {
            playerHealth = player.GetComponent<Health>(); // <-- DIUBAH
        }
        else
        {
            Debug.LogError("GameObject Player belum di-assign di CountdownTimer!");
        }
        
        if (enemy != null)
        {
            enemyHealth = enemy.GetComponent<Health>(); // <-- DIUBAH
        }
        else
        {
            Debug.LogError("GameObject Enemy belum di-assign di CountdownTimer!");
        }
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
        Debug.Log("Waktu Habis!");

        // Pastikan kedua komponen health ditemukan sebelum membandingkan
        if (playerHealth != null && enemyHealth != null)
        {
            // Mengambil nilai health saat ini dari properti CurrentHealth
            float pHealth = playerHealth.CurrentHealth; // <-- DIUBAH: Dari currentHealth ke CurrentHealth
            float eHealth = enemyHealth.CurrentHealth;  // <-- DIUBAH: Dari currentHealth ke CurrentHealth

            Debug.Log("Health Pemain: " + pHealth + " | Health Musuh: " + eHealth);

            if (pHealth > eHealth)
            {
                Debug.Log("Pemain Menang!");
                SceneManager.LoadScene("WinScene");
            }
            else if (eHealth > pHealth)
            {
                Debug.Log("Musuh Menang!");
                SceneManager.LoadScene("LoseScene");
            }
            else
            {
                Debug.Log("Seri!");
                SceneManager.LoadScene("LoseScene");
            }
        }
        else
        {
            Debug.LogError("Komponen Health tidak bisa ditemukan pada Player atau Enemy. Cek kembali pesan error di atas.");
        }
    }
}