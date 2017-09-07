using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public float EnemySpeed;
    public float EnemySpeedDownNearby;
    public float EnemyRandomness;

    public float CameraSpeed;

    public float PlayerSpeed;
    public int PlayerTotalHealth;
    public int PlayerCurrentHealth;
    public int Coins;

    public ItemList<Item> Inventory = new ItemList<Item>();
}

[Serializable]
public class Item
{
    public Item(string name, Action<float> powerupFunction, float increaseAmount)
    {
        this.Name = name;
        this.IncreaseAmount = increaseAmount;
        this.PowerupFunction = powerupFunction;
    }

    public float IncreaseAmount { get; set; }
    public string Name { get; set; }
    public Action<float> PowerupFunction { set; get; }
}

[Serializable]
public class ItemList<T> : List<T> where T : Item
{
    new public void Add(T item)
    {
        item.PowerupFunction.Invoke(item.IncreaseAmount);
        base.Add(item);
    }
}
