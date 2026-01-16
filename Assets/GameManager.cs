using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    // STATIC: Bu deðiþkene oyunun her yerinden "GameManager.oyunHizi" diyerek ulaþabiliriz.
    public static float oyunHizi = 5f;

    public GameObject gameOverCanvas;
    public GameObject girisEkrani;

    public Text skorText;
    public Text bestScoreText;

    private int skor = 0;

    void Start()
    {
        // ÖNEMLÝ: Oyun her baþladýðýnda hýzý normale (5) döndür.
        // Yoksa önceki oyundan hýzlý kalýr!
        oyunHizi = 5f;

        Time.timeScale = 0;

        if (girisEkrani != null) girisEkrani.SetActive(true);
        if (gameOverCanvas != null) gameOverCanvas.SetActive(false);

        skor = 0;
        if (skorText != null) skorText.text = skor.ToString();
    }

    public void OyunuBaslat()
    {
        if (girisEkrani != null) girisEkrani.SetActive(false);
        Time.timeScale = 1;
    }

    public void SkorArtir()
    {
        skor++;
        if (skorText != null) skorText.text = skor.ToString();

        // --- ZORLUK AYARI ---
        // Her 3 puanda bir hýzý 0.5 artýr
        if (skor % 3 == 0)
        {
            oyunHizi += 0.5f;
            // Konsola yazalým ki hýzlandýðýný görelim (Ýstersen sonra sil)
            Debug.Log("Hýz Artýrýldý! Yeni Hýz: " + oyunHizi);
        }
    }

    public void OyunuBitir()
    {
        if (gameOverCanvas != null) gameOverCanvas.SetActive(true);
        Time.timeScale = 0;

        int eskiRekor = PlayerPrefs.GetInt("EnYuksekSkor", 0);
        if (skor > eskiRekor)
        {
            eskiRekor = skor;
            PlayerPrefs.SetInt("EnYuksekSkor", eskiRekor);
        }

        if (bestScoreText != null)
            bestScoreText.text = "EN YÜKSEK: " + eskiRekor.ToString();
    }

    public void TekrarBaslat()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}