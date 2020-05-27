using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmeraldLib
{
    [Serializable]
    public class Street
    {
        public string Name { get; set; }
        public int[] Houses { get; set; }

        public static int operator ~(Street str) => str.Houses.Length;
        public static bool operator +(Street str) => str.Houses.Contains(7);

        public override string ToString()
        {
            return $"Name: {Name}, Houses: {string.Join(", ", Houses)}, Contains7: {+this}, Length: {~this}";
        }
    }
}
