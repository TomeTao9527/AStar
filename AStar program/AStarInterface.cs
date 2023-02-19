using System.Numerics;

namespace AStar_program
{
    public class MapNode
    {
        public Vector2 _vec;
        public int _weight;
        public MapNode? father;
        public bool ContainInPath=false;
        public float g;
        public float h;
        public float f
        {
            get { return g + h; }
        }
        public MapNode(Vector2 vec,int weight)
        {
            _vec = vec;
            _weight= weight;
            g = h = int.MaxValue;
        }
    }
    public class Map
    {
        private int Height;
        private int Width;
        public List<MapNode> Nodes;
        public Map(int height, int width)
        {
            Height = height;
            Width = width;
            Nodes = new List<MapNode>();
            for (int i = 1; i <= height; i++)
            {
                for (int j = 1; j <= width; j++)
                {
                    if (i == 5 && 2 <= j && j <= 8)
                    {
                        Nodes.Add(new MapNode(new Vector2(i, j), 10));
                    }
                    else
                        Nodes.Add(new MapNode(new Vector2(i, j), 1));
                }
            }
        }
    }
    internal class AStarInterface
    {
        private Map map;
        private List<MapNode> Open;
        private List<MapNode> Close;
        private MapNode? End;
        public int Count = 0;
        public AStarInterface(Map map)
        {
            Open = new List<MapNode>();
            Close = new List<MapNode>();
            this.map = map;
        }

        public List<MapNode> GetPath(MapNode Start, MapNode End)
        {
            Start.g = 0;
            Start.h= 0;
            Open.Add(Start);
            this.End = End;
            End.ContainInPath=true;
            GetNext();
            return Close;
        }

        /// <summary>
        ///  Key function
        /// </summary>
        private void GetNext()
        {
            if(Open.Count==0)
            {
                Console.WriteLine("Fail Get Path!!!");
                return;
            }
            Open.Sort((a, b) => a.f.CompareTo(b.f));
            MapNode _last = Open.First();
            Open.Remove(_last);
            Close.Add(_last);
            if (_last._vec == End?._vec)
                return;
            List<MapNode> subs = GetRangePoss(_last);
            if (subs != null && subs.Count > 0)
            {
                for (int i = 0; i < subs.Count; i++)
                {
                    if (Close.Contains(subs[i]))
                        continue;
                    if(!Open.Contains(subs[i]))
                    {
                        subs[i].father = _last;
                        subs[i].g = GetG(subs[i], _last);
                        subs[i].h = GetH(subs[i], End);
                        Open.Add(subs[i]);
                    }
                }
            }
            GetNext();
        }

        /// <summary>
        /// Get the surrounding points of the target point
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private List<MapNode> GetRangePoss(MapNode pos)
        {
            List<MapNode> Subs = new List<MapNode>();
            MapNode? node;
            node = map.Nodes.Where(p => p._vec.X == pos._vec.X && p._vec.Y == pos._vec.Y + 1)?.FirstOrDefault();
            if (node != null) Subs.Add(node);
            node = map.Nodes.Where(p => p._vec.X == pos._vec.X && p._vec.Y == pos._vec.Y - 1).FirstOrDefault();
            if (node != null) Subs.Add(node);
            node = map.Nodes.Where(p => p._vec.X == pos._vec.X + 1 && p._vec.Y == pos._vec.Y)?.FirstOrDefault();
            if (node != null) Subs.Add(node);
            node = map.Nodes.Where(p => p._vec.X == pos._vec.X + 1 && p._vec.Y == pos._vec.Y + 1)?.FirstOrDefault();
            if (node != null) Subs.Add(node);
            node = map.Nodes.Where(p => p._vec.X == pos._vec.X + 1 && p._vec.Y == pos._vec.Y - 1)?.FirstOrDefault();
            if (node != null) Subs.Add(node);
            node = map.Nodes.Where(p => p._vec.X == pos._vec.X - 1 && p._vec.Y == pos._vec.Y)?.FirstOrDefault();
            if (node != null) Subs.Add(node);
            node = map.Nodes.Where(p => p._vec.X == pos._vec.X - 1 && p._vec.Y == pos._vec.Y + 1)?.FirstOrDefault();
            if (node != null) Subs.Add(node);
            node = map.Nodes.Where(p => p._vec.X == pos._vec.X - 1 && p._vec.Y == pos._vec.Y - 1)?.FirstOrDefault();
            if (node != null) Subs.Add(node);
            return Subs;
        }

        /// <summary>
        /// the consume which between Start and node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="Last"></param>
        /// <returns></returns>
        private float GetG(MapNode node,MapNode Last)
        {
            return Last.g + Vector2.Distance(node._vec, Last._vec)*node._weight;
        }

        /// <summary>
        /// the estimate distance between node and the end(曼哈顿距离)
        /// </summary>
        /// <param name="node"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private float GetH(MapNode node, MapNode end)
        {
            return Math.Abs(node._vec.X - end._vec.X) + Math.Abs(node._vec.Y - end._vec.Y);
        }
    }
}
