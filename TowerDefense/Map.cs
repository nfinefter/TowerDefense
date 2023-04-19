using System.Drawing;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeightedDirectedGraphs;

using static TowerDefense.Game1;

namespace TowerDefense
{
    public sealed class Map 
    {
        Vertex<Point> Start;
        Vertex<Point> End;
        List<Vertex<Point>> Path = new List<Vertex<Point>>();
        public Graph<Point> Graph;

        public Map ()
        {

        }

        public List<Vertex<Point>> GeneratePath(int scrWidth, int scrHeight)
        {
            GenerateGraph(scrWidth, scrHeight);

            List<Vertex<Point>> points = MakePoints();

            for (int i = 0; i < points.Count - 1; i++)
            {
               PathFinding.AStar(out _, out Path, Graph, points[i].Value, points[i + 1].Value, PathFinding.Diagonal);
            }

            return Path;
        }
        public List<Vertex<Point>> MakePoints()
        {
            Random rand = new Random();

            List<Vertex<Point>> points = new List<Vertex<Point>>();

            int section1 = Graph.Count / 4;
            int section2 = section1 * 2;
            int section3 = section1 * 3;
            int section4 = section1 * 4;

            points.Add(Graph[rand.Next(0, section1 / 2)]);
            points.Add(Graph[rand.Next(section1, section2)]);
            points.Add(Graph[rand.Next(section2, section3)]);
            points.Add(Graph[rand.Next(section3, section4)]);

            return points;
        }
        public void GenerateGraph(int scrWidth, int scrHeight)
        {
            Graph = new Graph<Point>();

            
            for (int x = 0; x+ size< scrWidth; x +=size)
            {
                for (int y = 0; y + size < scrHeight; y += size)
                {
                    Vertex<Point> temp = new Vertex<Point>(new Point(x, y));
                    Graph.AddVertex(temp);

                    if (x >= 0 && y >= 0)
                    {
                        Vertex<Point> prevX = Graph.Search(new Point(x - size, y));
                        Graph.AddEdge(prevX, temp, 1);
                        Graph.AddEdge(temp, prevX, 1);

                        Vertex<Point> prevY = Graph.Search(new Point(x, y - size));
                        Graph.AddEdge(prevY, temp, 1);
                        Graph.AddEdge(temp, prevY, 1);
                        
                    }
                }
            }
        }

    }
}
