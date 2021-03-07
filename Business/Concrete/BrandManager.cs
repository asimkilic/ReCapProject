using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        private IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public void Add(Brand brand)
        {
            if (IsValidName(brand))
            {
                _brandDal.Add(brand);
            }
            else
            {
                throw new Exception("Brand name must be equal or greater than 2 characters.");
            }
           
        }

        public void Delete(Brand brand)
        {
            _brandDal.Delete(brand);
        }

        public List<Brand> GetAll()
        {
          return  _brandDal.GetAll();

        }

        public Brand GetById(int brandId)
        {
           return _brandDal.Get(b=>b.Id==brandId);

        }

        public void Update(Brand brand)
        {
            _brandDal.Update(brand);
        }

        bool IsValidName(Brand brand)
        {
           return brand.Name.Trim().Length>=2?true:false;
        }
    }
}
