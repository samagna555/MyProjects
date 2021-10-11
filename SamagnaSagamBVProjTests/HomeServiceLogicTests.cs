using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SamagnaSagamBVProj.BusinessLogic;
using SamagnaSagamBVProj.Models;
using SamagnaSagamBVProjTests.Mocks;

namespace SamagnaSagamBVProjTests
{
    [TestClass]
    public class HomeServiceLogicTests
    {
        [TestMethod]
        public void UsersCount_GreaterThan_Zero_Test()
        {
            var homeServiceLogic = new MockedHomeServiceLogic();
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            httpResponse.StatusCode = HttpStatusCode.OK;

            var json = JsonConvert.SerializeObject(homeServiceLogic.produceHomeData());
            httpResponse.Content = new StringContent(json);

            string result = homeServiceLogic.serviceLogic.GetDataByAge(httpResponse);
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public void UsersCount_Equals_Zero_When_BadRequest_Test()
        {
            var homeServiceLogic = new MockedHomeServiceLogic();
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            httpResponse.StatusCode = HttpStatusCode.BadRequest;

            var json = JsonConvert.SerializeObject(homeServiceLogic.produceHomeData());
            httpResponse.Content = new StringContent(json);

            string result = homeServiceLogic.serviceLogic.GetDataByAge(httpResponse);
            Assert.IsTrue(homeServiceLogic.FakeLogger.Messages.Any(c => c.Message.Contains("Response status code does not indicate success: 400")));
        }

        [TestMethod]
        public void Send401ErrorCode_When_Unauthorized_Test()
        {
            var homeServiceLogic = new MockedHomeServiceLogic();
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            httpResponse.StatusCode = HttpStatusCode.Unauthorized;

            var json = JsonConvert.SerializeObject(homeServiceLogic.produceHomeData());
            httpResponse.Content = new StringContent(json);

            string result = homeServiceLogic.serviceLogic.GetDataByAge(httpResponse);
            Assert.IsTrue(homeServiceLogic.FakeLogger.Messages.Any(c => c.Message.Contains("Response status code does not indicate success: 401")));
        }

        [TestMethod]
        public void Send401ErrorCode_ReturnResult_Empty_Test()
        {
            var homeServiceLogic = new MockedHomeServiceLogic();
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            httpResponse.StatusCode = HttpStatusCode.Unauthorized;

            var json = JsonConvert.SerializeObject(homeServiceLogic.produceHomeData());
            httpResponse.Content = new StringContent(json);

            string result = homeServiceLogic.serviceLogic.GetDataByAge(httpResponse);
            Assert.AreEqual(string.Empty, result);
        }


        [TestMethod]
        public void GetUserCount_ForAge_12_Test()
        {
            var homeServiceLogic = new MockedHomeServiceLogic();
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            httpResponse.StatusCode = HttpStatusCode.OK;

            var json = JsonConvert.SerializeObject(homeServiceLogic.produceHomeData());
            httpResponse.Content = new StringContent(json);

            string result = homeServiceLogic.serviceLogic.GetDataByAge(httpResponse);
            Assert.IsTrue(result.Contains("12:2"));
        }

        [TestMethod]
        public void GetUserCount_ForAge_23_Test()
        {
            var homeServiceLogic = new MockedHomeServiceLogic();
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            httpResponse.StatusCode = HttpStatusCode.OK;

            var json = JsonConvert.SerializeObject(homeServiceLogic.produceHomeData());
            httpResponse.Content = new StringContent(json);

            string result = homeServiceLogic.serviceLogic.GetDataByAge(httpResponse);
            Assert.IsTrue(result.Contains("23:1"));
        }


        [TestMethod]
        public void Get_Success_Connection_to_the_URL_Test()
        {
            var homeServiceLogic = new MockedHomeServiceLogic();
            homeServiceLogic.SetPath();
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            
            var json = JsonConvert.SerializeObject(homeServiceLogic.produceHomeData());
            httpResponse.Content = new StringContent(json);

             httpResponse = homeServiceLogic.serviceLogic.GetConnection().GetAwaiter().GetResult();
            Assert.AreEqual(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [TestMethod]
        public void Get_Wrong_Correction_to_the_URL_Test()
        {
            var homeServiceLogic = new MockedHomeServiceLogic();
            homeServiceLogic.SetPath();
            homeServiceLogic.mockedHomeConfig.Object.url = "https://localhost:8080";
            HttpResponseMessage httpResponse = new HttpResponseMessage();

            var json = JsonConvert.SerializeObject(homeServiceLogic.produceHomeData());
            httpResponse.Content = new StringContent(json);

            httpResponse = homeServiceLogic.serviceLogic.GetConnection().GetAwaiter().GetResult();
            Assert.AreEqual(null, httpResponse);
        }

        [TestMethod]
        public void Get_LoggerInfo_For_Wrong_Correction_to_the_URL_Test()
        {
            var homeServiceLogic = new MockedHomeServiceLogic();
            homeServiceLogic.SetPath();
            homeServiceLogic.mockedHomeConfig.Object.url = "https://localhost:8080";
            HttpResponseMessage httpResponse = new HttpResponseMessage();

            var json = JsonConvert.SerializeObject(homeServiceLogic.produceHomeData());
            httpResponse.Content = new StringContent(json);

            httpResponse = homeServiceLogic.serviceLogic.GetConnection().GetAwaiter().GetResult();
            Assert.IsTrue(homeServiceLogic.FakeLogger.Messages.Any(s => s.Message.Contains("Invalid URI"))); ;
        }
    }
}
