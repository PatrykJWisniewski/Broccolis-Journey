using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    public GameObject CompleteLevelUI;
    public GameObject Canvas;
    //public GameObject PlayerTime;
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
        Canvas = GameObject.Find("Canvas");
        Canvas.GetComponent<PlayerTime>().Submit();
        StartCoroutine(Pause());
        
        
    }
    private IEnumerator Pause()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(1);
    }
}
