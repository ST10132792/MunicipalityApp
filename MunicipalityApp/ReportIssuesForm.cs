using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MunicipalityApp
{
    public partial class ReportIssuesForm : Form
    {

        private IssueManager _issueManager;
        string selectedMediaFile = "";
        private MainMenuForm _mainMenuForm;

        public ReportIssuesForm(IssueManager issueManager, MainMenuForm mainMenuForm)
        {
            InitializeComponent();
            _issueManager = issueManager;
            _mainMenuForm = mainMenuForm;
            PopulateCategoryComboBox();
            dgvReportedIssues.CellClick += DgvReportedIssues_CellClick;
            UpdateReportedIssuesGrid();
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Method handling the open media button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvReportedIssues_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the "OpenFile" button was clicked
            if (e.ColumnIndex == dgvReportedIssues.Columns["OpenFile"].Index && e.RowIndex >= 0)
            {
                // Get the media file path for the selected row
                ReportedIssue selectedIssue = _issueManager.ReportedIssues[e.RowIndex];
                string filePath = selectedIssue.MediaFilePath;

                if (!string.IsNullOrEmpty(filePath))
                {
                    try
                    {
                        // Open the file using the default program
                        System.Diagnostics.Process.Start(filePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error opening file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No file attached for this issue.", "No File", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Adding data to select from in the combo box
        /// </summary>
        private void PopulateCategoryComboBox()
        {
            cbCategory.Items.Add("Sanitation");
            cbCategory.Items.Add("Roads");
            cbCategory.Items.Add("Utilities");
            cbCategory.Items.Add("Water Supply");
            cbCategory.Items.Add("Electricity");
            cbCategory.Items.Add("Public Transport");
            cbCategory.Items.Add("Waste Management");
            cbCategory.Items.Add("Street Lighting");
            cbCategory.Items.Add("Other");
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Handles the attach media button clicking event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAttachMedia_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png|Document Files|*.pdf";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                selectedMediaFile = openFileDialog.FileName;
                MessageBox.Show("File attached: " + selectedMediaFile);
            }
            else
            {
                selectedMediaFile = "";
            }

            UpdateProgressBar();
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Handles the submit button clicking event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Input validation
            if (string.IsNullOrEmpty(txtLocation.Text))
            {
                MessageBox.Show("Please enter a location.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cbCategory.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a category.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(rtbDescription.Text))
            {
                MessageBox.Show("Please enter a description of the issue.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Create a new ReportedIssue object and add it to the list
            try
            {
                ReportedIssue issue = new ReportedIssue
                {
                    Location = txtLocation.Text,
                    Category = cbCategory.SelectedItem.ToString(),
                    Description = rtbDescription.Text,
                    MediaFilePath = selectedMediaFile
                };

                _issueManager.AddIssue(issue);

                // Feedback to the user
                MessageBox.Show("Issue reported successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear fields after submission
                txtLocation.Clear();
                rtbDescription.Clear();
                cbCategory.SelectedIndex = -1;
                selectedMediaFile = "";

                // Reset the progress bar just in case
                progressBar.Value = 0;

                // Update the DataGridView with the latest list of reported issues
                UpdateReportedIssuesGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Handles the return to menu button clicking event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBackToMenu_Click(object sender, EventArgs e)
        {
            this.Hide();
            //MainMenuForm mainMenu = new MainMenuForm();
            //mainMenu.Show();
            _mainMenuForm.Show();
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// update the progress bar to provide better user engagement
        /// </summary>
        private void UpdateProgressBar()
        {
            int progress = 0;

            if (!string.IsNullOrEmpty(txtLocation.Text))
                progress += 25;

            if (cbCategory.SelectedIndex != -1)
                progress += 25;

            if (!string.IsNullOrEmpty(rtbDescription.Text))
                progress += 25;

            if (!string.IsNullOrEmpty(selectedMediaFile))
                progress += 25;

            progressBar.Value = progress;
        }

        private void txtLocation_TextChanged(object sender, EventArgs e)
        {
            UpdateProgressBar();
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateProgressBar();
        }

        private void rtbDescription_TextChanged(object sender, EventArgs e)
        {
            UpdateProgressBar();
        }
        
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// makes sure app is closed when the 'X' top right is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportIssuesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Method to update the datagrid with issues (temporary addition to the application)
        /// </summary>
        private void UpdateReportedIssuesGrid()
        {
            dgvReportedIssues.DataSource = null;  // Clear the current binding
            dgvReportedIssues.DataSource = _issueManager.ReportedIssues.Select(issue => new
            {
                Location = issue.Location,
                Category = issue.Category,
                Description = issue.Description,
                MediaFile = string.IsNullOrEmpty(issue.MediaFilePath) ? "No File Attached" : "File Attached"
            }).ToList();

            // Add Open button if it doesn't exist
            if (!dgvReportedIssues.Columns.Contains("OpenFile"))
            {
                DataGridViewButtonColumn openFileButtonColumn = new DataGridViewButtonColumn();
                openFileButtonColumn.Name = "OpenFile";
                openFileButtonColumn.HeaderText = "Open Media";
                openFileButtonColumn.Text = "Open";
                openFileButtonColumn.UseColumnTextForButtonValue = true;
                dgvReportedIssues.Columns.Add(openFileButtonColumn);
            }

            // makes sure the OpenFile button is on the far right
            dgvReportedIssues.Columns["OpenFile"].DisplayIndex = dgvReportedIssues.ColumnCount - 1;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    }
}
