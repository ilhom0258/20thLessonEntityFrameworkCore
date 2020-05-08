using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Transactions;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            bool working = true;


            while ( working)
            {
                Console.Clear(); 
                Console.Write("1.Create\n2.Read\n3.Update\n4.Delete\n5.Exit\nYour Choice : ");
                int choice; 
                if ( int.TryParse(Console.ReadLine(), out choice))
                {
                    if ( choice > 0 && choice < 6)
                    {
                        switch( choice)
                        {
                            case 1:Create();
                                break;
                            case 2:Read(); 
                                break;
                            case 3:Update();
                                break;
                            case 4:Delete(); 
                                break;
                            case 5:working = false;
                                break;
                        }
                    }
                }
            }
        }
        private static void Create()
        {
            try
            {
                using ( AcademyContext db = new AcademyContext())
                {
                    Console.Write("Item Name: "); 
                    var itemName = Console.ReadLine().Trim();
                    Console.Write("Item Price: ");
                    var itemPrice = Console.ReadLine().Trim();
                    Console.Write("Item Category: "); 
                    var category = Console.ReadLine().Trim();
                    Item item = new Item { Name = itemName, Category = category, Price = itemPrice};
                    db.Item.Add(item);
                    db.SaveChanges(); 
                }
            }
            catch( Exception ex)
            {
                Console.WriteLine(ex.Message); 
            }
        }
        private static void Read(string com = "null")
        {
            try
            {
                using (AcademyContext db = new AcademyContext())
                {
                    var items = db.Item.ToList();
                    // Show items; 
                    Console.WriteLine("Item ID..................Item Name...............Item Price..................Item Category"); 
                    foreach ( var item in items)
                    {
                        Console.WriteLine($"{item.Id}........{item.Name}.........{item.Price}........{item.Category}"); 
                    }
                    if ( com == "null")
                    {
                        PressAnyKey();
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static void Update()
        {
            try
            {
                using (AcademyContext db = new AcademyContext())
                {
                    Read("update"); 
                    Console.Write("Item Id: "); 
                    int id = int.Parse(Console.ReadLine());

                    var item = db.Item.Find(id); 

                    if ( item != null)
                    {
                        Console.Write("Item Price: ");
                        var price = Console.ReadLine().Trim();
                        item.Price = price;
                        db.SaveChanges();
                        PressAnyKey();
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static void Delete()
        {
            try
            {
                using (AcademyContext db = new AcademyContext())
                {
                    Read("delete");
                    int id = -1;
                    bool t = true; 
                    while ( t )
                    {
                        Console.Write("ID: "); 
                        if ( int.TryParse(Console.ReadLine(), out id))
                        {
                            var item = db.Item.Find(id);
                            if (item != null)
                            {
                                db.Item.Remove(item);
                                db.SaveChanges(); 
                                Console.WriteLine("Item Removed");
                                PressAnyKey();
                            }
                            break; 
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static void PressAnyKey()
        {
            Console.WriteLine("Press any key to continue.....");
            Console.ReadKey(); 
        }
    }
   
}
