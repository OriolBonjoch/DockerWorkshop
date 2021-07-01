#!/bin/sh
dotnet PokemonSeeder.dll
echo "Seeding is done, creating healtcheck file"
touch /tmp/healthy
echo "Keep container healthy"
tail -f /dev/null