using System;
using System.Collections.Generic;

namespace MunicipalServiceAppPart1
{
    // Binary Search Tree Node
    public class BSTNode
    {
        public ServiceRequest Data { get; set; }
        public BSTNode Left { get; set; }
        public BSTNode Right { get; set; }

        public BSTNode(ServiceRequest data)
        {
            Data = data;
            Left = null;
            Right = null;
        }
    }

    // Binary Search Tree Implementation
    public class BinarySearchTree
    {
        public BSTNode Root { get; private set; }

        public BinarySearchTree()
        {
            Root = null;
        }

        // Insert a service request
        public void Insert(ServiceRequest request)
        {
            Root = InsertRec(Root, request);
        }

        private BSTNode InsertRec(BSTNode root, ServiceRequest request)
        {
            if (root == null)
            {
                root = new BSTNode(request);
                return root;
            }

            // Compare by RequestId (string comparison)
            if (string.Compare(request.RequestId, root.Data.RequestId) < 0)
            {
                root.Left = InsertRec(root.Left, request);
            }
            else if (string.Compare(request.RequestId, root.Data.RequestId) > 0)
            {
                root.Right = InsertRec(root.Right, request);
            }

            return root;
        }

        // Search for a service request by ID
        public ServiceRequest Search(string requestId)
        {
            return SearchRec(Root, requestId);
        }

        private ServiceRequest SearchRec(BSTNode root, string requestId)
        {
            if (root == null || root.Data.RequestId == requestId)
            {
                return root?.Data;
            }

            if (string.Compare(requestId, root.Data.RequestId) < 0)
            {
                return SearchRec(root.Left, requestId);
            }

            return SearchRec(root.Right, requestId);
        }

        // In-order traversal (returns sorted list)
        public List<ServiceRequest> InOrderTraversal()
        {
            List<ServiceRequest> result = new List<ServiceRequest>();
            InOrderRec(Root, result);
            return result;
        }

        private void InOrderRec(BSTNode root, List<ServiceRequest> result)
        {
            if (root != null)
            {
                InOrderRec(root.Left, result);
                result.Add(root.Data);
                InOrderRec(root.Right, result);
            }
        }

        // Get all requests
        public List<ServiceRequest> GetAllRequests()
        {
            return InOrderTraversal();
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