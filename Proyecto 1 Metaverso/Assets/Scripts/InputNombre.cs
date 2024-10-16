using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InputNombre : MonoBehaviour
{
    public TMP_InputField inputNombre; 
    public string nombreJugador; 
    public Button botonInicio;
    void Start(){
        botonInicio.interactable = false;
        inputNombre.characterLimit = 12;
        inputNombre.onValueChanged.AddListener(VerificarTexto);
        CambiarPlaceholder("Ingrese su nombre");
    }
    public void CapturarNombre()
    {
        nombreJugador = inputNombre.text;
        GameManager.instance.nombreJugador = nombreJugador;
    }
    private void VerificarTexto(string texto)
    {
        botonInicio.interactable = !string.IsNullOrEmpty(texto);
    }
    public void CambiarPlaceholder(string nuevoTexto)
    {
        TMP_Text placeholder = inputNombre.placeholder as TMP_Text;
        if (placeholder != null)
        {
            placeholder.text = nuevoTexto;
        }
    }
}
