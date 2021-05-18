using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDoor : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> EnemyArray = new List<GameObject>();
    public List<GameObject> PuzzleArray = new List<GameObject>();
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (EnemyArray.Count > 0)
        {
            bool AllEnemiesDead = true;
            for (int i = 0; i < EnemyArray.Count; i++)
            {
                if (EnemyArray[i] != null)
                {
                    AllEnemiesDead = false;
                }
            }
            if (AllEnemiesDead)
            {
                Destroy(gameObject);
            }
        }
    }
}
