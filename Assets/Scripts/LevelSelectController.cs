using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectController : MonoBehaviour
{
    public Sprite[] imageSketch;
    public Sprite[] imageColor;
    public GameObject[] gbrCerita;
    public GameObject[] button;

    private Image imageGanti;
    private string ceritaDipilih = "";
    private int ceritaSaatIniIndex;
    private const string KUNCI_PROGRES = "ProgresCerita"; // Kunci untuk menyimpan data
    // Start is called before the first frame update
    void Start()
    {
         // Muat progres terakhir saat game dimulai. Jika belum ada, mulai dari 0 (cerita pertama).
        ceritaSaatIniIndex = PlayerPrefs.GetInt(KUNCI_PROGRES, 0);

        // Tampilkan cerita yang sesuai dengan progres
        TampilkanCeritaSaatIni();
    }

    // Update is called once per frame
    // Fungsi untuk menampilkan cerita yang aktif dan menyembunyikan yang lain
    void TampilkanCeritaSaatIni()
    {
        Debug.LogWarning("tampil");
        // Loop melalui semua object cerita
        for (int i = 0; i < gbrCerita.Length; i++)
        {
            imageGanti = gbrCerita[i].GetComponent<Image>();
            // Jika indeks object sama dengan progres pemain, aktifkan. Jika tidak, nonaktifkan.
            Debug.LogWarning(i);
            if (i <= ceritaSaatIniIndex)
            {
                Debug.LogWarning("set gambar");
                imageGanti.sprite = imageColor[i];
                button[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                Debug.LogWarning("gambar lain");
                imageGanti.sprite = imageSketch[i];
                button[i].GetComponent<Button>().interactable = false;
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
