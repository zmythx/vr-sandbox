using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public float Health;
    public float MaxHealth;

    void Start()
    {
        
    }
    public void TakeDamage(float damage)
    {
        Health -= damage;
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetComponent<Renderer>().material.SetColor("_Color", new Color(1 - (Health / MaxHealth), Health / MaxHealth, 0.1f));
        if(Health < 0)
        {
            Destroy(transform.gameObject);
        }
    }
}
