using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Xml;
using System;

public class GameManager : MonoBehaviour
{
    // Singleton pattern
    [HideInInspector] public static GameManager gameManager;

    // Player position
    [HideInInspector] public Vector3 firstPosPl;
    [HideInInspector] public Vector3 posHelper;
    [HideInInspector] public int sceneCurrent = 1;
    [HideInInspector] public int sceneHelper;

    // Anim
    public Animator sliderAnim;
    //[SerializeField] Image mouse;
    //[SerializeField] Sprite[] mouseSp;

    // History
    [HideInInspector] public bool english;

    private void Awake()
    {
        firstPosPl = new Vector3(1.5f, 8.5f, 0);
        if (gameManager == null)
        {
            gameManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        Auxiliar.aux.AuxiliarVoid();

    } // Singleton pattern

    // ------------------------------------------------------------------ \\

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Method to be executed automatically when a scene is executed
    void OnSceneLoaded(Scene escenaCargada, LoadSceneMode mode)
    {
        RellenarPlayer();
        sliderAnim = GameObject.FindGameObjectWithTag("slider").GetComponent<Animator>();
    }

    void RellenarPlayer()
    {
        Player.player.gameObject.SetActive(true);
        Player.player.gameObject.transform.position = firstPosPl;
        Player.player.destinationPoint = firstPosPl;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // ------------------------------------------------------------------ \\

    // Use this for initialization
    void Start()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        //mouse.enabled = false;

        bottonIndex = -1;
    }

    // ------------------------------------------------------------------ \\

    private XmlDocument xmlDoc;
    private TextAsset ta;
    private XmlNodeList nodosInfo;

    public Image character;
    [SerializeField] Text speakerName, textBox;

    public GameObject answers, spriteBox;
    [SerializeField] Text ansTxt1, ansTxt2, ansTxt3;

    private bool slowType = false;
    private int typePos = 0;

    bool question;
    [HideInInspector] public int bottonIndex;

    // ------------------------------------------------------------------ \\

    // Page data.
    public struct pageNovel
    {
        public int pageType;
        public int posSpeaker;
        public string name;
        public string text;
        public Sprite spCharac;
        public int[] nextPag;
    }

    // Page list
    private List<pageNovel> pages = new List<pageNovel>();
    private pageNovel page;

    private enum estados { paintingPage, waitingClick, waitingAnswer };
    private estados state = estados.paintingPage;

    [HideInInspector] public int posPage = 0;

    // ------------------------------------------------------------------ \\

    public void activateHistory(int currentChapter)
    {
        string fileName = PlayerPrefs.GetString("NovelName");

        if (english)
        {
            fileName = "NovelEn_" + currentChapter;           
        }
        else
        {
            fileName = "Novel_" + currentChapter;
        }
       
        Player.player.enabled = false;

        question = false;
        pages.Clear();
        PayloadXML(fileName);
        LoadPage();
    }

    // ------------------------------------------------------------------ \\

    public void PayloadXML(string novel)
    {

        xmlDoc = new XmlDocument();
        ta = (TextAsset)Resources.Load(novel);
        xmlDoc.LoadXml(ta.ToString());

        XmlNodeList rootnode = xmlDoc.SelectNodes("Novel");

        nodosInfo = rootnode[0].SelectNodes("Stories");

        string miStr = rootnode[0].SelectSingleNode("Stories").SelectSingleNode("Speaker").InnerText;

        string[] arrayStr;

        foreach (XmlNode nodo in nodosInfo)
        {
            try
            {
                page = new pageNovel();
                page.pageType = Int32.Parse(nodo.SelectSingleNode("Type").InnerText);
            }
            catch (Exception e)
            {
                Debug.Log("Error Type");
            }
            try
            {
                page.text = nodo.SelectSingleNode("Text").InnerText;
            }
            catch (Exception e)
            {
                Debug.Log("Error Text");
            }
            try
            {
                XmlNodeList characters = nodo.SelectNodes("Character");

                page.spCharac = Resources.Load<Sprite>(characters[0].SelectSingleNode("Image").InnerText);
                page.name = characters[0].SelectSingleNode("Name").InnerText;
            }
            catch (Exception e)
            {
                Debug.Log("Error character");
            }
            try
            {
                arrayStr = nodo.SelectSingleNode("Next").InnerText.Split('-');
                page.nextPag = new int[arrayStr.Length];

                for (int i = 0; i < arrayStr.Length; i++)
                {
                    page.nextPag[i] = Int32.Parse(arrayStr[i]);
                }
            }
            catch (Exception e)
            {
                Debug.Log("Error Nexts");
            }
            pages.Add(page);

        } // End foreach

    } // End payloadXML

    // ------------------------------------------------------------------ \\

    private void LoadPage()
    {
        try
        {
            page = pages[posPage];
        }
        catch (Exception e)
        {
            posPage = 0;
            page = pages[posPage];
        }

        // Canvas cleaner
        textBox.text = "";
        ansTxt1.text = "";
        ansTxt2.text = "";
        ansTxt3.text = "";
        answers.SetActive(false);

        character.sprite = page.spCharac;

        // Canvas filler
        switch (page.posSpeaker)
        {
            case 0:
                speakerName.text = page.name;
                break;
            case 1:
            default:
                // No orador
                break;
        }

        switch (page.pageType)
        {
            case 0: // Normal text
                spriteBox.SetActive(true);
                typePos = 0;
                slowType = true;
                break;
            case 1: // Ask the player
                spriteBox.SetActive(true);
                question = true;
                ProcessesQuestion();
                break;
            case 2: // End of conversation
            default:
                //StartCoroutine(BlinkMouse());
                spriteBox.SetActive(false);
                posPage = 0;
                question = true;
                pages.Clear();
                Player.player.enabled = true;
                break;
        }

    } // End loadPage

    // ------------------------------------------------------------------ \\

    private void ProcessesQuestion()
    {
        question = true;
        string[] aux = page.text.Split('_');
        answers.SetActive(true);

        page.text = aux[0];
        textBox.text = aux[0];
        ansTxt1.text = aux[1];
        ansTxt2.text = aux[2];
        ansTxt3.text = aux[3];

        state = estados.waitingAnswer;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(posPage);
        if (slowType)
        {
            if (typePos < page.text.Length)
            {
                textBox.text += page.text[typePos];
                typePos++;
            }
            else
            {
                slowType = false;
                state = estados.waitingClick;
            }
        }

        if (!question)
        {
            if ((state == estados.waitingClick) && (Input.GetKeyUp(KeyCode.Space)) || (Input.GetMouseButtonDown(0)) || Input.GetKeyUp(KeyCode.Q))
            {
                state = estados.paintingPage;
                posPage = page.nextPag[0];
                LoadPage();
            }
        }

    } // End Update 

    public void clickAnswer(int posButton)
    {
        if (state == estados.waitingAnswer)
        {
            state = estados.paintingPage;            
            int[] nextPages = page.nextPag;
            if (posButton >= 0 && posButton < nextPages.Length)
            {
                posPage = nextPages[posButton];
            }
            else
            {
                posPage = nextPages[0];
            }
            bottonIndex = posButton;
            question = false;
            LoadPage();
        }
    }

    //IEnumerator BlinkMouse()
    //{
    //    mouse.enabled = true;
    //    for (int i = 0; i < 6; i++)
    //    {
    //        mouse.sprite = mouseSp[0];
    //        yield return new WaitForSeconds(0.2f);
    //        mouse.sprite = mouseSp[1];
    //        yield return new WaitForSeconds(0.2f);
    //    }
    //    mouse.enabled = false;
    //}
}
