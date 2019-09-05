cat <<EOF > /hostroot/etc/kubernetes/manifests/static-web.yaml

```yaml
apiVersion: v1
kind: Pod
metadata:
  name: static-web
  labels:
    role: myrole
    app: static-web
spec:
  containers:
    - name: web
      image: nginx
      ports:
        - name: web
          containerPort: 80
          protocol: TCP
---
kind: Service
apiVersion: v1
metadata:
  name: my-service
spec:
  selector:
    app: static-web
  ports:
  - protocol: TCP
    port: 80
    targetPort: 8443
EOF
```