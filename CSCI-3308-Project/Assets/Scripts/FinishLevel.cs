using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    public GameObject CompleteLevelUI;
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

        StartCoroutine(Pause());
        
        
    }
    private IEnumerator Pause()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(1);
    }
}
