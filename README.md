# 外汇行情任务栏工具

一个轻量级的Windows 11任务栏工具，用于实时显示外汇行情（XAUUSD、EURUSD、GBPUSD等）。

## 功能特性

- ✅ 显示实时外汇价格和涨跌幅
- ✅ 支持多种货币对切换（XAUUSD、EURUSD、GBPUSD）
- ✅ 自动每30秒更新数据
- ✅ 系统托盘图标显示
- ✅ 右键菜单快速切换品种

## 使用方法

### 下载预编译版本

1. 前往 [Releases](../../releases) 页面
2. 下载最新版本的 `ForexTaskbarTool.exe`
3. 双击运行即可

### 从源码编译

如果你想自己编译：

```bash
git clone https://github.com/你的用户名/ForexTaskbarTool.git
cd ForexTaskbarTool
dotnet publish ForexTaskbarTool/ForexTaskbarTool/ForexTaskbarTool.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

编译后的文件在 `ForexTaskbarTool/ForexTaskbarTool/bin/Release/net6.0-windows/win-x64/publish/` 目录下。

## GitHub Actions 自动编译

本项目配置了 GitHub Actions，会在以下情况自动编译：

- 推送代码到 main/master 分支
- 创建新的 tag（如 v1.0.0）
- 手动触发工作流

### 创建 Release 版本

```bash
git tag v1.0.0
git push origin v1.0.0
```

GitHub Actions 会自动编译并创建 Release，附带编译好的 exe 文件。

## 操作说明

1. **启动程序**：双击 exe 文件，程序会最小化到系统托盘
2. **查看行情**：鼠标悬停在托盘图标上查看当前价格
3. **切换品种**：右键点击托盘图标，选择不同的货币对
4. **刷新数据**：右键菜单选择"刷新"
5. **退出程序**：右键菜单选择"退出"

## 技术栈

- .NET 6.0
- WPF (Windows Presentation Foundation)
- Hardcodet.NotifyIcon.Wpf (系统托盘支持)
- HttpClient (数据获取)

## 数据来源

数据来自 MyFXBook Widget API：
- https://widget.myfxbook.com/widget/market-quotes.html?symbols=XAUUSD

## 注意事项

- 需要 Windows 10/11 系统
- 需要网络连接获取实时数据
- 数据更新频率为30秒

## 许可证

MIT License

## 贡献

欢迎提交 Issue 和 Pull Request！
