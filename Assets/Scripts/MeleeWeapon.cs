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
        int layerMask = 1 << 14;
        if(collision.gameObject.layer == layerMask)
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            collision.transform.GetComponent<Enemy>().TakeDamage(Damage);
        }
        
    }
}
