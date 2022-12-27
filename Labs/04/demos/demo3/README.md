# Demo 3

Grafana and the Prometheus API.

## 3.1 - HTTP API

Query an instant vector expression:

```
worker_active_jobs
```

- http://localhost:9090/api/v1/query?query=worker_jobs_active

Query a range vector expression:

```
worker_jobs_total[5m]
```

- http://localhost:9090/api/v1/query_range?query=rate(worker_jobs_total[5m])&start=1592382830&end=1592383030&step=60

## 3.2 - Grafana

Run Grafana in a container:

```
docker run -d --name=grafana -p 3000:3000 grafana/grafana:7.0.3
```

> Browse to http://localhost:3000, sign in with `admin`/`admin`

Add Prometheus data source `http://host.docker.internal:9090`

Load the dashboard from [dashboard.json](dashboard.json).

http://host.docker.internal:9090

http://promthus:9090


FROM prom/prometheus

ADD prometheus.yml /etc/prometheus
