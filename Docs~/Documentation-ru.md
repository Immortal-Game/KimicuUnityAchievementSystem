# Инициализация
> > [!NOTE]  
> Инициализация главного объекта для ачивок (можно создавать несколько под разные задачи)
```csharp
AchievementRoot root = await AchievementRoot.Initialize(parent, prefabView);
```
* parent: родительский объект на сцене (Canvas). Рекомендуеться сделать его DontDestroyOnLoad() 
* prefabView: Путь до кастомного окна (не обязательно)

--------

# Данные
> [!NOTE]  
> Сухие данных для сохранения или чтения
```csharp
// Базовый класс для даных
var item = new AchievementItem(id, title, description);
```
* id: уникальный ID
* title: Название, которое будет отображено на View
* description: Описание, которое будет отображено на View

```csharp
// Класс для даных с прогрессом
var progressItem = new ProgressAchievementItem<T>(id, title, description, target);
```
* id: уникальный ID
* title: Название, которое будет отображено на View
* description: Описание, которое будет отображено на View
* T: Тип прогресса. Может быть как обычном `int`, `float` так и `List<T2>`, `object` и тд 
* target: конечный результат после которого ачивка будет выполнена

> [!TIP]  
> Вы также можете добавлять свои сухие данные путем наследования 
> от `AchievementItem` или любых дочерних ему классов [**(Пример)**]()

--------

# Модель для работы ачивок
> [!NOTE]  
> Model для обработки данных
```csharp
var achievement = new Achievement<T>(item, isComplete);

achievement.MarkComplete(); // Пометить выполненой
achievement.OnComplete; // Событие при выполнении
achievement.IsComplete; // Состояние завершения
```
* item: Экземпляр сухих данных ачивки **(`Achievement` корректно будет работать только с `AchievementItem`)**
* isComplete: Выполнена ли ачивка в текущий момент **(Можно подгрузить из сохранений)**
* T: Для `Achievement` всезда указывает `Unit` или любой другой, это никак не влияет

```csharp
var progressAchievement = new ProgressAchievement<T>(item, startProgress, isComplete);

progressAchievement.ProgressToString += () => $"{progressAchievement.Progress} / {progressAchievement.ProgressItem.TargetProgress}";

progressAchievement.OnStep<ProgressAchievement<T> this, T oldProgress, T newProgress> // Событие при изменении прогресса
progressAchievement.Progress<T> // Текущий прогресс
```
* item: Экземпляр сухих данных ачивки (`Achievement` корректно будет работать только с `AchievementItem`)
* startProgress: Текущее значение прогресса **(Можно подгрузить из сохранений)**
* isComplete: Выполнена ли ачивка в текущий момент **(Можно подгрузить из сохранений)**
* T: Обязательно указать тот же тип, как и у сухих данных в `<T>`
* ProgressToString: Func<string> функция реализующая вывод програсса в строку **(Обязательно для заполнения)**

--------

# Группа ачивок
> [!NOTE]  
> Настройка группы ачивок для последующего отображения
```csharp
var intAchievements = new Dictionary<AchievementView, Achievement<int>[]> {
    {
        progressPrefab, new Achievement<int>[] {
            achievement1, ...
        },
        progressPrefab2, new Achievement<int>[] {
            achievement10, ...
        },
    }
};
var otherAchievements = new Dictionary<AchievementView, Achievement<...>[]> { ... };
```
* new Dictionary<AchievementView, Achievement<T>[]>: Словарь хранящий список ачивок одного типа
* prefab: Префаб для группы ачивок
* achievement: Экземпляр ачивки
> [!TIP]
> Префабы по умолчанию можно подгрузить из ресурсов: ["Achievement View", "Progress Achievement View"]

--------

# Инициализация View
> [!NOTE]  
> Задаем параметры для визуальной части
```csharp
public void SetupGroup<T>(
    Action<IAchievementItem> onComplete, 
    Dictionary<AchievementView,Achievement<T>[]> achievementsGroup);

root.RootView.SetupGroup(root.NotificationRootView.Notify, intAchievements);
root.RootView.SetupGroup(root.NotificationRootView.Notify, otherAchievements);

root.RootView.Show() // Отобразить список ачивок
root.RootView.Hide() // Спрятать список ачивок
root.RootView.Redraw() // Удалить лишний мусор и UpdateValues
root.RootView.UpdateValues() // Обновление сухих данных
```
* onComplete: Событие при завершении экземпляра объекта
* achievementsGroup: Группа ачивок одного типа, например `int`










