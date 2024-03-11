using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{[SerializeField] string itemName;

private InventoryManager inventoryManager;

void Awake()
{
    inventoryManager = FindObjectOfType<InventoryManager>();
    if (inventoryManager == null)
    {
        Debug.LogError("InventoryManager not found in the scene.");
    }
}
	void OnTriggerEnter(Collider other)
{
    if (other.gameObject.CompareTag("Player"))
    {
        inventoryManager.AddItem(other.gameObject.name, "Collectible");
        Destroy(gameObject);
    }
}
}