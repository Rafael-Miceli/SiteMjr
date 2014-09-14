﻿using System;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SharedKernel;
using SharedKernel.Interfaces;
using WebSiteMjr.Domain.CustomerService.Events;
using WebSiteMjr.Domain.CustomerService.Model;
using WebSiteMjr.Domain.CustomerService.Services;
using WebSiteMjr.Domain.Interfaces.CustomerService;
using WebSiteMjr.Domain.Interfaces.CustomerService.Repository;
using WebSiteMjr.Domain.Test.Model;
using EmailHandler =  WebSiteMjr.Notifications.Email.MjrEmailNotification;
using SignalRHandler = WebSiteMjr.Notifications.SignalR;

namespace WebSiteMjr.Domain.Test.CustomerService
{
    [TestClass]
    public class CallTests
    {
        private Mock<ICallRepository> _callRepositoryMock;
        private IUnityContainer _container;
        private ICallService _callService;

        [TestInitialize]
        public void Initialize()
        {
            _callRepositoryMock = new Mock<ICallRepository>();
            _container = new UnityContainer();

            _container.RegisterType<IHandle<CallAddedEvent>, EmailHandler.CallAddedHandler>("CallAddedEmailEventHandler");
            _container.RegisterType<IHandle<CallAddedEvent>, SignalRHandler.CallAddedHandler>("CallAddedSignalREventHandler");

            DomainEvents.Container = _container;

            _callService = new CallService(_callRepositoryMock.Object, new StubUnitOfWork());
        }

        [TestMethod]
        public void Given_A_Valid_Call_When_Client_Called_And_Im_Creating_A_New_Call_Then_Create_In_Database()
        {
            _callRepositoryMock.Setup(x => x.Add(It.IsAny<Call>()));

            var portoverano = CompanyDummies.CreatePortoveranoWithCompanyArea();
            var serviceType = ServiceTypeDummies.CreateGenericProblem();
            var lorenaId = EmployeeDummies.CreateListOfEmployees().FirstOrDefault(em => em.Id == 3).Id;

            var call = new Call(portoverano, "Problema em cameras", serviceType, false, lorenaId);

            DomainEvents.Register<CallAddedEvent>(null);

            _callService.CreateCall(call);

            _callRepositoryMock.VerifyAll();
        }
    }

    public static class ServiceTypeDummies
    {
        //public static ServiceDetails CreateProblemWithCameras()
        //{
        //    return new CameraServic
        //    {
        //        Details = "Problema com cameras nesses lugares"
        //    };
        //}

        public static ServiceDetails CreateGenericProblem()
        {
            return new ServiceDetails
            {
                Details = "Problema com cameras nesses lugares"
            };
        }
    }
}
