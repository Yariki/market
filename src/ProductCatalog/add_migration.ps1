param([string]$name = 'Initial')

dotnet-ef migrations add $name -s API\Api.csproj -p Infrastructure\Infrastructure.csproj -o Persistence\Migrations