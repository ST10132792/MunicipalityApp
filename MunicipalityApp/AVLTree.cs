using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalityApp
{
    public class AVLNode<T> where T : IComparable<T>
    {
        public T Data { get; set; }
        public AVLNode<T> Left { get; set; }
        public AVLNode<T> Right { get; set; }
        public int Height { get; set; }

        public AVLNode(T data)
        {
            Data = data;
            Height = 1;
        }
    }

    public class AVLTree<T> where T : IComparable<T>
    {
        private AVLNode<T> root;

        private int Height(AVLNode<T> node)
        {
            return node == null ? 0 : node.Height;
        }

        private int GetBalance(AVLNode<T> node)
        {
            return node == null ? 0 : Height(node.Left) - Height(node.Right);
        }

        private AVLNode<T> RightRotate(AVLNode<T> y)
        {
            AVLNode<T> x = y.Left;
            AVLNode<T> T2 = x.Right;

            x.Right = y;
            y.Left = T2;

            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;
            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;

            return x;
        }

        private AVLNode<T> LeftRotate(AVLNode<T> x)
        {
            AVLNode<T> y = x.Right;
            AVLNode<T> T2 = y.Left;

            y.Left = x;
            x.Right = T2;

            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;
            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;

            return y;
        }

        public void Insert(T data)
        {
            root = InsertRec(root, data);
        }

        private AVLNode<T> InsertRec(AVLNode<T> node, T data)
        {
            if (node == null)
                return new AVLNode<T>(data);

            if (data.CompareTo(node.Data) < 0)
                node.Left = InsertRec(node.Left, data);
            else if (data.CompareTo(node.Data) > 0)
                node.Right = InsertRec(node.Right, data);
            else
                return node;

            node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));

            int balance = GetBalance(node);

            // Left Left Case
            if (balance > 1 && data.CompareTo(node.Left.Data) < 0)
                return RightRotate(node);

            // Right Right Case
            if (balance < -1 && data.CompareTo(node.Right.Data) > 0)
                return LeftRotate(node);

            // Left Right Case
            if (balance > 1 && data.CompareTo(node.Left.Data) > 0)
            {
                node.Left = LeftRotate(node.Left);
                return RightRotate(node);
            }

            // Right Left Case
            if (balance < -1 && data.CompareTo(node.Right.Data) < 0)
            {
                node.Right = RightRotate(node.Right);
                return LeftRotate(node);
            }

            return node;
        }

        public T Find(T data)
        {
            AVLNode<T> result = FindRec(root, data);
            return result != null ? result.Data : default(T);
        }

        private AVLNode<T> FindRec(AVLNode<T> node, T data)
        {
            if (node == null || data.CompareTo(node.Data) == 0)
                return node;

            if (data.CompareTo(node.Data) < 0)
                return FindRec(node.Left, data);

            return FindRec(node.Right, data);
        }

        public List<T> InOrderTraversal()
        {
            List<T> result = new List<T>();
            InOrderTraversalRec(root, result);
            return result;
        }

        private void InOrderTraversalRec(AVLNode<T> node, List<T> result)
        {
            if (node != null)
            {
                InOrderTraversalRec(node.Left, result);
                result.Add(node.Data);
                InOrderTraversalRec(node.Right, result);
            }
        }
    }
}
