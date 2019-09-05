- Simulate that RBAC disabled by granting admin access to the default service account

```
kubectl create clusterrolebinding default-admin-binding --clusterrole=admin --serviceaccount=default:default
```

- Deploy web app using deployment object
```
k apply -f pod.yaml
```

- Run exec -it to the deployed pod
```
k exec -it nginx bash
```

- Install curl or use image with curl installed
```
apt-get update && apt-get install -y curl
```

- Navigate to the following directory to verify that the service account token exist
```
cat /var/run/secrets/kubernetes.io/serviceaccount/token
```

- Use the token to query pods from the API server
```
KUBE_TOKEN=$(cat /var/run/secrets/kubernetes.io/serviceaccount/token)
curl -sSk -H "Authorization: Bearer $KUBE_TOKEN" \
      https://$KUBERNETES_SERVICE_HOST:$KUBERNETES_PORT_443_TCP_PORT/api/v1/namespaces/default/pods/$HOSTNAME
```
OR

```
TOKEN=$(cat /var/run/secrets/kubernetes.io/serviceaccount/token)
curl -LO https://storage.googleapis.com/kubernetes-release/release/$(curl -s https://storage.googleapis.com/kubernetes-release/release/stable.txt)/bin/linux/amd64/kubectl
chmod +x kubectl
./kubectl get pods
./kubectl auth can-i create pods

```

```
k auth can-i get pods -n webapp-namespace --as system:serviceaccount:webapp-namespace:webapp-service-account
```
- RBAC to riscue: Enable RBAC again by remove the cluster role binding
```
kubectl delete clusterrolebinding default-admin-binding
```

- Create Service Account & update the pod to use the new service account
```
k apply -f service-account.yaml
```

- Verify that token can't be used to get the running pods

## Enable service account to access the pods
- Create Role
- Create Role Binding

- Update the POD template section with the serviceAccountName

- Reconnect to the pod and verify that get pods will be disabled



https://kubernetes.io/docs/tasks/configure-pod-container/configure-service-account/
