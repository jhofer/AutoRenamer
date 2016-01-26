using System;


namespace AutoRenamer
{
    public interface ILogger
    {
        void Error(Exception e);
        void Info(String str);
        void Error(string str);
    }
}