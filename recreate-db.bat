cd TC.DataAccess
del /f /q Migrations
dotnet ef --startup-project ../TC.WebService dotnet ef database drop -f
dotnet ef --startup-project ../TC.WebService migrations add Init -c TestingCenterDbContext
dotnet ef --startup-project ../TC.WebService database update Init -c TestingCenterDbContext
cd..