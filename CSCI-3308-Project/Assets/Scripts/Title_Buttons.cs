using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Title_Buttons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    //This has to be set from the inspector
    public int optionNum; //This Mouse_Menu scripts number based off the option going from order from top to bottom
    public Image background;

    public void OnPointerEnter(PointerEventData eventData)
    {
        //If the user is currently using their mouse to select stuff
        if(Title_Menu.S.mouseMode)
        {
            Select();
            Title_Menu.S.selectedOptionNum = optionNum;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //If the user is currently using their mouse to select stuff
        if (Title_Menu.S.mouseMode)
        {
            Deselect();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Title_Menu.S.Enter();
        Reset();
    }

    //Moves the option too the left and changes the color
    public void Select()
    {
        background.enabled = true;
    }

    //Moves the option back too the right and changes the color
    public void Deselect()
    {
        background.enabled = false;
    }

    //Moves it too it's defualt posision
    public void Reset()
    {
        background.enabled = false;
    }
}
