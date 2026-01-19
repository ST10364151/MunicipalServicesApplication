using System;
using System.Collections.Generic;

namespace MunicipalServiceAppPart1
{
    // Red-Black Tree Node
    public enum NodeColor { Red, Black }

    public class RBNode
    {
        public ServiceRequest Data { get; set; }
        public RBNode Left { get; set; }
        public RBNode Right { get; set; }
        public RBNode Parent { get; set; }
        public NodeColor Color { get; set; }

        public RBNode(ServiceRequest data)
        {
            Data = data;
            Left = null;
            Right = null;
            Parent = null;
            Color = NodeColor.Red;
        }
    }

    // Red-Black Tree Implementation
    public class RedBlackTree
    {
        private RBNode root;
        private RBNode nil; // Sentinel node

        public RedBlackTree()
        {
            nil = new RBNode(null) { Color = NodeColor.Black };
            root = nil;
        }

        // Left rotate
        private void LeftRotate(RBNode x)
        {
            RBNode y = x.Right;
            x.Right = y.Left;

            if (y.Left != nil)
                y.Left.Parent = x;

            y.Parent = x.Parent;

            if (x.Parent == nil)
                root = y;
            else if (x == x.Parent.Left)
                x.Parent.Left = y;
            else
                x.Parent.Right = y;

            y.Left = x;
            x.Parent = y;
        }

        // Right rotate
        private void RightRotate(RBNode x)
        {
            RBNode y = x.Left;
            x.Left = y.Right;

            if (y.Right != nil)
                y.Right.Parent = x;

            y.Parent = x.Parent;

            if (x.Parent == nil)
                root = y;
            else if (x == x.Parent.Right)
                x.Parent.Right = y;
            else
                x.Parent.Left = y;

            y.Right = x;
            x.Parent = y;
        }

        // Insert a service request
        public void Insert(ServiceRequest request)
        {
            RBNode node = new RBNode(request);
            node.Left = nil;
            node.Right = nil;

            RBNode y = nil;
            RBNode x = root;

            while (x != nil)
            {
                y = x;
                if (string.Compare(node.Data.RequestId, x.Data.RequestId) < 0)
                    x = x.Left;
                else
                    x = x.Right;
            }

            node.Parent = y;

            if (y == nil)
                root = node;
            else if (string.Compare(node.Data.RequestId, y.Data.RequestId) < 0)
                y.Left = node;
            else
                y.Right = node;

            node.Color = NodeColor.Red;
            InsertFixup(node);
        }

        // Fix Red-Black Tree properties after insertion
        private void InsertFixup(RBNode z)
        {
            while (z.Parent.Color == NodeColor.Red)
            {
                if (z.Parent == z.Parent.Parent.Left)
                {
                    RBNode y = z.Parent.Parent.Right;
                    if (y.Color == NodeColor.Red)
                    {
                        z.Parent.Color = NodeColor.Black;
                        y.Color = NodeColor.Black;
                        z.Parent.Parent.Color = NodeColor.Red;
                        z = z.Parent.Parent;
                    }
                    else
                    {
                        if (z == z.Parent.Right)
                        {
                            z = z.Parent;
                            LeftRotate(z);
                        }
                        z.Parent.Color = NodeColor.Black;
                        z.Parent.Parent.Color = NodeColor.Red;
                        RightRotate(z.Parent.Parent);
                    }
                }
                else
                {
                    RBNode y = z.Parent.Parent.Left;
                    if (y.Color == NodeColor.Red)
                    {
                        z.Parent.Color = NodeColor.Black;
                        y.Color = NodeColor.Black;
                        z.Parent.Parent.Color = NodeColor.Red;
                        z = z.Parent.Parent;
                    }
                    else
                    {
                        if (z == z.Parent.Left)
                        {
                            z = z.Parent;
                            RightRotate(z);
                        }
                        z.Parent.Color = NodeColor.Black;
                        z.Parent.Parent.Color = NodeColor.Red;
                        LeftRotate(z.Parent.Parent);
                    }
                }
            }
            root.Color = NodeColor.Black;
        }

        // Search for a service request
        public ServiceRequest Search(string requestId)
        {
            RBNode node = SearchNode(root, requestId);
            return node != nil ? node.Data : null;
        }

        private RBNode SearchNode(RBNode node, string requestId)
        {
            if (node == nil || node.Data.RequestId == requestId)
                return node;

            if (string.Compare(requestId, node.Data.RequestId) < 0)
                return SearchNode(node.Left, requestId);
            else
                return SearchNode(node.Right, requestId);
        }

        // In-order traversal
        public List<ServiceRequest> InOrderTraversal()
        {
            List<ServiceRequest> result = new List<ServiceRequest>();
            InOrderRec(root, result);
            return result;
        }

        private void InOrderRec(RBNode node, List<ServiceRequest> result)
        {
            if (node != nil)
            {
                InOrderRec(node.Left, result);
                result.Add(node.Data);
                InOrderRec(node.Right, result);
            }
        }
    }
}

