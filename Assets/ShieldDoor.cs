using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDoor : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> EnemyArray = new List<GameObject>();
    public List<GameObject> PuzzleArray = new List<GameObject>();
    public bool IsFinalDoor = false;
    private bool PlayerClearedRoom = false;
    private bool GenNewRoom = false;
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
            if (AllEnemiesDead && !IsFinalDoor)
            {
                Destroy(gameObject);
            }
            if(AllEnemiesDead && IsFinalDoor)
            {
                PlayerClearedRoom = true;
                GetComponent<Collider>().isTrigger = true;
            }
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player" && PlayerClearedRoom && !GenNewRoom)
        {
            GenNewRoom = true;
            GameObject masterparent = collision.gameObject;
            while(masterparent.transform.parent != null)
            {
                masterparent = masterparent.transform.parent.gameObject;
            }
            masterparent.transform.position = new Vector3(1, 2.83f, 0);
            foreach(GameObject ob in GameObject.FindGameObjectsWithTag("GeneratedLevel"))
            {
                if (ob != gameObject)
                {
                    Destroy(ob);
                }
            }
            GameObject prevGen = GameObject.FindGameObjectWithTag("LevelGenerator");
            GameObject newGen = Instantiate(prevGen);
            newGen.transform.position = prevGen.transform.position;
            Destroy(gameObject);
        }
    }
}
