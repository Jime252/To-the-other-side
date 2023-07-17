using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemys : ScriptableObject
{
    public Sprite sprite;

    public Sprite image;

    public string enemyName;

    public float health;

    public int level;

    public float attack;

    public bool fire, land, grass, water, electric;
}
