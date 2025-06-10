using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public Sprite[] allBackground;
    public GameObject background; 
    private string ceritaDipilih;
    private SpriteRenderer backgroundSprite;
    // Start is called before the first frame update
    void Start()
    {
        backgroundSprite = background.GetComponent<SpriteRenderer>();
        ceritaDipilih = PlayerPrefs.GetString("ceritaDipilih", "10Nov");
        switch (ceritaDipilih)
        {
            case "10Nov":
                backgroundSprite.sprite = allBackground[0];
                break;
            case "Ambarawa":
                backgroundSprite.sprite = allBackground[1];
                break;
            case "BandungLautanApi":
                backgroundSprite.sprite = allBackground[2];
                break;
            case "MedanArea":
                backgroundSprite.sprite = allBackground[3];
                break;
            case "1Maret1949":
                backgroundSprite.sprite = allBackground[4];
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
