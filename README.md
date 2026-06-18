# DapperApi

## Project Structure

```txt
DapperApi/
├─ Controllers/
│  └─ StudentsController.cs
├─ Models/
│  ├─ Student.cs
│  ├─ Course.cs
│  └─ StudentWithCourses.cs
├─ Repositories/
│  ├─ IStudentRepository.cs
│  └─ StudentRepository.cs
├─ Database/
│  └─ SchoolDB.sql
├─ appsettings.json
├─ Program.cs
└─ DapperApi.csproj
```

## Setup

### 1. Create database

Open SQL Server Management Studio and run:

```sql
Database/SchoolDB.sql
```

### 2. Update connection string

Open `appsettings.json`.

Windows Authentication:

```json
"DefaultConnection": "Server=localhost;Database=SchoolDB;Trusted_Connection=True;TrustServerCertificate=True;"
```

SQL Authentication:

```json
"DefaultConnection": "Server=localhost;Database=SchoolDB;User Id=sa;Password=yourpassword;TrustServerCertificate=True;"
```

### 3. Restore and run

```bash
dotnet restore
dotnet run
```

Open Swagger:

```txt
https://localhost:5001/swagger
```

or

```txt
http://localhost:5000/swagger
```

## API Endpoints

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/students` | Get all students |
| GET | `/api/students/{id}` | Get student by id |
| POST | `/api/students` | Create student |
| PUT | `/api/students` | Update student |
| DELETE | `/api/students/{id}` | Delete student |
| GET | `/api/students/search?name=An` | Search students by name |
| GET | `/api/students/courses` | Get students with courses |

## Example POST Body

```json
{
  "name": "Nguyen Van Dung",
  "age": 23,
  "email": "dung@example.com"
}
```

## Example PUT Body

```json
{
  "id": 1,
  "name": "Nguyen Van An Updated",
  "age": 22,
  "email": "an.updated@example.com"
}
```
