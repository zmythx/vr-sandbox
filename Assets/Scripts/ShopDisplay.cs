using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject CostBox;
    public GameObject ItemSpawnLocation;
    public GameObject playerRef;
    public GameObject PlayerGoldText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerGoldText.GetComponent<TextMeshPro>().text = "Gold: " + playerRef.GetComponent<PlayerStatManagement>().Gold.ToString();
    }
    public void Display(string ItemCost)
    {
        CostBox.GetComponent<TextMeshPro>().text = "Cost: " + ItemCost;
    }
    public void KillDisplay()
    {
        CostBox.GetComponent<TextMeshPro>().text = "Cost:";
    }
    public void SpawnItem(GameObject ItemToSell)
    {
        GameObject soldItem = Instantiate(ItemToSell);
        soldItem.transform.position = ItemSpawnLocation.transform.position;
    }
}
