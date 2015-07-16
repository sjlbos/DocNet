
using System;
using DocNet.Core.Models.CSharp;
using DocNet.Core.Output.Html.Helpers;
using NUnit.Framework;

namespace DocNet.Core.Tests.Output.Helpers
{
    [TestFixture]
    public class GetDeclarationTests
    {
        #region Interface Tests
        
        [Test]
        public void TestOfInterfaceReturnsEmptyStringForInvalidInput()
        {
            Assert.That(String.Empty, Is.EqualTo(GetDeclaration.OfInterface(null)));
            Assert.That(String.Empty, Is.EqualTo(GetDeclaration.OfInterface(new InterfaceModel())));
            Assert.That(String.Empty, Is.EqualTo(GetDeclaration.OfInterface(new InterfaceModel{ Name = "   "})));
        }

        private static readonly object[] IntertfaceTestCases =
        {
            new object[]
            {
                "public interface IDummy",
                new InterfaceModel
                {
                    Name = "IDummy",
                    AccessModifier = AccessModifier.Public
                }
            },
            new object[]
            {
                "internal interface IDummy<T>",
                new InterfaceModel
                {
                    Name = "IDummy",
                    AccessModifier = AccessModifier.Internal,
                    TypeParameters = new [] { new TypeParameterModel { Name = "T" } }
                }
            },
            new object[]
            {
                "protected interface IDummy<T, V>",
                new InterfaceModel
                {
                    Name = "IDummy",
                    AccessModifier = AccessModifier.Protected,
                    TypeParameters = new []
                    {
                        new TypeParameterModel { Name = "T"}, 
                        new TypeParameterModel { Name = "V"} 
                    }
                }
            }
        };

        [Test, TestCaseSource("IntertfaceTestCases")]
        public void TestOfInterfaceReturnsCorrectDeclaration(string expectedOutput, InterfaceModel model)
        {
            Assert.That(expectedOutput, Is.EqualTo(GetDeclaration.OfInterface(model)));
        }

        #endregion

        #region Class Tests

        [Test]
        public void TestOfClassReturnsEmptyStringForInvalidInput()
        {
            Assert.That(String.Empty, Is.EqualTo(GetDeclaration.OfClass(null)));
            Assert.That(String.Empty, Is.EqualTo(GetDeclaration.OfClass(new ClassModel())));
            Assert.That(String.Empty, Is.EqualTo(GetDeclaration.OfClass(new ClassModel { Name = "   " })));
        }

        private static readonly object[] ClassTestCases =
        {
            new object[]
            {
                "public class Dummy",
                new ClassModel
                {
                    Name = "Dummy",
                    AccessModifier = AccessModifier.Public
                }
            },
            new object[]
            {
                "private abstract class Dummy",
                new ClassModel
                {
                    Name = "Dummy",
                    AccessModifier = AccessModifier.Private,
                    IsAbstract = true
                }
            },
            new object[]
            {
                "protected static class Dummy<T>",
                new ClassModel
                {
                    Name = "Dummy",
                    AccessModifier = AccessModifier.Protected,
                    IsStatic = true,
                    TypeParameters = new []{ new TypeParameterModel{ Name = "T" }}
                }
            },
            new object[]
            {
                "protected internal sealed class Dummy<T, V>",
                new ClassModel
                {
                    Name = "Dummy",
                    AccessModifier = AccessModifier.ProtectedInternal,
                    IsSealed = true,
                    TypeParameters = new []
                    {
                        new TypeParameterModel{ Name = "T" },
                        new TypeParameterModel{ Name = "V" }
                    }
                }
            }
        };

        [Test, TestCaseSource("ClassTestCases")]
        public void TestOfClassReturnsCorrectDeclaration(string expectedOutput, ClassModel model)
        {
            Assert.That(expectedOutput, Is.EqualTo(GetDeclaration.OfClass(model)));
        }

        #endregion

        #region Struct

