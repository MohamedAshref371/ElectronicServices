# ElectronicServices

**ElectronicServices** is a Windows desktop business application built with **.NET 9 WinForms** for managing electronic payment services. It provides an efficient solution for handling customers, transactions, digital wallets, expenses, and daily financial reports.

## Overview

ElectronicServices helps businesses that offer electronic payment and mobile wallet services organize their daily operations through a simple and fast desktop interface.

## Key Features

- 👥 Customer management (add, edit, search, and organize customers)
- 💳 Transaction management
- 📱 Mobile wallet and payment app management
- 📒 Record management
- 💰 Expense tracking
- 📊 Daily closure and PayApp closure reports
- 📈 Excel export using **ClosedXML**
- 🔍 Fast search and navigation
- ⌨️ Keyboard and mouse friendly interface
- 📄 Custom dialog and message windows

## Architecture

- **Framework:** .NET 9 WinForms
- **Database:** SQLite
- **UI Library:** Guna.UI2
- **Export Library:** ClosedXML

Database creation and schema management are handled by **DatabaseHelper**.

## Runtime Features

- ✅ Single-instance protection using a named `Mutex`
- 📝 Global exception logging
- 🕒 Timestamped error information saved to `Errors.txt`
- 🔧 Startup dependency validation (including Guna.UI2)

## Technologies Used

- .NET 9
- Windows Forms (WinForms)
- SQLite
- Guna.UI2
- ClosedXML

## Purpose

ElectronicServices is designed to simplify the daily workflow of businesses that provide electronic payment services by offering a fast, reliable, and easy-to-use desktop application.
