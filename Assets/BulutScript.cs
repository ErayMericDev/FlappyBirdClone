using UnityEngine;

public class BulutScript : MonoBehaviour
{
    // Bulutlar zeminden daha yavaþ gitmeli (Derinlik hissi için).
    // Bu deðeri 0.5 yaparsan zemin hýzýnýn yarýsý kadar giderler.
    public float hizCarpani = 0.5f;

    private float baslangicX;
    private float bitisX;

    void Start()
    {
        // Bulut ekranýn saðýndan (X: 10) baþlayýp solundan (X: -10) çýkacak gibi düþünelim.
        // Bu deðerleri sahnenin geniþliðine göre ayarlayabilirsin.
        baslangicX = 12f; // Ekranýn saðý (Gireceði yer)
        bitisX = -12f;    // Ekranýn solu (Çýkacaðý yer)
    }

    void Update()
    {
        // GameManager'daki hýzý alýp çarpanla (0.5) yavaþlatýyoruz.
        // Ama oyun hýzlandýkça bu da orantýlý olarak hýzlanacak!
        float guncelHiz = GameManager.oyunHizi * hizCarpani;

        transform.Translate(Vector2.left * guncelHiz * Time.deltaTime);

        // Eðer bulut ekranýn solundan çýktýysa...
        if (transform.position.x < bitisX)
        {
            BaþaSar();
        }
    }

    void BaþaSar()
    {
        // 1. En saða ýþýnla
        float yeniY = Random.Range(1f, 4f); // Yükseklik rastgele olsun (1 ile 4 arasý)

        transform.position = new Vector3(baslangicX, yeniY, transform.position.z);
    }
}