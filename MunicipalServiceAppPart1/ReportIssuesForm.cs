using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MunicipalServiceAppPart1
{
    public partial class ReportIssuesForm : Form
    {
        private const string LocationPlaceholder = "e.g., Main Street, Johannesburg CBD";
        private const string DescriptionPlaceholder = "Please provide a detailed description of the issue...";

        private TextBox locationTextBox;
        private ComboBox categoryComboBox;
        private RichTextBox descriptionRichTextBox;
        private ListBox attachmentsListBox;
        private Label engagementLabel;
        private ProgressBar progressBar;
        private Label estimatedResponseLabel;
        private Label trackingInfoLabel;
        private List<string> attachedFiles;
        private int formCompletionPercentage = 0;

        public ReportIssuesForm()
        {
            attachedFiles = new List<string>();
            InitializeComponent();
            UpdateEngagementFeature();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form properties
            this.Text = "Report Issues - Municipal Services";
            this.Size = new Size(800, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.White;

            // Title Label
            Label titleLabel = new Label();
            titleLabel.Text = "Report Municipal Issues";
            titleLabel.Font = new Font("Arial", 20, FontStyle.Bold);
            titleLabel.ForeColor = Color.DarkBlue;
            titleLabel.Size = new Size(750, 35);
            titleLabel.Location = new Point(25, 20);
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(titleLabel);

            // Notification Info Label
            Label notificationInfoLabel = new Label();
            notificationInfoLabel.Text = "📱 You'll receive real-time updates on your issue status";
            notificationInfoLabel.Font = new Font("Arial", 10, FontStyle.Italic);
            notificationInfoLabel.ForeColor = Color.FromArgb(46, 204, 113);
            notificationInfoLabel.Size = new Size(750, 20);
            notificationInfoLabel.Location = new Point(25, 55);
            notificationInfoLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(notificationInfoLabel);

            // Location Label and TextBox
            Label locationLabel = new Label();
            locationLabel.Text = "Location of Issue:";
            locationLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            locationLabel.Size = new Size(150, 25);
            locationLabel.Location = new Point(50, 90);
            this.Controls.Add(locationLabel);

            locationTextBox = new TextBox();
            locationTextBox.Font = new Font("Arial", 11);
            locationTextBox.Size = new Size(500, 25);
            locationTextBox.Location = new Point(220, 90);
            locationTextBox.Text = LocationPlaceholder;
            locationTextBox.ForeColor = Color.Gray;
            locationTextBox.Enter += LocationTextBox_Enter;
            locationTextBox.Leave += LocationTextBox_Leave;
            locationTextBox.TextChanged += Field_Changed;
            this.Controls.Add(locationTextBox);

            // Category Label and ComboBox
            Label categoryLabel = new Label();
            categoryLabel.Text = "Issue Category:";
            categoryLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            categoryLabel.Size = new Size(150, 25);
            categoryLabel.Location = new Point(50, 140);
            this.Controls.Add(categoryLabel);

            categoryComboBox = new ComboBox();
            categoryComboBox.Font = new Font("Arial", 11);
            categoryComboBox.Size = new Size(500, 25);
            categoryComboBox.Location = new Point(220, 140);
            categoryComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            categoryComboBox.Items.AddRange(new string[] {
                "Select Category...",
                "Sanitation",
                "Roads and Infrastructure",
                "Water and Utilities",
                "Electricity",
                "Housing",
                "Public Safety",
                "Parks and Recreation",
                "Transportation",
                "Environmental Issues",
                "Other"
            });
            categoryComboBox.SelectedIndex = 0;
            categoryComboBox.SelectedIndexChanged += Field_Changed;
            this.Controls.Add(categoryComboBox);

            // Description Label and RichTextBox
            Label descriptionLabel = new Label();
            descriptionLabel.Text = "Issue Description:";
            descriptionLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            descriptionLabel.Size = new Size(150, 25);
            descriptionLabel.Location = new Point(50, 190);
            this.Controls.Add(descriptionLabel);

            descriptionRichTextBox = new RichTextBox();
            descriptionRichTextBox.Font = new Font("Arial", 11);
            descriptionRichTextBox.Size = new Size(650, 120);
            descriptionRichTextBox.Location = new Point(50, 220);
            descriptionRichTextBox.Text = DescriptionPlaceholder;
            descriptionRichTextBox.ForeColor = Color.Gray;
            descriptionRichTextBox.Enter += DescriptionRichTextBox_Enter;
            descriptionRichTextBox.Leave += DescriptionRichTextBox_Leave;
            descriptionRichTextBox.TextChanged += Field_Changed;
            this.Controls.Add(descriptionRichTextBox);

            // Attachments Label
            Label attachmentsLabel = new Label();
            attachmentsLabel.Text = "Attached Files:";
            attachmentsLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            attachmentsLabel.Size = new Size(150, 25);
            attachmentsLabel.Location = new Point(50, 360);
            this.Controls.Add(attachmentsLabel);

            // Attachments ListBox
            attachmentsListBox = new ListBox();
            attachmentsListBox.Font = new Font("Arial", 10);
            attachmentsListBox.Size = new Size(450, 60);
            attachmentsListBox.Location = new Point(50, 390);
            this.Controls.Add(attachmentsListBox);

            // Attach File Button
            Button attachFileBtn = new Button();
            attachFileBtn.Text = "Attach File";
            attachFileBtn.Font = new Font("Arial", 11, FontStyle.Bold);
            attachFileBtn.Size = new Size(120, 35);
            attachFileBtn.Location = new Point(520, 390);
            attachFileBtn.BackColor = Color.FromArgb(46, 204, 113);
            attachFileBtn.ForeColor = Color.White;
            attachFileBtn.FlatStyle = FlatStyle.Flat;
            attachFileBtn.FlatAppearance.BorderSize = 0;
            attachFileBtn.Click += AttachFileBtn_Click;
            this.Controls.Add(attachFileBtn);

            // Remove File Button
            Button removeFileBtn = new Button();
            removeFileBtn.Text = "Remove";
            removeFileBtn.Font = new Font("Arial", 11, FontStyle.Bold);
            removeFileBtn.Size = new Size(120, 35);
            removeFileBtn.Location = new Point(520, 430);
            removeFileBtn.BackColor = Color.FromArgb(231, 76, 60);
            removeFileBtn.ForeColor = Color.White;
            removeFileBtn.FlatStyle = FlatStyle.Flat;
            removeFileBtn.FlatAppearance.BorderSize = 0;
            removeFileBtn.Click += RemoveFileBtn_Click;
            this.Controls.Add(removeFileBtn);

            // Progress Bar (Engagement Feature)
            progressBar = new ProgressBar();
            progressBar.Size = new Size(500, 20);
            progressBar.Location = new Point(50, 480);
            progressBar.Minimum = 0;
            progressBar.Maximum = 100;
            progressBar.Value = 0;
            this.Controls.Add(progressBar);

            // Engagement Label
            engagementLabel = new Label();
            engagementLabel.Text = "Your Progress: Getting Started!";
            engagementLabel.Font = new Font("Arial", 11, FontStyle.Bold);
            engagementLabel.ForeColor = Color.FromArgb(52, 152, 219);
            engagementLabel.Size = new Size(500, 25);
            engagementLabel.Location = new Point(50, 510);
            engagementLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(engagementLabel);

            // Estimated Response Label
            estimatedResponseLabel = new Label();
            estimatedResponseLabel.Text = "Estimated Response Time: Within 24-48 hours";
            estimatedResponseLabel.Font = new Font("Arial", 10, FontStyle.Italic);
            estimatedResponseLabel.ForeColor = Color.FromArgb(155, 89, 182);
            estimatedResponseLabel.Size = new Size(500, 20);
            estimatedResponseLabel.Location = new Point(50, 535);
            estimatedResponseLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(estimatedResponseLabel);

            // Tracking Info Label
            trackingInfoLabel = new Label();
            trackingInfoLabel.Text = "You will receive a unique tracking ID upon submission";
            trackingInfoLabel.Font = new Font("Arial", 9, FontStyle.Regular);
            trackingInfoLabel.ForeColor = Color.Gray;
            trackingInfoLabel.Size = new Size(500, 20);
            trackingInfoLabel.Location = new Point(50, 555);
            trackingInfoLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(trackingInfoLabel);

            // Submit Button
            Button submitBtn = new Button();
            submitBtn.Text = "Submit Issue Report";
            submitBtn.Font = new Font("Arial", 14, FontStyle.Bold);
            submitBtn.Size = new Size(200, 50);
            submitBtn.Location = new Point(200, 590);
            submitBtn.BackColor = Color.FromArgb(52, 152, 219);
            submitBtn.ForeColor = Color.White;
            submitBtn.FlatStyle = FlatStyle.Flat;
            submitBtn.FlatAppearance.BorderSize = 0;
            submitBtn.Click += SubmitBtn_Click;
            submitBtn.MouseEnter += (s, e) => submitBtn.BackColor = Color.FromArgb(41, 128, 185);
            submitBtn.MouseLeave += (s, e) => submitBtn.BackColor = Color.FromArgb(52, 152, 219);
            this.Controls.Add(submitBtn);

            // Back Button
            Button backBtn = new Button();
            backBtn.Text = "Back to Main Menu";
            backBtn.Font = new Font("Arial", 12, FontStyle.Regular);
            backBtn.Size = new Size(200, 50);
            backBtn.Location = new Point(420, 590);
            backBtn.BackColor = Color.Gray;
            backBtn.ForeColor = Color.White;
            backBtn.FlatStyle = FlatStyle.Flat;
            backBtn.FlatAppearance.BorderSize = 0;
            backBtn.Click += BackBtn_Click;
            backBtn.MouseEnter += (s, e) => backBtn.BackColor = Color.FromArgb(127, 140, 141);
            backBtn.MouseLeave += (s, e) => backBtn.BackColor = Color.Gray;
            this.Controls.Add(backBtn);

            // Tip Label
            Label tipLabel = new Label();
            tipLabel.Text = "Tip: Providing detailed information and photos helps our municipality respond faster!";
            tipLabel.Font = new Font("Arial", 9, FontStyle.Italic);
            tipLabel.ForeColor = Color.Gray;
            tipLabel.Size = new Size(700, 20);
            tipLabel.Location = new Point(50, 660);
            tipLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(tipLabel);

            this.ResumeLayout(false);
        }

        // Manual placeholder handlers for Location
        private void LocationTextBox_Enter(object sender, EventArgs e)
        {
            if (locationTextBox.Text == LocationPlaceholder && locationTextBox.ForeColor == Color.Gray)
            {
                locationTextBox.Text = "";
                locationTextBox.ForeColor = Color.Black;
            }
        }

        private void LocationTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(locationTextBox.Text))
            {
                locationTextBox.Text = LocationPlaceholder;
                locationTextBox.ForeColor = Color.Gray;
            }
        }

        // Manual placeholder handlers for Description
        private void DescriptionRichTextBox_Enter(object sender, EventArgs e)
        {
            if (descriptionRichTextBox.Text == DescriptionPlaceholder &&
                descriptionRichTextBox.ForeColor == Color.Gray)
            {
                descriptionRichTextBox.Text = "";
                descriptionRichTextBox.ForeColor = Color.Black;
            }
        }

        private void DescriptionRichTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(descriptionRichTextBox.Text))
            {
                descriptionRichTextBox.Text = DescriptionPlaceholder;
                descriptionRichTextBox.ForeColor = Color.Gray;
            }
        }

        private void Field_Changed(object sender, EventArgs e)
        {
            UpdateEngagementFeature();
        }

        private void UpdateEngagementFeature()
        {
            int completedFields = 0;
            int totalFields = 3; // Required: Location, Category, Description

            // Check location (ignore placeholder)
            if (!string.IsNullOrWhiteSpace(locationTextBox.Text) &&
                !(locationTextBox.Text == LocationPlaceholder && locationTextBox.ForeColor == Color.Gray))
            {
                completedFields++;
            }

            // Check category
            if (categoryComboBox.SelectedIndex > 0)
                completedFields++;

            // Check description (ignore placeholder)
            if (!string.IsNullOrWhiteSpace(descriptionRichTextBox.Text) &&
                !(descriptionRichTextBox.Text == DescriptionPlaceholder &&
                  descriptionRichTextBox.ForeColor == Color.Gray))
            {
                completedFields++;
            }

            formCompletionPercentage = (completedFields * 100) / totalFields;
            progressBar.Value = Math.Min(formCompletionPercentage, 100);

            // Update engagement message and estimated response time
            if (formCompletionPercentage == 0)
            {
                engagementLabel.Text = "Your Progress: Let's get started!";
                engagementLabel.ForeColor = Color.Gray;
                estimatedResponseLabel.Text = "Complete the form to see estimated response time";
                estimatedResponseLabel.ForeColor = Color.Gray;
            }
            else if (formCompletionPercentage < 50)
            {
                engagementLabel.Text = "Your Progress: Keep going! Your 50% Complete!";
                engagementLabel.ForeColor = Color.Orange;
                estimatedResponseLabel.Text = "Estimated Response Time: Within 48-72 hours";
                estimatedResponseLabel.ForeColor = Color.FromArgb(230, 126, 34);
            }
            else if (formCompletionPercentage < 100)
            {
                engagementLabel.Text = "Your Progress: Almost there!";
                engagementLabel.ForeColor = Color.FromArgb(230, 126, 34);
                estimatedResponseLabel.Text = "Estimated Response Time: Within 24-48 hours";
                estimatedResponseLabel.ForeColor = Color.FromArgb(155, 89, 182);
            }
            else
            {
                engagementLabel.Text = "Your Progress: Ready to submit! Help improve your community!";
                engagementLabel.ForeColor = Color.FromArgb(46, 204, 113);

                // Update estimated response based on category priority
                string selectedCategory = categoryComboBox.SelectedItem?.ToString() ?? "";
                if (selectedCategory == "Electricity" || selectedCategory == "Water and Utilities" ||
                    selectedCategory == "Public Safety")
                {
                    estimatedResponseLabel.Text = "Priority Issue: Response within 12-24 hours";
                    estimatedResponseLabel.ForeColor = Color.FromArgb(231, 76, 60);
                }
                else
                {
                    estimatedResponseLabel.Text = "Estimated Response Time: Within 24-48 hours";
                    estimatedResponseLabel.ForeColor = Color.FromArgb(46, 204, 113);
                }
            }

            // Update tracking info
            if (formCompletionPercentage >= 75)
            {
                trackingInfoLabel.Text = "You will receive SMS and in-app notifications about your issue";
                trackingInfoLabel.ForeColor = Color.FromArgb(46, 204, 113);
            }
        }

        private void AttachFileBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select Files to Attach";
                openFileDialog.Filter = "All Files (*.*)|*.*|Images (*.jpg;*.jpeg;*.png;*.gif;*.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp|Documents (*.pdf;*.doc;*.docx;*.txt)|*.pdf;*.doc;*.docx;*.txt";
                openFileDialog.FilterIndex = 1;
                openFileDialog.Multiselect = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (string fileName in openFileDialog.FileNames)
                    {
                        if (!attachedFiles.Contains(fileName))
                        {
                            attachedFiles.Add(fileName);
                            attachmentsListBox.Items.Add(Path.GetFileName(fileName));
                        }
                    }

                    if (openFileDialog.FileNames.Length > 0)
                    {
                        NotificationService.ShowNotification("Files Attached",
                            $"{openFileDialog.FileNames.Length} file(s) attached successfully! This will help speed up resolution.");
                    }
                }
            }
        }

        private void RemoveFileBtn_Click(object sender, EventArgs e)
        {
            if (attachmentsListBox.SelectedIndex >= 0)
            {
                int selectedIndex = attachmentsListBox.SelectedIndex;
                attachedFiles.RemoveAt(selectedIndex);
                attachmentsListBox.Items.RemoveAt(selectedIndex);

                NotificationService.ShowNotification("File Removed", "File removed successfully!");
            }
            else
            {
                MessageBox.Show("Please select a file to remove.", "No File Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            // Validate required fields
            if (!ValidateForm())
                return;

            try
            {
                // Show immediate feedback
                NotificationService.ShowProgressUpdate("Processing your issue report...");

                // Create new issue
                Issue newIssue = new Issue(
                    locationTextBox.Text.Trim(),
                    categoryComboBox.SelectedItem.ToString(),
                    descriptionRichTextBox.Text.Trim()
                );

                // Add attachments
                foreach (string file in attachedFiles)
                {
                    newIssue.AddAttachment(file);
                }

                // Add to issue manager (this will trigger notification and simulate status changes)
                IssueManager.AddIssue(newIssue);

                // Clear form for next use
                ClearForm();

                // Engagement message
                NotificationService.ShowProgressUpdate(
                    $"Your community now has {IssueManager.GetTotalIssueCount()} active issues being tracked. Thank you for your civic participation!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while submitting your issue:\n{ex.Message}",
                    "Submission Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateForm()
        {
            // Check location (ignore placeholder)
            if (string.IsNullOrWhiteSpace(locationTextBox.Text) ||
                (locationTextBox.Text == LocationPlaceholder && locationTextBox.ForeColor == Color.Gray))
            {
                MessageBox.Show("Please enter the location of the issue.", "Location Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                locationTextBox.Focus();
                return false;
            }

            // Check category
            if (categoryComboBox.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select an issue category.", "Category Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                categoryComboBox.Focus();
                return false;
            }

            // Check description (ignore placeholder)
            if (string.IsNullOrWhiteSpace(descriptionRichTextBox.Text) ||
                (descriptionRichTextBox.Text == DescriptionPlaceholder &&
                 descriptionRichTextBox.ForeColor == Color.Gray))
            {
                MessageBox.Show("Please provide a description of the issue.", "Description Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                descriptionRichTextBox.Focus();
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            // Reset location with placeholder
            locationTextBox.Text = LocationPlaceholder;
            locationTextBox.ForeColor = Color.Gray;

            categoryComboBox.SelectedIndex = 0;

            // Reset description with placeholder
            descriptionRichTextBox.Text = DescriptionPlaceholder;
            descriptionRichTextBox.ForeColor = Color.Gray;

            attachedFiles.Clear();
            attachmentsListBox.Items.Clear();
            UpdateEngagementFeature();
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            // Do NOT instantiate a new MainMenuForm here.
            // Just close this form; MainMenuForm will re-show via its FormClosed handler.
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Only exit the application if this is the last open form (safety for Alt+F4 etc.)
            if (e.CloseReason == CloseReason.UserClosing && Application.OpenForms.Count <= 1)
            {
                Application.Exit();
            }
            base.OnFormClosing(e);
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