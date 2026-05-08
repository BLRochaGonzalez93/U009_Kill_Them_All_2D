using UnityEngine;

/// <summary>
/// Base class for both the Passive and the Weapon classes.
/// </summary>
public abstract class Item : MonoBehaviour
{
    public int currentLevel = 1, maxLevel = 1;
    [HideInInspector] public ItemData data;
    protected PlayerInventory inventory;
    protected PlayerStats owner;

    public PlayerStats Owner { get { return owner; } }

    [System.Serializable]
    public class LevelData
    {
        public string name, description;
    }

    public virtual void Initialise(ItemData data)
    {
        maxLevel = data.maxLevel;

        // We have to find a better way to reference the player inventory
        // in future, as this is inefficient.
        inventory = GetComponentInParent<PlayerInventory>();
        owner = GetComponentInParent<PlayerStats>();
    }


    public virtual bool CanLevelUp()
    {
        return currentLevel <= maxLevel;
    }

    // Whenever an item levels up, attempt to make it evolve.
    public virtual bool DoLevelUp()
    {
        return true;
    }

    // What effects you receive on equipping an item.
    public virtual void OnEquip() { }

    // What effects are removed on unequipping an item.
    public virtual void OnUnequip() { }
}