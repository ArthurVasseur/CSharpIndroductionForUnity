# Introduction
In this workshop you will create your first space shooter in C#. You will use an engine that looks like Unity.

# Installation

First and foremost, you have to clone this repository :

```shell
git clone git@github.com:ArthurVasseur/CSharpIndroductionForUnity.git
```

## Dotnet

Dotnet-core is a free and open-source framework for Linux, Windows and MacOS.

### Ubuntu

https://docs.microsoft.com/fr-fr/dotnet/core/install/linux-ubuntu


### Fedora 
https://docs.microsoft.com/fr-fr/dotnet/core/install/linux-fedora

## SFML

### Ubuntu

```shell
sudo apt install libsfml-dev
```

### Fedora

```shell
sudo dnf install SFML
```


## Understaining OOP

In Oriented Object Programming (OOP) you will create classes to represent an object.
Each object contains data and methods to manipulate and access that data.

For example in C you will create a struct for representing data.

```c
struct Vector2D
{
    int x;
    int y;
}
```

You can access this data by calling :
```c
Vector2D vec = {10, 25};
int x = vec.x;
```

And to modify this data :
```c
Vector2D vec = {10, 25};
vec.x = 12;
```

If you want to manipulate this data you will create a function :

```c
Vector2D add(const Vector2D *a, const Vector2D *b)
{
    Vector2D sum;
    sum.x = a->x + b->x;
    sum.y = a->y + b->y;
    return sum;
}
```

In C# you can simply create a Class : 

```csharp
public class Vector2D
{
    private float _x;
    private float _y;

    public Vector2D(float x, float y)
    {
        _x = x;
        _y = y;
    }

    public int GetX()
    {
        return _x;
    }

    public int GetY()
    {
        return _x;
    }

    public void Add(Vector2D b)
    {
        _x += b.GetX();
        _y += b.GetY());
    }
}

public static void Main(string[] args)
{
    Vector2D position = new Vector2D(10, 15);
    Vector2D otherPosition = new Vector2D(24, 6);
    position.Add(otherPosition);

    Console.WriteLine("X : " + position.GetX() + "Y : " + position.GetY());
}
```

**private** is an access modifier meaning that you can only access this data/function inside a class.

**public Vector2D(float x, float y)** is a constructor, it's a special function that is automaticlly called when a object is created. 

**new** is a keyword for create a class instance (memory allocation), and call the constructor.

> Of course the written code can be greatly impoved by using [Properties](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/using-properties), [Auto-Implemented Properties](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/auto-implemented-properties), [Operator overloading](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/operator-overloading).

## Step 1 - Display your first sprite

The first step of this workshop is simple, you need to create a scene.
For this you need to create a class and [inherits from Scene class ](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/inheritance), then in the constructor you can create a [gameObject](https://docs.unity3d.com/Manual/class-GameObject.html) and add it a Transform and Sprite component.

To instantiate gameobject in the scene you have to create a gameobject, then add it to the scene : 

```csharp
Scene scene = new Scene();
GameObject background = new GameObject();
background.AddComponent(new Transform(new Vector2D(0,0),0, new Vector2D(1,1)));
background.AddComponent(new Sprite("../../../Assets/background.png"));
scene.AddGameObject(background);
```

Or if you are inside a custom class that inherits from `Component` class you can call `Instantiate` function.
> You can destroy gameObject by calling `Destroy` function.

> Take a look at the GameObject class.

> Don't forget to instantiate your class with **new** keyword, for example : **new Sprite("texture path")**


## Step 2 moving a gameobject with your keyboard

The behavior of GameObjects is controlled by the Components that are attached to them. You can create your own components by inherits from **Component** class. <br>
Component class implements two functions **OnConstruct()** and **Update(float deltaTime)**. <br>

**OnConstruct()** is called when a object is created in a scene. <br>
**Update(float deltaTime)** is called every frame, the deltatime is the time ealapsed between the last and the curent frame.

### Moving a gameObject

This script move an object every frame to the right: 
```csharp
public class Move : Component
{
    public override void OnConstruct()
    {

    }

    public override void Update(float deltaTime)
    {
        Transform.Position.X += deltaTime; // move the gameObject to the right
    }
}
```

> The Transform variable references the **Transform component** of the current gameObject.

### Interacting with keyboard

This script print in the console "Left action pressed" when "Q" is pressed

```csharp
public class Move : Component
{
    public override void OnConstruct()
    {
        Input.RegisterAction("Left", KeyCode.Q);
        Input.RegisterAction("Right", KeyCode.D);
        Input.RegisterAction("Up", KeyCode.Z);
        Input.RegisterAction("Down", KeyCode.S);
        Input.RegisterAction("Fire", KeyCode.Space);
    }

    public override void Update(float deltaTime)
    {
        if(Input.GetKeyDown("Left"))
            Console.WriteLine("Left action pressed");
    }
}
```

## Step 3 Detecting collisions

When a GameObject collides with another GameObject, Unity calls OnTriggerEnter.
You can initialize it like that :
```csharp
{
    GameObject gameObject = new GameObject();
    gameObject.AddComponent(new Transform(new Vector2D(100, 300), 0, new Vector2D(1, 1));
    gameObject.AddComponent(new Sprite("./index.png"));
    gameObject.AddComponent(new Collider(new Vector2D(10, 10)));
    gameObject.Tag = "objectA";
    scene.AddGameObject(gameObject);
}
{
    GameObject gameObject = new GameObject();
    gameObject.AddComponent(new Transform(new Vector2D(100, 300), 0, new Vector2D(1, 1));
    gameObject.AddComponent(new Sprite("./index.png"));
    gameObject.AddComponent(new Collider(new Vector2D(10, 10)));
    gameObject.Tag = "objectB";
    scene.AddGameObject(gameObject);
}
```

Now you can create a new script named CollisionDetector and overrides `OnCollisionEnter` function. Add it to one of the two gameObject : 

```csharp
public override void OnCollisionEnter(GameObject go)
{
    Console.WriteLine("Gameobject : " + GameObject.Tag + " collides with Gameobject " + go.Tag);
}
```

Now when your are executing your game, you should saw in the console `Gameobject :  X collides with Gameobject Y`

> By default the trigger of the collider is disable, enables it by setting `IsTrigger` to true


## Step 4 Creating a space shooter

For this last exercise we will condense all the skills seen in this workshop for create a space shooter, you have no constraints.
You can implements theses features :

- Enemy
- Player shoot
- Enemy shoot
- Enemy move pattern
- Create a UI with the `Text` and `Sprite` components

You can download free assets on [Itch.io](https://itch.io/)