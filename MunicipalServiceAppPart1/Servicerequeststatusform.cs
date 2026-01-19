using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MunicipalServiceAppPart1
{
    public partial class ServiceRequestStatusForm : Form
    {
        private ServiceRequestManager requestManager;
        private TextBox txtSearchId;
        private Button btnSearch;
        private Button btnViewAll;
        private Button btnViewByPriority;
        private Button btnViewByStatus;
        private Button btnViewDependencies;
        private Button btnBack;
        private ListBox lstRequests;
        private RichTextBox rtbDetails;
        private ComboBox cmbStatus;
        private ComboBox cmbCategory;
        private Button btnFilterStatus;
        private Button btnFilterCategory;
        private Label lblStats;
        private ProgressBar progressBar;

        public ServiceRequestStatusForm()
        {
            InitializeComponent();
            requestManager = new ServiceRequestManager();
            LoadAllRequests();
            UpdateStatistics();
        }

        private void InitializeComponent()
        {
            this.Text = "Service Request Status";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 248, 255);

            // Title Label
            Label lblTitle = new Label();
            lblTitle.Text = "Service Request Status Tracking";
            lblTitle.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitle.Location = new Point(20, 20);
            lblTitle.Size = new Size(500, 40);
            lblTitle.ForeColor = Color.FromArgb(0, 102, 204);
            this.Controls.Add(lblTitle);

            // Search Panel
            Panel searchPanel = new Panel();
            searchPanel.Location = new Point(20, 70);
            searchPanel.Size = new Size(940, 50);
            searchPanel.BorderStyle = BorderStyle.FixedSingle;
            searchPanel.BackColor = Color.White;
            this.Controls.Add(searchPanel);

            Label lblSearch = new Label();
            lblSearch.Text = "Search by Request ID:";
            lblSearch.Location = new Point(10, 15);
            lblSearch.Size = new Size(150, 20);
            searchPanel.Controls.Add(lblSearch);

            txtSearchId = new TextBox();
            txtSearchId.Location = new Point(170, 12);
            txtSearchId.Size = new Size(200, 25);
            searchPanel.Controls.Add(txtSearchId);

            btnSearch = new Button();
            btnSearch.Text = "Search";
            btnSearch.Location = new Point(380, 10);
            btnSearch.Size = new Size(100, 30);
            btnSearch.BackColor = Color.FromArgb(0, 123, 255);
            btnSearch.ForeColor = Color.White;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Click += BtnSearch_Click;
            searchPanel.Controls.Add(btnSearch);

            btnViewAll = new Button();
            btnViewAll.Text = "View All";
            btnViewAll.Location = new Point(490, 10);
            btnViewAll.Size = new Size(100, 30);
            btnViewAll.BackColor = Color.FromArgb(40, 167, 69);
            btnViewAll.ForeColor = Color.White;
            btnViewAll.FlatStyle = FlatStyle.Flat;
            btnViewAll.Click += BtnViewAll_Click;
            searchPanel.Controls.Add(btnViewAll);

            btnViewByPriority = new Button();
            btnViewByPriority.Text = "By Priority";
            btnViewByPriority.Location = new Point(600, 10);
            btnViewByPriority.Size = new Size(100, 30);
            btnViewByPriority.BackColor = Color.FromArgb(255, 193, 7);
            btnViewByPriority.ForeColor = Color.Black;
            btnViewByPriority.FlatStyle = FlatStyle.Flat;
            btnViewByPriority.Click += BtnViewByPriority_Click;
            searchPanel.Controls.Add(btnViewByPriority);

            btnViewDependencies = new Button();
            btnViewDependencies.Text = "Dependencies";
            btnViewDependencies.Location = new Point(710, 10);
            btnViewDependencies.Size = new Size(110, 30);
            btnViewDependencies.BackColor = Color.FromArgb(108, 117, 125);
            btnViewDependencies.ForeColor = Color.White;
            btnViewDependencies.FlatStyle = FlatStyle.Flat;
            btnViewDependencies.Click += BtnViewDependencies_Click;
            searchPanel.Controls.Add(btnViewDependencies);

            // Filter Panel
            Panel filterPanel = new Panel();
            filterPanel.Location = new Point(20, 130);
            filterPanel.Size = new Size(940, 50);
            filterPanel.BorderStyle = BorderStyle.FixedSingle;
            filterPanel.BackColor = Color.White;
            this.Controls.Add(filterPanel);

            Label lblFilterStatus = new Label();
            lblFilterStatus.Text = "Filter by Status:";
            lblFilterStatus.Location = new Point(10, 15);
            lblFilterStatus.Size = new Size(100, 20);
            filterPanel.Controls.Add(lblFilterStatus);

            cmbStatus = new ComboBox();
            cmbStatus.Location = new Point(120, 12);
            cmbStatus.Size = new Size(150, 25);
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.Items.AddRange(new string[] { "All", "Pending", "In Progress", "Completed" });
            cmbStatus.SelectedIndex = 0;
            filterPanel.Controls.Add(cmbStatus);

            btnFilterStatus = new Button();
            btnFilterStatus.Text = "Apply";
            btnFilterStatus.Location = new Point(280, 10);
            btnFilterStatus.Size = new Size(80, 30);
            btnFilterStatus.BackColor = Color.FromArgb(0, 123, 255);
            btnFilterStatus.ForeColor = Color.White;
            btnFilterStatus.FlatStyle = FlatStyle.Flat;
            btnFilterStatus.Click += BtnFilterStatus_Click;
            filterPanel.Controls.Add(btnFilterStatus);

            Label lblFilterCategory = new Label();
            lblFilterCategory.Text = "Filter by Category:";
            lblFilterCategory.Location = new Point(400, 15);
            lblFilterCategory.Size = new Size(120, 20);
            filterPanel.Controls.Add(lblFilterCategory);

            cmbCategory = new ComboBox();
            cmbCategory.Location = new Point(530, 12);
            cmbCategory.Size = new Size(150, 25);
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.Items.AddRange(new string[] { "All", "Roads", "Utilities", "Water", "Sanitation", "Parks" });
            cmbCategory.SelectedIndex = 0;
            filterPanel.Controls.Add(cmbCategory);

            btnFilterCategory = new Button();
            btnFilterCategory.Text = "Apply";
            btnFilterCategory.Location = new Point(690, 10);
            btnFilterCategory.Size = new Size(80, 30);
            btnFilterCategory.BackColor = Color.FromArgb(0, 123, 255);
            btnFilterCategory.ForeColor = Color.White;
            btnFilterCategory.FlatStyle = FlatStyle.Flat;
            btnFilterCategory.Click += BtnFilterCategory_Click;
            filterPanel.Controls.Add(btnFilterCategory);

            // Statistics Label
            lblStats = new Label();
            lblStats.Location = new Point(20, 190);
            lblStats.Size = new Size(940, 40);
            lblStats.BackColor = Color.FromArgb(255, 248, 220);
            lblStats.BorderStyle = BorderStyle.FixedSingle;
            lblStats.Padding = new Padding(10);
            lblStats.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            this.Controls.Add(lblStats);

            // Progress Bar
            progressBar = new ProgressBar();
            progressBar.Location = new Point(20, 235);
            progressBar.Size = new Size(940, 25);
            progressBar.Style = ProgressBarStyle.Continuous;
            this.Controls.Add(progressBar);

            // ListBox for requests
            Label lblRequests = new Label();
            lblRequests.Text = "Service Requests:";
            lblRequests.Location = new Point(20, 270);
            lblRequests.Size = new Size(200, 20);
            lblRequests.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.Controls.Add(lblRequests);

            lstRequests = new ListBox();
            lstRequests.Location = new Point(20, 295);
            lstRequests.Size = new Size(550, 280);
            lstRequests.Font = new Font("Consolas", 9);
            lstRequests.DrawMode = DrawMode.OwnerDrawFixed; // Enable custom drawing
            lstRequests.ItemHeight = 28; // Taller items for better visibility
            lstRequests.SelectedIndexChanged += LstRequests_SelectedIndexChanged;
            lstRequests.DrawItem += LstRequests_DrawItem; // Custom draw event
            this.Controls.Add(lstRequests);

            // RichTextBox for details
            Label lblDetails = new Label();
            lblDetails.Text = "Request Details:";
            lblDetails.Location = new Point(590, 270);
            lblDetails.Size = new Size(200, 20);
            lblDetails.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.Controls.Add(lblDetails);

            rtbDetails = new RichTextBox();
            rtbDetails.Location = new Point(590, 295);
            rtbDetails.Size = new Size(370, 280);
            rtbDetails.Font = new Font("Segoe UI", 9);
            rtbDetails.ReadOnly = true;
            rtbDetails.BackColor = Color.FromArgb(248, 249, 250);
            this.Controls.Add(rtbDetails);

            // Back button
            btnBack = new Button();
            btnBack.Text = "Back to Main Menu";
            btnBack.Location = new Point(420, 590);
            btnBack.Size = new Size(150, 40);
            btnBack.BackColor = Color.FromArgb(108, 117, 125); // Grey color
            btnBack.ForeColor = Color.White;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnBack.Cursor = Cursors.Hand;
            btnBack.Click += BtnBack_Click;
            btnBack.MouseEnter += (s, e) => btnBack.BackColor = Color.FromArgb(90, 98, 104);
            btnBack.MouseLeave += (s, e) => btnBack.BackColor = Color.FromArgb(108, 117, 125);
            this.Controls.Add(btnBack);
        }

        private void LoadAllRequests()
        {
            var requests = requestManager.GetAllRequests();
            DisplayRequests(requests);
        }

        private void DisplayRequests(List<ServiceRequest> requests)
        {
            lstRequests.Items.Clear();
            foreach (var request in requests)
            {
                // Store the request object directly for easier access in DrawItem
                lstRequests.Items.Add(request);
            }

            // Update progress bar based on completion rate
            int total = requests.Count;
            int completed = requests.Count(r => r.Status == "Completed");
            if (total > 0)
            {
                progressBar.Value = (int)((double)completed / total * 100);
            }
        }

        // Custom draw method for color-coded list items
        private void LstRequests_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            e.DrawBackground();

            ServiceRequest request = (ServiceRequest)lstRequests.Items[e.Index];

            // Determine colors based on priority
            Color priorityColor;
            Color textColor = Color.White;
            string priorityLabel;

            switch (request.Priority)
            {
                case 1:
                    priorityColor = Color.FromArgb(220, 53, 69); // Red - High Priority
                    priorityLabel = "HIGH";
                    break;
                case 2:
                    priorityColor = Color.FromArgb(255, 193, 7); // Yellow - Medium Priority
                    textColor = Color.Black;
                    priorityLabel = "MEDIUM";
                    break;
                case 3:
                    priorityColor = Color.FromArgb(40, 167, 69); // Light Green - Low Priority
                    priorityLabel = "LOW";
                    break;
                case 4:
                    priorityColor = Color.FromArgb(23, 162, 184); // Teal/Green - Very Low Priority
                    priorityLabel = "VERY LOW";
                    break;
                default:
                    priorityColor = Color.Gray;
                    priorityLabel = "UNKNOWN";
                    break;
            }

            // Draw priority indicator bar on the left
            using (SolidBrush priorityBrush = new SolidBrush(priorityColor))
            {
                Rectangle priorityRect = new Rectangle(e.Bounds.Left, e.Bounds.Top, 8, e.Bounds.Height);
                e.Graphics.FillRectangle(priorityBrush, priorityRect);
            }

            // Draw priority badge
            Rectangle badgeRect = new Rectangle(e.Bounds.Left + 12, e.Bounds.Top + 4, 75, 20);
            using (SolidBrush badgeBrush = new SolidBrush(priorityColor))
            {
                e.Graphics.FillRectangle(badgeBrush, badgeRect);
            }

            // Draw priority text
            using (Font badgeFont = new Font("Segoe UI", 7, FontStyle.Bold))
            using (SolidBrush badgeTextBrush = new SolidBrush(textColor))
            {
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString(priorityLabel, badgeFont, badgeTextBrush, badgeRect, sf);
            }

            // Draw request details
            string displayText = $"ID: {request.RequestId}  |  {request.Category}  |  {request.Location}  |  Status: {request.Status}";

            // Background color for selected item
            Color bgColor = (e.State & DrawItemState.Selected) == DrawItemState.Selected
                ? Color.FromArgb(230, 240, 255)
                : Color.White;

            using (SolidBrush bgBrush = new SolidBrush(bgColor))
            {
                Rectangle textBgRect = new Rectangle(e.Bounds.Left + 8, e.Bounds.Top, e.Bounds.Width - 8, e.Bounds.Height);
                e.Graphics.FillRectangle(bgBrush, textBgRect);
            }

            // Redraw priority bar and badge on top of background
            using (SolidBrush priorityBrush = new SolidBrush(priorityColor))
            {
                Rectangle priorityRect = new Rectangle(e.Bounds.Left, e.Bounds.Top, 8, e.Bounds.Height);
                e.Graphics.FillRectangle(priorityBrush, priorityRect);
                e.Graphics.FillRectangle(priorityBrush, badgeRect);
            }

            using (Font badgeFont = new Font("Segoe UI", 7, FontStyle.Bold))
            using (SolidBrush badgeTextBrush = new SolidBrush(textColor))
            {
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString(priorityLabel, badgeFont, badgeTextBrush, badgeRect, sf);
            }

            // Draw main text
            using (Font itemFont = new Font("Segoe UI", 9, FontStyle.Regular))
            using (SolidBrush textBrush = new SolidBrush(Color.FromArgb(50, 50, 50)))
            {
                Rectangle textRect = new Rectangle(e.Bounds.Left + 95, e.Bounds.Top + 6, e.Bounds.Width - 100, e.Bounds.Height);
                e.Graphics.DrawString(displayText, itemFont, textBrush, textRect);
            }

            // Draw border
            using (Pen borderPen = new Pen(Color.FromArgb(200, 200, 200), 1))
            {
                e.Graphics.DrawRectangle(borderPen, new Rectangle(e.Bounds.Left + 8, e.Bounds.Top, e.Bounds.Width - 9, e.Bounds.Height - 1));
            }

            e.DrawFocusRectangle();
        }

        private void LstRequests_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstRequests.SelectedIndex >= 0)
            {
                ServiceRequest request = (ServiceRequest)lstRequests.SelectedItem;

                if (request != null)
                {
                    rtbDetails.Clear();

                    // Add title with color coding
                    rtbDetails.SelectionFont = new Font("Segoe UI", 12, FontStyle.Bold);
                    rtbDetails.SelectionColor = Color.FromArgb(41, 128, 185);
                    rtbDetails.AppendText("━━━━━━━━━━━━━━━━━━━━━━━━━━━\n");
                    rtbDetails.AppendText("   SERVICE REQUEST DETAILS\n");
                    rtbDetails.AppendText("━━━━━━━━━━━━━━━━━━━━━━━━━━━\n\n");

                    // Priority indicator with color
                    rtbDetails.SelectionFont = new Font("Segoe UI", 9, FontStyle.Bold);
                    rtbDetails.AppendText("Priority Level: ");

                    string priorityText;
                    Color priorityColor;
                    switch (request.Priority)
                    {
                        case 1:
                            priorityText = "🔴 HIGH PRIORITY";
                            priorityColor = Color.FromArgb(220, 53, 69);
                            break;
                        case 2:
                            priorityText = "🟡 MEDIUM PRIORITY";
                            priorityColor = Color.FromArgb(255, 140, 0);
                            break;
                        case 3:
                            priorityText = "🟢 LOW PRIORITY";
                            priorityColor = Color.FromArgb(40, 167, 69);
                            break;
                        case 4:
                            priorityText = "🔵 VERY LOW PRIORITY";
                            priorityColor = Color.FromArgb(23, 162, 184);
                            break;
                        default:
                            priorityText = "UNKNOWN";
                            priorityColor = Color.Gray;
                            break;
                    }

                    rtbDetails.SelectionColor = priorityColor;
                    rtbDetails.SelectionFont = new Font("Segoe UI", 10, FontStyle.Bold);
                    rtbDetails.AppendText(priorityText + "\n\n");

                    // Request details
                    rtbDetails.SelectionFont = new Font("Segoe UI", 9, FontStyle.Regular);
                    rtbDetails.SelectionColor = Color.Black;

                    AddDetailLine("Request ID:", request.RequestId);
                    AddDetailLine("Category:", request.Category);
                    AddDetailLine("Location:", request.Location);
                    AddDetailLine("Status:", request.Status);
                    AddDetailLine("Date Submitted:", request.DateSubmitted.ToShortDateString());

                    int daysAgo = (DateTime.Now - request.DateSubmitted).Days;
                    string ageText = daysAgo == 0 ? "Today" : daysAgo == 1 ? "1 day ago" : $"{daysAgo} days ago";
                    AddDetailLine("Request Age:", ageText);

                    rtbDetails.AppendText("\n");
                    rtbDetails.SelectionFont = new Font("Segoe UI", 9, FontStyle.Bold);
                    rtbDetails.AppendText("Description:\n");
                    rtbDetails.SelectionFont = new Font("Segoe UI", 9, FontStyle.Regular);
                    rtbDetails.AppendText($"{request.Description}\n");

                    // Show dependencies
                    var dependencies = requestManager.GetRequestDependencies(request.RequestId);
                    if (dependencies.Count > 0)
                    {
                        rtbDetails.AppendText("\n");
                        rtbDetails.SelectionFont = new Font("Segoe UI", 9, FontStyle.Bold);
                        rtbDetails.SelectionColor = Color.FromArgb(230, 126, 34);
                        rtbDetails.AppendText("⚠️ Dependencies:\n");
                        rtbDetails.SelectionFont = new Font("Segoe UI", 9, FontStyle.Regular);
                        rtbDetails.SelectionColor = Color.Black;
                        rtbDetails.AppendText("This request depends on:\n");
                        foreach (var dep in dependencies)
                        {
                            rtbDetails.AppendText($"  • {dep.RequestId}: {dep.Description}\n");
                        }
                    }
                }
            }
        }

        // Helper method to add formatted detail lines
        private void AddDetailLine(string label, string value)
        {
            rtbDetails.SelectionFont = new Font("Segoe UI", 9, FontStyle.Bold);
            rtbDetails.SelectionColor = Color.FromArgb(70, 70, 70);
            rtbDetails.AppendText($"{label} ");

            rtbDetails.SelectionFont = new Font("Segoe UI", 9, FontStyle.Regular);
            rtbDetails.SelectionColor = Color.Black;
            rtbDetails.AppendText($"{value}\n");
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchId = txtSearchId.Text.Trim();
            if (!string.IsNullOrEmpty(searchId))
            {
                var request = requestManager.SearchByIdBST(searchId);
                if (request != null)
                {
                    DisplayRequests(new List<ServiceRequest> { request });
                    MessageBox.Show($"Request found!\nStatus: {request.Status}",
                                  "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Request not found!", "Search Result",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please enter a Request ID", "Input Required",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnViewAll_Click(object sender, EventArgs e)
        {
            LoadAllRequests();
            UpdateStatistics();
        }

        private void BtnViewByPriority_Click(object sender, EventArgs e)
        {
            var requests = requestManager.GetRequestsByPriority();
            DisplayRequests(requests);
            MessageBox.Show("Requests sorted by priority (1 = Highest)",
                          "Sorted by Priority", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnViewDependencies_Click(object sender, EventArgs e)
        {
            if (lstRequests.SelectedIndex >= 0)
            {
                ServiceRequest request = (ServiceRequest)lstRequests.SelectedItem;

                var relatedRequests = requestManager.GetRelatedRequests(request.RequestId);
                if (relatedRequests.Count > 1)
                {
                    DisplayRequests(relatedRequests);
                    MessageBox.Show($"Found {relatedRequests.Count} related requests (including selected)",
                                  "Related Requests", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No dependencies found for this request",
                                  "Dependencies", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please select a request first", "Selection Required",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnFilterStatus_Click(object sender, EventArgs e)
        {
            string selectedStatus = cmbStatus.SelectedItem.ToString();
            if (selectedStatus == "All")
            {
                LoadAllRequests();
            }
            else
            {
                var requests = requestManager.GetRequestsByStatus(selectedStatus);
                DisplayRequests(requests);
            }
            UpdateStatistics();
        }

        private void BtnFilterCategory_Click(object sender, EventArgs e)
        {
            string selectedCategory = cmbCategory.SelectedItem.ToString();
            if (selectedCategory == "All")
            {
                LoadAllRequests();
            }
            else
            {
                var requests = requestManager.GetRequestsByCategory(selectedCategory);
                DisplayRequests(requests);
            }
        }

        private void UpdateStatistics()
        {
            var statusStats = requestManager.GetStatusStatistics();
            string statsText = "Statistics - ";
            foreach (var stat in statusStats)
            {
                statsText += $"{stat.Key}: {stat.Value}  |  ";
            }
            lblStats.Text = statsText.TrimEnd('|', ' ');
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}


//Bibliography
//College, I. V., 2025. PROG7312 Module-Manual / Module-Outline. Pretoria: Varsity College Pretoria.
//Geeks, G. f., 2025. Introduction to C# Windows Forms Applications. [Online] 
//Available at: https://www.geeksforgeeks.org/c-sharp/introduction-to-c-sharp-windows-forms-applications/
//[Accessed 8 September 2025].
//Microsoft, 2025.Tutorial: Create a Windows Forms app in Visual Studio with C#. [Online] 
//Available at: https://learn.microsoft.com/en-us/visualstudio/ide/create-csharp-winform-visual-studio?view=vs-2022
//[Accessed 8 September 2025].
//ProgrammingKnowledge2, 2023.Create Your First C# Windows Forms Application using Visual Studio. [Online]
//Available at: https://youtu.be/JSJ1Jl2aLJg?si=A4tSAz_ueBg5cOgf
//[Accessed 08 September 2025].
//BroCode, 2021. Tree data structures in 2 minutes. [Online]
//Available at: https://youtu.be/Etpc_-br5rl?si=tY2wjw9rX62kTGUD
//[Accessed 12 November 2025].