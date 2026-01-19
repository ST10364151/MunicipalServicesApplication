using System;
using System.Collections.Generic;
using System.Linq;

namespace MunicipalServiceAppPart1
{
    public class ServiceRequestManager
    {
        private BinarySearchTree bst;
        private AVLTree avlTree;
        private Minheap priorityQueue;
        private ServiceRequestGraph dependencyGraph;
        private RedBlackTree rbTree;
        private List<ServiceRequest> allRequests;

        public ServiceRequestManager()
        {
            bst = new BinarySearchTree();
            avlTree = new AVLTree();
            priorityQueue = new Minheap();
            dependencyGraph = new ServiceRequestGraph();
            rbTree = new RedBlackTree();
            allRequests = new List<ServiceRequest>();

            // Initialize with some sample data
            InitializeSampleData();
        }

        private void InitializeSampleData()
        {
            // Create sample service requests in MIXED order (not sorted by priority)
            // Users will see requests in submission order initially
            ServiceRequest[] sampleRequests = new ServiceRequest[]
            {
                // Mixed order by submission date - NOT sorted by priority
                new ServiceRequest("SR023", "Community garden maintenance", "Parks", "Community Garden", DateTime.Now.AddDays(-20), 4),
                new ServiceRequest("SR022", "Request for additional street bins", "Sanitation", "Shopping District", DateTime.Now.AddDays(-15), 4),
                new ServiceRequest("SR017", "Faded road markings need repainting", "Roads", "Highland Road", DateTime.Now.AddDays(-14), 3),
                new ServiceRequest("SR024", "Flower bed needs replanting", "Parks", "City Hall Garden", DateTime.Now.AddDays(-13), 4),
                new ServiceRequest("SR011", "Multiple potholes on residential street", "Roads", "Sunset Boulevard", DateTime.Now.AddDays(-12), 2),
                new ServiceRequest("SR012", "Graffiti on public building wall", "Infrastructure", "Community Center", DateTime.Now.AddDays(-11), 3),
                new ServiceRequest("SR001", "Large pothole on Main Street causing vehicle damage", "Roads", "Main Street", DateTime.Now.AddDays(-10), 1),
                new ServiceRequest("SR008", "Sidewalk cracked but still walkable", "Roads", "Birch Street", DateTime.Now.AddDays(-9), 3),
                new ServiceRequest("SR021", "Suggestion for new park equipment", "Parks", "Children's Park", DateTime.Now.AddDays(-8), 4),
                new ServiceRequest("SR002", "Street light not working on busy intersection", "Utilities", "Oak Avenue", DateTime.Now.AddDays(-8), 2),
                new ServiceRequest("SR014", "Park fountain not working", "Parks", "Memorial Park", DateTime.Now.AddDays(-7), 3),
                new ServiceRequest("SR005", "Traffic sign damaged and unreadable", "Roads", "Maple Drive", DateTime.Now.AddDays(-7), 2),
                new ServiceRequest("SR006", "Park bench needs minor repair", "Parks", "Central Park", DateTime.Now.AddDays(-6), 4),
                new ServiceRequest("SR003", "Major water pipe burst flooding residential area", "Water", "Pine Road", DateTime.Now.AddDays(-5), 1),
                new ServiceRequest("SR019", "Public toilet facility needs cleaning", "Sanitation", "Beach Park", DateTime.Now.AddDays(-5), 3),
                new ServiceRequest("SR018", "Vandalized bus stop needs repair", "Infrastructure", "Transit Hub", DateTime.Now.AddDays(-4), 2),
                new ServiceRequest("SR007", "Severe sewer blockage affecting multiple homes", "Sanitation", "Cedar Lane", DateTime.Now.AddDays(-4), 1),
                new ServiceRequest("SR004", "Missed garbage collection on schedule", "Sanitation", "Elm Street", DateTime.Now.AddDays(-3), 3),
                new ServiceRequest("SR013", "Broken fire hydrant leaking water", "Water", "Liberty Street", DateTime.Now.AddDays(-3), 2),
                new ServiceRequest("SR009", "Power outage affecting entire neighborhood", "Utilities", "Willow Way", DateTime.Now.AddDays(-2), 1),
                new ServiceRequest("SR016", "Overflowing public trash bins", "Sanitation", "City Center", DateTime.Now.AddDays(-2), 2),
                new ServiceRequest("SR010", "Tree trimming needed in park", "Parks", "Oak Avenue Park", DateTime.Now.AddDays(-1), 4),
                new ServiceRequest("SR015", "Gas leak reported near shopping center", "Utilities", "Market Square", DateTime.Now.AddDays(-1), 1),
                new ServiceRequest("SR025", "Bike rack installation request", "Infrastructure", "Train Station", DateTime.Now.AddHours(-18), 4),
                new ServiceRequest("SR020", "Dangerous tree falling onto roadway", "Parks", "Forest Avenue", DateTime.Now.AddHours(-6), 1)
            };

            // Set realistic statuses based on priority and age
            sampleRequests[0].Status = "In Progress";  // SR001 - High priority, older
            sampleRequests[1].Status = "In Progress";  // SR003 - High priority
            sampleRequests[2].Status = "In Progress";  // SR007 - High priority
            sampleRequests[3].Status = "Completed";    // SR009 - High priority, completed
            sampleRequests[4].Status = "In Progress";  // SR015 - High priority, recent
            sampleRequests[5].Status = "Pending";      // SR020 - High priority, very recent

            sampleRequests[6].Status = "Completed";    // SR002 - Medium, completed
            sampleRequests[7].Status = "In Progress";  // SR005 - Medium
            sampleRequests[8].Status = "Pending";      // SR011 - Medium
            sampleRequests[9].Status = "In Progress";  // SR013 - Medium
            sampleRequests[10].Status = "Pending";     // SR016 - Medium
            sampleRequests[11].Status = "Pending";     // SR018 - Medium

            sampleRequests[12].Status = "Completed";   // SR004 - Low
            sampleRequests[13].Status = "Pending";     // SR008 - Low
            sampleRequests[14].Status = "In Progress"; // SR012 - Low
            sampleRequests[15].Status = "Pending";     // SR014 - Low
            sampleRequests[16].Status = "Pending";     // SR017 - Low
            sampleRequests[17].Status = "Completed";   // SR019 - Low

            sampleRequests[18].Status = "Pending";     // SR006 - Very Low
            sampleRequests[19].Status = "Pending";     // SR010 - Very Low
            sampleRequests[20].Status = "Pending";     // SR021 - Very Low
            sampleRequests[21].Status = "Pending";     // SR022 - Very Low
            sampleRequests[22].Status = "In Progress"; // SR023 - Very Low
            sampleRequests[23].Status = "Pending";     // SR024 - Very Low
            sampleRequests[24].Status = "Pending";     // SR025 - Very Low

            foreach (var request in sampleRequests)
            {
                AddServiceRequest(request);
            }

            // Add realistic dependencies
            dependencyGraph.AddEdge("SR007", "SR003"); // Sewer blockage depends on water pipe fix
            dependencyGraph.AddEdge("SR008", "SR001"); // Sidewalk repair depends on pothole repair
            dependencyGraph.AddEdge("SR005", "SR001"); // Traffic sign depends on road repair
            dependencyGraph.AddEdge("SR013", "SR003"); // Fire hydrant depends on water pipe fix
            dependencyGraph.AddEdge("SR011", "SR001"); // Multiple potholes related to main street repair
        }

