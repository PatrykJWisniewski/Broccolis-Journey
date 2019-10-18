using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //This allows us to load scenes

public class Main : MonoBehaviour
{
    public static Main S;

    // Start is called before the first frame update
    void Start()
    {
        S = this;
    }

    //Loads the scene when the player loads the game. Called from CharacterController.cs
    public void PlatformerFailed()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene("Platformer");
    }

    public void LoadNewGame()
    {
        SceneManager.LoadScene("Hub");
    }
}
