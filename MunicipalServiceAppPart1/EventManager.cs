using System;
using System.Collections.Generic;
using System.Linq;

namespace MunicipalServiceAppPart1
{
    public static class EventManager
    {
        // ADVANCED DATA STRUCTURES for Part 2:

        // Dictionary: Fast O(1) lookup by event ID
        private static Dictionary<int, Event> eventDictionary = new Dictionary<int, Event>();

        // Sorted Dictionary: Events organized by date for efficient chronological access O(log n)
        private static SortedDictionary<DateTime, List<Event>> eventsByDate = new SortedDictionary<DateTime, List<Event>>();

        // HashSet: Unique categories for efficient category management O(1)
        private static HashSet<string> categories = new HashSet<string>();

        // Queue: Recent searches for recommendation feature (FIFO)
        private static Queue<string> recentSearches = new Queue<string>();

        // Dictionary: Search pattern tracking for smart recommendations
        private static Dictionary<string, int> searchPatterns = new Dictionary<string, int>();

        // Stack: For undo functionality (bonus feature)
        private static Stack<Event> deletedEvents = new Stack<Event>();

        private static int nextId = 1;

        static EventManager()
        {
            InitializeSampleEvents();
        }

        private static void InitializeSampleEvents()
        {
            // Sample events with various priorities and categories (15+ events)
            AddEvent(new Event(
                "Community Clean-Up Drive",
                "Community Service",
                "Join us for a community clean-up initiative in Johannesburg CBD. Bring gloves and bags. Refreshments provided!",
                DateTime.Now.AddDays(5),
                "Johannesburg CBD",
                "City Council",
                2
            ));

            AddEvent(new Event(
                "Municipal Budget Meeting",
                "Government",
                "Public meeting to discuss the municipal budget for the upcoming fiscal year. All residents welcome to attend.",
                DateTime.Now.AddDays(10),
                "City Hall, Pretoria",
                "Municipal Finance Department",
                1
            ));

            AddEvent(new Event(
                "Water Conservation Workshop",
                "Education",
                "Learn about water conservation techniques and sustainable practices for your home and community.",
                DateTime.Now.AddDays(7),
                "Community Center, Cape Town",
                "Department of Water Affairs",
                2
            ));

            AddEvent(new Event(
                "Road Maintenance Notification",
                "Infrastructure",
                "Scheduled road maintenance on Main Street from 8 AM to 5 PM. Please expect delays and use alternative routes.",
                DateTime.Now.AddDays(2),
                "Main Street, Johannesburg",
                "Road Maintenance Department",
                1
            ));

            AddEvent(new Event(
                "Local Music Festival",
                "Entertainment",
                "Annual music festival featuring local artists, food vendors, and family activities. Free entry!",
                DateTime.Now.AddDays(15),
                "Park Stadium, Durban",
                "Arts and Culture Department",
                3
            ));

            AddEvent(new Event(
                "Electricity Load Shedding Schedule",
                "Utilities",
                "Important update on load shedding schedule for the week. Stage 2 expected during peak hours.",
                DateTime.Now.AddDays(1),
                "Citywide",
                "Electricity Department",
                1
            ));

            AddEvent(new Event(
                "Youth Development Program Launch",
                "Education",
                "Skills development program for youth aged 18-25. Learn coding, business skills, and entrepreneurship.",
                DateTime.Now.AddDays(12),
                "Community Center, Soweto",
                "Youth Development Office",
                2
            ));

            AddEvent(new Event(
                "Public Safety Awareness Campaign",
                "Public Safety",
                "Community safety awareness and crime prevention workshop. Learn how to keep your neighborhood safe.",
                DateTime.Now.AddDays(8),
                "Police Station Hall, Sandton",
                "South African Police Service",
                2
            ));

            AddEvent(new Event(
                "Heritage Day Celebration",
                "Cultural",
                "Celebrate South Africa's diverse heritage with cultural performances, traditional food, and exhibitions.",
                DateTime.Now.AddDays(20),
                "Freedom Park, Pretoria",
                "Heritage Department",
                3
            ));

            AddEvent(new Event(
                "Small Business Support Seminar",
                "Economic Development",
                "Support and resources for small business owners and entrepreneurs. Grants and funding information available.",
                DateTime.Now.AddDays(14),
                "Business Hub, Johannesburg",
                "Economic Development Department",
                2
            ));

            AddEvent(new Event(
                "Vaccination Drive",
                "Health",
                "Free vaccination drive for children and adults. Bring your vaccination card and ID document.",
                DateTime.Now.AddDays(4),
                "Health Clinic, Midrand",
                "Department of Health",
                1
            ));

            AddEvent(new Event(
                "Recycling Awareness Workshop",
                "Environmental",
                "Learn about proper waste management and recycling. Receive free recycling bins!",
                DateTime.Now.AddDays(9),
                "Environmental Center, Centurion",
                "Environmental Services",
                2
            ));

            // NEW EVENTS (13-18) to meet 15+ requirement
            AddEvent(new Event(
                "Job Fair 2025",
                "Employment",
                "Connect with local employers. Bring your CV and dress professionally. Over 50 companies attending!",
                DateTime.Now.AddDays(18),
                "Convention Center, Johannesburg",
                "Department of Labour",
                2
            ));

            AddEvent(new Event(
                "Free Legal Advice Clinic",
                "Legal Services",
                "Free legal consultation for residents. Family law, property disputes, and consumer rights covered.",
                DateTime.Now.AddDays(6),
                "Municipal Offices, Pretoria",
                "Legal Aid South Africa",
                2
            ));

            AddEvent(new Event(
                "Sports Day for Youth",
                "Sports",
                "Annual youth sports day with soccer, netball, athletics, and more. Prizes for winners!",
                DateTime.Now.AddDays(11),
                "Municipal Sports Complex, Durban",
                "Sports and Recreation Department",
                3
            ));

            AddEvent(new Event(
                "Pensioners' Information Session",
                "Social Services",
                "Important information about pension grants, healthcare benefits, and social services for senior citizens.",
                DateTime.Now.AddDays(13),
                "Community Hall, Cape Town",
                "Department of Social Development",
                2
            ));

            AddEvent(new Event(
                "Pothole Repair Schedule",
                "Infrastructure",
                "Major pothole repair project on N1 highway. Expect delays between 9 AM - 4 PM daily for 2 weeks.",
                DateTime.Now.AddDays(3),
                "N1 Highway, Johannesburg North",
                "Road Maintenance Department",
                1
            ));

            AddEvent(new Event(
                "Community Garden Workshop",
                "Environmental",
                "Start your own community garden! Learn about sustainable urban farming and get free seedlings.",
                DateTime.Now.AddDays(16),
                "Botanical Gardens, Pretoria",
                "Department of Agriculture",
                3
            ));
        }

