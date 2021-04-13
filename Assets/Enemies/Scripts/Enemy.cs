using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public float Health;
    public float MaxHealth;
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
    public ElementalType EnemyType;
    void Start()
    {
        
    }
    public void TakeDamage(float damage)
    {
        Health -= damage;
    }
    public void TakeDamage(float damage, string damtype)
    {
        switch (EnemyType.ToString())
        {
            case "None":
                break;
            case "Fire":
                if(damtype == "Water")
                {
                    damage = damage * 1.5f;
                }
                if(damtype == "Gaia")
                {
                    damage = damage * 0.75f;
                }
                if (damtype == "Holy" || damtype == "Unholy")
                {
                    damage = damage * 1.25f;
                }
                break;
            case "Water":
                if(damtype == "Lightning")
                {
                    damage = damage * 1.5f;
                }
                if(damtype == "Fire")
                {
                    damage = damage * 0.75f;
                }
                if (damtype == "Holy" || damtype == "Unholy")
                {
                    damage = damage * 1.25f;
                }
                break;
            case "Gaia":
                if (damtype == "Fire")
                {
                    damage = damage * 1.5f;
                }
                if (damtype == "Air")
                {
                    damage = damage * 0.75f;
                }
                if (damtype == "Holy" || damtype == "Unholy")
                {
                    damage = damage * 1.25f;
                }
                break;
            case "Earth":
                if (damtype == "Air")
                {
                    damage = damage * 1.5f;
                }
                if (damtype == "Lightning")
                {
                    damage = damage * 0.75f;
                }
                if (damtype == "Holy" || damtype == "Unholy")
                {
                    damage = damage * 1.25f;
                }
                break;
            case "Air":
                if (damtype == "Gaia")
                {
                    damage = damage * 1.5f;
                }
                if (damtype == "Earth")
                {
                    damage = damage * 0.75f;
                }
                if (damtype == "Holy" || damtype == "Unholy")
                {
                    damage = damage * 1.25f;
                }
                break;
            case "Lightning":
                if (damtype == "Earth")
                {
                    damage = damage * 1.5f;
                }
                if (damtype == "Water")
                {
                    damage = damage * 0.75f;
                }
                if (damtype == "Holy" || damtype == "Unholy")
                {
                    damage = damage * 1.25f;
                }
                break;
            case "Holy":
                if (damtype == "Holy")
                {
                    damage = damage * 0.5f;
                }
                if(damtype == "Unholy")
                {
                    damage = damage * 1.5f;
                }
                if(damtype!="Holy" && damtype!="Unholy")
                {
                    damage = damage * 0.8f;
                }
                break;
            case "Unholy":
                if (damtype == "Unholy")
                {
                    damage = damage * 0.5f;
                }
                if (damtype == "Holy")
                {
                    damage = damage * 1.5f;
                }
                if (damtype != "Holy" && damtype != "Unholy")
                {
                    damage = damage * 0.8f;
                }
                break;
            case "Legendary":
                if(damtype != "Legendary")
                {
                    damage = damage * 0.5f;
                }
                break;
        }
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
