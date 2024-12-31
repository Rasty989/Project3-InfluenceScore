using System;
using System.Collections.Generic;

namespace Project3
{
    class Graph
    {
        private int V; // Number of nodes
        private List<int>[] unweightedAdjList; // Adjacency List for unweighted graph
        private List<(int, double)>[] weightedAdjList; // Adjacency List for weighted graph
        private Dictionary<int, double> influenceScores; // Store influence scores for nodes

        public Graph(int vertices)
        {
            V = vertices;
            unweightedAdjList = new List<int>[V];
            weightedAdjList = new List<(int, double)>[V];
            influenceScores = new Dictionary<int, double>();

            for (int i = 0; i < V; i++)
            {
                unweightedAdjList[i] = new List<int>();
                weightedAdjList[i] = new List<(int, double)>();
            }
        }

        // Add edge to unweighted graph
        public void AddUnweightedEdge(int u, int v)
        {
            unweightedAdjList[u].Add(v);
            unweightedAdjList[v].Add(u);
        }

        // Add edge to weighted graph
        public void AddWeightedEdge(int u, int v, double weight)
        {
            weightedAdjList[u].Add((v, weight));
            weightedAdjList[v].Add((u, weight));
        }

        // BFS for unweighted graph
        private int[] BFS(int start)
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
        private double[] Dijkstra(int start)
        {
            double[] distance = new double[V];
            bool[] visited = new bool[V];
            for (int i = 0; i < V; i++) distance[i] = double.MaxValue;
            distance[start] = 0;

            var pq = new PriorityQueue<(int node, double dist), double>();
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

        // Calculate influence score
        public void CalculateInfluenceScore(bool isWeighted)
        {
            Console.WriteLine(isWeighted ? "Weighted Graph:" : "Unweighted Graph:");
            for (int i = 0; i < V; i++)
            {
                double[] distances = isWeighted ? Dijkstra(i) : Array.ConvertAll(BFS(i), x => (double)x);
                double totalDistance = 0;

                foreach (double d in distances)
                {
                    if (d != double.MaxValue && d != -1)
                    {
                        totalDistance += d;
                    }
                }

                if (totalDistance > 0)
                {
                    double influenceScore = (double)(V - 1) / totalDistance;
                    influenceScores[i] = influenceScore;
                    Console.WriteLine($"Influence_Score of Node {i}: {influenceScore:F2}");
                }
                else
                {
                    influenceScores[i] = 0;
                }
            }
        }

        // Get adjacency list for unweighted graph
        public List<int>[] GetAdjacencyListUnweighted()
        {
            return unweightedAdjList;
        }

        // Get adjacency list for weighted graph
        public List<(int, double)>[] GetAdjacencyListWeighted()
        {
            return weightedAdjList;
        }

        // Get influence scores
        public Dictionary<int, double> GetInfluenceScores()
        {
            return influenceScores;
        }
    }
}
