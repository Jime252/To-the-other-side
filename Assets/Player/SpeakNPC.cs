using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpeakNPC : MonoBehaviour
{
    Collider2D coll;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Dialogue();
    }

    void Dialogue()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            coll = Player.player.coll;
            if (coll != null && coll.CompareTag("npc"))
            {
                NPC npScrpt = coll.GetComponent<NPC>();
                npScrpt.Talking();
            }
            else if (coll != null && coll.CompareTag("item"))
            {
                if (canvas.canvasManager.sphereInt < 4)
                {
                    canvas.canvasManager.sphereInt++;
                    canvas.canvasManager.Sphere(canvas.canvasManager.sphereInt);
                }
                
                Player.player.spriteColor.a = 0.7f;
                Player.player.GetComponent<SpriteRenderer>().material.color = Player.player.spriteColor;

                Sphere sphereGO = coll.GetComponent<Sphere>();
                if (sphereGO != null)
                {
                    GameManager.gameManager.firstPosPl = new Vector3(-0.5f, -0.5f, 0);
                    SceneManager.LoadScene(sphereGO.scene);
                }
            }
            else if (coll != null && coll.CompareTag("itemEnd"))
            {
                if (canvas.canvasManager.sphereInt < 4)
                {
                    canvas.canvasManager.sphereInt++;
                    canvas.canvasManager.Sphere(canvas.canvasManager.sphereInt);
                }

                Sphere sphereGO = coll.GetComponent<Sphere>();
                if (sphereGO != null)
                {
                    GameManager.gameManager.firstPosPl = new Vector3(-0.5f, -0.5f, 0);
                    SceneManager.LoadScene(sphereGO.scene);
                }
            }
            else if (coll != null && coll.CompareTag("treasure"))
            {
                GameManager.gameManager.posHelper = Player.player.transform.position;
                GameManager.gameManager.sceneHelper = GameManager.gameManager.sceneCurrent;
                GameManager.gameManager.sceneCurrent = SceneManager.GetActiveScene().buildIndex;

                SceneManager.LoadScene(14);
            }
        }
    }
}
