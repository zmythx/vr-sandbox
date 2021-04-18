using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class UIElementShopButton : UIElement
{
    // Start is called before the first frame update
    public Player player;
    public GameObject ItemToSell;
    private PlayerStatManagement psm;
    public GameObject parentShop;
    private Gun itemScript;

    public void SetParentShop(GameObject parentS)
    {
        parentShop = parentS;
    }

    protected override void Start()
    {
        base.Start();
        psm = player.GetComponent<PlayerStatManagement>();
        itemScript = ItemToSell.GetComponent<Gun>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    public override void OnHover()
    {
        base.OnHover();
        if (psm.Gold >= itemScript.ItemCost)
        {
            transform.GetComponent<Renderer>().material.SetColor("_Color", new Color(0.2f, 0.8f, 0.2f));
        }
        else
        {
            transform.GetComponent<Renderer>().material.SetColor("_Color", new Color(0.8f, 0.2f, 0.2f));
        }
        parentShop.SendMessage("Display", itemScript.ItemCost);
    }
    public override void OnExit()
    {
        base.OnExit();
        parentShop.SendMessage("KillDisplay");
    }
    public override void OnClick()
    {
        base.OnClick();
        
        if (psm.Gold > itemScript.ItemCost)
        {
            parentShop.SendMessage("SpawnItem", ItemToSell);
            psm.Gold -= itemScript.ItemCost;
            
        }
    }
}
