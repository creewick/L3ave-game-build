.PHONY: *

build-all: build-win-x64 build-osx-x64 build-osx-arm64 build-linux-x64

build-win-x64:
	dotnet build \
		--configuration Release \
		--os win \
		--arch x64 \
		--output assets/win-x64/ \
		--self-contained

	cd assets/win-x64/ && zip -r ../L3ave-win-x64.zip .

build-osx-x64:
	dotnet build \
		--configuration Release \
		--os osx \
		--arch x64 \
		--output assets/osx-x64/ \
		--self-contained

	cd assets/osx-x64/ && zip -r ../L3ave-osx-x64.zip .

build-osx-arm64:
	dotnet build \
		--configuration Release \
		--os osx \
		--arch arm64 \
		--output assets/osx-arm64/ \
		--self-contained

	cd assets/osx-arm64/ && zip -r ../L3ave-osx-arm64.zip .

build-linux-x64:
	dotnet build \
		--configuration Release \
		--os linux \
		--arch x64 \
		--output assets/linux-x64/ \
		--self-contained

	cd assets/linux-x64/ && zip -r ../L3ave-linux-x64.zip .