        // Add a new service request to all data structures
        public void AddServiceRequest(ServiceRequest request)
        {
            allRequests.Add(request);
            bst.Insert(request);
            avlTree.Insert(request);
            priorityQueue.Insert(request);
            dependencyGraph.AddVertex(request);
            rbTree.Insert(request);
        }

        // Search for a request by ID using BST
        public ServiceRequest SearchByIdBST(string requestId)
        {
            return bst.Search(requestId);
        }

        // Search for a request by ID using AVL Tree
        public ServiceRequest SearchByIdAVL(string requestId)
        {
            return avlTree.Search(requestId);
        }

        // Search for a request by ID using Red-Black Tree
        public ServiceRequest SearchByIdRBT(string requestId)
        {
            return rbTree.Search(requestId);
        }

        // Get all requests sorted by ID (using BST)
        public List<ServiceRequest> GetAllRequestsSorted()
        {
            return bst.GetAllRequests();
        }

        // Get requests sorted by priority (using MinHeap)
        public List<ServiceRequest> GetRequestsByPriority()
        {
            return priorityQueue.GetSortedByPriority();
        }

        // Get the highest priority request
        public ServiceRequest GetHighestPriorityRequest()
        {
            return priorityQueue.Peek();
        }

        // Get requests by status
        public List<ServiceRequest> GetRequestsByStatus(string status)
        {
            return allRequests.Where(r => r.Status.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        // Get requests by category
        public List<ServiceRequest> GetRequestsByCategory(string category)
        {
            return allRequests.Where(r => r.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        // Get all requests in original order (not sorted)
        public List<ServiceRequest> GetAllRequests()
        {
            return new List<ServiceRequest>(allRequests);
        }

        // Get dependencies for a request
        public List<ServiceRequest> GetRequestDependencies(string requestId)
        {
            return dependencyGraph.GetDependencies(requestId);
        }

        // Get related requests (using graph traversal)
        public List<ServiceRequest> GetRelatedRequests(string requestId)
        {
            return dependencyGraph.GetRelatedRequests(requestId);
        }

        // Update request status
        public void UpdateRequestStatus(string requestId, string newStatus)
        {
            var request = allRequests.FirstOrDefault(r => r.RequestId == requestId);
            if (request != null)
            {
                request.Status = newStatus;
            }
        }

        // Get statistics
        public Dictionary<string, int> GetStatusStatistics()
        {
            return allRequests.GroupBy(r => r.Status)
                             .ToDictionary(g => g.Key, g => g.Count());
        }

        public Dictionary<string, int> GetCategoryStatistics()
        {
            return allRequests.GroupBy(r => r.Category)
                             .ToDictionary(g => g.Key, g => g.Count());
        }

        // Search requests by multiple criteria
        public List<ServiceRequest> SearchRequests(string searchTerm)
        {
            return allRequests.Where(r =>
                r.RequestId.Contains(searchTerm) ||
                r.Description.Contains(searchTerm) ||
                r.Location.Contains(searchTerm) ||
                r.Category.Contains(searchTerm) ||
                r.Status.Contains(searchTerm)
            ).ToList();
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