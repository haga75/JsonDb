using System;
using System.Text;

namespace JsonDbTest
{
    public class Word
    {
        public string W { get; set; }
        public override int GetHashCode()
        {
            return (int)Convert.ToUInt32(Encoding.UTF8.GetBytes(s: W));
        }
    }
}
