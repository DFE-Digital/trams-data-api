# trams-data-api
Simple api for getting data from the TRAMS system



## Development Setup
### Setting up local trams database image
We use Github Container Registry to host our docker images.
You can sign into ghcr.io by first [creating a Personal Access Token (PAT) for Github](https://docs.github.com/en/github/authenticating-to-github/keeping-your-account-and-data-secure/creating-a-personal-access-token) and then following the guide [here](https://docs.github.com/en/packages/working-with-a-github-packages-registry/working-with-the-container-registry#authenticating-to-the-container-registry).

Once signed into ghcr.io you can pull down and run the `trams-development-database` image using:

`docker run -d -p 1433:1433 ghcr.io/dfe-digital/trams-development-database:latest`

You can connect to the MSSQL Server on port `1433`.

### EntityFramework and Migrations

We currently have two database contexts defined: `LegacyTramsDbContext` and `TramsDbContext`. Both database contexts manage the same database, but are used to manage different sets of tables.

`LegacyTramsDbContext` is used to manage our models for tables which exist in the `sip` database and we have no control over - we treat these tables as read-only and don't commit migrations for them. If you do generate migrations for this context, it should not be commited to the repository.

`TramsDbContext` is the db context for models that we _do_ control, and we can generate migrations for. These migrations will be applied to the database in `dev`, `pre-prod`, and `prod`, and so should be commited to the repository when changes are made to models.

### Generating Migrations

To generate migrations for `TramsDbContext`, use the following command:

```
dotnet ef migrations add <MIGRATION_NAME> --project TramsDataApi --context TramsDataApi.DatabaseModels.TramsDbContext -o Migrations/TramsDb
```

And to generate migrations for `LegacyTramsDbContext` use:

```
dotnet ef migrations add <MIGRATION_NAME> --project TramsDataApi --context TramsDataApi.DatabaseModels.LegacyTramsDbContext -o Migrations/LegacyTramsDb
```

Migrations put into the `Migrations/LegacyTramsDb` directory will not be commited to git.

### Applying Migrations

To apply a set of migrations to the database, use `dotnet ef database update`:

```
dotnet ef database update --connection <CONNECTION_STRING> --project TramsDataApi --context TramsDataApi.DatabaseModels.<CONTEXT>
```

For example, to apply the migrations generated by `TramsDbContext` to the database Docker image:

```
dotnet ef database update --connection "Server=localhost,1433;Database=sip;User=sa;Password=StrongPassword905" --project TramsDataApi --context TramsDataApi.DatabaseModels.TramsDbContext
```

## API Key Management

Api Key management is done through the use of config files. There are currently placeholder entries in the various forms of `appsettings.json`, but new keys should *not* be added to these files, or committed to this repository.

For more information on how this decision was reached, please refer to [this ADR](https://github.com/DFE-Digital/sdd-technical-documentation/blob/main/adrs/adr_a002_how-do-we-secure-the-TRAMS-data-API.md).

Api Keys are provisioned at the environment level, and are stored as JSON objects in the following format:

```json
{
    "userName": "<the user name>",
    "apiKey": "<the unique api key>"
}
```

If injected through the environment, use `ApiKeys__x` naming conventions for the variables, as .NET will automatically configure this for us. e.g. `export ApiKeys__0=xxxx` will define the first API in the array. 


## Adding new census data csv file

The current census .csv file can be found a `TramsDataApi/CensusData/`

If you need to replace this file with a newer one then add the new file to the `TramsDataApi/CensusData/` folder and then update the `CensusDataGateway.cs` file to reflect the new filename.