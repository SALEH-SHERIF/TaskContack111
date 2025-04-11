
# 📇 Contact Management API

A secure and lightweight .NET Core Web API for user registration, authentication, and personal contact management.  
Users can sign up, sign in, and manage their own address book.

## 🚀 Live Demo

👉 [Swagger UI](http://contact1.runasp.net/swagger/index.html)

---

## 📌 Features

- User Registration & Login (with JWT)
- Password Hashing and Identity Lockout
- JWT-Based Authentication
- Add / View / Retrieve / Delete Contacts
- Entity Framework Core with SQL Server
- Input Validation (Name, Email, Password)
- Swagger UI for API Testing

---

## 🛠️ Technologies Used

- ASP.NET Core 8 Web API
- Entity Framework Core
- SQL Server
- JWT Authentication
- Identity Framework
- Swagger / postman

---

## 📂 Project Structure

```
📁 Contact management
├── 📁 Contexts                # EF DbContext
├── 📁 Controllers            # API endpoints
├── 📁 Dtos                   # Request/Response models
├── 📁 Migrations             # EF Migrations
├── 📁 Models                 # ApplicationUser, Contact, etc.
├── 📁 Repository             # Interfaces + Implementations
├── 📁 Services               # Token and business logic
├── appsettings.json         # JWT & DB Configuration
├── Program.cs               # Main app entrypoint
```

---

## 🔐 Authentication

- JWT-based authentication is implemented.
- Each user has access only to their own contacts.
- Passwords are hashed using ASP.NET Identity.
- Lockout policy after 5 failed attempts for 2 hours.

---

## 📮 Available Endpoints

### 🔑 Auth

| Method | Route                 | Description       |
|--------|-----------------------|-------------------|
| POST   | `/api/Auth/register`  | Register new user |
| POST   | `/api/Auth/login`     | Login and get JWT |

### 📇 Contact

| Method | Route                    | Description               |
|--------|--------------------------|---------------------------|
| POST   | `/api/Contact`           | Add new contact           |
| GET    | `/api/Contact`           | List all contacts         |
| GET    | `/api/Contact/{id}`      | Get contact by ID         |
| DELETE | `/api/Contact/{id}`      | Delete contact by ID      |
| GET    | `/api/Contact/GetContactsSorting` | List contacts with sorting and pagination |


---

## 🛡️ JWT Setup (in `Program.cs`)

- JWT key is loaded from `appsettings.json`
- Used to validate and secure each request
- Swagger supports adding a Bearer token for testing

```json
// appsettings.json
"Jwt": {
  "Key": "your_super_secret_key"
}
```

---

## 🧪 Swagger Setup

- Enabled by default
- Visit: [`/swagger/index.html`](http://contact1.runasp.net/swagger/index.html)
- Click **Authorize** and enter: `Bearer <token>`

---

## 🧱 Database & Migrations

- EF Core is used with SQL Server
- Create DB using the following:

```bash
dotnet ef migrations add Init
dotnet ef database update
```

> Make sure to set the correct connection string in `appsettings.json`

---

## ▶️ Running the Project Locally

1. Clone the repository
2. Update `appsettings.json` with your DB and JWT key
3. Run the following:

```bash
dotnet restore
dotnet ef database update
dotnet run
```

4. Open browser: `https://localhost:7189/swagger/index.html`

---

## 📬 Contact Fields

```json
{
  "firstName": "John",
  "lastName": "Doe",
  "email": "john@example.com",
  "phoneNumber": "+201000000000",
  "birthDate": "1990-01-01"
}
```

---

## 🙋‍♂️ Author

Made with ❤️ by [SALEH SHERIF]  
Deployed on [MonsterHost](https://www.monsterasp.net/)
