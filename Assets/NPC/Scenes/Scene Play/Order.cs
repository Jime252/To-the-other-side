using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    [SerializeField] GameObject[] entries;
    [SerializeField] GameObject[] advices;

    [SerializeField] bool advice;

    GameObject adviceIM;

    // Start is called before the first frame update
    void Start()
    {
        adviceIM = GameObject.FindGameObjectWithTag("advice");
    }

    // Update is called once per frame
    void Update()
    {
        if (!advice)
        {
            if (canvas.canvasManager.sphereInt >= -1 && canvas.canvasManager.sphereInt < entries.Length)
            {
                for (int i = 0; i < entries.Length; i++)
                {
                    if (i == canvas.canvasManager.sphereInt + 1)
                    {
                        entries[i].SetActive(true);
                        //advices[i].SetActive(false);
                    }
                    else
                    {
                        entries[i].SetActive(false);
                        //advices[i].SetActive(true);
                    }
                }
            }
        }       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (advice)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                //adviceIM.GetComponent<Image>.eneable = true;
            }
        }      
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (advice)
        {
            if (collision.gameObject.CompareTag("Player"))
            {

            }
        }      
    }
}
