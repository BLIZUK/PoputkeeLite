# Poputkee
Приложение по поиску попутчиков и водителей в дорогу.

## 🔹Цель работы

 1. Создание продукта для курсовой работы 2 курса 2 семестра университета СФ МЭИ Кафедры ВТ. 
 2. Более глубокое знакомство с архитектурой MVVM, ~~с базами данных, а именно - PostgreSQL,~~ оттачивания навыков работы с языком программирования C#.
 3. Цель проектирования: 
    - "Разработка программы поиска попутчиков в дороге."
 4. Технические требования:
    - Создание возможности выбора типа поездки (за рулем или в качестве пассажира).
    - Добавить базу данных для пользователей.
    - Реализовать оценку пользователей.


## 🔹Стек технологий

1. Язык: C# (.NET 6/7/8).

2. ~~База данных: PostgreSQL (востребованная, открытая, поддерживает сложные запросы).~~

3. ~~ORM: Entity Framework Core (для работы с БД).~~

4. Архитектура:
    - MVVM (Model-View-ViewModel) + Dependency Injection.
    - Repository Pattern + Unit of Work.
    - MediatR (опционально, для CQRS).

5. Фреймворк для UI: WPF (с привязкой данных, стилями, ControlTemplate).

6. Дополнительно:
    - AutoMapper (для маппинга DTO).
    - FluentValidation (валидация данных).
    - Serilog (логгирование).

## 🔹Структурное дерево проекта
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

## 🔹Пример работы продукта


## 🔹Особенности установки и сборки

1. ~~Установка Nuget-пакетов:~~
    - ~~Install-Package Microsoft.EntityFrameworkCore~~
    - ~~Install-Package Microsoft.EntityFrameworkCore.Design~~
    - ~~Install-Package Microsoft.EntityFrameworkCore.Tools~~

## 🔹Ход работы:
1. Начало разработки - 01.06.2025 .
