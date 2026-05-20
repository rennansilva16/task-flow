#!/bin/bash

echo "Matando processos antigos..."
killall dotnet 2>/dev/null

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
echo "Subindo Client..."
dotnet run &

echo "Aguardando Client subir..."

until curl -k -s $CLIENT_URL > /dev/null; do
  sleep 1
done

echo "Client pronto! Abrindo navegador..."

open $CLIENT_URL

wait