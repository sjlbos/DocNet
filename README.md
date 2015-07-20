DocNet
======
### Introduction
DocNet is a reusable class library and command-line tool for generating HTML documentation for C# source code. This is tool is capable of processing C# source (.cs) files, C# project (.csproj) files, and entire Visual Studio solutions (.sln files).

__Disclamer__: This project is was developed as a post-secondary project design project and is not under active development. It may see further development in the future, but in the meantime, __use at your own risk!__
  
### Generation Process
When generating documentation, DocNet does not compile the input code; all documentation comments and associated metadata are extracted directly from a parse tree of the source code. To get this parse tree, DocNet calls into the C# compiler (Roslyn) API and then walks the parse tree to convert the metadata it requires into an internal data model. Because a compiler is not used, the input code is not linked, preventing DocNet from fully resolving types. However, this limitation also provides the ability to document single files, code snippets, or other partial, non-compilable elements. 

Once the input source code has been processed and modelled, DocNet uses the Razor view engine to generate an HTML representation of the documented source. Razor is typically used in an ASP.NET MVC context to provide runtime HTML view generation from a template. However, in DocNet, Razor is used to create pre-compiled views generated at design-time. This is accomplished by the [RazorGenerator](https://github.com/RazorGenerator/RazorGenerator) plugin for Visual Studio, which converts Razor view templates into C# classes that can be executed programmatically.

###  Building the Project
Currently, the project does not have a stand-alone build script. To compile to project, open __~/DocNet.sln__ in Visual Studio and build the solution. The solution contains two projects:
- __DocNet.Core__: A class library project containing the core DocNet engine which compiles to a .dll.
- __DocNet.Console__: A lightweight console application project that wraps the DocNEt.Core library. Compiles to produce __DocNet.exe__.

_Note:_ Before building, ensure that NuGet package restore is enabled to allow DocNet's package dependencies to be installed.

### Running the Project
DocNet can be run by calling __DocNet.exe__. Use the __--help__ option to output a complete list of available commands. Basic execution uses the following argument format:

```DocNet -i [path\to\inputFile.cs, path\to\inputProject.csproj, path\to\inputSolution.sln] -o [path\to\outputDirectory]```

### Project Background
DocNet was built by Stephen Bos, Scott Byrne, Christopher Carr, and Keith Rollans, software engineering students at the University of Victoria. The project was developed over three months for SENG 499, the capstone design project of the BSeng degree.
