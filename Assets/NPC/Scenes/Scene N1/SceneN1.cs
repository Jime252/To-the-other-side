using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneN1 : MonoBehaviour
{
    public Vector3 destination;
    [SerializeField] float speed;

    [SerializeField] int person;

    [HideInInspector] public Animator anim;

    Vector3 playerPosition;
    Vector3 myPosition;
    [HideInInspector] public string animation;
    [HideInInspector] public bool follow;

    bool sceneN4, end, backstay, twon;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        follow = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        if (transform.position == destination)
        {
            anim.SetBool("Idle", true);
        }
        else
        {
            anim.SetBool("Idle", false);
        }

        if (follow)
        {
            playerPosition = Player.player.transform.position + myPosition;
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, 10 * Time.deltaTime);
            anim.SetBool(animation, true);
        }

        if (sceneN4)
        {
            if (person == 1 || person == 3 || person == 4 || person == 5)
            {
                if (transform.position == destination)
                {
                    BackStay();
                }
            }            
        }

        if (backstay)
        {
            EndSnN3();
        }
        else if (end)
        {
            ByeText();
        }
        else if (twon)
        {
            TwonText();
        }

    } // End Update

    public void ControlTheWay(int buttonIndex)
    {
        if (buttonIndex == 0)
        {
            if (person == 1)
            {
                animation = "WalkUp";
                myPosition = new Vector3(1, -1, 0);
                follow = true;
            }
            else if (person == 4 || person == 5)
            {
                destination = new Vector3(125, -95.5f, 0);
                speed = 5;
            }
        }
        else if (buttonIndex == 1)
        {
            if (person == 1 || person == 5)
            {
                destination = new Vector3(114, 90.45f, 0);
                speed = 5;
            }
            else if (person == 4)
            {
                animation = "WalkDown";
                myPosition = new Vector3(-1, 1, 0);
                follow = true;
            }
        }
        else if (buttonIndex == 2)
        {
            if (person == 1 || person == 5)
            {
                destination = new Vector3(114, 90.45f, 0);
                speed = 7;
            }
            else if (person == 3 || person == 4)
            {
                destination = new Vector3(125, -95.5f, 0);
                speed = 7;
            }

            sceneN4 = true;

        } // End buttonIndex == 2       

    } // End ControlTheWay()

    public void Back(int scene)
    {
        if (scene == 1)
        {
            animation = "WalkDown";
            myPosition = new Vector3(-1, 1, 0);
            follow = true;
        }
        else
        {
            animation = "WalkUp";
            myPosition = new Vector3(1, -1, 0);
            follow = true;
        }
    }

    void BackStay()
    {
        sceneN4 = false;
        GameManager.gameManager.activateHistory(13);

        backstay = true;

    } // End BackStay()

    void EndSnN3()
    {
        if (GameManager.gameManager.posPage >= 15 && GameManager.gameManager.posPage < 20)
        {
            if (person == 1)
            {
                animation = "IdleDown";
                anim.SetBool(animation, true);
                destination = new Vector3(115.5f, 2.5f, 0);
                speed = 7;
            }
            else if (person == 5)
            {
                animation = "IdleDown";
                anim.SetBool(animation, true);
                destination = new Vector3(114.5f, 2.5f, 0);
                speed = 7;
            }
            else if (person == 4)
            {
                animation = "IdleDown";
                anim.SetBool(animation, true);
                destination = new Vector3(114.5f, 0.5f, 0);
                speed = 7;
            }
            else if (person == 3)
            {
                animation = "IdleUp";
                anim.SetBool(animation, true);
                destination = new Vector3(115.5f, 0.5f, 0);
                speed = 6.8f;
                end = true;
            }

        } // End posPage >= 17
        else if (GameManager.gameManager.posPage >= 20)
        {
            backstay = false;
        }              

    } // End EndSnN3()

    void ByeText()
    {
        if (person == 3 && transform.position == destination)
        {
            backstay = false;
            GameManager.gameManager.activateHistory(14);
            twon = true;
        }
    }

    void TwonText()
    {
        if (GameManager.gameManager.posPage == 7)
        {
            anim.SetBool(animation, false);
            destination = new Vector3(137.5f, 0.5f, 0);
            end = false;
        }
    }
}
