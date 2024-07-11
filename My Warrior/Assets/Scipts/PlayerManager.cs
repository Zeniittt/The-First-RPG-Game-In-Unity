using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, ISaveManager
{
    public static PlayerManager instance;
    public Player player;

    public int myAmountSoul;

    private void Awake()
    { 
        if(instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    public bool HaveEnoughSoul(int _amountSoul)
    {
        if(_amountSoul > myAmountSoul)
        {
            Debug.Log("Not enough soul");
            return false;
        }

        myAmountSoul -= _amountSoul;

        return true;
    }

    public int GetCurrentSouls() => myAmountSoul;

    public void LoadData(GameData _data)
    {
        this.myAmountSoul = _data.amountSoul;
    }

    public void SaveData(ref GameData _data)
    {
        _data.amountSoul = this.myAmountSoul;
    }
}
