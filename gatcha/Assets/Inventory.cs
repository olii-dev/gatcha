using System;
using System.Collections.Generic;

namespace GachaGame
{
    public class Item
    {
        public string Name { get; private set; }
        public string Rarity { get; private set; }

        public Item(string name, string rarity)
        {
            Name = name;
            Rarity = rarity;
        }

        public override string ToString()
        {
            return $"{Name} (Rarity: {Rarity})";
        }
    }

    public class Inventory
    {
        private List<Item> items;

        public Inventory()
        {added
            items = new List<Item>();
        }

        public void AddItem(Item item)
        {
            items.Add(item);
            Console.WriteLine($"{item.Name} has been added to your inventory.");
        }

        public void RemoveItem(Item item)
        {
            if (items.Remove(item))
            {
                Console.WriteLine($"{item.Name} has been removed from your inventory.");
            }
            else
            {
                Console.WriteLine($"{item.Name} is not in your inventory.");
            }
        }

        public void DisplayItems()
        {
            Console.WriteLine("Your Inventory:");
            if (items.Count == 0)
            {
                Console.WriteLine("Inventory is empty.");
                return;
            }

            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Inventory inventory = new Inventory();

            Item sword = new Item("", "Epic");
            Item shield = new Item("", "Rare");
            Item potion = new Item("", "Common");

            inventory.AddItem(sword);
            inventory.AddItem(shield);
            inventory.AddItem(potion);

            inventory.DisplayItems();

            inventory.RemoveItem(shield);
            inventory.DisplayItems();

            inventory.RemoveItem(shield);
        }
    }
}