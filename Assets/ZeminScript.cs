using UnityEngine;

public class ZeminScript : MonoBehaviour
{
    // public float hiz = 5f;  <-- BU ESKÝ KODDU, ARTIK GEREK YOK

    private float baslangicX;
    private float uzunluk;

    void Start()
    {
        baslangicX = transform.position.x;
        uzunluk = GetComponent<BoxCollider2D>().size.x;
    }

    void Update()
    {
        // ARTIK HIZI "GameManager"DAN ALIYORUZ
        float guncelHiz = GameManager.oyunHizi;

        transform.Translate(Vector2.left * guncelHiz * Time.deltaTime);

        if (transform.position.x < baslangicX - (uzunluk / 2))
        {
            transform.position = new Vector3(baslangicX, transform.position.y, transform.position.z);
        }
    }
}