using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PuzzleSlot
{
    public Item item;
    public bool isInserted;
}

[CreateAssetMenu(fileName = "PuzzleInventory", menuName = "Puzzle/Inventory")]
public class PuzzleObject : ScriptableObject
{
    public List<PuzzleSlot> requiredItems = new List<PuzzleSlot>();

    public bool isDone()
    {
        foreach (var slot in requiredItems)
        {
            if (!slot.isInserted) return false;
        }
        return true;
    }
    
    public void ResetPuzzle() 
    {
        foreach(var slot in requiredItems) slot.isInserted = false;
    }
}
