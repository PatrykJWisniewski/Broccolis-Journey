using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class User
{
    public string userName;
    public string finalTime;
    public string name;
    // Start is called before the first frame update
    public User()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        name = currentScene.name;
        userName = PlayerTime.PlayerName;
        finalTime = PlayerTime.TimePassed;
    }
}
