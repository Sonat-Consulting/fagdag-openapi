from fastapi import Depends, FastAPI, Path, Query, Body, HTTPException, Security
from fastapi.security import OAuth2PasswordBearer, OAuth2PasswordRequestForm

from pydantic import BaseModel, Schema

from sqlalchemy import Boolean, Column, Integer, String, create_engine
from sqlalchemy.ext.declarative import declarative_base, declared_attr
from sqlalchemy.orm import Session, sessionmaker
from sqlalchemy import select, column

from starlette.requests import Request
from starlette.responses import Response


# for testing in python console:
# SQLALCHEMY_DATABASE_URI = "sqlite:///./Python/FastAPI/sonat_employees.db"

# for live server:
SQLALCHEMY_DATABASE_URI = "sqlite:///sonat_employees.db"


engine = create_engine(SQLALCHEMY_DATABASE_URI, connect_args={"check_same_thread": False})
SessionLocal = sessionmaker(autocommit=False, autoflush=False, bind=engine)

class CustomBase:
    @declared_attr
    def __tablename__(cls):
        return cls.__name__.lower()


Base = declarative_base(cls=CustomBase)


# dependency
def get_db(request: Request):
    return request.state.db


# FastAPI specific code
app = FastAPI()


class Employee(Base):
    __tablename__ = "employees"

    employee_id = Column(Integer, primary_key=True)
    first_name = Column(String)
    last_name = Column(String)
    is_active = Column(Boolean(), default=True)


class EditEmployee(BaseModel):
    first_name: str = Schema(..., title="First name of employee", example={"first_name": "Ola"})
    last_name: str = Schema(..., title="Last name of employee", example={"last_name": "Nordmann"})


@app.middleware("http")
async def db_session_middleware(request: Request, call_next):
    response = Response("Internal server error", status_code=500)
    try:
        request.state.db = SessionLocal()
        response = await call_next(request)
    finally:
        request.state.db.close()
    return response


@app.get("/employees")
async def read_employees(skip: int = 0,
                         limit: int = 100,
                         short: bool = Query(True,
                                             description="Drop column representing "
                                                         "if employee is active or not"),
                         db_session: Session = Depends(get_db)):
    if short:
        columns = [column("employee_id"), column("first_name"), column("last_name")]
        out = db_session.query(select(from_obj=Employee, columns=columns)).all()[skip: skip + limit]

    else:
        out = db_session.query(Employee).all()[skip: skip + limit]
    return out


@app.get("/employees/{employee_id}")
async def read_employee(employee_id: int = Path(...,
                                                description="Search employee by unique ID"),
                        db_session: Session = Depends(get_db)):
    user = db_session.query(Employee).filter(Employee.employee_id == employee_id).all()
    return user


@app.post("/employees/")
async def create_employee(item: EditEmployee = Body(...,
                                                    description="JSON body with attribues "
                                                                "for an employee (first and last name)",
                                                    example={"first_name": "Ola",
                                                             "last_name": "Nordmann"}),
                          db_session: Session = Depends(get_db)):
    item = item.dict()
    new_employee = Employee(first_name=item["first_name"], last_name = item["last_name"])
    db_session.add(new_employee)
    db_session.commit()
    return item


@app.delete("/employees/{employee_id}")
async def delete_employee(employee_id: int = Path(...,
                                                description="Delete employee by unique ID")):
    stmt = Employee.__table__.delete().where(Employee.employee_id == employee_id)
    engine.execute(stmt)

    return employee_id


@app.put("/employees/{employee_id}")
async def update_employee(item: EditEmployee = Body(...,
                                                    description="JSON body with attribues "
                                                             "for an employee (first and last name)",
                                                    example={"first_name": "Ola",
                                                             "last_name": "Nordmann"}),
                          employee_id: int = Path(...,
                                                  description="Unique ID of employee to update name")):
    item = item.dict()
    stmt = Employee.__table__.update().\
        where(Employee.employee_id == employee_id).\
        values(first_name=item["first_name"], last_name = item["last_name"])
    engine.execute(stmt)

    return item








################################################
###### sequrity and user accounts ##############
################################################

fake_users_db = {
    "admin": {
        "username": "admin",
        "full_name": "Administrator",
        "email": "admin@api.com",
        "hashed_password": "hashed_admin_password",
        "disabled": False,
    },
    "service_account": {
        "username": "service_account",
        "full_name": "Service Account",
        "email": "service_account@api.com",
        "hashed_password": "hashed_service_account_password",
        "disabled": True,
    },
}


def fake_hash_password(password: str):
    return "hashed_" + password


oauth2_scheme = OAuth2PasswordBearer(tokenUrl="/token")


@app.post("/token")
async def login(form_data: OAuth2PasswordRequestForm = Depends()):
    user_dict = fake_users_db.get(form_data.username)

    if not user_dict:
        raise HTTPException(status_code=400, detail="Incorrect username or password")

    user = UserInDB(**user_dict)
    hashed_password = fake_hash_password(form_data.password)

    if not hashed_password == user.hashed_password:
        raise HTTPException(status_code=400, detail="Incorrect username or password")

    return {"access_token": user.username, "token_type": "bearer"}



class User(BaseModel):
    username: str
    email: str = None
    full_name: str = None
    disabled: bool = None


class UserInDB(User):
    hashed_password: str




def get_user(db, username: str):
    if username in db:
        user_dict = db[username]
        return UserInDB(**user_dict)

async def get_current_user(token: str = Security(oauth2_scheme)):
    user = get_user(db=fake_users_db, username=token)
    if not user:
        raise HTTPException(status_code=400, detail="Invalid authentication credentials")
    return user

async def get_current_active_user(current_user: User = Depends(get_current_user)):
    if current_user.disabled:
        raise HTTPException(status_code=400, detail="Inactive user")
    return current_user

@app.get("/users/me")
async def read_users_me(current_user: User = Depends(get_current_active_user)):
    return current_user
