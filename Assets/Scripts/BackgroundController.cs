using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public Sprite[] allBackground;
    private Sprite background;
    private string ceritaDipilih;
    // Start is called before the first frame update
    void Start()
    {
        ceritaDipilih = PlayerPrefs.GetString("ceritaDipilih", "10Nov");
        switch (ceritaDipilih)
        {
            case "10Nov":
                background = allBackground[0];
                break;
            case "Ambarawa":
                background = allBackground[1];
                break;
            case "BandungLautanApi":
                background = allBackground[2];
                break;
            case "MedanArea":
                background = allBackground[3];
                break;
            case "1Maret1949":
                background = allBackground[4];
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
