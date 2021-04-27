using Doozy.Engine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionMenu : MonoBehaviour
{
    [SerializeField] protected Transform itemsTransform;
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

    protected virtual void updateItems() { }

    public virtual void doAction()
    {
        GetComponent<UIView>().Hide();
        card.doIt();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
