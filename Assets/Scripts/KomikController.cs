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

        // Ambil cerita & halaman yang terakhir dipilih (atau default)
        ceritaDipilih = PlayerPrefs.GetString("ceritaDipilih", "10Nov");
        index = PlayerPrefs.GetInt("halamanTerakhir", 0);

        Debug.Log("Cerita: " + ceritaDipilih + " | Halaman: " + index);

        // Tentukan komik dan titik pindah scene berdasarkan cerita
        switch (ceritaDipilih)
        {
            case "10Nov":
                komikSekarang = komikCerita10Nov;
                triggerHalaman = 5;
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

        TampilkanHalaman(index);
    }

    public void TampilkanHalaman(int i)
    {
        if (komikSekarang != null && i < komikSekarang.Length && panelKomikRenderer != null)
        {
            index = i;
            panelKomikRenderer.sprite = komikSekarang[i];
        }
    }

    public void HalamanBerikutnya()
    {
        if (komikSekarang == null) return;

        // --- AWAL PERUBAHAN ---

        // Cek apakah halaman saat ini adalah halaman terakhir
        if (index >= komikSekarang.Length - 1)
        {
            // Cerita sudah selesai. Hapus PlayerPrefs yang terkait.
            Debug.Log("Cerita selesai. Menghapus PlayerPrefs.");
            PlayerPrefs.DeleteKey("ceritaDipilih");
            PlayerPrefs.DeleteKey("halamanTerakhir");
            PlayerPrefs.Save(); // Pastikan perubahan disimpan

            // Kembali ke menu utama atau scene lain setelah cerita selesai
            // Ganti "MainMenu" dengan nama Scene menu utama Anda jika berbeda.
            SceneManager.LoadScene("LevelSelection"); 
            return;
        }

        // --- AKHIR PERUBAHAN ---

        int nextIndex = index + 1;

        if (nextIndex == triggerHalaman)
        {
            // Simpan data sebelum pindah ke scene Fight
            PlayerPrefs.DeleteAll();

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