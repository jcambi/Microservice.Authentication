# Microservice Authentication
Reference for DDD-oriented microservice: https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/ddd-oriented-microservice

## Implementation
Two routes were created `api/auth/register` and `api/auth/authenticate` to create a user and validate a user. The purpose of this project is to make an Authentication microservice that will provide the JWT access token to implement Authorization to routes of other microservices whenever a User is successfully authenticated.

To implement Authorization with other microservices, a JWT middleware needs to be added to the invoked microservice and the request should contain an Authorization header with the Bearer token as JWT.

A user will be created in the database. All data passed are going to be stored as is except for the passwords. Passwords will implement symmetrical hash + salt to improve security.

## How to Run
1. Clone the exisiting repository on your local machine with Git Bash or Windows Command Prompt

```bash
git clone git@github.com:jcambi/Microservice.Authentication.git
```
or
```bash
git clone https://github.com/jcambi/Microservice.Authentication.git
```
2. Navigate to the root directory of the project and build the Docker image:
```bash
docker build -t microservice-authentication .
```
3. Spin up a container with the Docker image:
```bash
docker run -d -p 5100:80 --name my-auth-api microservice-authentication
```
4. Launch Postman and create a POST request to add a User with a request body like this:
```json
{
  "firstName": "John",
  "lastName": "Smith",
  "email": "john.smith@fmail.com",
  "password": "MyUniquePassword"
}
```
5. Create another POST request to authenticate user with a request body like this:
```json
{
  "email": "john.smith@fmail.com",
  "password": "MyUniquePassword"
}
```
It will return the authenticated user together with the JWT token, otherwise it will return Unauthorized (401).

### Enhancements
- Add validations to register unique emails only.
- Add validation for maximum character length
- Add password policy (requirements to accept a password)
