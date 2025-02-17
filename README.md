## L3ave

### How to play?

Go to the [releases](https://github.com/creewick/L3ave-game-build/releases) page and download build for your platform.

> Note: if you use Windows 11 on ARM or MacOS on ARM (Apple M1) you should download x64 version, since Windows and MacOS both support x64 emulation (see [Windows on Arm](https://learn.microsoft.com/en-us/windows/arm/apps-on-arm-x86-emulation) and [Rosetta](https://developer.apple.com/documentation/apple-silicon/about-the-rosetta-translation-environment)).
> 
> If you use Linux on ARM then the build is not ready yet.

### How to build?

See [Makefile](Makefile).

- `make build` \
Build `win-x64`, `linux-x64` and `osx-x64` versions.

- `make build-wasm` \
Build `wasm` version.

### How to deploy `wasm` version?

Just download the build, extract `wwwroot` directory and serve it.

Example:

```
python3 -m http.server -d ./wwwroot 6767
```

Then open `http://localhost:6767/` in your browser.

