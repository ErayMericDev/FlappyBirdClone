using UnityEngine;

public class PuanNoktasi : MonoBehaviour
{
    private bool puanVerildi = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<BirdScript>() != null && !puanVerildi)
        {
            puanVerildi = true;

            // KONSOLA YAZDIR: Bu borunun adý ne?
            Debug.Log("Puan Alýndý! Boru ID: " + gameObject.GetInstanceID());

            FindObjectOfType<GameManager>().SkorArtir();
        }
    }
}