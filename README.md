# Poputkee
Прототип Desktop-приложения для поиска попутчиков, разработанный в рамках курсового проекта.
Основной акцент сделан на правильную архитектуру **MVVM** и построение отзывчивого UI.

> **Автор:** Близученко Андрей (ИВТ2-23)
> **Статус:** Курсовая работа (защищена)

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

## Пример работы продукта



 Поиск и создание поездок
![Поиск и создание поездок](https://github.com/BLIZUK/PoputkeeLite/blob/master/images/image2.png)

Редактирование аккаунта
![Редактирование аккаунта](https://github.com/BLIZUK/PoputkeeLite/blob/master/images/image4.png)

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
