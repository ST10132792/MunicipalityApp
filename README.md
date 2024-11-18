# Municipality Services Application - Implementation Report

## Table of Contents
1. [Getting Started](#getting-started)
2. [Overview](#overview)
3. [How to Run](#how-to-run)
4. [Features](#features)
5. [Data Structures Implementation](#data-structures-implementation)
6. [Usage Guide](#usage-guide)

## Getting Started

### Cloning the Repository

1. Open your terminal/command prompt
2. Navigate to desired directory
3. Run the following command:
```git clone https://github.com/ST10132792/MunicipalityApp.git```
4. Navigate into the project directory:
```cd MunicipalityApp```

### Prerequisites
- Visual Studio 2019 or later
- .NET Framework 4.8 or later
- Git installed on your machine

### Setting Up the Development Environment
1. Open Visual Studio
2. Go to File > Open > Project/Solution
3. Navigate to the cloned repository
4. Select the `MunicipalityApp.sln` file
5. Wait for Visual Studio to load all dependencies
6. Build the solution (Ctrl + Shift + B)

## Overview
The Municipality Services Application is a Windows Forms application designed to manage municipal services, including issue reporting, event management, and service request tracking. This report focuses on the Service Request Status feature and its underlying data structures.

## How to Run
1. Open the solution in Visual Studio 2019 or later
2. Build the solution (Ctrl + Shift + B)
3. Run the application (F5)
4. Click "Service Request Status" from the main menu

## Features
The Service Request Status system implements several advanced data structures to efficiently manage and display service requests:

### Core Features
- Track service requests using unique identifiers
- View all service requests in a organized list
- Monitor emergency requests
- View related requests
- Analyze request relationships through minimum spanning trees

## Data Structures Implementation

### 1. AVL Tree
**Purpose**: Primary storage and retrieval of service requests
csharp
public class AVLTree<T> where T : IComparable<T>
{
private AVLNode<T> root;
// Implementation details...
}
**Benefits**:
- O(log n) search, insert, and delete operations
- Self-balancing ensures optimal performance
- Efficient for ordered traversal of requests

**Example Usage**:
csharp
// Adding a new service request
requestTree.Insert(new ServiceRequest(1, "Road maintenance", "Infrastructure"));
// Finding a specific request
var request = requestTree.Find(searchRequest);


### 2. Min Heap
**Purpose**: Prioritizes emergency service requests

csharp
public class ServiceRequestHeap
{
private ServiceRequest[] heap;
private int size;
}

**Benefits**:
- O(1) access to highest priority emergency requests
- O(log n) insertion and deletion
- Optimal for priority-based operations

**Example Usage**:
csharp
// Adding emergency request
emergencyHeap.Insert(emergencyRequest);
// Retrieving emergency requests
var emergencies = emergencyHeap.GetEmergencyRequests();

### 3. Graph Structure
**Purpose**: Manages relationships between related service requests

csharp
public class ServiceRequestGraph
{
private Dictionary<int, HashSet<int>> adjacencyList;
private Dictionary<int, ServiceRequest> requests;
}

**Benefits**:
- Efficient relationship mapping between requests
- Supports complex relationship queries
- Enables relationship analysis

**Example Usage**:
csharp
// Adding related requests
requestGraph.AddRelation(request1.Id, request2.Id);
// Finding related requests
var related = requestGraph.GetRelatedRequests(requestId);


### 4. Graph Traversal (BFS)
**Purpose**: Efficiently finds all related requests

csharp
public List<ServiceRequest> GetRelatedRequests(int requestId)
{
var visited = new HashSet<int>();
var queue = new Queue<int>();
// Implementation details...
}

**Benefits**:
- O(V + E) time complexity
- Memory-efficient implementation

### 5. Minimum Spanning Tree
**Purpose**: Optimizes request relationship visualization

csharp
public List<(int, int)> GetMinimumSpanningTree()
{
// Kruskal's Algorithm implementation
}

**Benefits**:
- Identifies optimal relationships between requests
- Reduces redundant connections
- Provides efficient relationship overview

### 6. Custom Priority Queue for Events
**Purpose**: Manages and sorts upcoming events

**Benefits**:
- Efficient event sorting
- Efficient retrieval using priority queue
- Used by event recommendation system

### 7. Event Recommendation System
**Purpose**: Provides personalized event suggestions based on user preferences

**How it Works**:
1. Uses weighted search terms to find relevant events
2. Maintains a priority queue of upcoming events
3. Uses an algorithm to find the most relevant events based on the search query
4. Displays the most relevant events at the top of the list and keeps a history of recommended events

**Benefits**:
- Personalized event suggestions
- Balances relevance with date proximity, name, and category
- Efficient retrieval using priority queue

## Usage Guide

### Reporting issues
1. Launch the application
2. Click "Report Issues"
3. Enter the required details such as location, description, category, and optionally an image
4. Click "Submit" and you will see your submission in the table

### Local Events and Announcements
1. Launch the application
2. Click "Local Events and Announcements"
3. Enter the event category or name in the search bar and click search
4. You will see the events and announcements in the list, 1 list with the most accurate results at the top and another with recommended results at the top based on search history

### Tracking Service Requests
1. Launch the application
2. Click "Service Request Status"
3. Enter a request ID in the tracking field
4. Click "Track" to view request details

### Viewing Emergency Requests
1. Click "Show Emergencies" button
2. View emergency requests
3. System displays all requests marked as "Emergency Response"

### Analyzing Request Relationships
1. Track a specific request
2. View related requests in the details section

### Performance Considerations
- AVL Tree ensures balanced access to requests
- Heap structure optimizes emergency request handling
- Graph implementation enables efficient relationship tracking
- BFS traversal provides systematic relationship exploration
- MST algorithm optimizes relationship visualization

