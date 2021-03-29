using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        private ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }
     //   [SecuredOperation("admin,carimage.admin,carimage.add")]
        [CacheRemoveAspect("ICarImageService.Get")]
        [ValidationAspect(typeof(CarImageValidator))]
        [TransactionScopeAspect]
        public IResult Add(IFormFile file, CarImage carImage)
        {
            
            IResult result = BusinessRules.Run(CheckImageLimitExceeded(carImage.CarId));
            if (result!=null)
            {
                return result;
            }
            
            carImage.ImagePath = FileHelper.Add(file);
            carImage.Date = DateTime.Now;
            _carImageDal.Add(carImage);
            return new SuccessResult(Messages.ImageAdded);
        }
       
        [CacheRemoveAspect("ICarImageService.Get")]
     //   [SecuredOperation("admin,carimage.admin,carimage.delete")]
        [ValidationAspect(typeof(CarImageValidator))]
        [TransactionScopeAspect]
        public IResult Delete(CarImage carImage)
        {
            FileHelper.Delete(carImage.ImagePath);
            _carImageDal.Delete(carImage);
            
            return new SuccessResult();
        }

      // [SecuredOperation("admin,carimage.admin,carimage.get")]
        public IDataResult<CarImage> Get(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(p => p.Id == id));
        }
      //  [SecuredOperation("admin,carimage.admin,carimage.getall")]
       // [CacheAspect]
        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }
        [SecuredOperation("admin,carimage.admin,carimage.getimagesbycarid")]
        public IDataResult<List<CarImage>> GetImagesByCarId(int id)
        {
            return ReturnImagesNullCheckedByCarId(id);
        }
        [ValidationAspect(typeof(CarImageValidator))]
     //   [SecuredOperation("admin,carimage.admin,carimage.update")]
        [CacheRemoveAspect("ICarImageService.Get")]
        [TransactionScopeAspect]
        public IResult Update(IFormFile file, CarImage carImage)
        {
            FileHelper.Delete(GetImagePathByImageId(carImage.Id));
            carImage.ImagePath = FileHelper.Add(file);
            carImage.Date = DateTime.Now;
            _carImageDal.Update(carImage);
            return new SuccessResult(Messages.ImageUpdated);
        
        }

        private IResult CheckImageLimitExceeded(int carId)
        {
            var carImageCount = _carImageDal.GetAll(p => p.CarId == carId).Count;
            if(carImageCount<=5)
            {
                return new SuccessResult();
            }
            return new ErrorResult(Messages.CarImageLimitExceeded);
        }
        private IResult CheckIfCarImageNull(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId).Any();
            if (result)
            {
                return new ErrorResult();
               
            }
            return new SuccessResult();
        }
        private IDataResult<List<CarImage>> ReturnImagesNullCheckedByCarId(int carId)
        {
            List<CarImage> carImages = new List<CarImage>();
            if (CheckIfCarImageNull(carId).Success)
            {
                string path = @"\Uploads\default.jpg";
                carImages.Add(new CarImage { CarId = carId, ImagePath = path });
                return new SuccessDataResult<List<CarImage>>(carImages);
            }
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(c => c.CarId == carId));


        }
        private string GetImagePathByImageId(int id)
        {
            return _carImageDal.Get(p => p.Id == id).ImagePath;
        }

       
    }
}
