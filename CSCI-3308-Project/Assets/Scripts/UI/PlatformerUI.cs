using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatformerUI : MonoBehaviour
{
    public static PlatformerUI S;

    public Text moneyText;
    private float fill;

    [SerializeField] private Image healthBar;
    [SerializeField] GameObject player;

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

    private void Update()
    {
        player_health stats = player.GetComponent<player_health>();
        fill = stats.health / stats.originalHealth;
        healthBar.fillAmount = fill;
    }
}
