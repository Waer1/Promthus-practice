# Demo 2

Aggregation and filtering, summaries and histograms.

## Pre-requisites

Generate some load to the web app instances using [loadgen.ps1](loadgen.ps1).

```
./loadgen.ps1
```

## 2.1 - Summary 

Calculate the average delay added in slow mode.

- `web_delay_seconds_count`
- `sum without(job, os, runtime) (rate(web_delay_seconds_count[5m]))` - req/s
- `sum without(job, os, runtime) (rate(web_delay_seconds_sum[5m]))` - delay/s
- `sum without(job, os, runtime) (rate(web_delay_seconds_sum[5m])) / sum without(job, os, runtime) (rate(web_delay_seconds_count[5m]))` - avg delay

## 2.2 - Histogram

Calculate the 90th percentile response time.

- `http_request_duration_seconds_bucket`
- `rate(http_request_duration_seconds_bucket[5m])`
- `rate(http_request_duration_seconds_bucket{code="200"}[5m])`
- `sum without(code, job, method, os, runtime) (rate(http_request_duration_seconds_bucket{code="200"}[5m]))`
- `histogram_quantile(0.90, sum without(code, job, method, os, runtime) (rate(http_request_duration_seconds_bucket{code="200"}[5m])))`
