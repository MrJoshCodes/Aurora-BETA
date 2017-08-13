namespace AuroraEmu.Game.Rooms.Pathfinder
{
    public class BinaryHeap
    {
        private Node listHead;

        public bool HasNext()
        {
            return listHead != null;
        }

        public void Add(Node item)
        {
            if (listHead == null)
            {
                listHead = item;
            }
            else if (listHead.Next == null && item.Cost <= listHead.Cost)
            {
                item.NextListElement = listHead;
                listHead = item;
            }
            else
            {
                Node ptr = listHead;
                while (ptr.NextListElement != null && ptr.NextListElement.Cost < item.Cost)
                    ptr = ptr.NextListElement;
                item.NextListElement = ptr.NextListElement;
                ptr.NextListElement = item;
            }
        }

        public Node ExtractFirst()
        {
            Node result = listHead;
            listHead = listHead.NextListElement;
            return result;
        }
    }
}
