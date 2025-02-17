FROM mcr.microsoft.com/dotnet/sdk:9.0

RUN apt update \
    && apt install -y make zip

ENTRYPOINT ["bash", "-c", "cd /tmp/L3ave && make"]
