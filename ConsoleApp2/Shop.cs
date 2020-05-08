using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public partial class Shop
    {
        public string Id { get; set; }
        public int ItemId { get; set; }
        public string Category { get; set; }

        public virtual Item Item { get; set; }
    }
}