        public static void AddEvent(Event evt)
        {
            if (evt != null)
            {
                evt.Id = nextId++;

                // Add to dictionary for fast lookup
                eventDictionary[evt.Id] = evt;

                // Add to sorted dictionary by date
                DateTime dateKey = evt.EventDate.Date;
                if (!eventsByDate.ContainsKey(dateKey))
                {
                    eventsByDate[dateKey] = new List<Event>();
                }
                eventsByDate[dateKey].Add(evt);

                // Add category to set
                categories.Add(evt.Category);
            }
        }

        public static List<Event> GetAllEvents()
        {
            return eventDictionary.Values.OrderBy(e => e.EventDate).ToList();
        }

        public static List<Event> GetUpcomingEvents(int count = 10)
        {
            DateTime now = DateTime.Now;
            return eventDictionary.Values
                .Where(e => e.EventDate >= now)
                .OrderBy(e => e.EventDate)
                .Take(count)
                .ToList();
        }

        public static List<Event> SearchEventsByCategory(string category)
        {
            TrackSearch(category);
            return eventDictionary.Values
                .Where(e => e.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                .OrderBy(e => e.EventDate)
                .ToList();
        }

        public static List<Event> SearchEventsByDate(DateTime date)
        {
            TrackSearch(date.ToString("yyyy-MM-dd"));
            DateTime dateKey = date.Date;

            if (eventsByDate.ContainsKey(dateKey))
            {
                return eventsByDate[dateKey].OrderBy(e => e.EventDate).ToList();
            }

            return new List<Event>();
        }

        public static List<Event> SearchEventsByDateRange(DateTime startDate, DateTime endDate)
        {
            TrackSearch($"{startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}");

            return eventDictionary.Values
                .Where(e => e.EventDate.Date >= startDate.Date && e.EventDate.Date <= endDate.Date)
                .OrderBy(e => e.EventDate)
                .ToList();
        }

        public static List<Event> SearchEventsByKeyword(string keyword)
        {
            TrackSearch(keyword);

            if (string.IsNullOrWhiteSpace(keyword))
                return GetAllEvents();

            keyword = keyword.ToLower();
            return eventDictionary.Values
                .Where(e => e.Title.ToLower().Contains(keyword) ||
                           e.Description.ToLower().Contains(keyword) ||
                           e.Location.ToLower().Contains(keyword) ||
                           e.Category.ToLower().Contains(keyword))
                .OrderBy(e => e.EventDate)
                .ToList();
        }

        public static List<Event> GetPriorityEvents()
        {
            return eventDictionary.Values
                .Where(e => e.Priority <= 2 && e.EventDate >= DateTime.Now)
                .OrderBy(e => e.Priority)
                .ThenBy(e => e.EventDate)
                .ToList();
        }

        public static HashSet<string> GetAllCategories()
        {
            return new HashSet<string>(categories);
        }

        public static Event GetEventById(int id)
        {
            return eventDictionary.ContainsKey(id) ? eventDictionary[id] : null;
        }

        // RECOMMENDATION FEATURE - Uses search patterns and algorithms
        private static void TrackSearch(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return;

            searchTerm = searchTerm.ToLower();

            // Queue: Add to recent searches (FIFO)
            recentSearches.Enqueue(searchTerm);
            if (recentSearches.Count > 20)
            {
                recentSearches.Dequeue();
            }

            // Dictionary: Track search frequency
            if (searchPatterns.ContainsKey(searchTerm))
            {
                searchPatterns[searchTerm]++;
            }
            else
            {
                searchPatterns[searchTerm] = 1;
            }
        }

        // SMART RECOMMENDATION ALGORITHM
        public static List<Event> GetRecommendedEvents()
        {
            if (searchPatterns.Count == 0)
            {
                // No search history: return priority events
                return GetPriorityEvents().Take(5).ToList();
            }

            // Get top 3 most frequently searched terms
            var topSearchTerms = searchPatterns
                .OrderByDescending(kvp => kvp.Value)
                .Take(3)
                .Select(kvp => kvp.Key)
                .ToList();

            HashSet<Event> recommendedEvents = new HashSet<Event>();

            // Find events matching top search patterns
            foreach (var searchTerm in topSearchTerms)
            {
                var matchingEvents = eventDictionary.Values
                    .Where(e => e.EventDate >= DateTime.Now &&
                               (e.Category.ToLower().Contains(searchTerm) ||
                                e.Title.ToLower().Contains(searchTerm) ||
                                e.Description.ToLower().Contains(searchTerm)))
                    .Take(3);

                foreach (var evt in matchingEvents)
                {
                    recommendedEvents.Add(evt);
                }
            }

            // If we have fewer than 5 recommendations, add priority events
            if (recommendedEvents.Count < 5)
            {
                var priorityEvents = GetPriorityEvents();
                foreach (var evt in priorityEvents)
                {
                    if (recommendedEvents.Count >= 5) break;
                    recommendedEvents.Add(evt);
                }
            }

            return recommendedEvents
                .OrderBy(e => e.Priority)
                .ThenBy(e => e.EventDate)
                .Take(5)
                .ToList();
        }

        public static Dictionary<string, int> GetSearchStatistics()
        {
            return new Dictionary<string, int>(searchPatterns);
        }

        public static int GetTotalEventCount()
        {
            return eventDictionary.Count;
        }

        public static int GetUpcomingEventCount()
        {
            return eventDictionary.Values.Count(e => e.EventDate >= DateTime.Now);
        }

        // Bonus: Undo feature using Stack
        public static bool DeleteEvent(int id)
        {
            if (eventDictionary.ContainsKey(id))
            {
                Event evt = eventDictionary[id];
                deletedEvents.Push(evt);

                eventDictionary.Remove(id);

                DateTime dateKey = evt.EventDate.Date;
                if (eventsByDate.ContainsKey(dateKey))
                {
                    eventsByDate[dateKey].Remove(evt);
                }

                return true;
            }
            return false;
        }

        public static Event UndoDelete()
        {
            if (deletedEvents.Count > 0)
            {
                Event evt = deletedEvents.Pop();
                AddEvent(evt);
                return evt;
            }
            return null;
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
