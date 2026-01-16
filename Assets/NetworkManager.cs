using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public InputField odaIsmiInput;
    public GameObject girisEkrani;
    public GameObject beklemeEkrani;
    public Button multiplayerButton;
    public Text butonYazisi;
    public Text durumYazisi; // Bekleme ekranında durum göstermek için (opsiyonel)

    void Start()
    {
        // Photon'un her detayı göstermesini sağla - debug için çok önemli
        PhotonNetwork.LogLevel = PunLogLevel.Full;

        // Butonu devre dışı bırak ve kullanıcıya durum göster
        if (multiplayerButton != null)
        {
            multiplayerButton.interactable = false;
        }

       

        // Eğer zaten bağlıysak (örneğin oyunu yeniden başlattıysak) direkt hazır ol
        if (PhotonNetwork.IsConnectedAndReady && PhotonNetwork.InLobby)
        {
            Debug.Log("♻️ Zaten lobideyiz, buton açılıyor!");
            ButonuAc();
        }
        else if (PhotonNetwork.IsConnected)
        {
            // Bağlıyız ama lobide değiliz, lobiye gir
            Debug.Log("🔄 Bağlıyız ama lobide değiliz, lobiye giriliyor...");
            PhotonNetwork.JoinLobby();
        }
        else
        {
            // Hiç bağlı değiliz, sıfırdan bağlan
            Debug.Log("🚀 Sunucuya bağlanılıyor...");
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        // Sunucuya bağlandık, şimdi lobiye girmeliyiz
        Debug.Log("✅ Master sunucuya bağlandı! Lobiye giriliyor...");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        // Lobiye girdik, artık oda oluşturabiliriz veya katılabiliriz
        Debug.Log("✅ Lobiye başarıyla girildi! Buton aktifleştiriliyor...");
        ButonuAc();
    }

    void ButonuAc()
    {
        // Butonu aktif et ve yazısını değiştir
        if (multiplayerButton != null)
        {
            multiplayerButton.interactable = true;
        }

        if (butonYazisi != null)
        {
            butonYazisi.text = "ARKADAŞLARLA OYNA";
        }

        Debug.Log("✅ Buton aktif, oyuncu artık odaya girebilir!");
    }

    public void OdayaGir()
    {
        // GÜVENLİK KONTROLÜ 1: Bağlantı durumunu kontrol et
        Debug.Log($"🔍 Odaya girme isteği - Bağlantı Durumu: IsConnected={PhotonNetwork.IsConnected}, InLobby={PhotonNetwork.InLobby}, State={PhotonNetwork.NetworkClientState}");

        // Eğer hazır değilsek işlemi yapma
        if (!PhotonNetwork.IsConnectedAndReady || !PhotonNetwork.InLobby)
        {
            Debug.LogWarning("⚠️ Henüz hazır değiliz! Lütfen bağlantı tamamlanana kadar bekleyin.");

            // Kullanıcıya görsel geri bildirim ver
            if (butonYazisi != null)
            {
                butonYazisi.text = "BEKLEYİN...";
            }

            return; // Fonksiyondan çık, devam etme
        }

        // GÜVENLİK KONTROLÜ 2: Oda ismini al ve kontrol et
        string odaAdi = odaIsmiInput.text.Trim(); // Trim ile başındaki ve sonundaki boşlukları temizle

        if (string.IsNullOrEmpty(odaAdi))
        {
            // Eğer kullanıcı oda ismi girmediyse rastgele bir isim oluştur
            odaAdi = "Oda_" + Random.Range(1000, 9999);
            Debug.Log($"📝 Kullanıcı oda ismi girmedi, rastgele oluşturuldu: {odaAdi}");
        }
        else
        {
            Debug.Log($"📝 Kullanıcı oda ismi girdi: {odaAdi}");
        }

        // Oda ayarlarını hazırla
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4; // Maksimum 4 oyuncu
        options.IsVisible = true; // Odayı listede göster
        options.IsOpen = true; // Yeni oyuncular katılabilsin

        Debug.Log($"🚪 '{odaAdi}' odasına girme isteği gönderiliyor...");

        // Odaya katılma veya oluşturma isteğini gönder
        bool basarili = PhotonNetwork.JoinOrCreateRoom(odaAdi, options, TypedLobby.Default);

        if (basarili)
        {
            Debug.Log("✅ İstek başarıyla gönderildi, sunucu yanıtı bekleniyor...");

            // Kullanıcıya görsel geri bildirim ver - ama henüz ekranları değiştirme!
            if (butonYazisi != null)
            {
                butonYazisi.text = "BAĞLANIYOR...";
            }

            // Eğer bekleme ekranında durum yazısı varsa onu güncelle
            if (durumYazisi != null)
            {
                durumYazisi.text = "Odaya bağlanılıyor...";
            }
        }
        else
        {
            Debug.LogError("❌ İstek gönderilemedi! Muhtemelen bağlantı koptu.");
        }
    }

    public override void OnJoinedRoom()
    {
        // BAŞARILI! Artık odadayız
        Debug.Log($"🎉 ODAYA BAŞARIYLA GİRİLDİ!");
        Debug.Log($"   Oda Adı: {PhotonNetwork.CurrentRoom.Name}");
        Debug.Log($"   Oyuncu Sayısı: {PhotonNetwork.CurrentRoom.PlayerCount}/{PhotonNetwork.CurrentRoom.MaxPlayers}");
        Debug.Log($"   Ben kimim: {PhotonNetwork.LocalPlayer.NickName} (ID: {PhotonNetwork.LocalPlayer.ActorNumber})");

        // Şimdi güvenli bir şekilde ekranları değiştirebiliriz
        if (girisEkrani != null)
        {
            girisEkrani.SetActive(false);
            Debug.Log("✅ Giriş ekranı kapatıldı");
        }

        if (beklemeEkrani != null)
        {
            beklemeEkrani.SetActive(true);
            Debug.Log("✅ Bekleme ekranı açıldı");
        }

        // Durum yazısını güncelle
        if (durumYazisi != null)
        {
            durumYazisi.text = $"Odada {PhotonNetwork.CurrentRoom.PlayerCount} oyuncu var\nOyun başlamayı bekliyor...";
        }

        // Eski offline kuşu temizle
        GameObject offlineKus = GameObject.Find("OfflineKus");
        if (offlineKus == null) offlineKus = GameObject.Find("bluebird-midflap");

        if (offlineKus != null)
        {
            Destroy(offlineKus);
            Debug.Log("🗑️ Offline kuş silindi");
        }

        // Yeni multiplayer kuşu oluştur
        try
        {
            Debug.Log("🐣 Multiplayer kuş yaratılmaya çalışılıyor...");

            // ÇOK ÖNEMLİ: Bu prefab Assets/Resources/ klasöründe olmalı!
            // Tam yolu: Assets/Resources/bluebird-midflap.prefab
            GameObject yeniKus = PhotonNetwork.Instantiate("bluebird-midflap", Vector3.zero, Quaternion.identity);

            if (yeniKus != null)
            {
                PhotonView photonView = yeniKus.GetComponent<PhotonView>();
                Debug.Log($"✅ KUŞ BAŞARIYLA OLUŞTURULDU!");
                Debug.Log($"   PhotonView ID: {photonView?.ViewID}");
                Debug.Log($"   Benim kuşum mu: {photonView?.IsMine}");
            }
            else
            {
                Debug.LogError("❌ Kuş null döndü!");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("🚨 KRİTİK HATA: Kuş yaratılamadı!");
            Debug.LogError($"   Hata Mesajı: {e.Message}");
            Debug.LogError($"   Stack Trace: {e.StackTrace}");

            // Kullanıcıya hata mesajı göster
            if (durumYazisi != null)
            {
                durumYazisi.text = "HATA: Kuş oluşturulamadı!\nLütfen Resources klasörünü kontrol edin";
            }
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        // Odaya katılma başarısız oldu
        Debug.LogError($"❌ ODAYA KATILMA BAŞARISIZ!");
        Debug.LogError($"   Hata Kodu: {returnCode}");
        Debug.LogError($"   Hata Mesajı: {message}");

        // Ekranları eski haline getir
        if (beklemeEkrani != null) beklemeEkrani.SetActive(false);
        if (girisEkrani != null) girisEkrani.SetActive(true);

        // Butonu tekrar aktif et
        if (multiplayerButton != null) multiplayerButton.interactable = true;
        if (butonYazisi != null) butonYazisi.text = "ARKADAŞLARLA OYNA";

        // Kullanıcıya ne olduğunu göster
        if (durumYazisi != null)
        {
            durumYazisi.text = "Bağlantı hatası: " + message;
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        // Oda oluşturma başarısız oldu
        Debug.LogError($"❌ ODA OLUŞTURMA BAŞARISIZ!");
        Debug.LogError($"   Hata Kodu: {returnCode}");
        Debug.LogError($"   Hata Mesajı: {message}");

        // Aynı işlemleri yap
        if (beklemeEkrani != null) beklemeEkrani.SetActive(false);
        if (girisEkrani != null) girisEkrani.SetActive(true);

        if (multiplayerButton != null) multiplayerButton.interactable = true;
        if (butonYazisi != null) butonYazisi.text = "ARKADAŞLARLA OYNA";
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        // Bağlantı kesildi
        Debug.LogError($"❌ BAĞLANTI KESİLDİ!");
        Debug.LogError($"   Sebep: {cause}");

        // Her şeyi sıfırla
        if (beklemeEkrani != null) beklemeEkrani.SetActive(false);
        if (girisEkrani != null) girisEkrani.SetActive(true);

        // Butonu kapat
        if (multiplayerButton != null) multiplayerButton.interactable = false;
        if (butonYazisi != null) butonYazisi.text = "BAĞLANTI KESİLDİ";

        // Yeniden bağlanmayı dene
        Debug.Log("🔄 5 saniye içinde yeniden bağlanılacak...");
        Invoke("YenidenBaglan", 5f);
    }

    void YenidenBaglan()
    {
        Debug.Log("🔄 Yeniden bağlanılıyor...");
        if (butonYazisi != null) butonYazisi.text = "BAĞLANIYOR...";
        PhotonNetwork.ConnectUsingSettings();
    }
}