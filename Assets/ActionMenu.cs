using Doozy.Engine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionMenu : MonoBehaviour
{
    [SerializeField] Transform itemsTransform;
    public TMP_Text details;
    public ActionCard card;
    [SerializeField] UIButton actionButton;
    // Start is called before the first frame update
    void Start()
    {
        updateItems();
        GetComponent<UIView>().ShowBehavior.OnStart.Event.AddListener(delegate { updateItems(); });
        actionButton.GetComponent<UIButton>().OnClick.OnTrigger.Event.AddListener(delegate { doAction(); });
        
    }

    void updateItems()
    {
        int i = 0;

        itemsTransform.GetChild(i).GetComponent<SelectableItemCard>().updateCard(JsonManager.Instance.studyActionDict["none"],this);
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
            if (invPair.Value > 0)
            {
                if (JsonManager.Instance.studyActionDict.ContainsKey("paper"))
                {
                    itemsTransform.GetChild(i).gameObject.SetActive(true);
                    var serializedParent = JsonUtility.ToJson(JsonManager.Instance.studyActionDict["paper"]);
                    PaperStudyActionInfo newInfo = JsonUtility.FromJson<PaperStudyActionInfo>(serializedParent);
                    newInfo.paperId = invPair.Key;

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


    public void doAction()
    {
        GetComponent<UIView>().Hide();
        card.doIt();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
