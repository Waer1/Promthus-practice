# Demo 1

Download, run and explore Prometheus.

## 1.1 Download the Prometheus server

Check [Prometheus download options](https://prometheus.io/download/) (including OS and CPU architecture).

Run on Windows:

```
$ProgressPreference = "SilentlyContinue"; `
iwr -useb `
  -o prometheus-2.18.1.windows-amd64.tar.gz `
  https://github.com/prometheus/prometheus/releases/download/v2.37.4/prometheus-2.37.4.linux-amd64.tar.gz

tar xvfz prometheus.tar.gz
```

Explore [prometheus.yml](../prometheus-2.18.1.windows-amd64/prometheus.yml):

- alerting and rules 
- delete all except global and scrape config

## 1.2 Run Prometheus with default config

Switch to the download folder and run the binary:

```
cd prometheus-2.18.1.windows-amd64

./prometheus.exe
```

Check the log output:

- Starting TSDB
- Loading configuration file filename=prometheus.yml
- Server is ready to receive web requests

And the `data` directory.

## 1.3 Explore the Prometheus UI

Check Prometheus' own metrics at http://localhost:9090/metrics

Browse to the UI at http://localhost:9090

Check the _Status_ pages:

- Configuration
- Command-Line Flags
- Targets
- Service Discovery

And some data in the _Graph_ page:

- `go_info`, standard Go info with instance and job labels
- `promhttp_metric_handler_requests_total`, graph (UI shows number of time series)