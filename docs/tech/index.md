## MkDocs 现已交由 GitHub.Actions 自动构建
每次向 main 分支的推送将会触发构建和部署，无需手动 `gh-deploy`.

## 已经将 OpenTK 5.x 的最新代码 fork, 并作为子模块置于 `src/` 下
克隆仓库时请使用 `git clone git@github.com:rscrsc/eltitnu.git --recurse-submodules` 以确保此模块也被克隆。

在构建前，请先设置项目 `Eltitnu` 为启动项目。

构建时，将自动构建 `OpenTK 5.x` 模块，并将依赖复制在 `bin/{Release / Debug}/{.NET version}/` 内。

### 小问题
已知在 `Windows` 平台上， `glfw` 的动态链接库需要手动更名为 `glfw3.dll` （默认有 `-x86` 或 `-x64` 后缀）。
其它平台可能也存在此问题。不影响开发。

## <del>非 Windows 平台开发者注意</del>
<del>[中断性变更：仅在 Windows 上支持 System.Drawing.Common](https://aka.ms/systemdrawingnonwindows)</del>

<del>相应的解决措施已经被写入 `runtimeconfig.template.json` 中。</del>

<del>它应该会工作，但并未在非 Windows 平台上测试过。</del>