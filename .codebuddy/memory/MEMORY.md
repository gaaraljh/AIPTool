# AIPTool 项目记忆（长期）

## 项目性质
- Inventor 2016–2026 的 COM 外接程序（AddIn），VB.NET WinForms，原 .NET Framework 4.8，**2026-10-17 起为 .NET 8.0（net8.0-windows，SDK 风格工程）**。
- 入口类：`StandardAddInServer`（`Implements Inventor.ApplicationAddInServer`），ProgID `InventorAddIn.StandardAddInServer`，CLSID `{4D6D2EAC-11B9-4BB1-9244-86AB8D5F8014}`。
- 插件目录：`C:\ProgramData\Autodesk\Inventor Addins\AIPTool\`（含 `Autodesk.AIPTool.Inventor.addin` 清单，LoadOnStartUp=1）。

## 构建/部署约定
- 编译命令（本机无 .NET 8 SDK，只有 .NET 10 SDK，8.0 目标包由 NuGet 还原）：
  `& 'C:\Program Files\Microsoft Visual Studio\18\Enterprise\MSBuild\Current\Bin\MSBuild.exe' AIPTool.vbproj /p:Configuration=Debug /p:RegisterForComInterop=false /p:OutputPath=D:\Temp\xxx\ /v:q /nologo`
- 旧 .NET Framework 工程备份：`AIPTool.vbproj.net48.bak`。
- COM 注册脚本：`RegisterAddin.ps1`（管理员；纯英文，避免 PS5.1 ANSI 解析中文失败）。
- 备份/还原插件目录时注意：`AIPTool.comhost.dll` 时间戳不可信，判断新旧要看内容（含不含 `StandardAddInServer` 字样）。

## .NET 8 COM AddIn 的固定坑（务必记住）
1. 入口类必须显式 `<ComVisible(True)>` + `<ClassInterface(ClassInterfaceType.None)>`，否则 comhost 类清单为空 → 0x80040111 → Inventor 闪屏后退出且无事件日志。
2. comhost 时间戳会被 SDK 保留，MSBuild 增量复制会跳过 → 输出目录残留旧 comhost；已在 vbproj 用 `RefreshComHostInOutput` 目标强制复制。
3. 验证 COM 激活要用原生客户端（`cscript` + `CreateObject`），不要用 PowerShell 5.1 的 `New-Object -ComObject`（会误报 Typelib 0x80131165）。
4. **.NET Core 不再有 .NET Framework 的 COM 未处理异常兜底**：任何事件回调里逃逸的未处理异常都会直接终止进程（Inventor 退出码 1、无弹窗、无事件日志）。给所有 Inventor 事件/定时器回调包 Try/Catch 是迁移后的必要加固。
5. 判断是否插件导致 Inventor 闪退：把 `Autodesk.AIPTool.Inventor.addin` 改名成 `.disabled` 再启动 Inventor，能起来就是插件问题。
6. `Activate` 里已内置诊断日志：`DbgLog()` 写 `%TEMP%\AIPTool_debug.log`（ctor / Activate 各步骤 / CreatUI 起止 / 未处理异常），排障完成后可删。
7. **VS 无法调试 AIPTool 的 .NET 8 COM 组件**：F5（调试启动）退出码 1（调试器启动期与 comhost 冲突）；「附加到进程」选托管 .NET Core 附加到 Inventor.exe 触发 **0xc0000005 访问违规**（调试器干扰 CLR 与原生宿主内存）。两者都是调试器与 comhost 的不兼容、**非插件 bug**（Ctrl+F5 不调试完全正常，Autodesk 官方也不支持 .NET 8 插件）。排障只能靠 Ctrl+F5 + `%TEMP%\AIPTool_debug.log` 日志，不要依赖 VS 调试器。


## 代码风格
- VB 源码：`Imports System` 下**不能**写 `Windows.Forms.Xxx`，必须写全 `System.Windows.Forms.Xxx`。
- VB 里 `Namespace InventorAddIn` + RootNamespace `AIPTool` → 全名 `AIPTool.InventorAddIn.Xxx`。
- 布局控件集中在设计器（FormBomTool 等），运行时代码只做数据/事件，不要 New 控件。
- `ThisApplication` 是 `Module\PublicParameters.vb` 里的全局变量。
- 工程内 `library\`：Inventor/Excel/stdole 互操作副本，互操作引用一律 `EmbedInteropTypes=True` + `Private=False`。

## 当前功能状态
- BOM 按钮 → `clsBomTool.Run()`（代码自建界面，伪模态：自建窗体 Show 后 DoEvents 循环直到关闭；依赖全局 `ThisApplication`，内部用 `ThisApplication.ActiveEditDocument`）。`FormBomTool` 设计器窗体仍在工程但已不被按钮调用。
- `Class\clsBomTool.vb`（约 1.2 万行，代码自建界面）已从 vbproj 排除列表移除并纳入 .NET 8 编译（net48 迁移时曾排除，属历史遗留文件）。
- 待办/遗留：WebClient 过时警告 SYSLIB0014×4；插件目录残留 `__write_test.tmp`；Autodesk 官方不支持 .NET 8 插件，需在 Inventor 实测验证。
