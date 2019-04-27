# OpenAPI with FastAPI

Create and run an API from Python. 

This is a simple implementation of FastAPI in Python to test out a few constructs:
- Running an API on Python
- OpenAPI documentation
- Write and read from SQLite via SQLAlchemy
- Authentication
 

The main data base consists of two tables
1. Employee (id, name)
2. Employee address (id, street, postal code etc.)

The address data base is not used, and was only created as a foundation for further development and testing.

The way SQLAlchemy and FastAPI handles writing and reading from the SQLite database, seems a bit messy.
The documentation and examples that this code is based upon was not consistent.
Any input on this would be much appreciated.

There is a second in-code implementation of a user data base.
This was to test out authentication.
It is possible to read out employee (SQLite) information without any authentication.


### Functionality
- Read all employees
- Read one employee
- Update one employee
- Delete one employee

- Authenticate user
- Read user info


### Install dependencies

````bash
pip install fastapi
pip install uvicorn
````


### Initiate data base

```bash
python ./Python/FastAPI/data_base_initiation.py
```

### Run server (in development)
You must navigate to THIS folder te be able to run the server:

````bash
cd Python/FastAPI
uvicorn main:app --reload
````

To run in prod:
````bash
cd Python/FastAPI
uvicorn main:app
````


## Use

Visit http://127.0.0.1:8000/docs in your browser to interact with the API.

Authenticate by clicking "authentication" in upper right corner.

User `admin`, password `admin_password`.

## Documentation

* http://127.0.0.1:8000/docs
* http://127.0.0.1:8000/redoc



## Querying

* http://127.0.0.1:8000/employees
* http://127.0.0.1:8000/employees?skip=1&limit=1
* http://127.0.0.1:8000/employees/1




# Rescources

* https://www.pythoncentral.io/introductory-tutorial-python-sqlalchemy/
* https://docs.sqlalchemy.org/en/13/core/dml.html
* https://fastapi.tiangolo.com
* https://editor.swagger.io/
* https://github.com/Sonat-Consulting/fagdag-openapi/
