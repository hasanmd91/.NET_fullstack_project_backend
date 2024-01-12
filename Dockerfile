FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /

# copy solution file 
COPY  *.sln .

# copy project file for nuget restore 
COPY Ecom.Core/Ecom.Core.csproj Ecom.Core/
COPY Ecom.WebAPI/Ecom.WebAPI.csproj  Ecom.WebAPI/
COPY Ecom.Service/Ecom.Service.csproj Ecom.Service/
COPY Ecom.Controller/Ecom.Controller.csproj Ecom.Controller/
COPY Ecom.Test/Ecom.Test.csproj Ecom.Test/

# resotre nuget packages 
RUN dotnet restore

# copy rest of the source code 
COPY Ecom.Core/ Ecom.Core/
COPY Ecom.WebAPI/ Ecom.WebAPI/
COPY Ecom.Service/ Ecom.Service/
COPY Ecom.Controller/ Ecom.Controller/
COPY Ecom.Test/ Ecom.Test/ 

# Build and publish the web api project 

WORKDIR  /Ecom.WebAPI 
RUN dotnet publish -c Release -o /publish 

# Final stage/image
FROM mcr.microsoft.com/dotnet/sdk:7.0 
WORKDIR /
COPY --from=build /publish . 
EXPOSE 8080
ENTRYPOINT [ "dotnet","Ecom.WebAPI.dll" ]

