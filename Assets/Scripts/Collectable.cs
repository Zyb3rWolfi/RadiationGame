using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] PlayerInventory playerInventory; // Reference to the player's inventory
    [SerializeField] Item item; // The item this collectable represents

    public void onCollected()
    {
        if (playerInventory.AddItem(item))
        {
            // Successfully added to inventory, destroy the collectable
            Destroy(gameObject);
        }
        else
        {
            // Inventory full, you can add feedback here (e.g., UI message)
            Debug.Log("Inventory is full! Cannot collect item.");
        }
    }
}
