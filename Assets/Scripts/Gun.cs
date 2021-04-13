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

    public Mesh UnloadedMesh;
    public Mesh LoadedMesh;

    public bool ClipLoaded;
    public SteamVR_Action_Boolean UnloadButton;

    public string AmmoType;

    private Interactable interactable;

    public bool FullyAutomatic;

    private bool justFired;

    private GameObject rightHand;
    private GameObject leftHand;

    // Start is called before the first frame update
    void Start()
    {
        CurrentAmmo = MaxAmmo;
        justFired = false;
        interactable = transform.GetComponent<Interactable>();
        rightHand = GameObject.Find("RightHand");
        leftHand = GameObject.Find("LeftHand");
    }

    // Update is called once per frame
    void Update()
    {
        //held in left
        TrigInput.GetAxis(SteamVR_Input_Sources.LeftHand);
        if(TrigInput.GetAxis(SteamVR_Input_Sources.LeftHand) > FireThreshold && !justFired && interactable.attachedToHand != null)
        {
            justFired = true;
            GameObject bullet = Instantiate(Ammo, Firepoint.transform.position, Firepoint.transform.rotation);
            bullet.GetComponent<Bullet>().Damage += Damage;
            bullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * FireForce * 1000);
        }
        if (TrigInput.GetAxis(SteamVR_Input_Sources.RightHand) > FireThreshold && !justFired && interactable.attachedToHand != null)
        {
            justFired = true;
            GameObject bullet = Instantiate(Ammo, Firepoint.transform.position, Firepoint.transform.rotation);
            bullet.GetComponent<Bullet>().Damage += Damage;
            bullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * FireForce * 1000);
        }
        if ((TrigInput.GetAxis(SteamVR_Input_Sources.RightHand) == 0 || TrigInput.GetAxis(SteamVR_Input_Sources.RightHand) < FireThreshold) && interactable.attachedToHand != null)
        {
            justFired = false;
        }
        if ((TrigInput.GetAxis(SteamVR_Input_Sources.LeftHand) == 0 || TrigInput.GetAxis(SteamVR_Input_Sources.LeftHand) < FireThreshold) && interactable.attachedToHand != null)
        {
            justFired = false;
        }
    }
}
