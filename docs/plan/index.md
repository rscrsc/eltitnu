# 初期架构设计

## 主类：Game

“游戏”类是汇总各个模块的地方，负责储存游戏相关变量，处理初始化和游戏循环

看起来应该像这样：

``` mermaid
classDiagram
    class Game {
        +gameStatus
        +width
        +height
        +Game(width, height)
        +init() //加载着色器、纹理、世界
        +processInput() //处理输入
        +update() //根据输入更新 GameObject
        +render() //根据 GameObject 渲染画面
    }
```
其中：
- `init()` 用于加载着色器、纹理、世界
- `processInput()` 用于处理用户输入
- `update()` 用于根据输入更新 World
- `render()` 用于根据 World 渲染画面

## OpenGL 工具类

将常用的 OpenGL 代码段进行封装，方便复用。

主要包含：
- 着色器类 `class Shader`
- 2D纹理类 `class Texture2D`
- ...

## 资源管理器类：ResourceManager

由于需要加载外部文件（着色器源代码、纹理图片、存档文件等），将该部分常用功能封装，方便复用。OpenGL 工具类用到的资源也由这里加载。

初步确定使用 Singleton 设计模式，即只允许创建单一实例，将资源加载到该实例内的属性中，整个项目共享。

对每种资源，需要编写：
- `load`方法
- `use`方法
- `delete`方法 ***（？待定）***

## 游戏对象类：GameObject

游戏中所有的对象都属于或派生于该类。

<u>***TODO: 根据游戏内容设计该类及其子类***</u>

## 世界类：World

包含所有的游戏对象`gameObjectList`，由`update()`更新，并为`render()`提供信息。依赖于资源管理器类进行存档的读入和写出。