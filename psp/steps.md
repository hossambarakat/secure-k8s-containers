- Delete all PSPs 

```bash
k delete psp --all
```
- Verify that PodSecurityPolicy admission controller enabled

```
minikube ssh
sudo cat /etc/kubernetes/manifests/kube-apiserver.yaml | grep admission
```

- Enable PSP admission controller
```
rerun minikube
```

- After enabling PSP, create POD and verify that it will not be recreated because of missing PSP

```
kubectl apply -f pod.yaml
```

The server will return the following message because there is no PSP matches that can be used

```
Error from server (Forbidden): error when creating "pod.yaml": pods "psp-nginx" is forbidden: no providers available to validate pod request
```

- Create example PSP that is permissive to run privileged containers, Create Cluster Role the give `use` access to the PSP, Create cluster role binding that glue the role and PSP, all system service accounts
```
kubectl apply -f example-psp.yaml
```

The request will fail because the PSP doesn't enable privileged containers
```
Error from server (Forbidden): error when creating "pod.yaml": pods "psp-nginx" is forbidden: unable to validate against any pod security policy: [spec.containers[0].securityContext.privileged: Invalid value: true: Privileged containers are not allowed]
```

- create the pod should fail because the example psp doesn't enable privileged container

```
kubectl apply -f pod.yaml
```

- Modify the psp to enable privileged container and run the pod again, it should work without issue

