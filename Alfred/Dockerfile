FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

COPY Alfred.sln ./
COPY . ./
RUN dotnet restore Alfred.sln


RUN dotnet build -c Release
ENTRYPOINT [ "dotnet", "test" ]