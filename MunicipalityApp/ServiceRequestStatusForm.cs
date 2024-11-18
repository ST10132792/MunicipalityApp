using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MunicipalityApp
{
    public partial class ServiceRequestStatusForm : Form
    {
        private AVLTree<ServiceRequest> requestTree; // AVL Tree for efficient request storage and retrieval
        private ServiceRequestHeap emergencyHeap; // Min-heap for prioritizing emergency requests
        private ServiceRequestGraph requestGraph; // Graph structure for managing request relationships
        private int nextRequestId; // Next available request ID

        public ServiceRequestStatusForm()
        {
            InitializeComponent();
            requestTree = new AVLTree<ServiceRequest>();
            emergencyHeap = new ServiceRequestHeap();
            requestGraph = new ServiceRequestGraph();
            nextRequestId = 1;
            
            // Initialize category dropdown
            cboCategory.Items.AddRange(new string[] {
                "Roads", "Traffic", "Infrastructure", "Utilities",
                "Sanitation", "Parks", "Recreation", "Safety",
                "Environmental"
            });
            cboCategory.SelectedIndex = 0;
            
            // Initialize status dropdown
            cboStatus.Items.AddRange(new string[] {
                "Pending",
                "In Progress",
                "Under Review",
                "Emergency Response",
                "Completed",
                "Cancelled"
            });
            cboStatus.SelectedIndex = 0;  // Default to "Pending"
            
            InitializeSampleData();
            UpdateRequestsDisplay();
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Initializes sample data for demonstration purposes
        /// </summary>  
        private void InitializeSampleData()
        {
            // Infrastructure
            AddServiceRequest("Pothole repair needed on Main Street", "Roads");
            AddServiceRequest("Broken traffic light at 5th and Oak", "Traffic");
            AddServiceRequest("Sidewalk crack repair required", "Infrastructure");
            AddServiceRequest("Bridge maintenance inspection", "Infrastructure");
            AddServiceRequest("Road line repainting needed", "Roads");
            
            // Utilities
            AddServiceRequest("Street light replacement", "Utilities");
            AddServiceRequest("Power outage in West District", "Utilities");
            AddServiceRequest("Water main leak on Elm Street", "Utilities");
            AddServiceRequest("Storm drain blockage", "Utilities");
            AddServiceRequest("Damaged electrical box", "Utilities");
            
            // Sanitation
            AddServiceRequest("Garbage collection issue", "Sanitation");
            AddServiceRequest("Illegal dumping report", "Sanitation");
            AddServiceRequest("Public bin overflow", "Sanitation");
            AddServiceRequest("Street cleaning request", "Sanitation");
            AddServiceRequest("Recycling bin replacement needed", "Sanitation");

            // Parks and Recreation
            AddServiceRequest("Playground equipment damage", "Parks");
            AddServiceRequest("Park bench vandalism", "Parks");
            AddServiceRequest("Sports field maintenance", "Recreation");
            AddServiceRequest("Tree trimming needed", "Parks");
            AddServiceRequest("Public pool maintenance", "Recreation");

            // Public Safety
            AddServiceRequest("Broken emergency call box", "Safety");
            AddServiceRequest("Missing street sign", "Safety");
            AddServiceRequest("Crosswalk signal malfunction", "Safety");
            AddServiceRequest("Fire hydrant inspection needed", "Safety");
            AddServiceRequest("Street camera maintenance", "Safety");

            // Environmental
            AddServiceRequest("Noise pollution complaint", "Environmental");
            AddServiceRequest("Air quality monitoring request", "Environmental");
            AddServiceRequest("Wildlife concern in residential area", "Environmental");
            AddServiceRequest("Chemical spill report", "Environmental");
            AddServiceRequest("Water quality testing request", "Environmental");

            // Update various statuses to show progression
            UpdateRequestStatus(1, "In Progress");
            UpdateRequestStatus(2, "Completed");
            UpdateRequestStatus(3, "Under Review");
            UpdateRequestStatus(4, "Scheduled");
            UpdateRequestStatus(5, "In Progress");
            UpdateRequestStatus(6, "Completed");
            UpdateRequestStatus(7, "Emergency Response");
            UpdateRequestStatus(8, "Pending Parts");
            UpdateRequestStatus(9, "Scheduled");
            UpdateRequestStatus(10, "In Progress");
            UpdateRequestStatus(11, "Under Review");
            UpdateRequestStatus(12, "Completed");
            UpdateRequestStatus(15, "In Progress");
            UpdateRequestStatus(18, "Scheduled");
            UpdateRequestStatus(20, "Emergency Response");
            UpdateRequestStatus(22, "Completed");
            UpdateRequestStatus(25, "In Progress");
            UpdateRequestStatus(27, "Under Review");
            UpdateRequestStatus(30, "Scheduled");
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Adds a new service request to the system
        /// </summary>
        private void AddServiceRequest(string description, string category)
        {
            var request = new ServiceRequest(nextRequestId++, description, category);
            requestTree.Insert(request);
            requestGraph.AddRequest(request);
            
            // Add to emergency heap if it's an emergency
            if (request.Status == "Emergency Response")
            {
                emergencyHeap.Insert(request);
            }

            // Create relationships between similar requests
            var allRequests = requestTree.InOrderTraversal();
            foreach (var existingRequest in allRequests)
            {
                if (existingRequest.RequestId != request.RequestId && 
                    existingRequest.Category == request.Category)
                {
                    Console.WriteLine($"Creating relationship between {request.RequestId} ({request.Category}) and {existingRequest.RequestId} ({existingRequest.Category})");
                    requestGraph.AddRelation(existingRequest.RequestId, request.RequestId);
                }
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Checks if two requests are in the same category
        /// </summary>
        private bool AreLocationsNearby(ServiceRequest request1, ServiceRequest request2)
        {
            return request1.Category == request2.Category;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Updates the status of a request
        /// </summary>
        private void UpdateRequestStatus(int requestId, string newStatus)
        {
            var searchRequest = new ServiceRequest(requestId, "", "");
            var request = requestTree.Find(searchRequest);
            if (request != null)
            {
                request.UpdateStatus(newStatus);
                
                // Add to emergency heap if status is changed to Emergency Response
                if (newStatus == "Emergency Response")
                {
                    emergencyHeap.Insert(request);
                }
                
                UpdateRequestsDisplay();
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Updates the display of all requests
        /// </summary>
        private void UpdateRequestsDisplay()
        {
            var requests = requestTree.InOrderTraversal();
            dgvRequests.DataSource = requests.Select(r => new
            {
                r.RequestId,
                r.Description,
                r.Category,
                r.Status,
                r.SubmissionDate
            }).ToList();
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Handles the click event for tracking a request
        /// </summary>
        private void btnTrack_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtRequestId.Text, out int requestId))
            {
                // Add debug logging
                Console.WriteLine($"Tracking request {requestId}");
                
                var searchRequest = new ServiceRequest(requestId, "", "");
                var foundRequest = requestTree.Find(searchRequest);
                
                if (foundRequest != null)
                {
                    Console.WriteLine($"Found request in tree: {foundRequest.Category}");
                    var relatedRequests = requestGraph.GetRelatedRequests(requestId);
                    Console.WriteLine($"Found {relatedRequests.Count} related requests");
                    
                    StringBuilder statusText = new StringBuilder();
                    statusText.AppendLine($"Request ID: {foundRequest.RequestId}");
                    statusText.AppendLine($"Status: {foundRequest.Status}");
                    statusText.AppendLine($"Category: {foundRequest.Category}");
                    statusText.AppendLine($"Submitted: {foundRequest.SubmissionDate}");
                    statusText.AppendLine("\nStatus History:");
                    statusText.AppendLine(string.Join("\n", foundRequest.StatusHistory));
                    
                    statusText.AppendLine("\nRelated Requests:");
                    foreach (var related in relatedRequests.Where(r => r.RequestId != requestId))
                    {
                        statusText.AppendLine($"- ID {related.RequestId}: {related.Description} ({related.Status})");
                    }

                    txtStatus.Text = statusText.ToString();
                    
                    // Show the minimum spanning tree for related requests
                    ShowMinimumSpanningTree();
                }
                else
                {
                    MessageBox.Show("Request not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid Request ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Displays the minimum spanning tree for related requests
        /// </summary>
        private void ShowMinimumSpanningTree()
        {
            var mst = requestGraph.GetMinimumSpanningTree();
            if (mst.Any())
            {
                int trackedRequestId = int.Parse(txtRequestId.Text);
                StringBuilder mstText = new StringBuilder("\nRelated Request Relationships (MST):");
                
                // Filter MST edges to only show those connected to the tracked request
                var relevantEdges = mst.Where(edge => 
                    edge.Item1 == trackedRequestId || edge.Item2 == trackedRequestId);

                if (relevantEdges.Any())
                {
                    foreach (var (request1, request2) in relevantEdges)
                    {
                        mstText.AppendLine($"\nRequest {request1} <-> Request {request2}");
                    }
                    txtStatus.AppendText(mstText.ToString());
                }
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Displays emergency requests
        /// </summary>
        private void btnShowEmergency_Click(object sender, EventArgs e)
        {
            var emergencyRequests = emergencyHeap.GetEmergencyRequests();
            if (emergencyRequests.Any())
            {
                dgvRequests.DataSource = emergencyRequests.Select(r => new
                {
                    r.RequestId,
                    r.Description,
                    r.Category,
                    r.Status,
                    r.SubmissionDate
                }).ToList();
            }
            else
            {
                MessageBox.Show("No emergency requests found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Navigates back to the main menu
        /// </summary>
        private void btnBackToMenu_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenuForm mainMenu = new MainMenuForm();
            mainMenu.Show();
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Handles the click event for adding a new request
        /// </summary>
        private void btnAddRequest_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewDescription.Text))
            {
                MessageBox.Show("Please enter a description.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Create new request with selected status
            var request = new ServiceRequest(nextRequestId++, txtNewDescription.Text, cboCategory.SelectedItem.ToString());
            request.UpdateStatus(cboStatus.SelectedItem.ToString());  // Set selected status
            
            // Add to data structures
            requestTree.Insert(request);
            requestGraph.AddRequest(request);
            
            // Create relationships between similar requests
            var allRequests = requestTree.InOrderTraversal();
            foreach (var existingRequest in allRequests)
            {
                if (existingRequest.RequestId != request.RequestId && 
                    existingRequest.Category == request.Category)
                {
                    Console.WriteLine($"Creating relationship between {request.RequestId} ({request.Category}) and {existingRequest.RequestId} ({existingRequest.Category})");
                    requestGraph.AddRelation(existingRequest.RequestId, request.RequestId);
                }
            }
            
            // Add to emergency heap if status is Emergency Response
            if (request.Status == "Emergency Response")
            {
                emergencyHeap.Insert(request);
            }
            
            UpdateRequestsDisplay();
            
            // Clear inputs but keep status as is
            txtNewDescription.Clear();
            cboCategory.SelectedIndex = 0;
            
            MessageBox.Show($"Request added successfully. Request ID: {nextRequestId - 1}", 
                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Handles the form closing event
        /// </summary>
        private void ServiceRequestStatusForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    }
}
