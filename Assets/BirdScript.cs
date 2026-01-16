using UnityEngine;

public class BirdScript : MonoBehaviour
{
    [Header("Skin Ayarlarý")]
    public Sprite[] skinler; // Farklý kuþ resimlerini tutacak bir liste (Dizi)
    private SpriteRenderer kusRessami; // Kuþun üzerindeki resmi deðiþtiren bileþen
    // ---------------------------
    public float ziplamaGucu = 8f;
    public float donusHizi = 5f; // Kuþun ne kadar hýzlý döneceði

    // SES DEÐÝÞKENLERÝ (Birazdan içini dolduracaðýz)
    public AudioClip ziplamaSesi;
    public AudioClip olmeSesi;
    private AudioSource audioSource;
    public ParticleSystem olumEfekti;

    public Rigidbody2D myRigidbody;
    public GameManager gameManager;

    private bool olduMu = false; // Ýki kere ses çalmasýn diye kontrol
    public void SkinGuncelle()
    {
        // Eðer ressam bileþeni henüz bulunamadýysa bul
        if (kusRessami == null) kusRessami = GetComponent<SpriteRenderer>();

        int secilenIndex = PlayerPrefs.GetInt("SecilenSkin", 0);

        if (secilenIndex < skinler.Length)
        {
            kusRessami.sprite = skinler[secilenIndex];
        }
    }
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        kusRessami = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        // Oyun baþlarken bir kere giyin
        SkinGuncelle();
        // --- YENÝ EKLENEN KISIM ---
        kusRessami = GetComponent<SpriteRenderer>(); // Ressamý bul

        // 1. Hafýzadan seçilen numarayý getir (Hiç yoksa 0 yani orjinali getir)
        int secilenIndex = PlayerPrefs.GetInt("SecilenSkin", 0);

        // 2. Eðer bu numara listemizde varsa, o resmi giydir
        if (secilenIndex < skinler.Length)
        {
            kusRessami.sprite = skinler[secilenIndex];
        }
        // ---------------------------

        myRigidbody = GetComponent<Rigidbody2D>();
        // Kuþun üzerindeki ses çalarý (hoparlörü) al
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Eðer öldüysek hareket etme ve dönme
        if (olduMu) return;

        // --- 1. ZIPLAMA ---
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            myRigidbody.velocity = Vector2.up * ziplamaGucu;

            // Sesi çal (Eðer ses dosyasý varsa)
            if (audioSource != null && ziplamaSesi != null)
                audioSource.PlayOneShot(ziplamaSesi);
        }

        // --- 2. DÖNME EFEKTÝ (ROTATION) ---
        // Kuþun yönünü hesapla (Hýzýna göre)
        // Yukarý çýkarken +30, aþaðý inerken -90'a kadar dönsün
        float aci = Mathf.Clamp(myRigidbody.velocity.y * 6f, -90, 30);

        // Bu açýyý yumuþak bir þekilde uygula (Lerp)
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, aci), donusHizi * Time.deltaTime);
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Daha önce ölmediysek...
        if (!olduMu)
        {
            olduMu = true; // Artýk öldük

            if (olumEfekti != null)
            {
                olumEfekti.Play(); // PATLAT!
            }
            // Ölme sesini çal
            if (audioSource != null && olmeSesi != null)
                audioSource.PlayOneShot(olmeSesi);

            if (gameManager != null)
            {
                gameManager.OyunuBitir();
            }
        }
    }
}