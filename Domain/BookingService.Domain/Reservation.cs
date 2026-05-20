using BookingService.Domain.Base;
using BookingService.Domain.Exceptions;
using BookingService.ValueObjects;

namespace BookingService.Domain
{
    /// <summary>
    /// Represents the reservation of a table by a user for a time range.
    /// </summary>
    public class Reservation : Entity<Guid>
    {
        public User User { get; } = default!;

        public Table Table { get; } = default!;

        public TimeRange TimeRange { get; } = default!; ///

        public GuestsCount GuestsCount { get; } = default!;

        public ReservationStatus Status { get; private set; } = ReservationStatus.Pending;

        public Admin? ConfirmedBy { get; private set; } = null;

        protected Reservation()
        {
        }

        public Reservation(
            User user,
            Table table,
            TimeRange timeRange,
            GuestsCount guestsCount)
            : this(Guid.NewGuid(), user, table, timeRange, guestsCount) { }

        protected Reservation(
            Guid id,
            User user,
            Table table,
            TimeRange timeRange,
            GuestsCount guestsCount,
            ReservationStatus status = ReservationStatus.Pending,
            Admin? confirmedBy = null)
            : base(id)
        {
            User = user ?? throw new ArgumentNullValueException(nameof(user));
            Table = table ?? throw new ArgumentNullValueException(nameof(table));
            TimeRange = timeRange ?? throw new ArgumentNullValueException(nameof(timeRange));
            GuestsCount = guestsCount ?? throw new ArgumentNullValueException(nameof(guestsCount));

            if (guestsCount.Value > table.Seats.Value)
                throw new GuestsExceedSeatsException(table, guestsCount.Value);

            Status = status;
            ConfirmedBy = confirmedBy;
        }

        /// <summary>
        /// Confirms this reservation by an admin of the cafe the table belongs to.
        /// </summary>
        public void Confirm(Admin admin)
        {
            if (admin == null) throw new ArgumentNullValueException(nameof(admin));

            if (admin.Cafe != Table.Cafe) throw new AnotherCafeAdminException(Table.Cafe, admin);

            if (Status != ReservationStatus.Pending)
                throw new InvalidReservationStatusException(this, ReservationStatus.Reserved);

            Status = ReservationStatus.Reserved;
            ConfirmedBy = admin;
        }

        /// <summary>
        /// Rejects this reservation by an admin of the cafe the table belongs to.
        /// </summary>
        public void Reject(Admin admin)
        {
            if (admin == null) throw new ArgumentNullValueException(nameof(admin));

            if (admin.Cafe != Table.Cafe) throw new AnotherCafeAdminException(Table.Cafe, admin);

            if (Status == ReservationStatus.Cancelled)
                throw new InvalidReservationStatusException(this, ReservationStatus.Cancelled);

            Status = ReservationStatus.Cancelled;
            ConfirmedBy = admin;
        }

        /// <summary>
        /// Cancels this reservation (typically initiated by the owning user).
        /// </summary>
        internal void Cancel()
        {
            if (TimeRange.StartTime < DateTime.UtcNow)
                throw new CancelPastReservationException(this);

            if (Status == ReservationStatus.Cancelled)
                throw new InvalidReservationStatusException(this, ReservationStatus.Cancelled);

            Status = ReservationStatus.Cancelled;
        }

        /// <summary>
        /// Checks whether this reservation overlaps with the given time range.
        /// Cancelled reservations never block the table.
        /// </summary>
        public bool OverlapsWith(TimeRange other)
        {
            if (other == null) return false;
            if (Status == ReservationStatus.Cancelled) return false;
            return TimeRange.Overlaps(other);
        }
    }
}
