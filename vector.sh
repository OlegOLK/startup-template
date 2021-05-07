#! /bin/sh

file=/etc/vector/vector.toml

sed -i -e "s/\(address=\).*/\1\"0.0.0.0:$PORT\"/" /etc/vector/vector.toml
echo $PORT
vector -c /etc/vector/vector.toml


