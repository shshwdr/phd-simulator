using Doozy.Engine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectableItemCard : MonoBehaviour
{
    ActionMenu parentMenu;
    [SerializeField] UIButton button;
    [SerializeField] TMP_Text nameText;
    ObjectInfo actionInfo;
    public void updateCard(ObjectInfo info, ActionMenu menu)
    {
        actionInfo = info;
        parentMenu = menu;
        nameText.text = info.name;//todo: should find real name in item json
        //updateDetail();
        button.GetComponent<UIButton>().OnClick.OnTrigger.Event.AddListener(delegate { select(); });
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void select()
    {
        if(actionInfo is ActionInfo)
        {
            parentMenu.card.changeSelectedAction(actionInfo as ActionInfo);

        }
        parentMenu.selectedInfo = actionInfo;
        updateDetail();
    }

    public void updateDetail()
    {
        if(parentMenu is CombineActionMenu)
        {
            ((CombineActionMenu)parentMenu).select(actionInfo);
        }
        else if (parentMenu is ShopActionMenu)
        {
            parentMenu.updateDetails();
        }
        else if (actionInfo is ActionInfo)
        {
            parentMenu.details.text = ((ActionInfo)actionInfo).actionName + "\n" + actionInfo.desc;
        }
        else
        {

            parentMenu.details.text = actionInfo.name + "\n" + actionInfo.desc + "\n" + actionInfo.currentValue;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
