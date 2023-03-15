using TMPro;
using UnityEngine;
public class CityMoneyManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyText;
    private int money, sceneMoney;
    [SerializeField] private PlayerMove _player;
    private const string MONEY_KEY = "CityMoney";

    private void Start()
    {
        _player.AddMoney += AddMoney;
        _player.Finish += Finish;
        
        if (PlayerPrefs.HasKey(MONEY_KEY) == false) 
            PlayerPrefs.SetInt(MONEY_KEY, 0);

        money = PlayerPrefs.GetInt(MONEY_KEY);
    }

    private void AddMoney()
    {
        money++;
        sceneMoney++;
    }

    private void Finish(bool finish)
    {
        if(finish == false) return;
        
        _moneyText.text = sceneMoney.ToString();
        PlayerPrefs.SetInt(MONEY_KEY, money);
    }
}
