namespace Project3
{
    class Program
    {
        static void Main()
        {
            // Create a graph with 4 nodes
            Graph g = new Graph(4);

            // Example for unweighted graph
            g.AddUnweightedEdge(0, 1);
            g.AddUnweightedEdge(0, 2);
            g.AddUnweightedEdge(1, 3);
            g.AddUnweightedEdge(2, 3);

            // Calculate Influence_Score for unweighted graph
            g.CalculateInfluenceScore(false);

            Console.WriteLine();

            // Example for weighted graph
            g.AddWeightedEdge(0, 1, 1);
            g.AddWeightedEdge(0, 2, 3);
            g.AddWeightedEdge(1, 3, 4);
            g.AddWeightedEdge(2, 3, 2);

            // Calculate Influence_Score for weighted graph
            g.CalculateInfluenceScore(true);
        }
    }
}
