using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamuraiCore.WebApp
{
    public class Tests1
    {
    }

    public class ThirdPartyServiceSimfyTests
    {


        [TestMethod]
        public void Contrsuct_Verify_Defaults()
        {
            var mockingKernel = new MoqMockingKernel();

            //Arrange
            var mockConfig = mockingKernel.GetMock<IConfig>();
            mockConfig.SetupGet(p => p.SimfyBaseUrl).Returns("SimfyBaseUrl");
            mockConfig.SetupGet(p => p.SimfyApiUsername).Returns("SimfyApiUsername");
            mockConfig.SetupGet(p => p.SimfyApiPassword).Returns("SimfyApiPassword");
            mockConfig.SetupGet(p => p.SimfyEmail).Returns("SimfyEmail");

            var service = new ThirdPartyServiceSimfy(mockingKernel);

            //Act
            PrivateObject privateObject = new PrivateObject(service);
            var SimfyBaseUrl = privateObject.GetFieldOrProperty("SimfyBaseUrl", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var SimfyApiUsername = privateObject.GetFieldOrProperty("SimfyApiUsername", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var SimfyApiPassword = privateObject.GetFieldOrProperty("SimfyApiPassword", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var SimfyEmail = privateObject.GetFieldOrProperty("SimfyEmail", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            //Assert
            mockConfig.VerifyGet(p => p.SimfyBaseUrl, Times.Once);
            mockConfig.VerifyGet(p => p.SimfyApiUsername, Times.Once);
            mockConfig.VerifyGet(p => p.SimfyApiPassword, Times.Once);
            mockConfig.VerifyGet(p => p.SimfyEmail, Times.Once);

            Assert.AreEqual("SimfyBaseUrl", SimfyBaseUrl);
            Assert.AreEqual("SimfyApiUsername", SimfyApiUsername);
            Assert.AreEqual("SimfyApiPassword", SimfyApiPassword);
            Assert.AreEqual("SimfyEmail", SimfyEmail);
        }


        [TestMethod]
        public void AccountCreation_Given_Model_Should_SendPOSTRequest()
        {
            var mockingKernel = new MoqMockingKernel();

            //Arrange
            var mockConfig = mockingKernel.GetMock<IConfig>();
            mockConfig.SetupGet(p => p.SimfyBaseUrl).Returns("SimfyBaseUrl");
            mockConfig.SetupGet(p => p.SimfyApiUsername).Returns("SimfyApiUsername");
            mockConfig.SetupGet(p => p.SimfyApiPassword).Returns("SimfyApiPassword");
            mockConfig.SetupGet(p => p.SimfyEmail).Returns("SimfyEmail");

            var mockHttpUtility = mockingKernel.GetMock<IHttpUtility>();


            var service = new ThirdPartyServiceSimfy(mockingKernel);
            PrivateObject privateObject = new PrivateObject(service);
            var SimfyBaseUrl = privateObject.GetFieldOrProperty("SimfyBaseUrl", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var SimfyApiUsername = privateObject.GetFieldOrProperty("SimfyApiUsername", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var SimfyApiPassword = privateObject.GetFieldOrProperty("SimfyApiPassword", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var SimfyEmail = privateObject.GetFieldOrProperty("SimfyEmail", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            var model = new SimfyAccountCreateRequest();
            var simfy = SimfyBaseUrl.ToString() + "/create"; ;
            HttpResponseMessage value = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            SimfyAccountCreationResponse accountResponse = new SimfyAccountCreationResponse();

            value.Content = new StringContent(JsonConvert.SerializeObject(accountResponse));
            value.Content.Headers.Clear();
            value.Content.Headers.Add("Content-Type", "application/json");

            mockHttpUtility.Setup(p => p.PostRequest(simfy, "", model, null, false, 5))
                .Returns(value);
            //Act

            service.AccountCreation(model);
            //Assert
            Assert.AreEqual(model.ApiLogin, SimfyApiUsername);
            Assert.AreEqual(model.ApiPassword, SimfyApiPassword);
            Assert.AreEqual(model.Email, model.MobileNumber + SimfyEmail);

            mockHttpUtility.Verify(p => p.PostRequest(simfy, "", model, null, false, 5), Times.Once);

        }


        [TestMethod]
        public void AccountCreation_Given_ModelSuccess_Should_ReturnSuccessModel()
        {
            //Arrange
            var mockingKernel = new MoqMockingKernel();
            var mockConfig = mockingKernel.GetMock<IConfig>();
            mockConfig.SetupGet(p => p.SimfyBaseUrl).Returns("SimfyBaseUrl");
            mockConfig.SetupGet(p => p.SimfyApiUsername).Returns("SimfyApiUsername");
            mockConfig.SetupGet(p => p.SimfyApiPassword).Returns("SimfyApiPassword");
            mockConfig.SetupGet(p => p.SimfyEmail).Returns("SimfyEmail");
            var mockHttpUtility = mockingKernel.GetMock<IHttpUtility>();
            var service = new ThirdPartyServiceSimfy(mockingKernel);
            PrivateObject privateObject = new PrivateObject(service);
            var SimfyBaseUrl = privateObject.GetFieldOrProperty("SimfyBaseUrl", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var SimfyApiUsername = privateObject.GetFieldOrProperty("SimfyApiUsername", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var SimfyApiPassword = privateObject.GetFieldOrProperty("SimfyApiPassword", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var SimfyEmail = privateObject.GetFieldOrProperty("SimfyEmail", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            var model = new SimfyAccountCreateRequest();

            var simfy = SimfyBaseUrl.ToString() + "/create"; ;
            HttpResponseMessage value = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            SimfyAccountCreationResponse accountResponse = new SimfyAccountCreationResponse()
            {
                Success = true
            };

            value.Content = new StringContent(JsonConvert.SerializeObject(accountResponse));
            value.Content.Headers.Clear();
            value.Content.Headers.Add("Content-Type", "application/json");
            mockHttpUtility.Setup(p => p.PostRequest(simfy, "", model, null, false, 5))
                .Returns(value);
            //Act

            var responce = service.AccountCreation(model);
            //Assert
            mockHttpUtility.Verify(p => p.PostRequest(simfy, "", model, null, false, 5), Times.Once);
            Assert.IsTrue(responce.IsSuccess);
            Assert.IsTrue(string.IsNullOrEmpty(responce.Message));
        }

        [TestMethod]
        public void AccountCreation_Given_ModelFailure_Should_ReturnFailureModel()
        {
            //Arrange
            var mockingKernel = new MoqMockingKernel();
            var mockConfig = mockingKernel.GetMock<IConfig>();
            mockConfig.SetupGet(p => p.SimfyBaseUrl).Returns("SimfyBaseUrl");
            mockConfig.SetupGet(p => p.SimfyApiUsername).Returns("SimfyApiUsername");
            mockConfig.SetupGet(p => p.SimfyApiPassword).Returns("SimfyApiPassword");
            mockConfig.SetupGet(p => p.SimfyEmail).Returns("SimfyEmail");
            var mockHttpUtility = mockingKernel.GetMock<IHttpUtility>();
            var service = new ThirdPartyServiceSimfy(mockingKernel);
            PrivateObject privateObject = new PrivateObject(service);
            var SimfyBaseUrl = privateObject.GetFieldOrProperty("SimfyBaseUrl", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var SimfyApiUsername = privateObject.GetFieldOrProperty("SimfyApiUsername", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var SimfyApiPassword = privateObject.GetFieldOrProperty("SimfyApiPassword", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var SimfyEmail = privateObject.GetFieldOrProperty("SimfyEmail", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            var model = new SimfyAccountCreateRequest();

            var simfy = SimfyBaseUrl.ToString() + "/create"; ;
            HttpResponseMessage value = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            SimfyAccountCreationResponse accountResponse = new SimfyAccountCreationResponse()
            {
                Success = false
            };

            value.Content = new StringContent(JsonConvert.SerializeObject(accountResponse));
            value.Content.Headers.Clear();
            value.Content.Headers.Add("Content-Type", "application/json");
            mockHttpUtility.Setup(p => p.PostRequest(simfy, "", model, null, false, 5))
                .Returns(value);
            //Act

            var responce = service.AccountCreation(model);
            //Assert
            mockHttpUtility.Verify(p => p.PostRequest(simfy, "", model, null, false, 5), Times.Once);
            Assert.IsFalse(responce.IsSuccess);
            Assert.IsTrue(!string.IsNullOrEmpty(responce.Message));
        }


        [TestMethod]
        public void AccountCancellation_Given_ValidModel_Should_SendPOSTRequest()
        {
            //Arrange
            var mockingKernel = new MoqMockingKernel();
            var mockConfig = mockingKernel.GetMock<IConfig>();
            mockConfig.SetupGet(p => p.SimfyBaseUrl).Returns("SimfyBaseUrl");
            mockConfig.SetupGet(p => p.SimfyApiUsername).Returns("SimfyApiUsername");
            mockConfig.SetupGet(p => p.SimfyApiPassword).Returns("SimfyApiPassword");
            mockConfig.SetupGet(p => p.SimfyEmail).Returns("SimfyEmail");
            var mockHttpUtility = mockingKernel.GetMock<IHttpUtility>();
            var service = new ThirdPartyServiceSimfy(mockingKernel);
            PrivateObject privateObject = new PrivateObject(service);
            var SimfyBaseUrl = privateObject.GetFieldOrProperty("SimfyBaseUrl", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var SimfyApiUsername = privateObject.GetFieldOrProperty("SimfyApiUsername", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var SimfyApiPassword = privateObject.GetFieldOrProperty("SimfyApiPassword", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var SimfyEmail = privateObject.GetFieldOrProperty("SimfyEmail", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var simfy = SimfyBaseUrl.ToString() + "/unsubscribe"; ;
            HttpResponseMessage value = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            var accountResponse = new SimfyAccountCancellationRequest();

            value.Content = new StringContent(JsonConvert.SerializeObject(accountResponse));
            value.Content.Headers.Clear();
            value.Content.Headers.Add("Content-Type", "application/json");
            mockHttpUtility.Setup(p => p.PostRequest(simfy, "", accountResponse, null, false, 5))
                .Returns(value);
            //Act

            service.AccountCancellation(accountResponse);
            //Assert
            Assert.AreEqual(accountResponse.ApiLogin, SimfyApiUsername);
            Assert.AreEqual(accountResponse.ApiPassword, SimfyApiPassword);
            mockHttpUtility.Verify(p => p.PostRequest(simfy, "", accountResponse, null, false, 5), Times.Once);

        }
        [TestMethod]
        public void AccountCancellation_Given_ModelSuccess_Should_ReturnSuccessModel()
        {
            //Arrange
            var mockingKernel = new MoqMockingKernel();
            var mockConfig = mockingKernel.GetMock<IConfig>();
            mockConfig.SetupGet(p => p.SimfyBaseUrl).Returns("SimfyBaseUrl");
            mockConfig.SetupGet(p => p.SimfyApiUsername).Returns("SimfyApiUsername");
            mockConfig.SetupGet(p => p.SimfyApiPassword).Returns("SimfyApiPassword");
            mockConfig.SetupGet(p => p.SimfyEmail).Returns("SimfyEmail");
            var mockHttpUtility = mockingKernel.GetMock<IHttpUtility>();
            var service = new ThirdPartyServiceSimfy(mockingKernel);
            PrivateObject privateObject = new PrivateObject(service);
            var SimfyBaseUrl = privateObject.GetFieldOrProperty("SimfyBaseUrl", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var SimfyApiUsername = privateObject.GetFieldOrProperty("SimfyApiUsername", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var SimfyApiPassword = privateObject.GetFieldOrProperty("SimfyApiPassword", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var SimfyEmail = privateObject.GetFieldOrProperty("SimfyEmail", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var simfy = SimfyBaseUrl.ToString() + "/unsubscribe"; ;
            HttpResponseMessage value = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            var accountResponse = new SimfyAccountCancellationResponse()
            {
                Success = true
            };

            value.Content = new StringContent(JsonConvert.SerializeObject(accountResponse));
            var model = new SimfyAccountCancellationRequest();

            value.Content.Headers.Clear();
            value.Content.Headers.Add("Content-Type", "application/json");
            mockHttpUtility.Setup(p => p.PostRequest(simfy, "", model, null, false, 5))
                .Returns(value);
            //Act

            var responce = service.AccountCancellation(model);
            //Assert
            mockHttpUtility.Verify(p => p.PostRequest(simfy, "", model, null, false, 5), Times.Once);

            Assert.IsTrue(responce.IsSuccess);
            Assert.IsTrue(string.IsNullOrEmpty(responce.Message));
        }


        [TestMethod]
        public void AccountCancellation_Given_ModelFailure_Should_ReturnFailureModel()
        {
            //Arrange
            var mockingKernel = new MoqMockingKernel();
            var mockConfig = mockingKernel.GetMock<IConfig>();
            mockConfig.SetupGet(p => p.SimfyBaseUrl).Returns("SimfyBaseUrl");
            mockConfig.SetupGet(p => p.SimfyApiUsername).Returns("SimfyApiUsername");
            mockConfig.SetupGet(p => p.SimfyApiPassword).Returns("SimfyApiPassword");
            mockConfig.SetupGet(p => p.SimfyEmail).Returns("SimfyEmail");
            var mockHttpUtility = mockingKernel.GetMock<IHttpUtility>();
            var service = new ThirdPartyServiceSimfy(mockingKernel);
            PrivateObject privateObject = new PrivateObject(service);
            var SimfyBaseUrl = privateObject.GetFieldOrProperty("SimfyBaseUrl", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var SimfyApiUsername = privateObject.GetFieldOrProperty("SimfyApiUsername", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var SimfyApiPassword = privateObject.GetFieldOrProperty("SimfyApiPassword", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var SimfyEmail = privateObject.GetFieldOrProperty("SimfyEmail", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var simfy = SimfyBaseUrl.ToString() + "/unsubscribe"; ;
            HttpResponseMessage value = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            var accountResponse = new SimfyAccountCancellationResponse()
            {
                Success = false,
                FailureReasons = new[]
                {
                    "Test Failed To Cancel Simfy Service"
                }
            };

            value.Content = new StringContent(JsonConvert.SerializeObject(accountResponse));
            var model = new SimfyAccountCancellationRequest();

            value.Content.Headers.Clear();
            value.Content.Headers.Add("Content-Type", "application/json");
            mockHttpUtility.Setup(p => p.PostRequest(simfy, "", model, null, false, 5))
                .Returns(value);
            //Act

            var responce = service.AccountCancellation(model);
            //Assert
            mockHttpUtility.Verify(p => p.PostRequest(simfy, "", model, null, false, 5), Times.Once);

            Assert.IsFalse(responce.IsSuccess);
            Assert.IsTrue(!string.IsNullOrEmpty(responce.Message));
        }

        [TestMethod]
        public void AccountCancellation_Given_PostRequestFailure_Should_ReturnError()
        {
            //Arrange
            var mockingKernel = new MoqMockingKernel();
            var mockConfig = mockingKernel.GetMock<IConfig>();
            mockConfig.SetupGet(p => p.SimfyBaseUrl).Returns("SimfyBaseUrl");
            mockConfig.SetupGet(p => p.SimfyApiUsername).Returns("SimfyApiUsername");
            mockConfig.SetupGet(p => p.SimfyApiPassword).Returns("SimfyApiPassword");
            mockConfig.SetupGet(p => p.SimfyEmail).Returns("SimfyEmail");
            var mockHttpUtility = mockingKernel.GetMock<IHttpUtility>();
            var service = new ThirdPartyServiceSimfy(mockingKernel);
            PrivateObject privateObject = new PrivateObject(service);
            var SimfyBaseUrl = privateObject.GetFieldOrProperty("SimfyBaseUrl", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var SimfyApiUsername = privateObject.GetFieldOrProperty("SimfyApiUsername", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var SimfyApiPassword = privateObject.GetFieldOrProperty("SimfyApiPassword", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var SimfyEmail = privateObject.GetFieldOrProperty("SimfyEmail", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var simfy = SimfyBaseUrl.ToString() + "/unsubscribe"; ;
            HttpResponseMessage value = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            SimfyAccountCancellationRequest accountResponse = new SimfyAccountCancellationRequest()
            {

            };

            value.Content = new StringContent(JsonConvert.SerializeObject(accountResponse))
            {

            };
            value.Content.Headers.Clear();
            value.Content.Headers.Add("Content-Type", "application/json");
            mockHttpUtility.Setup(p => p.PostRequest(simfy, "", accountResponse, null, false, 5))
                .Returns(value);
            //Act

            var responce = service.AccountCancellation(accountResponse);
            //Assert
            mockHttpUtility.Verify(p => p.PostRequest(simfy, "", accountResponse, null, false, 5), Times.Once);
            Assert.IsFalse(responce.IsSuccess);
            Assert.AreEqual("Internal Server Error", responce.Message);
        }

        [TestMethod]
        public void AccountCreation_Given_PostRequestFailure_Should_ReturnError()
        {
            //Arrange
            var mockingKernel = new MoqMockingKernel();
            var mockConfig = mockingKernel.GetMock<IConfig>();
            mockConfig.SetupGet(p => p.SimfyBaseUrl).Returns("SimfyBaseUrl");
            mockConfig.SetupGet(p => p.SimfyApiUsername).Returns("SimfyApiUsername");
            mockConfig.SetupGet(p => p.SimfyApiPassword).Returns("SimfyApiPassword");
            mockConfig.SetupGet(p => p.SimfyEmail).Returns("SimfyEmail");
            var mockHttpUtility = mockingKernel.GetMock<IHttpUtility>();
            var service = new ThirdPartyServiceSimfy(mockingKernel);
            PrivateObject privateObject = new PrivateObject(service);
            var SimfyBaseUrl = privateObject.GetFieldOrProperty("SimfyBaseUrl", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var SimfyApiUsername = privateObject.GetFieldOrProperty("SimfyApiUsername", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var SimfyApiPassword = privateObject.GetFieldOrProperty("SimfyApiPassword", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var SimfyEmail = privateObject.GetFieldOrProperty("SimfyEmail", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            var model = new SimfyAccountCreateRequest();

            var simfy = SimfyBaseUrl.ToString() + "/create"; ;
            HttpResponseMessage value = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            SimfyAccountCreationResponse accountResponse = new SimfyAccountCreationResponse()
            {
                Success = true
            };

            value.Content = new StringContent(JsonConvert.SerializeObject(accountResponse));
            value.Content.Headers.Clear();
            value.Content.Headers.Add("Content-Type", "application/json");
            mockHttpUtility.Setup(p => p.PostRequest(simfy, "", model, null, false, 5))
                .Returns(value);
            //Act

            var responce = service.AccountCreation(model);

            //Assert
            mockHttpUtility.Verify(p => p.PostRequest(simfy, "", model, null, false, 5), Times.Once);
            Assert.IsFalse(responce.IsSuccess);
            Assert.AreEqual("Internal Server Error", responce.Message);
        }

        [TestMethod]
        public void AccountCreation_Given_HTTPRequestException_Should_CatchException()
        {
            //Arrange
            var mockingKernel = new MoqMockingKernel();
            var mockConfig = mockingKernel.GetMock<IConfig>();
            mockConfig.SetupGet(p => p.SimfyBaseUrl).Returns("SimfyBaseUrl");
            mockConfig.SetupGet(p => p.SimfyApiUsername).Returns("SimfyApiUsername");
            mockConfig.SetupGet(p => p.SimfyApiPassword).Returns("SimfyApiPassword");
            mockConfig.SetupGet(p => p.SimfyEmail).Returns("SimfyEmail");
            var mockHttpUtility = mockingKernel.GetMock<IHttpUtility>();
            var service = new ThirdPartyServiceSimfy(mockingKernel);
            PrivateObject privateObject = new PrivateObject(service);
            var SimfyBaseUrl = privateObject.GetFieldOrProperty("SimfyBaseUrl", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var SimfyApiUsername = privateObject.GetFieldOrProperty("SimfyApiUsername", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var SimfyApiPassword = privateObject.GetFieldOrProperty("SimfyApiPassword", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var SimfyEmail = privateObject.GetFieldOrProperty("SimfyEmail", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            var model = new SimfyAccountCreateRequest();

            var simfy = SimfyBaseUrl.ToString() + "/create"; ;
            HttpResponseMessage value = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            SimfyAccountCreationResponse accountResponse = new SimfyAccountCreationResponse()
            {
                Success = true
            };

            value.Content = new StringContent(JsonConvert.SerializeObject(accountResponse));
            value.Content.Headers.Clear();
            value.Content.Headers.Add("Content-Type", "application/json");
            mockHttpUtility.Setup(p => p.PostRequest(simfy, "", model, null, false, 5))
                .Throws<Exception>();
            //Act

            var responce = service.AccountCreation(model);

            //Assert
            mockHttpUtility.Verify(p => p.PostRequest(simfy, "", model, null, false, 5), Times.Once);
            Assert.IsFalse(responce.IsSuccess);
            Assert.AreEqual("Exception: Creating Simfy Account.", responce.Message);
        }

        [TestMethod]
        public void AccountCancellation_Given_HTTPRequestException_Should_CatchException()
        {
            //Arrange
            var mockingKernel = new MoqMockingKernel();
            var mockConfig = mockingKernel.GetMock<IConfig>();
            mockConfig.SetupGet(p => p.SimfyBaseUrl).Returns("SimfyBaseUrl");
            mockConfig.SetupGet(p => p.SimfyApiUsername).Returns("SimfyApiUsername");
            mockConfig.SetupGet(p => p.SimfyApiPassword).Returns("SimfyApiPassword");
            mockConfig.SetupGet(p => p.SimfyEmail).Returns("SimfyEmail");
            var mockHttpUtility = mockingKernel.GetMock<IHttpUtility>();
            var service = new ThirdPartyServiceSimfy(mockingKernel);
            PrivateObject privateObject = new PrivateObject(service);
            var SimfyBaseUrl = privateObject.GetFieldOrProperty("SimfyBaseUrl", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var SimfyApiUsername = privateObject.GetFieldOrProperty("SimfyApiUsername", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var SimfyApiPassword = privateObject.GetFieldOrProperty("SimfyApiPassword", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var SimfyEmail = privateObject.GetFieldOrProperty("SimfyEmail", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var simfy = SimfyBaseUrl.ToString() + "/unsubscribe"; ;
            HttpResponseMessage value = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            SimfyAccountCancellationRequest accountResponse = new SimfyAccountCancellationRequest()
            {

            };

            value.Content = new StringContent(JsonConvert.SerializeObject(accountResponse))
            {

            };
            value.Content.Headers.Clear();
            value.Content.Headers.Add("Content-Type", "application/json");
            mockHttpUtility.Setup(p => p.PostRequest(simfy, "", accountResponse, null, false, 5))
                .Throws<Exception>();
            //Act

            var responce = service.AccountCancellation(accountResponse);

            //Assert
            mockHttpUtility.Verify(p => p.PostRequest(simfy, "", accountResponse, null, false, 5), Times.Once);
            Assert.IsFalse(responce.IsSuccess);
            Assert.AreEqual("Exception: Cancelling Simfy Account.", responce.Message);
        }

    }

}
