FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
FROM mcr.microsoft.com/dotnet/core/sdk:3.1


COPY /bin/Release/netcoreapp3.1/publish .

ENV ASPNETCORE_ENVIRONMENT Production

EXPOSE 5000
WORKDIR /NewsPortal
ENTRYPOINT ["dotnet", "NewsPortal.dll", "--urls=https://0.0.0.0.5000"]


