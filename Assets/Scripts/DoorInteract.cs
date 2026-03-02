using System;
using TMPro;
using UnityEngine;

public class DoorInteract : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private PuzzleObject requiredItems;
    [SerializeField] private Inventory inventory;
    
    [SerializeField] private CanvasGroup taskList;


    private void Start()
    {
        door = gameObject;
    }

    public void onInteract(Item itemSelected) 
    {
        if (itemSelected == null)
        {
            Debug.Log("no items");
            return;
        }

        foreach (var slot in requiredItems.requiredItems)
        {
            if (itemSelected == slot.item && !slot.isInserted)
            {
                slot.isInserted = true;
                Debug.Log("inserted");
                
                
                inventory.RemoveItem(itemSelected);
                if (requiredItems.isDone())
                {
                    OpenDoor();
                }

                return;
            }
        }

    }
        private void OpenDoor()
        {
            Debug.Log("Open door");
            door.SetActive(false);
        }
}
