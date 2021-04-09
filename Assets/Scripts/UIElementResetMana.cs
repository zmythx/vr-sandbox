using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class UIElementResetMana : UIElement
{
    // Start is called before the first frame update
    public Player player;
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    public override void OnHover()
    {
        base.OnHover();
        transform.GetComponent<Renderer>().material.SetColor("_Color", new Color(0.5f, 0.5f, 0.5f));
    }
    public override void OnClick()
    {
        base.OnClick();
        PlayerStatManagement psm = player.GetComponent<PlayerStatManagement>();
        psm.mana += 10;
        if(psm.mana >= psm.mMana)
        {
            psm.mana = psm.mMana;
        }
    }
}
