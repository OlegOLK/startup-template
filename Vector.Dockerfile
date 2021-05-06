FROM timberio/vector:latest-alpine

WORKDIR /app
COPY vector.config.toml /etc/vector/vector.toml

COPY . /app

# CMD -d -v vector.config.toml:/etc/vector/vector.toml:ro -p 80


# VOLUME [ "~/vector.config.toml:/etc/vector/vector.toml:ro" ]
# RUN -v ~/vector.toml:/etc/vector/vector.toml:ro -p 80  timberio/vector:latest-alpine