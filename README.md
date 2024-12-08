# CarlitosDroid Web Api (.NET 8.0)

## Change Environment Variable
* If you are Using Visual Studio Code -> Go to .vscode -> launch.json -> ASPNETCORE_ENVIRONMENT (Development, Staging, Production)

## Run database with docker

1. Run `docker pull mcr.microsoft.com/mssql/server`
2. Run `docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Password_123#" -p 1433:1433 --name sql_server_container -d mcr.microsoft.com/mssql/server`
3. Stop database container `docker stop sql_server_container`
4. Remove database container `docker rm sql_server_container`
5. Start database container again `docker start sql_server_container`
6. Check container logs `docker logs sql_server_container`

Server: `localhost,1433`
Username: `sa`