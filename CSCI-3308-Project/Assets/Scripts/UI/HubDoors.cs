using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HubDoors : MonoBehaviour
{
    public GameObject triggerFlavorText;

    public float lerpTime;
    public float currentLerpTime;
    public Vector3 newScale;

    public string sceneName;
    public bool canLoadScene;

    private void Update()
    {
        if (gameObject.layer == 12) return;
        //Increment timer once per frame
        currentLerpTime += Time.unscaledDeltaTime;
        if (currentLerpTime > lerpTime)
        {
            currentLerpTime = lerpTime;
        }

        //Moves in a smooth motion
        float perc = currentLerpTime / lerpTime;
        perc = perc * perc * (3f - 2f * perc); //Smooth lerp curve
        triggerFlavorText.transform.localScale = Vector3.Lerp(triggerFlavorText.transform.localScale, newScale, perc);

        if (Input.GetKeyDown(KeyCode.W) && canLoadScene)
        {
            UIFadeOutIn.S.currentLerpTime = 0;
            UIFadeOutIn.S.newTranparency = 255;

            SceneManager.LoadSceneAsync(sceneName);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(gameObject.layer == 12)
        {
            SceneManager.LoadScene(sceneName);
        }

        canLoadScene = true;
        newScale = new Vector3(.15f, .15f, 1);
        currentLerpTime = 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canLoadScene = false;
        newScale = new Vector3(0, 0, 0);
        currentLerpTime = 0;
    }
}
