using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MunicipalServiceAppPart1
{
    public static class NotificationService
    {
        private static List<string> notificationHistory = new List<string>();
        private static Queue<string> pendingNotifications = new Queue<string>();

        public static void ShowNotification(string title, string message)
        {
            try
            {
                // Add to history
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string notification = $"[{timestamp}] {title}: {message}";
                notificationHistory.Add(notification);

                // Show immediate feedback
                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Add to pending notifications for future reference
                pendingNotifications.Enqueue(notification);

                // Keep only last 100 notifications to prevent memory issues
                if (notificationHistory.Count > 100)
                {
                    notificationHistory.RemoveAt(0);
                }
            }
            catch (Exception ex)
            {
                // Fallback notification method
                MessageBox.Show($"Notification: {message}", "System Update",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static void ShowIssueSubmissionConfirmation(Issue issue)
        {
            string message = $"Issue reported successfully!\n\n" +
                           $"Tracking ID: #{issue.Id:D6}\n" +
                           $"Location: {issue.Location}\n" +
                           $"Category: {issue.Category}\n" +
                           $"Submitted: {issue.SubmissionDate:yyyy-MM-dd HH:mm}\n" +
                           $"Status: {issue.Status}\n\n" +
                           $"📱 You will receive notifications about status updates.\n" +
                           $"🏆 Thank you for helping improve your community!\n\n" +
                           $"Total issues reported today: {IssueManager.GetTotalIssueCount()}";

            ShowNotification("Issue Submitted Successfully!", message);
        }

        public static void ShowProgressUpdate(string message)
        {
            ShowNotification("Progress Update", message);
        }

        public static List<string> GetNotificationHistory()
        {
            return notificationHistory.ToList(); // Return copy to prevent external modification
        }

        public static bool HasPendingNotifications()
        {
            return pendingNotifications.Count > 0;
        }

        public static string GetNextPendingNotification()
        {
            return pendingNotifications.Count > 0 ? pendingNotifications.Dequeue() : null;
        }

        public static void ClearHistory()
        {
            notificationHistory.Clear();
            pendingNotifications.Clear();
        }

        // Simulate status updates for demonstration
        public static void SimulateStatusUpdates()
        {
            var issues = IssueManager.GetAllIssues();
            if (issues.Any())
            {
                var random = new Random();
                var issue = issues[random.Next(issues.Count)];

                string[] statuses = { "Under Review", "In Progress", "Resolved", "Requires Additional Information" };
                string newStatus = statuses[random.Next(statuses.Length)];

                if (issue.Status != newStatus && issue.Status != "Resolved")
                {
                    issue.UpdateStatus(newStatus);
                }
            }
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