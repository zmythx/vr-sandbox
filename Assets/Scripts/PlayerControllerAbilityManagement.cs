using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerControllerAbilityManagement : MonoBehaviour
{
    // Start is called before the first frame update
    public SteamVR_Action_Boolean triggerTouch;
    public SteamVR_Action_Boolean abilityTouch;
    public SteamVR_Action_Boolean actionCast;
    public SteamVR_Action_Boolean spellTouch;

    public Player player;
    private PlayerStatManagement psm;
    private PlayerFingerManagement pfm;
    private PlayerHandManagement phm;

    public Hand rightHand;
    public Hand leftHand;


    public GameObject LeftEquipSpell;
    public GameObject RightEquipSpell;

    //Expectation for both hands is: black forward, white behind, clockwise from 12: green, yellow, red, blue

    private bool readyToCast = false;
    public bool isCasting = false;

    private Vector3 spellHandAdjustmentVector;

    public void AttachSpellToHand(GameObject spell)
    {
        spell.SetActive(true);
        spell.transform.position = rightHand.gameObject.transform.position + -rightHand.gameObject.transform.right * 0.01f + -rightHand.gameObject.transform.forward * 0.01f + -rightHand.gameObject.transform.up * 0.01f;
    }
    private void Start()
    {
        psm = player.GetComponent<PlayerStatManagement>();
        pfm = player.GetComponent<PlayerFingerManagement>();
        phm = player.GetComponent<PlayerHandManagement>();

        rightHand = player.rightHand;
        leftHand = player.leftHand;

        psm.mana = 100;

        #region Debug shit
        //isChargingLightningPalm = true;
        //lightningPalmInitialFlag = true;
        //isCasting = true;

        /*   greenCube = Instantiate(DebugCube);
           redCube = Instantiate(DebugCube);
           blueCube = Instantiate(DebugCube);
           yellowCube = Instantiate(DebugCube);
           whiteCube = Instantiate(DebugCube);
           blackCube = Instantiate(DebugCube);

           greenCube.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
           redCube.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
           blueCube.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
           yellowCube.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
           whiteCube.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
           blackCube.GetComponent<Renderer>().material.SetColor("_Color", Color.black); */
        #endregion

        spellHandAdjustmentVector = -rightHand.gameObject.transform.right * 0.01f + -rightHand.gameObject.transform.forward * 0.01f + -rightHand.gameObject.transform.up * 0.01f; 
    }
    // Update is called once per frame
    void Update()
    {
        rightHand = player.rightHand;
        leftHand = player.leftHand;
        bool rightState = spellTouch.GetState(SteamVR_Input_Sources.RightHand);
        bool leftState = spellTouch.GetState(SteamVR_Input_Sources.LeftHand);

        if(rightState && spellTouch.changed)
        {
            if (RightEquipSpell.GetComponent<Spell>().ManaCost <= psm.mana)
            {
                if (RightEquipSpell.GetComponent<Spell>().GetSpellType() == "Throwable")
                {
                    GameObject fireSpell = Instantiate(RightEquipSpell);
                    Debug.Log("Setting owner to " + transform.gameObject.name);
                    fireSpell.GetComponent<Spell>().SetOwner(transform.gameObject);
                    Debug.Log("Setting position ref to " + rightHand.gameObject.name);
                    fireSpell.GetComponent<Spell>().SetPositionReference(rightHand.gameObject);
                }
                psm.mana -= RightEquipSpell.GetComponent<Spell>().ManaCost;
            }
            
        }
        if (leftState && spellTouch.changed)
        {
            if (LeftEquipSpell.GetComponent<Spell>().ManaCost <= psm.mana)
            {
                if (LeftEquipSpell.GetComponent<Spell>().GetSpellType() == "Throwable")
                {
                    GameObject fireSpell = Instantiate(LeftEquipSpell);
                    Debug.Log("Setting owner to " + transform.gameObject.name);
                    fireSpell.GetComponent<Spell>().SetOwner(transform.gameObject);
                    Debug.Log("Setting position ref to " + leftHand.gameObject.name);
                    fireSpell.GetComponent<Spell>().SetPositionReference(leftHand.gameObject);
                }
                psm.mana -= LeftEquipSpell.GetComponent<Spell>().ManaCost;
            }
        }
        if (triggerTouch.state)
        {

            Transform handPos = rightHand.gameObject.transform;
        }
        if (abilityTouch.stateDown)
        {
            readyToCast = !readyToCast;
        }

        #region shit old code
        /*
        if (readyToCast)
        {
            readyToCast = false;
            if (isCasting)
            {
                StopAllCasting();
            }
            else if (!isCasting && psm.chargeState == 0)
            {
                if (!isCastingLightningPalm && pfm.RCurrentState == "Openpalm" && phm.GetHandState() == "RightAboveLeft")
                {
                    lightningPalmInitialFlag = true;
                    lightningCharge = 0;
                    isChargingLightningPalm = true;
                    isCasting = true;
                }
                if (!isCastingFirePalm && pfm.RCurrentState == "Firecast")
                {
                    isCastingFirePalm = true;
                    isCasting = true;
                }

            }

        }
        if(lightningPalmInitialFlag)
        {

            lightningPalmInitialFlag = false;
            lightningAbilityInstance.transform.localScale = lightningPalmOriginalSize * 0.5f;

        }
        if (isChargingLightningPalm && phm.GetHandState() == "RightAboveLeft" && psm.mana > 0 && lightningCharge < maxLightningCharge)
        {
            AttachSpellToHand(lightningAbilityInstance.gameObject);
            lightningAbilityInstance.transform.localScale *= 1.001f;
            lightningCharge += 0.1f;
            psm.mana -= 0.05f;
        }
        if (isChargingLightningPalm && phm.GetHandState() != "RightAboveLeft" || isChargingLightningPalm && lightningCharge >= maxLightningCharge)
        {
            AttachSpellToHand(lightningAbilityInstance.gameObject);
            isChargingLightningPalm = false;
            isCastingLightningPalm = true;
            LightningPalmHitboxRef = Instantiate(LightningPalmHitbox);
            LightningPalmHitboxRef.GetComponent<Bullet>().Damage = 100 + (lightningCharge * 10);
            LightningPalmHitboxRef.GetComponent<Bullet>().SetOwner(transform.gameObject);
            LightningPalmHitboxRef.transform.position = rightHand.transform.position;
        }

        if(isCastingLightningPalm && psm.mana > 0)
        {
            AttachSpellToHand(lightningAbilityInstance.gameObject);
            LightningPalmHitboxRef.transform.position = rightHand.transform.position;
            psm.mana-=0.02f;
        }
        if (isCastingFirePalm && psm.mana > 0)
        {
            AttachSpellToHand(fireAbilityInstance.gameObject);
            fireAbilityInstance.transform.LookAt(rightHand.gameObject.transform.position + -rightHand.gameObject.transform.forward * 2 + -rightHand.gameObject.transform.up * 2);
            psm.mana -= 0.01f;
            FireBallElapsedTime += Time.deltaTime / FireballCastRate;
            if (FireBallElapsedTime > FireballCastRate)
            {
                FireBallElapsedTime = 0;
                GameObject newProj = Instantiate(FireballHitbox, rightHand.gameObject.transform.position, rightHand.gameObject.transform.rotation);
               // newProj.transform.LookAt(rightHand.gameObject.transform.position + -rightHand.gameObject.transform.forward * 2 + -rightHand.gameObject.transform.up * 2);
                newProj.GetComponent<Rigidbody>().AddForce(phm.RHandRightVector(rightHand, 1800));

            }
        }
        */
        #endregion

        if (psm.mana <= 0)
        {
            StopAllCasting();
        }
    }
    private void StopAllCasting()
    {
        isCasting = false;
    }
}
