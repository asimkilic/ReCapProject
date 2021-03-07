using DataAccess.Abstract;
using Entities.Concrete;
using Core.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBrandDal:EfEntityRepositoryBase<Brand,RentACarContext>,IBrandDal
    {

    }
}
