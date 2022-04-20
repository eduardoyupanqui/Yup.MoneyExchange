#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Yup.TipoCambio.Api/Yup.TipoCambio.Api.csproj", "src/Yup.TipoCambio.Api/"]
RUN dotnet restore "src/Yup.TipoCambio.Api/Yup.TipoCambio.Api.csproj"
COPY . .
WORKDIR "/src/src/Yup.TipoCambio.Api"
RUN dotnet build "Yup.TipoCambio.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Yup.TipoCambio.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Yup.TipoCambio.Api.dll"]