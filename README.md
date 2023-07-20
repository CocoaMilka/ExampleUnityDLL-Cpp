# Creating a C++ plugin for use in Unity
Sometimes it's useful to be able to access C++ scripts in Unity, this will serve as a simple guide on how to do so.

### Overview
1. Create a DLL
2. Import DLL into Unity
3. Access C++ functions within Unity
4. Profit

## Creating a DLL

To create a DLL I'd recommend using an IDE like Visual Studio, Microsoft has a guide on how to specifically [Create a DLL in Visual Studio](https://learn.microsoft.com/en-us/cpp/build/walkthrough-creating-and-using-a-dynamic-link-library-cpp?view=msvc-170)

## Creating Project in Visual Studio

1. `File > New > Project`
2. Set language to `C++`, set Platform to `Windows`, set Project type to `Library`
3. Select `Dynamic-Link Library (DLL)` from the list of options
4. Enter project name and uncheck `Place solution and project in the same directory`
5. Create project

## Creating your Header and Source Files

1. Start off by creating a header file `ExampleUnityDll.h` and adding the following code:

**Note:** You are not limited to functions, structs and classes can be exported as well!
```c++  
#pragma once

#ifdef EXAMPLEUNITYDLL_EXPORTS
#define EXAMPLEUNITYDLL_API __declspec(dllexport)
#else
#define EXAMPLEUNITYDLL_API __declspec(dllimport)
#endif

extern "C" EXAMPLEUNITYDLL_API void yourFunction_1();
// .
// .
// .
extern "C" EXAMPLEUNITYDLL_API void yourFunction_N();
```

For multiline exporting (macros aren't used in this example):

```c++
#pragma once

#ifdef EXAMPLEUNITYDLL_EXPORTS
#define EXAMPLEUNITYDLL_API __declspec(dllexport)
#else
#define EXAMPLEUNITYDLL_API __declspec(dllimport)
#endif

extern "C"
{
  __declspec(dllexport) void yourFunction_1();
  // .
  // .
  // .
  __declspec(dllexport) void yourFunction_N();
}
```

This file will contain all your function declarations and make sure they are properly exported.

**Note:** Replace all occurances of `ExampleUnityDll` or `EXAMPLEUNITYDLL` with the name of your DLL.

2. Create a source file `ExampleUnityDll.cpp` this will contain your function definitions.
```c++
#include "pch.h" // Automatically created for this project
#include "ExampleUnityDll.h" // Your header file created in the previous step

void yourFunction_1()
{
  // Code definition
}
// .
// .
// .
void yourFunction_N()
{
  // Code definition
}
```
3. Once finished, `Build > Build Solution`. Assuming you have done everything correctly, your code will compile and a `.dll` file will be created in your project directory.

## Accessing your C++ functions in Unity
1. Create a Unity project if you don't have one already...
2. Create a `Plugins` folder in the `Assets` folder in your Unity project.
3. Place your newly compiled `.dll` file in the `Plugins` folder.
4. Create a script or modify an existing one that you want to access your functions in.
5. Add the following lines of code:
```c#
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices; // VERY IMPORTANT !!!
using UnityEngine;

public class yourScript : MonoBehaviour
{
  [DllImport("ExampleUnityDll")] // This wil retrieve your functions from the .dll, make sure the name matches!
  public static extern void yourFunction_1(); // Function from your .dll that you want to import
  // .
  // .
  // .
  public static extern void yourFunction_N(); // List all the functions that you wish to use in this script

  void Start()
  {
    yourFunction_1(); // Call your function
  }
}
```

