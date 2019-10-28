using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Shop_Buttons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    //This has to be set from the inspector
    public int optionNum; //This Mouse_Menu scripts number based off the option going from order from top to bottom
    public GameObject selectedGUIImage;

    public Image icon;
    public Text itemName;
    public Text cost;

    public Color hoverColor;
    public Color noHoverColor;

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
        Shop_Menu.S.Enter();
    }

    public void Select()
    {
        selectedGUIImage.SetActive(true);
    }

    public void Deselect()
    {
        selectedGUIImage.SetActive(false);
    }
}
