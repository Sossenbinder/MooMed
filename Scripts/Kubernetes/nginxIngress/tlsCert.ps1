kubectl delete secret nginxtlscert
kubectl create secret tls nginxtlscert --key "./Certs/Local/nginxTls.key.pem" --cert "./Certs/Local/nginxTls.cert.pem"