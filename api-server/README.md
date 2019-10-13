# Call Insecure API Server

Follow the below steps to test calling insecure API Server:
- Simulate that RBAC disabled by granting admin access to the default service account

```
kubectl create clusterrolebinding default-admin-binding --clusterrole=admin --serviceaccount=default:default
```

- Deploy web app pod
```
kubectl apply -f pod.yaml
```

- Access the webapp pod shell
```
kubectl exec -it webapp sh
```

- Verify that the service account token exist
```
cat /var/run/secrets/kubernetes.io/serviceaccount/token
```

## Access API Server using Curl

- Install curl to the running container

```
apk add curl
```

** Use the token to query pods from the API server

```
KUBE_TOKEN=$(cat /var/run/secrets/kubernetes.io/serviceaccount/token)
curl -sSk -H "Authorization: Bearer $KUBE_TOKEN" \
      https://$KUBERNETES_SERVICE_HOST:$KUBERNETES_PORT_443_TCP_PORT/api/v1/namespaces/default/pods/$HOSTNAME
```

## OR Access API Server using KubeCtl

The following command will work because I have installed KubeCtl in the image which is **NOT** recommended for production

```
kubectl get pods
kubectl auth can-i create pods
```

- Enable RBAC again by removing the cluster role binding
```
kubectl delete clusterrolebinding default-admin-binding
```

# Enable service account to access the pods
- Create Service Account, Role and Role Binding
```
kubectl apply -f rbac.yaml
```

- Delete the currently running pod
```
kubectl delete pod webapp
```

- Update the Pod template section with the serviceAccountName

```
serviceAccountName: webapp-service-account
```

- Deploy web app pod with service account configured
```
kubectl apply -f pod.yaml
```

- Access the webapp pod shell
```
kubectl exec -it webapp sh
```

- Verify that token can get the running pods but can get pods but can NOT create pods

```
kubectl auth can-i get pods
kubectl auth can-i create pods
```