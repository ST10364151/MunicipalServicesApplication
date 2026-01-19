[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/W-LsnvKp)

ST10364151

Part3 Video Link: https://youtu.be/O0qtgsOMRFg 

Municipal Services Application — Part 1 (PROG7312)


1. ---Introduction---
This application is part of the PROG7312 Portfolio of Evidence.
It is a C# .NET Window-Forms desktop application that allows South African citizens to essentually report municipal issues (e.g., potholes, water leaks, power outages) and receive timely feedback.
The system demonstrates user engagement strategies, also focusing on push notifications and real-time updates to keep citizens informed and motivated.
**Part 2** builds upon Part 1 by adding a comprehensive Local Events and Announcements feature with advanced data structures, intelligent search capabilities, and a smart recommendation system based on user behavior.

The system demonstrates advanced programming concepts including:
- Complex data structures (Dictionary, SortedDictionary, HashSet, Queue, Stack)
- Search and filter algorithms
- Smart recommendation engine
- Professional UI/UX design with South African municipal branding
  --------------------------------------------------------------------------------------

2. Features
2.1 Main Menu
Three buttons:
Report Issues (active in Part 1)
Service Request Status (coming soon in next part)
Local Events and Announcements (coming soonvin next part)
Real-time notifications banner + “View Notifications” button.
Disabled tasks are visually distinct (greyed out).
2.2 Report Issues Form
Inputs:
Location (with placeholder)
Category (predefined list: Roads, Electricity, Water, etc.)
Description (placeholder guidance)
Attachments: Add/remove multiple files with filters (images/docs).
Engagement features:
ProgressBar showing completion percentage.
Motivational messages adapting to completion level.
Estimated response time updates (priority for electricity, water, public safety).
Feedback:
Unique tracking ID generated on submission.
Notifications simulate progress updates (“Under Review”, “In Progress”, etc.).
Notification history accessible via the main menu.
2.3 Notifications
Built-in NotificationService handles:
Issue submission confirmations.
Status change alerts.
Progress updates.
Notifications appear as MessageBoxes, are logged in history, and can be viewed later.


3. Technical Architecture
3.1 Core Classes
Issue
Fields: Id, Location, Category, Description, AttachedFiles, SubmissionDate, Status, LastUpdated, StatusHistory.
Methods: AddAttachment(), UpdateStatus(), ToString().
IssueManager
Handles storage (List<Issue>).
Methods: Add, Get by Id/Category/Status, Get summaries, Simulate status progression, Clear.
NotificationService
Handles notifications, history, simulated status updates.
ReportIssuesForm
UI for submitting issues with placeholders, validation, and engagement elements.
MainMenuForm
Entry point UI, navigation, and notification viewing.
3.2 Data Handling
Part 1 uses in-memory lists (List<Issue>).
Part 2 will integrate a database for persistence.


4. Installation & Setup
Requirements:
Windows OS
.NET Framework (4.7.2) and Hogher
Visual Studio 2019/2022
Steps:
Open the solution file (.sln) in Visual Studio.
Set startup project to MunicipalServiceAppPart1.
Build (Ctrl+Shift+B).
Run (F5).


5. Usage Guide
Launch → Main Menu appears.
Click Report Issues.
Enter Location, select Category, describe the issue.
Attach files (optional).
Watch the progress bar & messages as you complete fields.
Submit → receive tracking ID + confirmation.
Notifications simulate issue updates over time.
Return to main menu; use View Notifications to check history.


6. Engagement Strategy Implementation
Strategy chosen: Push notifications & timely feedback.
Mapped to app:
Confirmation with tracking ID.
Progress updates displayed as notifications.
History stored and accessible for citizen review.
Motivational UI (progress bar, labels, estimated response times).
Ensures trust, transparency, and continued participation.


7. Limitations (Part 1)
Data not persisted (clears on exit).
Only “Report Issues” is functional.
Notifications are local MessageBox popups, not external push.
12. Planned Features (Part 2+)
Persistent database (SQL).
Service request tracking interface.
Local events/announcements page.
Authentication + user roles.
Mobile extension with real push notifications.
Accessibility and multilingual support.
13. Acknowledgements
Developed for PROG7312 POE, 2025.
User engagement framework informed by Hart et al. (2016) and POE brief.

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
------------------------------------------------ ################ What's New in Part 2 ###################-----------------------------------------------------------------------
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

