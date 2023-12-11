using System.Collections.Generic;
using System.IO;
using System;
using System.Reflection;

static class Program
{
    static void Main()
    {
        Order order = new Order();
        order.RootPage();
    }
}



class Order
{
    private List<Item> Items = new List<Item>();

    private Item[] Form = { new Item("  Круг", 100), new Item("  Квадрат", 200), new Item("  Овал", 200), new Item("  Сердце", 500) };

    private Item[] Size = { new Item("  Маленький", 200), new Item("  Средний", 400), new Item("  Большой", 600) };
    private Item[] Taste = { new Item("  Малина", 300), new Item("  Фисташка", 200), new Item("  Шоколад", 100), new Item("  Клубника", 300) };

    private Item[] Glaze = { new Item("  Шоколадная", 100), new Item("  Ягодная", 200), new Item("  Кремовая", 200) };
    private Item[] Decor = { new Item("  Сердца", 100), new Item("  Голуби", 200), new Item("  Кружева", 200) };
    private Dictionary<int, Item[]> menus = new Dictionary<int, Item[]>();

    public Order()
    {
        menus.Add(0, Form);
        menus.Add(1, Size);
        menus.Add(2, Taste);
        menus.Add(3, Glaze);
        menus.Add(4, Decor);
        if (!File.Exists("C:\\Users\\genri\\OneDrive\\Рабочий стол\\code\\История заказов.txt"))
             File.Create("C:\\Users\\genri\\OneDrive\\Рабочий стол\\code\\История заказов.txt");
    }

    public void RootPage()
    {
        Console.Clear();
        string[] subMenus = { "  Форма", "  Размер", "  Вкус", "  Глазурь", "  Декор", "  Конец заказа" };
        foreach (var menu in subMenus)
        {
            Console.WriteLine(menu);
        }
        Console.WriteLine("Цена: " + ShowPrice());
        Console.WriteLine("Ваш торт: " + ShowDetails());
        SubPage(Menu.CheckMainMenu(subMenus.Length - 1));

    }

    public void SubPage(int type)
    {

        Console.Clear();
        if (type == 5)
        {
            EndOffer();
            RootPage();
        }

        foreach (Item menu in menus[type])
        {
            Console.WriteLine(menu.Title + "---" + menu.Price);
        }
        int pos = Menu.CheckSubMenu(menus[type].Length - 1);
        if (pos == -1) RootPage();
        else AddItem(menus[type][pos]);
    }

    private void EndOffer()
    {
        if (Items != null && Items.Count > 0)
        {
            string data = "";
            data += ShowDetails() + "----" + ShowPrice();

            using (StreamWriter SW = new StreamWriter("C:\\Users\\genri\\OneDrive\\Рабочий стол\\code\\История заказов.txt"))
            {
                SW.WriteLine(data);
            }
            Items = new List<Item>();
        }
    }
    private int ShowPrice()
    {
        int price = 0;
        if (Items != null && Items.Count > 0)
        {
            foreach (Item it in Items)
                price += it.Price;
        }
        return price;
    }
    private string ShowDetails()
    {
        string about = "";
        if (Items != null && Items.Count > 0)
        {
            foreach (Item it in Items)
                about += it.Title + "+";
            about = about.Remove(about.Length - 1);
        }
        return about;
    }

    private void AddItem(Item item)
    {
        Items.Add(item);
        RootPage();
    }


}


static class Menu
{
    public static int CheckMainMenu(int count, int startPos = 0, string arrow = "->")
    {
        string empty = new string(' ', arrow.Length);
        int i = startPos;
        Console.SetCursorPosition(0, startPos);
        Console.Write(arrow);
        ConsoleKeyInfo key;
        do
        {
            key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.DownArrow:
                    if (i == count + startPos)
                        continue;

                    Console.SetCursorPosition(0, i);
                    Console.Write(empty);
                    Console.SetCursorPosition(0, ++i);
                    Console.Write(arrow);
                    break;
                case ConsoleKey.UpArrow:

                    if (i == startPos)
                        continue;
                    Console.SetCursorPosition(0, i);
                    Console.Write(empty);
                    Console.SetCursorPosition(0, --i);
                    Console.Write(arrow);
                    break;

                case ConsoleKey.Enter:
                    break;
            }
        } while (key.Key != ConsoleKey.Enter);
        return i;
    }

    public static int CheckSubMenu(int count, int startPos = 0, string arrow = "->")
    {
        string empty = new string(' ', arrow.Length);
        int i = startPos;
        Console.SetCursorPosition(0, startPos);
        Console.Write(arrow);
        ConsoleKeyInfo key;
        do
        {
            key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.DownArrow:
                    if (i == count + startPos)
                        continue;

                    Console.SetCursorPosition(0, i);
                    Console.Write(empty);
                    Console.SetCursorPosition(0, ++i);
                    Console.Write(arrow);
                    break;
                case ConsoleKey.UpArrow:

                    if (i == startPos)
                        continue;
                    Console.SetCursorPosition(0, i);
                    Console.Write(empty);
                    Console.SetCursorPosition(0, --i);
                    Console.Write(arrow);
                    break;

                case ConsoleKey.Enter:
                    break;
                case ConsoleKey.Escape:
                    return -1;
            }
        } while (key.Key != ConsoleKey.Enter);
        return i;
    }
}

public class Item
{
    public string Title { get; set; }
    public int Price { get; set; }

    public Item(string title, int price)
    {
        Title = title;
        Price = price;
    }
}