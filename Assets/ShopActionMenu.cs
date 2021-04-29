using Doozy.Engine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopActionMenu : ActionMenu
{
    protected override void updateItems()
    {
        int i = 0;

        foreach (var invPair in JsonManager.Instance.itemExperimentToolDict)
        {
            //if (invPair.Value > 0)
            {
                //if (JsonManager.Instance.studyActionDict.ContainsKey("element"))
                {
                    itemsTransform.GetChild(i).gameObject.SetActive(true);
                    itemsTransform.GetChild(i).GetComponent<SelectableItemCard>().updateCard(invPair.Value, this);
                    i++;
                }
            }
        }

        for (; i < itemsTransform.childCount; i++)
        {
            itemsTransform.GetChild(i).gameObject.SetActive(false);
        }
    }
    public override void doAction()
    {
        if(selectedInfo == null)
        {
            return;
        }
        Inventory.Instance.tryPurchaseItem(((PurchaseableInfo)selectedInfo));
        itemsTransform.GetChild(0).GetComponent<SelectableItemCard>().updateDetail();
        //GetComponent<UIView>().Hide();
        //card.doIt();
    }

    public override void updateDetails(ObjectInfo info = null)
    {
       
        details.text = selectedInfo.name + "\n" + selectedInfo.desc + "\n" + selectedInfo.currentValue + "\n" + ((PurchaseableInfo)selectedInfo).costDesc;
    }
}
