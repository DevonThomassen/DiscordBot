Commands:

```
dotnet ef migrations add Name -c DatabaseContext -o .\Database\Migrations --startup-project ..\ArcadeVault.Web
dotnet ef database update -c DatabaseContext --startup-project ..\ArcadeVault.Web
dotnet ef migrations remove -c DatabaseContext --startup-project ..\ArcadeVault.Web
```