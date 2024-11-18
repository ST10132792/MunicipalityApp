using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalityApp
{
    public class ServiceRequest : IComparable<ServiceRequest>
    {
        // Core properties for request identification and tracking
        public int RequestId { get; set; }
        public string Description { get; set; }
        public DateTime SubmissionDate { get; set; }
        
        // Status management properties
        public string Status { get; set; }
        public string Category { get; set; }
        // Maintains a chronological history of status changes
        public List<string> StatusHistory { get; set; }

        /// <summary>
        /// Creates a new service request with initial pending status
        /// </summary>
        public ServiceRequest(int requestId, string description, string category)
        {
            RequestId = requestId;
            Description = description;
            Category = category;
            SubmissionDate = DateTime.Now;
            Status = "Pending";
            // Initialize history with submission entry
            StatusHistory = new List<string> { $"{DateTime.Now}: Request submitted - Status: Pending" };
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Implements IComparable for AVL Tree operations
        /// Compares requests based on their IDs
        /// </summary>
        public int CompareTo(ServiceRequest other)
        {
            return this.RequestId.CompareTo(other.RequestId);
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Updates request status and logs the change in history
        /// </summary>
        public void UpdateStatus(string newStatus)
        {
            Status = newStatus;
            StatusHistory.Add($"{DateTime.Now}: Status updated to {newStatus}");
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    }

}
