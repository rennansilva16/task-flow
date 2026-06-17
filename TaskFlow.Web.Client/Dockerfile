# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY . .

RUN dotnet restore "TaskFlow.Web.Client/TaskFlow.Web.Client.csproj"
RUN dotnet publish "TaskFlow.Web.Client/TaskFlow.Web.Client.csproj" -c Release -o /app/publish

# Runtime
FROM nginx:alpine

COPY --from=build /app/publish/wwwroot /usr/share/nginx/html

EXPOSE 80