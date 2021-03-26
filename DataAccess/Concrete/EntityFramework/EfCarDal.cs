using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, RentACarContext>, ICarDal
    {
        public List<CarDetailDto> GetCarsDetails()
        {
            using (RentACarContext context=new RentACarContext())
            {
                //CarName, BrandName, ColorName, DailyPrice.
                var result = from c in context.Cars
                             join b in context.Brands on c.BrandId equals b.Id
                             join col in context.Colors on c.ColorId equals col.Id
                             select new CarDetailDto {CarName=c.CarName,BrandName=b.Name,ColorName=col.Name,DailyPrice=c.DailyPrice,ModelYear=c.ModelYear,Description=c.Description };
                return result.ToList();
                            

            }
        }
    }
}
