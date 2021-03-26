using System.Collections.Generic;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Linq;
using Business.Constants;
using System;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Business.BusinessAspects.Autofac;
using Core.Aspects.Autofac.Caching;
using Entities.DTOs;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        private IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }
       // [SecuredOperation("admin,rental.admin,rental.add")]
        [ValidationAspect(typeof(RentalValidator))]
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Add(Rental rental)
        {
            var result=BusinessRules.Run(CheckRentalAvailable(rental.CarId));
            if (result==null)
            {
                _rentalDal.Add(rental);
                return new SuccessResult();
            }
            return new ErrorResult(Messages.RentalAddError);
        
        }
      //  [SecuredOperation("admin,rental.admin,rental.delete")]
        [ValidationAspect(typeof(RentalValidator))]
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult();
        }
       // [SecuredOperation("admin,rental.admin,rental.getall")]
        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }
        // [SecuredOperation("admin,rental.admin,rental.getrentaldetails")]
        public IDataResult<List<RentalDetailDto>> GetRentalsDetails()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalsDetails());
        }

        //  [SecuredOperation("admin,rental.admin,rental.update")]
        [ValidationAspect(typeof(RentalValidator))]
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult();
        }
        private IResult CheckRentalAvailable(int carId)
        {
            Rental result = ((_rentalDal.GetAll(x => x.CarId == carId)).OrderByDescending(x => x.RentDate)).FirstOrDefault();
            if (result == null)
            {
                return new SuccessResult();

            }
            if ((result.ReturnDate.HasValue) && (DateTime.Compare(DateTime.Now, (DateTime)result.ReturnDate) > 0))
            {

                return new SuccessResult();

            }
            return new ErrorResult(Messages.RentalAddError);

        }
    }
}