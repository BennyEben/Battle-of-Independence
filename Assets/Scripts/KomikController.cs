using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KomikController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string cerita = PlayerPrefs.GetString("ceritaDipilih", "A"); // default "A" kalau belum diset
        Debug.Log("Cerita yang dipilih: " + cerita);

        // Lanjutkan: tampilkan komik yang sesuai
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
