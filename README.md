
# 🔐 Authentication and Authorization with ASP.NET Core Identity Inside Out

This project provides a deep-dive into **Authentication** and **Authorization** using **ASP.NET Core Identity**. It covers not only the out-of-the-box features of Identity but also custom implementations, configurations, and advanced techniques for building secure web applications.

---

## 📘 Table of Contents

- [Introduction](#introduction)
- [Project Structure](#project-structure)
- [Features Covered](#features-covered)
- [Getting Started](#getting-started)
- [Authentication Workflow](#authentication-workflow)
- [Authorization Techniques](#authorization-techniques)
- [Customization Options](#customization-options)
- [Security Best Practices](#security-best-practices)
- [Useful Commands](#useful-commands)
- [Resources](#resources)

---

## 📌 Introduction

ASP.NET Core Identity is a membership system that adds login functionality to ASP.NET Core applications. This project takes you **inside out**—from basic setup to customizations like user claims, roles, policies, and token-based auth.

Whether you're building a secure internal dashboard or a public-facing web app, this guide will help you manage authentication and authorization robustly and flexibly.

---

## 🧱 Project Structure

```bash
/AuthenticationInsideOut
│
├── /Areas/Identity/         # Razor Pages for login, register, etc.
├── /Data/                   # ApplicationDbContext and Identity models
├── /Models/                 # Custom user models and view models
├── /Services/               # Token generation, email sender, etc.
├── /Pages/                  # Razor Pages using Identity
├── /Controllers/            # API endpoints with [Authorize]
└── Startup.cs / Program.cs  # Identity configuration and middleware
````

---

## ✨ Features Covered

### ✔️ Authentication

* ASP.NET Core Identity setup
* User registration, login, logout
* Email confirmation and password reset
* External providers (Google, Facebook, etc.)
* Two-Factor Authentication (2FA)

### ✔️ Authorization

* Role-based access control
* Claims-based policies
* Custom authorization handlers
* \[Authorize], \[AllowAnonymous], \[Authorize(Roles = "...")]

### ✔️ Security Enhancements

* Token expiration & refresh tokens
* Password complexity rules
* Lockout after failed attempts
* Cookie configuration & sliding expiration

---

## 🚀 Getting Started

### Prerequisites

* [.NET 6 or .NET 7 SDK](https://dotnet.microsoft.com/download)
* Visual Studio 2022+ or VS Code
* SQL Server or SQLite

### Setup Instructions

1. **Clone the Repository**

   ```bash
   git clone https://github.com/your-username/AuthenticationInsideOut.git
   cd AuthenticationInsideOut
   ```

2. **Apply Migrations & Run**

   ```bash
   dotnet ef database update
   dotnet run
   ```

3. **Access the App**
   Navigate to `https://localhost:5001` in your browser.

---

## 🔁 Authentication Workflow

```text
[User] → [Login Page] → [Identity SignInManager]
      → [Check Credentials] → [Generate Auth Cookie]
      → [Set Auth Cookie in Browser]
```

* Authenticated users are tracked via secure cookies.
* ASP.NET Core handles cookie validation automatically.

---

## 🛡️ Authorization Techniques

### Role-Based

```csharp
[Authorize(Roles = "Admin")]
```

### Policy-Based

```csharp
services.AddAuthorization(options =>
{
    options.AddPolicy("Over18Only", policy =>
        policy.RequireClaim("Age", "18"));
});
```

### Custom Handlers

```csharp
public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(...) { ... }
}
```

---

## 🛠️ Customization Options

* **Extend IdentityUser** to include extra properties like `FirstName`, `Department`, etc.
* **Override Identity UI** by scaffolding pages.
* **Customize token lifespan** via `TokenOptions`.
* **Configure cookies**:

  ```csharp
  services.ConfigureApplicationCookie(options =>
  {
      options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
      options.SlidingExpiration = true;
  });
  ```

---

## 🧯 Security Best Practices

* Use HTTPS (enforced in `Program.cs`)
* Use `IEmailSender` to verify users' emails before login
* Protect sensitive routes using `[Authorize]`
* Implement account lockout
* Store secrets securely (e.g., Azure Key Vault, User Secrets)
* Keep Identity packages up to date

---

## 🛠️ Useful Commands

```bash
# Add migration
dotnet ef migrations add InitialCreate

# Update database
dotnet ef database update

# Scaffold Identity UI (for customization)
dotnet aspnet-codegenerator identity -dc ApplicationDbContext
```

---

## 📚 Resources

* [ASP.NET Core Identity Docs](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity)
* [Microsoft Identity Platform](https://learn.microsoft.com/en-us/azure/active-directory/develop/)
* [OWASP Cheat Sheet Series](https://cheatsheetseries.owasp.org/)
* [Jwt.io Debugger](https://jwt.io/)

---

## 📬 Contact / Contributions

Have questions or suggestions? Open an issue or submit a PR.

---

## 🔖 License

This project is licensed under the [MIT License](LICENSE).

```

---

Let me know if you want this customized for:
- **Blazor Server / WASM**
- **API-first apps with JWT tokens**
- **Minimal APIs**
- Or a **step-by-step tutorial README**.

I can also scaffold a real example project for you.
```
