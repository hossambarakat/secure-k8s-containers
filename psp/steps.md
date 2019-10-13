# Stop Privileged containers with Pod Security Policy

- Delete all PSPs 

    ```
    kubectl delete psp --all
    ```

- Verify that `PodSecurityPolicy` admission controller enabled

    ```
    minikube ssh
    sudo cat /etc/kubernetes/manifests/kube-apiserver.yaml | grep admission
    ```

    Verify that the outcome of the above command include `PodSecurityPolicy`

- Create Pod and verify that it will not be recreated because of missing PSP

    ```
    kubectl apply -f pod.yaml
    ```

    The server will return the following message because there is no PSP matches that can be used

    ```
    Error from server (Forbidden): error when creating "pod.yaml": pods "psp-nginx" is forbidden: no providers available to validate pod request
    ```

- Create example PSP that does **NOT** permit running privileged containers

    ```
    kubectl apply -f example-psp.yaml
    ```
- Try scheduling the pod again

    ```
    kubectl apply -f pod.yaml
    ```

    The request will fail because the PSP doesn't enable privileged containers:

    ```
    Error from server (Forbidden): error when creating "pod.yaml": pods "psp-nginx" is forbidden: unable to validate against any pod security policy: [spec.containers[0].securityContext.privileged: Invalid value: true: Privileged containers are not allowed]
    ```