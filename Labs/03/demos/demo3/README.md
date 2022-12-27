# Demo 3

Customizing metric labels in config.

## 3.1 Add target labels

Config with extra labels [prometheus-demo3-1.yml](./prometheus-demo3-1.yml) config.

Stop existing Prometheus.

Run with new config:

```
..\prometheus-2.18.1.windows-amd64\prometheus.exe `
  --config.file="prometheus-demo3-1.yml" 
```

Check http://localhost:9090/service-discovery

Graph:

- `worker-jobs-active`

> Now includes `os` and `runtime` labels

## 3.2 Using relabel config for consolidation

Browse to the web app at http://ps-prom-win2019:8080/?slow

Graph:

- `worker-jobs-active`
- `web_delay_seconds_count`

> Mismatched label values for `runtime`

New config uses file service discovery & adds relabel config:

- [prometheus-demo3-2.yml](prometheus-demo3-2.yml)
- [web.json](web.json)
- [batch.json](batch.json)

```
..\prometheus-2.18.1.windows-amd64\prometheus.exe `
  --config.file="prometheus-demo3-2.yml" 
```

Check the target config at http://localhost:9090/service-discovery & http://localhost:9090/targets

And the new data in the _Graph_ page:

- `worker-jobs-active`
- `web_delay_seconds_count`

> Consistent label values for `runtime`

## 3.3 Manipulating metrics & labels in config

Check in the _Graph_ page:

- `go_goroutines` - all Go apps record this
- `node_filesystem_avail_bytes` - includes a mountpoint label
- `windows_logical_disk_free_bytes` - includes a volume label

New config file [prometheus-demo3-3.yml](prometheus-demo3-3.yml):

- drops Go metrics from servers
- removes the `mountpoint` label from the node available bytes metric
- copies the `volume` label for Windows free bytes to `device`

```
..\prometheus-2.18.1.windows-amd64\prometheus.exe `
  --config.file="prometheus-demo3-3.yml" 
```

