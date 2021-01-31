./openssl.exe req -x509 -newkey rsa:4096 -keyout nginxTls.key.pem -out nginxTls.cert.pem -days 365 -nodes -subj "/C=DE/ST=DE/L=DE/O=MooMed/OU=RD/CN=moomedlocal.com" -addext "subjectAltName=DNS:moomedlocal.com"
./openssl.exe pkcs12 -inkey nginxTls.key.pem -in nginxTls.cert.pem -export -out nginxTls.pfx
./openssl.exe x509 -outform der -in nginxTls.cert.pem -out nginxTls.crt