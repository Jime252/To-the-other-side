using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] CombatManager combatScript;
    [SerializeField] CreateEnemy createEnemyScript;

    public void InicioCombate()
    {
        StartCoroutine(combatScript.CompletarFrase());
    }

    public void Victoria() // We call it in the animation
    {
        //GameManager.gameManager.money += 305;
        //GameManager.gameManager.level += createEnemyScript.level;
        SceneManager.LoadScene(GameManager.gameManager.sceneCurrent);
    }

    public void Derrota() // We call it in the animation
    {
        SceneManager.LoadScene(GameManager.gameManager.sceneCurrent);
    }
}
