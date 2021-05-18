using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Spell : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem AssociatedParticles;
    private bool hasBeenGrabbed = false;
    private GameObject positionReference;
    public float Damage;
    private GameObject owner;
    
    public void SetPositionReference(GameObject t)
    {
        positionReference = t;
    }
    public void SetOwner(GameObject ow)
    {
        owner = ow;
    }
    void Start()
    {
        if(AssociatedParticles!=null)
        {
            AssociatedParticles.Play(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasBeenGrabbed)
        {
            transform.position = positionReference.transform.position;
            if(transform.GetComponent<Interactable>().attachedToHand != null)
            {
                hasBeenGrabbed = true;
            }
        }

    }
    private void OnTriggerEnter(Collider collision)
    {
        if (hasBeenGrabbed)
        {
            if (collision.gameObject == owner)
            {
                Physics.IgnoreCollision(collision, GetComponent<Collider>());
            }
            if (owner != null && owner.tag == "Player")
            {
                owner.SendMessage("StopAllCasting");
            }
            if (collision.gameObject.tag == "Enemy")
            {
                collision.transform.GetComponent<Enemy>().TakeDamage(Damage, "Fire");
                Destroy(transform.gameObject);
            }
            if (!collision.gameObject == owner)
            {
                Destroy(transform.gameObject);
            }
        }
    }

}
