FROM microsoft/aspnetcore-build
WORKDIR /source

COPY tests/Oggstreamer.IntegrationTests/Oggstreamer.IntegrationTests.csproj ./tests/Oggstreamer.IntegrationTests/
COPY src/Oggstreamer/Oggstreamer.csproj ./src/Oggstreamer/
RUN dotnet restore tests/Oggstreamer.IntegrationTests/Oggstreamer.IntegrationTests.csproj

COPY ./tests/Oggstreamer.IntegrationTests ./tests/Oggstreamer.IntegrationTests
COPY ./src/Oggstreamer ./src/Oggstreamer
COPY ./tests/Oggstreamer.IntegrationTests/assets/test01.ogg ../tests/Oggstreamer.IntegrationTests/assets/test01.ogg

RUN dotnet build tests/Oggstreamer.IntegrationTests/Oggstreamer.IntegrationTests.csproj

# run tests
ENTRYPOINT ["dotnet", "test", "tests/Oggstreamer.IntegrationTests/Oggstreamer.IntegrationTests.csproj", "--no-build"]