### Major Features Added:

1. **Local Events and Announcements Page**
   - Display 18+ pre-loaded community events
   - Professional visual layout with detailed event information
   - Real-time statistics tracking

2. **Advanced Search Functionality**
   - Keyword search across all event fields
   - Category-based filtering
   - Date range filtering
   - Combined search and filter capabilities

3. **Sorting System**
   - Sort by Date (Earliest/Latest First)
   - Sort by Name (A-Z / Z-A)
   - Sort by Category
   - Sort by Priority (High to Low)

4. **Smart Recommendation Engine**
   - Tracks user search patterns
   - Analyzes search frequency
   - Suggests relevant events based on interests
   - Improves accuracy with continued usage

5. **Enhanced Visual Design**
   - Professional municipality logo with detailed city hall icon
   - Colorful South African flag-inspired border
   - Gradient background with subtle patterns
   - Consistent government-grade branding
   - Semi-transparent panels for depth

6. **Advanced Data Structures**
   - Dictionary for O(1) event lookup
   - SortedDictionary for chronological organization
   - HashSet for unique category management
   - Queue for recent searches (FIFO)
   - Stack for undo functionality (bonus)

---

## Features Overview

### 3.1 Main Menu (Updated)

**Active Features:**
-  Report Issues (Part 1 - Fully functional)
-  Local Events & Announcements (Part 2 - NEW!)
-  Service Request Status (Part 3 - Coming soon)

**Visual Elements:**
- Ultra-detailed professional municipality logo (160x160px)
- Colorful South African flag-inspired gradient border
- Real-time notifications status indicator
- View Notifications button
- Professional footer with copyright

**Branding:**
- City of Johannesburg Metropolitan Municipality logo
- South African flag colors (Green, Gold, Red, Blue)
- Government-style seal with city hall icon
- Curved text reading "JOHANNESBURG MUNICIPALITY"
- "EST. 2000" establishment year

---

### 3.2 Report Issues Form (Part 1 - Enhanced)

**Core Functionality:**
- Location input with placeholder text
- Category dropdown (10+ categories)
- Rich text description box
- Multiple file attachments

**Engagement Features:**
- Real-time progress bar (0-100%)
- Dynamic motivational messages
- Estimated response time calculator
- Priority categorization
- Unique tracking ID generation

**Push Notifications:**
- Instant submission confirmation
- Status update notifications
- Progress tracking
- Notification history storage

---

### 3.3 Local Events and Announcements (Part 2)

#### Display Area

**Events List (Left Panel):**
- Scrollable list of upcoming events
- Format: "Title - Date at Location"
- Supports 50+ events
- Real-time filtering and sorting

**Event Details (Right Panel):**
- Comprehensive event information
- Color-coded categories
- Professional formatting
- Organizer information
- Full description

**Statistics Bar:**
- Total events in system
- Upcoming events count
- Currently displayed events
- Updates in real-time

#### Search and Filter System

**Keyword Search:**
- Search across: Title, Description, Location, Category
- Real-time results
- Search pattern tracking
- Placeholder guidance

**Category Filter:**
- Dropdown with all available categories:
  - Community Service
  - Government
  - Education
  - Infrastructure
  - Entertainment
  - Utilities
  - Health
  - Environmental
  - Cultural
  - Economic Development
  - Employment
  - Legal Services
  - Sports
  - Social Services
- "All Categories" option included

**Date Range Filter:**
- Start Date picker (From)
- End Date picker (To)
- Default range: Today → 3 months
- Works with category filter

**Clear Filters Button:**
- Resets all search criteria
- Restores default view
- Clears sort selection

#### Sorting Options (NEW!)

Users can sort events by:
1. **Date (Earliest First)** - Upcoming events first
2. **Date (Latest First)** - Furthest events first
3. **Name (A-Z)** - Alphabetical order
4. **Name (Z-A)** - Reverse alphabetical
5. **Category (A-Z)** - Grouped by category, then by date
6. **Priority (High to Low)** - Urgent events first

**How Sorting Works:**
- Dropdown selection in filter panel
- Applies to current filtered results
- Real-time re-ordering
- Maintains filter selections

#### Smart Recommendation System 

**How It Works:**
1. Tracks what users search for
2. Counts search frequency by keyword/category
3. Identifies user's top 3 interests
4. Suggests 5 most relevant upcoming events
5. Updates dynamically after each search

