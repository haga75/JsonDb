using System;
using System.Collections.Generic;
using System.Text;

namespace JsonDbTest
{
    public class Item
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public IList<string>? Content { get; set; }
        public override int GetHashCode()
        {
            return (int)Convert.ToUInt32(Encoding.UTF8.GetBytes(Id.ToString() + Name));
        }
    }
}
