using System.Collections.Generic;

namespace LibraryApp.Class
{
    public class Sort
    {

        public static List<DCode> QuickSort(List<DCode> array, int left, int right)
            {

            //(see How to implement Quick Sort Algorithm in C#, 2020) A YouTube video by Learn with Code
            var tempArray = new List<string>();
                foreach (var variable in array)
                {
                    tempArray.Add(variable.bookCode);
                }

            /*Using quicksort because it is faster than bubble sort according to
             * (15 Sorting Algorithms in 6 Minutes, 2013). This a YouTube video by Timo Bingmann
             */

            while (true)
                {
                    int i = left, j = right;
                    var pivot = int.Parse(tempArray[(left + right) / 2]);

                    while (i <= j)
                    {
                        while (int.Parse(tempArray[i]) < pivot) i++;
                        while (int.Parse(tempArray[j]) > pivot) j--;
                        if (i > j) continue;
                        (tempArray[i], tempArray[j]) = (tempArray[j], tempArray[i]);
                        (array[i], array[j]) = (array[j], array[i]);
                        i++;
                        j--;
                    }

                    if (left < j) QuickSort(array, left, j);

                    if (i < right)
                    {
                        left = i;
                        continue;
                    }
                    break;
                }
                return array;
            }
        }
    }