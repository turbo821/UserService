# UserService

### Как запустить

1. Изначально используется in memory базы данных

Для использования postgres в Program.cs нужно раскомментировать и закомментировать, чтобы было так на:
```C#
// builder.Services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("usersdb")); // It is database in memory
// OR
string connection = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection)); // It is database in postgresql 
```

И далее создать бд, настроить в appsettings.json подключение и провести миграцию:
`dotnet ef database update`

2. Данные админа (прописаны в appsettings.json):
Логин: `admin`
Пароль: `admin123`

5. Запустить проект в Visual Studio, после чего swagger будет доступен по адресу `https://localhost:7216/swagger`

6. Для выполнения действий с UserController нужно войти используя AuthController (ввести данные админа), после чего в куках будет храниться jwt токен
