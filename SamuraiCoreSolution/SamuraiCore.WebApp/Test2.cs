using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamuraiCore.WebApp
{
    public class Test2
    {
    }

    public class ComponentSimfyTests
    {
        [TestMethod]
        public void SimfyCancellation_Given_OrderItemIsNull_Expect_ValidationMessage()
        {
            //Arrange
            var kernel = new MoqMockingKernel();
            var thirdPartyService = kernel.GetMock<IThirdPartyServiceSimfy>();
            var orderItemRepo = kernel.GetMock<IRepositoryOrderItemData>();
            orderItemRepo.Setup(p => p.GetByOrderItemId(1))
                .Returns(new EnumerableQuery<OrderItemData>(new List<OrderItemData>()));
            ComponentSimfy componentSimfy = new ComponentSimfy(kernel);
            //Act
            var response = componentSimfy.SimfyCancellation(1);
            //Assert
            Assert.AreEqual("Order item data not found.", response.Message);
            Assert.IsFalse(response.IsSuccess);
            orderItemRepo.VerifyAll();
        }

        [TestMethod]
        public void SimfyActivation_Given_EmptyUsername_Expect_ValidationMessage()
        {
            //Arrange
            var kernel = new MoqMockingKernel();
            ComponentSimfy componentSimfy = new ComponentSimfy(kernel);
            //Act
            var response = componentSimfy.SimfyActivation("", "password", 1);
            //Assert
            Assert.AreEqual("Username and Password must be populated.", response.Message);
            Assert.IsFalse(response.IsSuccess);
        }

        [TestMethod]
        public void SimfyActivation_Given_EmptyPassword_Expect_ValidationMessage()
        {
            //Arrange
            var kernel = new MoqMockingKernel();
            ComponentSimfy componentSimfy = new ComponentSimfy(kernel);
            //Act
            var response = componentSimfy.SimfyActivation("password", "", 1);
            //Assert
            Assert.AreEqual("Username and Password must be populated.", response.Message);
            Assert.IsFalse(response.IsSuccess);
        }

        [TestMethod]
        public void SimfyActivation_Given_IsOrderItemActivated_Expect_ValidationMessage()
        {
            //Arrange
            var kernel = new MoqMockingKernel();
            var thirdPartyService = kernel.GetMock<IThirdPartyServiceSimfy>();
            var orderItemRepo = kernel.GetMock<IRepositoryOrderItemData>();
            orderItemRepo.Setup(p => p.IsOrderItemActivated(1)).Returns(true);
            orderItemRepo.Setup(p => p.GetByOrderItemId(1))
                .Returns(new EnumerableQuery<OrderItemData>(new List<OrderItemData>()
                {
                    new OrderItemData()
                    {
                        IsActivated=true
                    }
                }));
            ComponentSimfy componentSimfy = new ComponentSimfy(kernel);
            //Act
            var response = componentSimfy.SimfyActivation("password", "password", 1);
            //Assert
            Assert.AreEqual("Order is Activated", response.Message);
            Assert.IsTrue(response.IsSuccess);
            orderItemRepo.Verify(p => p.GetByOrderItemId(1), Times.Once);
            orderItemRepo.Verify(p => p.IsOrderItemActivated(1), Times.Never);
        }


        [TestMethod]
        public void SimfyActivation_Given_ExistingUser_Expect_UserIdReturned()
        {
            //Arrange
            var kernel = new MoqMockingKernel();
            var thirdPartyService = kernel.GetMock<IThirdPartyServiceSimfy>();
            var orderItemRepo = kernel.GetMock<IRepositoryOrderItemData>();
            orderItemRepo.Setup(p => p.IsOrderItemActivated(1)).Returns(true);
            orderItemRepo.Setup(p => p.GetByOrderItemId(1))
                .Returns(new EnumerableQuery<OrderItemData>(new List<OrderItemData>()
                {
                    new OrderItemData()
                    {
                        IsActivated=false,
                        AccountNumber="111"
                    }
                }));
            ComponentSimfy componentSimfy = new ComponentSimfy(kernel);
            //Act
            var response = componentSimfy.SimfyActivation("password", "password", 1);
            //Assert
            var realResponse = response as APIItemResponseModel<SimfyAccountCreationResponse>;
            Assert.IsNotNull(response);
            Assert.IsTrue(string.IsNullOrEmpty(realResponse.Message));
            Assert.IsTrue(realResponse.IsSuccess);
            var logIn = realResponse.Item.login;
            Assert.AreEqual("password", logIn);
            var accountNumber = realResponse.Item.id;
            Assert.AreEqual("111", accountNumber);

            orderItemRepo.Verify(p => p.GetByOrderItemId(1), Times.Exactly(1));
            orderItemRepo.Verify(p => p.IsOrderItemActivated(1), Times.Never);
        }

        [TestMethod]
        public void SimfyActivation_Given_UserHasEmptyAccountNumber_Expect_serviceAccount_Creation()
        {
            //Arrange
            var kernel = new MoqMockingKernel();
            var thirdPartyService = kernel.GetMock<IThirdPartyServiceSimfy>();
            var orderItemRepo = kernel.GetMock<IRepositoryOrderItemData>();
            var repositoryOrderItem = kernel.GetMock<IRepositoryOrderItem>();
            var repositoryDeal = kernel.GetMock<IRepositoryDeal>();
            var componentOrderItemData = kernel.GetMock<IRepositoryOrderItemData>();
            orderItemRepo.Setup(p => p.IsOrderItemActivated(1)).Returns(true);
            repositoryOrderItem.Setup(p => p.GetContactByOrderItemId(1, (int)CustomerContactType.MobileNumber)).Returns("0721234567");
            repositoryOrderItem.Setup(p => p.GetDealExternalRefByOrderItemId(1)).Returns("0721234567");
            repositoryDeal.Setup(p => p.GetByIdAsNoTracking(2)).Returns(new EnumerableQuery<Deal>(new List<Deal>()));
            orderItemRepo.Setup(p => p.GetByOrderItemId(1)).Returns(new EnumerableQuery<OrderItemData>(new List<OrderItemData>()
                {
                    new OrderItemData()
                    {
                        IsActivated=false,
                        AccountNumber=""
                    }
                }));
            componentOrderItemData.Setup(p => p.UpdateAccountNumber(1, "1"));
            APIItemResponseModel<SimfyAccountCreationResponse> serviceReturnResponse = new APIItemResponseModel<SimfyAccountCreationResponse>()
            {
                IsSuccess = true,
                Item = new SimfyAccountCreationResponse()
                {
                    ResultantUserUUID = "1"
                }
            };
            thirdPartyService.Setup(p => p.AccountCreation(It.IsAny<SimfyAccountCreateRequest>()))
                .Callback((SimfyAccountCreateRequest request) =>
                {
                    Assert.AreEqual("password", request.Username);
                    Assert.AreEqual("password", request.Password);
                    Assert.IsFalse(request.ReceiveNewsletter);
                    Assert.IsTrue(request.AcceptPrivacyPolicyAndTermsOfUse);
                    Assert.AreEqual("password", request.Email);
                    Assert.AreEqual("0721234567", request.MobileNumber);
                    Assert.AreEqual("0721234567", request.ExternalReference);
                })
                .Returns(serviceReturnResponse);
            ComponentSimfy componentSimfy = new ComponentSimfy(kernel);
            //Act
            var response = componentSimfy.SimfyActivation("password", "password", 1);
            //Assert
            var realResponse = response as APIItemResponseModel<SimfyAccountCreationResponse>;
            Assert.IsNotNull(response);
            Assert.IsTrue(string.IsNullOrEmpty(realResponse.Message));
            Assert.IsTrue(realResponse.IsSuccess);
            Assert.IsTrue(ReferenceEquals(serviceReturnResponse, response));
            orderItemRepo.Verify(p => p.GetByOrderItemId(1), Times.Exactly(1));
            orderItemRepo.Verify(p => p.IsOrderItemActivated(1), Times.Never);
            repositoryOrderItem.Verify(p => p.GetContactByOrderItemId(1, (int)CustomerContactType.MobileNumber), Times.Once);
            repositoryOrderItem.Verify(p => p.GetDealExternalRefByOrderItemId(1), Times.Once);
            componentOrderItemData.VerifyAll();
            thirdPartyService.VerifyAll();
        }

        [TestMethod]
        public void SimfyActivation_Given_NoOrderItemInRepo_Expect_ValidationMessage()
        {
            //Arrange
            var kernel = new MoqMockingKernel();
            var thirdPartyService = kernel.GetMock<IThirdPartyServiceSimfy>();
            var orderItemRepo = kernel.GetMock<IRepositoryOrderItemData>();
            orderItemRepo.Setup(p => p.IsOrderItemActivated(1))
                .Returns(true);
            ComponentSimfy componentSimfy = new ComponentSimfy(kernel);
            //Act
            var response = componentSimfy.SimfyActivation("password", "password", 1);
            //Assert
            Assert.AreEqual("No Order Item Data could be found.", response.Message);
            Assert.IsFalse(response.IsSuccess);
            orderItemRepo.Verify(p => p.IsOrderItemActivated(1), Times.Never);
        }

        [TestMethod]
        public void SimfyCancellation_Given_AccountNumberIsNull_Expect_ValidationMessage()
        {
            //Arrange
            var kernel = new MoqMockingKernel();
            var thirdPartyService = kernel.GetMock<IThirdPartyServiceSimfy>();
            var orderItemRepo = kernel.GetMock<IRepositoryOrderItemData>();
            orderItemRepo.Setup(p => p.GetByOrderItemId(1))
                .Returns(new EnumerableQuery<OrderItemData>(new List<OrderItemData>()
                {
                    new OrderItemData()
                    {

                    }
                }));
            ComponentSimfy componentSimfy = new ComponentSimfy(kernel);
            //Act
            var response = componentSimfy.SimfyCancellation(1);
            //Assert
            Assert.AreEqual("Order item data :Account Number not available.", response.Message);
            Assert.IsFalse(response.IsSuccess);
            orderItemRepo.VerifyAll();
        }
        [TestMethod]
        public void SimfyCancellation_Given_ServiceResponseNotSuccesfull_Expect_ValidationMessage()
        {
            //Arrange
            var kernel = new MoqMockingKernel();
            var thirdPartyService = kernel.GetMock<IThirdPartyServiceSimfy>();
            var orderItemRepo = kernel.GetMock<IRepositoryOrderItemData>();
            orderItemRepo.Setup(p => p.GetByOrderItemId(1))
                .Returns(new EnumerableQuery<OrderItemData>(new List<OrderItemData>()
                {
                    new OrderItemData()
                    {
                        AccountNumber="TestAccouint"
                    }
                }));
            thirdPartyService.Setup(p => p.AccountCancellation(It.IsAny<SimfyAccountCancellationRequest>()))
                .Returns(new APIResponseModel()
                {
                    Message = "API error",
                    IsSuccess = false

                });

            ComponentSimfy componentSimfy = new ComponentSimfy(kernel);
            //Act
            var response = componentSimfy.SimfyCancellation(1);
            //Assert
            Assert.AreEqual("API error", response.Message);
            Assert.IsFalse(response.IsSuccess);
            thirdPartyService.VerifyAll();
            orderItemRepo.VerifyAll();
        }

        [TestMethod]
        public void SimfyCancellation_Given_ServiceCancellsSuccesfully_Expect_SuccesMessage()
        {
            //Arrange
            var kernel = new MoqMockingKernel();
            var thirdPartyService = kernel.GetMock<IThirdPartyServiceSimfy>();
            var orderItemRepo = kernel.GetMock<IRepositoryOrderItemData>();
            orderItemRepo.Setup(p => p.GetByOrderItemId(1))
                .Returns(new EnumerableQuery<OrderItemData>(new List<OrderItemData>()
                {
                    new OrderItemData()
                    {
                        AccountNumber="TestAccouint"
                    }
                }));
            thirdPartyService.Setup(p => p.AccountCancellation(It.IsAny<SimfyAccountCancellationRequest>()))
                .Returns(new APIResponseModel()
                {
                    IsSuccess = true

                });

            ComponentSimfy componentSimfy = new ComponentSimfy(kernel);
            //Act
            var response = componentSimfy.SimfyCancellation(1);
            //Assert
            Assert.AreEqual("Order cancelled successfully.", response.Message);
            Assert.IsTrue(response.IsSuccess);
            thirdPartyService.VerifyAll();
            orderItemRepo.VerifyAll();
        }


        [TestMethod]
        public void SimfyCancellation_Given_ThrowsException_Expect_ExceptionMessage()
        {
            //Arrange
            var kernel = new MoqMockingKernel();
            var thirdPartyService = kernel.GetMock<IThirdPartyServiceSimfy>();
            var orderItemRepo = kernel.GetMock<IRepositoryOrderItemData>();
            orderItemRepo.Setup(p => p.GetByOrderItemId(1))
                .Returns(new EnumerableQuery<OrderItemData>(new List<OrderItemData>()
                {
                    new OrderItemData()
                    {
                        AccountNumber="TestAccouint"
                    }
                }));
            thirdPartyService.Setup(p => p.AccountCancellation(It.IsAny<SimfyAccountCancellationRequest>()))
                .Throws(new System.Exception());

            ComponentSimfy componentSimfy = new ComponentSimfy(kernel);
            //Act
            var response = componentSimfy.SimfyCancellation(1);
            //Assert
            Assert.AreEqual("Exception: Cancelling on Simfy ThirdParty API.", response.Message);
            Assert.IsFalse(response.IsSuccess);
            thirdPartyService.VerifyAll();
            orderItemRepo.VerifyAll();
        }

        [TestMethod]
        public void AddToCancellationQueue_Given_BillingCanelQueue_IsPopulated_Expect_SuccesMessage()
        {
            //Arrange
            var kernel = new MoqMockingKernel();
            var workflowStorageComponent = kernel.GetMock<IWorkflowStorageComponent>();
            int orderItemId = 666;
            int providerId = 1;
            workflowStorageComponent.Setup(p => p.PopulateBillingCancelQueue(It.IsAny<BillingExtractState>()))
                .Callback((BillingExtractState state) =>
                {
                    Assert.AreEqual(666, state.OrderItemId);
                    Assert.AreEqual(providerId, state.ProviderId);
                    Assert.AreEqual((int)Activation.Cancelled, state.ExtractStatus);
                })
                .Returns(true);
            ComponentOrderCancellation componentOrderAddToCancelQueue = new ComponentOrderCancellation(kernel);
            //Act
            var response = componentOrderAddToCancelQueue.AddToCancellationQueue(orderItemId, providerId);
            //Assert
            Assert.AreEqual("Order cancellation process has been initiated", response.Message);
            Assert.IsTrue(response.IsSuccess);
            workflowStorageComponent.VerifyAll();
        }

        [TestMethod]
        public void AddToCancellationQueue_Given_BillingCanelQueue_Is_Not_Populated_Expect_SuccesMessage()
        {
            //Arrange
            var kernel = new MoqMockingKernel();
            var workflowStorageComponent = kernel.GetMock<IWorkflowStorageComponent>();
            int orderItemId = 666;
            int providerId = 1;
            workflowStorageComponent.Setup(p => p.PopulateBillingCancelQueue(It.IsAny<BillingExtractState>()))
                .Callback((BillingExtractState state) =>
                {
                    Assert.AreEqual(666, state.OrderItemId);
                    Assert.AreEqual(providerId, state.ProviderId);
                    Assert.AreEqual((int)Activation.Cancelled, state.ExtractStatus);
                })
                .Returns(false);
            ComponentOrderCancellation componentOrderAddToCancelQueue = new ComponentOrderCancellation(kernel);
            //Act
            var response = componentOrderAddToCancelQueue.AddToCancellationQueue(orderItemId, providerId);
            //Assert
            Assert.AreEqual("Error occured while populating queue", response.Message);
            Assert.IsFalse(response.IsSuccess);
            workflowStorageComponent.VerifyAll();
        }

        [TestMethod]
        public void AddToCancellationQueue_Given_Exception_Expect_ExceptionMessage()
        {
            //Arrange
            var kernel = new MoqMockingKernel();
            var workflowStorageComponent = kernel.GetMock<IWorkflowStorageComponent>();
            var providerMock = kernel.GetMock<IRepositoryProvider>();

            int orderItemId = 666;
            int providerId = 1;

            providerMock.Setup(p => p.GetNameByProviderId(providerId)).Returns("Simfy");
            workflowStorageComponent.Setup(p => p.PopulateBillingCancelQueue(It.IsAny<BillingExtractState>()))
                .Throws<Exception>();
            ComponentOrderCancellation componentOrderAddToCancelQueue = new ComponentOrderCancellation(kernel);
            //Act
            var response = componentOrderAddToCancelQueue.AddToCancellationQueue(orderItemId, providerId);
            //Assert
            Assert.AreEqual("Exception: Adding Cancellation to Billing Cancel Queue", response.Message);
            Assert.IsFalse(response.IsSuccess);
            workflowStorageComponent.VerifyAll();
            providerMock.Verify(p => p.GetNameByProviderId(providerId), Times.Once);
        }

        [TestMethod]
        public void AddToCancellationQueue_Given_NoProvidersExist_Expect_ExceptionMessage()
        {
            var kernel = new MoqMockingKernel();
            var workflowStorageComponent = kernel.GetMock<IWorkflowStorageComponent>();
            var providerMock = kernel.GetMock<IRepositoryProvider>();

            int orderItemId = 666;
            int providerId = 1;

            workflowStorageComponent.Setup(p => p.PopulateBillingCancelQueue(It.IsAny<BillingExtractState>()))
                .Throws<Exception>();
            ComponentOrderCancellation componentOrderAddToCancelQueue = new ComponentOrderCancellation(kernel);
            //Act
            var response = componentOrderAddToCancelQueue.AddToCancellationQueue(orderItemId, providerId);
            //Assert
            Assert.AreEqual("Exception: Adding Cancellation to Billing Cancel Queue", response.Message);
            Assert.IsFalse(response.IsSuccess);
            workflowStorageComponent.VerifyAll();
            providerMock.Verify(p => p.GetNameByProviderId(providerId), Times.Once);
        }

    }

}
