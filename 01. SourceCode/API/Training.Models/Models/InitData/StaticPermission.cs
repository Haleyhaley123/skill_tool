using System.Collections.Generic;

namespace Training.Models.Models.InitData
{
    public class StaticPermission
    {
        public List<StaticGroupFunction> Groups { get; set; }

        public List<StaticFuncfion> Functions { get; set; }
    }

    public class StaticGroupFunction
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
    }

    public class StaticFuncfion
    {
        public string Id { get; set; }
        public string GroupId { get; set; }

        public string Name { get; set; }
        public string Code { get; set; }
        public string ScreenCode { get; set; }
        public int Index { get; set; }
    }
}
