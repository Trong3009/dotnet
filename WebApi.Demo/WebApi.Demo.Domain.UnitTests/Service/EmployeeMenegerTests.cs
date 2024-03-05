using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Demo.Domain;

namespace WebApi.Demo.Domain.UnitTests.Service
{
    [TestFixture]
    public class EmployeeMenegerTests
    {
        [Test]
        public async Task CheckDuplicateCode_EmployeeNotExist_Success()
        {
            // Arrange
            string code = "NV-HelloWorld";

            var employeeRepositoryFake = Substitute.For<IEmployeeRepository>();
            var employeeManager = new EmployeeManager(employeeRepositoryFake);

            // Actual
            await employeeManager.CheckDuplicateCodeAsync(code);

            // Assert
            await employeeRepositoryFake.Received(1).FindByCodeAsync(code);
        }
        [Test]
        public async Task CheckDuplicateCodeAsync_EmployeeExist_ThrowsException()
        {
            // Arrange
            string code = "NV-TonTai";

            var employeeRepositoryFake = Substitute.For<IEmployeeRepository>();
            employeeRepositoryFake.FindByCodeAsync(code).Returns(new Employee());
            var employeeManager = new EmployeeManager(employeeRepositoryFake);

            // Actual and Assert
            Assert.ThrowsAsync<ConflictException>(async () => await employeeManager.CheckDuplicateCodeAsync(code));
            await employeeRepositoryFake.Received(1).FindByCodeAsync(code);
        }
    }
}
