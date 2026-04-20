using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BookingService.ValueObject.Validators;

namespace BookingService.Domain.Entities;

/// Сущность "Кафе" с бизнес-логикой
public class Cafe
{
    private static readonly NameValidator _nameValidator = new();

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Address { get; private set; }

    // Навигационные свойства
    public virtual ICollection<Table> Tables { get; private set; }
    public virtual ICollection<Admin> Admins { get; private set; }

    private Cafe() { }

    public Cafe(Guid id, string name, string address)
    {
        _nameValidator.Validate(name);

        Id = id;
        Name = name;
        Address = address;
        Tables = new List<Table>();
        Admins = new List<Admin>();
    }


    /// Добавление стола в кафе
    public void AddTable(Table table)
    {
        if (table.CafeId != Id)
            throw new InvalidOperationException("Стол принадлежит другому кафе");

        Tables.Add(table);
    }


    /// Добавление администратора в кафе
    public void AddAdmin(Admin admin)
    {
        if (admin.CafeId != Id)
            throw new InvalidOperationException("Администратор принадлежит другому кафе");

        Admins.Add(admin);
    }
}