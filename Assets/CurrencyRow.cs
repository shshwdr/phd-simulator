using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
public class CurrencyRow : MonoBehaviour
{
    [SerializeField] TMP_Text currencyName;
    [SerializeField] TMP_Text currencyAmount;

    public void updateCurrencyRow(string name, int amount)
    {
        currencyName.text = name;
        currencyAmount.text = amount.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
