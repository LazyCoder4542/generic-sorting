using System;
using GenericSort.Sorting;

namespace GenericSort
{
  public static class Program
  {
    static void Main(String[] args)
    {
      int[] arr = { 2, 3, 1, 4, 12, 8, 0 };
      Sorting<int>.QuickSort(arr);
      PrintArray<int>(arr);

      double[] arr2 = { 0.1, 0.23, 0.04, 0.32, 0.92, 0.11, 0.3 };
      NonComparableSorting.BucketSort<double>(arr2);
      PrintArray<double>(arr2);

      int[] arr3 = { 2, 3, 1, 4, 8, 0, 9 };
      int[] arr3Sorted = new int[7];
      NonComparableSorting.CountingSort<int>(arr3, arr3Sorted, 9);
      PrintArray<int>(arr3Sorted);

      int[] arr4 = { 836, 937, 149, 036, 371, 293, 395 };
      NonComparableSorting.RadixSort(arr4, 3);
      PrintArray<int>(arr4);
    }

    public static void PrintArray<T>(T[] arr)
    {
      Console.Write("{ ");
      foreach (T itm in arr)
      {
        Console.Write(itm);
        Console.Write(", ");
      }
      Console.WriteLine("}");
    }
  }
}
