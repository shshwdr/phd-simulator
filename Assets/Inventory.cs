using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class levelupInfo {
    public int level;
    public int exp;
}
public class Inventory : Singleton<Inventory>
{
    //currency
    public Dictionary<string, int> countItems = new Dictionary<string, int>();
    //paper
    public Dictionary<string, float> toreadPapers = new Dictionary<string, float>();//how much have been read
    //element
    //item
    //paper proposal
    //paper submitted

    public List<string> currencyNames = new List<string>(){ "knowledge","money","reputation",};
public Dictionary<string, levelupInfo> levelUpItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool isCurrency(string item)
    {
        foreach(var c in currencyNames)
        {
            if (item == c)
            {
                return true;
            }
        }
        return false;
    }
    public bool isCountItem(string item)
    {
        return isCurrency(item);
    }
    public void addItem(string item, int amount)
    {
        if (isCountItem(item))
        {
            addCountItem(item, amount);
        }else if(item == "paper")
        {
            PaperInfo paperInfo = PaperGeneration.Instance.generatePaper();
            toreadPapers[paperInfo.id] = 1;
        }
    }
    public void addCountItem(string item, int amount)
    {
        if (!countItems.ContainsKey(item))
        {
            countItems[item] = 0;
        }
        countItems[item] += amount;
        HUD.Instance. updateCurrencies();
    }

    public bool canConsumeCountItem(string item, int amount)
    {
        if (!countItems.ContainsKey(item))
        {
            return false;
        }
        return countItems[item] >= amount;
    }

    public bool hasItem(string item)
    {
        return countItems.ContainsKey(item);
    }

    public void consumeCountItem(string item, int amount)
    {
        if (!canConsumeCountItem(item, amount))
        {
            Debug.LogError(item + " does not have " + amount);
            return;
        }
        if (!countItems.ContainsKey(item))
        {
            countItems[item] = 0;
        }
        countItems[item] -= amount;
        HUD.Instance.updateCurrencies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
