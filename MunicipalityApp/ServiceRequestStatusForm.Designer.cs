using System;
using System.Drawing;
using System.Windows.Forms;

namespace MunicipalityApp
{
    partial class ServiceRequestStatusForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgvRequests = new System.Windows.Forms.DataGridView();
            this.grpTracking = new System.Windows.Forms.GroupBox();
            this.txtRequestId = new System.Windows.Forms.TextBox();
            this.lblRequestId = new System.Windows.Forms.Label();
            this.btnTrack = new System.Windows.Forms.Button();
            this.txtStatus = new System.Windows.Forms.RichTextBox();
            this.btnBackToMenu = new System.Windows.Forms.Button();
            this.lblAllRequests = new System.Windows.Forms.Label();
            this.btnShowEmergency = new System.Windows.Forms.Button();
            this.grpNewRequest = new System.Windows.Forms.GroupBox();
            this.txtNewDescription = new System.Windows.Forms.TextBox();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.btnAddRequest = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequests)).BeginInit();
            this.grpTracking.SuspendLayout();
            this.grpNewRequest.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblTitle.Location = new System.Drawing.Point(12, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(355, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Service Request Status";
            // 
            // dgvRequests
            // 
            this.dgvRequests.AllowUserToAddRows = false;
            this.dgvRequests.AllowUserToDeleteRows = false;
            this.dgvRequests.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRequests.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRequests.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(25)))), ((int)(((byte)(72)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRequests.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRequests.EnableHeadersVisualStyles = false;
            this.dgvRequests.Location = new System.Drawing.Point(15, 110);
            this.dgvRequests.Name = "dgvRequests";
            this.dgvRequests.ReadOnly = true;
            this.dgvRequests.RowHeadersVisible = false;
            this.dgvRequests.Size = new System.Drawing.Size(800, 200);
            this.dgvRequests.TabIndex = 1;
            // 
            // grpTracking
            // 
            this.grpTracking.Controls.Add(this.txtRequestId);
            this.grpTracking.Controls.Add(this.lblRequestId);
            this.grpTracking.Controls.Add(this.btnTrack);
            this.grpTracking.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.grpTracking.Location = new System.Drawing.Point(15, 480);
            this.grpTracking.Name = "grpTracking";
            this.grpTracking.Size = new System.Drawing.Size(800, 80);
            this.grpTracking.TabIndex = 2;
            this.grpTracking.TabStop = false;
            this.grpTracking.Text = "Track Request";
            // 
            // txtRequestId
            // 
            this.txtRequestId.Location = new System.Drawing.Point(110, 35);
            this.txtRequestId.Name = "txtRequestId";
            this.txtRequestId.Size = new System.Drawing.Size(100, 25);
            this.txtRequestId.TabIndex = 0;
            // 
            // lblRequestId
            // 
            this.lblRequestId.AutoSize = true;
            this.lblRequestId.Location = new System.Drawing.Point(20, 35);
            this.lblRequestId.Name = "lblRequestId";
            this.lblRequestId.Size = new System.Drawing.Size(79, 19);
            this.lblRequestId.TabIndex = 1;
            this.lblRequestId.Text = "Request ID:";
            // 
            // btnTrack
            // 
            this.btnTrack.BackColor = System.Drawing.Color.SteelBlue;
            this.btnTrack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrack.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTrack.ForeColor = System.Drawing.Color.White;
            this.btnTrack.Location = new System.Drawing.Point(220, 35);
            this.btnTrack.Name = "btnTrack";
            this.btnTrack.Size = new System.Drawing.Size(100, 30);
            this.btnTrack.TabIndex = 2;
            this.btnTrack.Text = "Track";
            this.btnTrack.UseVisualStyleBackColor = false;
            this.btnTrack.Click += new System.EventHandler(this.btnTrack_Click);
            // 
            // txtStatus
            // 
            this.txtStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtStatus.Location = new System.Drawing.Point(15, 570);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(800, 150);
            this.txtStatus.TabIndex = 3;
            this.txtStatus.Text = "";
            // 
            // btnBackToMenu
            // 
            this.btnBackToMenu.BackColor = System.Drawing.Color.Gray;
            this.btnBackToMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackToMenu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBackToMenu.ForeColor = System.Drawing.Color.White;
            this.btnBackToMenu.Location = new System.Drawing.Point(15, 730);
            this.btnBackToMenu.Name = "btnBackToMenu";
            this.btnBackToMenu.Size = new System.Drawing.Size(120, 35);
            this.btnBackToMenu.TabIndex = 4;
            this.btnBackToMenu.Text = "Back to Menu";
            this.btnBackToMenu.UseVisualStyleBackColor = false;
            this.btnBackToMenu.Click += new System.EventHandler(this.btnBackToMenu_Click);
            // 
            // lblAllRequests
            // 
            this.lblAllRequests.AutoSize = true;
            this.lblAllRequests.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblAllRequests.ForeColor = System.Drawing.Color.Gray;
            this.lblAllRequests.Location = new System.Drawing.Point(15, 80);
            this.lblAllRequests.Name = "lblAllRequests";
            this.lblAllRequests.Size = new System.Drawing.Size(167, 21);
            this.lblAllRequests.TabIndex = 5;
            this.lblAllRequests.Text = "All Service Requests:";
            // 
            // btnShowEmergency
            // 
            this.btnShowEmergency.BackColor = System.Drawing.Color.Red;
            this.btnShowEmergency.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowEmergency.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnShowEmergency.ForeColor = System.Drawing.Color.White;
            this.btnShowEmergency.Location = new System.Drawing.Point(665, 75);
            this.btnShowEmergency.Name = "btnShowEmergency";
            this.btnShowEmergency.Size = new System.Drawing.Size(150, 30);
            this.btnShowEmergency.TabIndex = 6;
            this.btnShowEmergency.Text = "Show Emergency Requests";
            this.btnShowEmergency.UseVisualStyleBackColor = false;
            this.btnShowEmergency.Click += new System.EventHandler(this.btnShowEmergency_Click);
            // 
            // grpNewRequest
            // 
            this.grpNewRequest.Controls.Add(this.txtNewDescription);
            this.grpNewRequest.Controls.Add(this.cboCategory);
            this.grpNewRequest.Controls.Add(this.lblDescription);
            this.grpNewRequest.Controls.Add(this.lblCategory);
            this.grpNewRequest.Controls.Add(this.btnAddRequest);
            this.grpNewRequest.Controls.Add(this.lblStatus);
            this.grpNewRequest.Controls.Add(this.cboStatus);
            this.grpNewRequest.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.grpNewRequest.Location = new System.Drawing.Point(15, 320);
            this.grpNewRequest.Name = "grpNewRequest";
            this.grpNewRequest.Size = new System.Drawing.Size(800, 150);
            this.grpNewRequest.TabIndex = 7;
            this.grpNewRequest.TabStop = false;
            this.grpNewRequest.Text = "Add New Request";
            // 
            // txtNewDescription
            // 
            this.txtNewDescription.Location = new System.Drawing.Point(110, 30);
            this.txtNewDescription.Name = "txtNewDescription";
            this.txtNewDescription.Size = new System.Drawing.Size(670, 25);
            this.txtNewDescription.TabIndex = 0;
            // 
            // cboCategory
            // 
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.Location = new System.Drawing.Point(110, 70);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(200, 25);
            this.cboCategory.TabIndex = 1;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(20, 30);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(81, 19);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Description:";
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(20, 70);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(68, 19);
            this.lblCategory.TabIndex = 3;
            this.lblCategory.Text = "Category:";
            // 
            // btnAddRequest
            // 
            this.btnAddRequest.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnAddRequest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddRequest.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddRequest.ForeColor = System.Drawing.Color.White;
            this.btnAddRequest.Location = new System.Drawing.Point(680, 110);
            this.btnAddRequest.Name = "btnAddRequest";
            this.btnAddRequest.Size = new System.Drawing.Size(100, 30);
            this.btnAddRequest.TabIndex = 4;
            this.btnAddRequest.Text = "Add Request";
            this.btnAddRequest.UseVisualStyleBackColor = false;
            this.btnAddRequest.Click += new System.EventHandler(this.btnAddRequest_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(330, 70);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(50, 19);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "Status:";
            // 
            // cboStatus
            // 
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.Location = new System.Drawing.Point(420, 70);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(200, 25);
            this.cboStatus.TabIndex = 6;
            // 
            // ServiceRequestStatusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(830, 780);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.dgvRequests);
            this.Controls.Add(this.grpTracking);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.btnBackToMenu);
            this.Controls.Add(this.lblAllRequests);
            this.Controls.Add(this.btnShowEmergency);
            this.Controls.Add(this.grpNewRequest);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ServiceRequestStatusForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Service Request Status";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServiceRequestStatusForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequests)).EndInit();
            this.grpTracking.ResumeLayout(false);
            this.grpTracking.PerformLayout();
            this.grpNewRequest.ResumeLayout(false);
            this.grpNewRequest.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvRequests;
        private System.Windows.Forms.GroupBox grpTracking;
        private System.Windows.Forms.TextBox txtRequestId;
        private System.Windows.Forms.Label lblRequestId;
        private System.Windows.Forms.Button btnTrack;
        private System.Windows.Forms.RichTextBox txtStatus;
        private System.Windows.Forms.Button btnBackToMenu;
        private System.Windows.Forms.Label lblAllRequests;
        private System.Windows.Forms.Button btnShowEmergency;
        private System.Windows.Forms.GroupBox grpNewRequest;
        private System.Windows.Forms.TextBox txtNewDescription;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Button btnAddRequest;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cboStatus;
    }
}