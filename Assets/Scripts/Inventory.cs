using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject currentObjectCanvas;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private Color[] slotColors = new  Color[2];
    [SerializeField] private TextMeshProUGUI[] items = new TextMeshProUGUI[4];

    [SerializeField] private int SelectedSlot = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateInventorySlots();
    }
    public void UpdateInventorySlots()
    {
        int itemCount = playerInventory.items.Count;

        for (int i = 0; i < items.Length; i++)
        {
            // Set default color
            items[i].color = slotColors[1];

            if (i < itemCount)
            {
                // Set name if item exists
                items[i].text = playerInventory.items[i].itemName;
            }
            else
            {
                // Clear text if slot is empty
                items[i].text = "-";
            }
        }
    }

    public void SelectSlot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            switch (context.control.name)
            {
                case "1":
                    HighlightSlot(1);
                    break;
                case "2":
                    HighlightSlot(2);
                    break;
                case "3":
                    HighlightSlot(3);
                    break;
                case "4":
                    HighlightSlot(4);
                    break;

            }
            print(SelectedSlot);
            
        }
    }

    private void HighlightSlot(int slotIndex)
    {
        // If slotIndex comes in as 1, 2, 3, or 4...

        if (slotIndex == SelectedSlot)
        {
            items[slotIndex-1].color = slotColors[1];
            SelectedSlot = 0;
            return;
        }
        
        SelectedSlot = slotIndex;
        // Use a standard for-loop to avoid manual index management
        for (int i = 0; i < items.Length; i++)
        {
            // Compare current loop index to (pressed button - 1)
            if (i == slotIndex - 1)
            {
                items[i].color = slotColors[0]; // The "Active" Green
            }
            else
            {
                items[i].color = slotColors[1]; // The "Inactive" Color
            }
        }
    }
    
    public Item GetSelectedItem()
    {
        // If SelectedSlot is 0, nothing is selected.
        // Otherwise, check if the index exists in the player inventory.
        if (SelectedSlot > 0 && (SelectedSlot - 1) < playerInventory.items.Count)
        {
            return playerInventory.items[SelectedSlot - 1];
        }
        return null;
    }
    public void RemoveItem(Item itemToRemove)
    {
        if (playerInventory.items.Contains(itemToRemove))
        {
            // 1. Remove from the ScriptableObject list
            playerInventory.items.Remove(itemToRemove);
        
            // 2. Deselect the slot so the player isn't "holding" air
            SelectedSlot = 0;
        
            // 3. Refresh the UI text
            UpdateInventorySlots();
        }
    }
}