**Recommendation Algorithm:**
- Analyzes search patterns using Dictionary<string, int>
- Tracks recent searches using Queue<string>
- Prioritizes high-priority events when no history
- Combines user interests with event urgency

**Display:**
- Bottom right panel
- Labeled " Recommended Events (Based on Your Interests)"
- Shows  indicator when selected
- Explains recommendation basis


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
------------------------------------------------ ################ What's New in Part 3 (Final Full Project) ###################--------------------------------------------------
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

## Introduction

This application is part of the Programming PROG7312 Portfolio of Evidence. It is a C# .NET Window-Forms desktop application that essentually allows South African citizens to report municipal issues, view local events, as well as track service request statuses in real-time. The system demonstrates quite advanced programming concepts, user engagement strategies, and also sophisticated data structures.

**Complete Feature Set:**
- **Part 1:** Report Issues (Issue submission and tracking)
- **Part 2:** Local Events and Announcements (Event discovery and recommendations)
- **Part 3:** Service Request Status (Advanced tracking with complex data structures)

The system demonstrates professional-grade concepts including:
- Advanced data structures (BST, AVL Tree, Red-Black Tree, Min Heap, Graph)
- Complex algorithms (BFS, DFS, Tree balancing, Priority queues)
- Professional UI/UX design with South African municipal branding
- Real-time status tracking as well as dependency management
- Intelligent search, filter, and sort capabilities

---

## Features Overview

### 2.1 Main Menu (All Parts Active)

