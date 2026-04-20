# ContactListWebApp - krotka specyfikacja techniczna

## Opis klas i metod

### Program i konfiguracja
- `Program.Main(string[] args)`
  - dodaje konfiguracje bazy przez `AddDatabase()`,
  - wykonuje migracje i seed danych przez `MigrateAndSeedDatabase()`.

### Warstwa danych
- `ContactDbContext`
  - udostepnia tabele: `Contacts`, `ContactCategories`, `ContactSubcategories`,
  - w `OnModelCreating(...)` ustawia unikalny indeks na `Email` i seeduje slowniki kategorii.
- `DataExtensions`
  - `AddDatabase(...)`: rejestruje `DbContext` z SQL Server,
  - `MigrateAndSeedDatabase(...)`: migracje + seed danych.
- `DataSeeder.SeedSampleData(...)`
  - dodaje przykladowe kontakty przy pierwszym uruchomieniu.

### API (kontrolery)
- `AuthController`
  - `Login(...)` (`POST /api/auth/login`) - logowanie i ustawienie cookie,
  - `Logout()` (`POST /api/auth/logout`) - wylogowanie,
  - `Me()` (`GET /api/auth/me`) - dane zalogowanego uzytkownika.
- `CategoryController`
  - `GetCategories()` (`GET /api/category`) - lista kategorii z podkategoriami.
- `ContactController`
  - `GetAllContacts()` (`GET /api/contact`) - lista kontaktow,
  - `GetContactDetails(Guid id)` (`GET /api/contact/{id}`) - szczegoly kontaktu,
  - `CreateContact(CreateContactDto dto)` (`POST /api/contact`) - dodanie kontaktu,
  - `UpdateContact(Guid id, UpdateContactDto dto)` (`PATCH /api/contact/{id}`) - edycja,
  - `DeleteContact(Guid id)` (`DELETE /api/contact/{id}`) - usuniecie.

### Logika biznesowa i mapowanie
- `IContactService` - kontrakt operacji CRUD dla kontaktow.
- `ContactService` - implementacja logiki: pobieranie, tworzenie, aktualizacja, usuwanie.
- `ContactMapper`
  - mapowanie encji <-> DTO,
  - metody: `mapToContactDto`, `mapToContactDetailsDto`, `mapToContactEntity`, `updateContactEntity`.

### Modele
- Encje: `Contact`, `ContactCategory`, `ContactSubcategory`.
- DTO: `LoginDto`, `ContactDto`, `ContactDetailsDto`, `CreateContactDto`, `UpdateContactDto`, `CategoryDto`, `SubcategoryDto`.

## Wykorzystane biblioteki

Pakiety NuGet backendu:
- `Microsoft.AspNetCore.OpenApi` (10.0.5)
- `Microsoft.EntityFrameworkCore.SqlServer` (10.0.6)
- `Microsoft.EntityFrameworkCore.Design` (10.0.6)

Wykorzystane namespace frameworkowe:
- `Microsoft.AspNetCore.Authentication.Cookies`
- `Microsoft.AspNetCore.Authorization`
- `Microsoft.AspNetCore.Mvc`

## Uruchomienie z Docker Compose

W katalogu glownym projektu uruchom:

```bash
docker compose up --build -d
```

Po uruchomieniu uslug:
- Frontend: `http://localhost:4200`
- Backend API: `http://localhost:8080`
- SQL Server: `localhost:1433`

```Przykładowe dane do logowania:
- "email": jan.kowalski@example.com
- "password": SecurePass123!+```