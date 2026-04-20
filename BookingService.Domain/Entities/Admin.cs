using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BookingService.Domain.Entities;


/// Сущность "Администратор" с бизнес-логикой
using BookingService.ValueObject.Validators;
public class Admin
{
    private static readonly NameValidator _nameValidator = new();
    private static readonly PhoneValidator _phoneValidator = new();

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Phone { get; private set; }
    public Guid CafeId { get; private set; }

    // Навигационные свойства
    public virtual Cafe Cafe { get; private set; }
    public virtual ICollection<Reservation> ConfirmedReservations { get; private set; }

    private Admin() { }

    public Admin(Guid id, string name, string phone, Guid cafeId)
    {
        _nameValidator.Validate(name);
        _phoneValidator.Validate(phone);

        Id = id;
        Name = name;
        Phone = phone;
        CafeId = cafeId;
        ConfirmedReservations = new List<Reservation>();
    }
}
