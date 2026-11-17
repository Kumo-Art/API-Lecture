## 1. The request journey

```
Client (Postman or browser)
    | JSON: a DTO
    V
 Controller (speaks HTTP. reads the request, picks the status code.)
    | DTO
    V
Services (The Rules / Logic. Converts DTO's to entities and back.)
    | Entity
    V       
Repository (The only layer that talks to the database.)
    | Entity
    V
DbContext (Ef Core)    
    | SQL
    V
 Database
 ```   
