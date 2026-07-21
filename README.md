# ElectronicServices

This is a Windows desktop business application (WinForms) named "ElectronicServices". Short description:
-	Purpose: manage electronic payment services — customers, transactions, wallets (mobile/payment apps), records, expenses, and daily/closure reports.
-	Architecture: .NET 9 WinForms application with custom user controls for rows (CustomerRow, TransactionRow, WalletRow, RecordRow, ExpenseRow).
-	Data storage: local SQLite database stored at data/ProgData.ds with images in data/images and backups in data/backup; database schema created/managed by DatabaseHelper.
-	Features: add/edit/search customers and transactions, wallet management, daily closures and payapp closures, expense tracking, export to Excel via ClosedXML.
-	UI/UX: uses Guna.UI2 (checked at startup), custom message/dialog forms, and keyboard/mouse navigation and paging.
-	Runtime behavior: single-instance enforcement via a named Mutex; global exception logging to Errors.txt; error metadata saved with timestamps.
-	Notable files: Program.cs (startup, dependency and single-instance checks), DatabaseHelper.cs (DB access and schema), Form1.cs (main UI and app logic).
