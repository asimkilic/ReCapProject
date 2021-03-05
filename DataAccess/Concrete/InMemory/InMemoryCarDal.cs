using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete
{
    public class InMemoryCarDal : ICarDal
    {

        List<Car> _cars;
        public InMemoryCarDal()
        {
            _cars = new List<Car> 
            {
                new Car{Id=1,BrandId=1,ColorId=1,DailyPrice=5000,Description="Good",ModelYear=DateTime.Now.Year-7},
                new Car{Id=2,BrandId=2,ColorId=2,DailyPrice=12000,Description="Very Good",ModelYear=DateTime.Now.Year-1},
               // new Car{Id=3,BrandId=2,ColorId=3,DailyPrice=10000,Description="Not Bad",ModelYear=DateTime.Now.Year-2},
               //new Car{Id=4,BrandId=3,ColorId=2,DailyPrice=7500,Description="Normal",ModelYear=DateTime.Now.Year-5}


            };
        }

        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            Car deletedCar = _cars.SingleOrDefault(x => x.Id == car.Id);
            _cars.Remove(deletedCar);
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public Car GetById(int carId)
        {
            return _cars.Where(x => x.Id == carId).SingleOrDefault();
        }

        public void Update(Car car)
        {
            var updatedCar = _cars.FirstOrDefault(x => x.Id == car.Id);
            updatedCar.ColorId = car.ColorId;
            updatedCar.BrandId = car.BrandId;
            updatedCar.DailyPrice = car.DailyPrice;
            updatedCar.Description = car.Description;
            updatedCar.ModelYear = car.ModelYear;
           
        }
    }
}
