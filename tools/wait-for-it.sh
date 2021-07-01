#!/bin/sh
# wait-for-it.sh
set -e
host="$1"
shift
cmd="$@"
while [[ "$(curl -s -o /dev/null -w ''%{http_code}'' ''$host'')" != "200" ]]
do
  >&2 echo "Web API is unavailable - sleeping ($(curl -s -o /dev/null -w ''%{http_code}'' ''$host''))"
  sleep 5
done
>&2 echo "Web API is up - executing command"
exec $cmd