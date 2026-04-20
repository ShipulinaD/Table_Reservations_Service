using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BookingService.ValueObject.Validators;

namespace BookingService.Domain.Entities;


/// Сущность "Пользователь" с бизнес-логикой
public class User
{
    private static readonly NameValidator _nameValidator = new();
    private static readonly PhoneValidator _phoneValidator = new();

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Phone { get; private set; }

    // Навигационные свойства
    public virtual ICollection<Reservation> Reservations { get; private set; }

    private User() { }

    public User(Guid id, string name, string phone)
    {
        _nameValidator.Validate(name);
        _phoneValidator.Validate(phone);

        Id = id;
        Name = name;
        Phone = phone;
        Reservations = new List<Reservation>();
    }

   
    /// Обновление имени пользователя
    public void UpdateName(string newName)
    {
        _nameValidator.Validate(newName);
        Name = newName;
    }

   
    /// Обновление телефона пользователя
    public void UpdatePhone(string newPhone)
    {
        _phoneValidator.Validate(newPhone);
        Phone = newPhone;
    }
}