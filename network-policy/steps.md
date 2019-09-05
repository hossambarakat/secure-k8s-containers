## Deploy my application

```bash
k apply -f my-app.yaml
```

## Access the service from another pod

```
kubectl run busybox --rm -ti --image=busybox /bin/sh
```

from inside the pod run the following command

```
wget --spider --timeout=1 my-web-service
```
The response should be success 
```
remote file exists
```

## Limit access using network policy

deploy the network policy
```
k apply -f nginx-policy.yaml
```

## Test access to the service when access label is not defined

from inside the pod run the following command

```
wget --spider --timeout=1 my-web-service
```
The response should be failure 
```
wget: download timed out
```


## Define access label and test again

```
kubectl run busybox --rm -ti --labels="access=true" --image=busybox /bin/sh
```

from inside the pod run the following command

```
wget --spider --timeout=1 my-web-service
```

## Reference
https://kubernetes.io/docs/tasks/administer-cluster/declare-network-policy/
[Use Cilium for NetworkPolicy](https://kubernetes.io/docs/tasks/administer-cluster/network-policy-provider/cilium-network-policy/)
[Declare Network Policy](https://kubernetes.io/docs/tasks/administer-cluster/declare-network-policy/)
[Example recipes for Kubernetes Network Policies](https://github.com/ahmetb/kubernetes-network-policy-recipes)