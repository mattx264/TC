cd TC.DataAccess
dotnet ef --startup-project ../TC.WebService database drop -f
dotnet ef --startup-project ../TC.WebService database update Init -c TestingCenterDbContext
cd..