        [Test]
        public void TestOfStructReturnsEmptyStringForInvalidInput()
        {
            Assert.That(String.Empty, Is.EqualTo(GetDeclaration.OfStruct(null)));
            Assert.That(String.Empty, Is.EqualTo(GetDeclaration.OfStruct(new StructModel())));
            Assert.That(String.Empty, Is.EqualTo(GetDeclaration.OfStruct(new StructModel { Name = "   " })));
        }

        private static readonly object[] StructTestCases =
        {
            new object[]
            {
                "public struct Dummy",
                new StructModel
                {
                    Name = "Dummy",
                    AccessModifier = AccessModifier.Public
                }
            },
            new object[]
            {
                "protected struct Dummy<T>",
                new StructModel
                {
                    Name = "Dummy",
                    AccessModifier = AccessModifier.Protected,
                    TypeParameters = new []{ new TypeParameterModel{Name = "T"} }
                }
            },
            new object[]
            {
                "internal struct Dummy<T, V>",
                new StructModel
                {
                    Name = "Dummy",
                    AccessModifier = AccessModifier.Internal,
                    TypeParameters = new []
                    {
                        new TypeParameterModel{Name = "T"},
                        new TypeParameterModel{Name = "V"}
                    }
                }
            }
        };

        [Test, TestCaseSource("StructTestCases")]
        public void TestOfStructReturnsCorrectDeclaration(string expectedOutput, StructModel model)
        {
            Assert.That(expectedOutput, Is.EqualTo(GetDeclaration.OfStruct(model)));
        }

        #endregion

        #region Enum

        [Test]
        public void TestOfEnumReturnsEmptyStringForInvalidInput()
        {
            Assert.That(String.Empty, Is.EqualTo(GetDeclaration.OfEnum(null)));
            Assert.That(String.Empty, Is.EqualTo(GetDeclaration.OfEnum(new EnumModel())));
            Assert.That(String.Empty, Is.EqualTo(GetDeclaration.OfEnum(new EnumModel { Name = "   " })));
        }

        private static readonly object[] EnumTestCases =
        {
            new object[]
            {
                "public enum Dummy",
                new EnumModel
                {
                    Name = "Dummy",
                    AccessModifier = AccessModifier.Public
                }
            }
        };

        [Test, TestCaseSource("EnumTestCases")]
        public void TestOfEnumReturnsCorrectDeclaration(string expectedOutput, EnumModel model)
        {
            Assert.That(expectedOutput, Is.EqualTo(GetDeclaration.OfEnum(model)));
        }

        #endregion

        #region Delegate

        [Test]
        public void TestOfDelegateReturnsEmptyStringForInvalidInput()
        {
            Assert.That(String.Empty, Is.EqualTo(GetDeclaration.OfDelegate(null)));
            Assert.That(String.Empty, Is.EqualTo(GetDeclaration.OfDelegate(new DelegateModel())));
            Assert.That(String.Empty, Is.EqualTo(GetDeclaration.OfDelegate(new DelegateModel{ Name = "   " })));
        }

        private static readonly object[] DelegateTestCases =
        {
            new object[]
            {
                "public delegate void Dummy()",
                new DelegateModel
                {
                    Name = "Dummy",
                    AccessModifier = AccessModifier.Public,
                    ReturnType = "void",
                }      
            },
            new object[]
            {
                "protected delegate Foo Dummy(Bar bar)",
                new DelegateModel
                {
                    Name = "Dummy",
                    AccessModifier = AccessModifier.Protected,
                    ReturnType = "Foo",
                    Parameters = new []{ new ParameterModel { Name = "bar", TypeName = "Bar" }}
                }
            },
            new object[]
            {
                "internal delegate T Dummy<T>(Foo foo, Bar bar)",
                new DelegateModel
                {
                    Name = "Dummy",
                    AccessModifier = AccessModifier.Internal,
                    ReturnType = "T",
                    Parameters = new []
                    {
                        new ParameterModel { Name = "foo", TypeName = "Foo" },
                        new ParameterModel { Name = "bar", TypeName = "Bar" } 
                    },
                    TypeParameters = new []{ new TypeParameterModel{ Name = "T" }}
                }
            },
            new object[]
            {
                "protected internal delegate T Dummy<T, V>(V value)",
                new DelegateModel
                {
                    Name = "Dummy",
                    AccessModifier = AccessModifier.ProtectedInternal,
                    ReturnType = "T",
                    Parameters = new [] { new ParameterModel { Name = "value", TypeName = "V"} },
                    TypeParameters = new []
                    {
                        new TypeParameterModel { Name = "T" },
                        new TypeParameterModel { Name = "V" } 
                    }
                }
            }
        };

