using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectController : MonoBehaviour
{
    public Sprite[] imageColor;
    public GameObject[] gbrCerita;

    private string ceritaDipilih = "";

    // Start is called before the first frame update
    private int ceritaSaatIniIndex;
    private const string KUNCI_PROGRES = "ProgresCerita"; // Kunci untuk menyimpan data

    void Start()
    {
        // Muat progres terakhir saat game dimulai. Jika belum ada, mulai dari 0 (cerita pertama).
        ceritaSaatIniIndex = PlayerPrefs.GetInt(KUNCI_PROGRES, 0);

        // Tampilkan cerita yang sesuai dengan progres
        TampilkanCeritaSaatIni();
    }

    // Fungsi untuk menampilkan cerita yang aktif dan menyembunyikan yang lain
    void TampilkanCeritaSaatIni()
    {
        // Loop melalui semua object cerita
        for (int i = 0; i < gbrCerita.Length; i++)
        {
            
            // Jika indeks object sama dengan progres pemain, aktifkan. Jika tidak, nonaktifkan.
            if (i == ceritaSaatIniIndex)
            {
                gbrCerita[i].GetComponent<Image>();
            }
            else
            {
                
            }
        }
    }

    public void PilihCerita(string idCerita)
    {
        ceritaDipilih = idCerita;
        PlayerPrefs.SetString("ceritaDipilih", idCerita);
        Debug.LogWarning("Cerita yang dipilih: " + idCerita);
    }

    public void PlayGame()
    {
        if (!string.IsNullOrEmpty(ceritaDipilih))
        {
            SceneManager.LoadScene("Komik");
        }
        else
        {
            Debug.LogWarning("Pilih cerita dulu!");
        }
    }
    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }


}
