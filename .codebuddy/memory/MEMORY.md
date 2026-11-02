# GLTool 项目记忆（长期）

## 项目性质
- Inventor 2016–2026 的 COM 外接程序（AddIn），VB.NET WinForms，原 .NET Framework 4.8，**2026-10-17 起为 .NET 8.0（net8.0-windows，SDK 风格工程）**。
- 入口类：`StandardAddInServer`（`Implements Inventor.ApplicationAddInServer`），ProgID `InventorAddIn.StandardAddInServer`，CLSID `{4D6D2EAC-11B9-4BB1-9244-86AB8D5F8014}`。
- 插件目录：`C:\ProgramData\Autodesk\Inventor Addins\GLTool\`（含 `Autodesk.GLTool.Inventor.addin` 清单，LoadOnStartUp=1）。

## 构建/部署约定
- 编译命令（本机无 .NET 8 SDK，只有 .NET 10 SDK，8.0 目标包由 NuGet 还原）：
  `& 'C:\Program Files\Microsoft Visual Studio\18\Enterprise\MSBuild\Current\Bin\MSBuild.exe' GLTool.vbproj /p:Configuration=Debug /p:RegisterForComInterop=false /p:OutputPath=D:\Temp\xxx\ /v:q /nologo`
- 旧 .NET Framework 工程备份：`GLTool.vbproj.net48.bak`。
- COM 注册脚本：`RegisterAddin.ps1`（管理员；纯英文，避免 PS5.1 ANSI 解析中文失败）。
- 备份/还原插件目录时注意：`GLTool.comhost.dll` 时间戳不可信，判断新旧要看内容（含不含 `StandardAddInServer` 字样）。
- **部署目录必须同时有 5 个文件**：`GLTool.dll` / `GLTool.comhost.dll` / `GLTool.runtimeconfig.json` / `GLTool.deps.json` / `Autodesk.GLTool.Inventor.addin`。缺 comhost → Inventor 显示“已卸载”；缺 runtimeconfig → 加载失败。
- 用 `/p:OutputPath=<其它目录>` 编译后手工拷贝时，最容易漏掉 comhost；拷完必须重跑 `RegisterAddin.ps1 -AddInDir "C:\ProgramData\Autodesk\Inventor Addins\GLTool" -InventorVersion 30.0`（本机 Inventor 2026.3 的 RegistryVersion 仍是 30.0）。

## .NET 8 COM AddIn 的固定坑（务必记住）
1. 入口类必须显式 `<ComVisible(True)>` + `<ClassInterface(ClassInterfaceType.None)>`，否则 comhost 类清单为空 → 0x80040111 → Inventor 闪屏后退出且无事件日志。
2. comhost 时间戳会被 SDK 保留，MSBuild 增量复制会跳过 → 输出目录残留旧 comhost；已在 vbproj 用 `RefreshComHostInOutput` 目标强制复制。
3. 验证 COM 激活要用原生客户端（`cscript` + `CreateObject`），不要用 PowerShell 5.1 的 `New-Object -ComObject`（会误报 Typelib 0x80131165）。
4. **.NET Core 不再有 .NET Framework 的 COM 未处理异常兜底**：任何事件回调里逃逸的未处理异常都会直接终止进程（Inventor 退出码 1、无弹窗、无事件日志）。给所有 Inventor 事件/定时器回调包 Try/Catch 是迁移后的必要加固。
5. 判断是否插件导致 Inventor 闪退：把 `Autodesk.GLTool.Inventor.addin` 改名成 `.disabled` 再启动 Inventor，能起来就是插件问题。
6. `Activate` 里已内置诊断日志：`DbgLog()` 写 `%TEMP%\GLTool_debug.log`（ctor / Activate 各步骤 / CreatUI 起止 / 未处理异常），排障完成后可删。
7. **VS 无法调试 GLTool 的 .NET 8 COM 组件**：F5（调试启动）退出码 1（调试器启动期与 comhost 冲突）；「附加到进程」选托管 .NET Core 附加到 Inventor.exe 触发 **0xc0000005 访问违规**（调试器干扰 CLR 与原生宿主内存）。两者都是调试器与 comhost 的不兼容、**非插件 bug**（Ctrl+F5 不调试完全正常，Autodesk 官方也不支持 .NET 8 插件）。排障只能靠 Ctrl+F5 + `%TEMP%\GLTool_debug.log` 日志，不要依赖 VS 调试器。
8. **互操作版本与 `ApplicationAddInSite` 的 IID 在各 Inventor 大版本间是固定的**（踩坑结论，纠正旧笔记）：`ApplicationAddInServer`=E3571293、`ApplicationAddInSite`=E3571299，2021 与 2026 完全一致，接口都是 dual。**所以换互操作 DLL 并不能解决封送问题**——之前误判为互操作版本错，已验证换成 2026 互操作后 E_FAIL 依旧。
9. **`Activate` 参数封送失败的真正根因与修复（.NET 8 comhost 已知坑）**：Inventor 通过 IDispatch 调 `Activate` 时，CLR 的 `InvokeMember` 在参数封送阶段抛 `E_FAIL(0x80004005)`，方法体（含 `DbgLog("Activate begin...")`）根本进不去（日志特征：ctor 之后紧跟 `FIRSTCHANCE COMException E_FAIL` 且栈顶为 `System.RuntimeType.InvokeMember`，无 `Activate begin`/无 `ACTIVATE EXCEPTION`）。
   **关键结论：把 `Activate` 首参改成 `Object` 是死路**——仍走 `InvokeMember` 的 COM 对象参数封送路径，E_FAIL 依旧（已实测验证）。
   **真正修复：`EmbedInteropTypes=True` 是元凶**。.NET 8 的 CCW 对**嵌入式互操作类型**的参数封送有缺陷，导致 `InvokeMember` 无法为传入的 `ApplicationAddInSite` COM 对象创建 RCW → E_FAIL。改为 **`EmbedInteropTypes=False` + `Private=True`（即随 DLL 部署真实互操作程序集 `Autodesk.Inventor.Interop.dll`）** 后，`ApplicationAddInSite` 成为可正常封送的真实类型，问题消失。这是 Office/Inventor 外接程序迁移 .NET Core/8 的标准做法。
   **当前代码状态**：`StandardAddInServer.vb` 里保留了一个自定义 dual 接口 `IInventorAddInServer`（`Guid`=E3571293、`DispId` 50336257/58/59/60 与官方 `ApplicationAddInServer` 完全一致，用于显式 ComVisible）；`Activate(addInSiteObject As Inventor.ApplicationAddInSite, firstTime As Boolean)` 用真实互操作类型。诊断日志（ctor 的 UnhandledException/FirstChanceException 处理器、`DbgLog` 各步骤、增强的 `FIRSTCHANCE` 完整异常打印）确认修复后可清理。
10. **comhost 是通用宿主外壳，时间戳陈旧无害**：`GLTool.comhost.dll` 只内嵌 CLSID/ProgId，`EnableComHosting` 生成的它基本是静态模板；真正的 COM 接口暴露由运行时读取 `GLTool.dll` 元数据动态完成。所以 `CreateComHost` 任务走增量、comhost 时间戳长期停在首次生成的 10-04 是正常现象，**不必强求重生成**；改 COM 可见接口后只要 `GLTool.dll` 重新编过即生效（已用 `findstr IInventorAddInServer` 确认部署 DLL 含新接口）。
9. **构建前必须先关闭 Inventor**：Debug 的 `OutputPath` 就是插件目录 `C:\ProgramData\Autodesk\Inventor Addins\GLTool\`，Inventor 运行时会锁住 `GLTool.dll`，MSBuild 拷贝会重试 10 次后失败（MSB3027）→ 新 DLL 部署不进去。关闭 Inventor 再生成。
10. **诊断日志「只有 ctor、没有 Activate begin」的含义**：Inventor 成功 `CoCreateInstance`（建出对象），但 `Activate` 从未被调用/进入。先排除 QI 失败（用 `cscript` 调接口方法验证接口已暴露），再排查 `Activate` 参数跨 COM 封送失败（封送异常发生在方法体之前，方法内 Try 抓不到，需把 `UnhandledException`/`FirstChanceException` 处理器提前到 `ctor` 注册）。


## 代码风格
- VB 源码：`Imports System` 下**不能**写 `Windows.Forms.Xxx`，必须写全 `System.Windows.Forms.Xxx`。
- VB 里 `Namespace InventorAddIn` + RootNamespace `GLTool` → 全名 `GLTool.InventorAddIn.Xxx`。
- 布局控件集中在设计器（FormBomTool 等），运行时代码只做数据/事件，不要 New 控件。
- `ThisApplication` 是 `Module\PublicParameters.vb` 里的全局变量。
- 工程内 `library\`：Inventor/Excel/stdole 互操作副本。**Inventor 互操作引用现为 `EmbedInteropTypes=False` + `Private=True`**（随插件部署 `Autodesk.Inventor.Interop.dll` 真实程序集——.NET 8 下嵌入式互操作类型会导致 Activate 参数封送 E_FAIL）；Excel/stdole 仍 `EmbedInteropTypes=True` 嵌入。
- **添加新资源图标必须同时改 3 处**（否则 BC30456 "不是 My.Resources 的成员"）：① `Resources\xxx.ico` 文件；② `My Project\Resources.resx` 加 `<data>` 条目（ICO 用 `System.Drawing.Icon` 类型，代码 `.ToBitmap()` 才能用）；③ `My Project\Resources.Designer.vb` 加 `Friend ReadOnly Property Xxx() As System.Drawing.Icon`。本工程 My.Resources 是手写强类型类（vbproj:145 注释），SDK 不会自动生成属性；resx 里资源名重复也会导致后续属性生成失败。

## 当前功能状态
- BOM 按钮 → `clsBomTool.Run()`（代码自建界面，伪模态：自建窗体 Show 后 DoEvents 循环直到关闭；依赖全局 `ThisApplication`，内部用 `ThisApplication.ActiveEditDocument`）。`FormBomTool` 设计器窗体仍在工程但已不被按钮调用。
- `Class\clsBomTool.vb`（约 1.2 万行，代码自建界面）已从 vbproj 排除列表移除并纳入 .NET 8 编译（net48 迁移时曾排除，属历史遗留文件）。
- 待办/遗留：WebClient 过时警告 SYSLIB0014×4；插件目录残留 `__write_test.tmp`；Autodesk 官方不支持 .NET 8 插件，需在 Inventor 实测验证。
