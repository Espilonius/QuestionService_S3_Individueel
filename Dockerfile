#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["QuestionService_S3_Individueel/QuestionService_S3_Individueel.csproj", "QuestionService_S3_Individueel/"]
RUN dotnet restore "QuestionService_S3_Individueel/QuestionService_S3_Individueel.csproj"
COPY . .
WORKDIR "/src/QuestionService_S3_Individueel"
RUN dotnet build "QuestionService_S3_Individueel.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QuestionService_S3_Individueel.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QuestionService_S3_Individueel.dll"]