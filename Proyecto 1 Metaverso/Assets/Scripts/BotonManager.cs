using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonManager : MonoBehaviour
{
    
    public GameObject panel;
    public void NuevoJuego(){
        SceneManager.LoadScene(1);
    }
    public void MostrarTabla(){
        panel.SetActive(true);
    }
    public void QuitarTabla(){
         panel.SetActive(false);
    }
}
