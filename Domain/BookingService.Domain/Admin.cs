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
        /// Подтверждает бронь от имени администратора этого кафе.
        /// Правила:
        ///  - бронь должна относиться к кафе этого админа;
        ///  - столик должен быть свободен на интервал брони (нет других активных пересекающихся броней).
        /// Use-case: "Подтвердить бронь".
        /// </summary>
        public void ConfirmReservation(Reservation reservation)
        {
            if (reservation == null) throw new ArgumentNullValueException(nameof(reservation));

            // 1) Админ работает только со своим кафе
            if (reservation.Table.Cafe != this.Cafe)
                throw new AnotherCafeAdminException(reservation.Table.Cafe, this);

            // 2) Столик должен быть свободен на интервал этой брони.
            //    Сама бронь учитываться не должна — её состояние решит Reservation.Confirm.
            var hasConflict = reservation.Table.Reservations
                .Any(r => r != reservation && r.OverlapsWith(reservation.TimeRange));

            if (hasConflict)
                throw new TableAlreadyBookedException(reservation.Table, reservation);

            reservation.Confirm(this);
        }

        /// <summary>
        /// Отклоняет бронь от имени администратора этого кафе.
        /// Правило: бронь должна относиться к кафе этого админа.
        /// Use-case: "Отклонить бронь".
        /// </summary>
        public void RejectReservation(Reservation reservation)
        {
            if (reservation == null) throw new ArgumentNullValueException(nameof(reservation));

            // Админ работает только со своим кафе
            if (reservation.Table.Cafe != this.Cafe)
                throw new AnotherCafeAdminException(reservation.Table.Cafe, this);

            reservation.Reject(this);
        }

    }
}