using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cortinilla : MonoBehaviour
{    
    public void CambioEscena() // Lo llamamos en la animación del slider
    {
        SceneManager.LoadScene(3);
    }
}
