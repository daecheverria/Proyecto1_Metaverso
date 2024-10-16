using UnityEngine;
public class Brick : MonoBehaviour
{
    public int vidas = 3;
    public GameObject[] capas;

    void Start()
    {
        ActualizarCapas();
        Color colorActual = GetComponent<SpriteRenderer>().color;
        capas[1].GetComponent<SpriteRenderer>().color = colorActual;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Romper();
        }
    }

    void Romper()
    {
        vidas--;
        ActualizarCapas();
        if (vidas <= 0)
        {
            GameManager.instance.BlockDestroyed(gameObject);
            Destroy(gameObject);
        }
    }
    void ActualizarCapas()
    {
        for (int i = 0; i < capas.Length; i++)
        {
            if (i < vidas-1)
            {
                capas[i].SetActive(true);
            }
            else
            {
                capas[i].SetActive(false);
            }
        }
    }
}
