using System;

namespace PivotalPoker.Models
{
    public interface IConfig
    {
        T Get<T>(string key);
    }
}