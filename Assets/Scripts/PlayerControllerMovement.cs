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
    public SteamVR_Action_Boolean jumpinput;
    public bool isSprint = false;
    public bool isMoving = false;
    public bool isIgnoringGravity = false;
    private bool isJumping = false;
    private bool isChargingLightning = false;
    public Collider playerCollider;
    private PlayerStateManagement psm;
    private Rigidbody rigbod;
    public float speed = 2;
    public float walkSpeed = 3;
    public float sprintSpeed = 5;
    public float gravityconst = 9.81f;
    public float downwardMomementum = 0f;
    public float maxDownwardMomentum = 100f;
    public float xaxismove = 0;
    public float yaxismove = 0;
    public float jumpForce = 5;
    Vector3 directionlock;
  //  private float mTimeThresholdSinceLastJump = 0.5f;
  //  private bool passedTimeThreshold = true;
  //  private float timeThresholdSinceLastJump = 0;

    bool isTouchingKillzone()
    {
        int layermask = 1 << 15;
        return Physics.Raycast(transform.position, -Vector3.up, playerCollider.bounds.extents.y + 0.1f, layermask);
    }
    bool isGrounded()
    {
        int layermask = 1 << 15;
        layermask = ~layermask;
        return Physics.Raycast(transform.position, -Vector3.up, playerCollider.bounds.extents.y + 0.1f, layermask);
    }
    bool isCollidingWithGround()
    {
        int layermask = 1 << 15;
        layermask = ~layermask;
        if(isGrounded() && Physics.Raycast(transform.position, -Vector3.up, playerCollider.bounds.extents.y + 0.095f, layermask))
        {
            return true;
        }
        return false;
    }
    private CharacterController characterController;
    // Update is called once per frame
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        psm = GetComponent<PlayerStateManagement>();
        rigbod = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if(isTouchingKillzone())
        {
            characterController.enabled = false;
            characterController.transform.position = new Vector3(0, 10, 0);
            characterController.enabled = true;
        }
        characterController.enabled = false;
        while (isCollidingWithGround())
        {
            
      //      Debug.Log("Pushing Up...");
            characterController.transform.position = new Vector3(transform.position.x, transform.position.y + 0.005f, transform.position.z);
        //    Debug.Log(transform.position);
        }
        characterController.enabled = true;
        //  Debug.Log("Made it!");

        if (isGrounded())
        {
            isJumping = false;
        }
        if ((isGrounded() || isIgnoringGravity))
        {
            downwardMomementum = 0;
        }
        else
        {
            downwardMomementum += gravityconst * Time.deltaTime;
        }
        if(downwardMomementum > maxDownwardMomentum)
        {
            downwardMomementum = maxDownwardMomentum;
        }
        if (jumpinput.changed && jumpinput.state && isGrounded())
        {
            isJumping = true;
            downwardMomementum -= jumpForce;
        }

        if(sprinput.changed && sprinput.state)
        {
            if(isSprint)
            {
                speed = walkSpeed;
                isSprint = false;
            }
            else
            {
                speed = sprintSpeed;
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
      //  Debug.Log("X axis: " + xaxismove + " Y axis: " + yaxismove);
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
            characterController.Move(speed * Time.deltaTime * Vector3.ProjectOnPlane(directionlock, Vector3.up) - new Vector3(0, downwardMomementum, 0) * Time.deltaTime);
        }
        else
        {
            Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(xaxismove, 0, yaxismove));
            characterController.Move(speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up) - new Vector3(0, downwardMomementum, 0) * Time.deltaTime);
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
