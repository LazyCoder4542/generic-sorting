using System.Numerics;

namespace Test.Lectures
{
    public static class Sorting<T>
        where T : notnull, IComparable<T>
    {
        #region merge sort
        private static void Merge(T[] A, int p, int q, int r)
        {
            int n1 = q - p + 1;
            int n2 = r - q;
            T[] L = new T[n1];
            T[] R = new T[n2];
            for (int _i = 0; _i < n1; _i++)
            {
                L[_i] = A[p + _i];
            }
            for (int _j = 0; _j < n2; _j++)
            {
                R[_j] = A[q + _j + 1];
            }
            int k = p,
                i = 0,
                j = 0;
            while (i < n1 || j < n2)
            {
                if (i < n1 && (j == n2 || L[i].CompareTo(R[j]) <= 0))
                {
                    A[k++] = L[i++];
                }
                else
                {
                    A[k++] = R[j++];
                }
            }
        }

        public static void MergeSort(T[] A)
        {
            int n = A.Length;
            MergeSort(A, 0, n - 1);
        }

        private static void MergeSort(T[] A, int p, int r)
        {
            if (p < r)
            {
                int m = (p + r) / 2;
                MergeSort(A, p, m);
                MergeSort(A, m + 1, r);
                Merge(A, p, m, r);
            }
        }
        #endregion

        #region insertion sort
        public static void InsertionSort(T[] A)
        {
            int n = A.Length;
            for (int i = 1; i < n; i++)
            {
                T key = A[i];
                int j = i - 1;
                while (j >= 0 && A[j].CompareTo(key) > 0)
                {
                    A[j + 1] = A[j];
                    j--;
                }
                A[j + 1] = key;
            }
        }
        #endregion

        #region selection sort
        public static void SelectionSort(T[] A)
        {
            int n = A.Length;
            for (int i = 0; i < n - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (A[j].CompareTo(A[min]) < 0)
                    {
                        min = j;
                    }
                }
                (A[i], A[min]) = (A[min], A[i]);
            }
        }
        #endregion

        #region heap sort
        private static void MaxHeapify(T[] A, int index, int heapSize)
        {
            int first = 2 * index;
            int second = first + 1;
            // Checks if node i is a parent node
            if (heapSize >= first)
            {
                int max = first;
                if (heapSize >= second && A[second - 1].CompareTo(A[max - 1]) > 0)
                    max = second;
                if (A[index - 1].CompareTo(A[max - 1]) < 0)
                {
                    (A[index - 1], A[max - 1]) = (A[max - 1], A[index - 1]);
                    MaxHeapify(A, max, heapSize);
                }
            }
        }

        private static void BuildMaxHeap(T[] A, int heapSize)
        {
            for (int i = heapSize / 2; i > 0; i--)
            {
                MaxHeapify(A, i, heapSize);
            }
        }

        public static void HeapSort(T[] A)
        {
            int n = A.Length;
            BuildMaxHeap(A, n);
            for (int i = n; i >= 2; i--)
            {
                (A[0], A[i - 1]) = (A[i - 1], A[0]);
                MaxHeapify(A, 1, i - 1);
            }
        }
        #endregion

        #region quick sort
        private static int Partition(T[] A, int p, int r)
        {
            T pivot = A[r];
            int i = p - 1;
            for (int j = p; j < r; j++)
            {
                if (A[j].CompareTo(pivot) <= 0)
                {
                    i++;
                    (A[i], A[j]) = (A[j], A[i]);
                }
            }
            (A[i + 1], A[r]) = (A[r], A[i + 1]);
            return i + 1;
        }

        private static void QuickSort(T[] A, int p, int r)
        {
            if (p < r)
            {
                int q = Partition(A, p, r);
                QuickSort(A, p, q - 1);
                QuickSort(A, q + 1, r);
            }
        }

        public static void QuickSort(T[] A)
        {
            int n = A.Length;
            QuickSort(A, 0, n - 1);
        }
        #endregion
    }

    public static class NonComparableSorting
    {
        #region bucket sort
        public static void BucketSort<TFloating>(TFloating[] A)
            where TFloating : IFloatingPoint<TFloating>
        {
            int n = A.Length;
            var buckets = new List<TFloating>[n];
            for (int i = 0; i < n; i++)
            {
                int id = int.CreateChecked(TFloating.Floor(TFloating.CreateChecked(n) * A[i]));
                if (id < 0 || id >= n)
                {
                    throw new InvalidOperationException(
                        "Numbers in A must be in [0, 1), reading " + A[i]
                    );
                }
                var bucket = buckets[
                    int.CreateChecked(TFloating.Floor(TFloating.CreateChecked(n) * A[i]))
                ];
                if (bucket == null)
                {
                    bucket = new List<TFloating>();
                }
                buckets[int.CreateChecked(TFloating.Floor(TFloating.CreateChecked(n) * A[i]))] =
                    bucket;
                bucket.Add(A[i]);
            }
            int index = 0;
            for (int i = 0; i < n; i++)
            {
                var bucket = buckets[i];
                if (bucket != null)
                {
                    var bucketArray = bucket.ToArray();
                    if (bucketArray.Length > 1)
                    {
                        Sorting<TFloating>.InsertionSort(bucketArray);
                    }
                    for (int j = 0; j < bucketArray.Length; j++)
                    {
                        A[index++] = bucketArray[j];
                    }
                }
            }
        }
        #endregion

        #region counting sort
        public static int[] CountingSort<T>(T[] A, int k)
            where T : IBinaryInteger<T>
        {
            int n = A.Length;
            return CountingSort<T>(A, new T[n], k);
        }

        public static int[] CountingSort<T>(T[] A, T[] B, int k)
            where T : IBinaryInteger<T>
        {
            int n = A.Length;
            int[] C = new int[k + 1];
            int[] map = new int[n];
            for (int i = 0; i < n; i++)
            {
                int item = int.CreateChecked(A[i]);
                C[item] += 1;
            }

            for (int i = 1; i <= k; i++)
            {
                C[i] += C[i - 1];
            }

            for (int i = n - 1; i >= 0; i--)
            {
                int item = int.CreateChecked(A[i]);

                map[--C[item]] = i;
                B[C[item]] = A[i];
            }

            return map;
        }
        #endregion

        #region radix sort
        private static void Reorder<T>(T[] A, int[] P)
        {
            int n = A.Length;
            for (int i = 0; i < n; i++)
            {
                int next = i;

                while (P[next] > i)
                {
                    int temp = P[next];

                    (A[next], A[temp]) = (A[temp], A[next]);

                    P[next] = -1;

                    next = temp;
                }
            }
        }

        public static void RadixSort(int[] A, int k, int r = 10)
        {
            int n = A.Length;
            int[] digits = new int[n];

            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    digits[j] = (A[j] / ((int)Math.Pow(10, i))) % 10;
                }

                int[] sortingMap = CountingSort<int>(digits, 9);

                Reorder<int>(A, sortingMap);
            }
        }
        #endregion
    }
}
