using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager {
    public ManagerStatus status { get; private set; }

    private Dictionary<string, Dictionary<string, int>> playerInventories;

    public void Startup() {
        status = ManagerStatus.Started;
        playerInventories = new Dictionary<string, Dictionary<string, int>>();
    }

    public void AddItem(string playerName, string itemName) {
        if (!playerInventories.ContainsKey(playerName)) {
            playerInventories[playerName] = new Dictionary<string, int>();
        }

        if (playerInventories[playerName].ContainsKey(itemName)) {
            playerInventories[playerName][itemName]++;
        } else {
            playerInventories[playerName][itemName] = 1;
        }

        DisplayItems(playerName);
    }

    public int GetItemCount(string playerName, string itemName) {
        if (playerInventories.ContainsKey(playerName) && playerInventories[playerName].ContainsKey(itemName)) {
            return playerInventories[playerName][itemName];
        }

        return 0;
    }

    public bool ConsumeItem(string playerName, string itemName) {
        if (playerInventories.ContainsKey(playerName) && playerInventories[playerName].ContainsKey(itemName)) {
            playerInventories[playerName][itemName]--;
            if (playerInventories[playerName][itemName] == 0) {
                playerInventories[playerName].Remove(itemName);
            }
            DisplayItems(playerName);
            return true;
        }

        Debug.Log($"Cannot consume {itemName}");
        return false;
    }

    public List<string> GetItemList(string playerName) {
        if (playerInventories.ContainsKey(playerName)) {
            return new List<string>(playerInventories[playerName].Keys);
        }
        return new List<string>();
    }

    /*public bool EquipItem(string playerName, string itemName) {
        if (playerInventories.ContainsKey(playerName) && playerInventories[playerName].ContainsKey(itemName)) {
            // Implement item equipping logic here
            return true;
        }
        return false;
    }*/

    private void DisplayItems(string playerName) {
        string itemDisplay = $"{playerName}'s Items: ";
        foreach (KeyValuePair<string, int> item in playerInventories[playerName]) {
            itemDisplay += item.Key + "(" + item.Value + ") ";
        }
        Debug.Log(itemDisplay);
    }
}