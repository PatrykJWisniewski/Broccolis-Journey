using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class User
{
    public string userName;
    public string finalTime;
    // Start is called before the first frame update
    public User()
    {
        userName = PlayerTime.PlayerName;
        finalTime = PlayerTime.TimePassed;

    }
}
