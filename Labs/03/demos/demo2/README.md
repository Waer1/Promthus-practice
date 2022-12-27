# Demo 2

Configuring Prometheus.

## 2.1 Static configuration for scrape targets

Using custom [prometheus-demo2.yml](./prometheus-demo2.yml) config.

```
..\prometheus-2.18.1.windows-amd64\promtool.exe check config prometheus.yml
```

Stop existing Prometheus.

Run with new config:

```
..\prometheus-2.18.1.windows-amd64\prometheus.exe `
  --config.file="prometheus-demo2.yml" `
  --web.enable-lifecycle
```

## 2.2 Explore the new targets

Check http://localhost:9090/config and http://localhost:9090/targets

Check the metrics being scraped in the _Graph_ page:

- `up`
- `scrape_duration_seconds`
- `scrape_samples_scraped`
- `worker_jobs_active` - labels & graph

> Browse to & refresh the web app at http://ps-prom-win2019:8080/

Check metrics:

- `http_request_duration_seconds_bucket`


## 2.3 Make a live config update

Check the latest config load timestamp:

- `prometheus_config_last_reload_success_timestamp_seconds`

> Make a change to the [config file](./prometheus-demo2.yml)

```
curl -X POST http://localhost:9090/-/reload
```