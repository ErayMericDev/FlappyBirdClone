using UnityEngine;

public class SkinSecici : MonoBehaviour
{
    public void SkinSec(int skinNumarasi)
    {
        // 1. Seçimi kaydet
        PlayerPrefs.SetInt("SecilenSkin", skinNumarasi);
        PlayerPrefs.Save();

        // 2. Sahnedeki kuþu bul ve "Güncellen" de!
        BirdScript kus = FindObjectOfType<BirdScript>();
        if (kus != null)
        {
            kus.SkinGuncelle();
        }

        Debug.Log("Skin Seçildi ve Güncellendi: " + skinNumarasi);
    }
}