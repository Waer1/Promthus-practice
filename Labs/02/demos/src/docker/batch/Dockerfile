ARG SDK_TAG=3.1.300
ARG RUNTIME_TAG=3.1.4
FROM mcr.microsoft.com/dotnet/core/sdk:$SDK_TAG as builder

WORKDIR /src/PrometheusDemo.Batch
COPY PrometheusDemo/PrometheusDemo.Batch/PrometheusDemo.Batch.csproj .
RUN dotnet restore

COPY PrometheusDemo/PrometheusDemo.Batch/ .
RUN dotnet publish -c Release -o /out PrometheusDemo.Batch.csproj

# app image
FROM mcr.microsoft.com/dotnet/core/runtime:$RUNTIME_TAG

EXPOSE 80
ENTRYPOINT ["dotnet", "/app/PrometheusDemo.Batch.dll"]

WORKDIR /app
COPY --from=builder /out/ .