using System;
using System.Diagnostics;
using UnityEngine;
using Proyecto26;
using UnityEngine.UI;

public class PlayerTime : MonoBehaviour
{
    public GameObject Time;

    public bool isPlaying;
    Text txt;
    //Test Database  https://write-from-unity.firebaseio.com/
    //Real https://csci3308-8fc5a.firebaseio.com/
    public static Stopwatch sw = new Stopwatch();
    public static string TimePassed;
    public static string PlayerName = "Guest";


    private const string projectId = "csci3308-8fc5a"; // You can find this in your Firebase project settings
    private static readonly string databaseURL = $"https://{projectId}.firebaseio.com/";

    // Start is called before the first frame update

    void Start()
    {
        sw.Restart();
        isPlaying=true;
        StartTimer();
    }

    void Awake()
    {
        txt = Time.GetComponent<Text>();
    }
    void Update()
    {
        if(isPlaying)
        {
            TimeSpan ts = sw.Elapsed;
            TimePassed = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            txt.text = TimePassed;
        }
    }
    public void StartTimer()
    {
        sw.Start();
        Console.WriteLine("timer started");
    }

    public void RestartTimer()
    {
        sw.Restart();
    }

    // Call with unityInstance.SendMessage('UserInfo', 'getUser','username');

    public void SetUser(string setUser)
    {
        PlayerName = setUser;
    }

    public void Submit()
    {
        sw.Stop();
        if (PlayerName == "0")
        {
            PlayerName = "Guest";
        }
        TimeSpan ts = sw.Elapsed;
        TimePassed = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
        PostToDatabase();
    }
    private void PostToDatabase()
    {
        User user = new User();
        RestClient.Put<User>($"{databaseURL}users/" + PlayerName + ".json", user).Then(response =>
        {
            UnityEngine.Debug.Log("The user was successfully uploaded to the database");
        });
    }
}
