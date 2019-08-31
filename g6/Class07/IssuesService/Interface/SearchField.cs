using System;

namespace IssuesService.Interface
{
    [Flags]
    public enum SearchField
    {
        None = 0,
        Assigned = 1,
        Reporter = 2,
        Tester = 4,
        Manager = 8,
        AccountManager = 16,
        SalesRep = 32,
        CEO = 64,
        All = 127
    }
}
 
// 11 => 0001011
// 127 =>1111111

