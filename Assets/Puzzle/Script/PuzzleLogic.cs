using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PuzzleLogic : MonoBehaviour 
{

    public bool[,] puzzle;

    [SerializeField] int scene;
    [SerializeField] Vector3 posScene;

    int XmaxM = 3;
    int YmaxM = 3;

    int xAux, yAux;

    [SerializeField] Text winText;
    Image imgStart;

    [SerializeField] Sprite emptyHeart;
    [SerializeField] Sprite stuffedHeart;

    bool gameInProgress = true;

    // Use this for initialization
    void Start () 
    {
        StartPuzzle();

        Destroy(Player.player.gameObject);
    }

    public void StartPuzzle()
    {
        puzzle = new bool[XmaxM, YmaxM];

        for (int w = 0; w < puzzle.GetLength(0); w++)
        {
            for (int h = 0; h < puzzle.GetLength(1); h++)
            {
                int moneda = Random.Range(0, 2);
                if (moneda == 0) // 0 = True
                {
                    puzzle[w, h] = true;                   
                }
                else // 1 = False
                {
                    puzzle[w, h] = false;
                }

                GameObject auxGO = GameObject.Find("pos" + w + h);
                imgStart = auxGO.GetComponent<Image>();
                if (puzzle[w, h])
                {
                    imgStart.sprite = stuffedHeart;
                }
                else
                {
                    imgStart.sprite = emptyHeart;
                }

            } // End 2º For

        } // End 1º For
    }

    public bool compVictoria()
    {
        bool victoria = true;

        for (int w = 0; w < puzzle.GetLength(0); w++)
        {
            for (int h = 0; h < puzzle.GetLength(1); h++)
            {
                if (puzzle[w, h] == false)
                {
                    victoria = false;
                }
            }
        }

        return victoria;   
    }

    public void getX(int x)
    {
        xAux = x;
    }

    public void getY(int y)
    {
        yAux = y;
        if (gameInProgress == true)
        {
            ChangeSprite(xAux, yAux);
        }
    }

    void ChangeSprite(int i, int j) 
    {
        ChangeBox(i, j);

        if ((i + 1) < puzzle.GetLength(0))
        {
            ChangeBox(i + 1, j);
        }
        if ((j + 1) < puzzle.GetLength(1))
        {
            ChangeBox(i, j + 1);
        }
        if ((i - 1) >= 0)
        {
            ChangeBox(i - 1, j);
        }
        if ((j - 1) >= 0)
        {
            ChangeBox(i, j - 1);
        }

        if (compVictoria())
        {
            winText.text = "Redirigiendo...";

            GameManager.gameManager.sceneCurrent = SceneManager.GetActiveScene().buildIndex;
            GameManager.gameManager.firstPosPl = posScene;

            Player.player.spriteColor.a = 0.7f;
            if (canvas.canvasManager.sphereInt < 4)
            {
                canvas.canvasManager.sphereInt++;
                canvas.canvasManager.Sphere(canvas.canvasManager.sphereInt);
            }            
            SceneManager.LoadScene(scene);

            gameInProgress = false;
        }

    } // End ChangeSprite

    void ChangeBox(int x, int y)
    {
        puzzle[x, y] = !(puzzle[x, y]);

        GameObject aux = GameObject.Find("pos" + x + y);
        imgStart = aux.GetComponent<Image>();

        if (puzzle[x, y])
        {
            imgStart.sprite = stuffedHeart;
        }
        else
        {
            imgStart.sprite = emptyHeart;
        }
    }

    public void Restart()
    {
        StartPuzzle();
    }

    public void Return()
    {
        GameManager.gameManager.sceneCurrent = SceneManager.GetActiveScene().buildIndex;
        GameManager.gameManager.firstPosPl = GameManager.gameManager.posHelper;

        Player.player.spriteColor.a = 255;
        SceneManager.LoadScene(GameManager.gameManager.sceneHelper);
    }
}
