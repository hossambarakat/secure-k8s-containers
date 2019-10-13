
# Verify that root on container is root on host

- Deploy a privileged pod with access to the root directory of the host

    ```
    kubectl apply -f pod.yaml
    ```

- SSH into the minikube node

    ```
    minikube ssh
    ```

- Find the currently running sleep processes inside minikube

    ```
    ps -C sleep -o user,uid,cmd
    ```

- Access the shell of the running container

    ```
    kubectl exec -it webapp sh
    ```

- Run `sleep 100` inside the shell of the running container

- Verify that the node has another sleep process with root user by running the following command inside the minikube shell

    ```
    ps -C sleep -o user,uid,cmd
    ```

# Modify web app file system

- Deploy a privileged pod with access to the root directory of the host

    ```
    kubectl apply -f pod.yaml
    ```

- Access the shell of the running container

    ```
    kubectl exec -it webapp sh
    ```

- Verify the identity of the container running user is root

    ```
    id
    ```

- Verify the capabilities that the user has by running `amicontained` inside the container shell

- Open the web app in browser buing using port-forward command then open the url in the browser `http://localhost:3000/`

    ```
    kubectl port-forward webapp 3000:3000
    ```

- Modify `index.html` by adding an image to the bottom of the html file

    ```
    echo "<img src=\"lack-of-security.jpg\" />" >> /app/wwwroot/index.html
    ```

# Schedule a crypto miner using static pods

** This demo deploys a pod with an infinite loop, not a real crypto miner :)

- Deploy a privileged pod with access to the root directory of the host

    ```
    kubectl apply -f pod.yaml
    ```

- Access the shell of the running container

    ```
    kubectl exec -it webapp sh
    ```

- Create fake crypto miner pod by creating the `crypto-miner.yaml` file inside `etc/kubernetes/manifests` directory

  * Paste the following inside the container shell
    ```
    cat <<EOF > /hostroot/etc/kubernetes/manifests/crypto-miner.yaml
    ```

  * Paste the content of the `crypto-miner` pod from `crypto-miner.yaml` 

  * Enter EOF

  * Remember to delete the `crypto-miner.yaml`