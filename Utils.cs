using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserForm
{
    public class Utils
    {
        public static ushort ConvertBoolArrayToByte(bool[] source)
        {
            ushort result = 0;
            // This assumes the array never contains more than 8 elements!

            int index = 0;

            // Loop through the array
            foreach (bool b in source)
            {
                // if the element is 'true' set the bit at that position
                if (b)
                    result |= (ushort)(1 << (source.Length - index - 1));
                index++;
            }

            return result;
        }
        public static bool[] ConvertByteToBoolArray(ushort b, int len)
        {
            // prepare the return result
            bool[] result = new bool[len];

            // check each bit in the byte. if 1 set to true, if 0 set to false
            for (int i = 0; i < len; i++)
                result[i] = (b & (1 << i)) != 0;

            // reverse the array
            Array.Reverse(result);

            return result;
        }
    }
}
