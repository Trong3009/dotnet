using AutoMapper;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using WebApi.Demo.Domain;

namespace WebApi.Demo.Application.UnitTests
{
    public class EmployeeServiceTests
    {
        private IEmployeeRepository _employeeRepository;
        private IEmployeeManager _employeeManager;
        private IMapper _mapper;
        private EmployeeService _employeeService;

        [SetUp]
        public void SetUp()
        {
            _employeeRepository = Substitute.For<IEmployeeRepository>();
            _employeeManager = Substitute.For<IEmployeeManager>();
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EmployeeProfile>();
            });

            _mapper = configuration.CreateMapper();
            _employeeService = new EmployeeService(_employeeRepository, _mapper, _employeeManager);
        }
        [Test]
        public async Task GetAllAsync_NotEmptyList_ReturnsMappedEmployeeDtos()
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee { EmployeeId = Guid.NewGuid() },
                new Employee { EmployeeId = Guid.NewGuid() }
            };
            _employeeRepository.GetAllAsync().Returns(employees);

            // Act
            var result = await _employeeService.GetAllAsync();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(employees.Count));
            Assert.That(result.ElementAt(0).EmployeeId, Is.EqualTo(employees[0].EmployeeId));
            Assert.That(result.ElementAt(1).EmployeeId, Is.EqualTo(employees[1].EmployeeId));
        }
        [Test]
        public async Task GetAllAsync_EmptyList_ReturnsEmptyList()
        {
            // Arrange
            var emptyList = new List<Employee>();
            _employeeRepository.GetAllAsync().Returns(emptyList);

            // Act
            var result = await _employeeService.GetAllAsync();

            // Assert
            Assert.That(result, Is.Empty);
        }
        [Test]
        public async Task GetAsync_ValidId_ReturnsMappedEmployeeDto()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var employee = new Employee { EmployeeId = employeeId };
            _employeeRepository.GetAsync(employeeId).Returns(employee);

            // Act
            var result = await _employeeService.GetAsync(employeeId);

            // Assert
            Assert.That(result.EmployeeId, Is.EqualTo(employee.EmployeeId));
        }
        [Test]
        public async Task GetAsync_InvalidId_ReturnsNull()
        {
            // Arrange
            var invalidId = Guid.NewGuid();

            _employeeRepository.GetAsync(invalidId).ReturnsNull();

            // Act
            var result = await _employeeService.GetAsync(invalidId);

            // Assert
            Assert.That(result, Is.Null);
        }

        /// <summary>
        /// Hàm test tạo một EmployeeCode mới
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetNewCodeAsync_ValidCode_ReturnsNewCode()
        {
            // Arrange
            string newCode = "NV-NewCode";
            _employeeRepository.GetNewCodeAsync().Returns(newCode);

            // Act
            var result = await _employeeService.GetNewCodeAsync();

            // Assert
            Assert.That(result, Is.EqualTo(newCode));
        }

        /// <summary>
        /// Hàm test thêm một bản ghi, trong trường hợp EmployeeCode không bị trùng
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AddAsync_NotDuplicateCode_ReturnsMappedEmployeeDto()
        {
            // Arrange
            var employeeCreateDto = new EmployeeCreateDto() { EmployeeCode = "NV-ValidCode" };
            var addedEmployee = new Employee { EmployeeCode = "NV-ValidCode" };

            _employeeManager.CheckDuplicateCodeAsync(Arg.Any<string>()).Returns(Task.CompletedTask);
            _employeeRepository.InsertAsync(Arg.Any<Employee>()).Returns(addedEmployee);

            // Act
            var result = await _employeeService.InsertAsync(employeeCreateDto);

            // Assert
            await _employeeRepository.Received(1).InsertAsync(Arg.Any<Employee>());
            Assert.That(result.EmployeeCode, Is.EqualTo(addedEmployee.EmployeeCode));
        }

        /// <summary>
        /// Hàm test thêm một bản ghi, trong trường hợp EmployeeCode bị trùng
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AddAsync_DuplicateCode_ThrowsException()
        {
            // Arrange
            var employeeCreateDto = new EmployeeCreateDto();

            _employeeManager.CheckDuplicateCodeAsync(employeeCreateDto.EmployeeCode).Throws(new ConflictException());

            // Act
            var handler = async () => await _employeeService.InsertAsync(employeeCreateDto);

            // Assert
            await _employeeRepository.Received(1).InsertAsync(Arg.Any<Employee>());
            Assert.ThrowsAsync<ConflictException>(async () => await handler());
        }

        /// <summary>
        /// Hàm test sửa một bản ghi, trong trường hợp EmployeeCode không bị trùng
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task UpdateAsync_NotDuplicateCode_ReturnsMappedEmployeeDto()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var employeeUpdateDto = new EmployeeUpdateDto();
            var updatedEmployee = new Employee { EmployeeId = employeeId };

            _employeeRepository.GetAsync(employeeId).Returns(updatedEmployee);
            _employeeManager.CheckDuplicateCodeAsync(Arg.Any<string>()).Returns(Task.CompletedTask);
            _employeeRepository.UpdateAsync(Arg.Any<Employee>()).Returns(updatedEmployee);

            // Act
            var result = await _employeeService.UpdateAsync(employeeId, employeeUpdateDto);

            // Assert
            await _employeeRepository.Received(1).UpdateAsync(Arg.Any<Employee>());
            Assert.That(result.EmployeeId, Is.EqualTo(updatedEmployee.EmployeeId));
        }

        /// <summary>
        /// Hàm test sửa một bản ghi, trong trường hợp EmployeeCode bị trùng
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task UpdateAsync_DuplicateCode_ThrowsException()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var employeeUpdateDto = new EmployeeUpdateDto();
            var existingEmployee = new Employee { EmployeeId = employeeId, EmployeeCode = "NV-DuplicateCode" };

            _employeeRepository.GetAsync(employeeId).Returns(existingEmployee);
            _employeeManager.CheckDuplicateCodeAsync(employeeUpdateDto.EmployeeCode).Throws(new ConflictException());

            // Act
            var handler = async () => await _employeeService.UpdateAsync(employeeId, employeeUpdateDto);

            // Assert
            Assert.ThrowsAsync<ConflictException>(async () => await handler());
        }

        /// <summary>
        /// Hàm test xoá một bản ghi, trong trường hợp bản ghi cần xoá tồn tại trong database
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task DeleteAsync_EmployeeExists_Returns1()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var employee = new Employee { EmployeeId = employeeId };
            _employeeRepository.GetAsync(employeeId).Returns(employee);
            _employeeRepository.DeleteAsync(employee).Returns(1);

            // Act
            var result = await _employeeService.DeleteAsync(employeeId);

            // Assert
            await _employeeRepository.Received(1).GetAsync(employeeId);
            await _employeeRepository.Received(1).DeleteAsync(employee);
            Assert.That(result, Is.EqualTo(1));
        }

        /// <summary>
        /// Hàm test xoá một bản ghi, trong trường hợp bản ghi cần xoá không tồn tại trong database
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task DeleteAsync_EmployeeNotExists_Returns0()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var employee = new Employee { EmployeeId = employeeId };
            _employeeRepository.GetAsync(employeeId).Returns(employee);
            _employeeRepository.DeleteAsync(employee).Returns(0);

            // Act
            var result = await _employeeService.DeleteAsync(employeeId);

            // Assert
            await _employeeRepository.Received(1).GetAsync(employeeId);
            await _employeeRepository.Received(1).DeleteAsync(employee);
            Assert.That(result, Is.EqualTo(0));
        }

        /// <summary>
        /// Hàm kiểm tra trùng EmployeeCode, trong trường hợp Code cần kiểm tra chưa tồn tại
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task IsDuplicateCodeAsync_CodeNotExist_ReturnsFalse()
        {
            // Arrange
            var nonExistentCode = "NV-NonExistentCode";
            _employeeRepository.FindByCodeAsync(nonExistentCode).ReturnsNull();

            // Act
            var result = await _employeeService.CheckDuplicateCodeAsync(nonExistentCode);

            // Assert
            Assert.That(result, Is.False);
        }

        /// <summary>
        /// Hàm kiểm tra trùng EmployeeCode, trong trường hợp Code cần kiểm tra đã tồn tại
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task IsDuplicateCodeAsync_CodeExists_ReturnsTrue()
        {
            // Arrange
            var existingCode = "NV-ExistingCode";
            _employeeRepository.FindByCodeAsync(existingCode).Returns(new Employee());

            // Act
            var result = await _employeeService.CheckDuplicateCodeAsync(existingCode);

            // Assert
            Assert.That(result, Is.True);
        }

        /// <summary>
        /// Hàm test xóa nhiều bản ghi trong database
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task DeleteManyAsync_List10Ids_Delete10Entity()
        {
            // Arrange
            var ids = new List<Guid>();
            var employees = new List<Employee>();
            for (int i = 0; i < 10; i++)
            {
                var id = Guid.NewGuid();
                var employee = new Employee() { EmployeeId = id };

                ids.Add(id);
                employees.Add(employee);
            }

            _employeeRepository.GetListAsync(ids).Returns((employees, new List<Guid>()));
            _employeeRepository.DeleteManyAsync(employees).Returns(10);

            var expectedResult = 10;

            // Action
            var actualResult = await _employeeService.DeleteManyAsync(ids);
            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));

            await _employeeRepository.Received(1).GetListAsync(ids);
            await _employeeRepository.Received(1).DeleteManyAsync(employees);
        }

        /// <summary>
        /// Hàm test xóa nhiều bản ghi trong database trong trường hợp một vài bản ghi không tồn tại
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task DeleteManyAsync_SomeRecordsNotExist_ThrowsException()
        {
            var ids = new List<Guid>();
            var idsError = new List<Guid>();
            var idsSuccess = new List<Guid>();

            for (int i = 0; i < 8; i++)
            {
                var id = Guid.NewGuid();

                idsSuccess.Add(id);
            }

            for (int i = 0; i <= 2; i++)
            {
                var id = Guid.NewGuid();

                idsError.Add(id);
            }
            ids = idsSuccess.Concat(idsError).ToList();

            var employee = idsSuccess.Select(id => new Employee()
            {
                EmployeeId = id
            }).ToList();


            _employeeRepository.GetListAsync(ids).Returns((employee, idsError));
            _employeeRepository.DeleteManyAsync(employee).Returns(employee.Count);
            var expectedResult = $"Không tìm thấy: {string.Join(", ", idsError)}";

            // Action
            var handle = async () => await _employeeService.DeleteManyAsync(ids);

            // Assert
            var exception = Assert.ThrowsAsync<NotfoundException>(async () => await handle());

            Assert.That(exception.Message, Is.EqualTo(expectedResult));

            await _employeeRepository.Received(0).DeleteManyAsync(employee);
        }
    }
}