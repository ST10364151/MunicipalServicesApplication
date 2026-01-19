using System;
using System.Collections.Generic;
using System.Linq;

namespace MunicipalServiceAppPart1.DataStructures
{
    // Minimum Spanning Tree implementations
    public class MinimumSpanningTree<T>
    {
        // Kruskal's Algorithm implementation
        public static List<MSTEdge<T>> KruskalMST(List<T> vertices, List<MSTEdge<T>> edges)
        {
            List<MSTEdge<T>> mst = new List<MSTEdge<T>>();

            // Sort edges by weight
            var sortedEdges = edges.OrderBy(e => e.Weight).ToList();

            // Create disjoint set
            DisjointSet<T> disjointSet = new DisjointSet<T>(vertices);

            foreach (var edge in sortedEdges)
            {
                T root1 = disjointSet.Find(edge.Source);
                T root2 = disjointSet.Find(edge.Destination);

                // If including this edge doesn't create a cycle
                if (!root1.Equals(root2))
                {
                    mst.Add(edge);
                    disjointSet.Union(edge.Source, edge.Destination);
                }

                // MST is complete when we have V-1 edges
                if (mst.Count == vertices.Count - 1)
                    break;
            }

            return mst;
        }

        // Prim's Algorithm implementation
        public static List<MSTEdge<T>> PrimMST(Dictionary<T, List<MSTEdge<T>>> adjacencyList, T startVertex)
        {
            List<MSTEdge<T>> mst = new List<MSTEdge<T>>();
            HashSet<T> visited = new HashSet<T>();

            // Priority queue to select minimum weight edge
            SortedSet<MSTEdge<T>> priorityQueue = new SortedSet<MSTEdge<T>>(new EdgeComparer<T>());

            // Start from the given vertex
            visited.Add(startVertex);

            // Add all edges from start vertex to priority queue
            if (adjacencyList.ContainsKey(startVertex))
            {
                foreach (var edge in adjacencyList[startVertex])
                {
                    priorityQueue.Add(edge);
                }
            }

            while (priorityQueue.Count > 0 && visited.Count < adjacencyList.Count)
            {
                // Get minimum weight edge
                MSTEdge<T> minEdge = priorityQueue.Min;
                priorityQueue.Remove(minEdge);

                // If destination is already visited, skip
                if (visited.Contains(minEdge.Destination))
                    continue;

                // Add edge to MST
                mst.Add(minEdge);
                visited.Add(minEdge.Destination);

                // Add all edges from newly added vertex
                if (adjacencyList.ContainsKey(minEdge.Destination))
                {
                    foreach (var edge in adjacencyList[minEdge.Destination])
                    {
                        if (!visited.Contains(edge.Destination))
                        {
                            priorityQueue.Add(edge);
                        }
                    }
                }
            }

            return mst;
        }

        // Calculate total weight of MST
        public static int CalculateMSTWeight(List<MSTEdge<T>> mst)
        {
            return mst.Sum(edge => edge.Weight);
        }

        // Check if MST is valid (has V-1 edges)
        public static bool IsValidMST(List<MSTEdge<T>> mst, int vertexCount)
        {
            return mst.Count == vertexCount - 1;
        }
    }

    // MST Edge class
    public class MSTEdge<T>
    {
        public T Source { get; set; }
        public T Destination { get; set; }
        public int Weight { get; set; }

        public MSTEdge(T source, T destination, int weight)
        {
            Source = source;
            Destination = destination;
            Weight = weight;
        }

        public override string ToString()
        {
            return $"{Source} -- {Weight} --> {Destination}";
        }
    }

    // Edge Comparer for SortedSet
    public class EdgeComparer<T> : IComparer<MSTEdge<T>>
    {
        public int Compare(MSTEdge<T> x, MSTEdge<T> y)
        {
            int weightComparison = x.Weight.CompareTo(y.Weight);
            if (weightComparison != 0)
                return weightComparison;

            // If weights are equal, compare by source and destination to maintain uniqueness
            int sourceComparison = Comparer<T>.Default.Compare(x.Source, y.Source);
            if (sourceComparison != 0)
                return sourceComparison;

            return Comparer<T>.Default.Compare(x.Destination, y.Destination);
        }
    }

    // Disjoint Set (Union-Find) for Kruskal's algorithm
    public class DisjointSet<T>
    {
        private Dictionary<T, T> parent;
        private Dictionary<T, int> rank;

        public DisjointSet(List<T> vertices)
        {
            parent = new Dictionary<T, T>();
            rank = new Dictionary<T, int>();

            foreach (var vertex in vertices)
            {
                parent[vertex] = vertex;
                rank[vertex] = 0;
            }
        }

        // Find with path compression
        public T Find(T item)
        {
            if (!parent[item].Equals(item))
            {
                parent[item] = Find(parent[item]); // Path compression
            }
            return parent[item];
        }

        // Union by rank
        public void Union(T item1, T item2)
        {
            T root1 = Find(item1);
            T root2 = Find(item2);

            if (root1.Equals(root2))
                return;

            // Union by rank
            if (rank[root1] < rank[root2])
            {
                parent[root1] = root2;
            }
            else if (rank[root1] > rank[root2])
            {
                parent[root2] = root1;
            }
            else
            {
                parent[root2] = root1;
                rank[root1]++;
            }
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
