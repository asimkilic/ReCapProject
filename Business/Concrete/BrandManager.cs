using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
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

        [ValidationAspect(typeof(BrandValidator))]
        [SecuredOperation("admin,brand.admin,brand.add")]
        [CacheRemoveAspect("IBrandService.Get")]
        public IResult Add(Brand brand)
        {
             _brandDal.Add(brand);
             return new SuccessResult(Messages.ProductAdded);
        }
        [ValidationAspect(typeof(BrandValidator))]
        [SecuredOperation("admin,brand.admin,brand.delete")]
        [CacheRemoveAspect("IBrandService.Get")]
        public IResult Delete(Brand brand)
        {
            _brandDal.Delete(brand);
            return new SuccessResult();
        }
        [SecuredOperation("admin,brand.admin,brand.getall")]
        [CacheAspect]
        public IDataResult<List<Brand>> GetAll()
        {
          return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(),Messages.ProductListed);
        }
        [SecuredOperation("admin,brand.admin,brand.getbyid")]
        public IDataResult<Brand> GetById(int brandId)
        {
           return new SuccessDataResult<Brand>(_brandDal.Get(b=>b.Id==brandId));

        }
        [SecuredOperation("admin,brand.admin,brand.update")]
        [ValidationAspect(typeof(BrandValidator))]
        [CacheRemoveAspect("IBrandService.Get")]
        public IResult Update(Brand brand)
        {
            _brandDal.Update(brand);
            return new SuccessResult();
        }

      
    }
}
