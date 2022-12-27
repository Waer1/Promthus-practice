ARG SDK_TAG=3.1.300
ARG RUNTIME_TAG=3.1.4
FROM mcr.microsoft.com/dotnet/core/sdk:$SDK_TAG as builder

WORKDIR /src/PrometheusDemo.Web
COPY PrometheusDemo/PrometheusDemo.Web/PrometheusDemo.Web.csproj .
RUN dotnet restore

COPY PrometheusDemo/PrometheusDemo.Web/ .
RUN dotnet publish -c Release -o /out PrometheusDemo.Web.csproj

# app image
FROM  mcr.microsoft.com/dotnet/core/aspnet:$RUNTIME_TAG

EXPOSE 80
ENTRYPOINT ["dotnet", "/app/PrometheusDemo.Web.dll"]

WORKDIR /app
COPY --from=builder /out/ .