using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public float Damage = 1;
    public enum WeaponType
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
    public MeshCollider Hitbox;
    public WeaponType WepType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("On Collision Enter!");
        if(collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Found enemy through collision!");
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("On Trigger Enter!");
        if(other.tag == "Enemy")
        {
            Debug.Log("Found enemy through trigger!");
            other.transform.GetComponent<Enemy>().TakeDamage(Damage);
        }
    }
}
