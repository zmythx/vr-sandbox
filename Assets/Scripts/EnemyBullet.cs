using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
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
        
        if(collision.gameObject == owner)
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
        if(collision.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
        if(!collision.gameObject == owner)
        {
            Destroy(transform.gameObject);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            object[] args = new object[2];
            args[0] = Damage;
            args[1] = bulType.ToString();
            other.gameObject.SendMessageUpwards("TakeDamage", args);
            Destroy(transform.gameObject);
            Debug.Log("Damage?");
        }
        else if (other.gameObject != owner)
        {
            Destroy(transform.gameObject);
        }    
    }
}
