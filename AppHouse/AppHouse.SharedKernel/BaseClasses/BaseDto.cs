namespace AppHouse.SharedKernel.BaseClasses
{
    public abstract record BaseDto(Guid? Id, DateTime? DateCreated, bool? IsActive);
}
