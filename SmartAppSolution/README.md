# SmartAppSolution (C# .NET 8)

Це готовий Visual Studio solution з мікросервісами-пристроями і центральним контролером.

**Що всередині**
- SmartApp.Common — загальні моделі
- SmartApp.Device.Speaker — ASP.NET Core minimal API (порт 5001)
- SmartApp.Device.Light — ASP.NET Core minimal API (порт 5002)
- SmartApp.Device.Curtains — ASP.NET Core minimal API (порт 5003)
- SmartApp.Device.Thermostat — ASP.NET Core minimal API (порт 5004)
- SmartApp.Controller — Console App, контролер (Facade + Logging HttpHandler)

**Вимоги**
- .NET 8 або .NET 9 SDK
- Visual Studio 2022/2023 або VS Code

**Інструкції**
1. Розпакуйте архів і відкрийте `SmartAppSolution.sln` у Visual Studio.
2. Встановіть Multiple Startup Projects (для всіх Device проектів та Controller) або запускайте окремо:
   - Speaker -> http://localhost:5001
   - Light -> http://localhost:5002
   - Curtains -> http://localhost:5003
   - Thermostat -> http://localhost:5004
3. Запустіть, відкрийте консоль контролера і використовуйте меню.

Якщо у вас будуть питання або потрібно змінити під .NET 7 — скажіть.
