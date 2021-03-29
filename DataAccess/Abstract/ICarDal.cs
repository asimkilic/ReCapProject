using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ICarDal:IEntityRepository<Car>
    {
        List<CarDetailDto> GetCarsDetails();
        CarDetailDto GetCarDetailsById(int id);

        List<CarDetailDto> GetCarsDetailsByBrandId(int id);
        List<CarDetailDto> GetCarsDetailsByColorId(int id);

        


    }
}
