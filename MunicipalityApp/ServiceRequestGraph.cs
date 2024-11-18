using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalityApp
{
    public class ServiceRequestGraph
    {
        private Dictionary<int, List<int>> adjacencyList; // Adjacency list for graph representation and request lookup dictionary
        private Dictionary<int, ServiceRequest> requests;

        public ServiceRequestGraph()
        {
            adjacencyList = new Dictionary<int, List<int>>();
            requests = new Dictionary<int, ServiceRequest>();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Adds a new request to the graph and adjacency list
        /// </summary>
        public void AddRequest(ServiceRequest request)
        {
            if (!requests.ContainsKey(request.RequestId))
            {
                requests[request.RequestId] = request;
                adjacencyList[request.RequestId] = new List<int>();
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Adds a relationship between two requests in the graph
        /// </summary>
        public void AddRelation(int requestId1, int requestId2)
        {
            Console.WriteLine($"Attempting to add relation between {requestId1} and {requestId2}");
            Console.WriteLine($"Request1 exists: {adjacencyList.ContainsKey(requestId1)}");
            Console.WriteLine($"Request2 exists: {adjacencyList.ContainsKey(requestId2)}");
            
            if (adjacencyList.ContainsKey(requestId1) && adjacencyList.ContainsKey(requestId2))
            {
                if (!adjacencyList[requestId1].Contains(requestId2))
                {
                    adjacencyList[requestId1].Add(requestId2);
                    adjacencyList[requestId2].Add(requestId1);
                    Console.WriteLine($"Relation added successfully");
                }
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Performs a Breadth-First Search (BFS) to find related requests
        /// </summary>
        public List<ServiceRequest> GetRelatedRequests(int requestId)
        {
            Console.WriteLine($"Getting related requests for {requestId}");
            Console.WriteLine($"Request exists in graph: {adjacencyList.ContainsKey(requestId)}");
            Console.WriteLine($"Number of relations: {adjacencyList[requestId]?.Count ?? 0}");

            if (!adjacencyList.ContainsKey(requestId))
                return new List<ServiceRequest>();

            var visited = new HashSet<int>();
            var queue = new Queue<int>();
            var result = new List<ServiceRequest>();

            visited.Add(requestId);
            queue.Enqueue(requestId);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                result.Add(requests[current]);

                foreach (var neighbor in adjacencyList[current])
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }
            return result;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Calculates the Minimum Spanning Tree using Kruskal's Algorithm
        /// </summary>
        public List<(int, int)> GetMinimumSpanningTree()
        {
            var edges = new List<(int, int, int)>();
            var result = new List<(int, int)>();
            var disjointSet = new Dictionary<int, int>();

            // Initialize disjoint set
            foreach (var vertex in adjacencyList.Keys)
            {
                disjointSet[vertex] = vertex;
            }

            // Collect all edges
            foreach (var vertex in adjacencyList.Keys)
            {
                foreach (var neighbor in adjacencyList[vertex])
                {
                    if (vertex < neighbor) // Avoid duplicates
                    {
                        var weight = CalculateWeight(vertex, neighbor);
                        edges.Add((vertex, neighbor, weight));
                    }
                }
            }

            // Sort edges by weight
            edges.Sort((a, b) => a.Item3.CompareTo(b.Item3));

            foreach (var edge in edges)
            {
                var set1 = Find(disjointSet, edge.Item1);
                var set2 = Find(disjointSet, edge.Item2);

                if (set1 != set2)
                {
                    result.Add((edge.Item1, edge.Item2));
                    Union(disjointSet, set1, set2);
                }
            }

            return result;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Finds the root of the set for a given vertex
        /// </summary>
        private int Find(Dictionary<int, int> disjointSet, int vertex)
        {
            if (disjointSet[vertex] != vertex)
                disjointSet[vertex] = Find(disjointSet, disjointSet[vertex]);
            return disjointSet[vertex];
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Unites two sets in the disjoint set
        /// </summary>
        private void Union(Dictionary<int, int> disjointSet, int x, int y)
        {
            disjointSet[x] = y;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Calculates the weight of an edge based on category similarity
        /// </summary>
        private int CalculateWeight(int request1, int request2)
        {
            // Calculate weight based on category similarity
            return requests[request1].Category == requests[request2].Category ? 1 : 2;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    }
}
