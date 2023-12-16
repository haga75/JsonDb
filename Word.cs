using System;
using System.Text;

namespace JsonDbTest
{
    public class Word
    {
        public string? W { get; set; }
        public override int GetHashCode()
        {
            return (int)Convert.ToUInt32(Encoding.ASCII.GetBytes(1.ToString()));
            //var result = (int)Convert.ToUInt32(Encoding.ASCII.GetBytes(s: W));

            //return result;
        }
    }
}
