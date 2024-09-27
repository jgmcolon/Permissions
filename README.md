# Permissions N5 Challenge

## Definicion and structure
This challenge was create use a clean Architecture with .NET 8.
It contains a structure in layers  for 
* API 
* Domain
* Infrastructure to Persistence data SQLSERVER
* Infrastructure to Elastic Search
* Infrastructure to Kafka
* Website REACT with VITE

## TO EXECUTE APP

### Prerequisites
* Get Docker Desktop Windows/Linux/Mac 
  (this app was test in docker 4.34.2 )
* GIT 
  (was test in git version 2.45.1.windows.1)

### RUN APP

1. Clone Current Repository
2. Execute docker-compose -f docker-compose.yml up  -d --build 
3. Open in browser
   * http://localhost:3000 to website
   * http://localhost:8090/swagger to api
   * http://localhost:5601 to Kibana web to check Elasticsearch
   * http://localhost:8080 to Kafka-ui Web to check Kafka  
