using System;

namespace MunicipalServiceAppPart1
{

    public class ServiceRequest
    {
        /// Represents a municipal service request submitted by a citizen

        public string RequestId { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public DateTime DateSubmitted { get; set; }
        public int Priority { get; set; }
        public string AttachedMedia { get; set; }

        public ServiceRequest(string requestId, string description, string category,
                            string location, DateTime dateSubmitted, int priority = 3)
        {
            RequestId = requestId;
            Description = description;
            Category = category;
            Location = location;
            DateSubmitted = dateSubmitted;
            Priority = priority;
            Status = "Pending";
            AttachedMedia = "";
        }

        /// Returns a string representation of the service request

        public override string ToString()
        {
            return $"ID: {RequestId} | {Category} | {Location} | Status: {Status} | Priority: {Priority}";
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