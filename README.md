# 简介
此项目是一个2人合作项目，2020年一月中进行ProtoType，2月初开始正式制作，将在5月初完成。
以下内容如无特殊说明由本人（Zhengli）负责制作

# 基础机制
利用视角进行解密，关卡中有一些特殊开关，当其位于玩家视野中时会启动，离开视野后关闭
![游戏画面1](https://github.com/NaughtyFishRei/CTIN532_Project/raw/master/Screenshots/game1.PNG)

![游戏画面2](https://github.com/NaughtyFishRei/CTIN532_Project/raw/master/Screenshots/game2.PNG)

# 遥控相机
在某个关卡，玩家可以得到一个遥控相机，玩家可以在场景中设置相机，并通过手持终端观看相机画面，当特殊开关出现在相机画面上时，视作改开关位于玩家视野内
![遥控相机](https://github.com/NaughtyFishRei/CTIN532_Project/raw/master/Screenshots/cam1.PNG)

# 机关道具
## 门
基础的门，在连接的开关被触发后打开。模型来自Unity Asset Store。经过简单修改加上开门时的灯效。
## 电梯
电梯位于每一关开始和结束，进入电梯后下一关会在后台加载，加载完成后切换至下一关。模型来自Unity Asset Store。
![门和电梯](https://github.com/NaughtyFishRei/CTIN532_Project/raw/master/Screenshots/Door.PNG)
## 浮板
开关被触发后从"关闭位置"逐渐移动到"开启位置".模型来自Unity Asset Store。经过简单修改加上开启时的灯效。
![门和电梯](https://github.com/NaughtyFishRei/CTIN532_Project/raw/master/Screenshots/FloatingBoard.PNG)
