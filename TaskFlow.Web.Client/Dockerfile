# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ["TaskFlow.Web.Client/TaskFlow.Web.Client.csproj", "TaskFlow.Web.Client/"]
COPY ["TaskFlow.Shared/TaskFlow.Shared.csproj", "TaskFlow.Shared/"]
COPY ["TaskFlow.Domain/TaskFlow.Domain.csproj", "TaskFlow.Domain/"]

RUN dotnet restore "TaskFlow.Web.Client/TaskFlow.Web.Client.csproj"

COPY . .
RUN dotnet publish "TaskFlow.Web.Client/TaskFlow.Web.Client.csproj" -c Release -o /app/publish

# Runtime
FROM nginx:alpine

COPY --from=build /app/publish/wwwroot /usr/share/nginx/html

EXPOSE 80