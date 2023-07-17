using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    // Canvas
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject buttons, buttonsExit;
    [SerializeField] Image plantButton;
    string frase; // Nombre del enemigo aceptó tu desafío

    // Personajes
    [SerializeField] SpriteRenderer player;
    [SerializeField] GameObject playerAttack, enemyAttack;
    // Enemigo
    [SerializeField] GameObject enemy;
    SpriteRenderer enemySR;
    // Aleatoriedad
    [HideInInspector] public int enemigoAle;
    [SerializeField] CreateEnemy enemyAle;

    // Animaciones
    Animator animPlayerAttack, animEnemyAttack;
    [SerializeField] Animator canvasDos;

    // Turnos
    enum Estado { PlayerTurn, EnemyTurn, Victory, Defeat }
    Estado estadoActual;

    // Dañar y Curar
    bool enEjecucion;
    [SerializeField] Slider playerLive, enemyLive;

    // Ataques
    bool waterBool;
    bool plant = true;
    [SerializeField] GameObject coolDown;

    // Fade out
    [SerializeField] Animator animCanvas;

    private void Awake()
    {
        // Animaciones de ataque
        animPlayerAttack = playerAttack.GetComponent<Animator>();
        animEnemyAttack = enemyAttack.GetComponent<Animator>();

        // Enemigos 
        enemySR = enemy.GetComponent<SpriteRenderer>();

        Probabilidad();

        estadoActual = Estado.PlayerTurn;
    }

    private void Start()
    {
        enemyLive.value = enemyAle.health;
    }

    // Update is called once per frame
    void Update()
    {
        // Turno del enemigo
        if (estadoActual == Estado.EnemyTurn)
        {
            if (!enEjecucion)
            {
                if (enemyAle.water == true)
                {
                    animPlayerAttack.SetTrigger("waterAttack");
                }
                else if (enemyAle.grass == true)
                {
                    animPlayerAttack.SetTrigger("plantAttack");
                }
                else if (enemyAle.electric == true)
                {
                    animPlayerAttack.SetTrigger("thunderAttack");
                }
                else if (enemyAle.land == true)
                {
                    animPlayerAttack.SetTrigger("rockAttack");
                }
                else if (enemyAle.fire == true)
                {
                    animPlayerAttack.SetTrigger("flamAttack");
                }
                if (!waterBool)
                {
                    StartCoroutine(ActualizarBarra(playerLive, enemyAle.attack, false, true));
                }
                else
                {
                    StartCoroutine(ActualizarBarra(playerLive, 0, false, true));
                    waterBool = false;
                }               
            }
        }       
    }

    private void Probabilidad()
    {
        float prob = Random.Range(0f, 100f);
        if (prob <= 55) // Común 50%
        {
            frase = " El mismo enemigo de siempre";
            enemigoAle = Random.Range(0, 7);
        }
        else if (prob <= 75) // Raro 25%
        {
            frase = " Una criatura... enorme aparecio";
            enemigoAle = Random.Range(7, 14);
        }
        else if (prob <= 90) // Épico
        {
            frase = " Una criatura fuerte aparecio!!";
            enemigoAle = Random.Range(14, 16);
        }
        else if (prob <= 99.5f) // Legendario
        {
            frase = " Ojo a quien tenemos aca!!";
            enemigoAle = Random.Range(16, 21);
        }
        else
        {
            frase = " AY EL BICHO!!";
            enemigoAle = Random.Range(21, 24);
        }
    }

    IEnumerator ActualizarBarra(Slider barra, float vida, bool curar, bool character)
    {
        enEjecucion = true;
        Image imgBarra = barra.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        float vidaIni = barra.value;
        if (!curar)
        {
            barra.value -= vida;
            if (character)
            {
                StartCoroutine(ParpadeoRojo());
            }
            else
            {
                StartCoroutine(ParpadeoRojoEnemy());
            }
            yield return new WaitForSeconds(1f);
        }
        else
        {
            barra.value += vida;           
            curar = false;
            yield return new WaitForSeconds(1f);
        }
        if (estadoActual == Estado.PlayerTurn)
        {
            estadoActual = Estado.EnemyTurn;
            buttons.SetActive(false);
        }
        else
        {
            estadoActual = Estado.PlayerTurn;
            buttons.SetActive(true);
        }

        // Ganar/Perder combate
        if (enemyLive.value <= 0)
        {
            estadoActual = Estado.Victory;
            Victoria();
        }
        if (playerLive.value <= 0)
        {
            estadoActual = Estado.Defeat;
            Derrota();
        }

        enEjecucion = false;
    }

    IEnumerator ParpadeoRojo()
    {
        for (int i = 0; i < 3; i++)
        {
            player.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            player.color = Color.white;
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator ParpadeoRojoEnemy()
    {
        for (int i = 0; i < 3; i++)
        {
            enemySR.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            enemySR.color = Color.white;
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator CoolDown()
    {
        plantButton.color = Color.red;
        yield return new WaitForSeconds(5.5f);
        plant = true;
        plantButton.color = Color.white;
        yield return new WaitForSeconds(1.5f);
    }

    public IEnumerator CompletarFrase()
    {
        // El método ToCharArray sirve para deglosar un string en char = carácteres, que es justo lo que te devuelve
        char[] caracs = frase.ToCharArray();
        for (int i = 0; i < caracs.Length; i++)
        {
            text.text += caracs[i];
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.5f);
        text.text = "";
        yield return new WaitForSeconds(0.5f);
        buttons.SetActive(true);
    }

    public void DaggerButton() // Llamado en el botón
    {
        animPlayerAttack.SetTrigger("projectile");
        //StartCoroutine(ActualizarBarra(playerLive, 5 + GameManager.gameManager.level, false, false));
    }

    public void WaterButton() // Llamado en el botón
    {
        waterBool = true;
        animPlayerAttack.SetTrigger("water");
        StartCoroutine(ActualizarBarra(playerLive, 0, false, true));
    }

    public void PlantButton() // Llamado en el botón
    {
        if (plant)
        {
            plant = false;
            StartCoroutine(CoolDown());
            animPlayerAttack.SetTrigger("plant");
            StartCoroutine(ActualizarBarra(playerLive, 40, true, true));
        }
    }

    public void RockButton() // Llamado en el botón
    {
        animEnemyAttack.SetTrigger("rock");
        if (enemyAle.land == true)
        {
            StartCoroutine(ActualizarBarra(enemyLive, 0, false, false));
        }
        else
        {
            //StartCoroutine(ActualizarBarra(enemyLive, 20 + GameManager.gameManager.level, false, false));
        }       
    }

    public void ThunderButton() // Llamado en el botón
    {
        animEnemyAttack.SetTrigger("thunder");
        if (enemyAle.electric == true)
        {
            StartCoroutine(ActualizarBarra(enemyLive, 0, false, false));
        }
        else
        {
            //StartCoroutine(ActualizarBarra(enemyLive, 20 + GameManager.gameManager.level, false, false));
        }
    }
    public void FlamButton() // Llamado en el botón
    {
        animEnemyAttack.SetTrigger("flam");
        if (enemyAle.fire == true)
        {
            StartCoroutine(ActualizarBarra(enemyLive, 0, false, false));
        }
        else
        {
            //StartCoroutine(ActualizarBarra(enemyLive, 20 + GameManager.gameManager.level, false, false));
        }
    }

    public void ExitButton() // Llamado en el botón
    {
        buttons.SetActive(false);
        buttonsExit.SetActive(true);
    }

    public void NoButton()
    {
        buttons.SetActive(true);
        buttonsExit.SetActive(false);
    }

    public void YesButton()
    {
        StartCoroutine(Escapar());
    }

    IEnumerator Escapar()
    {
        buttons.SetActive(false);
        frase = "Escapaste sin problemas";
        // Desgloasamos
        char[] caracs = frase.ToCharArray();
        for (int i = 0; i < caracs.Length; i++)
        {
            text.text += caracs[i];
            yield return new WaitForSeconds(0.05f);
        }
        animCanvas.SetTrigger("fadeOut");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(GameManager.gameManager.sceneCurrent);
    }

    void Victoria()
    {
        frase = "VICTORIA";
        buttons.SetActive(false);
        StartCoroutine(CompletarFrase());
        canvasDos.SetTrigger("victory");
    }

    void Derrota()
    {
        frase = "DERROTA";
        buttons.SetActive(false);
        //GameManager.gameManager.playerLive.value -= 20;
        StartCoroutine(CompletarFrase());
        canvasDos.SetTrigger("defaut");
    }
}
