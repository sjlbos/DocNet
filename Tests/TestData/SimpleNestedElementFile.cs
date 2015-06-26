using System;

namespace N1
{
    public class ClassN1L1
    {
        public ClassN1L1() { }
        public void Method_N1L1() { }

        public class ClassC1L2
        {
            public ClassC1L2() { }
            public void Method_C1L2() { }
        }

        public struct StructC1L2
        {
            public StructC1L2() {}
            public void Method_StructC1L2() { }
        }
    }

    public struct StructN1L1
    {
        public StructN1L1() { }
        public void Method_StructN1L1 { }

        public class ClassS1L2
        {
            public ClassS1L2() {}
            public void Method_ClassS1L2() {}
        }

        public struct StructS1L2
        {
            public StructS1L2(){}
            public void Method_StructS1L2(){}
        }
    }

    namespace N2
    {
        public class ClassN2L1 { }
        public struct StructN2L1 { }
    }
}
