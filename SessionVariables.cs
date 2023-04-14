﻿using Microsoft.AspNetCore.Session;
namespace BloodNetwork
{
    public class SessionVariables
    {
        public const string SessionKeyUsername = "SessionKeyUsername";
        public const string SessionKeySessionId = "SessionKeySessionId";
    }
    public enum SessionKeyEnum
    {
        SessionKeyUsername =0,
        SessionKeySessionId =1
    }
}
