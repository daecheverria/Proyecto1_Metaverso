using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonManager : MonoBehaviour
{
    
    public GameObject panel;
    public GameManager tabla;
    private TablaClasificacion tabla2;
    public void NuevoJuego(){
        SceneManager.LoadScene(1);
    }
    public void MostrarTabla(){
        panel.SetActive(true);
        tabla = GameManager.instance;
        tabla2  = tabla.GetComponent<TablaClasificacion>();
        tabla2.MostrarPuntuaciones();
    }
    public void QuitarTabla(){
         panel.SetActive(false);
    }
}
