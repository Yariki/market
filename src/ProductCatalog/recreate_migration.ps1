param([string]$name = 'Initial')

dotnet ef migrations remove -f -p Infrastructure\Infrastructure.csproj -s API\Api.csproj
dotnet ef migrations add $name -p Infrastructure\Infrastructure.csproj -s API\Api.csproj  -o Persistence\Migrations