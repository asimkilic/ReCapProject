using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        private ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }
        [ValidationAspect(typeof(CarValidator))]
        [SecuredOperation("admin,car.admin,car.add")]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Add(Car car)
        {          
                _carDal.Add(car);
                return new SuccessResult();
        }
        [TransactionScopeAspect]
        [SecuredOperation("adminadminadmin")]
        public IResult AddTransactionalTest(Car car)
        {
           
                    Add(car);
                    if (car.ColorId == 1)
                    {
                        throw new Exception("");
                    }
                    Add(car);
              
            return null;
        }
        [SecuredOperation("admin,car.admin,car.delete")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult();
        }

        [SecuredOperation("admin,car.admin,car.getall")]
        [CacheAspect]
        public IDataResult<List<Car>> GetAll()
        {
          
            return new SuccessDataResult<List<Car>>(_carDal.GetAll());

        }
        [SecuredOperation("admin,car.admin,car.getbyid")]
        [CacheAspect]
        public IDataResult<Car> GetById(int carId)
        {
          

            return new SuccessDataResult<Car>(_carDal.Get(c=>c.Id==carId));
        }
        [SecuredOperation("admin,car.admin,car.getcarsbybrandid")]
        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == brandId));
        }
        [SecuredOperation("admin,car.admin,car.getcarsbycolorid")]
        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == colorId));
        }
        [SecuredOperation("admin,car.admin,car.getcarsdetails")]
        public IDataResult<List<CarDetailDto>> GetCarsDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarsDetails(),Messages.ProductListed);
        }
        [ValidationAspect(typeof(CarValidator))]
        [SecuredOperation("admin,car.admin,car.update")]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult();
        }
    }
}
