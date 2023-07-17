using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvas : MonoBehaviour
{
    public static canvas canvasManager; // Null

    [SerializeField] GameObject inventory;
    [SerializeField] GameObject[] sphere;
    bool active;
    [HideInInspector] public int sphereInt = -1;


    private void Awake()
    {
        // Evitar que es duplique pero si permanezca
        if (canvasManager == null)
        {
            canvasManager = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Button
    public void Invetory()
    {
        active = !active;
        if (active)
        {
            inventory.SetActive(true);
        }
        else
        {
            inventory.SetActive(false);
        }      
    }

    public void Sphere(int nSp)
    {
        sphere[nSp].SetActive(true);
    }
}
