using Pharmacy.Domain.Entities;
using Pharmacy.Domain.Exceptions;

namespace Pharmacy.Tests;

public class StorageSlotTests
{
    [Fact]
    public void Add_IncreasesQuantity()
    {
        var slot = new StorageSlot("A-01", Guid.NewGuid(), capacity: 10);

        slot.Add(4);

        Assert.Equal(4, slot.Quantity);
    }

    [Fact]
    public void Add_BeyondCapacity_Throws()
    {
        var slot = new StorageSlot("A-01", Guid.NewGuid(), capacity: 5);

        Assert.Throws<DomainException>(() => slot.Add(6));
    }

    [Fact]
    public void Remove_MoreThanAvailable_Throws()
    {
        var slot = new StorageSlot("A-01", Guid.NewGuid(), capacity: 5);
        slot.Add(2);

        Assert.Throws<DomainException>(() => slot.Remove(3));
    }
}
