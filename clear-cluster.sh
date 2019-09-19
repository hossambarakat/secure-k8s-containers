#!/usr/bin/env sh

kubectl delete pods --all
kubectl delete deployment --all
kubectl delete svc --all
kubectl delete networkpolicies --all
kubectl delete psp --all
kubectl apply -f ./psp/privileged-psp.yaml
clear