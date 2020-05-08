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

            using (var db = new AcademyContext())
            {
                var shop = db.Shop.Include(i => i.Item).ToList(); 
            }

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
                    if ( db.SaveChanges() > 0)
                    {
                        Successful("Successfully created"); 
                    }
                    else
                    {
                        Error("Error : something went wrong"); 
                    }
                }
            }
            catch( Exception ex)
            {
                Error($"Error: {ex.Message}");
            }
            finally
            {
                PressAnyKey();
            }
        }
        private static void Read(string com = "null")
        {
            try
            {
                using (AcademyContext db = new AcademyContext())
                {
                    var items = db.Item.ToList();
                    var shops = db.Shop.ToList(); 
                    // Show items; 
                    Console.WriteLine("Item ID\t\tItem Name\t\tItem Price\t\tItem Category"); 
                    foreach ( var item in items)
                    {
                        var id = item.Id.ToString().Trim();
                        var name = item.Name.ToString().Trim();
                        var price = item.Price.ToString().Trim();
                        var category = item.Category.ToString().Trim();

                        Console.Write($"{id}"); 
                        if ( id.Length < 2)
                        {
                            Console.Write(" "); 
                        }
                        Console.Write($"\t\t{name}"); 
                        for ( int i = 0; i < 9 - name.Length; i++)
                        {
                            Console.Write(" "); 
                        }
                        Console.Write($"\t\t{price}"); 
                        for ( int i = 0; i < 10 - price.Length; i++)
                        {
                            Console.Write(" ");
                        }
                        Console.WriteLine($"\t\t{category}");
                    }
                    Console.Write("\n\n\n"); 
                    foreach( var shop in shops)
                    {
                        Console.WriteLine($"{shop.Id}, {shop.Item}, {shop.ItemId}"); 
                    }
                    

                }
            }
            catch (Exception ex)
            {
                Error($"Error: {ex.Message}");
            }
            finally
            {
                if (com == "null")
                {
                    PressAnyKey();
                }
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
                        if (db.SaveChanges() > 0)
                        {
                            Successful("Successfully updated");
                        }
                        else
                        {
                            Error("Error : something went wrong");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Error($"Error: {ex.Message}");
            }
            finally
            {
                PressAnyKey();
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
                                if (db.SaveChanges() > 0)
                                {
                                    Successful("Successfully removed");
                                }
                                else
                                {
                                    Error("Error : something went wrong");
                                }
                            }
                            break; 
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Error($"Error: {ex.Message}"); 
            }
            finally
            {
                PressAnyKey();
            }
        }
        private static void PressAnyKey()
        {
            Console.WriteLine("Press any key to continue.....");
            Console.ReadKey(); 
        }

        private static void Successful ( string text )
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(text);
            Console.ResetColor(); 
        }
        private static void Error(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(text);
            Console.ResetColor(); 
        }
    }
   
}
