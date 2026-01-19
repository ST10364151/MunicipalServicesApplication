using System;
using System;
using System.Collections.Generic;

namespace MunicipalServiceAppPart1
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public string Organizer { get; set; }
        public int Priority { get; set; } // For priority queue (1=highest, 5=lowest)

        public Event()
        {
            Priority = 3; // Default priority
        }

        public Event(string title, string category, string description, DateTime eventDate, string location, string organizer, int priority = 3)
        {
            Title = title;
            Category = category;
            Description = description;
            EventDate = eventDate;
            Location = location;
            Organizer = organizer;
            Priority = priority;
        }

        public override string ToString()
        {
            return $"{Title} - {EventDate:MMM dd, yyyy} at {Location}";
        }

        public string GetDetailedInfo()
        {
            return $"Event: {Title}\n" +
                   $"Category: {Category}\n" +
                   $"Date: {EventDate:dddd, MMMM dd, yyyy HH:mm}\n" +
                   $"Location: {Location}\n" +
                   $"Organizer: {Organizer}\n" +
                   $"Description: {Description}";
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