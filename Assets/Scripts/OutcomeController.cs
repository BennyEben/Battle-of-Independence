using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutcomeController : MonoBehaviour
{
    private GameObject pauseCanvas;
    private bool isPaused = false;

    void Start()
    {
        pauseCanvas = GameObject.FindWithTag("pause");
        if (pauseCanvas != null)
        {
            pauseCanvas.SetActive(false);
        } 
        else 
        {
            Debug.LogError("Tidak dapat menemukan GameObject dengan tag 'pause'. Pastikan tag sudah diatur dengan benar pada Canvas Anda.");
        }
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ContinueGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // Fungsi untuk mem-pause permainan
    void PauseGame()
    {
        if (pauseCanvas != null)
        {
            // Tampilkan canvas pause menu
            pauseCanvas.SetActive(true);
        }
        
        // Hentikan waktu di dalam game (membuat semua animasi dan fisika berhenti)
        Time.timeScale = 0f;
        
        // Ubah status menjadi 'paused'
        isPaused = true;
    }

    // Fungsi ini sekarang berfungsi untuk melanjutkan permainan dari kondisi pause.
    public void ContinueGame()
    {
        if (pauseCanvas != null)
        {
            // Sembunyikan kembali canvas pause menu
            pauseCanvas.SetActive(false);
        }

        // Kembalikan waktu game ke normal
        Time.timeScale = 1f;

        // Ubah status menjadi tidak 'paused'
        isPaused = false;
    }

    public void Exit()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("MainMenu");
    }

    public void Retry()
    {
        // Pastikan timeScale kembali normal saat me-load scene baru
        Time.timeScale = 1f; 
        SceneManager.LoadScene("Fight");
    }

    public void Continue()
    {
        // Pastikan timeScale kembali normal saat me-load scene baru
        Time.timeScale = 1f; 
        SceneManager.LoadScene("Komik");
    }
}
