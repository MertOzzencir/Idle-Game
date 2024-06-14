using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AutoClickManager : MonoBehaviour
{
    public static AutoClickManager instance;
    public List<float> autoRoundUp = new List<float>();
    public float autoRoundUpPrice;
    public TextMeshProUGUI quantityText;
    public TextMeshProUGUI AutoBuyerCost;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        autoRoundUpPrice = PlayerPrefs.GetFloat("A");
        AutoBuyerCost.text = PlayerPrefs.GetString("AutoBuyerCost");
        quantityText.text = PlayerPrefs.GetString("quantityText");
    }
    void Update()
    {
        for (int i = 0; i < autoRoundUp.Count; i++)
        {
            if(Time.time - autoRoundUp[i] >= 1.0f)
            {
                autoRoundUp[i] = Time.time;
                AnimalManager.instance.curAnimal.Damage();
            }
        }
        PlayerPrefs.SetFloat("autoRoundUpPrice", autoRoundUpPrice);

    }
    public void OnBuyAutoRoundUp()
    {
        if(GameManager.Instance.Money >= autoRoundUpPrice)
        {
            GameManager.Instance.TakeMoney(autoRoundUpPrice);
            autoRoundUp.Add(Time.time);
            autoRoundUpPrice *= 2.2f;
            quantityText.text = "x " + autoRoundUp.Count.ToString();
            AutoBuyerCost.text = autoRoundUpPrice.ToString("F2") + "$";

        }
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("A", autoRoundUpPrice);
        PlayerPrefs.SetString("AutoBuyerCost", autoRoundUpPrice.ToString("F2") + "$");
        PlayerPrefs.SetString("quantityText", "x " + autoRoundUp.Count.ToString());
        PlayerPrefs.Save();
    }
}
