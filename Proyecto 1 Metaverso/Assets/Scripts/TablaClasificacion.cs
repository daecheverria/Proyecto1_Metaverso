using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using Unity.VisualScripting;
[System.Serializable]
public class Puntuacion
{
    public string nombre;
    public int puntos;
}

[System.Serializable]
public class TablaPuntuaciones
{
    public List<Puntuacion> puntuaciones;
}

public class TablaClasificacion : MonoBehaviour
{
    public TextMeshProUGUI tablaTexto;

    private string rutaJSON;
    private TablaPuntuaciones tablaPuntuaciones;

    void Start()
    {
        rutaJSON = Path.Combine(Application.persistentDataPath, "puntuaciones.json");
        
        LeerPuntuaciones();
    }


    void LeerPuntuaciones()
    {
        if (File.Exists(rutaJSON))
        {
            string json = File.ReadAllText(rutaJSON);
            tablaPuntuaciones = JsonUtility.FromJson<TablaPuntuaciones>(json);
        }
        else
        {
            tablaPuntuaciones = new TablaPuntuaciones();
            tablaPuntuaciones.puntuaciones = new List<Puntuacion>();
            GuardarPuntuaciones();
        }
    }

    public void MostrarPuntuaciones()
    {
        LeerPuntuaciones();
        GameObject ptsObject = GameObject.FindGameObjectWithTag("Panel");
        tablaTexto = ptsObject.GetComponent<TextMeshProUGUI>();
        tablaPuntuaciones.puntuaciones.Sort((a, b) => b.puntos.CompareTo(a.puntos));
        string textoTabla = "Top 10 Puntuaciones:\n";
        for (int i = 0; i < Mathf.Min(10, tablaPuntuaciones.puntuaciones.Count); i++)
        {
            textoTabla += (i + 1) + ". " + tablaPuntuaciones.puntuaciones[i].nombre + " - " + tablaPuntuaciones.puntuaciones[i].puntos + "\n";
        }
        tablaTexto.text = textoTabla;
    }


    public void GuardarSiEsSuperior()
    {
        if (tablaPuntuaciones.puntuaciones.Count < 10)
        {
            AgregarPuntuacion();
        }
        else
        {
            Puntuacion lowestScore = tablaPuntuaciones.puntuaciones[0];
            foreach (var puntos in tablaPuntuaciones.puntuaciones)
            {
                if (puntos.puntos < lowestScore.puntos)
                {
                    lowestScore = puntos;
                }
            }
            if (GameManager.instance.ptsTotal > lowestScore.puntos)
            {
                // Reemplazar la puntuación más baja
                tablaPuntuaciones.puntuaciones.Remove(lowestScore);
                AgregarPuntuacion();
            }
        }
    }

    void AgregarPuntuacion()
    {
        Puntuacion newScore = new Puntuacion();
        newScore.nombre = GameManager.instance.nombreJugador;
        newScore.puntos = GameManager.instance.ptsTotal;
        tablaPuntuaciones.puntuaciones.Add(newScore);

        tablaPuntuaciones.puntuaciones.Sort((x, y) => y.puntos.CompareTo(x.puntos));

        if (tablaPuntuaciones.puntuaciones.Count > 10)
        {
            tablaPuntuaciones.puntuaciones.RemoveAt(tablaPuntuaciones.puntuaciones.Count - 1);
        }

        GuardarPuntuaciones();
    }
    void GuardarPuntuaciones()
    {
        string json = JsonUtility.ToJson(tablaPuntuaciones, true);
        File.WriteAllText(rutaJSON, json); 
    }

}
