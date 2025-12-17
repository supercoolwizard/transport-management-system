FROM mcr.microsoft.com/dotnet/sdk:10.0

RUN apt-get update && \
    apt-get install -y python3 python3-pip python3-venv && \
    rm -rf /var/lib/apt/lists/*

WORKDIR /transport_management_system

COPY Python/requirements.txt Python/requirements.txt

RUN python3 -m venv venv && \
    ./venv/bin/pip install --no-cache-dir -r Python/requirements.txt

COPY . . 

RUN dotnet publish -c Release -o out

ENV PATH="/transport_management_system/venv/bin:$PATH"

CMD ["dotnet", "out/transport_management_system.dll"]