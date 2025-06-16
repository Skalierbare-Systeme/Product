# syntax=docker/dockerfile:1

# Set the name of the project's main DLL file
ARG PROJECT_NAME=postsPraktikum

################################################################################
# Build Stage
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build

# Copy all source code
COPY . /source
WORKDIR /source

# Define build arguments
ARG TARGETARCH
ARG PROJECT_NAME

# Publish the application, using the project name argument
# Note: This assumes your .csproj file is in a directory with the same name.
# e.g., source/postsPraktikum/postsPraktikum.csproj
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish "postsPraktikum.csproj" -a ${TARGETARCH/amd64/x64} --use-current-runtime --self-contained false -o /app
# If your .csproj is in the root, use this instead:
# RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
#    dotnet publish "${PROJECT_NAME}.csproj" -a ${TARGETARCH/amd64/x64} --use-current-runtime --self-contained false -o /app

################################################################################
# Final Stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS final
WORKDIR /app

# Define the project name argument again for the final stage
ARG PROJECT_NAME

# Copy the published application from the build stage
COPY --from=build /app .

# Switch to a non-privileged user
USER $APP_UID

# Use the project name argument to set the entrypoint
ENTRYPOINT ["dotnet", "postsPraktikum.dll"]
