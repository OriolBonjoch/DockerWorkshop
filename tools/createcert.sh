#!/bin/sh
cd /root/.aspnet/https
openssl req -x509 -newkey rsa:2048 -nodes -keyout aspnetapi.key -out aspnetapi.crt -subj "/CN=pokemonapi"
openssl pkcs12 -export -out aspnetapi.pfx -inkey aspnetapi.key -in aspnetapi.crt -passout pass:password
export sslsettings__thumbprint=`openssl x509 -in aspnetapi.crt -outform der | sha1sum | cut -d " " -f1`



export sslsettings__thumbprint=e7b4d75f8f570761bf5b6e5e761e9bfec3b9a728
