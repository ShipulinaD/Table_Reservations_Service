using BookingService.Domain.Base;
using BookingService.Domain.Exceptions;
using BookingService.ValueObjects;

namespace BookingService.Domain
{
    /// <summary>
    /// Представляет пользователя (клиента, который бронирует столики).
    /// </summary>
    public class User(Guid id, Name name, Phone phone) : Entity<Guid>(id)
    {
        /// <summary>
        /// Бронирования пользователя.
        /// </summary>
        private readonly ICollection<Reservation> _reservations = [];

        /// <summary>
        /// Получает имя пользователя.
        /// </summary>
        public Name Name { get; private set; } = name ?? throw new ArgumentNullValueException(nameof(name));

        /// <summary>
        /// Получает номер телефона пользователя.
        /// </summary>
        public Phone Phone { get; private set; } = phone ?? throw new ArgumentNullValueException(nameof(phone));

        /// <summary>
        /// Получает бронирования пользователя.
        /// </summary>
        public IReadOnlyCollection<Reservation> Reservations =>
            _reservations.ToList().AsReadOnly();

        /// <summary>
        /// Создаёт новое бронирование столика на указанный временной интервал.
        /// Use-case: "Создать бронь".
        /// </summary>
        public Reservation CreateReservation(Table table, TimeRange timeRange, GuestsCount guestsCount)
        {
            if (table == null) throw new ArgumentNullValueException(nameof(table));
            if (timeRange == null) throw new ArgumentNullValueException(nameof(timeRange));
            if (guestsCount == null) throw new ArgumentNullValueException(nameof(guestsCount));

            // Проверка: количество гостей не должно превышать количество мест за столиком
            if (guestsCount.Value > table.Seats.Value)
                throw new GuestsExceedSeatsException(table, guestsCount.Value);

            var reservation = new Reservation(this, table, timeRange, guestsCount);

            // регистрируем бронь в расписании столика
            // если столик уже занят на это время - здесь будет TableAlreadyBookedException
            table.AddReservation(reservation);

            _reservations.Add(reservation);
            return reservation;
        }

        /// <summary>
        /// Отменяет бронирование, сделанное этим пользователем.
        /// Use-case: "Отменить бронь".
        /// </summary>
        /// <param name="reservation">Бронирование, которое нужно отменить.</param>
        /// <returns><c>true</c>, если бронирование было успешно отменено.</returns>
        /// <exception cref="ArgumentNullValueException">
        /// Если <paramref name="reservation"/> равно <c>null</c>.
        /// </exception>
        /// <exception cref="AnotherUserCancelReservationException">
        /// Если бронирование не принадлежит этому пользователю
        /// или отсутствует в его списке бронирований.
        /// </exception>
        public bool CancelReservation(Reservation reservation)
        {
            // Проверка: бронирование не должно быть null
            if (reservation == null)
                throw new ArgumentNullValueException(nameof(reservation));

            // Проверка: пользователь может отменить только свои бронирования
            if (reservation.User != this)
                throw new AnotherUserCancelReservationException(reservation, this);

            // Проверка: бронирование должно присутствовать в списке бронирований пользователя
            if (!_reservations.Contains(reservation))
                throw new AnotherUserCancelReservationException(reservation, this);

            // Сам Reservation отвечает за проверку статуса и времени
            // (InvalidReservationStatusException / CancelPastReservationException)
            reservation.Cancel();
            return true;
        }
    }
}