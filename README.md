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

## Current issues
 * Code compiles (as above), however shaders appear to not compile
