using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace MunicipalServiceAppPart1
{
    public partial class MainMenuForm : Form
    {
        private Timer notificationTimer;
        private PictureBox logoBox;

        public MainMenuForm()
        {
            InitializeComponent();
            SetupNotificationTimer();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form properties
            this.Text = "Municipal Services Application - South Africa";
            this.Size = new Size(700, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Padding = new Padding(12);

            // Set background image
            SetBackgroundImage();

            // Make form double-buffered to prevent flickering
            this.DoubleBuffered = true;

            // Paint event for colorful border
            this.Paint += MainMenuForm_Paint;

            // LOGO
            logoBox = new PictureBox();
            logoBox.Size = new Size(160, 160);
            logoBox.Location = new Point(270, 20); // moved up a bit
            logoBox.SizeMode = PictureBoxSizeMode.Zoom;
            logoBox.BackColor = Color.Transparent;
            logoBox.Image = CreateUltraDetailedMunicipalityLogo(); // (curved text improved)
            this.Controls.Add(logoBox);

            // Title Panel (moved slightly up)
            Panel titlePanel = new Panel();
            titlePanel.Size = new Size(640, 110);
            titlePanel.Location = new Point(30, 180);
            titlePanel.BackColor = Color.FromArgb(240, 255, 255, 255);
            this.Controls.Add(titlePanel);

            Label titleLabel = new Label();
            titleLabel.Text = "Municipal Services Application";
            titleLabel.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(41, 128, 185);
            titleLabel.Size = new Size(640, 38);
            titleLabel.Location = new Point(0, 12);
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            titleLabel.BackColor = Color.Transparent;
            titlePanel.Controls.Add(titleLabel);

            Label subtitleLabel = new Label();
            subtitleLabel.Text = "City of Johannesburg Metropolitan Municipality";
            subtitleLabel.Font = new Font("Segoe UI", 12, FontStyle.Italic);
            subtitleLabel.ForeColor = Color.FromArgb(0, 119, 73);
            subtitleLabel.Size = new Size(640, 28);
            subtitleLabel.Location = new Point(0, 52);
            subtitleLabel.TextAlign = ContentAlignment.MiddleCenter;
            subtitleLabel.BackColor = Color.Transparent;
            titlePanel.Controls.Add(subtitleLabel);

            Label notificationLabel = new Label();
            notificationLabel.Text = "🔔 Real-time Push Notifications Enabled";
            notificationLabel.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            notificationLabel.ForeColor = Color.FromArgb(46, 204, 113);
            notificationLabel.Size = new Size(640, 22);
            notificationLabel.Location = new Point(0, 82);
            notificationLabel.TextAlign = ContentAlignment.MiddleCenter;
            notificationLabel.BackColor = Color.Transparent;
            titlePanel.Controls.Add(notificationLabel);

            // Separator line (moved up)
            Panel separatorLine = new Panel();
            separatorLine.Size = new Size(640, 2);
            separatorLine.Location = new Point(30, 305);
            separatorLine.BackColor = Color.FromArgb(100, 41, 128, 185);
            this.Controls.Add(separatorLine);

            // Main action buttons - slightly smaller + nudged up
            Button reportIssuesBtn = CreateProfessionalButton(
                "🔧 Report Issues",
                new Point(160, 325),                   // up by ~20px
                Color.FromArgb(52, 152, 219),
                Color.FromArgb(41, 128, 185)
            );
            reportIssuesBtn.Click += ReportIssuesBtn_Click;
            this.Controls.Add(reportIssuesBtn);

            Button localEventsBtn = CreateProfessionalButton(
                "📅 Local Events & Announcements",
                new Point(160, 400),                   // up by ~25px
                Color.FromArgb(46, 204, 113),
                Color.FromArgb(39, 174, 96)
            );
            localEventsBtn.Click += LocalEventsBtn_Click;
            this.Controls.Add(localEventsBtn);

            // UPDATED: Service Request Status button - NOW ENABLED
            Button serviceStatusBtn = CreateProfessionalButton(
                "📊 Service Request Status",
                new Point(160, 475),                   // up by ~30px
                Color.FromArgb(255, 193, 7),
                Color.FromArgb(230, 170, 0)
            );
            serviceStatusBtn.Click += ServiceStatusBtn_Click; // ADDED CLICK HANDLER
            this.Controls.Add(serviceStatusBtn);

            // Secondary button (more room now)
            Button checkNotificationsBtn = new Button();
            checkNotificationsBtn.Text = "🔔 View Notifications";
            checkNotificationsBtn.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            checkNotificationsBtn.Size = new Size(220, 36);
            checkNotificationsBtn.Location = new Point(240, 540); // up from 565
            checkNotificationsBtn.BackColor = Color.FromArgb(155, 89, 182);
            checkNotificationsBtn.ForeColor = Color.White;
            checkNotificationsBtn.FlatStyle = FlatStyle.Flat;
            checkNotificationsBtn.FlatAppearance.BorderSize = 0;
            checkNotificationsBtn.Cursor = Cursors.Hand;
            checkNotificationsBtn.Click += CheckNotificationsBtn_Click;
            checkNotificationsBtn.MouseEnter += (s, e) => checkNotificationsBtn.BackColor = Color.FromArgb(142, 68, 173);
            checkNotificationsBtn.MouseLeave += (s, e) => checkNotificationsBtn.BackColor = Color.FromArgb(155, 89, 182);
            this.Controls.Add(checkNotificationsBtn);

            // Footer
            Label footerLabel = new Label();
            footerLabel.Text = "© 2025 City of Johannesburg • Serving Our Communities with Excellence";
            footerLabel.Font = new Font("Segoe UI", 8, FontStyle.Italic);
            footerLabel.ForeColor = Color.FromArgb(120, 120, 120);
            footerLabel.Size = new Size(640, 20);
            footerLabel.Location = new Point(30, 605);
            footerLabel.TextAlign = ContentAlignment.MiddleCenter;
            footerLabel.BackColor = Color.Transparent;
            this.Controls.Add(footerLabel);

            this.ResumeLayout(false);
        }

        // Slightly smaller button footprint; centered text; same behavior.
        private Button CreateProfessionalButton(string text, Point location, Color normalColor, Color hoverColor)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Font = new Font("Segoe UI", 13, FontStyle.Bold); // 14 -> 13 for better fit
            btn.Size = new Size(380, 58);                        // 400x65 -> 380x58
            btn.Location = location;
            btn.BackColor = normalColor;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor = Cursors.Hand;
            btn.TextAlign = ContentAlignment.MiddleCenter;
            btn.UseCompatibleTextRendering = false;

            btn.MouseEnter += (s, e) => btn.BackColor = hoverColor;
            btn.MouseLeave += (s, e) => btn.BackColor = normalColor;

            return btn;
        }

        // Paint border
        private void MainMenuForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int borderWidth = 10;
            Rectangle borderRect = new Rectangle(
                borderWidth / 2,
                borderWidth / 2,
                this.ClientSize.Width - borderWidth,
                this.ClientSize.Height - borderWidth
            );

            // South African flag colors
            Color[] saColors = new Color[]
            {
                Color.FromArgb(0, 119, 73),
                Color.FromArgb(255, 184, 28),
                Color.FromArgb(222, 56, 49),
                Color.FromArgb(0, 22, 137),
                Color.FromArgb(0, 0, 0),
                Color.FromArgb(255, 255, 255)
            };

            int segmentAngle = 360 / saColors.Length;

            for (int i = 0; i < saColors.Length; i++)
            {
                using (LinearGradientBrush gradBrush = new LinearGradientBrush(
                    borderRect,
                    saColors[i],
                    Color.FromArgb(Math.Max(0, saColors[i].R - 30),
                                   Math.Max(0, saColors[i].G - 30),
                                   Math.Max(0, saColors[i].B - 30)),
                    (float)(i * segmentAngle)))
                {
                    using (Pen colorPen = new Pen(gradBrush, borderWidth))
                    {
                        colorPen.StartCap = LineCap.Round;
                        colorPen.EndCap = LineCap.Round;
                        g.DrawArc(colorPen, borderRect, i * segmentAngle, segmentAngle);
                    }
                }
            }

            // Inner borders
            using (Pen accentPen = new Pen(Color.FromArgb(255, 184, 28), 3))
            {
                Rectangle innerRect = new Rectangle(
                    borderWidth + 4,
                    borderWidth + 4,
                    this.ClientSize.Width - (borderWidth + 4) * 2,
                    this.ClientSize.Height - (borderWidth + 4) * 2
                );
                g.DrawRectangle(accentPen, innerRect);
            }

            using (Pen whitePen = new Pen(Color.FromArgb(200, 255, 255, 255), 1))
            {
                Rectangle whiteRect = new Rectangle(
                    borderWidth + 7,
                    borderWidth + 7,
                    this.ClientSize.Width - (borderWidth + 7) * 2,
                    this.ClientSize.Height - (borderWidth + 7) * 2
                );
                g.DrawRectangle(whitePen, whiteRect);
            }
        }

        // Create logo  (curved text improved)
        private Image CreateUltraDetailedMunicipalityLogo()
        {
            Bitmap logo = new Bitmap(160, 160);
            using (Graphics g = Graphics.FromImage(logo))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.Clear(Color.Transparent);

                // Outer shadow
                using (PathGradientBrush shadowBrush = new PathGradientBrush(
                    new Point[] {
                        new Point(80, 0), new Point(160, 80),
                        new Point(80, 160), new Point(0, 80)
                    }))
                {
                    shadowBrush.CenterColor = Color.Transparent;
                    shadowBrush.SurroundColors = new Color[] {
                        Color.FromArgb(60, 0, 0, 0),
                        Color.FromArgb(60, 0, 0, 0),
                        Color.FromArgb(60, 0, 0, 0),
                        Color.FromArgb(60, 0, 0, 0)
                    };
                    g.FillEllipse(shadowBrush, 2, 2, 156, 156);
                }

                // Outer ring
                using (LinearGradientBrush outerRingBrush = new LinearGradientBrush(
                    new Rectangle(0, 0, 160, 160),
                    Color.FromArgb(41, 128, 185),
                    Color.FromArgb(52, 152, 219),
                    45f))
                {
                    g.FillEllipse(outerRingBrush, 0, 0, 160, 160);
                }

                // Shine effect
                using (LinearGradientBrush shineBrush = new LinearGradientBrush(
                    new Rectangle(0, 0, 160, 80),
                    Color.FromArgb(100, 255, 255, 255),
                    Color.FromArgb(0, 255, 255, 255),
                    LinearGradientMode.Vertical))
                {
                    g.FillEllipse(shineBrush, 0, 0, 160, 80);
                }

                // White middle ring
                using (LinearGradientBrush whiteRingBrush = new LinearGradientBrush(
                    new Rectangle(12, 12, 136, 136),
                    Color.White,
                    Color.FromArgb(245, 245, 245),
                    LinearGradientMode.Vertical))
                {
                    g.FillEllipse(whiteRingBrush, 12, 12, 136, 136);
                }

                // SA Flag segments
                DrawEnhancedSAFlagSegments(g, 18, 18, 124, 124);

                // Central white circle
                using (LinearGradientBrush centralBrush = new LinearGradientBrush(
                    new Rectangle(35, 35, 90, 90),
                    Color.White,
                    Color.FromArgb(250, 250, 250),
                    LinearGradientMode.Vertical))
                {
                    g.FillEllipse(centralBrush, 35, 35, 90, 90);
                }

                // Inner shadow
                using (PathGradientBrush innerShadow = new PathGradientBrush(
                    new Point[] {
                        new Point(80, 35), new Point(125, 80),
                        new Point(80, 125), new Point(35, 80)
                    }))
                {
                    innerShadow.CenterColor = Color.Transparent;
                    innerShadow.SurroundColors = new Color[] {
                        Color.FromArgb(30, 0, 0, 0),
                        Color.FromArgb(30, 0, 0, 0),
                        Color.FromArgb(30, 0, 0, 0),
                        Color.FromArgb(30, 0, 0, 0)
                    };
                    g.FillEllipse(innerShadow, 35, 35, 90, 90);
                }

                // City hall icon
                DrawUltraDetailedCityHallIcon(g);

                // Stars
                DrawPremiumDecorativeStars(g);

                // Curved text (larger radius + explicit spacing + slightly smaller font inside method)
                DrawEnhancedCurvedText(g, "JOHANNESBURG", 80, 80, 74, -90, 180, 2.5f);
                DrawEnhancedCurvedText(g, "MUNICIPALITY", 80, 80, 74, 90, 0, 2.5f);

                // Year
                using (Font yearFont = new Font("Georgia", 7, FontStyle.Bold | FontStyle.Italic))
                {
                    using (StringFormat sf = new StringFormat())
                    {
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center;

                        g.DrawString("EST. 2000", yearFont,
                            new SolidBrush(Color.FromArgb(100, 0, 0, 0)),
                            new RectangleF(36, 108, 90, 15), sf);

                        g.DrawString("EST. 2000", yearFont,
                            new SolidBrush(Color.FromArgb(100, 100, 100)),
                            new RectangleF(35, 107, 90, 15), sf);
                    }
                }

                // Borders
                using (Pen shadowPen = new Pen(Color.FromArgb(80, 0, 0, 0), 5))
                {
                    g.DrawEllipse(shadowPen, 3, 3, 154, 154);
                }

                using (Pen borderPen = new Pen(Color.FromArgb(41, 128, 185), 4))
                {
                    g.DrawEllipse(borderPen, 4, 4, 152, 152);
                }

                using (Pen goldPen = new Pen(Color.FromArgb(255, 184, 28), 2))
                {
                    g.DrawEllipse(goldPen, 14, 14, 132, 132);
                }

                using (Pen whitePen = new Pen(Color.White, 1))
                {
                    g.DrawEllipse(whitePen, 17, 17, 126, 126);
                }
            }

            return logo;
        }

        private void DrawEnhancedSAFlagSegments(Graphics g, int x, int y, int width, int height)
        {
            Color[] saColors = new Color[]
            {
                Color.FromArgb(0, 119, 73),
                Color.FromArgb(255, 184, 28),
                Color.FromArgb(222, 56, 49),
                Color.FromArgb(0, 22, 137),
            };

            float startAngle = -45;
            float sweepAngle = 360f / saColors.Length;

            for (int i = 0; i < saColors.Length; i++)
            {
                using (LinearGradientBrush gradBrush = new LinearGradientBrush(
                    new Rectangle(x, y, width, height),
                    saColors[i],
                    Color.FromArgb(Math.Max(0, saColors[i].R - 40),
                                   Math.Max(0, saColors[i].G - 40),
                                   Math.Max(0, saColors[i].B - 40)),
                    startAngle + (i * sweepAngle)))
                {
                    g.FillPie(gradBrush, x, y, width, height,
                        startAngle + (i * sweepAngle), sweepAngle);
                }
            }
        }

        private void DrawUltraDetailedCityHallIcon(Graphics g)
        {
            Color buildingColor = Color.FromArgb(52, 73, 94);
            Color roofColor = Color.FromArgb(231, 76, 60);
            Color accentColor = Color.FromArgb(255, 184, 28);

            using (SolidBrush buildingBrush = new SolidBrush(buildingColor))
            using (SolidBrush roofBrush = new SolidBrush(roofColor))
            using (SolidBrush accentBrush = new SolidBrush(accentColor))
            {
                // Foundation
                using (LinearGradientBrush baseBrush = new LinearGradientBrush(
                    new Rectangle(58, 84, 44, 4),
                    Color.FromArgb(80, 80, 80),
                    Color.FromArgb(60, 60, 60),
                    LinearGradientMode.Vertical))
                {
                    g.FillRectangle(baseBrush, 58, 84, 44, 4);
                }

                // Building body
                using (LinearGradientBrush bodyBrush = new LinearGradientBrush(
                    new Rectangle(60, 62, 40, 22),
                    buildingColor,
                    Color.FromArgb(70, 90, 110),
                    LinearGradientMode.Vertical))
                {
                    g.FillRectangle(bodyBrush, 60, 62, 40, 22);
                }

                // Columns
                for (int i = 0; i < 5; i++)
                {
                    int columnX = 62 + (i * 9);

                    g.FillRectangle(new SolidBrush(Color.FromArgb(50, 0, 0, 0)),
                        columnX + 1, 63, 3, 21);

                    using (LinearGradientBrush columnBrush = new LinearGradientBrush(
                        new Rectangle(columnX, 62, 3, 22),
                        Color.White,
                        Color.FromArgb(220, 220, 220),
                        LinearGradientMode.Horizontal))
                    {
                        g.FillRectangle(columnBrush, columnX, 62, 3, 22);
                    }

                    g.FillRectangle(accentBrush, columnX - 1, 61, 5, 2);
                    g.FillRectangle(accentBrush, columnX - 1, 83, 5, 2);
                }

                // Windows
                int[,] windows = new int[,] {
                    {64, 65}, {73, 65}, {82, 65},
                    {64, 73}, {73, 73}, {82, 73}
                };

                for (int i = 0; i < windows.GetLength(0); i++)
                {
                    int wx = windows[i, 0];
                    int wy = windows[i, 1];

                    g.FillRectangle(accentBrush, wx - 1, wy - 1, 6, 6);

                    using (LinearGradientBrush glassBrush = new LinearGradientBrush(
                        new Rectangle(wx, wy, 4, 4),
                        Color.FromArgb(173, 216, 230),
                        Color.FromArgb(135, 206, 250),
                        LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(glassBrush, wx, wy, 4, 4);
                    }

                    g.FillRectangle(new SolidBrush(Color.FromArgb(100, 255, 255, 255)),
                        wx, wy, 2, 1);
                }

                // Door
                using (LinearGradientBrush doorBrush = new LinearGradientBrush(
                    new Rectangle(72, 78, 6, 10),
                    accentColor,
                    Color.FromArgb(200, 150, 0),
                    LinearGradientMode.Vertical))
                {
                    g.FillRectangle(doorBrush, 72, 78, 6, 10);
                }

                g.DrawRectangle(new Pen(Color.FromArgb(150, 100, 0), 1), 73, 79, 2, 3);
                g.DrawRectangle(new Pen(Color.FromArgb(150, 100, 0), 1), 75, 79, 2, 3);
                g.DrawRectangle(new Pen(Color.FromArgb(150, 100, 0), 1), 73, 83, 2, 3);
                g.DrawRectangle(new Pen(Color.FromArgb(150, 100, 0), 1), 75, 83, 2, 3);

                g.FillEllipse(Brushes.Black, 73, 81, 1, 1);
                g.FillEllipse(Brushes.Black, 76, 81, 1, 1);

                // Roof
                Point[] roofPoints = new Point[]
                {
                    new Point(80, 52),
                    new Point(57, 61),
                    new Point(103, 61)
                };

                Point[] shadowPoints = new Point[]
                {
                    new Point(81, 53),
                    new Point(58, 62),
                    new Point(104, 62)
                };
                g.FillPolygon(new SolidBrush(Color.FromArgb(80, 0, 0, 0)), shadowPoints);

                using (LinearGradientBrush roofGradBrush = new LinearGradientBrush(
                    new Rectangle(57, 52, 46, 10),
                    roofColor,
                    Color.FromArgb(180, 50, 40),
                    LinearGradientMode.Vertical))
                {
                    g.FillPolygon(roofGradBrush, roofPoints);
                }

                Point[] highlightPoints = new Point[]
                {
                    new Point(80, 53),
                    new Point(70, 58),
                    new Point(90, 58)
                };
                g.FillPolygon(new SolidBrush(Color.FromArgb(50, 255, 255, 255)), highlightPoints);

                g.DrawPolygon(new Pen(Color.FromArgb(180, 50, 40), 2), roofPoints);

                g.DrawLine(new Pen(accentColor, 2), 60, 60, 100, 60);

                // Tower and dome
                g.FillRectangle(accentBrush, 76, 48, 8, 4);

                using (LinearGradientBrush towerBrush = new LinearGradientBrush(
                    new Rectangle(77, 44, 6, 4),
                    accentColor,
                    Color.FromArgb(200, 150, 0),
                    LinearGradientMode.Vertical))
                {
                    g.FillRectangle(towerBrush, 77, 44, 6, 4);
                }

                using (LinearGradientBrush domeBrush = new LinearGradientBrush(
                    new Rectangle(75, 40, 10, 8),
                    Color.FromArgb(255, 215, 0),
                    accentColor,
                    LinearGradientMode.Vertical))
                {
                    g.FillEllipse(domeBrush, 75, 40, 10, 8);
                }

                g.FillEllipse(new SolidBrush(Color.FromArgb(100, 255, 255, 255)), 76, 41, 8, 3);

                g.DrawLine(new Pen(Color.FromArgb(100, 100, 100), 1), 80, 40, 80, 36);

                Point[] flagPoints = new Point[]
                {
                    new Point(80, 36),
                    new Point(85, 38),
                    new Point(80, 40)
                };
                g.FillPolygon(new SolidBrush(Color.FromArgb(0, 119, 73)), flagPoints);
            }
        }

        private void DrawPremiumDecorativeStars(Graphics g)
        {
            Color starColor = Color.FromArgb(255, 184, 28);

            DrawGlowingStar(g, 48, 80, 5, starColor);
            DrawGlowingStar(g, 112, 80, 5, starColor);
            DrawGlowingStar(g, 80, 30, 3, Color.FromArgb(255, 215, 0));
        }

        private void DrawGlowingStar(Graphics g, float centerX, float centerY, float radius, Color color)
        {
            for (int i = 3; i > 0; i--)
            {
                Color glowColor = Color.FromArgb(30 * i, color.R, color.G, color.B);
                DrawStarShape(g, centerX, centerY, radius + i, glowColor, true);
            }

            DrawStarShape(g, centerX, centerY, radius, color, false);
            DrawStarShape(g, centerX - 0.5f, centerY - 0.5f, radius * 0.6f,
                Color.FromArgb(150, 255, 255, 255), false);
        }

        private void DrawStarShape(Graphics g, float centerX, float centerY, float radius, Color color, bool isGlow)
        {
            PointF[] points = new PointF[10];
            float angle = -90;

            for (int i = 0; i < 10; i++)
            {
                float r = (i % 2 == 0) ? radius : radius / 2.5f;
                float angleRad = angle * (float)Math.PI / 180;
                points[i] = new PointF(
                    centerX + r * (float)Math.Cos(angleRad),
                    centerY + r * (float)Math.Sin(angleRad)
                );
                angle += 36;
            }

            using (SolidBrush brush = new SolidBrush(color))
            {
                g.FillPolygon(brush, points);
            }

            if (!isGlow)
            {
                using (Pen pen = new Pen(Color.FromArgb(200, 150, 0), 1))
                {
                    g.DrawPolygon(pen, points);
                }
            }
        }

        // NEW: extra char spacing + larger radius + slightly smaller font to avoid overlap
        private void DrawEnhancedCurvedText(Graphics g, string text, float centerX, float centerY,
            float radius, float startAngle, float endAngle, float charSpacingDegrees = 0f)
        {
            using (Font font = new Font("Arial", 8, FontStyle.Bold)) // 9 -> 8
            {
                float sweep = endAngle - startAngle;
                float angleStep = sweep / Math.Max(1, text.Length);
                float currentAngle = startAngle;

                // small outward offset so glyphs don't collide with ring
                float radialNudge = 1.5f;

                for (int i = 0; i < text.Length; i++)
                {
                    float angleRad = currentAngle * (float)Math.PI / 180f;
                    float x = centerX + (radius + radialNudge) * (float)Math.Cos(angleRad);
                    float y = centerY + (radius + radialNudge) * (float)Math.Sin(angleRad);

                    g.TranslateTransform(x, y);
                    g.RotateTransform(currentAngle + 90);

                    // soft drop shadow
                    using (SolidBrush shadow = new SolidBrush(Color.FromArgb(110, 0, 0, 0)))
                        g.DrawString(text[i].ToString(), font, shadow, 1, 1);

                    // gradient fill
                    using (LinearGradientBrush textBrush = new LinearGradientBrush(
                        new RectangleF(-6, -6, 12, 12),
                        Color.FromArgb(41, 128, 185),
                        Color.FromArgb(25, 90, 130),
                        LinearGradientMode.Vertical))
                    {
                        g.DrawString(text[i].ToString(), font, textBrush, 0, 0);
                    }

                    g.ResetTransform();
                    currentAngle += angleStep + charSpacingDegrees; // add spacing
                }
            }
        }

        private void SetBackgroundImage()
        {
            try
            {
                Bitmap background = new Bitmap(this.Width, this.Height);
                using (Graphics g = Graphics.FromImage(background))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;

                    // Gradient background
                    using (LinearGradientBrush gradientBrush = new LinearGradientBrush(
                        new Rectangle(0, 0, this.Width, this.Height),
                        Color.FromArgb(245, 250, 255),
                        Color.FromArgb(230, 242, 250),
                        LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(gradientBrush, 0, 0, this.Width, this.Height);
                    }

                    // Subtle pattern
                    using (Pen patternPen = new Pen(Color.FromArgb(8, 100, 150, 200), 1))
                    {
                        for (int i = -this.Height; i < this.Width + this.Height; i += 40)
                        {
                            g.DrawLine(patternPen, i, 0, i + this.Height, this.Height);
                        }

                        for (int i = 0; i < this.Height; i += 40)
                        {
                            g.DrawLine(new Pen(Color.FromArgb(5, 100, 150, 200), 1), 0, i, this.Width, i);
                        }
                    }

                    // Decorative corners
                    using (SolidBrush greenBrush = new SolidBrush(Color.FromArgb(15, 0, 119, 73)))
                    using (SolidBrush goldBrush = new SolidBrush(Color.FromArgb(15, 255, 184, 28)))
                    using (SolidBrush redBrush = new SolidBrush(Color.FromArgb(15, 222, 56, 49)))
                    using (SolidBrush blueBrush = new SolidBrush(Color.FromArgb(15, 0, 22, 137)))
                    {
                        g.FillEllipse(greenBrush, -70, -70, 200, 200);
                        g.FillEllipse(new SolidBrush(Color.FromArgb(20, 255, 255, 255)), -60, -60, 150, 150);

                        g.FillEllipse(goldBrush, this.Width - 130, -50, 180, 180);
                        g.FillEllipse(new SolidBrush(Color.FromArgb(20, 255, 255, 255)), this.Width - 120, -40, 140, 140);

                        g.FillEllipse(redBrush, -50, this.Height - 160, 190, 190);
                        g.FillEllipse(new SolidBrush(Color.FromArgb(20, 255, 255, 255)), -40, this.Height - 150, 150, 150);

                        g.FillEllipse(blueBrush, this.Width - 150, this.Height - 150, 200, 200);
                        g.FillEllipse(new SolidBrush(Color.FromArgb(20, 255, 255, 255)), this.Width - 140, this.Height - 140, 160, 160);
                    }

                    // Radial overlay
                    using (PathGradientBrush radialBrush = new PathGradientBrush(
                        new Point[] {
                            new Point(this.Width / 2, 0),
                            new Point(this.Width, this.Height / 2),
                            new Point(this.Width / 2, this.Height),
                            new Point(0, this.Height / 2)
                        }))
                    {
                        radialBrush.CenterColor = Color.FromArgb(20, 255, 255, 255);
                        radialBrush.SurroundColors = new Color[] {
                            Color.Transparent, Color.Transparent, Color.Transparent, Color.Transparent
                        };
                        g.FillRectangle(radialBrush, 0, 0, this.Width, this.Height);
                    }
                }

                this.BackgroundImage = background;
                this.BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch (Exception ex)
            {
                this.BackColor = Color.FromArgb(245, 250, 255);
                System.Diagnostics.Debug.WriteLine($"Background image creation failed: {ex.Message}");
            }
        }

        private void SetupNotificationTimer()
        {
            notificationTimer = new Timer();
            notificationTimer.Interval = 10000;
            notificationTimer.Tick += NotificationTimer_Tick;
            notificationTimer.Start();
        }

        private void NotificationTimer_Tick(object sender, EventArgs e)
        {
            if (IssueManager.GetTotalIssueCount() > 0)
            {
                Random random = new Random();
                if (random.Next(1, 100) <= 15)
                {
                    NotificationService.SimulateStatusUpdates();
                }
            }
        }

        private void ReportIssuesBtn_Click(object sender, EventArgs e)
        {
            var reportForm = new ReportIssuesForm();
            reportForm.FormClosed += (s, args) => this.Show();
            reportForm.Show(this);
            this.Hide();
        }

        private void LocalEventsBtn_Click(object sender, EventArgs e)
        {
            var localEventsForm = new LocalEventsForm();
            localEventsForm.FormClosed += (s, args) => this.Show();
            localEventsForm.Show(this);
            this.Hide();
        }

        // NEW: Service Request Status button click handler for Part 3
        private void ServiceStatusBtn_Click(object sender, EventArgs e)
        {
            var serviceStatusForm = new ServiceRequestStatusForm();
            serviceStatusForm.FormClosed += (s, args) => this.Show();
            serviceStatusForm.Show(this);
            this.Hide();
        }

        private void CheckNotificationsBtn_Click(object sender, EventArgs e)
        {
            var notifications = NotificationService.GetNotificationHistory();
            if (notifications.Count > 0)
            {
                var lastFiveNewestFirst = notifications
                    .Skip(Math.Max(0, notifications.Count - 5))
                    .Reverse();

                string notificationText = "Recent Notifications:\n\n" +
                    string.Join("\n", lastFiveNewestFirst);

                MessageBox.Show(notificationText, "Notification History",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No notifications yet.\n\nNotifications will appear here when:\n" +
                    "• Issues are submitted\n" +
                    "• Status updates occur\n" +
                    "• Important announcements are made",
                    "No Notifications", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (notificationTimer != null)
            {
                notificationTimer.Stop();
                notificationTimer.Dispose();
            }

            if (this.BackgroundImage != null)
            {
                this.BackgroundImage.Dispose();
            }

            if (logoBox?.Image != null)
            {
                logoBox.Image.Dispose();
            }

            if (e.CloseReason == CloseReason.UserClosing)
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