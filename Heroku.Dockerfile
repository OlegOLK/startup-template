FROM timberio/vector:latest-alpine

# VOLUME [ "~/vector.config.toml:/etc/vector/vector.toml:ro" ]
RUN -d -v ~/vector.toml:/etc/vector/vector.toml:ro -p $PORT  timberio/vector:latest-alpine