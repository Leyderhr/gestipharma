using Pharmacy.Domain.Entities;
using Pharmacy.Domain.Enums;
using Pharmacy.Domain.Exceptions;

namespace Pharmacy.Tests;

public class DispensingOrderTests
{
    [Fact]
    public void NewOrder_StartsAsPending()
    {
        var order = new DispensingOrder(Guid.NewGuid(), 2);

        Assert.Equal(OrderStatus.Pending, order.Status);
    }

    [Fact]
    public void HappyPath_PendingToPickingToDispensed()
    {
        var order = new DispensingOrder(Guid.NewGuid(), 2);

        order.StartPicking();
        order.MarkDispensed();

        Assert.Equal(OrderStatus.Dispensed, order.Status);
    }

    [Fact]
    public void Dispensed_CannotBeCancelled()
    {
        var order = new DispensingOrder(Guid.NewGuid(), 1);
        order.StartPicking();
        order.MarkDispensed();

        Assert.Throws<DomainException>(() => order.Cancel());
    }

    [Fact]
    public void Fail_StoresReason()
    {
        var order = new DispensingOrder(Guid.NewGuid(), 1);
        order.StartPicking();

        order.Fail("Atasco en el brazo robótico");

        Assert.Equal(OrderStatus.Failed, order.Status);
        Assert.Equal("Atasco en el brazo robótico", order.FailureReason);
    }

    // [Theory] + [InlineData] = una prueba parametrizada (como @ParameterizedTest en JUnit 5).
    [Theory]
    [InlineData(0)]
    [InlineData(-3)]
    public void Order_WithInvalidQuantity_Throws(int quantity)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new DispensingOrder(Guid.NewGuid(), quantity));
    }
}
