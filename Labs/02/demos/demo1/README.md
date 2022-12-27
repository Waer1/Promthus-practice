# Demo 1

Metrics for Linux servers and a batch processing app.

## Pre-reqs 

A Linux machine with Docker installed.

```
ssh ps-prom-ub1804
```

## 1.1 Batch processing app

This is a .NET Core console app which uses the Prometheus client library.

```
docker run -d -p 8080:80 --name batch sixeyed/prometheus-demo-batch:linux

docker logs batch
```

> Browse to http://ps-prom-ub1804:8080/metrics

### There are custom application metrics 

- `worker_jobs_active` is a _Gauge_, as you refresh you'll see it goes up and down
- `worker_jobs_total` is a _Counter_, the numbers always increase
- the counter also has labels to record variations

### And .NET runtime metrics

- `dotnet_total_memory_bytes` is a gauge showing allocated memory
- `dotnet_collection_count_total` is a counter of garbage collections


## 1.2 - Node Exporter

Download & extract the [Node Exporter](https://github.com/prometheus/node_exporter): 

```
wget https://github.com/prometheus/node_exporter/releases/download/v1.0.0/node_exporter-1.0.0.linux-amd64.tar.gz

tar xvfz node_exporter-1.0.0.linux-amd64.tar.gz

cd node_exporter-1.0.0.linux-amd64/
```

Run (normally this would be a daemon):

```
./node_exporter
```

> Browse to http://ps-prom-ub1804:9100/metrics

### There are hardware/OS metrics

- `node_disk_io_time_seconds_total` is a counter with a device name label
- `node_cpu_seconds_total` has labels for the CPU core and the work mode
- `node_filesystem_avail_bytes` is a gauge of free disk space

### And info metrics

- `node_uname_info` returns text data which describes version info
