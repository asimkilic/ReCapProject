using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            // InMemoryTest();
            CarTest();
            //BrandTest();
            //ColorTest();
           


        }
        private static void CarTest()
        {
            CarManager carManager = new CarManager(new EfCarDal());
            //carManager.Add(new Car { BrandId = 1, ColorId = 1, CarName=  "E200", DailyPrice = 15, Description = "Good", ModelYear = 2004 });
            //carManager.Add(new Car { BrandId = 2, ColorId = 2, CarName = "Logan", DailyPrice = 25, Description = "Bad", ModelYear = 2005 });
            //carManager.Add(new Car { BrandId = 3, ColorId = 3, CarName = "Passat", DailyPrice = 5, Description = "Not Bad", ModelYear = 2006 });
            //carManager.Add(new Car { BrandId = 4, ColorId = 4, CarName = "Focus", DailyPrice = 35, Description = "Very Good", ModelYear = 2007 });
            //carManager.Add(new Car { BrandId = 5, ColorId = 5, CarName = "3.20", DailyPrice = 45, Description = "Strong", ModelYear = 2008 });

       
            Console.WriteLine("----------------------------# Details of Cars #---------------------------------------");
            foreach (var car in carManager.GetCarsDetails())
            {
           
                Console.WriteLine("{0} {1,-15} {2,-20} {3,-20} {4,-20} {5}", "|", "Brand Name", "Car Name", "Color","Daily Price","|");
                Console.WriteLine();
                Console.WriteLine("{0} {1,-15} {2,-20} {3,-20} {4,-20} {5}", "|", car.BrandName, car.CarName, car.ColorName,car.DailyPrice+"$", "|");
            Console.WriteLine("--------------------------------------------------------------------------------------");

            }
        }

        private static void BrandTest()
        {
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            //brandManager.Add(new Brand { Name = "Mercedes" });
            //brandManager.Add(new Brand { Name = "Dacia" });
            //brandManager.Add(new Brand { Name = "Wolkswagen" });
            //brandManager.Add(new Brand { Name = "Ford" });
            //brandManager.Add(new Brand { Name = "BMW" });

            foreach (var brand in brandManager.GetAll())
            {
               
                Console.WriteLine(brand.Id + "/" + brand.Name);
            }
        }
        private static void ColorTest()
        {
            ColorManager colorManager = new ColorManager(new EfColorDal());
            //colorManager.Add(new Color { Name = "Sarı"});
            //colorManager.Add(new Color { Name = "Mavi" });
            //colorManager.Add(new Color { Name = "Turuncu" });
            //colorManager.Add(new Color { Name = "Siyah" });
            //colorManager.Add(new Color { Name = "Beyaz" });

            foreach (var color in colorManager.GetAll())
            {

                Console.WriteLine(color.Id + "/" + color.Name);
            }
            colorManager.Delete(new Color {Id=1, Name = "Sarı" });
        }

        private static void InMemoryTest()
        {
            CarManager carService = new CarManager(new InMemoryCarDal());
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
            var carGetById = carService.GetById(1);
            Console.WriteLine("-------------------------# GetById #------------------------------------");
            Console.WriteLine("{0} {1,-5} {2,-10} {3,-10} {4,-10} {5,-13} {6,-15} {7}", "|", "Id", "Brand Id", "Color Id", "Model Year", "Daily Price", "Description", "|");
            Console.WriteLine();
            Console.WriteLine("{0} {1,-5} {2,-10} {3,-10} {4,-10} {5,-13} {6,-15} {7}", "|", carGetById.Id, carGetById.BrandId, carGetById.ColorId, carGetById.ModelYear, carGetById.DailyPrice + "$", carGetById.Description, "|");
            Console.WriteLine("------------------------------------------------------------------------");
            Console.WriteLine();

            Car car = new Car();
            Console.WriteLine("-------------------------# Add #------------------------------------");
            Console.WriteLine("Car Id:");
            car.Id = int.Parse(Console.ReadLine());

            Console.WriteLine("Brand Id:");
            car.BrandId = int.Parse(Console.ReadLine());

            Console.WriteLine("Color Id:");
            car.ColorId = int.Parse(Console.ReadLine());

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
