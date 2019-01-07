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
            public readonly T[] _values = new T[3];
            public readonly Node[] _nextNodes = new Node[4];

            public int Count { get; set; }

            public Node() { }

            public Node(Node left, Node right, T value)
            {
                _values[0] = value;
                Count++;

                _nextNodes[0] = left;
                _nextNodes[1] = right;
            }

            public Node(T[] values, Node[] nextNodes, int count)
            {
                _values = values;
                _nextNodes = nextNodes;
                Count = count;
            }

            public void AddNodeData(Node node)
            {
                if (Count > 2) throw new Exception();
                var targetPos = GetTargetPos(node._values[0]);
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
                    if (_values[i].CompareTo(value) > 0)
                    {
                        return i;
                    }
                }
                return Count;
            }

            public bool RemoveKey(T value)
            {
                for (int i = 0; i < Count; i++)
                {
                    if (value.CompareTo(_values[i]) == 0)
                    {
                        RemoveAtPostion(i);
                        return true;
                    }
                }
                return false;
            }

            public void MergeChild(int pos)
            {
                var tempValues = new T[3] 
                {
                    _nextNodes[pos]._values[0],
                    _values[pos],
                    _nextNodes[pos+1]._values[0],
                };

                var tempNodes = new Node[4]
                {
                    _nextNodes[pos]._nextNodes[0],
                    _nextNodes[pos]._nextNodes[1],
                    _nextNodes[pos+1]._nextNodes[0],
                    _nextNodes[pos+1]._nextNodes[0],
                };

                _nextNodes[pos] = new Node(tempValues, tempNodes, 3);

                Count--;

                for (int i = pos+1; i < Count; i++)
                {
                    _values[i] = _values[i + 1];
                }

                for (int i = pos + 1; i <= Count; i++)
                {
                    _nextNodes[i] = _nextNodes[i + 1];
                }
            }

            public void TransactRight(int pos)
            {
                var right = _nextNodes[pos + 1];
                var left = _nextNodes[pos];

                right._values[1] = right._values[0];
                right._values[0] = _values[pos];

                for (int i = 0; i < 3; i++)
                {
                    right._nextNodes[i + 1] = right._nextNodes[i];
                }

                right._nextNodes[0] = left._nextNodes[left.Count];
                right.Count++;

                _values[pos] = left._values[left.Count - 1];

                left.Count--;
            }

            public void TransactLeft(int pos)
            {
                var right = _nextNodes[pos + 1];
                var left = _nextNodes[pos];

                left.Count++;

                left._values[left.Count - 1] = _values[pos];
                left._nextNodes[left.Count] = right._nextNodes[0];

                _values[pos] = right._values[0];

                right._values[0] = right._values[1];
                for (int i = 0; i < 3; i++)
                {
                    right._nextNodes[i] = right._nextNodes[i + 1];
                }

                right.Count--;
            }

            private void RemoveAtPostion(int pos)
            {
                for (int j = pos; j < Count - 1; j++)
                {
                    _values[j] = _values[j + 1];
                }
                Count--;
            }

            private void InsertNode(int index, Node node)
            {
                InsertValue(index, node._values[0]);
                MoveNodes(index, 1);
                _nextNodes[index] = node._nextNodes[0];
                _nextNodes[index + 1] = node._nextNodes[1];
            }

            private void MoveNodes(int pos, int count)
            {
                for (var i = 3 - count; i >= pos; i--)
                    _nextNodes[i + count] = _nextNodes[i];
            }

            private static void MoveValues(T[] arr, int pos)
            {
                var capacity = arr.Length;
                for (var i = capacity-2; i >= pos; i--)
                    arr[i + 1] = arr[i];
            }

            private void InsertValue(int index, T value)
            {
                MoveValues(_values, index);
                _values[index] = value;
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
            Add(node._nextNodes[targetPos], node, value, high - 1);
        }

        private bool Remove(Node node, T value, int high)
        {
            if (high == _deep && _deep != 0 && node.Count == 1 && node._nextNodes[0].Count == 1
                && node._nextNodes[1].Count == 1)
            {
                var valuesTemp = new T[3] 
                {
                    node._nextNodes[0]._values[0],
                    node._values[0],
                    node._nextNodes[1]._values[0]
                };

                var nodesTemp = new Node[4]
                {
                    node._nextNodes[0]._nextNodes[0],
                    node._nextNodes[0]._nextNodes[1],
                    node._nextNodes[1]._nextNodes[0],
                    node._nextNodes[1]._nextNodes[1],
                };

                _deep--;
                _root = new Node(valuesTemp, nodesTemp, 3);

                return Remove(_root, value, _deep);
            }

            if(high == 0)
            {
                return node.RemoveKey(value);
            }

            if (node._values[0].CompareTo(value) > 0)
            {
                if (node._nextNodes[0].Count == 1 && node._nextNodes[1].Count == 1)
                {
                    node.MergeChild(0);
                }
                else if (node._nextNodes[1].Count == 1)
                {
                    node.TransactLeft(0);
                }

                return Remove(node._nextNodes[0], value, high - 1);
            }

            for (int i = node.Count - 1; i >= 0; i--)
            {
                if (node._nextNodes[i].Count == 1 && node._nextNodes[i + 1].Count == 1)
                {
                    node.MergeChild(i);
                }
                else if (node._nextNodes[i + 1].Count == 1)
                {
                    node.TransactRight(i);
                }

                if (node._values[i].CompareTo(value) == 0)
                {
                    node._values[i] = SwapWithLeaf(node._nextNodes[i + 1], value, high - 1);
                    return true;
                }

                if(node._values[i].CompareTo(value) < 0)
                {
                    return Remove(node._nextNodes[i + 1], value, high - 1);
                }
            }
            return false;
        }

        private T SwapWithLeaf(Node node, T value, int heigh)
        {
            if(heigh != 0)
            {
                return SwapWithLeaf(node._nextNodes[0], value, heigh - 1);
            }
            var temp = node._values[0];
            node.RemoveKey(temp);
            return temp;
        }

        static Node Split(Node node)
        {
            if (node.Count != 3) throw new ArgumentException("Node type is not 3-node");

            return new Node
                (
                    new Node(node._nextNodes[0], node._nextNodes[1], node._values[0]),
                    new Node(node._nextNodes[2], node._nextNodes[3], node._values[2]),
                    node._values[1]
                );
        }

        public void Add(T value) => Add(_root, null, value, _deep);

        public bool Remove(T value) => Remove(_root, value, _deep);

        static void PrintTree(Node root, ref int counter, StringBuilder stringBuilder)
        {
            for (int i = 0; i < root.Count; i++)
            {
                if (root._nextNodes[i] != null)
                {
                    PrintTree(root._nextNodes[i], ref counter, stringBuilder);
                }
                stringBuilder.Append(root._values[i]).Append(' ');
                counter++;
            }
            if (root._nextNodes[root.Count] != null)
            {
                PrintTree(root._nextNodes[root.Count], ref counter, stringBuilder);
            }
        }

        static void ConvertTreeToLinkedList(Node root, LinkedList<T> list)
        {
            for (int i = 0; i < root.Count; i++)
            {
                if (root._nextNodes[i] != null)
                {
                    ConvertTreeToLinkedList(root._nextNodes[i], list);
                }
                list.AddLast(new LinkedListNode<T>(root._values[i]));
            }
            if (root._nextNodes[root.Count] != null)
            {
                ConvertTreeToLinkedList(root._nextNodes[root.Count], list);
            }
        }

        public override string ToString()
        {
            int counter = 0;
            var sb = new StringBuilder();
            PrintTree(_root, ref counter, sb);
            return sb.ToString();
        }

        public LinkedList<T> GetLinkedList()
        {
            var list = new LinkedList<T>();
            ConvertTreeToLinkedList(_root, list);
            return list;
        }
    }
}