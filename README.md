# SyncPoint

> **Group Contribution & Task Manager**

SyncPoint is a desktop application built using **C# WinForms** that helps groups and teams manage tasks, track individual contributions, and monitor overall team progress in one centralized system.

The project aims to solve common problems in group work such as lack of transparency, poor task organization, and uneven contribution distribution. Instead of relying on scattered messages or separate to-do lists, SyncPoint provides a structured environment where every member can stay updated and accountable.

Whether for school projects, capstones, or collaborative activities, SyncPoint helps teams stay organized and productive.

---

# Features

## First Time Setup
- Runs only once during the first launch
- Creates the Instructor account
- Stores credentials in a local config file
- Username validation:
  - Minimum 4 characters
  - No spaces allowed
- Password validation:
  - Minimum 6 characters
  - Requires confirmation
- Styled interface with gold-themed UI

---

# Authentication System

## Login Form
- Username and password authentication
- Password hashing support
- Automatic role detection
- Redirects users to the correct dashboard
- Enter key support for login
- Validation warnings for empty fields
- Prevents unassigned members from accessing dashboards
- Includes registration link

## Registration Form
- User account creation
- Full name, username, and password input
- Username uniqueness checking
- Placeholder text behavior
- Password confirmation validation
- Success notification after registration

---

# Instructor Dashboard

## Groups Tab
- Displays all created groups
- Shows:
  - Group Name
  - Appointed Leader
  - Member Count
- Dynamic group counter
- Create Group button
- Appoint Leader functionality
- Auto-refresh after updates

## Reports Tab
- Tab where the instructors can see a group's progress

## Navigation & Session
- Sidebar navigation
- Logout confirmation dialog

---

# Leader Dashboard

## Stats Overview
Displays:
- Total Tasks
- Completed Tasks
- Pending Tasks

All statistics are loaded from the database.

---

# Add Task Form
- Task title input
- Member assignment dropdown
- "Any Member" option
- Deadline picker
- Description field
- Prevents past deadlines
- Auto-refreshes dashboard after submission

---

# Add Member Form
- Displays available students
- Live search filtering
- Excludes leaders automatically
- Student count display
- Selection preview
- Confirmation dialog after adding

---

# Sidebar Navigation
- Dashboard
- Add Task
- Members
- Reports
- Logout

---

# Member Dashboard

## Stats Overview
Displays:
- Total Tasks
- Completed Tasks
- In Progress Tasks
- Shows assigned tasks to the logged-in member.

---

# My Tasks Table

### Columns
- Task Title
- Description
- Due Date
- Difficulty Level
- Status

### Additional Features
- Bold task titles
- Color-coded status indicators
- Color-coded difficulty labels
- Deadline warnings for approaching due dates
- Empty-state notification for users without groups

---

# Task Progress Form
A visual transparency screen that:
- Displays team member cards
- Shows completed task counts
- Provides quick team performance monitoring

---

# Object-Oriented Programming Principles Used

## 1. Abstraction
Examples:
- `ValidateLogin()`
- `GetTasksByMember()`
- `CreateTask()`
- `RegisterUser()`

Complex database and validation logic are hidden behind reusable methods.

---

## 2. Encapsulation
Examples:
- `Session` class stores logged-in user data securely
- Database access handled through `DatabaseHelper`
- Config file handling hidden inside setup methods

---

## 3. Inheritance
Examples:
- All forms inherit from `Form`
- `SidebarControl` inherits from `UserControl`
- Shared WinForms lifecycle functionality

---

## 4. Polymorphism
Examples:
- `OpenDashboard(role)`
- Dynamic UI styling using `CellFormatting`
- Sidebar active-state switching

---

# UML Diagram

> Attach UML diagram here

---

# Technologies Used

- **C#**
- **WinForms**
- **SQL Database**
- **.NET Framework / .NET 6+**
- **Visual Studio 2022**

---

# How to Run the Application

## Requirements
- Windows 10 or 11
- Visual Studio 2022
- .NET Framework 4.8 or .NET 6+
- SQL Server or SQLite

---

# Installation Steps

## 1. Clone the Repository

```bash
git clone https://github.com/minillyy/SyncPoint.git
```

## 2. Open the Solution
- Launch Visual Studio 2022
- Open `SyncPoint.slnx`

## 3. Run the Application

```text
F5
```

or

```text
Ctrl + F5
```

On first launch, the Setup screen will appear.

---

# How the Application Works

1. First launch opens `SetupForm`
2. Instructor sets up their account
3. User registers through `RegisterForm`
4. Instructor creates groups
5. Instructor appoints leaders
6. User logs in through `LoginForm`
7. System detects user role automatically
8. Leaders add members
9. Leaders assign tasks
10. Members monitor assigned tasks
11. Progress tracking updates in real time
12. Member submits tasks then wait for leader's approval
13. Once approved, the points are granted to that member

All data is stored in a local SQL database.

---

# Developers

| Name | Role |
|---|---|
| Dapoc, Romelie Joy M. | GUI Developer |
| Lozada, Chester | Logic Developer |
| Villegas, Lemuel | Project Manager |

---

# License

This project is for educational purposes only.

---

# SyncPoint Philosophy

> *"A group that tracks together, stays on track together."*
