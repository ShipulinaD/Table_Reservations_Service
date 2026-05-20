using BookingService.Domain.Base;
using BookingService.Domain.Exceptions;
using BookingService.ValueObjects;

namespace BookingService.Domain
{
    /// <summary>
    /// Represents the admin of a cafe.
    /// </summary>
    public class Admin : Entity<Guid>
    {
        public Name Name { get; } = default!;

        public Phone Phone { get; } = default!;

        public Cafe Cafe { get; } = default!;

        protected Admin()
        {
        }

        public Admin(Cafe cafe, Name name, Phone phone)
            : this(Guid.NewGuid(), cafe, name, phone) { }

        protected Admin(Guid id, Cafe cafe, Name name, Phone phone) : base(id)
        {
            Cafe = cafe ?? throw new ArgumentNullValueException(nameof(cafe));
            Name = name ?? throw new ArgumentNullValueException(nameof(name));
            Phone = phone ?? throw new ArgumentNullValueException(nameof(phone));
        }

        /// <summary>
        /// Confirms a reservation on behalf of the cafe this admin manages.
        /// Use-case: "Подтвердить бронь".
        /// </summary>
        public void ConfirmReservation(Reservation reservation)
        {
            if (reservation == null) throw new ArgumentNullValueException(nameof(reservation));
            reservation.Confirm(this);
        }

        /// <summary>
        /// Rejects a reservation on behalf of the cafe this admin manages.
        /// Use-case: "Отклонить бронь".
        /// </summary>
        public void RejectReservation(Reservation reservation)
        {
            if (reservation == null) throw new ArgumentNullValueException(nameof(reservation));
            reservation.Reject(this);
        }
    }
}