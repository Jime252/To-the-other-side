using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneN1Controller : MonoBehaviour
{
    [SerializeField] GameObject[] characterSN2;
    SceneN1[] sceneScrp = new SceneN1[6];
    Animator[] animScrp = new Animator[6];

    Animator anim;

    bool finish, twon, sceneCtrl, end;

    [SerializeField] bool sceneN1, sceneN2, sceneN3, sceneN4, sceneN5, sceneN6, sceneN7;
    [SerializeField] int samRick;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        if (sceneN2 || sceneN3 || sceneN4 || sceneN5 || sceneN6)
        {
            for (int i = 0; i < characterSN2.Length; i++)
            {
                animScrp[i] = characterSN2[i].GetComponent<Animator>();
                sceneScrp[i] = characterSN2[i].GetComponent<SceneN1>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneN1)
        {
            if (GameManager.gameManager.posPage == 9 && !finish)
            {
                anim.SetTrigger("Earthquake");
            }
        }
        else if (sceneN2)
        {          
            if (GameManager.gameManager.posPage >= 10 && !finish && !sceneCtrl)
            {
                GameManager.gameManager.bottonIndex = -1;
                for (int i = 0; i < characterSN2.Length; i++)
                {                  
                    animScrp[i].enabled = true;
                    sceneScrp[i].enabled = true;
                }                
                finish = true;
                sceneCtrl = true;
            }

            if (finish)
            {           
                if (sceneCtrl)
                {
                    GameManager.gameManager.posPage = 0;
                    sceneCtrl = false;
                }               
                if (GameManager.gameManager.posPage == 11) // Road to town
                {
                    for (int i = 0; i < characterSN2.Length; i++)
                    {
                        sceneScrp[i].destination = new Vector3(200.5f, Random.Range(1.5f, 3.5f), 0);
                    }                  
                }
                else if (GameManager.gameManager.posPage >= 17)
                {
                    finish = false;
                    for (int i = 0; i < characterSN2.Length; i++)
                    {
                        sceneScrp[i].ControlTheWay(GameManager.gameManager.bottonIndex);
                    }
                }
            }

        } // End sceneN2
        else if (sceneN3)
        {
            if (GameManager.gameManager.posPage == 11)
            {
                for (int i = 0; i < characterSN2.Length; i++)
                {
                    animScrp[i].enabled = true;
                    sceneScrp[i].enabled = true;
                    sceneScrp[i].anim.SetBool("WalkDown", true);
                }
            }
        }
        else if (sceneN4)
        {
            if (samRick == 1)
            {
                animScrp[4].enabled = true;
                sceneScrp[4].enabled = true;
                sceneScrp[4].Back(samRick);
            }
            else if (samRick == 2)
            {
                animScrp[0].enabled = true;
                sceneScrp[0].enabled = true;
                sceneScrp[0].Back(samRick);
            }

            if (GameManager.gameManager.posPage >= 9)
            {
                samRick += 2;
                for (int i = 0; i < characterSN2.Length; i++)
                {
                    sceneScrp[i].follow = false;
                    animScrp[i].enabled = true;
                    animScrp[i].SetBool(sceneScrp[i].animation, false);
                    sceneScrp[i].enabled = true;
                    sceneScrp[i].destination = new Vector3(143.5f, Random.Range(1.5f, 3.5f), 0);
                }

                if (!twon)
                {
                    StartCoroutine(Twon());
                }               
            }

        } // End Scene N4
        else if (sceneN5)
        {
            Player.player.spriteColor.a = 0.7f;
            Player.player.GetComponent<SpriteRenderer>().material.color = Player.player.spriteColor;

            if (GameManager.gameManager.posPage == 13)
            {
                for (int i = 0; i < characterSN2.Length; i++)
                {
                    animScrp[i].enabled = true;
                    sceneScrp[i].enabled = true;
                    sceneScrp[i].anim.SetBool("WalkDown", true);
                }
            }
        }
        else if (sceneN6)
        {
            if (GameManager.gameManager.posPage == 15)
            {
                for (int i = 0; i < characterSN2.Length; i++)
                {
                    animScrp[i].enabled = true;
                    sceneScrp[i].enabled = true;
                    sceneScrp[i].anim.SetBool("WalkDown", true);
                }
            }
        }
        else if (sceneN7)
        {
            if (GameManager.gameManager.posPage >= 17)
            {
                if (!end)
                {
                    End();
                }
            }
        }

    } // End Update

    // It is called in the animation.
    void NewScene()
    {
        if (sceneN1)
        {
            finish = true;
            SceneManager.LoadScene(3);
        }      
    }

    IEnumerator Twon()
    {
        twon = true;
        yield return new WaitForSeconds(3.5f);
        GameManager.gameManager.sceneCurrent = SceneManager.GetActiveScene().buildIndex;
        GameManager.gameManager.firstPosPl = new Vector3(-1.5f, -43.5f, 0);
        SceneManager.LoadScene(8);
        twon = false;
    }

    void End()
    {
        end = true;
        GameManager.gameManager.sceneCurrent = SceneManager.GetActiveScene().buildIndex;
        GameManager.gameManager.firstPosPl = new Vector3(1.5f, 8.5f, 0);

        SceneManager.LoadScene(22);
    }
}
