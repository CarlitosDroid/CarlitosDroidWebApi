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

# How to run the project in Production (using Google Cloud)
1. Do not forget to set the env variables (DATABASE_URL, JWT_ISSUER, JWT_SECRET_KEY, JWT_AUDIENCE, JWT_EXPIRATION, etc) in the server.
2. Enable Networking VPC for your web service (Google Cloud Run) to be able to connect to your database (Cloud SQL through private IP, etc).
3. Disable Public IP for your Database, don't expose your database to the public.

# Generate a JWT Secret

1. You can use Node.js: `node -e "console.log(require('crypto').randomBytes(32).toString('hex'))"` look at this website https://dev.to/tkirwa/generate-a-random-jwt-secret-key-39j4 
2. You can use https://jwtsecret.com/generate 

# Used Libraries

1. For documentation: `Swashbuckle.AspNetCore`
2. For documentation: `Swashbuckle.AspNetCore.Annotations`
3. For connecting DB: `Microsoft.EntityFrameworkCore.SqlServer`
4. For Authentication with JWT: `Microsoft.AspNetCore.Authentication.JwtBearer`