using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    // Text
    public int currentChapter;
    [SerializeField] bool history;

    // Turn
    [SerializeField] Sprite[] spritesTurn;
    SpriteRenderer sR;

    // Start is called before the first frame update
    void Start()
    {
        sR = GetComponent<SpriteRenderer>();
    }

    public void Talking()
    {
        Turn();
        if (history)
        {
            GameManager.gameManager.activateHistory(currentChapter);
        }    
    }

    void Turn()
    {
        if (transform.position.x < Player.player.gameObject.transform.position.x)
        {
            sR.sprite = spritesTurn[3]; // Right  
        }
        else if (transform.position.x > Player.player.gameObject.transform.position.x)
        {
            sR.sprite = spritesTurn[2]; // Left
        }
        else if (transform.position.y > Player.player.gameObject.transform.position.y)
        {
            sR.sprite = spritesTurn[0]; // Down
        }
        else
        {
            sR.sprite = spritesTurn[1]; // Up 
        }

    } // End Girar
}
