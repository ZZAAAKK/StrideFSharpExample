# StrideFSharpExample
Still very much in progress...
<br/>This project should eventually serve as a working example of how to get a code-only Stride project, written in F#, off the ground.

## Resolved Issues
 * `fsproj` file will not compile by default, work around is to add the following to the `fsproj` file:
    ```xml
    <PackageReference
      Include="Stride.Core.Assets.CompilerApp"
      Version="4.1.0.1734"
      PrivateAssets="contentFiles;analyzers"
      IncludeAssets="..\**\*.*"/>
    ```
      Please note that this 'fix' may have caused other issues that are still being worked out.

 * Code now runs with an interim fix
    * Created a new C# project
    * Added CodeCapital.Stride.GameDefaults nuget package
    * Built the project
    * Copied files from the ...\bin\debug\net6.0\data\db\bundles to the same folder in the F# project
