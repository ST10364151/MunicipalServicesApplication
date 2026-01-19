using System;
using System.Collections.Generic;
using System.Linq;

namespace MunicipalServiceAppPart1
{
    public static class IssueManager
    {
        private static List<Issue> reportedIssues = new List<Issue>();
        private static int nextId = 1;

        public static List<Issue> GetAllIssues()
        {
            return reportedIssues.ToList(); // Return a copy to prevent external modification
        }

        public static void AddIssue(Issue issue)
        {
            if (issue != null)
            {
                issue.Id = nextId++;
                reportedIssues.Add(issue);

                // Send immediate confirmation notification
                NotificationService.ShowIssueSubmissionConfirmation(issue);

                // Simulate automatic status progression for demonstration
                SimulateStatusProgression(issue);
            }
        }

        public static Issue GetIssueById(int id)
        {
            return reportedIssues.FirstOrDefault(i => i.Id == id);
        }

        public static List<Issue> GetIssuesByCategory(string category)
        {
            return reportedIssues.Where(i =>
                i.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public static List<Issue> GetIssuesByLocation(string location)
        {
            return reportedIssues.Where(i =>
                i.Location.ToLower().Contains(location.ToLower())).ToList();
        }

        public static List<Issue> GetIssuesByStatus(string status)
        {
            return reportedIssues.Where(i =>
                i.Status.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public static int GetTotalIssueCount()
        {
            return reportedIssues.Count;
        }

        public static List<string> GetAllCategories()
        {
            return reportedIssues.Select(i => i.Category).Distinct().ToList();
        }

        public static List<string> GetAllStatuses()
        {
            return reportedIssues.Select(i => i.Status).Distinct().ToList();
        }

        public static void ClearAllIssues()
        {
            reportedIssues.Clear();
            nextId = 1;
        }

        // Enhanced method to get issues with status counts
        public static Dictionary<string, int> GetStatusSummary()
        {
            return reportedIssues.GroupBy(i => i.Status)
                                .ToDictionary(g => g.Key, g => g.Count());
        }

        // Method to get recent issues (last 24 hours)
        public static List<Issue> GetRecentIssues()
        {
            DateTime yesterday = DateTime.Now.AddDays(-1);
            return reportedIssues.Where(i => i.SubmissionDate >= yesterday)
                                .OrderByDescending(i => i.SubmissionDate)
                                .ToList();
        }

        // Simulate status progression for demonstration purposes
        private static async void SimulateStatusProgression(Issue issue)
        {
            // This would normally be handled by backend processes
            // For demonstration, we'll simulate some status changes

            // Wait 2 seconds, then mark as "Under Review"
            await System.Threading.Tasks.Task.Delay(2000);
            if (issue.Status == "Submitted")
            {
                issue.UpdateStatus("Under Review");
            }

            // Wait another 3 seconds, then mark as "In Progress" 
            await System.Threading.Tasks.Task.Delay(3000);
            if (issue.Status == "Under Review")
            {
                issue.UpdateStatus("In Progress");
            }
        }

        // Method to manually update issue status (for testing/admin purposes)
        public static bool UpdateIssueStatus(int issueId, string newStatus)
        {
            var issue = GetIssueById(issueId);
            if (issue != null)
            {
                issue.UpdateStatus(newStatus);
                return true;
            }
            return false;
        }

        // Get priority issues (based on category)
        public static List<Issue> GetPriorityIssues()
        {
            string[] priorityCategories = { "Electricity", "Water and Utilities", "Public Safety" };
            return reportedIssues.Where(i => priorityCategories.Contains(i.Category))
                                .OrderByDescending(i => i.SubmissionDate)
                                .ToList();
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