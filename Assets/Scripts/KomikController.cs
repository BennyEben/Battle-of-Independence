using UnityEngine;
using UnityEngine.UI;

public class KomikLoader : MonoBehaviour
{
    public Image panelKomik;
    public Sprite[] komikCerita10Nov;
    public Sprite[] komikCeritaAmbarawa;
    public Sprite[] komikCeritaBandungLautanApi;
    public Sprite[] komikCeritaMedanArea;
    public Sprite[] komikCerita1Maret1949;

    private int index = 0;
    private Sprite[] komikSekarang;

    void Start()
    {
        string cerita = PlayerPrefs.GetString("ceritaDipilih", "10Nov");

        switch (cerita)
        {
            case "10Nov":
                komikSekarang = komikCerita10Nov;
                break;
            case "Ambarawa":
                komikSekarang = komikCeritaAmbarawa;
                break;
            case "BandungLautanApi":
                komikSekarang = komikCeritaBandungLautanApi;
                break;
            case "MedanArea":
                komikSekarang = komikCeritaMedanArea;
                break;
            case "1Maret1949":
                komikSekarang = komikCerita1Maret1949;
                break;
        }

        TampilkanHalaman(0);
    }

    public void TampilkanHalaman(int i)
    {
        if (komikSekarang != null && i < komikSekarang.Length)
        {
            index = i;
            panelKomik.sprite = komikSekarang[i];
        }
    }

    public void HalamanBerikutnya()
    {
        if (index + 1 < komikSekarang.Length)
        {
            TampilkanHalaman(index + 1);
        }
    }
}
