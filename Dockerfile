FROM mcr.microsoft.com/dotnet/sdk:9.0

RUN apt update \
    && apt install -y make zip

# 1. Copy source code
COPY L3ave /L3ave

# 2. Build project
RUN cd /L3ave \
    && make
