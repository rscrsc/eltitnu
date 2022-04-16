## MkDocs 现已交由 GitHub.Actions 自动构建
每次向 main 分支的推送将会触发构建和部署，无需手动 `gh-deploy`.

## 非 Windows 平台开发者注意
[中断性变更：仅在 Windows 上支持 System.Drawing.Common](https://aka.ms/systemdrawingnonwindows)

相应的解决措施已经被写入 `runtimeconfig.template.json` 中。

它应该会工作，但并未在非 Windows 平台上测试过。