**Three Fully Functional Features:**
- ✅ **Report Issues** (Part 1 - Active)
- ✅ **Local Events and Announcements** (Part 2 - Active)
- ✅ **Service Request Status** (Part 3 - NEW!(Final)

**Visual Elements:**
- Ultra-detailed professional municipality logo (160x160px)
- Colorful South African flag-inspired gradient border
- Real-time notifications status indicator 
- View Notifications button
- Professional footer with copyright

**Branding:**
- City of Johannesburg Metropolitan Municipality logo
- South African flag colors (Green, Gold, Red, Blue, Black, White)
- Government-style seal with detailed city hall icon
- Curved text: "JOHANNESBURG MUNICIPALITY"
- "EST. 2000" establishment year

---

### 2.2 Report Issues Form (Part 1 - Enhanced)

**Core Functionality:**
- Location input with placeholder text
- Category dropdown (Around 15 categories)
- Rich text description box
- Multiple file attachments with preview

**Engagement Features:**
- Real-time progress bar (0-100%)
- Dynamic motivational messages
- Estimated response time calculator
- Priority categorization implamentation
- Unique tracking ID generation

**Push Notifications:**
- Instant submission confirmation
- Status update notifications
- Progress tracking
- Notification history storage

---

### 2.3 Local Events and Announcements (Part 2)

**Display System:**
- Events list with Around 18+ pre-loaded community events
- Detailed event information panel
- Real-time statistics tracking
- Professional visual layout

**Search and Filter:**
- Keyword search across all event fields
- Category-based filtering (14 categories)
- Date range filtering
- Combined search capabilities

**Sorting Options:**
- Date (Earliest/Latest First)
- Name (A-Z / Z-A)
- Category (A-Z)
- Priority (High to Low)

**Smart Recommendation Engine:**
- Tracks user search patterns
- Analyzes search frequency
- Suggests relevant events based on interests
- Improves with continued usage

**Data Structures (Part 2):**
- Dictionary for O(1) event lookup
- SortedDictionary for chronological organization
- HashSet for unique category management
- Queue for recent searches (FIFO)
- Stack for undo functionality

-----

### 2.4 Service Request Status (Part 3 - NEW!)

**Overview:**
The Service Request Status feature provides comprehensive tracking of all municipal service requests using advanced data structures for optimal performance and advanced relationship management.

**Visual Design:**

**Color-Coded Priority System:**
- 🔴 **RED** - High Priority (1): Critical issues requiring immediate attention
  - Examples Include: Gas leaks, major water pipe bursts, power outages
- 🟡 **YELLOW** - Medium Priority (2): Important issues needing prompt attention
  - Examples Include: Broken traffic lights, damaged signs, fire hydrants
- 🟢 **GREEN** - Low Priority (3): Routine issues to be addressed
  - Examples Include: Graffiti, minor cracks, missed garbage collection
- 🔵 **TEAL** - Very Low Priority (4): Suggestions and minor requests
  - Examples Include: Park maintenance, flower beds, bike rack installation

**Display Features:**
- Professional list view with color-coded priority indicators
- Priority badges efectivly showing the urgency level
- Enhanced spacing and modern design (28px tall items)
- Left border color strip for instant visual recognition
- Master-detail view (list + detailed information panel)

**Search and Filter Capabilities:**

**Search Options:**
- Search by Request ID (quick lookup)
- Returns detailed request information
- Uses Binary Search Tree for O(log n) performance

**Filter Options Include:**
- **By Status:**
  - All Requests
  - Pending
  - In Progress
  - Completed
- **By Category:**
  - All Categories
  - Roads
  - Utilities
  - Water
  - Sanitation
  - Parks
  - Infrastructure

**View Options Include:**
- **View All:** Shows all of the requests in chronological order (mixed priorities)
- **By Priority:** Sorts requests by urgency (High → Very Low)
- **Dependencies:** Shows related requests and dependencies

**Request Details Panel:**

Displays comprehensive information:
- Request ID
- Priority Level (with color-coded emoji indicator)
- Category
- Location
- Current Status
- Date Submitted
- Request Age (calculated in days)
- Full Description
- Dependencies (if any)

**Statistics Display:**
- Real-time count by status (Pending, In Progress, Completed)
- Progress bar that shows the overall completion rate
- Updates dynamically with filters

**Sample Data:**
- Around 27 diverse service requests 
- Realistic descriptions and locations
- Mix of statuses and priorities
- 5 dependency relationships

---

## Technical Architecture - Part 3 (Advanced Data Structures)

### 3.1 Advanced Data Structures Implemented

**1. Binary Search Tree (BST)**
- **File:** `BinarySearchTree.cs`
- **Purpose:** Efficient/Effective searching and retrieval by Request ID
- **Performance:** O(log n) average case for search, insert
- **Use Case:** Primary search mechanism for finding requests
- **Features:**
  - Recursive insertion and search
  - In-order traversal for sorted output
  - Maintains requests sorted by ID

**2. AVL Tree (Self-Balancing BST)**
- **File:** `AVLTree.cs`
- **Purpose:** Guaranteed balanced tree structure
- **Performance:** O(log n) guaranteed for all operations
- **Use Case:** Alternative search with optimal worst-case performance
- **Features:**
  - Automatic balancing through rotations
  - Four rotation types (LL, RR, LR, RL)
  - Height tracking and balance factor calculation
  - Prevents degeneration into linked list

**3. Red-Black Tree**
- **File:** `RedBlackTree.cs`
- **Purpose:** Self-balancing BST with relaxed balancing
- **Performance:** O(log n) with fewer rotations than AVL
- **Use Case:** Efficient for insertion-heavy workloads
- **Features:**
  - Node coloring (Red/Black)
  - Sentinel nil node for boundaries
  - Five balancing properties maintained
  - Insertion fixup procedure
  - Less rigid balancing = better insertion performance

**4. Min Heap (Priority Queue)**
- **File:** `MinHeap.cs`
- **Purpose:** Priority-based service request management
- **Performance:** O(1) peek, O(log n) insert/extract
- **Use Case:** Sorting requests by priority, scheduling
- **Features:**
  - Array-based heap representation
  - Heapify-up for insertions
  - Heapify-down for extractions
  - Parent = (i-1)/2, Left = 2i+1, Right = 2i+2
  - Lower priority number = Higher urgency

**5. Graph (Adjacency List)**
- **File:** `ServiceRequestGraph.cs`
- **Purpose:** Model dependencies between service requests
- **Performance:** O(V + E) for traversal
- **Use Case:** Dependency tracking, relationship discovery
- **Features:**
  - Directed edges for dependencies
  - Breadth-First Search (BFS) traversal
  - Depth-First Search (DFS) traversal
  - Related request discovery
  - Path finding between requests


### 3.2 Algorithm Implementations

**Tree Balancing Algorithms:**
- Single rotations (Left,/ Right)
- Double rotations (Left-Right, Right-Left)
- Height calculation and balance factor computation
- Red-Black tree color fixup

**Graph Traversal Algorithms:**
- **BFS:** Level-order exploration, shortest path finding
- **DFS:** Depth-first exploration, cycle detection
- Path existence checking
- Connected component discovery

**Heap Operations:**
- Heapify-up (bubble up)
- Heapify-down (bubble down)
- Extract minimum
- Priority-based sorting

### 3.3 Core Classes (Part 3)

**ServiceRequest**
- **Fields:** RequestId, Description, Status, Category, Location, DateSubmitted, Priority, AttachedMedia
- **Purpose:** Data model for service requests
- **Features:** Default status ("Pending"), priority levels (1-4)

**ServiceRequestManager**
- **Purpose:** Facade class coordinating all data structures
- **Features:**
  - Manages BST, AVL, Red-Black Tree, Heap, and Graph
  - Provides unified interface for all operations
  - Includes 27 sample service requests
  - Generates statistics
  - Handles search, filter, and sort operations

**ServiceRequestStatusForm**
- **Purpose:** Windows Form UI for Part 3
- **Features:**
  - Color-coded list display (owner-drawn)
  - Custom drawing with priority indicators
  - Master-detail view
  - Multiple filter and sort options
  - Real-time statistics
  - Progress tracking
  - Professional grey back button

### 3.4 Data Structure Comparison

| Data Structure | Search | Insert |     Best Use Case       |
|----------------|--------|--------|-------------------------|
| BST | O(log n) avg | O(log n) avg | Quick sorted retrieval |
| AVL Tree | O(log n) | O(log n)    | Guaranteed performance |
| Red-Black Tree | O(log n) | O(log n) | Frequent insertions |
| Min Heap | O(n) | O(log n)        | Priority management    |
| Graph | O(V+E) |       O(1)       | Relationship modeling  |

------------------------------------

## Installation & Setup

**Requirements:**
- Windows OS
- .NET Framework 4.7.2 or higher
- Visual Studio 2019/2022

**Steps:**
1. Download or clone the repository
2. Open the solution file (.sln) in Visual Studio
3. Set startup project to `MunicipalServiceAppPart1`
4. Build the solution (Ctrl+Shift+B)
5. Run the application (F5)

-------

## Usage Guide

### Part 1 - Report Issues
1. Launch application → Main Menu appears
2. Click **"Report Issues"**
3. Enter Location, select Category, describe the issue
4. Attach files (optional)
5. Watch progress bar and motivational messages
6. Submit → receive tracking ID + confirmation
7. Notifications simulate issue updates over time
8. Return to main menu; use **"View Notifications"** to check history

### Part 2 - Local Events
1. From Main Menu, click **"Local Events & Announcements"**
2. Browse 18+ pre-loaded community events
3. Use keyword search to find specific events
4. Filter by category or date range
5. Sort by date, name, category, or priority
6. View recommendations based on your search patterns
7. Click any event for detailed information

### Part 3 - Service Request Status (NEW!)
1. From Main Menu, click **"Service Request Status"**
2. View all 27 service requests in mixed order
3. **Search** by Request ID for specific request
4. **Filter** by Status (Pending/In Progress/Completed)
5. **Filter** by Category (Roads/Utilities/Water/etc.)
6. **Sort** by clicking **"By Priority"** (Red → Yellow → Green → Blue)
7. Click any request to see detailed information
8. Check **Dependencies** to see related requests
9. Monitor progress bar showing completion rate
10. View real-time statistics by status

**Visual Guide:**
- 🔴 Red items = Handle immediately (High Priority)
- 🟡 Yellow items = Schedule soon (Medium Priority)
- 🟢 Green items = Normal priority (Low Priority)
- 🔵 Blue items = Suggestions (Very- Low Priority)

---


## Engagement Strategy Implementation

**Strategy Chosen:** Push notifications, timely feedback, and visual priority management

**Mapped to Application:**

**Part 1:**
- Confirmation with tracking ID
- Progress updates via notifications
- History stored and accessible
- Motivational UI elements

**Part 2:**
- Smart recommendations based on user behavior
- Real-time search results
- Personalized event suggestions
- Statistics tracking

**Part 3:**
- Color-coded priority visualization
- Real-time status updates
- Progress bar for completion tracking
- Professional UI encouraging engagement
- Clear dependency relationships
- Immediate search feedback

**Result:** Ensures trust, transparency, and continued citizen participation/satisfaction

---

## Advanced Features Demonstrated

### Part 3 Technical Achievements:

**1. Owner-Drawn Controls**
- Custom ListBox rendering
- Color-coded priority indicators
- Professional badges and borders
- Enhanced visual hierarchy

**2. Multiple Data Structure Integration**
- Coordinated use of 5 different structures
- Facade pattern for unified access
- Optimal structure selection per operation
- Demonstrates trade-off understanding

**3. Algorithm Efficiency**
- O(log n) guaranteed search performance
- O(1) priority queue peek
- Efficient graph traversal
- Balanced tree operations

**4. Dependency Management**
- Graph-based relationship modeling
- BFS/DFS traversal for discovery
- Visual dependency display
- Prevents circular dependencies

**5. Professional UI/UX**
- Color theory application
- Visual priority system
- Responsive design
- Modern, polished appearance

---

## Limitations & Future Enhancements

**Current Limitations:**
- Data not persisted (in-memory only)
- Notifications are local MessageBox popups
- Single-user system
- No authentication implamented

**Planned Enhancements Could Include The following:**
- Persistent database (SQL Server)
- External push notifications (Firebase)
- User authentication and roles
- Mobile application extension
- Multilingual support (English, Zulu, Afrikaans)
- Accessibility features (screen reader support)
- Real-time sync across devices
- Admin dashboard for municipal staff
- PDF report generation
- Email notifications
- SMS alerts for critical issues

---

## Technologies Used

- **Language:** C# (.NET Framework 4.7.2)
- **IDE:** Visual Studio 2022
- **UI Framework:** Windows Forms
- **Version Control:** Git/GitHub
- **Design Patterns:** Facade, Observer, Strategy
- **Data Structures:** BST, AVL Tree, Red-Black Tree, Min Heap, Graph, Dictionary, SortedDictionary, HashSet, Queue, Stack, List

---

**Research References:**
- User engagement framework informed by Hart et al. (2020)
- Municipal service best practices
- South African municipal standards

**Technical References:**
- IIE PROG7312 Module Manual
- Geeks for Geeks: C# Windows Forms Applications
- Microsoft Learn: Windows Forms Documentation
- Programming Knowledge: C# Windows Forms Tutorials
-----------------------------------------------------------------------------------------------


Bibliography:

College, I. V., 2025. PROG7312 Module-Manual / Module-Outline. Pretoria: Varsity College Pretoria.
Geeks, G. f., 2025. Introduction to C# Windows Forms Applications. [Online] 
Available at: https://www.geeksforgeeks.org/c-sharp/introduction-to-c-sharp-windows-forms-applications/
[Accessed 8 September 2025].
Microsoft, 2025. Tutorial: Create a Windows Forms app in Visual Studio with C#. [Online] 
Available at: https://learn.microsoft.com/en-us/visualstudio/ide/create-csharp-winform-visual-studio?view=vs-2022
[Accessed 8 September 2025].
ProgrammingKnowledge2, 2023. Create Your First C# Windows Forms Application using Visual Studio. [Online] 
Available at: https://youtu.be/JSJ1Jl2aLJg?si=A4tSAz_ueBg5cOgf
[Accessed 08 September 2025].
//BroCode, 2021. Tree data structures in 2 minutes. [Online]
//Available at: https://youtu.be/Etpc_-br5rl?si=tY2wjw9rX62kTGUD
//[Accessed 12 November 2025].


Photos of the 3 Main Windows of the application Below:

<img width="1396" height="1314" alt="image" src="https://github.com/user-attachments/assets/ba817543-9cf1-4de7-b761-52bd51d3c8b5" />

<img width="1594" height="1513" alt="image" src="https://github.com/user-attachments/assets/9fd29598-a10e-4270-8c31-4f2828fb054f" />

<img width="2398" height="1645" alt="image" src="https://github.com/user-attachments/assets/b310c35b-e3d1-48a8-9971-12c8a9318a13" />

<img width="1395" height="1311" alt="image" src="https://github.com/user-attachments/assets/c979ba13-83e2-4b98-b99a-213171b429d7" />

<img width="2001" height="1415" alt="image" src="https://github.com/user-attachments/assets/175126b7-fad6-4820-b47e-af2d84474e8a" />

<img width="2002" height="1418" alt="image" src="https://github.com/user-attachments/assets/5a8c75d8-cf9e-4c64-a0b3-5d209fbc2a89" />

<img width="2004" height="1417" alt="image" src="https://github.com/user-attachments/assets/97d359bf-93c0-4ded-9cf9-2ece27e05fd0" />













