using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_Data : MonoBehaviour
{
    static public Player_Data S;

    //Stats to be saved
    public int playerMoney;

    private void Awake()
    {
        //If the gameobject does not excist in the next scene then dont destroy it and set it's singalton staus
        if (S == null)
        {
            DontDestroyOnLoad(gameObject);
            S = this;
        }
        //Else if this is not the singleton and a second object then destroy it. For debugging.
        else if (S != this)
        {
            Destroy(gameObject);
        }
    }

    //Saves the players data such as Health and Itmes
    public void Save(int slot)
    {

    }

    //Loads the players data
    public void Load(int slot)
    {

    }
}