using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AStarPathSearch
{
    struct Node
    {
        public int value;
    }

    class PathNode : IComparable<PathNode>
    {
        public int G;
        public int H;
        public int F {
            get{
                return G + H;
            }
        }

        public PathNode Parent;
        public Point Position;

        public PathNode(Point pos)
        {
            this.Position = pos;
            this.Parent = null;
            this.G = 0;
            this.H = 0;
        }

        public override string ToString()
        {
            return Position.ToString();
        }

        #region IComparable<PathNode> Members

        public int CompareTo(PathNode other)
        {
            return F - other.F;
        }

        #endregion
    }

    class AStar
    {
        private Node[] map;
        public int Width { get; set; }
        public int Height { get; set; }

        public void InitMap(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            map = new Node[width * height];
            for (int i = 0; i < map.Length; i++)
            {
                map[i].value = 0;
            }
        }

        public void SetNodeValue(int x, int y, int value)
        {
            map[x + y * Width].value = value;
        }

        public void SetNodeValue(Point pt, int value)
        {
            SetNodeValue(pt.X, pt.Y, value);
        }

        public int GetNodeValue(int x, int y)
        {
            return map[x + y * Width].value;
        }

        #region A Star functions
        private int G(PathNode curNode, int deltaX, int deltaY)
        {
            if(deltaX == 0||
                deltaY == 0)
            {
                return 10 + curNode.G;
            }
            else
            {
                return 14 + curNode.G;
            }
        }

        private int H(int x, int y, Point end)
        {
            return (Math.Abs(x - end.X) + Math.Abs(y - end.Y)) * 10;
        }

        private List<PathNode> unLockList = new List<PathNode>();
        private Dictionary<string, PathNode> lockList = new Dictionary<string, PathNode>();
        private List<PathNode> path = new List<PathNode>();
        private int[][] delta = new int[][]{
            new int[]{0,1},
            new int[]{0,-1},
            new int[]{1,0},
            new int[]{-1,0},
            new int[]{1,1},
            new int[]{-1,-1},
            new int[]{1,-1},
            new int[]{-1,1}
        };

        public Point Start { get; set; }
        public Point End { get; set; }

        public List<PathNode> FindPath()
        {
            unLockList.Clear();
            lockList.Clear();
            path.Clear();
            doFindPath();
            path.Reverse();
            return path;
        }

        private void doFindPath()
        {
            PathNode start = new PathNode(Start);
            PathNode cur = start;
            while (true)
            {
                if(!lockList.ContainsKey(cur.ToString()))
                    lockList.Add(cur.ToString(), cur);
                for (int i = 0; i < delta.Length; i++)
                {
                    int deltaX = delta[i][0];
                    int deltaY = delta[i][1];
                    Point newp = new Point(cur.Position.X + deltaX,
                        cur.Position.Y + deltaY);
                    if (!canWalkOnIt(newp))
                        continue;

                    if (!canCrossWalk(newp, deltaX, deltaY))
                        continue;

                    if (lockList.ContainsKey(newp.ToString()))
                        continue;

                    if (isPointInUnlockList(newp))
                    {
                        PathNode ulnode = __pn;
                        int newg = G(cur, deltaX, deltaY);
                        if (newg < ulnode.G)
                        {
                            ulnode.Parent = cur;
                            ulnode.G = newg;
                        }
                        continue;
                    }
                    PathNode newpn = new PathNode(newp);
                    newpn.G = G(cur, deltaX, deltaY);
                    newpn.H = H(newp.X, newp.Y, End);
                    newpn.Parent = cur;
                    unLockList.Add(newpn);
                }
                if (unLockList.Count == 0)
                    break;
                unLockList.Sort();
                cur = unLockList[0];
                unLockList.Remove(cur);
                
                if (cur.Position.Equals(End))
                {
                    while (cur != null)
                    {
                        path.Add(cur);
                        cur = cur.Parent;
                    }
                    break;
                }
            }
        }

        private PathNode __pn;

        private bool isPointInUnlockList(Point src)
        {
            __pn = null;
            foreach (PathNode item in unLockList)
            {
                if (item.Position.Equals(src))
                {
                    __pn = item;
                    return true;
                }

            }
            return false;
        }

        private bool canWalkOnIt(Point node)
        {
            if (node.X < 0 || node.Y < 0)
                return false;
            if (node.X > Width - 1 || node.Y > Height - 1)
                return false;
            return GetNodeValue(node.X, node.Y) >= 0;
        }

        private bool canCrossWalk(Point node, int deltaX, int deltaY)
        {
            if (deltaX == 0 ||
               deltaY == 0)
            {
                return true;
            }
            else
            {
              return 
                    canWalkOnIt(new Point(node.X - deltaX, node.Y)) &&
                    canWalkOnIt(new Point(node.X, node.Y - deltaY));

            }
        }
        #endregion
    }
}
