FROM mcr.microsoft.com/dotnet/runtime:7.0
WORKDIR /app

COPY ./bin/Debug/net7.0/ /app

ENTRYPOINT ["dotnet", "Explode.dll"]