        [Test, TestCaseSource("DelegateTestCases")]
        public void TestOfDelegateReturnsCorrectDeclaration(string expectedOutput, DelegateModel model)
        {
            Assert.That(expectedOutput, Is.EqualTo(GetDeclaration.OfDelegate(model)));
        }

        #endregion

        #region Constructor

        [Test]
        public void TestOfConstructorReturnsEmptyStringForInvalidInput()
        {
            Assert.That(String.Empty, Is.EqualTo(GetDeclaration.OfConstructor(null)));
            Assert.That(String.Empty, Is.EqualTo(GetDeclaration.OfConstructor(new ConstructorModel())));
            Assert.That(String.Empty, Is.EqualTo(GetDeclaration.OfConstructor(new ConstructorModel{ Name = "   " })));
        }

        private static readonly object[] ConstructorTestCases =
        {
            new object[]
            {
                "public Dummy()",
                new ConstructorModel
                {
                    Name = "Dummy",
                    AccessModifier = AccessModifier.Public
                }
            },
            new object[]
            {
                "protected Dummy(Foo foo)",
                new ConstructorModel
                {
                    Name = "Dummy",
                    AccessModifier = AccessModifier.Protected,
                    Parameters = new []{ new ParameterModel{Name = "foo", TypeName = "Foo"} }
                }
            },
            new object[]
            {
                "internal static Dummy(Foo foo, Bar bar)",
                new ConstructorModel
                {
                    Name = "Dummy",
                    AccessModifier = AccessModifier.Internal,
                    IsStatic = true,
                    Parameters = new []
                    {
                        new ParameterModel{ Name = "foo", TypeName = "Foo"},
                        new ParameterModel{ Name = "bar", TypeName = "Bar"}
                    }
                }
            }
        };

        [Test, TestCaseSource("ConstructorTestCases")]
        public void TestOfConstructorReturnsCorrectDeclaration(string expectedOutput, ConstructorModel model)
        {
            Assert.That(expectedOutput, Is.EqualTo(GetDeclaration.OfConstructor(model)));
        }

        #endregion

        #region Method

        [Test]
        public void TestOfMethodReturnsEmptyStringForInvalidInput()
        {
            Assert.That(String.Empty, Is.EqualTo(GetDeclaration.OfMethod(null)));
            Assert.That(String.Empty, Is.EqualTo(GetDeclaration.OfMethod(new MethodModel())));
            Assert.That(String.Empty, Is.EqualTo(GetDeclaration.OfMethod(new MethodModel { Name = "   " })));
        }

