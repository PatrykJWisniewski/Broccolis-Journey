using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop_Menu : MonoBehaviour
{
    public static Shop_Menu S;

    public bool mouseMode;
    public Shop_Buttons[] buttons;
    public Shop_Buttons[] sellButtons;
    public int selectedOptionNum;

    public Text moneyText;

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
    }

    public void SetupShop(PlayerItems[] shopItems)
    {
        moneyText.text = Player_Data.S.playerMoney.ToString();

        //Gets all of the shopkeeps items to display in the UI
        int i;
        for (i = 0; i < shopItems.Length; i++)
        {
            buttons[i].gameObject.SetActive(true);
            buttons[i].icon.sprite = Player_Data.S.itemSprites[shopItems[i].spriteIndex];
            buttons[i].itemName.text = shopItems[i].name;
            buttons[i].cost.text = "Cost: " + shopItems[i].cost;
            buttons[i].costInt = shopItems[i].cost;
            buttons[i].iconIndex = shopItems[i].spriteIndex;
        }

        for (int j = i; j < buttons.Length; j++)
        {
            buttons[j].gameObject.SetActive(false);
        }

        UpdateSellPage();
    }

    public void UpdateSellPage()
    {
        //Gets all of the shopkeeps items to display in the UI
        int i;
        for (i = 0; i < Player_Data.S.PlayerItems.Count; i++)
        {
            sellButtons[i].gameObject.SetActive(true);
            sellButtons[i].icon.sprite = Player_Data.S.itemSprites[Player_Data.S.PlayerItems[i].spriteIndex];
            sellButtons[i].itemName.text = Player_Data.S.PlayerItems[i].name;
            sellButtons[i].cost.text = "Cost: " + Player_Data.S.PlayerItems[i].cost;
            sellButtons[i].costInt = Player_Data.S.PlayerItems[i].cost;
            sellButtons[i].iconIndex = Player_Data.S.PlayerItems[i].spriteIndex;
        }

        for (int j = i; j < buttons.Length; j++)
        {
            sellButtons[j].gameObject.SetActive(false);
        }
    }

    //Runs when the player wants to go back
    public void Exit()
    {
        Main.S.shopMenu.SetActive(false);
        HubCharacterController.S.hubChar.SetScale(new Vector3(1,1,1));
    }

    public void Enter(bool buying)
    {
        if (buying)
        {
            if (buttons[selectedOptionNum].costInt <= Player_Data.S.playerMoney)
            {
                //Checks if the item already exsists in the users inverntory
                for (int i = 0; i < Player_Data.S.PlayerItems.Count; i++)
                {
                    //It it does adds it too the total count and returns out of this funtion
                    if (Player_Data.S.PlayerItems[i].name == buttons[selectedOptionNum].itemName.text)
                    {
                        Player_Data.S.PlayerItems[i].quanity++;
                        Player_Data.S.playerMoney -= buttons[selectedOptionNum].costInt; //Reduces Players money
                        moneyText.text = Player_Data.S.playerMoney.ToString();
                        UpdateSellPage();
                        return;
                    }
                }

                //Creates a new item and adds it too the players inventory
                PlayerItems newItem = new PlayerItems();
                newItem.name = buttons[selectedOptionNum].itemName.text;
                newItem.quanity = 1;
                newItem.cost = buttons[selectedOptionNum].costInt;
                newItem.spriteIndex = buttons[selectedOptionNum].iconIndex;
                Player_Data.S.PlayerItems.Add(newItem);

                Player_Data.S.playerMoney -= buttons[selectedOptionNum].costInt; //Reduces Players money
                moneyText.text = Player_Data.S.playerMoney.ToString();
                UpdateSellPage();
            }
            else
            {
                buttons[selectedOptionNum].StartCoroutine("NotEnouthMoneyError");
                Debug.Log("Not Enough Money to buy");
            }
        }
        else
        {
            for (int i = 0; i < Player_Data.S.PlayerItems.Count; i++)
            {
                if (Player_Data.S.PlayerItems[i].name == sellButtons[selectedOptionNum].itemName.text)
                {
                    Player_Data.S.PlayerItems[i].quanity--;
                    Player_Data.S.playerMoney += buttons[selectedOptionNum].costInt; //Reduces Players money
                    moneyText.text = Player_Data.S.playerMoney.ToString();

                    if(Player_Data.S.PlayerItems[i].quanity == 0)
                    {
                        Player_Data.S.PlayerItems.RemoveAt(i);
                    }

                    UpdateSellPage();
                    return;
                }
            }
        }
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
