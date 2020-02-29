#!/usr/bin/env bash

# Publish Server
echo "Running dotnet publish for server"
dotnet publish \
  -o Publish/esusshots-server-linux-x64 \
  -r linux-x64 \
  -c Release \
  /p:PublishSingleFile=true \
  /p:PublishTrimmed=true \
  EstusShots.Server/EstusShots.Server.csproj

# Publish GTK Client
echo "Running dotnet publish for GTK client"
dotnet publish \
  -o Publish/esusshots-gtk-linux-x64 \
  -r linux-x64 \
  -c Release \
  /p:PublishSingleFile=true \
  /p:PublishTrimmed=true \
  EstusShots.Gtk/EstusShots.Gtk.csproj