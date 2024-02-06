# Серверное приложение для сервиса доставки еды

## Обзор

Этот репозиторий содержит серверную часть приложения для сервиса доставки еды. Приложение написано на C# с использованием фреймворка ASP.net. В качестве базы данных используется Postgres, а для взаимодействия с ней используется Entity Framework Core. Проект был создан в учебных целях.

## Возможности приложения

- **Управление пользователями:** Реализованы механизмы регистрации, входа, выхода и аутентификации пользователей.
- **Корзина товаров:** Возможность добавления/удаления блюд в корзину для последующего оформления заказа.
- **Оформление заказа:** Создание заказа на основе содержимого корзины.
- **Отслеживание заказа:** Реализован механизм отслеживания статуса выполнения заказа.

## Начало работы

Для локального запуска проекта выполните следующие шаги:

1. Клонируйте репозиторий:

```bash
git clone https://github.com/L11D/FoodDelivery.git
```

2. Перейдите в директорию проекта:

```bash
cd FoodDelivery
```

3. Установите зависимости:

```bash
dotnet restore
```

4. Настройте подключение к базе данных в файле `appsettings.json`.

5. Запустите приложение:

```bash
dotnet run
```

---

# Food Delivery Service Server Application

## Overview

This repository contains the server-side application for a food delivery service. The application is written in C# using the ASP.net framework. Postgres is used as the database, and Entity Framework Core is employed for database connectivity. The project was created for educational purposes.

## Application Features

- **User Management:** Mechanisms for user registration, login, logout, and authentication have been implemented.
- **Shopping Cart:** The ability to add/remove dishes to/from the cart for subsequent order placement.
- **Order Placement:** Creating an order based on the cart contents.
- **Order Tracking:** A mechanism for tracking the status of order fulfillment has been implemented.

## Getting Started

To run the project locally, follow these steps:

1. Clone the repository:

```bash
git clone https://github.com/L11D/FoodDelivery.git
```

2. Navigate to the project directory:

```bash
cd FoodDelivery
```

3. Install dependencies:

```bash
dotnet restore
```

4. Configure the database connection in the `appsettings.json` file.

5. Run the application:

```bash
dotnet run
```

The server-side application should now be running, and you can integrate it with the frontend of your food delivery service.
