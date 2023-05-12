//SOLID

#region 1 - Single Responsibility Principle
//public class Car
//{
//    public void Drive() { }

//    public void FixEngine() { }
//}

//public class Driver
//{
//    public void Drive(Car car)
//    {
//        car.Drive();
//    }
//}
#endregion

#region 2 - Open - Closed Principle
//public abstract class Car
//{
//    public string Make { get; set; }
//    public string Model { get; set; }
//    public double Speed { get; set; }

//    public virtual void Accelerate(double amount) { }
//    public virtual void Brake(double amount) { }
//}

//public class SportsCar : Car
//{

//    public override void Accelerate(double amount) { }

//    public override void Brake(double amount) { }

//    public void Drift() { }
//}

#endregion

#region 3 - Liskov Substitution Principle
//public class Car
//{
//    public virtual void StartEngine() { }

//    public virtual void Drive() { }
//}

//public class ElectricCar : Car
//{
//    public override void StartEngine() { }

//    public override void Drive() { }
//}

//public class GasolineCar : Car
//{
//    public override void StartEngine() { }

//    public override void Drive() { }
//}

//public class Driver
//{
//    public Car car = new ElectricCar();
//    public void DriveCar(Car car)
//    {
//        car.StartEngine();
//        car.Drive();
//    }
//}

#endregion

#region 4 - Interface Segregation Principle
//public interface IEngine
//{
//    void Start();
//    void Stop();
//}

//public interface IWheel
//{
//    void UseBreaks();
//}

//public interface ICar
//{
//    void Drive();
//    void Stop();
//}

//public class Engine : IEngine
//{
//    public void Start()
//    {
//        Console.WriteLine("Started engine!!!");
//    }

//    public void Stop()
//    {
//        Console.WriteLine("Stopped engine!!!");
//    }
//}

//public class Wheel : IWheel
//{
//    public void UseBreaks()
//    {
//        Console.WriteLine("Breaks");
//    }
//}

//public class Car : ICar
//{
//    private readonly IEngine _engine;
//    private readonly IWheel _wheel;

//    public Car(IEngine engine, IWheel wheel)
//    {
//        _engine = engine;
//        _wheel = wheel;
//    }

//    public void Drive()
//    {
//        _engine.Start();
//    }

//    public void Stop()
//    {
//        _engine.Stop();
//        _wheel.UseBreaks();
//    }
//}

#endregion

#region 5 - Dependency Inversion Principle
//public interface IEngine
//{
//    void Start();
//    void Stop();
//}

//public interface IWheel
//{
//    void UseBreaks();
//}

//public interface ICar
//{
//    void Drive();
//    void Stop();
//}

//public class Engine : IEngine
//{
//    public void Start()
//    {
//        Console.WriteLine("Starting engine...");
//    }

//    public void Stop()
//    {
//        Console.WriteLine("Stopping engine...");
//    }
//}

//public class Wheel : IWheel
//{
//    public void UseBreaks()
//    {
//        Console.WriteLine("Breaks");
//    }
//}

//public class Car : ICar
//{
//    private readonly IEngine _engine;
//    private readonly IWheel _wheel;

//    public Car(IEngine engine, IWheel wheel)
//    {
//        _engine = engine;
//        _wheel = wheel;
//    }

//    public void Drive()
//    {
//        _engine.Start();
//    }

//    public void Stop()
//    {
//        _engine.Stop();
//        _wheel.UseBreaks();
//    }
//}

//public class CarController
//{
//    private readonly ICar _car;

//    public CarController(ICar car)
//    {
//        _car = car;
//    }

//    public void DriveCar()
//    {
//        _car.Drive();
//    }

//    public void StopCar()
//    {
//        _car.Stop();
//    }
//}

#endregion