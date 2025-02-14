.PHONY: *

build-all: build-win-x64 build-osx-x64 build-linux-x64

clean-win-x64:
	rm -rf assets/win-x64/

build-win-x64: clean-win-x64
	dotnet publish \
		--configuration Release \
		--os win \
		--arch x64 \
		--output assets/win-x64/L3ave-game/ \
		--self-contained

	cd assets/win-x64/ && zip -r ../L3ave-win-x64.zip L3ave-game/

clean-osx-x64:
	rm -rf assets/osx-x64/

build-osx-x64: clean-osx-x64
	dotnet publish \
		--configuration Release \
		--os osx \
		--arch x64 \
		--output assets/osx-x64/L3ave-game/ \
		--self-contained

	cd assets/osx-x64/L3ave-game/ \
		&& cp libFAudio.0.dylib libFAudio.dylib \
		&& cp libFNA3D.0.dylib libFNA3D.dylib \
		&& cp libSDL2-2.0.0.dylib libSDL2.dylib

	cd assets/osx-x64/ && zip -r ../L3ave-osx-x64.zip L3ave-game/

clean-linux-x64:
	rm -rf assets/linux-x64/

build-linux-x64: clean-linux-x64
	dotnet publish \
		--configuration Release \
		--os linux \
		--arch x64 \
		--output assets/linux-x64/L3ave-game/ \
		--self-contained

	cd assets/linux-x64/L3ave-game/ \
		&& cp libSDL2-2.0.so.0 libSDL2.so.0

	cd assets/linux-x64/ && zip -r ../L3ave-linux-x64.zip L3ave-game/
