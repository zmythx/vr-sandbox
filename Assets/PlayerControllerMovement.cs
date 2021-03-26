using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerControllerMovement : MonoBehaviour
{
    public SteamVR_Action_Vector2 input;
    public SteamVR_Action_Boolean sprinput;
    public bool isSprint = false;
    public bool isMoving = false;
    private bool isChargingLightning = false;
    private PlayerStateManagement psm;
    public float speed = 2;
    public float gravity = 9.81f;
    public float xaxismove = 0;
    public float yaxismove = 0;
    Vector3 directionlock;

    private CharacterController characterController;
    // Update is called once per frame
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        psm = GetComponent<PlayerStateManagement>();
    }
    void Update()
    {
        if(sprinput.changed && sprinput.state)
        {
            if(isSprint)
            {
                speed = 2;
                isSprint = false;
            }
            else
            {
                speed = 3;
                isSprint = true;
            }    
        }
        xaxismove = input.axis.x;
        yaxismove = input.axis.y;
        if(psm.IsStunned())
        {
            xaxismove = 0;
            yaxismove = 0;
        }
        Debug.Log("X axis: " + xaxismove + " Y axis: " + yaxismove);
        if(psm.IsCastingLightningPalm() && !isChargingLightning && (xaxismove != 0 || yaxismove != 0))
        {
            isChargingLightning = true;
            speed *= psm.AbilityLightningPalmSpeedMultiplier();
            directionlock = Player.instance.hmdTransform.TransformDirection(new Vector3(0, 0, 1));

        }
        if(!psm.IsCastingLightningPalm() && isChargingLightning)
        {
            isChargingLightning = false;
            speed /= psm.AbilityLightningPalmSpeedMultiplier();
        }
        if(isChargingLightning)
        {
            characterController.Move(speed * Time.deltaTime * Vector3.ProjectOnPlane(directionlock, Vector3.up) - new Vector3(0, gravity, 0) * Time.deltaTime);
        }
        else
        {
            Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(xaxismove, 0, yaxismove));
            characterController.Move(speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up) - new Vector3(0, gravity, 0) * Time.deltaTime);
        }
        if (xaxismove != 0 || yaxismove != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        
        
    }
}
