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

    private string rutaJSON = "Assets/Scripts/puntuaciones.json"; 
    private TablaPuntuaciones tablaPuntuaciones;

    void Start()
    {
        
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
            Debug.LogError("Archivo JSON no encontrado: " + rutaJSON);
        }
    }

    void MostrarPuntuaciones()
    {
        tablaPuntuaciones.puntuaciones.Sort((a, b) => b.puntos.CompareTo(a.puntos));
        string textoTabla = "Top 10 Puntuaciones:\n";
        for (int i = 0; i < Mathf.Min(10, tablaPuntuaciones.puntuaciones.Count); i++)
        {
            textoTabla += (i + 1) + ". " + tablaPuntuaciones.puntuaciones[i].nombre + " - " + tablaPuntuaciones.puntuaciones[i].puntos + "\n";
        }
        tablaTexto.text = textoTabla;
    }
    void OnEnable(){ 
        LeerPuntuaciones();
        MostrarPuntuaciones();
    }
}
