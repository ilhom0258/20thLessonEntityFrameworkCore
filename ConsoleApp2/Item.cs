using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public partial class Item
    {
        public Item()
        {
            Shop = new HashSet<Shop>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Category { get; set; }

        public virtual ICollection<Shop> Shop { get; set; }
    }
}
