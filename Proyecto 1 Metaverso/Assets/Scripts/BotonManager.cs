using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonManager : MonoBehaviour
{
    public void NuevoJuego(){
        SceneManager.LoadScene(1);
    }
}
