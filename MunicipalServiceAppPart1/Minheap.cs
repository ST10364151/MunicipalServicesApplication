using System;
using System.Collections.Generic;

namespace MunicipalServiceAppPart1
{
    // Min Heap Implementation for Priority Queue
    public class Minheap
    {
        private List<ServiceRequest> heap;

        public Minheap()
        {
            heap = new List<ServiceRequest>();
        }

        public int Count => heap.Count;

        // Get parent index
        private int Parent(int index)
        {
            return (index - 1) / 2;
        }

        // Get left child index
        private int LeftChild(int index)
        {
            return 2 * index + 1;
        }

        // Get right child index
        private int RightChild(int index)
        {
            return 2 * index + 2;
        }

        // Swap two elements
        private void Swap(int i, int j)
        {
            ServiceRequest temp = heap[i];
            heap[i] = heap[j];
            heap[j] = temp;
        }

        // Insert a service request (lower priority number = higher priority)
        public void Insert(ServiceRequest request)
        {
            heap.Add(request);
            int current = heap.Count - 1;

            // Heapify up
            while (current > 0 && heap[current].Priority < heap[Parent(current)].Priority)
            {
                Swap(current, Parent(current));
                current = Parent(current);
            }
        }

        // Extract minimum (highest priority)
        public ServiceRequest ExtractMin()
        {
            if (heap.Count == 0)
                return null;

            ServiceRequest min = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);

            if (heap.Count > 0)
                MinHeapify(0);

            return min;
        }

        // Heapify down
        private void MinHeapify(int index)
        {
            int left = LeftChild(index);
            int right = RightChild(index);
            int smallest = index;

            if (left < heap.Count && heap[left].Priority < heap[smallest].Priority)
                smallest = left;

            if (right < heap.Count && heap[right].Priority < heap[smallest].Priority)
                smallest = right;

            if (smallest != index)
            {
                Swap(index, smallest);
                MinHeapify(smallest);
            }
        }

        // Peek at minimum without removing
        public ServiceRequest Peek()
        {
            return heap.Count > 0 ? heap[0] : null;
        }

        // Get all requests in heap (not sorted)
        public List<ServiceRequest> GetAllRequests()
        {
            return new List<ServiceRequest>(heap);
        }

        // Get requests sorted by priority
        public List<ServiceRequest> GetSortedByPriority()
        {
            List<ServiceRequest> sorted = new List<ServiceRequest>(heap);
            sorted.Sort((a, b) => a.Priority.CompareTo(b.Priority));
            return sorted;
        }
    }
}

//Bibliography
//College, I. V., 2025. PROG7312 Module-Manual / Module-Outline. Pretoria: Varsity College Pretoria.
//Geeks, G. f., 2025. Introduction to C# Windows Forms Applications. [Online] 
//Available at: https://www.geeksforgeeks.org/c-sharp/introduction-to-c-sharp-windows-forms-applications/
//[Accessed 8 September 2025].
//Microsoft, 2025.Tutorial: Create a Windows Forms app in Visual Studio with C#. [Online] 
//Available at: https://learn.microsoft.com/en-us/visualstudio/ide/create-csharp-winform-visual-studio?view=vs-2022
//[Accessed 8 September 2025].
//ProgrammingKnowledge2, 2023.Create Your First C# Windows Forms Application using Visual Studio. [Online]
//Available at: https://youtu.be/JSJ1Jl2aLJg?si=A4tSAz_ueBg5cOgf
//[Accessed 08 September 2025].
//BroCode, 2021. Tree data structures in 2 minutes. [Online]
//Available at: https://youtu.be/Etpc_-br5rl?si=tY2wjw9rX62kTGUD
//[Accessed 12 November 2025].