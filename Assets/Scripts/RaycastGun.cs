﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class RaycastGun : MonoBehaviour
{
    //public GameObject Ammo;
    public GameObject Firepoint;
    public float Damage;
    public float FireThreshold;
    public SteamVR_Action_Single TrigInput;
    public int MaxAmmo;
    public int CurrentAmmo;
    public float RecoilAmount;
    public float FireRate;
    public float FireForce;
    private float FirePerSec;
    private float stopWatch;

    public GameObject Grip;

    public Mesh UnloadedMesh;
    public Mesh LoadedMesh;

    public bool ClipLoaded;
    public SteamVR_Action_Boolean UnloadButton;
    public SteamVR_Action_Vibration HapticsVibe;

    public string AmmoType;
    public bool OverridesAmmoType;

    private Interactable interactable;

    public bool FullyAutomatic;

    private bool justFired;
    public string ItemDescription;
    public int ItemCost;

    public GameObject lineRef;
    string bulType = "null";

    string State = "";
    float t;

    Quaternion fireRotation;
    Vector3 firePosition;
    Quaternion newRot;
    Vector3 newPos;

    // Start is called before the first frame update
    void Start()
    {
        
        CurrentAmmo = MaxAmmo;
        justFired = false;
        interactable = transform.GetComponent<Interactable>();
        if (FireRate > 0)
        {
            FirePerSec = 1f / FireRate;
        }
        else
        {
            FirePerSec = 0;
        }
        stopWatch = 0;

    }
    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (interactable.attachedToHand != null)
        {
            SteamVR_Input_Sources source = interactable.attachedToHand.handType;
            if (TrigInput[source].axis > FireThreshold && !justFired)
            {
                // HapticsVibe.Execute(0.001f, 0.1f, 4, 0.8f, source);
                justFired = true;
                RaycastHit hit;
                LineRenderer line = lineRef.GetComponent<LineRenderer>();
                line.SetPosition(0, Firepoint.transform.position);
                line.SetPosition(1, Firepoint.transform.position + Firepoint.transform.forward * 10 * FireForce);
                Instantiate(lineRef);
                
                if (Physics.Raycast(Firepoint.transform.position, Firepoint.transform.TransformDirection(Vector3.forward), out hit, FireForce * 10))
                {
                    if (hit.transform.tag == "Enemy")
                    {
                        hit.transform.GetComponent<Enemy>().TakeDamage(Damage, bulType.ToString());
                    }
                }
                if (State != "recoil")
                {
                    State = "recoil";
                    t = 0;
                    firePosition = transform.localPosition;
                    fireRotation = transform.localRotation;
                    transform.Rotate(0, RecoilAmount * 10, 0);
                    transform.localPosition.Set(transform.localPosition.x, transform.localPosition.y + 0.1f, transform.localPosition.z - 0.3f);
                    newRot = transform.localRotation;
                    newPos = transform.localPosition;
                }
                else
                {
                    t = 0;
                    transform.Rotate(0, RecoilAmount * 10, 0);
                    transform.localPosition.Set(transform.localPosition.x, transform.localPosition.y + 0.1f, transform.localPosition.z - 0.3f);
                    newRot = transform.localRotation;
                    newPos = transform.localPosition;
                }
                stopWatch = 0;

            }
            if (TrigInput[source].axis == 0 || TrigInput[source].axis < FireThreshold)
            {
                justFired = false;
            }
            if (FullyAutomatic && stopWatch > FirePerSec && justFired)
            {
                justFired = false;
            }
            if (FullyAutomatic)
            {
                stopWatch += Time.deltaTime;
            }
            if (Grip != null && Grip.GetComponent<Interactable>() != null)
            {
                Grip.GetComponent<Interactable>().enabled = true;
            }
            if (State == "recoil")
            {
                if (t < 1)
                {
                    transform.localPosition = Vector3.Lerp(newPos, firePosition, t);
                    transform.localRotation = Quaternion.Lerp(newRot, fireRotation, t);
                }
                if(t >= 1)
                {
                    transform.localPosition = firePosition;
                    transform.localRotation = fireRotation;
                    State = "";
                    t = 0;
                }

            }
        }
    }

}