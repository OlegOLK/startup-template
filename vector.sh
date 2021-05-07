#! /bin/sh

sed -i -e "s/\(address=\).*/\1\"0.0.0.0:$PORT\"/" \
-e "s/\(auth.password=\).*/\1\"$LOKI_PASSWORD\"/" \
-e "s/\(auth.user=\).*/\1\"$LOKI_USER\"/" /etc/vector/vector.loki.toml

sed -i -e "s/\(auth.password=\).*/\1\"$PROM_PASSWORD\"/" \
-e "s/\(auth.user=\).*/\1\"$PROM_USER\"/" /etc/vector/vector.prom.toml


cat /etc/vector/vector.prom.toml

vector -c /etc/vector/vector.prom.toml

#-c /etc/vector/vector.loki.toml


