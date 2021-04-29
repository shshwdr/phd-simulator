using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : Singleton<HUD>
{
    [SerializeField] Transform currenciesTransform;
    // Start is called before the first frame update
    void Start()
    {
        updateCurrencies();
    }

    public void updateCurrencies()
    {
        int i = 0;
        foreach (var cur in JsonManager.Instance.itemCurrencyDict.Keys)
        {
            if (Inventory.Instance.hasItem(cur))
            {
                currenciesTransform.GetChild(i).gameObject.SetActive(true);
                currenciesTransform.GetChild(i).GetComponent<CurrencyRow>().updateCurrencyRow(cur, Inventory.Instance.countItems[cur]);
                i++;
            }
        }
        for (; i < currenciesTransform.childCount; i++)
        {
            currenciesTransform.GetChild(i).gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
