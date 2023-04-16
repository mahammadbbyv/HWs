using Adapter_Pattern;

Car car = new(new PetrolEngine());
car.Start();
car.Stop();
car = new(new ElectricEngine());
car.Start();
car.Stop();

