using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Gun : MonoBehaviour
{
    public GameObject Ammo;
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
        if(interactable.attachedToHand != null)
        {
            SteamVR_Input_Sources source = interactable.attachedToHand.handType;
            if(TrigInput[source].axis > FireThreshold && !justFired)
            {
               // HapticsVibe.Execute(0.001f, 0.1f, 4, 0.8f, source);
                justFired = true;
                GameObject bullet = Instantiate(Ammo, Firepoint.transform.position, Firepoint.transform.rotation);
                bullet.GetComponent<Bullet>().SetOwner(gameObject);
                bullet.GetComponent<Bullet>().Damage += Damage;
                bullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * FireForce * 1000);
                stopWatch = 0;

            }
            if(TrigInput[source].axis == 0 || TrigInput[source].axis < FireThreshold)
            {
                justFired = false;
            }
            if(FullyAutomatic && stopWatch > FirePerSec && justFired)
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
        }
    }
}
