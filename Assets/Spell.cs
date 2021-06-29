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
    public GameObject positionReference;
    public float Damage;
    public GameObject owner;
    private Quaternion freezeParticleRot;
    public float ManaCost;
    public bool IsHoming;
    private SphereCollider homingBubble;

    public enum ElementalType
    {
        None,
        Fire,
        Water,
        Earth,
        Air,
        Lightning,
        Holy,
        Unholy,
        Gaia,
        Galaxy,
        Legendary
    }
    public ElementalType EleType;

    public enum SpellType
    {
        Throwable,
        ConeCast,
        Equippable,
        Summon
    }
    public SpellType SpeType;

    public string GetElementalType()
    {
        return EleType.ToString();
    }
    public string GetSpellType()
    {
        return SpeType.ToString();
    }
    public void SetPositionReference(GameObject t)
    {
        positionReference = t;
        Debug.Log("Position Reference set to " + t.name);
    }
    public void SetOwner(GameObject ow)
    {
        Debug.Log("Setting owner to " + ow.name);
        owner = ow;
    }
    void Start()
    {
        hasBeenGrabbed = false;
        if(AssociatedParticles!=null)
        {
            AssociatedParticles.Play(true);
        }
        freezeParticleRot = AssociatedParticles.transform.rotation;
      //  positionReference = new GameObject();
      //  owner = new GameObject();
    }

    // Update is called once per frame
    void Update()
    {
        AssociatedParticles.transform.rotation = freezeParticleRot;
        if(!hasBeenGrabbed)
        {
            transform.position = positionReference.transform.position;
      //      Debug.Log(positionReference.gameObject.name);
            Debug.Log(positionReference.transform.position);
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
