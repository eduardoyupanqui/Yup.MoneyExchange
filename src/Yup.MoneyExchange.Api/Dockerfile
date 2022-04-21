#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Yup.MoneyExchange.Api/Yup.MoneyExchange.Api.csproj", "src/Yup.MoneyExchange.Api/"]
COPY ["src/Yup.MoneyExchange.Application/Yup.MoneyExchange.Application.csproj", "src/Yup.MoneyExchange.Application/"]
COPY ["src/Yup.MoneyExchange.Domain/Yup.MoneyExchange.Domain.csproj", "src/Yup.MoneyExchange.Domain/"]
COPY ["src/Yup.MoneyExchange.Infrastructure/Yup.MoneyExchange.Infrastructure.csproj", "src/Yup.MoneyExchange.Infrastructure/"]
RUN dotnet restore "src/Yup.MoneyExchange.Api/Yup.MoneyExchange.Api.csproj"
COPY . .
WORKDIR "/src/src/Yup.MoneyExchange.Api"
RUN dotnet build "Yup.MoneyExchange.Api.csproj" --no-restore -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Yup.MoneyExchange.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Yup.MoneyExchange.Api.dll"]