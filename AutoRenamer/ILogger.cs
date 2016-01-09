using System;
using System.Security.Cryptography.X509Certificates;

namespace AutoRenamer
{
    public interface ILogger
    {
        void Error(Exception e);
        void Info(String str);
        void Error(string str);
    }
}