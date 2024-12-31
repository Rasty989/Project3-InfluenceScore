using System;
using System.IO;

namespace Project3
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Enter the file path of the dataset:");
            string filePath = Console.ReadLine();

            // Read all lines from the file for node count calculation
            string[] allLines = File.ReadAllLines(filePath);

            // Dynamically calculate the number of nodes
            int numberOfNodes = allLines
                .Where(line => !string.IsNullOrWhiteSpace(line) && !line.StartsWith("%"))
                .Select(line => line.Split())
                .Select(parts => Math.Max(int.Parse(parts[0]), int.Parse(parts[1])))
                .Max();

            numberOfNodes += 1; 
            Console.WriteLine($"Number of nodes determined from dataset: {numberOfNodes}");

            Console.WriteLine("Is the graph weighted? (yes/no):");
            bool isWeighted = Console.ReadLine().Trim().ToLower() == "yes";

            
            Graph g = new Graph(numberOfNodes);

            try
            {
                
                foreach (string line in allLines)
                {
                    // Skip lines that start with '%' (comments) or are empty
                    if (string.IsNullOrWhiteSpace(line) || line.StartsWith("%"))
                        continue;

                    // Split the line into node pairs (and optionally weight)
                    string[] parts = line.Split();
                    if (parts.Length < 2) continue;

                    int node1 = int.Parse(parts[0]); // Adjust for 0-based indexing
                    int node2 = int.Parse(parts[1]); // Adjust for 0-based indexing

                    if (isWeighted && parts.Length == 3)
                    {
                        double weight = double.Parse(parts[2]);
                        g.AddWeightedEdge(node1, node2, weight);
                    }
                    else
                    {
                        g.AddUnweightedEdge(node1, node2);
                    }
                }

                // Calculate and print the influence scores
                Console.WriteLine("Calculating influence scores for the graph...");
                g.CalculateInfluenceScore(isWeighted);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file: {ex.Message}");
            }
        }

    }
}
