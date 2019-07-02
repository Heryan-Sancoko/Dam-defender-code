using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{

    // Use this for initialization

    public int inventory;
    public GameObject harvestJuicePrefab;
    public IntEvent onInventoryUpdate;
    public VoidEvent onWoodHarvest;
    public Vector3Event onWoodHarvestAtPos;

    private GameObject harvestJuice;
    private float woodTimer = 0;

    public Vector3Event onInventoryAddedAtPosition;

    void Start()
    {
        onInventoryUpdate.Invoke(inventory);
    }
    
    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Collectable")
        {
            woodTimer += Time.deltaTime;
            //set harvestJuice position
            Vector3 harvestPos = new Vector3(other.gameObject.transform.position.x, transform.position.y, other.gameObject.transform.position.z);
            if (harvestJuice==null)
            harvestJuice = Instantiate(harvestJuicePrefab, harvestPos, Quaternion.identity, null);

            harvestJuice.transform.localScale *= 1 - Time.deltaTime;

            if (woodTimer > 1)
            {
                onWoodHarvestAtPos.Invoke(other.transform.position);
                GameObject.Destroy(other.transform.gameObject);
                IncrementInventory();
                onWoodHarvest.Invoke();
                onWoodHarvestAtPos.Invoke(other.transform.position);
                woodTimer = 0;
                Destroy(harvestJuice);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Collectable")
        {
            woodTimer = 0;
            if (harvestJuice!=null)
                Destroy(harvestJuice);
        }
    }

    public int GetInventoryCount()
    {
        return inventory;
    }

   public void IncrementInventory()
    {
        inventory += 3;
        onInventoryUpdate.Invoke(inventory);

    }

    public void ChangeInventory(int deltaInventory)
    {
        inventory += deltaInventory;
        onInventoryUpdate.Invoke(inventory);

    }

    public void ClearInventory()
    {
        inventory = 0;
        onInventoryUpdate.Invoke(inventory);
    }

    public int CountInventory()
    {
        return inventory;
    }

}