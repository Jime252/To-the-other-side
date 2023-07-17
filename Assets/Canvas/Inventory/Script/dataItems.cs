using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataBase", menuName = "Items")]
public class dataItems : ScriptableObject
{
    [System.Serializable]

    public struct InventoryItems
    {
        public int ID;

        public Sprite icono;

        public string description;
    }

    public InventoryItems[] dataBase;
}
