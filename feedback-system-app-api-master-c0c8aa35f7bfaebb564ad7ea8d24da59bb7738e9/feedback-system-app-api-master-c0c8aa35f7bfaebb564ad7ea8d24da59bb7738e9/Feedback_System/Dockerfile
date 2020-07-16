#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-sac2016 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk-nanoserver-sac2016 AS build
WORKDIR /src
COPY ["Feedback_System/Feedback_System.csproj", "Feedback_System/"]
RUN dotnet restore "Feedback_System/Feedback_System.csproj"
COPY . .
WORKDIR "/src/Feedback_System"
RUN dotnet build "Feedback_System.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Feedback_System.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Feedback_System.dll"]