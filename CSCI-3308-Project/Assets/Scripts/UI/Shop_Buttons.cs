using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Shop_Buttons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    //This has to be set from the inspector
    public int optionNum; //This Mouse_Menu scripts number based off the option going from order from top to bottom
    public bool buying;
    public GameObject selectedGUIImage;

    public Image icon;
    public int iconIndex;
    public Text itemName;
    public Text cost;
    public int costInt;

    public float lerpTime;
    public float currentLerpTime;
    public float newTranparency;

    public Color hoverColor;
    public Color noHoverColor;
    private void Update()
    {
        //Increment timer once per frame
        currentLerpTime += Time.unscaledDeltaTime;
        if (currentLerpTime > lerpTime)
        {
            currentLerpTime = lerpTime;
        }

        //Moves in a smooth motion
        float perc = currentLerpTime / lerpTime;
        perc = perc * perc * (3f - 2f * perc); //Smooth lerp curve
        Color c = selectedGUIImage.GetComponent<Image>().color;
        c.a = Mathf.Lerp(c.a, newTranparency, perc);
        selectedGUIImage.GetComponent<Image>().color = c;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //If the user is currently using their mouse to select stuff
        if (Shop_Menu.S.mouseMode)
        {
            Select();
            Shop_Menu.S.selectedOptionNum = optionNum;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //If the user is currently using their mouse to select stuff
        if (Shop_Menu.S.mouseMode)
        {
            Deselect();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Shop_Menu.S.Enter(buying);
    }

    public IEnumerator NotEnouthMoneyError()
    {
        Color c = Shop_Menu.S.moneyText.color;
        c.g = 0;
        c.b = 0;
        Shop_Menu.S.moneyText.color = c;

        Color c2 = selectedGUIImage.GetComponent<Image>().color;
        c2.r = 255;
        selectedGUIImage.GetComponent<Image>().color = c2;

        yield return new WaitForSecondsRealtime(.1f);

        c = Shop_Menu.S.moneyText.color;
        c.g = 255;
        c.b = 255;
        Shop_Menu.S.moneyText.color = c;

        c2 = selectedGUIImage.GetComponent<Image>().color;
        c2.r = 0;
        selectedGUIImage.GetComponent<Image>().color = c2;
    }

    public void Select()
    {
        newTranparency = .7f;
        currentLerpTime = 0;
    }

    public void Deselect()
    {
        newTranparency = 0;
        currentLerpTime = 0;
    }
}
