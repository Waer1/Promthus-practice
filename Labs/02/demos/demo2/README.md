# Demo 2

Metrics for Windows servers and a web app.

## Pre-reqs 

A Windows machine with Docker installed.

```
ssh Administrator@ps-prom-win2019
```

## 2.1 Web application

This is a .NET Core web app which uses the Prometheus client library.

```
docker run -d -p 8080:80  --name web sixeyed/prometheus-demo-web:windows

docker logs web
```

> Browse to:

- http://ps-prom-win2019:8080
- http://ps-prom-win2019:8080/metrics

### The runtime metrics include the web server

- `dotnet_total_memory_bytes` and `dotnet_collection_count_total` - same as batch app
- `http_requests_received_total` is a counter with labels for HTTP method and response code
- `http_request_duration_seconds` is a histogram with durations for processing requests

> Now browse to:

- http://ps-prom-win2019:8080?slow
- http://ps-prom-win2019:8080/metrics

### The app metrics include a calculation time

- `web_delay_seconds` is a summary of the delay added to the response
- `http_request_duration_seconds` the histogram also shows the extra response time

## 2.2 - Windows Exporter 

Download & run the [Windows Exporter](https://github.com/prometheus-community/windows_exporter): 

```
iwr -useb -o windows_exporter-0.13.0-amd64.exe https://github.com/prometheus-community/windows_exporter/releases/download/v0.13.0/windows_exporter-0.13.0-amd64.exe

.\windows_exporter-0.13.0-amd64.exe
```

- Needs firewall configured for default port `9182`

> Browse to http://ps-prom-win2019:9182/metrics

> There are *a lot* of metrics

Filter output:

```
.\windows_exporter-0.13.0-amd64.exe --collectors.enabled "os,cpu,logical_disk"
```

> Refresh http://ps-prom-win2019:9182/metrics

> Almost all counters and gauges

## There are hardware/OS metrics

- `windows_logical_disk_read_seconds_total` is a counter with a volume name label
- `windows_logical_disk_write_seconds_total` is a counter with a volume name label
- `windows_cpu_time_total` has labels for the CPU core and the work mode
- `windows_logical_disk_free_bytes` is a gauge of free disk space

## And info metrics

- `windows_os_info` returns text data which describes version info

## Other exporters still running

Across the systems there is a consistent monitoring API and metrics format.

- http://ps-prom-win2019:8080/metrics
- http://ps-prom-win2019:9182/metrics
- http://ps-prom-ub1804:8080/metrics
- http://ps-prom-ub1804:9100/metrics