FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY bin/Release/netcoreapp2.1/publish .
EXPOSE 5000
ENTRYPOINT ["dotnet", "ChibiCmsWeb.dll"]