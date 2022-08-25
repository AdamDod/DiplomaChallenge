using System;
using Xunit;
using Classes;

namespace tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(5,5)]
        [InlineData(1,5)]
        [InlineData(0,5)]
        [InlineData(5,0)]
        [InlineData(-5,5)]
        [InlineData(5,-5)]
        public void TestTotal(int qty, int cost)
        {
            var order = new Order().total(qty,cost);
            var result = qty * cost;

            Assert.Equal(order,result);
        }

        [Theory]
        [InlineData(5,5)]
        [InlineData(1,5)]
        [InlineData(0,5)]
        [InlineData(5,0)]
        [InlineData(-5,5)]
        [InlineData(5,-5)]
        public void TestGST(int qty, float cost)
        {
            var order = new Order().GST(qty,cost);
            var result = (qty * cost)/10;

            Assert.Equal(order,result);
        }
    }
}
