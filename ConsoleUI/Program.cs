using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carService = new CarManager(new InMemoryCarDal());
            Console.WriteLine("-------#### Cars ####--------");
           
            foreach (var c in carService.GetAll())
            {

                //Id, BrandId, ColorId, ModelYear, DailyPrice, Description
                Console.WriteLine("{0,-15} {1,-1} {2,-9} {3,-2}", "# Car ID",":", c.Id,"#");
                Console.WriteLine("{0,-15} {1,-1} {2,-9} {3,-2}", "# Brand ID", ":", c.BrandId, "#");
                Console.WriteLine("{0,-15} {1,-1} {2,-9} {3,-2}", "# Color ID", ":", c.ColorId, "#");
                Console.WriteLine("{0,-15} {1,-1} {2,-9} {3,-2}", "# Model Year", ":", c.ModelYear, "#");
                Console.WriteLine("{0,-15} {1,-1} {2,-9} {3,-2}", "# Daily Price", ":", c.DailyPrice + "$", "#");
                Console.WriteLine("{0,-15} {1,-1} {2,-9} {3,-2}", "# Description", ":", c.Description, "#");
                Console.WriteLine("-----------------------------");

            }
            var carGetById = carService.GetById(1); 
            Console.WriteLine("-------------------------# GetById #------------------------------------");
            Console.WriteLine("{0} {1,-5} {2,-10} {3,-10} {4,-10} {5,-13} {6,-15} {7}", "|", "Id", "Brand Id", "Color Id", "Model Year", "Daily Price","Description", "|");
            Console.WriteLine();
            Console.WriteLine("{0} {1,-5} {2,-10} {3,-10} {4,-10} {5,-13} {6,-15} {7}", "|", carGetById.Id, carGetById.BrandId, carGetById.ColorId, carGetById.ModelYear, carGetById.DailyPrice+"$",carGetById.Description, "|");
            Console.WriteLine("------------------------------------------------------------------------");
            Console.WriteLine();

            Car car = new Car();
            Console.WriteLine("-------------------------# Add #------------------------------------");
            Console.WriteLine("Car Id:");
            car.Id = int.Parse(Console.ReadLine());

            Console.WriteLine("Brand Id:");
            car.BrandId= int.Parse(Console.ReadLine());

            Console.WriteLine("Color Id:");
            car.ColorId= int.Parse(Console.ReadLine());

            Console.WriteLine("Model Year:");
            car.ModelYear = int.Parse(Console.ReadLine());

            Console.WriteLine("Daily Price:");
            car.DailyPrice = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Description:");
            car.Description = Console.ReadLine();

            carService.Add(car);
            Console.WriteLine();
            Console.WriteLine("-------#### Cars ####--------");

            foreach (var c in carService.GetAll())
            {

                //Id, BrandId, ColorId, ModelYear, DailyPrice, Description
                Console.WriteLine("{0,-15} {1,-1} {2,-9} {3,-2}", "# Car ID", ":", c.Id, "#");
                Console.WriteLine("{0,-15} {1,-1} {2,-9} {3,-2}", "# Brand ID", ":", c.BrandId, "#");
                Console.WriteLine("{0,-15} {1,-1} {2,-9} {3,-2}", "# Color ID", ":", c.ColorId, "#");
                Console.WriteLine("{0,-15} {1,-1} {2,-9} {3,-2}", "# Model Year", ":", c.ModelYear, "#");
                Console.WriteLine("{0,-15} {1,-1} {2,-9} {3,-2}", "# Daily Price", ":", c.DailyPrice + "$", "#");
                Console.WriteLine("{0,-15} {1,-1} {2,-9} {3,-2}", "# Description", ":", c.Description, "#");
                Console.WriteLine("-----------------------------");

            }














        }
    }
}
