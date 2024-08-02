@echo off
docker build -t es .
docker run -d -p 5000:8080 -p 5001:8081 --name es-container -v image-es:/app/wwwroot es
PAUSE