using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Auxiliar : MonoBehaviour
{
    // Singleton pattern
    [HideInInspector] public static Auxiliar aux;

    [HideInInspector] public bool english = false;

    private void Awake()
    {
        if (aux == null)
        {
            aux = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    } // Singleton pattern

    public void AuxiliarVoid()
    {
        GameManager.gameManager.english = english;
    }
}
