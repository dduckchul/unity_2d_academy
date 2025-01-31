using System;
using System.Collections.Generic;

namespace AdvancedSort
{
    internal class Program
    {
        // 쉘 소트 : 삽입 정렬 개선 버전 알고리즘
        // 멀리 떨어진 요소를 비교하고, 점점 간격을 줄여가며 정렬하는 방식
        static void ShellSort(List<int> arr)
        {
            int n = arr.Count;

            for (int gap = n / 2; gap > 0; gap /= 2) // 간격을 반씩 줄인다.
            {
                for (int i = gap; i < n; i++)
                {
                    int key = arr[i];
                    int j = i;
                    while (j >= gap && arr[j - gap] > key) // 현재 key 보다 큰 값이 있으면 뒤로 밀어준다.
                    {
                        arr[j] = arr[j - gap];
                        j -= gap; // 이걸 계속 하면서 이전 간격 만큼 이동하며 비교한다.
                    }
                    arr[j] = key; // 적절한 위치 찾았으면 삽입
                }
            }
        }
        
        public static void Main(string[] args)
        {
            // 1. 간격을 정해서 정렬 수행 : 처음엔 큰 간격으로 원소들을 정렬
            // 2. 간격을 줄이며 반복 : 점점 Gap 을 줄인다.
            // 3. 마지막에 다다르면 거의 정렬된 상태여서 삽입 정렬보다 빠르다.

            List<int> arr = new List<int> { 8, 4, 1, 6, 2, 9, 5 };
            ShellSort(arr);

            foreach (int ele in arr)
            {
                Console.Write(ele);
            }
        }
    }
}