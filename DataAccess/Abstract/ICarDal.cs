using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ICarDal:IEntityRepository<Car>
    {
        List<CarDetailDto> GetCarsDetails();
        CarDetailDto GetCarDetailsById(int id);

        List<CarDetailDto> GetCarsDetailsByBrandId(int id);
        List<CarDetailDto> GetCarsDetailsByColorId(int id);

        List<CarDetailDto> GetCarsWithByColorIdAndBrandId(int colorId, int brandId);
        List<CarDetailDto> GetRelatedCarsBySegmentId(int segmentId);
    }
}
