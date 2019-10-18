using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title_Menu : MonoBehaviour
{
    static public Title_Menu S;

    public bool mouseMode;
    public Title_Buttons[] buttons;
    public int selectedOptionNum;

    private void Start()
    {
        S = this;
    }

    private void Update()
    {
        //If the user started to use the mouse
        if ((Input.GetAxis("Mouse X") < 0 || Input.GetAxis("Mouse X") > 0) && mouseMode == false)
        {
            Cursor.visible = true;
            mouseMode = true;
            buttons[selectedOptionNum].Reset(); //Resets the last option
        }

        //If the player has selected a valid key on the keyboard...
        if ((Input.GetKeyDown("w") || Input.GetKeyDown("s") || Input.GetKeyDown("up") || Input.GetKeyDown("down")) && mouseMode == true)
        {
            mouseMode = false;
            SwitchToKey(); //Sets the menu to have the first option selected and changes the mode to keyboard mode
        }
        else if ((Input.GetKeyDown("w") || Input.GetKeyDown("up")) && mouseMode == false)
        {
            buttons[selectedOptionNum].Deselect(); //Deselectes the last option

            //Incraments too the correct index number
            if (selectedOptionNum == 0)
            {
                selectedOptionNum = 2;
            }
            else
            {
                selectedOptionNum--;
            }

            buttons[selectedOptionNum].Select(); //Selects the new option
        }
        else if ((Input.GetKeyDown("s") || Input.GetKeyDown("down")) && mouseMode == false)
        {
            buttons[selectedOptionNum].Deselect(); //Deselectes the last option

            //Incraments too the correct index number
            if (selectedOptionNum == 2)
            {
                selectedOptionNum = 0;
            }
            else
            {
                selectedOptionNum++;
            }

            buttons[selectedOptionNum].Select(); //Selects the new option
        }
        else if (Input.GetKeyDown("return") || Input.GetKeyDown("enter"))
        {
            Enter();
        }
    }
    
    //Runs when the player selects enter
    public void Enter()
    {
        buttons[selectedOptionNum].Reset();

        switch (selectedOptionNum)
        {
            //If a new game is selected
            case 0:
                Main.S.LoadNewGame();
                break;
            //If the game is continued
            //case 1:
                //Main.S.loadsaveMenu.SetActive(true);
                //Main.S.titleMenu.SetActive(false);
                //Main.S.loadsaveMenu.GetComponent<LoadSave_Menu>().PrepSaveSlots();
                //break;
            //If the settings are selected
            //case 2:
                //Main.S.settingsMenu.SetActive(true);
                //Main.S.titleMenu.SetActive(false);
                //break;
        }
    }

    public void SwitchToKey()
    {
        Cursor.visible = false;
        mouseMode = false;
        buttons[selectedOptionNum].Reset(); //Makes the last selected option reset if needed
        buttons[0].Select(); //Selects the first option
        selectedOptionNum = 0;
    }
}
