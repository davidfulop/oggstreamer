FROM microsoft/aspnetcore-build
WORKDIR /source

COPY tests/Oggstreamer.UnitTests/Oggstreamer.UnitTests.csproj ./tests/Oggstreamer.UnitTests/
COPY src/Oggstreamer/Oggstreamer.csproj ./src/Oggstreamer/
RUN dotnet restore tests/Oggstreamer.UnitTests/Oggstreamer.UnitTests.csproj

COPY ./tests/Oggstreamer.UnitTests ./tests/Oggstreamer.UnitTests
COPY ./src/Oggstreamer ./src/Oggstreamer

RUN dotnet build tests/Oggstreamer.UnitTests/Oggstreamer.UnitTests.csproj

# run tests
ENTRYPOINT ["dotnet", "test", "tests/Oggstreamer.UnitTests/Oggstreamer.UnitTests.csproj", "--no-build"]
