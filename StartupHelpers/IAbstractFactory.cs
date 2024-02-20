using Instagram.Models;
using System;

namespace Instagram.StartupHelpers
{
    public interface IAbstractFactory<T>
    {
        T Create();
    }
}