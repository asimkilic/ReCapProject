using System.Collections.Generic;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private ICustomerDal _customerDal;
        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal=customerDal;
        }
        [SecuredOperation("admin,customer.admin,customer.add")]
        [ValidationAspect(typeof(CustomerValidator))]
        [CacheRemoveAspect("ICustomerService.Get")]
        public IResult Add(Customer customer)
        {
           _customerDal.Add(customer);
           return new SuccessResult(Messages.CustomerAdded);
        }
        [SecuredOperation("admin,customer.admin,customer.delete")]
        [ValidationAspect(typeof(CustomerValidator))]
        [CacheRemoveAspect("ICustomerService.Get")]
        public IResult Delete(Customer customer)
        {
           _customerDal.Delete(customer);
           return new SuccessResult(Messages.CustomerDeleted);
        }
        [SecuredOperation("admin,customer.admin,customer.getall")]
        public IDataResult<List<Customer>> GetAll()
        {
           return new SuccessDataResult<List<Customer>>(_customerDal.GetAll(),Messages.CustomerListed);
        }

        [SecuredOperation("admin,customer.admin,customer.update")]
        [ValidationAspect(typeof(CustomerValidator))]
        [CacheRemoveAspect("ICustomerService.Get")]
        public IResult Update(Customer customer)
        {
           _customerDal.Update(customer);
           return new SuccessResult(Messages.CustomerUpdated);
        }
    }
}