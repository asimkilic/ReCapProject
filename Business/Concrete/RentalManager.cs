using System.Collections.Generic;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Linq;
using Business.Constants;
using System;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        private IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        public IResult Add(Rental rental)
        {
            Rental result = ((_rentalDal.GetAll(x => x.CarId == rental.CarId)).OrderByDescending(x=>x.RentDate)).FirstOrDefault();
            if (result==null)
            {
                _rentalDal.Add(rental);
                return new SuccessResult();

            }
            if ((result.ReturnDate.HasValue) && (DateTime.Compare(DateTime.Now, (DateTime)result.ReturnDate) > 0))
            {
             
                   _rentalDal.Add(rental);
                   return new SuccessResult();
                
            }
            return new ErrorResult(Messages.RentalAddError);
        
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult();
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult();
        }

    }
}