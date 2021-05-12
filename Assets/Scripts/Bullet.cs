using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float Damage = 1;
    private GameObject owner;
    public enum BulletType
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
    public BulletType bulType;
    public void SetOwner(GameObject obje)
    {
        owner = obje;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        //stupid lightning palm cast stop
        
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject == owner)
        {
            Physics.IgnoreCollision(collision, GetComponent<Collider>());
        }
        if (owner != null && owner.tag == "Player")
        {
            owner.SendMessage("StopAllCasting");
        }
        if (collision.gameObject.tag == "Enemy")
        {
            collision.transform.GetComponent<Enemy>().TakeDamage(Damage, bulType.ToString());
            Destroy(transform.gameObject);
        }
        if (!collision.gameObject == owner)
        {
            Destroy(transform.gameObject);
        }

    }
}
