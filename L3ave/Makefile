.PHONY: *

build-all: build-win-x64 build-osx-x64 build-osx-arm64 build-linux-x64

clean-win-x64:
	rm -rf assets/win-x64/

build-win-x64: clean-win-x64
	dotnet publish \
		--configuration Release \
		--os win \
		--arch x64 \
		--output assets/win-x64/ \
		--self-contained

	cd assets/win-x64/ && zip -r ../L3ave-win-x64.zip .

clean-osx-x64:
	rm -rf assets/osx-x64/

build-osx-x64: clean-osx-x64
	dotnet publish \
		--configuration Release \
		--os osx \
		--arch x64 \
		--output assets/osx-x64/ \
		--self-contained

	mv assets/osx-x64/libFAudio.0.dylib assets/osx-x64/libFAudio.dylib
	mv assets/osx-x64/libFNA3D.0.dylib assets/osx-x64/libFNA3D.dylib
	mv assets/osx-x64/libSDL2-2.0.0.dylib assets/osx-x64/libSDL2.dylib

	cd assets/osx-x64/ && zip -r ../L3ave-osx-x64.zip .

clean-osx-arm64:
	rm -rf assets/osx-arm64/

build-osx-arm64: clean-osx-arm64
	dotnet publish \
		--configuration Release \
		--os osx \
		--arch arm64 \
		--output assets/osx-arm64/ \
		--self-contained

	mv assets/osx-arm64/libFAudio.0.dylib assets/osx-arm64/libFAudio.dylib
	mv assets/osx-arm64/libFNA3D.0.dylib assets/osx-arm64/libFNA3D.dylib
	mv assets/osx-arm64/libSDL2-2.0.0.dylib assets/osx-arm64/libSDL2.dylib

	cd assets/osx-arm64/ && zip -r ../L3ave-osx-arm64.zip .

clean-linux-x64:
	rm -rf assets/linux-x64/

build-linux-x64: clean-linux-x64
	dotnet publish \
		--configuration Release \
		--os linux \
		--arch x64 \
		--output assets/linux-x64/ \
		--self-contained

	mv assets/linux-x64/libSDL2-2.0.so.0 assets/linux-x64/libSDL2.so.0

	cd assets/linux-x64/ && zip -r ../L3ave-linux-x64.zip .
