using Core.DataAccess.EntityFramework;

using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, RentACarContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalsDetails()
        {
            using (RentACarContext context = new RentACarContext())
            {
                //CarName, BrandName, ColorName, DailyPrice.
                var result = from r in context.Rentals
                             join c in context.Cars on r.CarId equals c.Id
                             join brand in context.Brands on c.BrandId equals brand.Id
                             join cus in context.Customers on r.CustomerId equals cus.Id
                             select new RentalDetailDto {RentalId=r.Id,BrandName=brand.Name,CustomerFullName=cus.CompanyName,RentDate=r.RentDate,ReturnDate=r.ReturnDate };

             
                return result.ToList();


            }
        }
    }
}