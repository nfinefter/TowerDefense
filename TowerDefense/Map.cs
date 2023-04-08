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
        List<Vertex<Point>> Path;
        public Graph<Point> Graph;

        public Map ()
        {

        }

        public List<Vertex<Point>> GeneratePath(int scrWidth, int scrHeight)
        {
            Random rand = new Random();

            int xBound = scrWidth / 5;

            Start = new Vertex<Point>(new Point(rand.Next(0, xBound), rand.Next(0, scrHeight)));

            xBound = scrWidth / 5;

            xBound = scrWidth - xBound;

            End = new Vertex<Point>(new Point(rand.Next(0, xBound), rand.Next(0, scrHeight)));          
           
            GenerateGraph(scrWidth, scrHeight);

            List<Vertex<Point>> points = MakePoints(Start, End);

            Path = Graph.Djikstra(Start, points[0]);

            for (int i = 1; i < points.Count - 1; i++)
            {
               Path.AddRange(Graph.Djikstra(points[i], points[i + 1]));
            }

            Path.AddRange(Graph.Djikstra(points[points.Count - 1], End));

            return Path;
        }
        public List<Vertex<Point>> MakePoints(Vertex<Point> Start, Vertex<Point> End)
        {
            Random rand = new Random();

            List<Vertex<Point>> points = new List<Vertex<Point>>();

            int section1 = Graph.Count / 4;
            int section2 = section1 * 2;
            int section3 = section1 * 3;
            int section4 = Graph.Count;

            points.Add(Start);

            points.Add(Graph[rand.Next(0, section1)]);
            points.Add(Graph[rand.Next(section1, section2)]);
            points.Add(Graph[rand.Next(section2, section3)]);
            points.Add(Graph[rand.Next(section3, section4)]);

            points.Add(End);

            return points;
        }
        public void GenerateGraph(int scrWidth, int scrHeight)
        {
            Graph = new Graph<Point>();

            int size = 15;
            
            for (int x = 0; x < scrWidth; x+=size)
            {
                for (int y = 0; y < scrHeight; y+= size)
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
                        //Fix Y not making proper neighbors
                    }
                }
            }
        }

    }
}
