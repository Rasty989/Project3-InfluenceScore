using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3
{
    class Graph
    {
        int V; // Number of nodes
        List<int>[] unweightedAdjList; // For unweighted graph
        List<(int, int)>[] weightedAdjList; // For weighted graph

        // Constructor for both types of graphs
        public Graph(int vertices)
        {
            V = vertices;
            unweightedAdjList = new List<int>[V];
            weightedAdjList = new List<(int, int)>[V];
            for (int i = 0; i < V; i++)
            {
                unweightedAdjList[i] = new List<int>();
                weightedAdjList[i] = new List<(int, int)>();
            }
        }

        // Add edge for unweighted graph
        public void AddUnweightedEdge(int u, int v)
        {
            unweightedAdjList[u].Add(v);
            unweightedAdjList[v].Add(u);
        }

        // Add edge for weighted graph
        public void AddWeightedEdge(int u, int v, int weight)
        {
            weightedAdjList[u].Add((v, weight));
            weightedAdjList[v].Add((u, weight));
        }

        // BFS to find distances in unweighted graph
        public int[] BFS(int start)
        {
            int[] distance = new int[V];
            for (int i = 0; i < V; i++) distance[i] = -1;
            Queue<int> queue = new Queue<int>();

            distance[start] = 0;
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                int node = queue.Dequeue();
                foreach (int neighbor in unweightedAdjList[node])
                {
                    if (distance[neighbor] == -1)
                    {
                        distance[neighbor] = distance[node] + 1;
                        queue.Enqueue(neighbor);
                    }
                }
            }
            return distance;
        }

        // Dijkstra for weighted graph
        public int[] Dijkstra(int start)
        {
            int[] distance = new int[V];
            bool[] visited = new bool[V];
            for (int i = 0; i < V; i++) distance[i] = int.MaxValue;
            distance[start] = 0;

            var pq = new PriorityQueue<(int, int), int>();
            pq.Enqueue((start, 0), 0);

            while (pq.Count > 0)
            {
                var (node, dist) = pq.Dequeue();
                if (visited[node]) continue;
                visited[node] = true;

                foreach (var (neighbor, weight) in weightedAdjList[node])
                {
                    if (distance[node] + weight < distance[neighbor])
                    {
                        distance[neighbor] = distance[node] + weight;
                        pq.Enqueue((neighbor, distance[neighbor]), distance[neighbor]);
                    }
                }
            }
            return distance;
        }

        // Calculate Influence_Score for both graphs
        public void CalculateInfluenceScore(bool isWeighted)
        {
            Console.WriteLine(isWeighted ? "Weighted Graph:" : "Unweighted Graph:");
            for (int i = 0; i < V; i++)
            {
                int[] distances = isWeighted ? Dijkstra(i) : BFS(i);
                int totalDistance = 0;

                foreach (int d in distances)
                    if (d != int.MaxValue && d != -1) totalDistance += d;

                double influenceScore = (double)(V - 1) / totalDistance;
                Console.WriteLine($"Influence_Score of Node {i}: {influenceScore:F2}");
            }
        }
    }
}
