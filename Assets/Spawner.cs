using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject boruKalýbý;
    public float sure = 2f; // Ýki boru arasý bekleme süresi
    public float yukseklikAraligi = 1.5f;

    // HÝLE BURADA: Zamanlayýcýyý 0'dan deðil, süreden baþlatýyoruz.
    // Böylece kod "Aaa süre dolmuþ" sanýp anýnda ilk boruyu atacak.
    private float zamanlayici = 100f;

    void Update()
    {
        // Eðer oyun durmuþsa (Game Over veya Menü) üretim yapma
        if (GameManager.oyunHizi == 0 || Time.timeScale == 0) return;

        if (zamanlayici > sure)
        {
            BoruUret();
            zamanlayici = 0; // Sayacý sýfýrla, tekrar saymaya baþla
        }
        else
        {
            zamanlayici += Time.deltaTime;
        }
    }

    void BoruUret()
    {
        float rastgeleY = Random.Range(-yukseklikAraligi, yukseklikAraligi);
        Vector3 yeniPozisyon = transform.position + new Vector3(0, rastgeleY, 0);

        GameObject yeniBoru = Instantiate(boruKalýbý, yeniPozisyon, Quaternion.identity);
        Destroy(yeniBoru, 10f);
    }
}