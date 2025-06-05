using UnityEngine;
using UnityEngine.SceneManagement;

public class KomikLoader : MonoBehaviour
{
    public GameObject panelKomikObject;

    public Sprite[] komikCerita10Nov;
    public Sprite[] komikCeritaAmbarawa;
    public Sprite[] komikCeritaBandungLautanApi;
    public Sprite[] komikCeritaMedanArea;
    public Sprite[] komikCerita1Maret1949;

    private int index = 0;
    private Sprite[] komikSekarang;
    private SpriteRenderer panelKomikRenderer;

    private string ceritaDipilih;
    private int triggerHalaman = -1;

    void Start()
    {
        panelKomikRenderer = panelKomikObject.GetComponent<SpriteRenderer>();
        ceritaDipilih = PlayerPrefs.GetString("ceritaDipilih","10Nov");
        Debug.Log(PlayerPrefs.GetString("ceritaDipilih"));

        // Tentukan komik dan titik pindah scene berdasarkan cerita
        switch (ceritaDipilih)
        {
            case "10Nov":
                komikSekarang = komikCerita10Nov;
                triggerHalaman = 5;
                Debug.Log("10 Nov SET");
                break;
            case "Ambarawa":
                komikSekarang = komikCeritaAmbarawa;
                triggerHalaman = 6;
                break;
            case "BandungLautanApi":
                komikSekarang = komikCeritaBandungLautanApi;
                triggerHalaman = 4;
                break;
            case "MedanArea":
                komikSekarang = komikCeritaMedanArea;
                triggerHalaman = 7;
                break;
            case "1Maret1949":
                komikSekarang = komikCerita1Maret1949;
                triggerHalaman = 5;
                break;
        }

        TampilkanHalaman(0);
    }

    public void TampilkanHalaman(int i)
    {
        Debug.Log("Tampil");
        if (komikSekarang != null && i < komikSekarang.Length && panelKomikRenderer != null)
        {
            Debug.Log("Start");
            index = i;
            panelKomikRenderer.sprite = komikSekarang[i];
        }
    }

    public void HalamanBerikutnya()
    {
        if (komikSekarang == null) return;

        int nextIndex = index + 1;

        if (nextIndex == triggerHalaman)
        {
            SceneManager.LoadScene("Fight");
            return;
        }

        if (nextIndex < komikSekarang.Length)
        {
            TampilkanHalaman(nextIndex);
        }
    }
    void OnMouseDown()
    {
        HalamanBerikutnya();
    }

}
