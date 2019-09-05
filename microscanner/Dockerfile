FROM mcr.microsoft.com/dotnet/core/sdk:3.0.100-preview8-alpine3.9

RUN apk add --no-cache ca-certificates && update-ca-certificates

ADD https://get.aquasec.com/microscanner .
RUN chmod +x microscanner

RUN ./microscanner ${token}

#