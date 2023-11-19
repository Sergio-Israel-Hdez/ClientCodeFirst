# Etapa de restauración
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS restore
WORKDIR /src

# Copiar el archivo de proyecto y solución y restaurar dependencias
COPY ClientCodeFirst/*.csproj ClientCodeFirst/
COPY ClientCodeFirst/*.sln ClientCodeFirst/
RUN dotnet restore ClientCodeFirst/ClientCodeFirst.csproj

# Etapa de construcción
FROM restore AS build
WORKDIR /src

# Copiar el resto de los archivos y compilar la aplicación
COPY . .
RUN dotnet publish -c Release -o /app

# Etapa de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:7.0 
WORKDIR /app

# Exponer los puertos necesarios
EXPOSE 80
EXPOSE 5091

# Copiar la salida de la etapa de construcción a la etapa de ejecución
COPY --from=build /app .

# Punto de entrada para la aplicación
ENTRYPOINT ["dotnet", "ClientCodeFirst.dll"]
