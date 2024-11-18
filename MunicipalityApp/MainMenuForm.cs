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
    public partial class MainMenuForm : Form
    {

        private IssueManager _issueManager;
        public MainMenuForm()
        {
            InitializeComponent();
            _issueManager = new IssueManager();
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        private void MainMenuForm_Load(object sender, EventArgs e)
        {
            // Disable buttons for features not implemented yet
            btnLocalEvents.Enabled = false;
            // btnServiceStatus.Enabled = false;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        private void btnReportIssues_Click(object sender, EventArgs e)
        {
            this.Hide();  // Hide the Main Menu Form
            //this .Enabled = false;
            ReportIssuesForm reportIssuesForm = new ReportIssuesForm(_issueManager, this);
            reportIssuesForm.Show();  // Open the Report Issues Form
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        private void MainMenuForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void btnLocalEvents_Click(object sender, EventArgs e)
        {
            this.Hide();  // Hide the Main Menu Form
            LocalEventsForm eventsForm = new LocalEventsForm();
            eventsForm.Show();
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        private void btnServiceStatus_Click(object sender, EventArgs e)
        {
            this.Hide();
            ServiceRequestStatusForm serviceStatusForm = new ServiceRequestStatusForm();
            serviceStatusForm.Show();
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    }
}
