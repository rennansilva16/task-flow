#!/bin/bash

echo "Liberando portas..."

kill -9 $(lsof -ti:7206) 2>/dev/null
kill -9 $(lsof -ti:7215) 2>/dev/null

API_URL="https://localhost:7215"
CLIENT_URL="https://localhost:7206"

echo "Subindo API..."
cd TaskFlowAPI
dotnet run &

echo "Aguardando API subir..."

until curl -k -s $API_URL > /dev/null; do
  sleep 1
done

echo "API pronta!"

cd ../TaskFlow.Web.Client

# 👉 controla o browser aqui
export DOTNET_WATCH_SUPPRESS_LAUNCH_BROWSER=1 

echo "Subindo Client..."
dotnet watch run &

echo "Aguardando Client subir..."

until curl -k -s $CLIENT_URL > /dev/null; do
  sleep 1
done

echo "Client pronto! Abrindo navegador..."

open $CLIENT_URL

wait