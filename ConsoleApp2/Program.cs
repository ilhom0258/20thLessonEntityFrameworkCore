using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            bool working = true;


            while ( working)
            {
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
                            case 3:
                                break;
                            case 4:
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
                    var itemName = Console.ReadLine();
                    var itemPrice = Console.ReadLine();
                    var category = Console.ReadLine();
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
        private static void Read()
        {
            try
            {
                using (AcademyContext db = new AcademyContext())
                {
                    var shops = db.Shop.ToList();
                    var items = db.Item.ToList();
                    foreach( var shop in shops)
                    {
                        Console.WriteLine($"{shop.Id}.....${shop.Item}.....${shop.ItemId}"); 
                    }
                    foreach( var item in items)
                    {
                        Console.WriteLine($"{item.Id}.....{item.Name}....{item.Price}....{item.Category}"); 
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
   
}
