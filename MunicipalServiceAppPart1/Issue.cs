using System;
using System.Collections.Generic;

namespace MunicipalServiceAppPart1
{
    public class Issue
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public List<string> AttachedFiles { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string Status { get; set; }
        public DateTime LastUpdated { get; set; }
        public List<string> StatusHistory { get; set; }

        public Issue()
        {
            AttachedFiles = new List<string>();
            StatusHistory = new List<string>();
            SubmissionDate = DateTime.Now;
            LastUpdated = DateTime.Now;
            Status = "Submitted";
            StatusHistory.Add($"{DateTime.Now:yyyy-MM-dd HH:mm} - Issue Submitted");
        }

        public Issue(string location, string category, string description)
        {
            Location = location;
            Category = category;
            Description = description;
            AttachedFiles = new List<string>();
            StatusHistory = new List<string>();
            SubmissionDate = DateTime.Now;
            LastUpdated = DateTime.Now;
            Status = "Submitted";
            StatusHistory.Add($"{DateTime.Now:yyyy-MM-dd HH:mm} - Issue Submitted");
        }

        public void AddAttachment(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath) && !AttachedFiles.Contains(filePath))
            {
                AttachedFiles.Add(filePath);
            }
        }

        public void UpdateStatus(string newStatus)
        {
            if (Status != newStatus)
            {
                Status = newStatus;
                LastUpdated = DateTime.Now;
                StatusHistory.Add($"{DateTime.Now:yyyy-MM-dd HH:mm} - Status changed to: {newStatus}");

                // Trigger notification
                NotificationService.ShowNotification(
                    "Issue Status Update",
                    $"Issue #{Id} status updated to: {newStatus}"
                );
            }
        }

        public override string ToString()
        {
            return $"Issue #{Id} - {Category} at {Location} (Submitted: {SubmissionDate:yyyy-MM-dd HH:mm}) - Status: {Status}";
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