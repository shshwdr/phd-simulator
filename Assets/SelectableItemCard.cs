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
    ActionInfo actionInfo;
    public void updateCard(ActionInfo info, ActionMenu menu)
    {
        actionInfo = info;
        parentMenu = menu;
        nameText.text = info.input;//todo: should find real name in item json
        parentMenu.details.text = actionInfo.actionName + "\n" + actionInfo.desc;
        button.GetComponent<UIButton>().OnClick.OnTrigger.Event.AddListener(delegate { select(); });
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void select()
    {
        parentMenu.card.changeSelectedAction(actionInfo);
        parentMenu.details.text = actionInfo.actionName + "\n" + actionInfo.desc;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
