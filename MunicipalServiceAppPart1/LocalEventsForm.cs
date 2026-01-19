using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace MunicipalServiceAppPart1
{
    public partial class LocalEventsForm : Form
    {
        private ComboBox categoryFilterComboBox;
        private DateTimePicker startDatePicker;
        private DateTimePicker endDatePicker;
        private TextBox searchTextBox;
        private Button searchButton;
        private Button clearFilterButton;
        private ComboBox sortComboBox;
        private ListBox eventsListBox;
        private RichTextBox eventDetailsRichTextBox;
        private Label recommendationLabel;
        private ListBox recommendationsListBox;
        private Label statsLabel;
        private PictureBox municipalityLogo;
        private List<Event> currentDisplayedEvents;
        private Panel headerPanel;
        private Panel mainContentPanel;

        public LocalEventsForm()
        {
            currentDisplayedEvents = new List<Event>();
            InitializeComponent();
            LoadEvents();
            LoadRecommendations();
            UpdateStatistics();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form properties
            this.Text = "Local Events and Announcements";
            this.Size = new Size(1200, 820);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(236, 240, 241);
            this.Font = new Font("Segoe UI", 9F);

            // Add custom paint for bold border
            this.Paint += LocalEventsForm_Paint;

            // ======================
            // HEADER PANEL
            // ======================
            headerPanel = new Panel();
            headerPanel.Location = new Point(0, 0);
            headerPanel.Size = new Size(1200, 140);
            headerPanel.BackColor = Color.FromArgb(52, 152, 219);
            headerPanel.Paint += HeaderPanel_Paint;
            this.Controls.Add(headerPanel);

            // Municipality Logo
            municipalityLogo = new PictureBox();
            municipalityLogo.Location = new Point(30, 20);
            municipalityLogo.Size = new Size(100, 100);
            municipalityLogo.SizeMode = PictureBoxSizeMode.Zoom;
            municipalityLogo.BackColor = Color.Transparent;

            Bitmap logoBitmap = new Bitmap(100, 100);
            using (Graphics g = Graphics.FromImage(logoBitmap))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);

                // White circle background
                g.FillEllipse(Brushes.White, 5, 5, 90, 90);

                // Inner gradient circle
                using (LinearGradientBrush gradBrush = new LinearGradientBrush(
                    new Rectangle(10, 10, 80, 80),
                    Color.FromArgb(255, 184, 28),
                    Color.FromArgb(241, 196, 15),
                    45f))
                {
                    g.FillEllipse(gradBrush, 15, 15, 70, 70);
                }

                // Calendar icon
                using (Font iconFont = new Font("Segoe UI", 14, FontStyle.Bold))
                {
                    g.DrawString("📅", iconFont, Brushes.White,
                        new RectangleF(0, 0, 100, 100),
                        new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                }

                // Border
                using (Pen borderPen = new Pen(Color.FromArgb(52, 152, 219), 3))
                {
                    g.DrawEllipse(borderPen, 5, 5, 90, 90);
                }
            }
            municipalityLogo.Image = logoBitmap;
            headerPanel.Controls.Add(municipalityLogo);

            // Title Label
            Label titleLabel = new Label();
            titleLabel.Text = "Local Events & Announcements";
            titleLabel.Font = new Font("Segoe UI", 26, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.Size = new Size(700, 45);
            titleLabel.Location = new Point(150, 25);
            titleLabel.TextAlign = ContentAlignment.MiddleLeft;
            titleLabel.BackColor = Color.Transparent;
            headerPanel.Controls.Add(titleLabel);

            // Municipality Name Label
            Label municipalityLabel = new Label();
            municipalityLabel.Text = "Johannesburg Metropolitan Municipality";
            municipalityLabel.Font = new Font("Segoe UI", 12, FontStyle.Regular);
            municipalityLabel.ForeColor = Color.FromArgb(236, 240, 241);
            municipalityLabel.Size = new Size(700, 25);
            municipalityLabel.Location = new Point(150, 75);
            municipalityLabel.TextAlign = ContentAlignment.MiddleLeft;
            municipalityLabel.BackColor = Color.Transparent;
            headerPanel.Controls.Add(municipalityLabel);

            // Statistics Label
            statsLabel = new Label();
            statsLabel.Text = "Loading statistics...";
            statsLabel.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            statsLabel.ForeColor = Color.FromArgb(255, 255, 255);
            statsLabel.Size = new Size(700, 25);
            statsLabel.Location = new Point(150, 105);
            statsLabel.TextAlign = ContentAlignment.MiddleLeft;
            statsLabel.BackColor = Color.Transparent;
            headerPanel.Controls.Add(statsLabel);

            // ======================
            // HEADER BUTTONS (Top Right)
            // ======================
            Button backButton = CreateModernButton("← Back to Main Menu", new Point(870, 20), new Size(300, 50),
            Color.FromArgb(127, 140, 141), Color.White);  // CHANGED TO GREY
            backButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            backButton.Click += BackButton_Click;
            headerPanel.Controls.Add(backButton);

            Button refreshButton = CreateModernButton("🔄 Refresh Events", new Point(870, 80), new Size(145, 40),
                Color.FromArgb(149, 165, 166), Color.White);  // CHANGED TO GREY
            refreshButton.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            refreshButton.Click += RefreshButton_Click;
            headerPanel.Controls.Add(refreshButton);

            Button exportButton = CreateModernButton("💾 Export", new Point(1025, 80), new Size(145, 40),
            Color.FromArgb(149, 165, 166), Color.White);  // CHANGED TO GREY
            exportButton.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            exportButton.Click += (s, e) =>
            {
                NotificationService.ShowNotification("Export", "Event export feature - coming soon!");
            };
            headerPanel.Controls.Add(exportButton);

            // ======================
            // MAIN CONTENT PANEL
            // ======================
            mainContentPanel = new Panel();
            mainContentPanel.Location = new Point(15, 155);
            mainContentPanel.Size = new Size(1170, 640);
            mainContentPanel.BackColor = Color.Transparent;
            mainContentPanel.AutoScroll = false;
            this.Controls.Add(mainContentPanel);

            // ======================
            // SEARCH AND FILTER CARD
            // ======================
            Panel filterCard = CreateModernCard(new Point(0, 0), new Size(1170, 150),
                Color.FromArgb(255, 255, 255), Color.FromArgb(52, 152, 219));
            mainContentPanel.Controls.Add(filterCard);

            // Search Section Title
            Label searchSectionLabel = new Label();
            searchSectionLabel.Text = "🔍 SEARCH EVENTS";
            searchSectionLabel.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            searchSectionLabel.ForeColor = Color.FromArgb(52, 152, 219);
            searchSectionLabel.Location = new Point(20, 10);
            searchSectionLabel.Size = new Size(500, 25);
            searchSectionLabel.BackColor = Color.FromArgb(240, 248, 255);
            searchSectionLabel.TextAlign = ContentAlignment.MiddleLeft;
            searchSectionLabel.Padding = new Padding(5, 0, 0, 0);
            filterCard.Controls.Add(searchSectionLabel);

            searchTextBox = new TextBox();
            searchTextBox.Font = new Font("Segoe UI", 11);
            searchTextBox.Location = new Point(20, 45);
            searchTextBox.Size = new Size(350, 30);
            searchTextBox.Text = "Search by keyword...";
            searchTextBox.ForeColor = Color.Gray;
            searchTextBox.BorderStyle = BorderStyle.FixedSingle;
            searchTextBox.Enter += SearchTextBox_Enter;
            searchTextBox.Leave += SearchTextBox_Leave;
            filterCard.Controls.Add(searchTextBox);

            searchButton = CreateModernButton("🔍 Search", new Point(380, 43), new Size(120, 34),
                Color.FromArgb(46, 204, 113), Color.White);
            searchButton.Click += SearchButton_Click;
            filterCard.Controls.Add(searchButton);

            // Filter Section Title
            Label filterSectionLabel = new Label();
            filterSectionLabel.Text = "⚙️ FILTERS & SORTING";
            filterSectionLabel.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            filterSectionLabel.ForeColor = Color.FromArgb(155, 89, 182);
            filterSectionLabel.Location = new Point(20, 85);
            filterSectionLabel.Size = new Size(500, 25);
            filterSectionLabel.BackColor = Color.FromArgb(248, 243, 255);
            filterSectionLabel.TextAlign = ContentAlignment.MiddleLeft;
            filterSectionLabel.Padding = new Padding(5, 0, 0, 0);
            filterCard.Controls.Add(filterSectionLabel);

            // Category Filter
            Label categoryLabel = new Label();
            categoryLabel.Text = "Category:";
            categoryLabel.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            categoryLabel.ForeColor = Color.FromArgb(52, 73, 94);
            categoryLabel.Location = new Point(520, 10);
            categoryLabel.Size = new Size(70, 20);
            filterCard.Controls.Add(categoryLabel);

            categoryFilterComboBox = new ComboBox();
            categoryFilterComboBox.Font = new Font("Segoe UI", 10);
            categoryFilterComboBox.Location = new Point(520, 35);
            categoryFilterComboBox.Size = new Size(200, 30);
            categoryFilterComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            categoryFilterComboBox.BackColor = Color.White;
            categoryFilterComboBox.Items.Add("All Categories");
            foreach (var category in EventManager.GetAllCategories().OrderBy(c => c))
            {
                categoryFilterComboBox.Items.Add(category);
            }
            categoryFilterComboBox.SelectedIndex = 0;
            categoryFilterComboBox.SelectedIndexChanged += FilterChanged;
            filterCard.Controls.Add(categoryFilterComboBox);

            // Date Range
            Label dateLabel = new Label();
            dateLabel.Text = "Date Range:";
            dateLabel.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dateLabel.ForeColor = Color.FromArgb(52, 73, 94);
            dateLabel.Location = new Point(740, 10);
            dateLabel.Size = new Size(80, 20);
            filterCard.Controls.Add(dateLabel);

            startDatePicker = new DateTimePicker();
            startDatePicker.Font = new Font("Segoe UI", 9);
            startDatePicker.Location = new Point(740, 35);
            startDatePicker.Size = new Size(150, 25);
            startDatePicker.Value = DateTime.Now;
            startDatePicker.ValueChanged += FilterChanged;
            filterCard.Controls.Add(startDatePicker);

            Label toLabel = new Label();
            toLabel.Text = "to";
            toLabel.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            toLabel.ForeColor = Color.FromArgb(52, 73, 94);
            toLabel.Location = new Point(900, 37);
            toLabel.Size = new Size(20, 20);
            toLabel.TextAlign = ContentAlignment.MiddleCenter;
            filterCard.Controls.Add(toLabel);

            endDatePicker = new DateTimePicker();
            endDatePicker.Font = new Font("Segoe UI", 9);
            endDatePicker.Location = new Point(930, 35);
            endDatePicker.Size = new Size(150, 25);
            endDatePicker.Value = DateTime.Now.AddMonths(3);
            endDatePicker.ValueChanged += FilterChanged;
            filterCard.Controls.Add(endDatePicker);

            // Sort ComboBox
            Label sortLabel = new Label();
            sortLabel.Text = "Sort by:";
            sortLabel.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            sortLabel.ForeColor = Color.FromArgb(52, 73, 94);
            sortLabel.Location = new Point(35, 118);
            sortLabel.Size = new Size(60, 20);
            filterCard.Controls.Add(sortLabel);

            sortComboBox = new ComboBox();
            sortComboBox.Font = new Font("Segoe UI", 10);
            sortComboBox.Location = new Point(100, 115);
            sortComboBox.Size = new Size(220, 30);
            sortComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            sortComboBox.BackColor = Color.White;
            sortComboBox.Items.AddRange(new string[] {
                "Date (Earliest First)",
                "Date (Latest First)",
                "Name (A-Z)",
                "Name (Z-A)",
                "Category (A-Z)",
                "Priority (High to Low)"
            });
            sortComboBox.SelectedIndex = 0;
            sortComboBox.SelectedIndexChanged += SortComboBox_Changed;
            filterCard.Controls.Add(sortComboBox);

            // Clear Filters Button
            clearFilterButton = CreateModernButton("✖ Clear All Filters", new Point(340, 113), new Size(160, 34),
                Color.FromArgb(231, 76, 60), Color.White);
            clearFilterButton.Click += ClearFilterButton_Click;
            filterCard.Controls.Add(clearFilterButton);

            // Priority Events Button
            Button viewPriorityButton = CreateModernButton("⚠️ Priority Events", new Point(920, 110), new Size(160, 34),
                Color.FromArgb(230, 126, 34), Color.White);
            viewPriorityButton.Click += (s, e) =>
            {
                currentDisplayedEvents = EventManager.GetPriorityEvents();
                ApplySorting();
                NotificationService.ShowNotification("Priority Events", $"Showing {currentDisplayedEvents.Count} priority events");
                UpdateStatistics();
            };
            filterCard.Controls.Add(viewPriorityButton);

            // ======================
            // EVENTS LIST CARD
            // ======================
            Panel eventsCard = CreateModernCard(new Point(0, 165), new Size(570, 455),
                Color.FromArgb(255, 255, 255), Color.FromArgb(46, 204, 113));
            mainContentPanel.Controls.Add(eventsCard);

            Label eventsListLabel = new Label();
            eventsListLabel.Text = "📋 UPCOMING EVENTS";
            eventsListLabel.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            eventsListLabel.ForeColor = Color.White;
            eventsListLabel.BackColor = Color.FromArgb(46, 204, 113);
            eventsListLabel.Location = new Point(0, 0);
            eventsListLabel.Size = new Size(570, 40);
            eventsListLabel.TextAlign = ContentAlignment.MiddleLeft;
            eventsListLabel.Padding = new Padding(15, 0, 0, 0);
            eventsCard.Controls.Add(eventsListLabel);

            eventsListBox = new ListBox();
            eventsListBox.Font = new Font("Segoe UI", 10);
            eventsListBox.Location = new Point(10, 50);
            eventsListBox.Size = new Size(550, 395);
            eventsListBox.BorderStyle = BorderStyle.FixedSingle;
            eventsListBox.BackColor = Color.FromArgb(250, 252, 253);
            eventsListBox.SelectedIndexChanged += EventsListBox_SelectedIndexChanged;
            eventsListBox.DrawMode = DrawMode.OwnerDrawFixed;
            eventsListBox.ItemHeight = 50;
            eventsListBox.DrawItem += EventsListBox_DrawItem;
            eventsCard.Controls.Add(eventsListBox);

            // ======================
            // EVENT DETAILS CARD
            // ======================
            Panel detailsCard = CreateModernCard(new Point(585, 165), new Size(585, 280),
                Color.FromArgb(255, 255, 255), Color.FromArgb(52, 152, 219));
            mainContentPanel.Controls.Add(detailsCard);

            Label detailsLabel = new Label();
            detailsLabel.Text = "📄 EVENT DETAILS";
            detailsLabel.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            detailsLabel.ForeColor = Color.White;
            detailsLabel.BackColor = Color.FromArgb(52, 152, 219);
            detailsLabel.Location = new Point(0, 0);
            detailsLabel.Size = new Size(585, 40);
            detailsLabel.TextAlign = ContentAlignment.MiddleLeft;
            detailsLabel.Padding = new Padding(15, 0, 0, 0);
            detailsCard.Controls.Add(detailsLabel);

            eventDetailsRichTextBox = new RichTextBox();
            eventDetailsRichTextBox.Font = new Font("Segoe UI", 10);
            eventDetailsRichTextBox.Location = new Point(10, 50);
            eventDetailsRichTextBox.Size = new Size(565, 220);
            eventDetailsRichTextBox.ReadOnly = true;
            eventDetailsRichTextBox.BackColor = Color.FromArgb(250, 252, 253);
            eventDetailsRichTextBox.BorderStyle = BorderStyle.FixedSingle;
            eventDetailsRichTextBox.Text = "Select an event to view details...";
            detailsCard.Controls.Add(eventDetailsRichTextBox);

            // ======================
            // RECOMMENDATIONS CARD
            // ======================
            Panel recommendationsCard = CreateModernCard(new Point(585, 460), new Size(585, 160),
                Color.FromArgb(255, 255, 255), Color.FromArgb(155, 89, 182));
            mainContentPanel.Controls.Add(recommendationsCard);

            recommendationLabel = new Label();
            recommendationLabel.Text = "⭐ RECOMMENDED FOR YOU";
            recommendationLabel.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            recommendationLabel.ForeColor = Color.White;
            recommendationLabel.BackColor = Color.FromArgb(155, 89, 182);
            recommendationLabel.Location = new Point(0, 0);
            recommendationLabel.Size = new Size(585, 40);
            recommendationLabel.TextAlign = ContentAlignment.MiddleLeft;
            recommendationLabel.Padding = new Padding(15, 0, 0, 0);
            recommendationsCard.Controls.Add(recommendationLabel);

            recommendationsListBox = new ListBox();
            recommendationsListBox.Font = new Font("Segoe UI", 9);
            recommendationsListBox.Location = new Point(10, 50);
            recommendationsListBox.Size = new Size(565, 100);
            recommendationsListBox.BorderStyle = BorderStyle.FixedSingle;
            recommendationsListBox.BackColor = Color.FromArgb(250, 252, 253);
            recommendationsListBox.SelectedIndexChanged += RecommendationsListBox_SelectedIndexChanged;
            recommendationsCard.Controls.Add(recommendationsListBox);

            this.ResumeLayout(false);
        }

        // Create modern card panel with bold colored border
        private Panel CreateModernCard(Point location, Size size, Color bgColor, Color borderColor)
        {
            Panel card = new Panel();
            card.Location = location;
            card.Size = size;
            card.BackColor = bgColor;
            card.Paint += (s, e) =>
            {
                // Draw bold colored border
                using (Pen borderPen = new Pen(borderColor, 4))
                {
                    e.Graphics.DrawRectangle(borderPen, 2, 2, size.Width - 4, size.Height - 4);
                }

                // Draw inner shadow for depth
                using (Pen shadowPen = new Pen(Color.FromArgb(30, 0, 0, 0), 1))
                {
                    e.Graphics.DrawRectangle(shadowPen, 5, 5, size.Width - 10, size.Height - 10);
                }
            };
            return card;
        }

        // Create modern styled button with bold styling
        private Button CreateModernButton(string text, Point location, Size size, Color bgColor, Color fgColor)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btn.Size = size;
            btn.Location = location;
            btn.BackColor = bgColor;
            btn.ForeColor = fgColor;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 2;
            btn.FlatAppearance.BorderColor = ControlPaint.Dark(bgColor, 0.2f);
            btn.Cursor = Cursors.Hand;

            Color hoverColor = ControlPaint.Light(bgColor, 0.2f);
            btn.MouseEnter += (s, e) =>
            {
                btn.BackColor = hoverColor;
                btn.FlatAppearance.BorderColor = ControlPaint.Dark(hoverColor, 0.2f);
            };
            btn.MouseLeave += (s, e) =>
            {
                btn.BackColor = bgColor;
                btn.FlatAppearance.BorderColor = ControlPaint.Dark(bgColor, 0.2f);
            };

            return btn;
        }

        // Custom draw for events list items with vibrant styling
        private void EventsListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            e.DrawBackground();

            Event evt = eventsListBox.Items[e.Index] as Event;
            if (evt != null)
            {
                bool isSelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

                // Background with gradient
                Color bgColor = isSelected ? Color.FromArgb(46, 204, 113) : Color.White;
                using (SolidBrush bgBrush = new SolidBrush(bgColor))
                {
                    e.Graphics.FillRectangle(bgBrush, e.Bounds);
                }

                // Bold priority indicator bar
                Color priorityColor = evt.Priority <= 2 ? Color.FromArgb(231, 76, 60) :
                                     evt.Priority == 3 ? Color.FromArgb(243, 156, 18) :
                                     Color.FromArgb(149, 165, 166);
                using (SolidBrush priorityBrush = new SolidBrush(priorityColor))
                {
                    e.Graphics.FillRectangle(priorityBrush, e.Bounds.X, e.Bounds.Y, 6, e.Bounds.Height);
                }

                // Draw border around item
                using (Pen itemBorder = new Pen(Color.FromArgb(189, 195, 199), 1))
                {
                    e.Graphics.DrawRectangle(itemBorder, e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1);
                }

                // Text colors
                Color textColor = isSelected ? Color.White : Color.FromArgb(44, 62, 80);
                Color dateColor = isSelected ? Color.FromArgb(236, 240, 241) : Color.FromArgb(127, 140, 141);

                // Event title
                using (Font titleFont = new Font("Segoe UI", 10, FontStyle.Bold))
                using (SolidBrush textBrush = new SolidBrush(textColor))
                {
                    e.Graphics.DrawString(evt.Title, titleFont, textBrush,
                        new RectangleF(e.Bounds.X + 12, e.Bounds.Y + 8, e.Bounds.Width - 20, 22));
                }

                // Event date and location
                using (Font detailFont = new Font("Segoe UI", 9))
                using (SolidBrush dateBrush = new SolidBrush(dateColor))
                {
                    string details = $"📅 {evt.EventDate:MMM dd, yyyy} • 📍 {evt.Location}";
                    e.Graphics.DrawString(details, detailFont, dateBrush,
                        new RectangleF(e.Bounds.X + 12, e.Bounds.Y + 32, e.Bounds.Width - 20, 18));
                }
            }

            e.DrawFocusRectangle();
        }

        // Form border paint - BOLD
        private void LocalEventsForm_Paint(object sender, PaintEventArgs e)
        {
            using (Pen borderPen = new Pen(Color.FromArgb(52, 73, 94), 4))
            {
                e.Graphics.DrawRectangle(borderPen, 2, 2, this.ClientSize.Width - 4, this.ClientSize.Height - 4);
            }
        }

        // Header panel shadow
        private void HeaderPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            // Bottom bold shadow
            using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(60, 0, 0, 0)))
            {
                e.Graphics.FillRectangle(shadowBrush, 0, panel.Height - 6, panel.Width, 6);
            }
        }

        // All the existing event handler methods remain unchanged
        private void SearchTextBox_Enter(object sender, EventArgs e)
        {
            if (searchTextBox.Text == "Search by keyword..." && searchTextBox.ForeColor == Color.Gray)
            {
                searchTextBox.Text = "";
                searchTextBox.ForeColor = Color.Black;
            }
        }

        private void SearchTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchTextBox.Text))
            {
                searchTextBox.Text = "Search by keyword...";
                searchTextBox.ForeColor = Color.Gray;
            }
        }

        private void LoadEvents()
        {
            currentDisplayedEvents = EventManager.GetUpcomingEvents(50);
            ApplySorting();
        }

        private void LoadRecommendations()
        {
            recommendationsListBox.Items.Clear();
            var recommendations = EventManager.GetRecommendedEvents();
            if (recommendations.Count > 0)
            {
                foreach (var evt in recommendations)
                {
                    recommendationsListBox.Items.Add(evt);
                }
            }
            else
            {
                recommendationsListBox.Items.Add("Search for events to get personalized recommendations!");
            }
        }

        private void UpdateStatistics()
        {
            int total = EventManager.GetTotalEventCount();
            int upcoming = EventManager.GetUpcomingEventCount();
            statsLabel.Text = $"📊 Total Events: {total}  |  Upcoming: {upcoming}  |  Displayed: {currentDisplayedEvents.Count}";
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            string keyword = searchTextBox.Text.Trim();
            if (!string.IsNullOrWhiteSpace(keyword) && keyword != "Search by keyword...")
            {
                currentDisplayedEvents = EventManager.SearchEventsByKeyword(keyword);
                if (currentDisplayedEvents.Count > 0)
                {
                    ApplySorting();
                    NotificationService.ShowNotification("Search Complete",
                        $"Found {currentDisplayedEvents.Count} event(s) matching '{keyword}'");
                }
                else
                {
                    eventsListBox.Items.Clear();
                    eventsListBox.Items.Add($"No events found matching '{keyword}'");
                }
                LoadRecommendations();
                UpdateStatistics();
            }
            else
            {
                LoadEvents();
            }
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            string selectedCategory = categoryFilterComboBox.SelectedItem?.ToString();
            DateTime startDate = startDatePicker.Value.Date;
            DateTime endDate = endDatePicker.Value.Date;

            if (selectedCategory == "All Categories" || string.IsNullOrEmpty(selectedCategory))
            {
                currentDisplayedEvents = EventManager.SearchEventsByDateRange(startDate, endDate);
            }
            else
            {
                currentDisplayedEvents = EventManager.SearchEventsByCategory(selectedCategory)
                    .Where(evt => evt.EventDate.Date >= startDate && evt.EventDate.Date <= endDate)
                    .ToList();
            }

            ApplySorting();
            LoadRecommendations();
            UpdateStatistics();
        }

        private void SortComboBox_Changed(object sender, EventArgs e)
        {
            ApplySorting();
        }

        private void ApplySorting()
        {
            if (currentDisplayedEvents == null || currentDisplayedEvents.Count == 0)
            {
                eventsListBox.Items.Clear();
                eventsListBox.Items.Add("No events to display.");
                return;
            }

            List<Event> sortedEvents = new List<Event>(currentDisplayedEvents);

            switch (sortComboBox.SelectedIndex)
            {
                case 0: // Date (Earliest First)
                    sortedEvents = sortedEvents.OrderBy(evt => evt.EventDate).ToList();
                    break;
                case 1: // Date (Latest First)
                    sortedEvents = sortedEvents.OrderByDescending(evt => evt.EventDate).ToList();
                    break;
                case 2: // Name (A-Z)
                    sortedEvents = sortedEvents.OrderBy(evt => evt.Title).ToList();
                    break;
                case 3: // Name (Z-A)
                    sortedEvents = sortedEvents.OrderByDescending(evt => evt.Title).ToList();
                    break;
                case 4: // Category (A-Z)
                    sortedEvents = sortedEvents.OrderBy(evt => evt.Category).ThenBy(evt => evt.EventDate).ToList();
                    break;
                case 5: // Priority (High to Low)
                    sortedEvents = sortedEvents.OrderBy(evt => evt.Priority).ThenBy(evt => evt.EventDate).ToList();
                    break;
            }

            eventsListBox.Items.Clear();
            foreach (var evt in sortedEvents)
            {
                eventsListBox.Items.Add(evt);
            }
        }

        private void ClearFilterButton_Click(object sender, EventArgs e)
        {
            searchTextBox.Text = "Search by keyword...";
            searchTextBox.ForeColor = Color.Gray;
            categoryFilterComboBox.SelectedIndex = 0;
            startDatePicker.Value = DateTime.Now;
            endDatePicker.Value = DateTime.Now.AddMonths(3);
            sortComboBox.SelectedIndex = 0;
            LoadEvents();
            LoadRecommendations();
            UpdateStatistics();
        }

        private void EventsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eventsListBox.SelectedItem is Event selectedEvent)
            {
                eventDetailsRichTextBox.Clear();
                eventDetailsRichTextBox.SelectionFont = new Font("Segoe UI", 14, FontStyle.Bold);
                eventDetailsRichTextBox.SelectionColor = Color.FromArgb(52, 152, 219);
                eventDetailsRichTextBox.AppendText(selectedEvent.Title + "\n\n");

                eventDetailsRichTextBox.SelectionFont = new Font("Segoe UI", 11, FontStyle.Regular);
                eventDetailsRichTextBox.SelectionColor = Color.FromArgb(52, 73, 94);
                eventDetailsRichTextBox.AppendText(selectedEvent.GetDetailedInfo());
            }
        }

        private void RecommendationsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (recommendationsListBox.SelectedItem is Event selectedEvent)
            {
                eventDetailsRichTextBox.Clear();
                eventDetailsRichTextBox.SelectionFont = new Font("Segoe UI", 12, FontStyle.Bold);
                eventDetailsRichTextBox.SelectionColor = Color.FromArgb(155, 89, 182);
                eventDetailsRichTextBox.AppendText("⭐ RECOMMENDED EVENT\n\n");

                eventDetailsRichTextBox.SelectionFont = new Font("Segoe UI", 14, FontStyle.Bold);
                eventDetailsRichTextBox.SelectionColor = Color.FromArgb(52, 152, 219);
                eventDetailsRichTextBox.AppendText(selectedEvent.Title + "\n\n");

                eventDetailsRichTextBox.SelectionFont = new Font("Segoe UI", 11, FontStyle.Regular);
                eventDetailsRichTextBox.SelectionColor = Color.FromArgb(52, 73, 94);
                eventDetailsRichTextBox.AppendText(selectedEvent.GetDetailedInfo());
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            LoadEvents();
            LoadRecommendations();
            UpdateStatistics();
            NotificationService.ShowNotification("Events Refreshed", "Event list has been updated!");
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
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