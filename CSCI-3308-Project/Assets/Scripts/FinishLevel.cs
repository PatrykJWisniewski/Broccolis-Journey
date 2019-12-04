using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    public GameObject CompleteLevelUI;
    public GameObject Canvas;
<<<<<<< Updated upstream
    //public GameObject PlayerTime;
=======
>>>>>>> Stashed changes
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Finish();
        }
    }

    void Finish()
    {
        CompleteLevelUI.SetActive(true);
<<<<<<< Updated upstream
        Canvas = GameObject.Find("Canvas");
        Canvas.GetComponent<PlayerTime>().Submit();
=======
        PlayerTime pt = Canvas.GetComponent<PlayerTime>();
        pt.Submit();
>>>>>>> Stashed changes
        StartCoroutine(Pause());
        
        
    }
    private IEnumerator Pause()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(1);
    }
}
