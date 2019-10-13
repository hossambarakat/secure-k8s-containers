# Limit access to a service using a network policy

## Deploy the app

- Run the following command to deploy an nginx app with a service in front of it
    ```
    kubectl apply -f my-app.yaml
    ```

## Access the service from another pod

- Start a busybox with interactive bash
    ```
    kubectl run --generator=run-pod/v1 busybox --rm -ti --image=busybox /bin/sh
    ```

- Run the following command to test connectivity to the service

    ```
    wget --spider --timeout=1 my-web-service
    ```

    The response should be success 
    ```
    remote file exists
    ```

## Limit access using network policy

- Deploy the network policy

    ```
    kubectl apply -f nginx-policy.yaml
    ```

## Test access to the service when access label is not defined

- Start a busybox with interactive bash
```
kubectl run --generator=run-pod/v1 busybox --rm -ti --image=busybox /bin/sh
```

- Run the following command to test connectivity to the service

    ```
    wget --spider --timeout=1 my-web-service
    ```

    The response should be failure 

    ```
    wget: download timed out
    ```


## Define access label and test again

- Start a busybox with access label defined
    ```
    kubectl run --generator=run-pod/v1 busybox --rm -ti --labels="access=true" --image=busybox /bin/sh
    ```

- Run the following command to test connectivity to the service

    ```
    wget --spider --timeout=1 my-web-service
    ```

## References
- [Declare Network Policy](https://kubernetes.io/docs/tasks/administer-cluster/declare-network-policy/)
- [Use Cilium for NetworkPolicy](https://kubernetes.io/docs/tasks/administer-cluster/network-policy-provider/cilium-network-policy/)
- [Example recipes for Kubernetes Network Policies](https://github.com/ahmetb/kubernetes-network-policy-recipes)