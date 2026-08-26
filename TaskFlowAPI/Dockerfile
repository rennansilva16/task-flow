# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ["TaskFlowAPI/TaskFlowAPI.csproj", "TaskFlowAPI/"]
COPY ["TaskFlow.Application/TaskFlow.Application.csproj", "TaskFlow.Application/"]
COPY ["TaskFlow.Domain/TaskFlow.Domain.csproj", "TaskFlow.Domain/"]
COPY ["TaskFlow.Infrastructure/TaskFlow.Infrastructure.csproj", "TaskFlow.Infrastructure/"]
COPY ["TaskFlow.Shared/TaskFlow.Shared.csproj", "TaskFlow.Shared/"]

RUN dotnet restore "TaskFlowAPI/TaskFlowAPI.csproj"

COPY . .

RUN dotnet publish "TaskFlowAPI/TaskFlowAPI.csproj" -c Release -o /app/publish

# Etapa final
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "TaskFlowAPI.dll"]