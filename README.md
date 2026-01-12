# Poputkee
Прототип Desktop-приложения для поиска попутчиков, разработанный в рамках курсового проекта.
Основной акцент сделан на правильную архитектуру **MVVM** и построение отзывчивого UI.

## Особенности реализации
*   **Архитектура:** Чистый MVVM (Model-View-ViewModel).
*   **Data Access:** Реализован паттерн **Repository**. В текущей версии для простоты развертывания используется **файловое хранилище (TXT/JSON)**. Благодаря абстракции через интерфейсы, миграция на PostgreSQL/SQLite возможна без переписывания бизнес-логики.
*   **UI:** WPF, Custom Styles, DataTemplates.

##  Функционал
*   Регистрация и авторизация пользователей.
*   Создание и поиск поездок (Водитель/Пассажир).
*   Фильтрация списка поездок.

## Стек технологий

1. Язык: C# (.NET 6/7/8).
2. WPF
3. System.IO (File Storage)

4. Архитектура:
    - MVVM (Model-View-ViewModel) + Dependency Injection.
    - Repository Pattern + Unit of Work.
    - MediatR (опционально, для CQRS).

6. Дополнительно:
    - AutoMapper (для маппинга DTO).
    - FluentValidation (валидация данных).
    - Serilog (логгирование).

## Структурное дерево проекта
```
PoputkeeLite/
├── Core/
|   ├── Common/
|   │   ├── BaseViewModel.cs
|   │   ├── RelayCommand.cs
|   │   ├── GreaterThanZeroConverter.cs
|   │   ├── WatermarkCommand.cs
|   │   └── NotNullConverter.cs
|   ├── Models/
|   │   ├── User.cs
|   │   ├── Trip.cs
|   │   └── Booking.cs
|   ├── Services/
|   │   ├── SessionService.cs
|   │   ├── DataService.cs
|   │   ├── TripService.cs
|   │   ├── NotificationService.cs
|   │   ├── TextBoxWatermarkBehavior.cs
|   │   └── BookingService.cs
├── Desktop/
├── ViewModels/
|   │   ├── LoginViewModel.cs
|   │   ├── MainViewModel.cs
|   │   ├── TripViewModel.cs
|   │   ├── BookingViewModel.cs
|   │   └── AccountViewModel.cs
|   ├── Views/
|   │   ├── LoginView.xaml
|   │   ├── MainWindow.xaml
|   │   ├── TripView.xaml
|   │   ├── BookingView.xaml
|   │   └── AccountView.xaml
├── App.xaml
└── App.xaml.cs
```
## 🔹Пример работы продукта

![Вход в аккауент](https://github.com/BLIZUK/PoputkeeLite/blob/master/images/image1.png)
![Поиск и создание поездок](https://github.com/BLIZUK/PoputkeeLite/blob/master/images/image2.png)
![Управление бронями](https://github.com/BLIZUK/PoputkeeLite/blob/master/images/image3.png)
![Редактирование аккаунта](https://github.com/BLIZUK/PoputkeeLite/blob/master/images/image4.png)
