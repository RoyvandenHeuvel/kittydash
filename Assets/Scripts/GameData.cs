using System;
using System.Collections.Generic;

/// <summary>
/// Class containing all the values the game needs to save and load its state.
/// </summary>
[Serializable]
public class GameData
{
    /// <summary>
    /// Speed the enemy has when further than EnemyNearbyDistance away.
    /// </summary>
    public float EnemySpeedFar;
    /// <summary>
    /// Speed the enemy has when less than EnemyNearbyDistance away.
    /// </summary>
    public float EnemySpeedNearby;
    /// <summary>
    /// Distance the enemy has to be away for it to switch its speed.
    /// </summary>
    public float EnemyNearbyDistance;
    /// <summary>
    /// The degree of randomness the enemy uses for movement when in the Nearby state.
    /// </summary>
    public float EnemyRandomness;
    /// <summary>
    /// Factor of the camera speed the enemy uses.
    /// </summary>
    public float EnemyCameraSpeedFactor;
    /// <summary>
    /// Speed of the camera.
    /// </summary>
    public float CameraSpeed;
    /// <summary>
    /// Speed of the player.
    /// </summary>
    public float PlayerSpeed;
    /// <summary>
    /// Total health the player can have.
    /// </summary>
    public int PlayerTotalHealth;
    /// <summary>
    /// Health the player currently has left.
    /// </summary>
    public int PlayerCurrentHealth;
    /// <summary>
    /// Coins the player possesses.
    /// </summary>
    public int Coins;
    /// <summary>
    /// List of all the items the player possesses.
    /// </summary>
    public ItemList<Item> Inventory = new ItemList<Item>();
}

/// <summary>
/// Item class, containing a name, a power-up function and the amount the power-up function powers up for.
/// </summary>
[Serializable]
public class Item
{
    public Item(string name, Action<float> powerupFunction, float increaseAmount)
    {
        this.Name = name;
        this.IncreaseAmount = increaseAmount;
        this.PowerupFunction = powerupFunction;
    }
    /// <summary>
    /// Parameter for the PowerupFunction action.
    /// </summary>
    public float IncreaseAmount { get; set; }
    /// <summary>
    /// Name of the item.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Function that will be executed when the item is added to an ItemList, with IncreaseAmount as its parameter.
    /// </summary>
    public Action<float> PowerupFunction { set; get; }
}

/// <summary>
/// List that will invoke the PowerupFunction of an item when it gets added.
/// </summary>
/// <typeparam name="T">Type, required to implement or extend the Item class.</typeparam>
[Serializable]
public class ItemList<T> : List<T> where T : Item
{
    new public void Add(T item)
    {
        item.PowerupFunction.Invoke(item.IncreaseAmount);
        base.Add(item);
    }
}
