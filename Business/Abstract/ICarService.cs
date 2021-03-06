using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
   public interface ICarService
    {

        IDataResult<List<Car>> GetCarsByBrandId(int brandId);
        IDataResult<List<Car>> GetCarsByColorId(int colorId);
        IDataResult<List<CarDetailDto>> GetCarsDetails();
        IDataResult<Car> GetById(int carId);
        IDataResult<List<Car>> GetAll();
        IResult Add(Car car);
        IResult Update(Car car);
        IResult Delete(Car car);
        IResult AddTransactionalTest(Car car); //uygulamalarda tutarlılığı sağlamak için kullandığımız yöntem.
        IDataResult<CarDetailDto> GetCarDetailsById(int id);
        IDataResult<List<CarDetailDto>> GetCarsDetailsByBrandId(int id);
        IDataResult<List<CarDetailDto>> GetCarsDetailsByColorId(int id);
        IDataResult<List<CarDetailDto>> GetCarsWithByColorIdAndBrandId(int colorId, int brandId);
        IDataResult<List<CarDetailDto>> GetRelatedCarsBySegmentId(int segmentId);
    }
}
