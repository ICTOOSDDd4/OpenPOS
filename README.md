[![Project Status: WIP – Initial development is in progress, but there has not yet been a stable, usable release suitable for the public.](https://www.repostatus.org/badges/latest/wip.svg)](https://www.repostatus.org/#wip)  [![.NET Testing (windows)](https://github.com/ICTOOSDDd4/OpenPOS/actions/workflows/dotnet-windows-testing.yml/badge.svg?branch=master)](https://github.com/ICTOOSDDd4/OpenPOS/actions/workflows/dotnet-windows-testing.yml)
> **WARNING**: THIS REPOSITORY IS A WORK IN PROGRESS, THE CURRENT VERSION IS NOT READY FOR DEPLOYMENT.
# Welcome to OpenPOS
Welcome to OpenPOS, a new Open Source Point of Sale system. We want to offer entrepreneurs a worry free the Point of Sale system that lets them focus on what they supposed to do, <ins>Running their business!</ins>

## Repository Rules
- When creating a branch for a new feature, the keyword feature is required in the branch name.
		Example: feature/Giftcard_support
- When creating a branch for a bug fix, the keyword bug is required in the branch name.
		Example: bug-fix/not_showing_the_order_total
- Commit descripting are required
	The + symbol is used for indicating that something was added.
	The - symbol is used for indicating something was removed.
		Example:
		+ Added Credit Cart support
		- Removed unused dependancies.

## Setup guide
This will follow.

## Testing
Step 1: Retoring dependacies
	
		dotnet restore
Step 2: Building the application
	
		dotnet build --no-restore
Step 3: Running tests
	
		dotnet test --no-buld --verbosity normal

## Deployment guide
Will follow




