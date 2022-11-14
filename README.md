[![Project Status: WIP – Initial development is in progress, but there has not yet been a stable, usable release suitable for the public.](https://www.repostatus.org/badges/latest/wip.svg)](https://www.repostatus.org/#wip)  [![.NET Testing](https://github.com/ICTOOSDDd4/OpenPOS/actions/workflows/dotnet-testing.yml/badge.svg)](https://github.com/ICTOOSDDd4/OpenPOS/actions/workflows/dotnet-testing.yml) [![CodeQL](https://github.com/ICTOOSDDd4/OpenPOS/actions/workflows/codeql.yml/badge.svg)](https://github.com/ICTOOSDDd4/OpenPOS/actions/workflows/codeql.yml) [![Open Source? Yes!](https://badgen.net/badge/Open%20Source%20%3F/Yes%21/blue?icon=github)](https://github.com/Naereen/badges/) [![GPLv3 license](https://img.shields.io/badge/License-GPLv3-blue.svg)](https://github.com/ICTOOSDDd4/OpenPOS/blob/master/LICENSE)



> **WARNING**: THIS REPOSITORY IS A WORK IN PROGRESS, THE CURRENT VERSION IS NOT READY FOR DEPLOYMENT.
# Welcome to OpenPOS
Welcome to OpenPOS, a new Open Source Point of Sale system. We want to offer entrepreneurs a modern and up to date Point of Sale system that lets them focus on what what is important!, <ins>OpenPOS will make you more efficient!</ins>

## Repository Rules
- When creating a branch for a new feature, feature/ is required in the branch name.
```
Example: feature/Giftcard_support
```
- When creating a branch for a bug fix, bug-fix/ is required in the branch name.
```
Example: bug-fix/not_showing_the_order_total
```
- When creating a branch for documentation, docs/ is required in the branch name.
```
Example: docs/improved-user-manual
```
- Commit description is required
	The + symbol is used for indicating that something was added.
	The - symbol is used for indicating something was removed.
```
Example:
+ Added Credit Cart support
- Removed unused dependancies.
```
- Every issue that is created should contain a label


## Setup guide
This will follow.

## Testing
Step 1: Installing .NET MAUI
	
```
dotnet nuget locals all --clear 
dotnet workload install maui
dotnet workload install android ios maccatalyst tvos macos maui wasm-tools
```

Step 2: Building the application
	
```
dotnet build --configuration Release
```

Step 3: Running tests
	
```
dotnet test --configuration Release --no-build --verbosity normal
```

## Deployment guide
Will follow

## Statements
This project is licensed under the [GNU General Public License v3.0](https://github.com/ICTOOSDDd4/OpenPOS/blob/master/LICENSE)

------------

*This project is part of the Open Business Project.*





