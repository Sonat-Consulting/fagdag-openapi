from sqlalchemy import create_engine
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy import Column, Integer, String, ForeignKey, Boolean
from sqlalchemy.orm import relationship

# set up data base
SQLALCHEMY_DATABASE_URI = "sqlite:///./Python/FastAPI/sonat_employees.db"
engine = create_engine(SQLALCHEMY_DATABASE_URI, connect_args={"check_same_thread": False})
Base = declarative_base()


# define tables
class Employee(Base):
    __tablename__ = "employees"

    employee_id = Column(Integer, primary_key=True)
    first_name = Column(String)
    last_name = Column(String)
    is_active = Column(Boolean(), default=True)

class Address(Base):
    __tablename__ = "addresses"

    address_id = Column(Integer, primary_key=True)
    street_name = Column(String(250))
    street_number = Column(String(250))
    postal_code = Column(String(250), nullable=False)
    sonat_employee_id = Column(Integer, ForeignKey("employees.employee_id"))
    sonat_employee = relationship(Employee)


# create tables
Base.metadata.create_all(engine)


# add some data to the table
import pandas as pd
df = pd.DataFrame({"first_name" : ["Henrik", "Ravn", "Espen"],
                   "last_name" : ["Nyhus", "Ivarson", "Kvalheim"]})
df
df.to_sql("employees", con=engine, if_exists="append", index=False)

# delete tables
# Employee.__table__.drop(engine)
# Address.__table__.drop(engine)

