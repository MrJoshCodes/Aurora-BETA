namespace AuroraEmu.Game.Rooms.Pathfinder
{
    internal sealed class BinaryHeap
    {
        private Node _head;

        public bool HasNext() => _head != null;

        public void Push(Node node)
        {
            if (_head == null)
            {
                _head = node;
            }
            else if (node.ExpectedCost < _head.ExpectedCost)
            {
                node.NextListElement = _head;
                _head = node;
            }
            else
            {
                var current = _head;
                while (current.NextListElement != null && current.NextListElement.ExpectedCost <= node.ExpectedCost)
                {
                    current = current.NextListElement;
                }

                node.NextListElement = current.NextListElement;
                current.NextListElement = node;
            }
        }

        /// <summary>
        /// Pops a node from the heap, this node is always the node
        /// with the cheapest path cost
        /// </summary>
        public Node Pop()
        {
            var top = _head;
            _head = _head.NextListElement;

            return top;
        }
    }
}