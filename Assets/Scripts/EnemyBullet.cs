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
            collision.transform.GetComponent<PlayerStatManagement>().TakeDamage(Damage, bulType.ToString());
            Destroy(transform.gameObject);
        }
        if(!collision.gameObject == owner)
        {
            Destroy(transform.gameObject);
        }
        
    }
}
