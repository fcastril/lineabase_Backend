
using Api.Controllers;
using Domain.Entities;
using ServiceApplication.Dto;
using UnitMSTest.Api.Controllers.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitMSTest.Api.Controllers
{
    [TestClass]
    public class CustomerControllerTest : HandlerBaseControllerTest<Customer, CustomerDto, CustomerController>
    {
    }
}