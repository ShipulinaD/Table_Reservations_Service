using BookingService.Domain.Base;
using BookingService.Domain.Exceptions;
using BookingService.ValueObjects;

namespace BookingService.Domain
{
    /// <summary>
    /// Represents the cafe. Contains a set of tables.
    /// </summary>
    public class Cafe(Guid id, Name name, Address address) : Entity<Guid>(id)
    {
        private readonly ICollection<Table> _tables = [];

        /// <summary>
        /// Gets the cafe's name.
        /// </summary>
        public Name Name { get; } = name ?? throw new ArgumentNullValueException(nameof(name));

        /// <summary>
        /// Gets the cafe's address.
        /// </summary>
        public Address Address { get; } = address ?? throw new ArgumentNullValueException(nameof(address));

        /// <summary>
        /// Gets the cafe's tables.
        /// </summary>
        public IReadOnlyCollection<Table> Tables =>
            _tables.ToList().AsReadOnly();

        /// <summary>
        /// Adds a table to the cafe.
        /// Use-case: "Управлять столиками".
        /// </summary>
        public void AddTable(Table table)
        {
            if (table == null) throw new ArgumentNullValueException(nameof(table));

            if (table.Cafe != this) throw new AnotherCafeTableException(this, table);

            if (!_tables.Contains(table))
                _tables.Add(table);
        }

        /// <summary>
        /// Removes a table from the cafe.
        /// Use-case: "Управлять столиками".
        /// </summary>
        public void RemoveTable(Table table)
        {
            if (table == null) throw new ArgumentNullValueException(nameof(table));

            if (table.Cafe != this) throw new AnotherCafeTableException(this, table);

            if (!_tables.Contains(table)) throw new TableNotBelongCafeException(this, table);

            _tables.Remove(table);
        }
    }
}