FROM mcr.microsoft.com/devcontainers/dotnet:1-8.0-bookworm

RUN apt-get update && export DEBIAN_FRONTEND=noninteractive \
     && apt-get install -y mariadb-client git unzip \
     && apt-get clean -y && rm -rf /var/lib/apt/lists/*

# Installera EF Core CLI Tools
RUN dotnet tool install --global dotnet-ef

RUN echo 'export PATH="$PATH:/root/.dotnet/tools"' >> ~/.bashrc
ENV PATH="$PATH:/root/.dotnet/tools"


COPY . .
