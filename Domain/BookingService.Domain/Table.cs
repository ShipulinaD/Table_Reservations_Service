using BookingService.Domain.Base;
using BookingService.Domain.Exceptions;
using BookingService.ValueObjects;

namespace BookingService.Domain
{
    /// <summary>
    /// Represents the table in a cafe.
    /// </summary>
    public class Table : Entity<Guid>
    {
        private readonly ICollection<Reservation> _reservations = new List<Reservation>();

        public int Number { get; private set; }

        public Seats Seats { get; private set; } = default!;

        public Cafe Cafe { get; } = default!;

        // Расписание столика — все его брони
        public IReadOnlyCollection<Reservation> Reservations =>
            _reservations.ToList().AsReadOnly();

        protected Table()
        {
        }

        public Table(Cafe cafe, int number, Seats seats)
            : this(Guid.NewGuid(), cafe, number, seats) { }

        protected Table(Guid id, Cafe cafe, int number, Seats seats) : base(id)
        {
            Cafe = cafe ?? throw new ArgumentNullValueException(nameof(cafe));
            Seats = seats ?? throw new ArgumentNullValueException(nameof(seats));

            if (number < 1)
                throw new ArgumentOutOfRangeException(nameof(number), "Table number must be positive.");

            Number = number;
        }

        public bool SetSeats(Seats newSeats)
        {
            if (newSeats == null) throw new ArgumentNullValueException(nameof(newSeats));
            if (Seats == newSeats) return false;
            Seats = newSeats;
            return true;
        }

        // Добавляет бронь в расписание столика.
        // Если столик уже занят на этот интервал — бросает исключение.
        internal void AddReservation(Reservation reservation)
        {
            if (reservation == null) throw new ArgumentNullValueException(nameof(reservation));

            var conflict = _reservations.FirstOrDefault(r => r.OverlapsWith(reservation.TimeRange));
            if (conflict != null)
                throw new TableAlreadyBookedException(this, conflict);

            _reservations.Add(reservation);
        }
    }
}