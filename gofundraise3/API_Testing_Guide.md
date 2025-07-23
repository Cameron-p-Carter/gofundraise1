# Task Management API - Testing Guide

## 🚀 API Overview

Your enterprise-grade Task Management API is now running successfully! Here's what you have:

### **Base URL**: `http://localhost:5265`
### **Swagger UI**: `http://localhost:5265/swagger`
### **Health Check**: `http://localhost:5265/health`

## 📊 Available Endpoints

### **Projects API** (`/api/v1/projects`)

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/v1/projects` | Get all projects |
| GET | `/api/v1/projects/{id}` | Get project by ID |
| POST | `/api/v1/projects` | Create new project |
| PUT | `/api/v1/projects/{id}` | Update project |
| DELETE | `/api/v1/projects/{id}` | Delete project |
| GET | `/api/v1/projects/status/{status}` | Get projects by status |

### **Tasks API** (`/api/v1/tasks`)

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/v1/tasks` | Get all tasks |
| GET | `/api/v1/tasks/{id}` | Get task by ID |
| POST | `/api/v1/tasks` | Create new task |
| PUT | `/api/v1/tasks/{id}` | Update task |
| DELETE | `/api/v1/tasks/{id}` | Delete task |
| GET | `/api/v1/tasks/project/{projectId}` | Get tasks by project |
| GET | `/api/v1/tasks/status/{status}` | Get tasks by status |
| GET | `/api/v1/tasks/priority/{priority}` | Get tasks by priority |

## 🧪 Sample API Calls

### 1. Get All Projects
```http
GET http://localhost:5265/api/v1/projects
```

### 2. Get Project by ID
```http
GET http://localhost:5265/api/v1/projects/1
```

### 3. Create New Project
```http
POST http://localhost:5265/api/v1/projects
Content-Type: application/json

{
  "name": "New Website Feature",
  "description": "Add user authentication system",
  "status": "Planning",
  "dueDate": "2025-03-01T00:00:00Z"
}
```

### 4. Get All Tasks
```http
GET http://localhost:5265/api/v1/tasks
```

### 5. Create New Task
```http
POST http://localhost:5265/api/v1/tasks
Content-Type: application/json

{
  "title": "Design login page",
  "description": "Create mockups for user login interface",
  "status": "Todo",
  "priority": "High",
  "projectId": 1,
  "dueDate": "2025-02-15T00:00:00Z"
}
```

### 6. Get Tasks by Status
```http
GET http://localhost:5265/api/v1/tasks/status/InProgress
```

### 7. Get Projects by Status
```http
GET http://localhost:5265/api/v1/projects/status/Active
```

## 📋 Valid Enum Values

### Project Status
- `Planning`
- `Active`
- `OnHold`
- `Completed`
- `Cancelled`

### Task Status
- `Todo`
- `InProgress`
- `Done`
- `Cancelled`

### Task Priority
- `Low`
- `Medium`
- `High`
- `Critical`

## 🗄️ Sample Data

The database comes pre-loaded with sample data:

### Projects:
1. **Website Redesign** (Active)
2. **Mobile App Development** (Planning)
3. **API Documentation** (Completed)

### Tasks:
1. **Create wireframes** (Done, High Priority)
2. **Implement responsive design** (InProgress, High Priority)
3. **Research frameworks** (Todo, Medium Priority)
4. **Write API documentation** (Done, Medium Priority)

## 🏗️ Architecture Features

✅ **Clean Architecture** - Separation of concerns with layers
✅ **Repository Pattern** - Data access abstraction
✅ **Service Layer** - Business logic separation
✅ **AutoMapper** - Object mapping
✅ **Entity Framework Core** - ORM with SQLite
✅ **Swagger/OpenAPI** - Interactive API documentation
✅ **Consistent API Responses** - Standardized response format
✅ **Input Validation** - Request validation
✅ **Error Handling** - Comprehensive error responses
✅ **CORS Support** - Cross-origin requests enabled
✅ **Health Checks** - API health monitoring

## 🎯 Testing in Swagger

1. Open `http://localhost:5265/swagger`
2. Expand any endpoint
3. Click "Try it out"
4. Fill in parameters/request body
5. Click "Execute"
6. View the response

## 🔧 Database

- **Type**: SQLite
- **File**: `TaskManagement.db` (in project root)
- **Migrations**: Automatically applied on startup
- **Seed Data**: Pre-loaded sample data

Your enterprise-grade Task Management API is ready for development and testing!
