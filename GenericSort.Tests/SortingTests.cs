using System.Linq;
using GenericSort.Sorting;
using Xunit;

namespace GenericSort.Tests
{
  public class SortingTests
  {
    [Fact]
    public void QuickSort_SortsIntArrayCorrectly()
    {
      int[] input = { 4, 1, 3, 9, 7 };
      int[] expected = { 1, 3, 4, 7, 9 };

      Sorting<int>.QuickSort(input);

      Assert.Equal(expected, input);
    }

    [Fact]
    public void MergeSort_SortsDoubleArrayCorrectly()
    {
      double[] input = { 0.4, 0.1, 0.3, 0.2 };
      double[] expected = { 0.1, 0.2, 0.3, 0.4 };

      Sorting<double>.MergeSort(input);

      Assert.Equal(expected, input);
    }

    [Fact]
    public void CountingSort_SortsIntegerArrayCorrectly()
    {
      int[] input = { 4, 1, 3, 4, 3 };
      int[] output = new int[input.Length];

      NonComparableSorting.CountingSort<int>(input, output, 4);

      Assert.Equal(new int[] { 1, 3, 3, 4, 4 }, output);
    }

    [Fact]
    public void BucketSort_SortsFloatArrayCorrectly()
    {
      double[] input = { 0.22, 0.09, 0.31, 0.18 };
      double[] expected = { 0.09, 0.18, 0.22, 0.31 };

      NonComparableSorting.BucketSort<double>(input);

      Assert.Equal(expected, input);
    }
  }

  public class Student
  {
    public string Name { get; set; } = String.Empty;
    public int Grade { get; set; }
  }

  public class SortingStabilityTests
  {
    [Fact]
    public void CountingSort_PreservesStability()
    {
      var students = new[]
      {
        new Student { Name = "Alice", Grade = 2 },
        new Student { Name = "Bob", Grade = 1 },
        new Student { Name = "Charlie", Grade = 2 },
        new Student { Name = "David", Grade = 1 },
      };

      // Extract grades
      int[] grades = students.Select(s => s.Grade).ToArray();
      int[] sortedGrades = new int[grades.Length];

      // Perform Counting Sort (get position map)
      int[] map = NonComparableSorting.CountingSort<int>(grades, sortedGrades, 2);

      // Reorder original array using the map
      var reordered = new Student[students.Length];
      for (int i = 0; i < map.Length; i++)
      {
        reordered[i] = students[map[i]];
      }

      // Expect: Bob (1), David (1), Alice (2), Charlie (2)
      Assert.Equal("Bob", reordered[0].Name);
      Assert.Equal("David", reordered[1].Name);
      Assert.Equal("Alice", reordered[2].Name);
      Assert.Equal("Charlie", reordered[3].Name);
    }

    [Fact]
    public void RadixSort_PreservesStability()
    {
      var numbers = new[]
      {
        (value: 231, tag: "A"),
        (value: 132, tag: "B"),
        (value: 231, tag: "C"),
        (value: 132, tag: "D"),
      };

      // Extract values
      int[] values = numbers.Select(n => n.value).ToArray();

      // Radix sort values
      NonComparableSorting.RadixSort(values, 3);

      // Reconstruct the sorted order using tags (by matching first unused tag per value)
      string[] actualTags = new string[numbers.Length];
      var usedTags = new HashSet<string>();
      int index = 0;

      foreach (var val in values)
      {
        var match = numbers.First(n => n.value == val && !usedTags.Contains(n.tag));
        actualTags[index++] = match.tag;
        usedTags.Add(match.tag);
      }

      string[] expectedTags = { "B", "D", "A", "C" };
      Assert.Equal(expectedTags, actualTags);
    }
  }
}
