using System.Reflection;
using System;


//Для каждого метода вывести значения для следующих свойств: IsAbstract, IsFamily, IsFamilyAndAssembly,
//IsFamilyOrAssembly, IsAssembly, IsPrivate, IsPublic, IsConstructor, IsStatic, IsVirtual, ReturnType
//2.Для каждого конструктора вывести значения для следующих свойств: IsFamily, IsFamilyAndAssembly, IsFamilyOrAssembly, IsAssembly, IsPrivate, IsPublic.
//3.Для каждого поля вывести значения для следующих свойств: IsFamily, IsFamilyAndAssembly, IsFamilyOrAssembly, IsAssembly, IsPrivate, IsPublic, IsStatic.
//4.Для каждого свойства вывести значения для следующих свойств: Attributes, CanRead, CanWrite, GetMethod, SetMethod, PropertyType.
Type myType = typeof(OperatingSystem);
 
Console.WriteLine("Поля:");
foreach (FieldInfo field in myType.GetFields(
    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)) //чтобы получить и статические,
                                                                                                 //и не статические, и публичные,
                                                                                                 //и непубличные поля, в метод
                                                                                                 //GetFields() передается набор флагов
{
    string modificator = "";
 
    // получаем модификатор доступа
    if (field.IsPublic)
        modificator += "public ";
    else if (field.IsPrivate)
        modificator += "private ";
    else if (field.IsAssembly)
        modificator += "internal ";
    else if (field.IsFamily)
        modificator += "protected ";
    else if (field.IsFamilyAndAssembly)
        modificator += "private protected ";
    else if (field.IsFamilyOrAssembly)
        modificator += "protected internal ";
    else if (field.IsFamilyOrAssembly)
        modificator += "protected internal ";

 
    // если поле статическое
    if (field.IsStatic) modificator += "static ";
 
    Console.WriteLine($"{modificator}{field.FieldType.Name} {field.Name}");
}
Console.WriteLine("Свойства:");
foreach (PropertyInfo prop in myType.GetProperties(
    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static))
{
    Console.Write($"{prop.PropertyType} {prop.Name} {{");

    // если свойство доступно для чтения
    if (prop.CanRead) Console.Write("get;");
    // если свойство доступно для записи
    if (prop.CanWrite) Console.Write("set;");
    Console.WriteLine("}");
}



Console.WriteLine("Методы:");
foreach (MethodInfo method in myType.GetMethods())
{
    string modificator = "";

    // если метод статический
    if (method.IsStatic) modificator += "static ";
    // если метод виртуальный
    if (method.IsVirtual) modificator += "virtual ";

    Console.WriteLine($"{modificator}{method.ReturnType.Name} {method.Name} ()");
}


Console.WriteLine("Конструкторы:");
foreach (ConstructorInfo ctor in myType.GetConstructors(
    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
{
    string modificator = "";

    // получаем модификатор доступа
    if (ctor.IsPublic)
        modificator += "public";
    else if (ctor.IsPrivate)
        modificator += "private";
    else if (ctor.IsAssembly)
        modificator += "internal";
    else if (ctor.IsFamily)
        modificator += "protected";
    else if (ctor.IsFamilyAndAssembly)
        modificator += "private protected";
    else if (ctor.IsFamilyOrAssembly)
        modificator += "protected internal";

    Console.Write($"{modificator} {myType.Name}(");
    // получаем параметры конструктора
    ParameterInfo[] parameters = ctor.GetParameters();
    for (int i = 0; i < parameters.Length; i++)
    {
        var param = parameters[i];
        Console.Write($"{param.ParameterType.Name} {param.Name}");
        if (i < parameters.Length - 1) Console.Write(", ");
    }
    Console.WriteLine(")");
}
