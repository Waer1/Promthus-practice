# Demo 1

Aggregation and filtering, counters and gauges.

## Pre-requisites

Run two instances of the batch app and the Node Exporter on Linux.

```
./node_exporter

docker run -d -p 8080:80 --name batch sixeyed/prometheus-demo-batch:linux

docker run -d -p 8081:80 --name batch2 sixeyed/prometheus-demo-batch:linux
```

Run two instances of the web app and the Windows Exporter on Windows.

```
.\windows_exporter-0.13.0-amd64.exe --collectors.enabled "os,cpu,logical_disk"

docker run -d -p 8080:80  --name web sixeyed/prometheus-demo-web:windows

docker run -d -p 8081:80  --name web2 sixeyed/prometheus-demo-web:windows
```

And run Prometheus, configured to scrape all the above - see:

- [prometheus.yml](../prometheus.yml)
- [batch.json](../batch.json)
- [web.json](../web.json)

```
.\prometheus-2.18.1.windows-amd64\prometheus.exe --config.file="prometheus.yml"
```

Verify targets at http://localhost:9090/targets.

Switch to Graph page.

## 1.1 - Gauge 

Evaluate expressions over the batch processor's active jobs metric.

- `worker_jobs_active`
- `worker_jobs_active{instance="ps-prom-ub1804:8080"}` - selector
- `sum (worker_jobs_active)` - aggregation operator
- `sum without(instance) (worker_jobs_active)` 
- `sum without(instance, job, os, runtime) (worker_jobs_active)` 
- `avg without(instance, job, os, runtime) (worker_jobs_active)`
- `max without(instance, job, os, runtime) (worker_jobs_active)`
- `sum without(job, os, runtime) (worker_jobs_active)` - Graph


## 1.2 - Counter

Evaluate expressions over the batch processor's total jobs metric.

- `worker_jobs_total` - one much bigger, started earlier
- `worker_jobs_total[5m]` - range vector
- `rate(worker_jobs_total[5m])` - rates very similar
- `sum without(instance, job, os, runtime) (rate(worker_jobs_total[5m]))`
- `sum without(status, instance, job, os, runtime) (rate(worker_jobs_total[5m]))`
- `sum without(instance, job, os, runtime) (rate(worker_jobs_total{status="failed"}[5m]))`
- `sum without(job, os, runtime) (rate(worker_jobs_total{status="failed"}[5m]))` - Graph
- `sum without(job, os, runtime) (rate(worker_jobs_total{status="processed"}[5m]))` - Graph
