using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject[] brickTypes;
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

    public TextMeshProUGUI nivel;
    public TextMeshProUGUI pts;
    public TextMeshProUGUI vidas;
    public string nombreJugador;
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
        if (scene.buildIndex == 1)
        {
            GameObject ptsObject = GameObject.FindGameObjectWithTag("Puntuacion");
            pts = ptsObject.GetComponent<TextMeshProUGUI>();
            GameObject vidasObject = GameObject.FindGameObjectWithTag("Vidas");
            vidas = vidasObject.GetComponent<TextMeshProUGUI>();
            GenerarNivel();

        }
    }
    void GenerarNivel()
    {
        level++;
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector2 brickPosition = new Vector2(
                    startPosition.x + col * brickSpacingX,
                    startPosition.y - row * brickSpacingY
                );

                GameObject brickPrefab = ElegirBloque();
                GameObject brick = Instantiate(brickPrefab, brickPosition, Quaternion.identity);
                float gradientValue = (float)row / (rows - 1); 
                Color brickColor = gradient.Evaluate(gradientValue);
                brick.GetComponent<SpriteRenderer>().color = brickColor;

                currentBricks.Add(brick);
            }
        }
    }

    GameObject ElegirBloque()
    {

        if (level == 1)
        {
            return brickTypes[0];
        }
        int randomIndex = Random.Range(0, Mathf.Min(level, brickTypes.Length));
        return brickTypes[randomIndex];
    }
    public void BlockDestroyed(GameObject brick)
    {
        currentBricks.Remove(brick);
        ptsTotal += 10;
        pts.text = $"Puntos: {ptsTotal}";
        if (currentBricks.Count == 0)
        {
            Debug.Log("hola");
        }
    }
    public void BajarVidas(){
        health--;
        vidas.text = $"Vidas: {health}";
        if(health==0){
            Debug.Log("Moriste");
            //terminar();
        }
    }
}