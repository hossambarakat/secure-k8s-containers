#!/usr/bin/env sh

kubectl delete -f ./api-server --ignore-not-found=true
kubectl delete -f ./network-policy --ignore-not-found=true
kubectl delete -f ./psp --ignore-not-found=true
kubectl delete -f ./security-context --ignore-not-found=true
kubectl apply -f ./psp/privileged-psp.yaml
clear
