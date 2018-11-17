using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace LinkedListApp
{
    class Tree234<T> where T : IComparable<T>
    {
        class Node
        {
            public readonly T[] values = new T[3];
            public readonly Node[] nextNodes = new Node[4];

            public int Count { get; set; }

            public Node() { }

            public Node(Node left, Node right, T value)
            {
                values[0] = value;
                Count++;
                //values.Add(value);
                nextNodes[0] = left;
                nextNodes[1] = right;
            }

            public void AddNodeData(Node node)
            {
                if (Count > 2) throw new Exception();
                var targetPos = GetTargetPos(node.values[0]);
                InsertNode(targetPos, node);
            }

            //Предполагается, что здесь не будет нижележащих узлов
            public void AddValue(T value)
            {
                if (Count > 2) throw new Exception();
                var targetPos = GetTargetPos(value);
                InsertValue(targetPos, value);
                //values.Insert(targetPos, value);
            }

            public int GetTargetPos(T value)
            {
                for (int i = 0; i < Count; i++)
                {
                    if (values[i].CompareTo(value) > 0)
                    {
                        return i;
                    }
                }
                return Count;
            }

            private void InsertNode(int index, Node node)
            {
                if (index < 2)
                {

                }
                InsertValue(index, node.values[0]);
                //values.Insert(index, node.values[0]);
                MoveNodes(index, 1);
                nextNodes[index] = node.nextNodes[0];
                nextNodes[index + 1] = node.nextNodes[1];
            }

            private void MoveNodes(int pos, int count)
            {
                for (var i = 3 - count; i >= pos; i--)
                    nextNodes[i + count] = nextNodes[i];
            }

            private static void MoveValues(T[] arr, int pos)
            {
                var capacity = arr.Length;
                for (var i = capacity-2; i >= pos; i--)
                    arr[i + 1] = arr[i];
            }

            private void InsertValue(int index, T value)
            {
                MoveValues(values, index);
                values[index] = value;
                Count++;
            }
        }

        Node _root = new Node();

        int _deep = 0;

        private void Add(Node node, Node parent, T value, int high)
        {
            if (node.Count == 3)
            {
                var splittedNode = Split(node);

                if (parent == null)
                {
                    _root = splittedNode;
                    _deep++;
                    node = splittedNode;
                }
                else
                {
                    node = parent;
                    parent.AddNodeData(splittedNode);
                }
                high++;
            }

            if (high == 0)
            {
                node.AddValue(value);
                return;
            }

            var targetPos = node.GetTargetPos(value);
            Add(node.nextNodes[targetPos], node, value, high - 1);
        }

        static Node Split(Node node)
        {
            if (node.Count != 3) throw new ArgumentException("Node type is not 3-node");

            return new Node
                (
                    new Node(node.nextNodes[0], node.nextNodes[1], node.values[0]),
                    new Node(node.nextNodes[2], node.nextNodes[3], node.values[2]),
                    node.values[1]
                );
        }

        public void Add(T value) => Add(_root, null, value, _deep);

        static void PrintTree(Node root, ref int counter, StringBuilder stringBuilder)
        {
            for (int i = 0; i < root.Count; i++)
            {
                if (root.nextNodes[i] != null)
                {
                    PrintTree(root.nextNodes[i], ref counter, stringBuilder);
                }
                stringBuilder.Append(root.values[i]).Append(' ');
                counter++;
            }
            if (root.nextNodes[root.Count] != null)
            {
                PrintTree(root.nextNodes[root.Count], ref counter, stringBuilder);
            }
        }

        public override string ToString()
        {
            int counter = 0;
            var sb = new StringBuilder();
            PrintTree(_root, ref counter, sb);
            return sb.ToString();
        }
    }
}