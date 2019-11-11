using System;
using System.Collections.Generic;
using System.Text;

namespace Workshop_1_Algoritmer.Algorithms
{
    public static class Binary
    {
        public static int[] Search(int[] data, int[] values)
        {
            int[] output = new int[values.Length];

            for (int i = 0; i < values.Length; i++)
            {
                output[i] = Search(data, values[i]);
            }
            return output;
        }

        public static int Search(int[] data, int value)
        {
            int lower = 0;
            int upper = data.Length;

            if (!IsSorted(data))
            {
                Quick.Sort(data);
            }

            while (lower <= upper)
            {
                int mid = (lower + upper) / 2;

                if (data[mid] > value)
                {
                    upper = mid - 1;
                }
                else if (data[mid] < value)
                {
                    lower = mid + 1;
                }
                else
                {
                    return mid;
                }
            }

            return -1;
        }

        private static bool IsSorted(int[] data)
        {
            for (int i = 1; i < data.Length; i++)
            {
                if (data[i] < data[i - 1])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
