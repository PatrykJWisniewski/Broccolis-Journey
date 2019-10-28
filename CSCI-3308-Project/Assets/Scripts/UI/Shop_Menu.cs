using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop_Menu : MonoBehaviour
{
    public static Shop_Menu S;

    public bool mouseMode;
    public Shop_Buttons[] buttons;
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
            buttons[selectedOptionNum].Deselect(); //Resets the last option
        }

        //If the player has selected a valid key on the keyboard...
        if ((Input.GetKeyDown("w") || Input.GetKeyDown("s") || Input.GetKeyDown("a") || Input.GetKeyDown("d") || Input.GetKeyDown("up") || Input.GetKeyDown("down") || Input.GetKeyDown("left") || Input.GetKeyDown("right")) && mouseMode == true)
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
                selectedOptionNum = 5;
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
            if (selectedOptionNum == 5)
            {
                selectedOptionNum = 0;
            }
            else
            {
                selectedOptionNum++;
            }

            buttons[selectedOptionNum].Select(); //Selects the new option

        }
        else if (Input.GetKeyDown("escape") || Input.GetKeyDown("backspace"))
        {
            Exit();
        }
        else if(Input.GetKeyDown(KeyCode.Return))
        {
            Enter();
        }
    }

    public void SetupShop(PlayerItems[] shopItems)
    {
        int i;
        for (i = 0; i < shopItems.Length; i++)
        {
            buttons[i].gameObject.SetActive(true);
            buttons[i].icon.sprite = Player_Data.S.itemSprites[shopItems[i].spriteIndex];
            buttons[i].itemName.text = shopItems[i].name;
            buttons[i].cost.text = "Cost: " + shopItems[i].cost;
        }

        for (int j = i; j < buttons.Length; j++)
        {
            buttons[j].gameObject.SetActive(false);
        }
    }

    //Runs when the player wants to go back
    public void Exit()
    {
        Main.S.shopMenu.SetActive(false);
        HubCharacterController.S.hubChar.SetScale(new Vector3(1,1,1));
    }

    public void Enter()
    {
        for(int i = 0; i<Player_Data.S.PlayerItems.Count; i++)
        {
            if(Player_Data.S.PlayerItems[i].name == buttons[selectedOptionNum].itemName.text)
            {
                Player_Data.S.PlayerItems[i].quanity++;
                return;
            }
        }

        PlayerItems newItem = new PlayerItems();
        newItem.name = buttons[selectedOptionNum].itemName.text;
        newItem.quanity = 1;
        Player_Data.S.PlayerItems.Add(newItem);
    }

    public void SwitchToKey()
    {
        Cursor.visible = false;
        mouseMode = false;
        buttons[selectedOptionNum].Deselect(); //Makes the last selected option reset if needed
        buttons[0].Select(); //Selects the first option
        selectedOptionNum = 0;
    }
}
