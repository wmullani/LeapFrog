using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{[SerializeField] string itemName;

	void OnTriggerEnter(Collider other) {
    if (Managers.Inventory != null) {
        Managers.Inventory.AddItem(itemName);
        Destroy(this.gameObject);
    } else {
        Debug.LogError("Managers.Inventory is null");
    }
}
}