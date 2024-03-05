using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestFixture]
    public class CalculatorTests
    {
        /// <summary>
        /// Hàm test cộng thành công 2 số nguyên
        /// </summary>
        /// 
        [TestCase(1, 2, 3)]
        [TestCase(2, 3, 5)]
        [TestCase(-1, 7, 6)]
        [TestCase(int.MaxValue, int.MaxValue, int.MaxValue * (long)2)]
        public void Add_ValidInput_ReturnsSum(int x, int y, long expectedResult)
        {
            // Action
            var actualResult = new Calculator().Add(x, y);
            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        /// <summary>
        /// Hàm test trừ thành công 2 số nguyên
        /// </summary>
        /// 
        [TestCase(1, -2, 3)]
        [TestCase(2, 3, -1)]
        [TestCase(-1, -7, 6)]
        [TestCase(int.MaxValue, int.MinValue, int.MaxValue * (long)2 + 1)]

        public void Sub_ValidInput_ReturnsDifference(int x, int y, long expectedResult)
        {
            // Action
            var actualResult = new Calculator().Sub(x, y);
            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        /// <summary>
        /// Hàm test nhân thành công 2 số nguyên
        /// </summary>
        /// 
        [TestCase(1, 2, 2)]
        [TestCase(2, 3, 6)]
        [TestCase(1, -7, -7)]
        [TestCase(int.MaxValue, int.MinValue, int.MaxValue * (long)int.MinValue)]

        public void Mul_ValidInput_ReturnsProduct(int x, int y, long expectedResult)
        {
            // Action
            var actualResult = new Calculator().Mul(x, y);
            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        /// <summary>
        /// Hàm test chia một số cho 0, ném ra exception
        /// </summary>
        /// 
        [Test]
        public void Div_ZeroInput_ThrowsException()
        {
            // Arrange
            var y = 0;
            var x = 1;
            var expectedMessage = "Không thể chia cho 0";

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => { new Calculator().Div(x, y); });
            Assert.That(exception.Message, Is.EqualTo(expectedMessage));
        }

        /// <summary>
        /// Hàm test chia thành công hai số nguyên
        /// </summary>
        /// 
        [TestCase(1, 2, 0.5f)]
        [TestCase(1, 3, 1 / (float)3)]
        [TestCase(int.MaxValue, 3, int.MaxValue / (double)3)]
        public void Div_ValidInput_ReturnsQuotient(int x, int y, double expectedResult)
        {
            // Actual
            var actualResult = new Calculator().Div(x, y);

            // Assert
            Assert.That(Math.Abs(actualResult - expectedResult), Is.LessThan(10e-3));
        }

        /// <summary>
        /// Hàm test tính tổng các số nguyên dương, với đầu vào là chuỗi rỗng hoặc null
        /// </summary>
        /// Created by: LXQuynh (13/08/2023)
        [TestCase("")]
        [TestCase(null)]
        public void Add_EmptyString_ReturnsZero(string input)
        {
            // Arrange
            var expectedResult = 0;

            // Actual
            var actualResult = new Calculator().Add(input);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        /// <summary>
        /// Hàm test tính tổng các số nguyên dương, với đầu vào là chuỗi chỉ có một số nguyên dương
        /// </summary>
        /// Created by: LXQuynh (13/08/2023)
        [Test]
        public void Add_SingleNumber_ReturnsNumber()
        {
            // Arrange
            var expectedResult = 1;

            // Actual
            var actualResult = new Calculator().Add("1");

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        /// <summary>
        /// Hàm test tính tổng các số nguyên dương, với đầu vào là chuỗi gồm các số nguyên dương ngăn cách nhau bởi dấu phẩy và không có khoảng trắng
        /// </summary>
        /// Created by: LXQuynh (13/08/2023)
        [TestCase("1,2", 3)]
        [TestCase("1,2,3", 6)]
        [TestCase("2147483647,2147483647,2147483647,2147483647", (long)4 * int.MaxValue)]
        public void Add_MultipleNumbers_ReturnsSum(string input, long expectedResult)
        {
            // Actual
            var actualResult = new Calculator().Add(input);
            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }


        /// <summary>
        /// Hàm test tính tổng các số nguyên dương, với đầu vào là chuỗi gồm các số nguyên dương ngăn cách nhau bởi dấu phẩy và có khoảng trắng
        /// </summary>
        /// Created by: LXQuynh (13/08/2023)
        [TestCase("1, 2", 3)]
        [TestCase("1, 2, 3", 6)]
        [TestCase("2147483647, 2147483647, 2147483647, 2147483647", (long)4 * int.MaxValue)]
        public void Add_NumbersWithSpaces_ReturnsSum(string input, long expectedResult)
        {
            // Actual
            var actualResult = new Calculator().Add(input);
            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        /// <summary>
        /// Hàm test tính tổng các số nguyên dương, với đầu vào là chuỗi gồm các số nguyên dương ngăn cách nhau bởi dấu phẩy và có nhiều khoảng trắng
        /// </summary>
        /// Created by: LXQuynh (13/08/2023)
        [TestCase("      1            , 2             ", 3)]
        [TestCase("      1,2           , 3", 6)]
        [TestCase("     2147483647,2147483647,    2147483647,2147483647       ", (long)4 * int.MaxValue)]
        public void Add_MultipleSpacesBetweenNumbers_ReturnsSum(string input, long expectedResult)
        {
            // Actual
            var actualResult = new Calculator().Add(input);
            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        /// <summary>
        /// Hàm test tính tổng các số nguyên dương, với đầu vào là chuỗi gồm các số nguyên trong đó có chứa cả số nguyên âm, ném ra exception
        /// </summary>
        /// Created by: LXQuynh (13/08/2023)
        [TestCase("1, -2, -3", "Không chấp nhận toán tử âm: -2, -3")]
        [TestCase("-1, 2, -3", "Không chấp nhận toán tử âm: -1, -3")]
        [TestCase("1, 2, -3", "Không chấp nhận toán tử âm: -3")]
        public void Add_NegativeNumbers_ThrowsException(string input, string expectedResult)
        {
            // Actual & Assert
            var ex = Assert.Throws<ArgumentException>(() => new Calculator().Add(input));
            Assert.That(ex.Message, Is.EqualTo(expectedResult));
        }

        /// <summary>
        /// Hàm test tính tổng các số nguyên dương, với đầu vào là chuỗi gồm các số nguyên dương ngăn cách nhau bởi dấu phẩy và có chứa kí tự đặc biệt
        /// </summary>
        /// Created by: LXQuynh (13/08/2023)
        [TestCase("1, a, 2", "Giá trị không hợp lệ: a")]
        [TestCase("1, b, a", "Giá trị không hợp lệ: b")]
        [TestCase("1, ., 2", "Giá trị không hợp lệ: .")]
        [TestCase("$, a, b", "Giá trị không hợp lệ: $")]
        public void Add_InvalidValue_ThrowsException(string input, string expectedResult)
        {
            // Actual & Assert
            var ex = Assert.Throws<ArgumentException>(() => new Calculator().Add(input));
            Assert.That(ex.Message, Is.EqualTo(expectedResult));
        }
    }
}
