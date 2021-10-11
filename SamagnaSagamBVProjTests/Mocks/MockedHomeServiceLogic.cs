using System;
using SamagnaSagamBVProj.BusinessLogic;
using SamagnaSagamBVProj.Models;
using Moq;


namespace SamagnaSagamBVProjTests.Mocks
{
    public class MockedHomeServiceLogic
    {
        public FakeLogger<HomeServiceLogic> FakeLogger { get; }
        public Mock<HomeConfig> mockedHomeConfig { get; }
        public HomeServiceLogic serviceLogic { get; }

        public MockedHomeServiceLogic()
        {
            FakeLogger = new FakeLogger<HomeServiceLogic>();
            mockedHomeConfig = new Mock<HomeConfig>();
            serviceLogic = new HomeServiceLogic(FakeLogger, mockedHomeConfig.Object);

        }

        public Userss produceHomeData()
        {
            return new Userss()
            {
                Users = new System.Collections.Generic.List<UserInfo>()
                {
                    new UserInfo(){id = "1", age = "12"},
                    new UserInfo(){id = "2", age = "23" },
                    new UserInfo(){id = "3", age = "12"},
                    
                }
            };
        }


        public void SetPath()
        {
            mockedHomeConfig.Object.url = "https://tupleschallenge.blob.core.windows.net/";
            mockedHomeConfig.Object.path = "interview/age_data.json";

        }
    }
}
