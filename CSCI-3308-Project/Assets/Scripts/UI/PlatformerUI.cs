using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatformerUI : MonoBehaviour
{
    public static PlatformerUI S;

    public Text moneyText;

    private void Start()
    {
        S = this;

        moneyText.text = Player_Data.S.playerMoney.ToString();
    }

    public void UpdateMoneyTextAndAmount(int change)
    {
        Player_Data.S.playerMoney += change;
        moneyText.text = Player_Data.S.playerMoney.ToString();
    }
}
