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
        public CarDetailDto GetCarDetailsById(int id)
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result= from c in context.Cars
                            join b in context.Brands on c.BrandId equals b.Id
                            join col in context.Colors on c.ColorId equals col.Id
                            where (c.Id==id)
                            select new CarDetailDto
                            {
                                CarId = c.Id,
                                CarName = c.CarName,
                                BrandName = b.Name,
                                ColorName = col.Name,
                                DailyPrice = c.DailyPrice,
                                ModelYear = c.ModelYear,
                                Description = c.Description,
                                CarImages = (from img in context.CarImages
                                             where (c.Id == img.CarId)
                                             select new CarImage { Id = img.Id, CarId = c.Id, Date = img.Date, ImagePath = img.ImagePath }).ToList()
                            };
                return result.SingleOrDefault();
            }
        }

        public List<CarDetailDto> GetCarsDetails()
        {
            using (RentACarContext context=new RentACarContext())
            {
              
                var result = from c in context.Cars
                             join b in context.Brands on c.BrandId equals b.Id
                             join col in context.Colors on c.ColorId equals col.Id
                             select new CarDetailDto { 
                                 CarId=c.Id,
                                 CarName = c.CarName, 
                                 BrandName = b.Name, 
                                 ColorName = col.Name, 
                                 DailyPrice = c.DailyPrice,
                                 ModelYear = c.ModelYear,
                                 Description = c.Description,
                                 CarImages = (from img in context.CarImages
                                              where (c.Id == img.CarId)
                                              select new CarImage { Id = img.Id, CarId = c.Id, Date = img.Date, ImagePath = img.ImagePath }).ToList()
                             };
                return result.ToList();
            }
        }

        public List<CarDetailDto> GetCarsDetailsByBrandId(int id)
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands on c.BrandId equals b.Id
                             join col in context.Colors on c.ColorId equals col.Id
                             where (b.Id == id)
                             select new CarDetailDto
                             {
                                 CarId = c.Id,
                                 CarName = c.CarName,
                                 BrandName = b.Name,
                                 ColorName = col.Name,
                                 DailyPrice = c.DailyPrice,
                                 ModelYear = c.ModelYear,
                                 Description = c.Description,
                                 CarImages = (from img in context.CarImages
                                              where (c.Id == img.CarId)
                                              select new CarImage { Id = img.Id, CarId = c.Id, Date = img.Date, ImagePath = img.ImagePath }).ToList()
                             };
                return result.ToList();
            }
        }
        public List<CarDetailDto> GetCarsDetailsByColorId(int id)
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands on c.BrandId equals b.Id
                             join col in context.Colors on c.ColorId equals col.Id
                             where (col.Id == id)
                             select new CarDetailDto
                             {
                                 CarId = c.Id,
                                 CarName = c.CarName,
                                 BrandName = b.Name,
                                 ColorName = col.Name,
                                 DailyPrice = c.DailyPrice,
                                 ModelYear = c.ModelYear,
                                 Description = c.Description,
                                 CarImages = (from img in context.CarImages
                                              where (c.Id == img.CarId)
                                              select new CarImage { Id = img.Id, CarId = c.Id, Date = img.Date, ImagePath = img.ImagePath }).ToList()
                             };
                return result.ToList();
            }
        }
    }
}
