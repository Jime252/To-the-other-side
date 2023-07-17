using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateEnemy : MonoBehaviour
{
    [SerializeField] Enemys[] enemy;
    [SerializeField] CombatManager combatScript;
    [SerializeField] Image imagenEnemy;

    // Características
    [HideInInspector] public SpriteRenderer sR;

    [HideInInspector] public string enemyName;

    [HideInInspector] public int level;

    [HideInInspector] public float health, attack;

    [HideInInspector] public bool fire, land, grass, water, electric;

    // Start is called before the first frame update
    void Start()
    {
        // Visual
        Enemys enemigoAle = enemy[combatScript.enemigoAle];

        sR = GetComponent<SpriteRenderer>();

        sR.sprite = enemigoAle.sprite;
        imagenEnemy.sprite = enemigoAle.image;

        // Características
        enemyName = enemigoAle.name;
        health = enemigoAle.health;
        level = enemigoAle.level;
        attack = enemigoAle.attack;
        fire = enemigoAle.fire;
        land = enemigoAle.land;
        grass = enemigoAle.grass;
        water = enemigoAle.water;
        electric = enemigoAle.electric;
    }
}
