# 🌀 GenericSort in C#

A C# project that implements **major sorting algorithms** using **generic classes** where applicable.  
Originally tasked with building any generic sorting algorithm, I went further to implement and organize multiple sorting techniques using powerful generic patterns in C#.

---

## 📁 Project Structure

```
GenericSort/
├── Sorting.cs        # All sorting algorithm implementations
├── Program.cs        # Demo
├── GenericSort.csproj
GenericSort.Tests/
└── SortingTests.cs   # Testing
generic-sorting.sln

```
---

## 🚀 How to Run

### 1. Clone the Repository

```bash
git clone https://github.com/LazyCoder4542/generic-sorting.git
cd generic-sorting
````

### 2. Run the Project

```bash
dotnet run --project GenericSort
```

Ensure you have [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download) or later installed.

Here’s a **Testing** section you can add to your `README.md`, formatted appropriately:

---

## 🧪 Testing

This project uses **xUnit** for unit testing.

### 🏁 Running Tests

To run all tests from the root of the solution:

```bash
dotnet test
```

Make sure you've restored packages first:

```bash
dotnet restore
```

### 🧠 What’s Covered

* ✅ **Correctness tests** for all implemented sorting algorithms (e.g. Quick Sort, Merge Sort, Counting Sort, Bucket Sort).
* 🔁 **Stability tests** for sorting algorithms where applicable (e.g. Counting Sort, Radix Sort).
* ⚠️ Both generic and non-generic algorithms are tested.

### 📂 Test Project Structure

Tests are located in the `GenericSort.Tests` project:

This file includes:

* Unit tests for sorting correctness
* Stability verification using custom object arrays
* Sample input/output assertions

---

## 📚 Implemented Sorting Algorithms

### ✅ Generic Sorting Algorithms (`Sorting<T>`)

All methods require `T : notnull, IComparable<T>`

* ### Merge Sort

  A classic divide-and-conquer algorithm. Recursively splits the array and merges sorted subarrays.

  ```csharp
  Sorting<T>.MergeSort(T[] array);
  ```

* ### Insertion Sort

  Efficient for small or nearly-sorted arrays. Shifts larger elements forward to insert the current one.

  ```csharp
  Sorting<T>.InsertionSort(T[] array);
  ```

* ### Selection Sort

  Selects the minimum element and places it at the beginning of the unsorted part.

  ```csharp
  Sorting<T>.SelectionSort(T[] array);
  ```

* ### Heap Sort

  Builds a max heap and repeatedly extracts the largest element.

  ```csharp
  Sorting<T>.HeapSort(T[] array);
  ```

* ### Quick Sort

  Picks a pivot and partitions the array around it. Fast on average, but unstable.

  ```csharp
  Sorting<T>.QuickSort(T[] array);
  ```

---

### ✅ Non-Generic or Specialized Sorting Algorithms (`NonComparableSorting`)

* ### Bucket Sort (Floating-Point Only)

  Works on numbers in the range \[0, 1). Uses `List<T>` buckets and insertion sort for internal sorting.

  ```csharp
  NonComparableSorting.BucketSort<double>(double[] array);
  ```

* ### Counting Sort (For integers)

  Stable, non-comparison sort. Assumes elements are small non-negative integers.

  ```csharp
  NonComparableSorting.CountingSort<int>(int[] input, int[] output, int maxValue);
  ```

  * Also returns a **mapping array** to help with stability or reorder tracking.

* ### Radix Sort (Base-10 integers)

  Performs digit-by-digit sorting using counting sort as a subroutine.
  Number of digits (`k`) and base (`r`, default 10) must be provided.

  ```csharp
  NonComparableSorting.RadixSort(int[] array, int digitCount);
  ```

---

## 🧪 Sample Output

When you run the project, it will sort 4 arrays using different algorithms and print the sorted results:

```bash
{ 0, 1, 2, 3, 4, 8, 12, }
{ 0.04, 0.1, 0.11, 0.23, 0.3, 0.32, 0.92, }
{ 0, 1, 2, 3, 4, 8, 9, }
{ 36, 149, 293, 371, 395, 836, 937, }
```

---

## 🧠 Concepts Demonstrated

* C# generics with constraints (`where T : IComparable<T>`)
* Sorting algorithms in a reusable static class
* Use of `System.Numerics` interfaces for numeric type constraints (`IFloatingPoint<T>`, `IBinaryInteger<T>`)
* Stability and in-place sorting
* Separation of concerns (comparable vs non-comparable types)

---

## 📜 License

MIT — free to use, modify, and distribute.

---

## 🙌 Acknowledgements

* [CLRS — Introduction to Algorithms](https://mitpress.mit.edu/9780262046305/introduction-to-algorithms/) for algorithm references
* .NET 7+ for generic math and numeric interfaces

