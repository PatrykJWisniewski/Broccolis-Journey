using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[System.Serializable]
public class PlayerItems
{
    public string name;
    public int quanity;
    public int cost;
    public int spriteIndex;
}

public class Player_Data : MonoBehaviour
{
    public string username = "0";
    static public Player_Data S;

    public Sprite[] itemSprites;

    //Stats to be saved
    public int playerMoney;
    public List<PlayerItems> PlayerItems;

    private void Awake()
    {
        // STOP TAKING OUT DAM INPUT
        #if !UNITY_EDITOR && UNITY_WEBGL
        WebGLInput.captureAllKeyboardInput = false;
        #endif

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

    // Call with unityInstance.SendMessage('Player_Data', 'SetUser','username');

    public void SetUser(string setUser)
    {
        username = setUser;
    }

    public string GetUser()
    {
        return username;
    }
    //Loads the players data
    public void Load(int slot)
    {

    }
}