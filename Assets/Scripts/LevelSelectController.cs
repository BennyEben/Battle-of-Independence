using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectController : MonoBehaviour
{
    private string ceritaDipilih = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
