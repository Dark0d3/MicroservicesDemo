# MicroservicesDemo

Learning Microservices => .net, RabbitMQ, Docker so on

Docker Commands

docker ps
docker build -t dark0d3/platformservice .
docker run --name platformservice -p 8080:8080 -d dark0d3/platformservice
docker stop <container-id>
docker start <container-id>
docker inspect <container-id>
docker logs <container-id>

K8S Commands

kubectl apply -f platforms-depl.yaml
kubectl get deployments
kubectl get pods
kubectl get services

delete deployment
kubectl delete deployment platforms-depl --that is the deployment name
