# MyClock

基于 Xamarin.Forms 实现的简单跨平台App——小闹钟。

## 说明

部分代码来自微软官方示例 [xamarin/xamarin-forms-samples](https://github.com/xamarin/xamarin-forms-samples)，引用遵从 Apache 协议。

P.S. 我另使用 WindowsForms 完成了一个相同功能的 Windows 实现，详见 [cdfmlr/Alarm-WindowsForms](https://github.com/cdfmlr/Alarm-WindowsForms)。

## 实现

1. 做一个小闹钟窗体应用程序。
2. 具备实时显示数字时钟的功能，方便使用者获取当前的时间。
3. 具备实时绘制表盘时钟的功能，根据当前时间再来绘制秒针、分针和时针。
4. 具备整点报时的功能，当处于整点时，系统进行报时。
5. 具备定点报时的功能，根据使用者的设定，进行报时。
6. 把使用者设置的报时时间，记录在 SQLite 数据库中，系统在启动时，自动加载该文本文件以便获取定点报时的时间。

## 协议

Copyright 2019 CDFMLR, 遵循 Apache 协议。
