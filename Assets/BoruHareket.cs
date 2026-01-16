using UnityEngine;

public class BoruHareket : MonoBehaviour
{
    // public float hiz = 5f; <-- ESKÝSÝ GÝTTÝ

    void Update()
    {
        // PATRON NE DERSE O HIZDA GÝT
        transform.Translate(Vector2.left * GameManager.oyunHizi * Time.deltaTime);
    }
}