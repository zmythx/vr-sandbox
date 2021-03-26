using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float Damage = 1;
    private GameObject owner;
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
        collision.transform.GetComponent<Enemy>().TakeDamage(Damage);
        if(owner != null)
        {
            owner.SendMessage("StopAllCasting");
        }
        Destroy(transform.gameObject);
    }
}
