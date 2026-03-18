namespace VegenereCipher.Tests
{
    public class Demo
    {

        [Fact]
        public void FluentAssertions_Should_Work()
        {
            var sum = 2 + 2;
            sum.Should().Be(4);
        }

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(5, 7, 12)]
        [InlineData(-1, 1, 0)]
        public void Xunit_Theory_Should_Work(int a, int b, int expected)
        {
            var result = a + b;
            result.Should().Be(expected);
        }
    }
}