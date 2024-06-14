using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    
    public float Money;

    public TextMeshProUGUI MoneyText;

    private void Start()
    {
        Money = PlayerPrefs.GetFloat("money");
        MoneyText.text = PlayerPrefs.GetString("MoneyText");
    }
    
    public void AddMoney(int money)
    {
        Money += money;
        MoneyText.text = Money.ToString("F2") + "$";
    }
    public void TakeMoney(float money)
    {
        Money -= money;
        MoneyText.text = Money.ToString("F2") + "$";
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("money", Money);
        PlayerPrefs.SetString("MoneyText",Money.ToString());
    }




    private void Awake()
    {
        Instance = this;
    }
}
