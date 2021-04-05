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
using Business.ServiceAdapters;
using Core.Aspects.Autofac.Transaction;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        private IRentalDal _rentalDal;
        private ICreditCardService _creditCardService;

        public RentalManager(IRentalDal rentalDal, ICreditCardService creditCardService)
        {
            _rentalDal = rentalDal;
            _creditCardService = creditCardService;
        }
       // [SecuredOperation("admin,rental.admin,rental.add")]
        [ValidationAspect(typeof(RentalValidator))]
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Add(Rental rental)
        {
            var result=BusinessRules.Run(CheckRentalDays(rental));
            if (result==null)
            {
                _rentalDal.Add(rental);
                return new SuccessResult();
            }
            return new ErrorResult(Messages.RentalAddError);
        
        }

        public IResult CheckCarRentByCarId(Rental rental)
        {
            var result = BusinessRules.Run(CheckRentalDays(rental));
            if (result==null)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
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
       
        public IResult RentWithCreditCard(RentWithCreditCard rentWithCreditCard)
        {
            if (_creditCardService.ValidatePayment(rentWithCreditCard))
            {

                this.Add(rentWithCreditCard.Rental);
                return new SuccessResult(Messages.PaymentSuccess);
            }
            return new ErrorResult(Messages.PoorUser);
        }

        //  [SecuredOperation("admin,rental.admin,rental.update")]
        [ValidationAspect(typeof(RentalValidator))]
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult();
        }
       

      
       
        private IResult CheckRentalDays(Rental rentalRequest)
        {
            List<Rental> result = _rentalDal.GetAll(x => x.CarId == rentalRequest.CarId && (x.RentDate > DateTime.Now ));
            if (result==null)
            {
                return new SuccessResult(); //araba gelecekte kirada olmayacak direk bo�
            }
            foreach (var rental in result)
            {
                if (rental.ReturnDate==null)
                {
                    //db return null ise
                    if (rentalRequest.ReturnDate== null)
                    {
                        //hem db return hem kullan�c� return  null ise
                        //db'de s�resiz kayd� olan ileri bir tarihte alacakta olsa d�n��� belli de�il o sebeple daha erken tarihte al�p s�resiz alacak birisine verilmez.
                        return new ErrorResult();
                    }
                    else
                    {
                        //db return null kullan�c� return dolu 
                        TimeSpan differenceTime = (TimeSpan)(rental.RentDate - rentalRequest.ReturnDate);
                        double differenceDays = differenceTime.TotalDays;
                        if (differenceDays<=0)
                        {
                            return new ErrorResult(); 
                        }
                       

                    }
                }
                else
                {
                    //db returndate null de�ilse
                    if (rentalRequest.ReturnDate== null)
                    {
                        //db return dolu  kulllan�c� return null
                        bool isAvailable = false;
                        foreach (var returnDate in result) // gelecek tarihe 1'den fazla kay�t varsa ilkinde returndate verilmi� ikincisinde verilmemi�se diye kontrol hepsine yap�yoruz. �ncelik first in first out(paray� veren d�d��� �alar)
                        {
                            if (returnDate.ReturnDate==null)
                            {
                                isAvailable = false;
                                break;
                            }
                            TimeSpan differenceTime = (TimeSpan)(rentalRequest.RentDate - returnDate.ReturnDate);
                            double differenceDays = differenceTime.TotalDays;

                            if (differenceDays > 0)
                            {
                                isAvailable = true;
                            }
                            else
                            {
                                isAvailable = false;

                            }

                        }
                        if (isAvailable==false)
                        {
                            return new ErrorResult();
                        }
                     

                    }
                    else
                    {
                        //db return 'de kullan�c� return'de dolu
                        //kullan�c�dan gelen tarihleri gez i�inde Db'den gelen al�� tarihi varsa false
                        DateTime tempStartDate = rentalRequest.RentDate;
                        DateTime tempEndDate = (DateTime)rental.ReturnDate;
                        tempEndDate = tempEndDate.AddDays(-1);

                        while (tempStartDate< rentalRequest.ReturnDate)
                        {
                            if (tempStartDate==rental.RentDate || tempStartDate==tempEndDate)
                            {
                                return new ErrorResult();
                                
                            }
                           tempStartDate= tempStartDate.AddDays(1);
                           
                        }
                        

                    }
                }

            }
            return new SuccessResult();
        }
    }
}