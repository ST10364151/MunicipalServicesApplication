using System;
using System.Collections.Generic;
using System.Linq;

namespace MunicipalServiceAppPart1
{
    // Graph implementation for service request dependencies
    public class ServiceRequestGraph
    {
        private Dictionary<string, List<string>> adjacencyList;
        private Dictionary<string, ServiceRequest> requests;

        public ServiceRequestGraph()
        {
            adjacencyList = new Dictionary<string, List<string>>();
            requests = new Dictionary<string, ServiceRequest>();
        }

        // Add a service request as a vertex
        public void AddVertex(ServiceRequest request)
        {
            if (!adjacencyList.ContainsKey(request.RequestId))
            {
                adjacencyList[request.RequestId] = new List<string>();
                requests[request.RequestId] = request;
            }
        }

        // Add a dependency edge (requestId1 depends on requestId2)
        public void AddEdge(string requestId1, string requestId2)
        {
            if (adjacencyList.ContainsKey(requestId1) && adjacencyList.ContainsKey(requestId2))
            {
                if (!adjacencyList[requestId1].Contains(requestId2))
                {
                    adjacencyList[requestId1].Add(requestId2);
                }
            }
        }

        // Get all dependencies for a request
        public List<ServiceRequest> GetDependencies(string requestId)
        {
            List<ServiceRequest> dependencies = new List<ServiceRequest>();
            if (adjacencyList.ContainsKey(requestId))
            {
                foreach (string depId in adjacencyList[requestId])
                {
                    if (requests.ContainsKey(depId))
                    {
                        dependencies.Add(requests[depId]);
                    }
                }
            }
            return dependencies;
        }

        // Breadth-First Search traversal
        public List<ServiceRequest> BFS(string startRequestId)
        {
            List<ServiceRequest> result = new List<ServiceRequest>();
            if (!adjacencyList.ContainsKey(startRequestId))
                return result;

            HashSet<string> visited = new HashSet<string>();
            Queue<string> queue = new Queue<string>();

            queue.Enqueue(startRequestId);
            visited.Add(startRequestId);

            while (queue.Count > 0)
            {
                string current = queue.Dequeue();
                if (requests.ContainsKey(current))
                {
                    result.Add(requests[current]);
                }

                foreach (string neighbor in adjacencyList[current])
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }

            return result;
        }

        // Depth-First Search traversal
        public List<ServiceRequest> DFS(string startRequestId)
        {
            List<ServiceRequest> result = new List<ServiceRequest>();
            if (!adjacencyList.ContainsKey(startRequestId))
                return result;

            HashSet<string> visited = new HashSet<string>();
            DFSRecursive(startRequestId, visited, result);
            return result;
        }

        private void DFSRecursive(string requestId, HashSet<string> visited, List<ServiceRequest> result)
        {
            visited.Add(requestId);
            if (requests.ContainsKey(requestId))
            {
                result.Add(requests[requestId]);
            }

            foreach (string neighbor in adjacencyList[requestId])
            {
                if (!visited.Contains(neighbor))
                {
                    DFSRecursive(neighbor, visited, result);
                }
            }
        }

        // Get all vertices (requests)
        public List<ServiceRequest> GetAllRequests()
        {
            return requests.Values.ToList();
        }

        // Check if there's a path between two requests
        public bool HasPath(string fromRequestId, string toRequestId)
        {
            if (!adjacencyList.ContainsKey(fromRequestId) || !adjacencyList.ContainsKey(toRequestId))
                return false;

            HashSet<string> visited = new HashSet<string>();
            Queue<string> queue = new Queue<string>();
            queue.Enqueue(fromRequestId);
            visited.Add(fromRequestId);

            while (queue.Count > 0)
            {
                string current = queue.Dequeue();
                if (current == toRequestId)
                    return true;

                foreach (string neighbor in adjacencyList[current])
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }

            return false;
        }

        // Get related requests (connected components)
        public List<ServiceRequest> GetRelatedRequests(string requestId)
        {
            return BFS(requestId);
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