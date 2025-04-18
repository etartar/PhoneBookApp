namespace PhoneBookApp.Core.Domain;

public interface ISoftDeletable
{
    DateTime? DeletedOn { get; set; }
}
