# Kimicu Achievements / Quest

## How to add in project
PackageManager > + > Add package from git URL..
```
https://github.com/Kitgun1/KimicuUnityAchievementSystem.git
```

------------

> [!NOTE]  
> Documentation [RU](Docs%7E/Documentation-ru.md) or [EN](Docs%7E/Documentation-en.md)

## Example
### 1. Инициализация в проекте
> [!NOTE]  
> [#Инициализация](Docs%7E/Documentation-en.md#Инициализация)
```csharp
AchievementRoot root = await AchievementRoot.Initialize(parent);
```

Если вы хотите добавить больше своих полей в элемент достижения, то: <br>
Создать новый класс с именем 'AchievementItem' или 'AchievementProgressItem<T>' или пользовательский элемент 
в пространстве имен 'Kimicu.Achievements' и не забудьте добавить 'partial'
```csharp
// Example: Добавление `Sprite` в сухие данные

namespace Kimicu.Achievements; // ВАЖНО!!!

public partial interface IAchievementItem
{
    public Sprite Icon { get; }
}

public partial class AchievementItem
{
    public Sprite Icon { get; }

    public AchievementItem(string id, string title, string description, Sprite icon)
    {
        Id = id;
        Title = title;
        Description = description;
        Icon = icon;
    }
}
```

### 2. Create achievement item
> [!NOTE]  
> [#Данные](Docs%7E/Documentation-en.md#Данные)
There are 2 types at the moment: (`AchievementItem` and `AchievementProgressItem<T>`)
```csharp
var achievementItem = new AchievementItem("id_1");

// or

var target = 100; // может быть 'string', 'list', 'class', etc.
var achievementItem = new ProgressAchievementItem<int>("id_1", "name", "description", target);
```


### 3. Создать достижение
> [!NOTE]  
> [#Данные](Docs%7E/Documentation-en.md#Данные)
Есть также 2 варианта создания: (`Achievement<T>` and `ProgressAchievement<T>`)
```csharp
var achievement = new Achievement<Unit>(achievementItem);

// or

int startProgress = 0; // Тип 'StartProgress' должен соответствовать типу 'Achievementitem<T>'
var achievement = new ProgressAchievement<int>(achievementItem, startProgress, isComplete);
// Если мы создаем с прогрессом, то нам нужно указать функцию вывода прогресса в string тип
// Пример для типа 'int':
achievement.ProgressToString = () => $"{achievement.Progress} / {achievement.ProgressItem.TargetProgress}";
```

### 4. To display a lot of ecups of achievement required:
```csharp
var prefab = Resources.Load<AchievementView>("Achievement View");
var achievements = new Dictionary<AchievementView, Achievement<Unit>[]> {
    {
        prefab, new[] {
            achievement1, achievement2, achievement3, etc.
        }
    }
};
// We will analyze in more detail:

Achievement<Unit>[]; // (Unit) - This is a plug for usually achievement 
    
// use default path or custom prefabs. 
prefab; // This is a proofab that will be used for this group of achievement. 
// default paths: "Achievement View" & "Progress Achievement View"

// For display in 'UI':
root.View.Setun(achievements);
```

### 6. To Update achievement progress or other data
```csharp
// change progress: (if achievement type is ProgressAchievement<T>)
achievement.Progress = 10;

var targetProgress = achievement.ProgressItem.TargetProgress; // ReadOnly
var id = achievement.ProgressItem.Id; // ReadOnly
var title = achievement.ProgressItem.Title; // ReadOnly
var description = achievement.ProgressItem.Description; // ReadOnly
achievement.OnStep += () => ...; // Progress Changed
achievement.OnCompleteEvent += () => ...; // Complete
achievement.Complete(); // Mark complete
achievement.IsComplete; // Is complete
achievement.Dispose(); // Clear object
```

### 7. How to add reward and display reward in UI
#### 7.1. Create new template for achievement ui
![Unity_wWtBMgW934.png](img%7E/Unity_wWtBMgW934.png)
![Unity_nwmgTE4NUs.png](img%7E/Unity_nwmgTE4NUs.png)
#### 7.2. Create script for template
```csharp
// Reward.cs
public readonly struct Reward
{
	public readonly Sprite Icon;
	public readonly int Count;

	public Reward(Sprite icon, int count) {
		Icon = icon;
		Count = count;
	}
}
```
```csharp
// RewardItemView.cs
public class RewardItemView : MonoBehaviour
{
	[SerializeField] private Image _iconTMP;
	[SerializeField] private TMP_Text _countTMP;
	
	public void Setup(Reward reward)
	{
		_iconTMP.sprite = reward.Icon;
		_countTMP.text = reward.Count.ToString();
	}
}
```
```csharp
// IRewardAchievementProgress.cs
public interface IRewardAchievementProgress<out T> : IAchievementProgress<T>
{
    public Reward[] Rewards { get; }
}
```
```csharp
// RewardProgressAchievementItem.cs
[Serializable]
public class RewardProgressAchievementItem<T> : IRewardAchievementProgress<T>
{
    public string Id { get; }
    public Dictionary<string, AchievementLocalizeInfo> LocalizeViewData { get; }
    public T TargetProgress { get; }
    public Reward[] Rewards { get; }

    public RewardProgressAchievementItem(string id, Dictionary<string, AchievementLocalizeInfo> localizeViewData, T targetProgress, Reward[] rewards)
    {
        Id = id;
        LocalizeViewData = localizeViewData;
        TargetProgress = targetProgress;
        Rewards = rewards;
    }
}
```
```csharp
// RewardProgressAchievement.cs
public class RewardProgressAchievement<T> : ProgressAchievement<T>
{
    public readonly IRewardAchievementProgress<T> RewardItem;

    public RewardProgressAchievement(IRewardAchievementProgress<T> rewardItem, T startProgress = default, bool isComplete = false) : base(rewardItem, startProgress, isComplete)
    {
        RewardItem = rewardItem;
    }
}
```
```csharp
// RewardProgressAchievementView.cs
public class RewardProgressAchievementView : ProgressAchievementView
{
    [SerializeField] protected Transform RewardsContainer;

    public override void Setup<T>(Achievement<T> achievement, string localizeKey)
    {
        base.Setup(achievement, localizeKey);

        var rewardProgressAchievement = (RewardProgressAchievement<T>)achievement;
        
        SetupRewards(Resources.Load<RewardItemView>("Reward Item Container"), rewardProgressAchievement.RewardItem.Rewards);
    }

    public virtual void SetupRewards(RewardItemView prefabTemplate, params Reward[] rewards)
    {
        // Clear garbage objects
        var childCount = RewardsContainer.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Destroy(RewardsContainer.GetChild(i).gameObject);
        }

        // Initialize reward views
        foreach (var reward in rewards)
        {
            var rewardView = Instantiate(prefabTemplate, RewardsContainer);
            rewardView.Setup(reward);
        }
    }
}
```
Also, do not forget to add `RewardProgressAchievementView.cs` and` RewardItemView.cs` on the prefabs.
