using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.UI;

public class PlayerStatManagement : MonoBehaviour
{
    // Start is called before the first frame update
    public Player player;
    public GameObject chargingParticle;
    private PlayerFingerManagement pfm;
    private PlayerHandManagement phm;
    private PlayerStateManagement psm;
    public float health = 0;
    public float mana = 0;
    public float manaRegen = 0;
    public float healthRegen = 0;
    public float mHealth;
    public float mMana = 0;

    public int chargeState = 0;


    public Text gameText;

    void Start()
    {
        pfm = player.GetComponent<PlayerFingerManagement>();
        phm = player.GetComponent<PlayerHandManagement>();
        psm = player.GetComponent<PlayerStateManagement>();
        health = 100;
        mana = 100;
        mHealth = health;
        mMana = 100;

    }

    // Update is called once per frame
    void Update()
    {
        chargeState = 0;
        if (pfm.RCurrentState == "Ninja")
        {
            chargeState += 1;
        }
        if (pfm.LCurrentState == "Ninja")
        {
            chargeState += 1;
        }
        if(chargeState == 2 && phm.GetHandState() == "RightBelowLeft")
        {
            chargeState += 1;
        }
        if (chargeState > 0 && !psm.IsCasting())
        {
            if (chargeState == 3 && mMana > mana)
            {
                chargingParticle.SetActive(true);
                chargingParticle.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 2, player.transform.position.z);
                psm.IncreaseMana(manaRegen * 6);

            }
            if (chargeState == 2 && mMana > mana)
            {
                chargingParticle.SetActive(true);
                chargingParticle.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 2, player.transform.position.z);
                psm.IncreaseMana(manaRegen * 3);

            }
            if (chargeState == 1 && mMana > mana)
            {
                chargingParticle.SetActive(true);
                chargingParticle.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 2, player.transform.position.z);
                psm.IncreaseMana(manaRegen);
            }
        }
        if (chargeState == 0)
        {
            chargingParticle.SetActive(false);
        }
        gameText.text = string.Format("Health: {0}/{1} Mana: {2}/{3}", health, mHealth, mana, mMana);
    }
}
