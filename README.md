# Info

## Environment

Built on MAC OS

## Usage (From root folder)

Test Suite - dotnet test MicroServices/DeltaTre.Service.WordApi.Tests
Run - docker compose

URLS:
Via Gateway
GET http://localhost:54241/word/top/{n} where N is a number. (Docs requested top 5 but made it customisable)
GET http://localhost:54241/word/get/{term} where Term is the word to search for

PUT http://localhost:54241/word/update (Updates list of words)
BODY string array

Via Service
GET http://localhost:54244/api/word/top/{n} where N is a number. (Docs requested top 5 but made it customisable)
GET http://localhost:54244/api/word/get/{term} where Term is the word to search for

PUT http://localhost:54244/api/word/update (Updates list of words)
BODY string array
