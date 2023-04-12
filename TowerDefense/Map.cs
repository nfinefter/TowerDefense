using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeightedDirectedGraphs;

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
               Path.AddRange(Graph.Djikstra(points[i], points[i + 1]));
            }

            return Path;
        }
        public List<Vertex<Point>> MakePoints()
        {
            Random rand = new Random();

            List<Vertex<Point>> points = new List<Vertex<Point>>();

            int section1 = Graph.Count / 6;
            int section2 = section1 * 2;
            int section3 = section1 * 3;
            int section4 = section1 * 4;
            int section5 = section1 * 5;
            int section6 = Graph.Count;

            points.Add(Graph[rand.Next(0, section1)]);
            points.Add(Graph[rand.Next(section1, section2)]);
            points.Add(Graph[rand.Next(section2, section3)]);
            points.Add(Graph[rand.Next(section3, section4)]);
            points.Add(Graph[rand.Next(section4, section5)]);
            points.Add(Graph[rand.Next(section5, section6)]);

            return points;
        }
        public void GenerateGraph(int scrWidth, int scrHeight)
        {
            Graph = new Graph<Point>();
            
            int size = 400;
            
            for (int x = 0; x < scrWidth; x +=size)
            {
                for (int y = 0; y < scrHeight; y += size)
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
