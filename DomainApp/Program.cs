using BookingService.Domain.Entities;
using BookingService.Domain.Exceptions;
using BookingService.ValueObject;

namespace BookingService.ConsoleDemo;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("=== Демонстрация работы Domain слоя ===");
        Console.WriteLine();

        // Создаём тестовые данные
        var cafeId = Guid.NewGuid();
        var tableId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var adminId = Guid.NewGuid();

        // Создаём кафе
        var cafe = new Cafe(cafeId, "Уютное кафе", "ул. Центральная, 15");
        Console.WriteLine($"✅ Создано кафе: {cafe.Name}");

        // Создаём стол (на 4 места)
        var table = new Table(tableId, cafeId, 1, 4);
        cafe.AddTable(table);
        Console.WriteLine($"✅ Создан стол №{table.Number} на {table.Seats} места");

        // Создаём пользователя
        var user = new User(userId, "Анна Петрова", "+7 (999) 123-45-67");
        Console.WriteLine($"✅ Создан пользователь: {user.Name}");

        // Создаём администратора
        var admin = new Admin(adminId, "Иван Админов", "+7 (999) 987-65-43", cafeId);
        cafe.AddAdmin(admin);
        Console.WriteLine($"✅ Создан администратор: {admin.Name}");

        Console.WriteLine();
        Console.WriteLine(new string('=', 60));
        Console.WriteLine();

        // ========== ТЕСТ 1: Успешное создание бронирования ==========
        Console.WriteLine("ТЕСТ 1: Успешное создание бронирования");
        Console.WriteLine("----------------------------------------");

        var startTime = DateTime.UtcNow.AddHours(2);
        var endTime = DateTime.UtcNow.AddHours(4);

        try
        {
            var reservation = new Reservation(
                Guid.NewGuid(),
                userId,
                tableId,
                startTime,
                endTime,
                3, // 3 гостя при 4 местах - ок
                table
            );

            Console.WriteLine($"✅ Бронирование создано успешно!");
            Console.WriteLine($"   Время: с {startTime:HH:mm} до {endTime:HH:mm}");
            Console.WriteLine($"   Гостей: 3, Статус: {reservation.Status}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Ошибка: {ex.Message}");
        }

        Console.WriteLine();

        // ========== ТЕСТ 2: Ошибка - гостей больше, чем мест ==========
        Console.WriteLine("ТЕСТ 2: Гостей больше, чем мест за столом");
        Console.WriteLine("----------------------------------------");

        try
        {
            var reservation = new Reservation(
                Guid.NewGuid(),
                userId,
                tableId,
                DateTime.UtcNow.AddHours(5),
                DateTime.UtcNow.AddHours(7),
                6, // 6 гостей при 4 местах - ошибка
                table
            );
        }
        catch (GuestsExceedSeatsException ex)
        {
            Console.WriteLine($"❌ Ожидаемая ошибка: {ex.Message}");
        }

        Console.WriteLine();

        // ========== ТЕСТ 3: Ошибка - бронирование в прошлом ==========
        Console.WriteLine("ТЕСТ 3: Бронирование в прошлом");
        Console.WriteLine("-------------------------------");

        try
        {
            var reservation = new Reservation(
                Guid.NewGuid(),
                userId,
                tableId,
                DateTime.UtcNow.AddHours(-2), // в прошлом
                DateTime.UtcNow.AddHours(-1),
                2,
                table
            );
        }
        catch (InvalidReservationTimeException ex)
        {
            Console.WriteLine($"❌ Ожидаемая ошибка: {ex.Message}");
        }

        Console.WriteLine();

        // ========== ТЕСТ 4: Ошибка - start_time >= end_time ==========
        Console.WriteLine("ТЕСТ 4: Время начала больше или равно времени окончания");
        Console.WriteLine("------------------------------------------------------");

        try
        {
            var reservation = new Reservation(
                Guid.NewGuid(),
                userId,
                tableId,
                DateTime.UtcNow.AddHours(3),
                DateTime.UtcNow.AddHours(2), // end_time раньше start_time
                2,
                table
            );
        }
        catch (InvalidReservationTimeException ex)
        {
            Console.WriteLine($"❌ Ожидаемая ошибка: {ex.Message}");
        }

        Console.WriteLine();

        // ========== ТЕСТ 5: Подтверждение брони ==========
        Console.WriteLine("ТЕСТ 5: Подтверждение брони администратором");
        Console.WriteLine("-------------------------------------------");

        var validReservation = new Reservation(
            Guid.NewGuid(),
            userId,
            tableId,
            DateTime.UtcNow.AddDays(1),
            DateTime.UtcNow.AddDays(1).AddHours(2),
            2,
            table
        );

        Console.WriteLine($"Статус до подтверждения: {validReservation.Status}");
        Console.WriteLine($"ConfirmedBy до: {validReservation.ConfirmedBy}");

        validReservation.Confirm(adminId);

        Console.WriteLine($"✅ Бронь подтверждена!");
        Console.WriteLine($"Статус после подтверждения: {validReservation.Status}");
        Console.WriteLine($"ConfirmedBy после: {validReservation.ConfirmedBy}");

        Console.WriteLine();

        // ========== ТЕСТ 6: Ошибка - подтверждение отменённой брони ==========
        Console.WriteLine("ТЕСТ 6: Подтверждение отменённой брони");
        Console.WriteLine("--------------------------------------");

        var cancelledReservation = new Reservation(
            Guid.NewGuid(),
            userId,
            tableId,
            DateTime.UtcNow.AddDays(2),
            DateTime.UtcNow.AddDays(2).AddHours(2),
            2,
            table
        );

        cancelledReservation.Cancel();
        Console.WriteLine($"Бронь отменена. Статус: {cancelledReservation.Status}");

        try
        {
            cancelledReservation.Confirm(adminId);
        }
        catch (ConfirmCancelledReservationException ex)
        {
            Console.WriteLine($"❌ Ожидаемая ошибка: {ex.Message}");
        }

        Console.WriteLine();

        // ========== ТЕСТ 7: Отмена брони ==========
        Console.WriteLine("ТЕСТ 7: Отмена брони (до начала)");
        Console.WriteLine("---------------------------------");

        var futureReservation = new Reservation(
            Guid.NewGuid(),
            userId,
            tableId,
            DateTime.UtcNow.AddDays(3),
            DateTime.UtcNow.AddDays(3).AddHours(2),
            2,
            table
        );

        Console.WriteLine($"Статус до отмены: {futureReservation.Status}");
        futureReservation.Cancel();
        Console.WriteLine($"✅ Бронь отменена!");
        Console.WriteLine($"Статус после отмены: {futureReservation.Status}");

        Console.WriteLine();

        // ========== ТЕСТ 8: Ошибка - отмена прошедшей брони ==========
        Console.WriteLine("ТЕСТ 8: Отмена уже прошедшей брони");
        Console.WriteLine("----------------------------------");

        // Создаём бронь в прошлом (обходим валидацию через рефлексию для демонстрации)
        var pastReservation = new Reservation(
            Guid.NewGuid(),
            userId,
            tableId,
            DateTime.UtcNow.AddDays(-1),
            DateTime.UtcNow.AddDays(-1).AddHours(2),
            2,
            table
        );

        try
        {
            pastReservation.Cancel();
        }
        catch (CancelPastReservationException ex)
        {
            Console.WriteLine($"❌ Ожидаемая ошибка: {ex.Message}");
        }

        Console.WriteLine();

        // ========== ТЕСТ 9: Проверка пересечения времени ==========
        Console.WriteLine("ТЕСТ 9: Проверка пересечения временных интервалов");
        Console.WriteLine("------------------------------------------------");

        var res1 = new Reservation(
            Guid.NewGuid(),
            userId,
            tableId,
            DateTime.UtcNow.AddHours(10),
            DateTime.UtcNow.AddHours(12),
            2,
            table
        );

        var res2 = new Reservation(
            Guid.NewGuid(),
            userId,
            tableId,
            DateTime.UtcNow.AddHours(11),
            DateTime.UtcNow.AddHours(13),
            2,
            table
        );

        bool overlaps = res1.OverlapsWith(res2.StartTime, res2.EndTime);
        Console.WriteLine($"Бронь 1: с {res1.StartTime:HH:mm} до {res1.EndTime:HH:mm}");
        Console.WriteLine($"Бронь 2: с {res2.StartTime:HH:mm} до {res2.EndTime:HH:mm}");
        Console.WriteLine($"Пересекаются? {overlaps} ✅ (ожидаем true)");

        var res3 = new Reservation(
            Guid.NewGuid(),
            userId,
            tableId,
            DateTime.UtcNow.AddHours(14),
            DateTime.UtcNow.AddHours(16),
            2,
            table
        );

        overlaps = res1.OverlapsWith(res3.StartTime, res3.EndTime);
        Console.WriteLine();
        Console.WriteLine($"Бронь 1: с {res1.StartTime:HH:mm} до {res1.EndTime:HH:mm}");
        Console.WriteLine($"Бронь 3: с {res3.StartTime:HH:mm} до {res3.EndTime:HH:mm}");
        Console.WriteLine($"Пересекаются? {overlaps} ✅ (ожидаем false)");

        Console.WriteLine();
        Console.WriteLine(new string('=', 60));
        Console.WriteLine("=== Демонстрация завершена ===");
        Console.WriteLine("Нажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}