## Publishing To Nuget

Follow these steps to create a new release for nuget.

[Bhtp.Url on nuget.org](https://www.nuget.org/packages/Bhtp-Url-DotNet)

1. After completing changes, increment the `AssemblyVersion` and `AssemblyFileVersion` in `AssemblyInfo.cs`

2. Verify that all information in Bhtp.Url.nuspec is up to date. Update release notes.

3. In the command line, run the following command in the project directory for Bhtp.Url.
   This will create a `nupkg` file. Verify that the version number matches what is in `AssemblyInfo.cs`
    ```
    nuget pack Bhtp.Url.csproj -Prop Configuration=Release
    ```

4. Login to the Bhtp account on nuget.org, and upload the generated `nupkg` file.
   It will take about 5 - 10 minutes for nuget.org to index it for search.

5. Update the Bhtp.Url.Demo project to use the new version of the nuget package.