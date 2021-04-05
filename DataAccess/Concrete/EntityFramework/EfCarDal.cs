using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Entities.DTOs;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, RentACarContext>, ICarDal
    {
        public List<CarDetailDto> GetCarsWithByColorIdAndBrandId(int colorId, int brandId)
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands on c.BrandId equals b.Id
                             join col in context.Colors on c.ColorId equals col.Id
                             join seg in context.Segments on c.SegmentId equals seg.Id

                             where (b.Id == brandId && col.Id==colorId)
                             select new CarDetailDto
                             {
                                 CarId = c.Id,
                                 CarName = c.CarName,
                                 BrandName = b.Name,
                                 ColorName = col.Name,
                                 DailyPrice = c.DailyPrice,
                                 ModelYear = c.ModelYear,
                                 Description = c.Description,
                                 SegmentClass = seg.SegmentClass,
                                 SegmentId=seg.Id,
                                 CarImages = (from img in context.CarImages
                                              where (c.Id == img.CarId)
                                              select new CarImage { Id = img.Id, CarId = c.Id, Date = img.Date, ImagePath = img.ImagePath }).ToList(),
                                 CarFeature = (from feature in context.CarFeatures
                                               where (feature.Id == c.CarFeaturesId)
                                               select new CarFeature
                                               {
                                                   CarId = feature.CarId,
                                                   Fuel = feature.Fuel,
                                                   Id = feature.Id,
                                                   Luggage = feature.Luggage,
                                                   Mileage = feature.Mileage,
                                                   Seats = feature.Seats,
                                                   Transmission = feature.Transmission
                                               }).SingleOrDefault()
                             };
                return result.ToList();
            }
        }


        public CarDetailDto GetCarDetailsById(int id)
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands on c.BrandId equals b.Id
                             join col in context.Colors on c.ColorId equals col.Id
                             join seg in context.Segments on c.SegmentId equals seg.Id
                             where (c.Id == id)
                             select new CarDetailDto
                             {
                                 CarId = c.Id,
                                 CarName = c.CarName,
                                 BrandName = b.Name,
                                 ColorName = col.Name,
                                 DailyPrice = c.DailyPrice,
                                 ModelYear = c.ModelYear,
                                 Description = c.Description,
                                 SegmentClass = seg.SegmentClass,
                                 SegmentId = seg.Id,
                                 CarImages = (from img in context.CarImages
                                              where (c.Id == img.CarId)
                                              select new CarImage { Id = img.Id, CarId = c.Id, Date = img.Date, ImagePath = img.ImagePath }).ToList(),
                                 CarFeature = (from feature in context.CarFeatures
                                               where (feature.Id == c.CarFeaturesId)
                                               select new CarFeature
                                               {
                                                   CarId = feature.CarId,
                                                   Fuel = feature.Fuel,
                                                   Id = feature.Id,
                                                   Luggage = feature.Luggage,
                                                   Mileage = feature.Mileage,
                                                   Seats = feature.Seats,
                                                   Transmission = feature.Transmission
                                               }).SingleOrDefault()

                             };
                return result.SingleOrDefault();
            }
        }

        public List<CarDetailDto> GetCarsDetails()
        {
            using (RentACarContext context = new RentACarContext())
            {

                var result = from c in context.Cars
                             join b in context.Brands on c.BrandId equals b.Id
                             join col in context.Colors on c.ColorId equals col.Id
                             join seg in context.Segments on c.SegmentId equals seg.Id
                             select new CarDetailDto
                             {
                                 CarId = c.Id,
                                 CarName = c.CarName,
                                 BrandName = b.Name,
                                 ColorName = col.Name,
                                 DailyPrice = c.DailyPrice,
                                 ModelYear = c.ModelYear,
                                 Description = c.Description,
                                 SegmentClass = seg.SegmentClass,
                                 SegmentId = seg.Id,
                                 CarImages = (from img in context.CarImages
                                              where (c.Id == img.CarId)
                                              select new CarImage { Id = img.Id, CarId = c.Id, Date = img.Date, ImagePath = img.ImagePath }).ToList(),
                                 CarFeature = (from feature in context.CarFeatures
                                               where (feature.Id == c.CarFeaturesId)
                                               select new CarFeature
                                               {
                                                   CarId = feature.CarId,
                                                   Fuel = feature.Fuel,
                                                   Id = feature.Id,
                                                   Luggage = feature.Luggage,
                                                   Mileage = feature.Mileage,
                                                   Seats = feature.Seats,
                                                   Transmission = feature.Transmission
                                               }).SingleOrDefault()
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
                             join seg in context.Segments on c.SegmentId equals seg.Id

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
                                 SegmentClass = seg.SegmentClass,
                                 SegmentId = seg.Id,
                                 CarImages = (from img in context.CarImages
                                              where (c.Id == img.CarId)
                                              select new CarImage { Id = img.Id, CarId = c.Id, Date = img.Date, ImagePath = img.ImagePath }).ToList(),
                                 CarFeature = (from feature in context.CarFeatures
                                               where (feature.Id == c.CarFeaturesId)
                                               select new CarFeature
                                               {
                                                   CarId = feature.CarId,
                                                   Fuel = feature.Fuel,
                                                   Id = feature.Id,
                                                   Luggage = feature.Luggage,
                                                   Mileage = feature.Mileage,
                                                   Seats = feature.Seats,
                                                   Transmission = feature.Transmission
                                               }).SingleOrDefault()
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
                             join seg in context.Segments on c.SegmentId equals seg.Id

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
                                 SegmentClass = seg.SegmentClass,
                                 SegmentId = seg.Id,
                                 CarImages = (from img in context.CarImages
                                              where (c.Id == img.CarId)
                                              select new CarImage { Id = img.Id, CarId = c.Id, Date = img.Date, ImagePath = img.ImagePath }).ToList(),
                                 CarFeature = (from feature in context.CarFeatures
                                               where (feature.Id == c.CarFeaturesId)
                                               select new CarFeature
                                               {
                                                   CarId = feature.CarId,
                                                   Fuel = feature.Fuel,
                                                   Id = feature.Id,
                                                   Luggage = feature.Luggage,
                                                   Mileage = feature.Mileage,
                                                   Seats = feature.Seats,
                                                   Transmission = feature.Transmission
                                               }).SingleOrDefault()
                             };
                return result.ToList();
            }
        }

      
        public List<CarDetailDto> GetRelatedCarsBySegmentId(int segmentId)
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands on c.BrandId equals b.Id
                             join col in context.Colors on c.ColorId equals col.Id
                             join seg in context.Segments on c.SegmentId equals seg.Id

                             where (c.SegmentId==segmentId)
                             select new CarDetailDto
                             {
                                 CarId = c.Id,
                                 CarName = c.CarName,
                                 BrandName = b.Name,
                                 ColorName = col.Name,
                                 DailyPrice = c.DailyPrice,
                                 ModelYear = c.ModelYear,
                                 Description = c.Description,
                                 SegmentClass = seg.SegmentClass,
                                 SegmentId = seg.Id,
                                 CarImages = (from img in context.CarImages
                                              where (c.Id == img.CarId)
                                              select new CarImage { Id = img.Id, CarId = c.Id, Date = img.Date, ImagePath = img.ImagePath }).ToList(),
                                 CarFeature = (from feature in context.CarFeatures
                                               where (feature.Id == c.CarFeaturesId)
                                               select new CarFeature
                                               {
                                                   CarId = feature.CarId,
                                                   Fuel = feature.Fuel,
                                                   Id = feature.Id,
                                                   Luggage = feature.Luggage,
                                                   Mileage = feature.Mileage,
                                                   Seats = feature.Seats,
                                                   Transmission = feature.Transmission
                                               }).SingleOrDefault()
                             };
                return result.ToList();
            }
        }
    }
}
