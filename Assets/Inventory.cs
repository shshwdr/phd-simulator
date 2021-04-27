using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LevelupInfo {
    public int level;
    public int exp;
    public int totalExp;
    //todo max level
    public int nextExp { get { return levelToExp(level + 1); } }
    public int levelToExp(int x)
    {
        return (int)(Mathf.Pow(x, 3) - 6 * x * x + 17 * x - 12) * 10 / 3;
    }

    public void addExp(int e)
    {
        totalExp += e;
        while (totalExp >= nextExp)
        {
            level += 1;
        }
        exp = totalExp - levelToExp(level);
    }
}
public class Inventory : Singleton<Inventory>
{
    //currency
    public Dictionary<string, int> countItems = new Dictionary<string, int>();
    //paper
    public Dictionary<string, float> toreadPapers = new Dictionary<string, float>();//how much have been read
    //element
    public Dictionary<string, LevelupInfo> elements = new Dictionary<string, LevelupInfo>();
    //ritual
    public Dictionary<string, LevelupInfo> rituals = new Dictionary<string, LevelupInfo>();
    //item
    //paper proposal
    //paper submitted

    public List<string> currencyNames = new List<string>(){ "knowledge","money","reputation",};
//public Dictionary<string, levelupInfo> levelUpItem;
    // Start is called before the first frame update
    void Start()
    {
        //test
        addItem("paper", 1, null);
        ElementStudyActionInfo infoe = new ElementStudyActionInfo();
        infoe.element = "fire";
        addItem("element", 1, infoe);
         RitualStudyActionInfo info = new RitualStudyActionInfo();
        info.ritual = "sword";
        addItem("ritual", 1, info);
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
    public void addItem(string item, int amount,ActionInfo actionInfo)
    {
        if (isCountItem(item))
        {
            addCountItem(item, amount);
        }else if(item == "paper")
        {
            PaperInfo paperInfo = PaperGeneration.Instance.generatePaper();
            toreadPapers[paperInfo.id] = 1;
        }else if(item == "ritualElement")
        {
            PaperInfo paperInfo = PaperGeneration.Instance.paperDict[((PaperStudyActionInfo)actionInfo).paperId];
            var elementId = paperInfo.element;
            var ritualId = paperInfo.ritual;
            string selectItem = "element";
            if (elementId != "none" && ritualId != "none")
            {
                selectItem = Random.value > 0.5f ? selectItem = selectItem : selectItem = "ritual";
            }
            else if(elementId == "none")
            {
                selectItem = "ritual";
            }
            addItem(selectItem, amount, actionInfo);
        }
        else if (item == "element")
        {
            if (actionInfo is PaperStudyActionInfo)
            {
                PaperInfo paperInfo = PaperGeneration.Instance.paperDict[((PaperStudyActionInfo)actionInfo).paperId];
                var elementId = paperInfo.element;
                addExpToItem(elements, elementId, amount);
            }
            else if (actionInfo is ElementStudyActionInfo)
            {
                addExpToItem(elements, ((ElementStudyActionInfo)actionInfo).element, amount);
            }
            else
            {
                Debug.LogError("item type is wrong");
            }
        }
        else if (item == "ritual")
        {
            if (actionInfo is PaperStudyActionInfo)
            {
                PaperInfo paperInfo = PaperGeneration.Instance.paperDict[((PaperStudyActionInfo)actionInfo).paperId];
                var elementId = paperInfo.ritual;
                addExpToItem(rituals, elementId, amount);
            }
            else if (actionInfo is RitualStudyActionInfo)
            {
                addExpToItem(rituals, ((RitualStudyActionInfo)actionInfo).ritual, amount);
            }
            else
            {
                Debug.LogError("item type is wrong");
            }
        }
    }
    public void addExpToItem(Dictionary<string,LevelupInfo> dict, string item, int exp)
    {
        if (!dict.ContainsKey(item))
        {
            dict[item] = new LevelupInfo();
        }
        dict[item].addExp(exp);
        //HUD.Instance.updateCurrencies();
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
