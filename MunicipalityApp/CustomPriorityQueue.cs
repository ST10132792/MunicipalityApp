using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalityApp
{/// <summary>
/// Custom priority queue made by Claude AI
/// 
/// Queues weren't compatible with this version of .NET and I was struggling with stacks
/// </summary>
/// <typeparam name="T"></typeparam>
    public class CustomPriorityQueue<T>
    {
        private List<T> data; // List to store queue elements.
        private readonly Comparison<T> comparison; // Function to compare element priorities.
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        // Constructor that initializes the queue with a custom comparison function.
        public CustomPriorityQueue(Comparison<T> comparison)
        {
            this.data = new List<T>();
            this.comparison = comparison;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        // Copy constructor for creating a new queue from an existing one.
        public CustomPriorityQueue(CustomPriorityQueue<T> other)
        {
            this.data = new List<T>(other.data); // Copy data from the other queue.
            this.comparison = other.comparison; // Copy the comparison function.
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        // Property to get the number of items in the queue.
        public int Count => data.Count;

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        // Adds an item to the queue, maintaining the heap property.
        public void Enqueue(T item)
        {
            data.Add(item); // Add item to the end of the list.
            int ci = data.Count - 1; // Child index.
            while (ci > 0)
            {
                int pi = (ci - 1) / 2; // Parent index.
                if (comparison(data[ci], data[pi]) >= 0) // If child has lower priority, stop.
                    break;
                // Swap child with parent if child has higher priority.
                T tmp = data[ci];
                data[ci] = data[pi];
                data[pi] = tmp;
                ci = pi; // Move child index up to parent.
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        // Removes and returns the highest priority item from the queue.
        public T Dequeue()
        {
            int li = data.Count - 1; // Last index.
            T frontItem = data[0]; // The item to return.
            data[0] = data[li]; // Move the last item to the root.
            data.RemoveAt(li); // Remove the last item.

            --li; // Update last index after removal.
            int pi = 0; // Start at the root.
            while (true)
            {
                int ci = pi * 2 + 1; // Left child index.
                if (ci > li) break; // If no children exist, stop.
                int rc = ci + 1; // Right child index.
                // Choose the child with higher priority.
                if (rc <= li && comparison(data[rc], data[ci]) < 0)
                    ci = rc;
                if (comparison(data[pi], data[ci]) <= 0) break; // If parent has higher priority, stop.
                // Swap parent with child if child has higher priority.
                T tmp = data[pi];
                data[pi] = data[ci];
                data[ci] = tmp;
                pi = ci; // Move parent index down to child.
            }
            return frontItem; // Return the highest priority item.
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    }
}
