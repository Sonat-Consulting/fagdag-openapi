from fastapi import Depends, FastAPI
from sqlalchemy import Boolean, Column, Integer, String, create_engine
from sqlalchemy.ext.declarative import declarative_base, declared_attr
from sqlalchemy.orm import Session, sessionmaker
from starlette.requests import Request
from starlette.responses import Response

import sys
sys.path.append('../FastAPI')

# SQLAlchemy specific code, as with any other app
#SQLALCHEMY_DATABASE_URI = "sqlite:///./FastAPI/sonat_employees.db"
SQLALCHEMY_DATABASE_URI = "sqlite:///sonat_employees.db"
engine = create_engine(SQLALCHEMY_DATABASE_URI, connect_args={"check_same_thread": False})

SessionLocal = sessionmaker(autocommit=False, autoflush=False, bind=engine)

class CustomBase:
    # Generate __tablename__ automatically
    @declared_attr
    def __tablename__(cls):
        return cls.__name__.lower()


Base = declarative_base(cls=CustomBase)


class Employee(Base):
    __tablename__ = "employees"

    employee_id = Column(Integer, primary_key=True)
    first_name = Column(String)
    last_name = Column(String)
    is_active = Column(Boolean(), default=True)



db_session = SessionLocal()
first_user = db_session.query(Employee).first()
db_session.close()


# Utility
def get_user(db_session: Session, employee_id: int):
    return db_session.query(Employee).filter(Employee.employee_id == employee_id).first()


# Dependency
def get_db(request: Request):
    return request.state.db


# FastAPI specific code
app = FastAPI()


@app.get("/employees/{employee_id}")
def read_user(employee_id: int, db_session: Session = Depends(get_db)):
    user = get_user(db_session=db_session, employee_id=employee_id)
    return user

@app.get("/employees")
def read_users(db_session: Session = Depends(get_db)):
    out = db_session.query(Employee).all()
    return out


for instance in db_session.query(Employee).order_by(Employee.employee_id):
    print(instance.first_name, instance.last_name)

for row in db_session.query(Employee).all():
    print(row.first_name, row.last_name)


@app.middleware("http")
async def db_session_middleware(request: Request, call_next):
    response = Response("Internal server error", status_code=500)
    try:
        request.state.db = SessionLocal()
        response = await call_next(request)
    finally:
        request.state.db.close()
    return response


# run:
# uvicorn ./FastAPI/main:app --reload