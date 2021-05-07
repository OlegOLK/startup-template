FROM timberio/vector:latest-debian

COPY vector.prom.toml /etc/vector/vector.prom.toml
COPY vector.loki.toml /etc/vector/vector.loki.toml
COPY vector.sh /usr/bin/vector.sh
RUN chmod 755 /usr/bin/vector.sh
ENTRYPOINT ["/usr/bin/vector.sh"]