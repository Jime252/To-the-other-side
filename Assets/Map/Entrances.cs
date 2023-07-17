using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entrances : MonoBehaviour
{
    // Animation
    [SerializeField] Animator animCanvas;

    // Scene
    [SerializeField] int sceneIndex;

    // Palyer position
    [SerializeField] Vector3 newPosPlayer;

    // Flashback
    [SerializeField] bool falsh;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //animCanvas = GameObject.FindGameObjectWithTag("canvas").GetComponent<Animator>();
            //animCanvas.SetTrigger("fadeOut");
            //sceneIndex += sphere;

            StartCoroutine(EsperarYCargar(0.45f));

        }
    }

    IEnumerator EsperarYCargar(float esperar)
    {
        yield return new WaitForSeconds(esperar);
        GameManager.gameManager.sceneCurrent = SceneManager.GetActiveScene().buildIndex;
        GameManager.gameManager.firstPosPl = newPosPlayer;

        if (falsh)
        {
            canvas.canvasManager.sphereInt++;
            canvas.canvasManager.Sphere(canvas.canvasManager.sphereInt);
            Player.player.spriteColor.a = 0.5f;
            Player.player.GetComponent<SpriteRenderer>().material.color = Player.player.spriteColor;
        }
        else
        {
            Player.player.Transparency();
        }       
        SceneManager.LoadScene(sceneIndex);
    }
}
