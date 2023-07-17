using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : MonoBehaviour
{
    public static Pet petManager; // Null

    Vector3 destination;
    Animator anim;

    private void Awake()
    {
        // Evitar que es duplique pero si permanezca
        if (petManager == null)
        {
            petManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        destination = Player.player.transform.position + new Vector3(0.5f, 0.5f, 0);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        destination = Player.player.transform.position + new Vector3(0.5f, 0.5f, 0);
        transform.position = Vector3.MoveTowards(transform.position, destination, 4.5f * Time.deltaTime);
        if (transform.position == destination)
        {
            anim.SetBool("moving", false);
            anim.SetBool("movingRight", false);
        }
        else
        {
            if (Player.player.h >= 0)
            {
                anim.SetBool("moving", true);
            }
            else if (Player.player.h <= 0)
            {
                anim.SetBool("movingRight", true);
            }
        }
    }
}
