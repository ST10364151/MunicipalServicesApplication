using System;
using System.Collections.Generic;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace MunicipalServiceAppPart1
{
    // AVL Tree Node
    public class AVLNode
    {
        public ServiceRequest Data { get; set; }
        public AVLNode Left { get; set; }
        public AVLNode Right { get; set; }
        public int Height { get; set; }

        public AVLNode(ServiceRequest data)
        {
            Data = data;
            Left = null;
            Right = null;
            Height = 1;
        }
    }

    // AVL Tree Implementation (Self-Balancing BST)
    public class AVLTree
    {
        public AVLNode Root { get; private set; }

        public AVLTree()
        {
            Root = null;
        }

        // Get height of node
        private int Height(AVLNode node)
        {
            return node == null ? 0 : node.Height;
        }

        // Get balance factor
        private int GetBalance(AVLNode node)
        {
            return node == null ? 0 : Height(node.Left) - Height(node.Right);
        }

        // Right rotate
        private AVLNode RightRotate(AVLNode y)
        {
            AVLNode x = y.Left;
            AVLNode T2 = x.Right;

            x.Right = y;
            y.Left = T2;

            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;
            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;

            return x;
        }

        // Left rotate
        private AVLNode LeftRotate(AVLNode x)
        {
            AVLNode y = x.Right;
            AVLNode T2 = y.Left;

            y.Left = x;
            x.Right = T2;

            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;
            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;

            return y;
        }

        // Insert a service request
        public void Insert(ServiceRequest request)
        {
            Root = InsertRec(Root, request);
        }

        private AVLNode InsertRec(AVLNode node, ServiceRequest request)
        {
            if (node == null)
                return new AVLNode(request);

            int comparison = string.Compare(request.RequestId, node.Data.RequestId);

            if (comparison < 0)
                node.Left = InsertRec(node.Left, request);
            else if (comparison > 0)
                node.Right = InsertRec(node.Right, request);
            else
                return node; // Duplicate keys not allowed

            // Update height
            node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));

            // Get balance factor
            int balance = GetBalance(node);

            // Left Left Case
            if (balance > 1 && string.Compare(request.RequestId, node.Left.Data.RequestId) < 0)
                return RightRotate(node);

            // Right Right Case
            if (balance < -1 && string.Compare(request.RequestId, node.Right.Data.RequestId) > 0)
                return LeftRotate(node);

            // Left Right Case
            if (balance > 1 && string.Compare(request.RequestId, node.Left.Data.RequestId) > 0)
            {
                node.Left = LeftRotate(node.Left);
                return RightRotate(node);
            }

            // Right Left Case
            if (balance < -1 && string.Compare(request.RequestId, node.Right.Data.RequestId) < 0)
            {
                node.Right = RightRotate(node.Right);
                return LeftRotate(node);
            }

            return node;
        }

        // Search for a service request
        public ServiceRequest Search(string requestId)
        {
            return SearchRec(Root, requestId);
        }

        private ServiceRequest SearchRec(AVLNode node, string requestId)
        {
            if (node == null)
                return null;

            int comparison = string.Compare(requestId, node.Data.RequestId);

            if (comparison == 0)
                return node.Data;
            else if (comparison < 0)
                return SearchRec(node.Left, requestId);
            else
                return SearchRec(node.Right, requestId);
        }

        // In-order traversal
        public List<ServiceRequest> InOrderTraversal()
        {
            List<ServiceRequest> result = new List<ServiceRequest>();
            InOrderRec(Root, result);
            return result;
        }

        private void InOrderRec(AVLNode node, List<ServiceRequest> result)
        {
            if (node != null)
            {
                InOrderRec(node.Left, result);
                result.Add(node.Data);
                InOrderRec(node.Right, result);
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

