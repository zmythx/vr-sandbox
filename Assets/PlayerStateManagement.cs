using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerStateManagement : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerControllerAbilityManagement pcam;
    private PlayerControllerMovement pcm;
    private PlayerFingerManagement pfm;
    private PlayerHandManagement phm;
    private PlayerStatManagement psm;
    public Player player;

    void Start()
    {
        pcam = player.GetComponent<PlayerControllerAbilityManagement>();
        pcm = player.GetComponent<PlayerControllerMovement>();
        pfm = player.GetComponent<PlayerFingerManagement>();
        phm = player.GetComponent<PlayerHandManagement>();
        psm = player.GetComponent<PlayerStatManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        StatUpdate();
    }
    public bool IsCasting()
    {
        return pcam.isCasting;
    }
    public bool IsCastingLightningPalm()
    {
        return pcam.isCastingLightningPalm;
    }
    public bool IsMoving()
    {
        return pcm.isMoving;
    }
    public bool IsStunned()
    {
        return (pcam.isChargingLightningPalm || psm.chargeState > 0);
    }
    public void DecreaseHealth(float damage)
    {
        psm.health -= damage;
        StatUpdate();
    }
    public void IncreaseHealth(float health)
    {
        psm.health += health;
        StatUpdate();
    }
    public void IncreaseMana(float health)
    {
        psm.mana += health;
        StatUpdate();
    }
    public void DecreaseMana(float dec)
    {
        psm.mana -= dec;
        StatUpdate();
    }

    public void StatUpdate()
    {
        if(psm.health >= psm.mHealth)
        {
            psm.health = psm.mHealth;
        }
        if(psm.mana >= psm.mMana)
        {
            psm.mana = psm.mMana;
        }
        if(psm.health <= 0)
        {
            Die();
        }
        if(psm.mana < 0)
        {
            psm.mana = 0;
        }
    }
    public float AbilityLightningPalmSpeedMultiplier()
    {
        return pcam.lightningPalmChargeMultiplier;
    }
    public void Die()
    {

    }

    
}
