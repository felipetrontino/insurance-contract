dotnet test /p:CollectCoverage=true `
/p:CoverletOutputFormat=opencover `
/p:CoverletOutput=.\results\coverage\ `

& "$env:USERPROFILE/.nuget/packages/reportgenerator/4.4.6/tools/netcoreapp3.0/ReportGenerator.exe" `
-reports:"./results/coverage/coverage.opencover.xml" `
-targetdir:"./results/coverage/Reports" `
-reportTypes:htmlInline

Start-Process "./results/coverage/reports/index.htm"