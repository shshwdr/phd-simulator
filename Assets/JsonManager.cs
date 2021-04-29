using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[Serializable]
public class ActionResult
{
    public string resultDesc;
    public List<string> resultItem;
    public List<int> count;
    public float prob;
}
[Serializable]
public class ObjectInfo
{
    public string _id;

    public virtual string id { get { return _id; } }
    public string _name;
    public virtual string name { get { return _name; } }
    public string _desc;
    public virtual string desc { get { return _desc; } }

    public virtual string currentValue { get { return ""; } }
}
[Serializable]
public class ItemElementInfo : ObjectInfo
{
    public bool isLocked;
    public int level { get { return Inventory.Instance.elements[id].level; } }
    public override string currentValue { get { var element = Inventory.Instance.elements[id]; 
            return "Level " + element.level+"  EXP "+ element.exp + " / "+ element.nextExp; } }
}
[Serializable]
public class ItemRitualInfo : ObjectInfo
{
    public bool isLocked;
    public int level { get { return Inventory.Instance.rituals[id].level; } }
    public override string currentValue
    {
        get
        {
            var element = Inventory.Instance.rituals[id];
            return "Level: " + element.level + "   EXP: " + element.exp + " / " + element.nextExp;
        }
    }
}
[Serializable]
public class ItemCurrencyInfo : ObjectInfo
{
    public override string currentValue
    {
        get
        {
            var element = Inventory.Instance.countItems[id];
            return "Amount: "+ element;
        }
    }
}
[Serializable]
public class ActionInfo: ObjectInfo
{
    //public string input;
    public string actionName;
    public string doActionDesc;
    public List<ActionResult> result;
    public float timeCost;


}
[Serializable]
public class StudyActionInfo : ActionInfo
{

}
[Serializable]
public class PaperStudyActionInfo : StudyActionInfo
{
    public PaperInfo paperInfo;
    public override string desc { get { return string.Format(_desc, paperInfo.name); } }
    public override string name { get { return paperInfo.name; } }
}

public class ElementStudyActionInfo : StudyActionInfo
{
    public string element;
    public ItemElementInfo ritualInfo { get { return JsonManager.Instance.itemElementDict[element]; } }
    public override string desc { get { return string.Format(_desc, ritualInfo.name); } }
    public override string name { get { return ritualInfo.name; } }
}
public class RitualStudyActionInfo : StudyActionInfo
{
    public string ritual;
    public ItemRitualInfo ritualInfo { get { return JsonManager.Instance.itemRitualDict[ritual]; } }
    public override string desc { get { return string.Format(_desc, ritualInfo.name); } }
    public override string name { get { return ritualInfo.name; } }
}

[Serializable]
public class AllActionInfo
{
    public List<StudyActionInfo> studyActions;
    //public List<StudyActionInfo> studyActions;
}
public class AllItemInfo
{
    public List<ItemCurrencyInfo> currencies;
    public List<ItemElementInfo> elements;
    public List<ItemRitualInfo> rituals;

    //public List<StudyActionInfo> studyActions;
}
public class JsonManager : Singleton<JsonManager>
{
    public Dictionary<string, StudyActionInfo> studyActionDict;
    public Dictionary<string, ItemCurrencyInfo> itemCurrencyDict;
    public Dictionary<string, ItemElementInfo> itemElementDict;
    public Dictionary<string, ItemRitualInfo> itemRitualDict;

    void Awake()
    {
        //actions
        string text = Resources.Load<TextAsset>("json/actions").text;
        AllActionInfo allActionInfoList = JsonUtility.FromJson<AllActionInfo>(text);
        studyActionDict = allActionInfoList.studyActions.ToDictionary(x => x.id, x => x);
        //items

        text = Resources.Load<TextAsset>("json/items").text;
        AllItemInfo allItemInfoList = JsonUtility.FromJson<AllItemInfo>(text);
        itemCurrencyDict = allItemInfoList.currencies.ToDictionary(x => x.id, x => x);
        itemElementDict = allItemInfoList.elements.ToDictionary(x => x.id, x => x);
        itemRitualDict = allItemInfoList.rituals.ToDictionary(x => x.id, x => x);
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
