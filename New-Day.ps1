param (
    [Parameter()]
    [string]
    $Day = (Get-Date -Format 'dd')
)

$projectName = "day$Day"
$libName = "$projectName.solution"

dotnet new sln -o $projectName
Push-Location $projectName
dotnet new classlib -o $libName
dotnet sln add ".\$libName\$libName.csproj"
dotnet new xunit -o "$libName.Tests"
dotnet add ".\$libName.Tests\$libName.Tests.csproj" reference ".\$libName\$libName.csproj"
dotnet sln add ".\$libName.Tests\$libName.Tests.csproj"
Pop-Location
