using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyActionMenu : ActionMenu
{
    protected override void updateItems()
    {
        int i = 0;

        itemsTransform.GetChild(i).GetComponent<SelectableItemCard>().updateCard(JsonManager.Instance.studyActionDict["none"], this);
        i++;
        foreach (var invPair in Inventory.Instance.countItems)
        {
            if (invPair.Value > 0)
            {
                if (JsonManager.Instance.studyActionDict.ContainsKey(invPair.Key))
                {

                    itemsTransform.GetChild(i).gameObject.SetActive(true);
                    itemsTransform.GetChild(i).GetComponent<SelectableItemCard>().updateCard(JsonManager.Instance.studyActionDict[invPair.Key], this);
                    i++;
                }
            }
        }
        foreach (var invPair in Inventory.Instance.toreadPapers)
        {
           // if (invPair.Value > 0)
            {
                if (JsonManager.Instance.studyActionDict.ContainsKey("paper"))
                {
                    itemsTransform.GetChild(i).gameObject.SetActive(true);
                    var serializedParent = JsonUtility.ToJson(JsonManager.Instance.studyActionDict["paper"]);
                    PaperStudyActionInfo newInfo = JsonUtility.FromJson<PaperStudyActionInfo>(serializedParent);
                    newInfo.paperInfo = invPair.Value;

                    itemsTransform.GetChild(i).GetComponent<SelectableItemCard>().updateCard(newInfo, this);
                    i++;
                }
            }
        }
        foreach (var invPair in Inventory.Instance.elements)
        {
            //if (invPair.Value > 0)
            {
                if (JsonManager.Instance.studyActionDict.ContainsKey("element"))
                {
                    itemsTransform.GetChild(i).gameObject.SetActive(true);
                    //var serializedParent = JsonUtility.ToJson(JsonManager.Instance.studyActionDict["paper"]);
                    //PaperStudyActionInfo newInfo = JsonUtility.FromJson<PaperStudyActionInfo>(serializedParent);
                    //newInfo.paperId = invPair.Key;

                    var serializedParent = JsonUtility.ToJson(JsonManager.Instance.studyActionDict["element"]);
                    ElementStudyActionInfo newInfo = JsonUtility.FromJson<ElementStudyActionInfo>(serializedParent);
                    newInfo.element = invPair.Key;
                    itemsTransform.GetChild(i).GetComponent<SelectableItemCard>().updateCard(newInfo, this);
                    i++;
                }
            }
        }
        foreach (var invPair in Inventory.Instance.rituals)
        {
            //if (invPair.Value > 0)
            {
                if (JsonManager.Instance.studyActionDict.ContainsKey("ritual"))
                {
                    itemsTransform.GetChild(i).gameObject.SetActive(true);
                    //var serializedParent = JsonUtility.ToJson(JsonManager.Instance.studyActionDict["paper"]);
                    //PaperStudyActionInfo newInfo = JsonUtility.FromJson<PaperStudyActionInfo>(serializedParent);
                    //newInfo.paperId = invPair.Key;

                    var serializedParent = JsonUtility.ToJson(JsonManager.Instance.studyActionDict["ritual"]);
                    RitualStudyActionInfo newInfo = JsonUtility.FromJson<RitualStudyActionInfo>(serializedParent);
                    newInfo.ritual = invPair.Key;
                    itemsTransform.GetChild(i).GetComponent<SelectableItemCard>().updateCard(newInfo, this);
                    i++;
                }
            }
        }
        for (; i < itemsTransform.childCount; i++)
        {
            itemsTransform.GetChild(i).gameObject.SetActive(false);
        }
    }

}
