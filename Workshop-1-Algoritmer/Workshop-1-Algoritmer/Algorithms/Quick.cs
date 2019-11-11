using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Workshop_1_Algoritmer.Algorithms
{
    public static class Quick
    {
        public static void Sort(int[] data)
        {
            Sort(data, 0, data.Length - 1);
        }

        public static void Sort(int[] data, int l, int r)
        {
            int i = l;
            int j = r;
            int pivot = data[l]; /* find pivot */

            while (true)
            {
                if (i > j)
                {
                    break;
                }
                while (data[i] < pivot)
                {
                    i++;
                }
                while (data[j] > pivot)
                {
                    j--;
                }
                if (i <= j)
                {
                    Exchange(data, i, j);
                    i++;
                    j--;
                }

            }
            if (l < j)
            {
                Sort(data, l, j);
            }
            if (i < r)
            {
                Sort(data, i, r);
            }
        }
        private static void Exchange(int[] data, int m, int n)
        {
            var temporary = data[m];
            data[m] = data[n];
            data[n] = temporary;
        }
    }
}
