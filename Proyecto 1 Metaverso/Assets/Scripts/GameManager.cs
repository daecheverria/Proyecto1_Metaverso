using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject brickPrefab;
    public int rows = 5;
    public int columns = 10;
    public Vector2 startPosition;
    public float brickSpacingX = 1.5f;
    public float brickSpacingY = 0.8f;
    public int level = 0;
    public int health = 3;
    public int ptsTotal = 0;
    private List<GameObject> currentBricks = new List<GameObject>();

    public Gradient gradient;
    public TablaClasificacion tabla;

    public TextMeshProUGUI nivel;
    public TextMeshProUGUI pts;
    public TextMeshProUGUI vidas;
    public string nombreJugador;
    public Ball pelota;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)

    {
        if(scene.buildIndex == 0 && ptsTotal!=0){
        tabla = gameObject.GetComponent<TablaClasificacion>();
        tabla.GuardarSiEsSuperior();
        }
        if (scene.buildIndex == 1)
        {
            GameObject ptsObject = GameObject.FindGameObjectWithTag("Puntuacion");
            pts = ptsObject.GetComponent<TextMeshProUGUI>();
            GameObject vidasObject = GameObject.FindGameObjectWithTag("Vidas");
            vidas = vidasObject.GetComponent<TextMeshProUGUI>();
            GameObject nivelObject = GameObject.FindGameObjectWithTag("Nivel");
            nivel = nivelObject.GetComponent<TextMeshProUGUI>();
            GameObject pelotaObject = GameObject.FindGameObjectWithTag("Ball");
            pelota = pelotaObject.GetComponent<Ball>();
            ptsTotal = 0;
            health = 3;
            level = 0;
            GenerarNivel();

        }
    }
    void GenerarNivel()
    {
        level++;
        pelota.ResetBall();
        nivel.text = $"Nivel: {level}";
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector2 brickPosition = new Vector2(
                    startPosition.x + col * brickSpacingX,
                    startPosition.y - row * brickSpacingY
                );
                GameObject brick = Instantiate(brickPrefab, brickPosition, Quaternion.identity);
                float gradientValue = (float)row / (rows - 1);
                Color brickColor = gradient.Evaluate(gradientValue);
                int vidaBloque = AsignarVida();
                brick.GetComponent<Brick>().SetVida(vidaBloque);
                brick.GetComponent<SpriteRenderer>().color = brickColor;

                currentBricks.Add(brick);
            }
        }
    }

    int AsignarVida()
    {
        if (level >= 1 && level <= 5)
        {
            return Random.Range(1, 101) <= 80 ? 1 : (Random.Range(1, 101) <= 50 ? 2 : 3);
        }
        else if (level >= 6 && level <= 10)
        {
            return Random.Range(1, 101) <= 50 ? 1 : (Random.Range(1, 101) <= 70 ? 2 : 3);
        }
        else
        {
            return Random.Range(1, 101) <= 30 ? 1 : (Random.Range(1, 101) <= 50 ? 2 : 3);
        }
    }

    public void BlockDestroyed(GameObject brick)
    {
        currentBricks.Remove(brick);
        ptsTotal += 10;
        pts.text = $"Puntos: {ptsTotal}";
        if (currentBricks.Count == 0)
        {
            GenerarNivel();
        }
    }
    public void BajarVidas()
    {
        health--;
        vidas.text = $"Vidas: {health}";
        if (health == 0)
        {
            Debug.Log("Moriste");
            GameOver();
        }
    }
    public void GameOver(){
        SceneManager.LoadScene(0);
    }
}