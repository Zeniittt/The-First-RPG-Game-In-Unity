using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public int amountSoul;
    public SerializableDictionary<string, int> inventory;

    public GameData()
    {
        this.amountSoul = 0;
        inventory = new SerializableDictionary<string, int>();
    }
}
