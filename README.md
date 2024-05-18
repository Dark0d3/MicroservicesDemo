# MicroservicesDemo

## Learning Microservices with .NET, RabbitMQ, and Docker

### Docker Commands

```sh
# List running containers:
docker ps

# Build an image:
docker build -t dark0d3/platformservice .

# Run a container:
docker run --name platformservice -p 8080:8080 -d dark0d3/platformservice

# Stop a container:
docker stop <container-id>

# Start a container:
docker start <container-id>

# Inspect a container:
docker inspect <container-id>

# View logs of a container:
docker logs <container-id>


# Apply a deployment configuration:
kubectl apply -f platforms-depl.yaml

# List deployments:
kubectl get deployments

# List pods:
kubectl get pods

# List services:
kubectl get services

# Rollout restart of a deployment (after updating from Docker Hub):
kubectl rollout restart deployment commands-depl

# List namespaces:
kubectl get namespace

# List pods in a specific namespace:
kubectl get pods --namespace=ingress-nginx

# Delete a deployment:
kubectl delete deployment platforms-depl


Ingress Controller Deployment for Docker Desktop
For deploying the Ingress controller on Docker Desktop, refer to the official documentation. https://kubernetes.github.io/ingress-nginx/deploy/#docker-desktop

Additional Kubernetes Commands

# List storage classes:
kubectl get storageclass

# List persistent volume claims (PVC):
kubectl get pvc

# Create a secret:
kubectl create secret generic mssql --from-literal=SA_PASSWORD="pass"

```
