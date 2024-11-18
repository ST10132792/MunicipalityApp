using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalityApp
{
    public class ServiceRequestHeap
    {
        private List<ServiceRequest> heap;

        public ServiceRequestHeap()
        {
            heap = new List<ServiceRequest>();
        }

        private int GetParentIndex(int index) => (index - 1) / 2;
        private int GetLeftChildIndex(int index) => 2 * index + 1;
        private int GetRightChildIndex(int index) => 2 * index + 2;

        public void Insert(ServiceRequest request)
        {
            heap.Add(request);
            HeapifyUp(heap.Count - 1);
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Ensures the heap property is maintained by moving the element up the heap
        /// </summary>
        private void HeapifyUp(int index)
        {
            while (index > 0)
            {
                int parentIndex = GetParentIndex(index);
                if (IsEmergency(heap[index]) && !IsEmergency(heap[parentIndex]))
                {
                    Swap(index, parentIndex);
                    index = parentIndex;
                }
                else
                {
                    break;
                }
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Checks if a request is an emergency
        /// </summary>
        private bool IsEmergency(ServiceRequest request)
        {
            return request.Status == "Emergency Response";
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Swaps two elements in the heap
        /// </summary>
        private void Swap(int i, int j)
        {
            var temp = heap[i];
            heap[i] = heap[j];
            heap[j] = temp;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Retrieves all emergency requests from the heap
        /// </summary>
        public List<ServiceRequest> GetEmergencyRequests()
        {
            return heap.Where(r => IsEmergency(r)).ToList();
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    }
}
