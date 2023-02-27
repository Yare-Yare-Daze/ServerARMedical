namespace Server.Services
{
    public interface IProfileService
    {
        void DoSomething();
    }


    public class ProfileService : IProfileService
    {
        public void DoSomething()
        {
            Console.WriteLine("Do something");
        }
    }

    public class MockProfileService : IProfileService
    {
        public void DoSomething()
        {
            Console.WriteLine("Do something, from mock service");
        }
    }
}
