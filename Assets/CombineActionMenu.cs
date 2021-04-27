using Doozy.Engine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineActionMenu : ActionMenu
{
    [SerializeField] protected Transform ritualItemsTransform;
    ItemRitualInfo ritualInfo;
    ItemElementInfo elementInfo;
    protected override void updateItems()
    {
        int i = 0;

        foreach (var invPair in Inventory.Instance.elements)
        {
            //if (invPair.Value > 0)
            {
                //if (JsonManager.Instance.studyActionDict.ContainsKey("element"))
                {
                    itemsTransform.GetChild(i).gameObject.SetActive(true);
                    //var serializedParent = JsonUtility.ToJson(JsonManager.Instance.studyActionDict["paper"]);
                    //PaperStudyActionInfo newInfo = JsonUtility.FromJson<PaperStudyActionInfo>(serializedParent);
                    //newInfo.paperId = invPair.Key;

                    var serializedParent = JsonUtility.ToJson(JsonManager.Instance.studyActionDict["element"]);
                    ElementStudyActionInfo newInfo = JsonUtility.FromJson<ElementStudyActionInfo>(serializedParent);
                    newInfo.element = invPair.Key;
                    itemsTransform.GetChild(i).GetComponent<SelectableItemCard>().updateCard(JsonManager.Instance.itemElementDict[invPair.Key], this);
                    i++;
                }
            }
        }

        for (; i < itemsTransform.childCount; i++)
        {
            itemsTransform.GetChild(i).gameObject.SetActive(false);
        }

        i = 0;
        foreach (var invPair in Inventory.Instance.rituals)
        {
            //if (invPair.Value > 0)
            {
                //if (JsonManager.Instance.studyActionDict.ContainsKey("element"))
                {
                    itemsTransform.GetChild(i).gameObject.SetActive(true);
                    //var serializedParent = JsonUtility.ToJson(JsonManager.Instance.studyActionDict["paper"]);
                    //PaperStudyActionInfo newInfo = JsonUtility.FromJson<PaperStudyActionInfo>(serializedParent);
                    //newInfo.paperId = invPair.Key;

                    var serializedParent = JsonUtility.ToJson(JsonManager.Instance.studyActionDict["element"]);
                    RitualStudyActionInfo newInfo = JsonUtility.FromJson<RitualStudyActionInfo>(serializedParent);
                    newInfo.ritual = invPair.Key;
                    ritualItemsTransform.GetChild(i).GetComponent<SelectableItemCard>().updateCard(JsonManager.Instance.itemRitualDict[invPair.Key], this);
                    i++;
                }
            }
        }

        for (; i < itemsTransform.childCount; i++)
        {
            ritualItemsTransform.GetChild(i).gameObject.SetActive(false);
        }
    }
    public override void doAction()
    {
        GetComponent<UIView>().Hide();
        //card.doIt();
        //combine!
    }
    public void select(ObjectInfo info)
    {
        if (info is ItemElementInfo)
        {
            elementInfo = (ItemElementInfo)info;
        }
        if (info is ItemRitualInfo)
        {
            ritualInfo = (ItemRitualInfo)info;
        }
        if (elementInfo==null)
        {

            details.text = "please select an element to combine";
        }else if (ritualInfo == null)
        {
            details.text = "please select a ritual to combine";
        }
        else
        {

            details.text = string.Format("combine elemten {0} (level {2}) and ritual {1} (level {3}).", elementInfo.name, ritualInfo.name, elementInfo.level, ritualInfo.level);
        }
    }
}
