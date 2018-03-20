ifndef VERSION
$(warning VERSION not defined; falling back to latest)
VERSION=latest
endif

build_server:
	@docker build --network=host -t oggstreamer .
.PHONY: build_server

build_server_debug:
	@docker build --network=host -t oggstreamer --build-arg configuration=Debug .
.PHONY: build_server_debug

run_server: build_server
	@ASPNETCORE_ENVIRONMENT=local docker run -p 80:80 oggstreamer
.PHONY: run_server


# Tests
build_unit_tests:
	@docker build --network=host -t oggstreamer-unittests:$(VERSION) -f UnitTests.Dockerfile .
.PHONY: build_unit_tests

run_unit_tests: build_unit_tests
	@docker run -e ASPNETCORE_ENVIRONMENT=$(ASPNETCORE_ENVIRONMENT) -t oggstreamer-unittests:$(VERSION)
.PHONY: run_unit_tests


cleanup:
	@docker kill oggstreamer
.PHONY: cleanup
