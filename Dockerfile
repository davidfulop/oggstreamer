FROM microsoft/aspnetcore-build:2.0 AS builder
WORKDIR /source
ARG configuration=Release

COPY src/Oggstreamer/Oggstreamer.csproj .
RUN dotnet restore

COPY src/Oggstreamer appsettings* ./
RUN dotnet publish --output /app/ --configuration $configuration

# Construct Stage
FROM microsoft/aspnetcore
WORKDIR /app
COPY --from=builder /app .
COPY ./src/Oggstreamer/assets/test01.flac ./assets/test01.flac

RUN apt-get update && apt-get install -y apt-transport-https
RUN apt-get install -y ffmpeg

ENTRYPOINT ["dotnet", "Oggstreamer.dll"]