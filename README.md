# MicroservicesDemo

Overview
Learning Microservices with .NET, RabbitMQ, Docker, and Kubernetes.

Docker Commands
docker ps
docker build -t dark0d3/platformservice .
docker run --name platformservice -p 8080:8080 -d dark0d3/platformservice
docker stop <container-id>
docker start <container-id>
docker inspect <container-id>
docker logs <container-id>
Kubernetes Commands
kubectl apply -f platforms-depl.yaml
kubectl get deployments
kubectl get pods
kubectl get services
kubectl rollout restart deployment commands-depl (rollout after update -- takes from DockerHub)
kubectl get namespace
kubectl get pods --namespace=ingress-nginx
kubectl delete deployment platforms-depl
kubectl get storageclass
kubectl get pvc
kubectl create secret generic mssql --from-literal=SA_PASSWORD="pass"
For more information, visit the Kubernetes Ingress NGINX deployment guide.


