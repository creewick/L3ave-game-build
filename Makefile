.PHONY: *

build-all: build build-wasm

clean:
	rm -rf assets

build: clean
	mkdir assets

	docker build \
		--file Dockerfile \
		--tag l3ave-build \
		.

	docker run \
		--rm \
		--volume ${PWD}/assets:/tmp/assets \
		l3ave-build \
		cp -r /L3ave/assets /tmp/

clean-wasm:
	rm -rf assets-wasm

build-wasm: clean-wasm
	mkdir assets-wasm

	docker build \
		--file Dockerfile-wasm \
		--tag l3ave-build-wasm \
		.

	docker run \
		--rm \
		--volume ${PWD}/assets-wasm:/tmp/assets \
		l3ave-build-wasm \
		cp -r /L3ave/assets /tmp
