.PHONY: *

all: build build-wasm

clean:
	rm -rf assets

build: clean
	docker build \
		--file Dockerfile \
		--tag l3ave-build \
		.

	docker run \
		--rm \
		--volume ${PWD}/L3ave:/tmp/L3ave \
		l3ave-build

	mkdir assets \
		&& cp L3ave/assets/*.zip assets/

clean-wasm:
	# rm -rf assets-wasm

build-wasm: clean-wasm
	# pass