        private static readonly object[] MethodTestCass =
        {
            new object[]
            {
                "public void Dummy()",
                new MethodModel
                {
                    Name = "Dummy",
                    AccessModifier = AccessModifier.Public,
                    ReturnType = "void"
                }
            },
            new object[]
            {
                "private static Foo Dummy(Bar bar)",
                new MethodModel
                {
                    Name = "Dummy",
                    AccessModifier = AccessModifier.Private,
                    IsStatic = true,
                    ReturnType = "Foo",
                    Parameters = new []{ new ParameterModel{ Name = "bar", TypeName = "Bar" } }
                }
            },
            new object[]
            {
                "protected override abstract T Dummy<T>(Foo foo, Bar bar)",
                new MethodModel
                {
                    Name = "Dummy",
                    AccessModifier = AccessModifier.Protected,
                    IsOverride = true,
                    IsAbstract = true,
                    ReturnType = "T",
                    TypeParameters = new []{ new TypeParameterModel{ Name = "T" }},
                    Parameters = new []
                    {
                        new ParameterModel{Name = "foo", TypeName = "Foo"},
                        new ParameterModel{Name = "bar", TypeName = "Bar"}
                    }
                }
            },
            new object[]
            {
                "protected internal virtual async void Dummy<T, V>(T t, V v)",
                new MethodModel
                {
                    Name = "Dummy",
                    AccessModifier = AccessModifier.ProtectedInternal,
                    IsVirtual = true,
                    IsAsync = true,
                    ReturnType = "void",
                    TypeParameters = new []
                    {
                        new TypeParameterModel{Name = "T"},
                        new TypeParameterModel{Name = "V"}
                    },
                    Parameters = new []
                    {
                       new ParameterModel{ Name = "t", TypeName = "T" },
                       new ParameterModel{ Name = "v", TypeName = "V" }
                    }
                }
            },
            new object[]
            {
                "new internal sealed void Dummy()",
                new MethodModel
                {
                    Name = "Dummy",
                    AccessModifier = AccessModifier.Internal,
                    HidesBaseImplementation = true,
                    IsSealed = true,
                    ReturnType = "void"
                }
            }
        };

        [Test, TestCaseSource("MethodTestCass")]
        public void TestOfMethodReturnsCorrectDeclaration(string expectedOutput, MethodModel model)
        {
            Assert.That(expectedOutput, Is.EqualTo(GetDeclaration.OfMethod(model)));
        }

        #endregion

        #region Property

        [Test]
        public void TestOfPropertyReturnsEmptyStringForInvalidInput()
        {
            Assert.That(String.Empty, Is.EqualTo(GetDeclaration.OfProperty(null)));
            Assert.That(String.Empty, Is.EqualTo(GetDeclaration.OfProperty(new PropertyModel())));
            Assert.That(String.Empty, Is.EqualTo(GetDeclaration.OfProperty(new PropertyModel { Name = "   " })));
        }

        private static readonly object[] PropertyTestCases =
        {
            new object[]
            {
                "public bool Dummy { get; set; }",
                new PropertyModel
                {
                    Name = "Dummy",
                    TypeName = "bool",
                    AccessModifier = AccessModifier.Public,
                    HasGetter = true,
                    GetterAccessModifier = AccessModifier.Public,
                    HasSetter = true,
                    SetterAccessModifier = AccessModifier.Public
                }
            },
            new object[]
            {
                "protected override sealed string Dummy { set; }",
                new PropertyModel
                {
                    Name = "Dummy",
                    TypeName = "string",
                    AccessModifier = AccessModifier.Protected,
                    IsOverride = true,
                    IsSealed = true,
                    HasSetter = true,
                    SetterAccessModifier = AccessModifier.Protected
                }
            },
            new object[]
            {
                "new protected internal virtual int Dummy { get; }",
                new PropertyModel
                {
                    Name = "Dummy",
                    TypeName = "int",
                    AccessModifier = AccessModifier.ProtectedInternal,
                    HidesBaseImplementation = true,
                    IsVirtual = true,
                    HasGetter = true,
                    GetterAccessModifier = AccessModifier.ProtectedInternal
                }
            },
            new object[]
            {
                "public abstract bool Dummy { get; private set; }",
                new PropertyModel
                {
                    Name = "Dummy",
                    TypeName = "bool",
                    AccessModifier = AccessModifier.Public,
                    IsAbstract = true,
                    HasGetter = true,
                    GetterAccessModifier = AccessModifier.Public,
                    HasSetter = true,
                    SetterAccessModifier = AccessModifier.Private
                }
            }
        };

        [Test, TestCaseSource("PropertyTestCases")]
        public void TestOfPropertyReturnsCorrectDeclaration(string expectedOutput, PropertyModel model)
        {
            Assert.That(expectedOutput, Is.EqualTo(GetDeclaration.OfProperty(model)));
        }

        #endregion
    }
}
