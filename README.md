# Anonymous🥸 methods and delegates
# Task#1:
Создать статический класс UiAnimations 
Добавить в него методы AnimateFadeOut и AnimateFadeIn, которые будут плавно изменять alpha у Image c течением времени (количество секунд тоже можно передать).
Сделать это в корутине.

Добавить в метод параметр типа Action с именем callback и вызывать его после завершения корутины.
Сделать вызов UiAnimations.AnimateFadeOut двумя способами:
- В качестве параметра callback передавать именованный метод
- В качестве параметра callback передавать анонимный метод


# Выполнение:
- [EntryPoint.cs](https://github.com/BashkaCoder/Unity_practice_2/blob/Task1/Assets/Scripts/EntryPoint.cs)
- [UiAnimations.cs](https://github.com/BashkaCoder/Unity_practice_2/blob/Task1/Assets/Scripts/UiAnimations.cs)