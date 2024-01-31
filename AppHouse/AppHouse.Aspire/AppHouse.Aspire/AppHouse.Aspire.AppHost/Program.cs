var builder = DistributedApplication.CreateBuilder(args);
#pragma warning disable CS8604 // Possible null reference argument start.
#region Redis
var dbRedis = builder.AddRedisContainer("dbRedis");
#endregion

#region MongoDb
var dbMongo = builder.AddContainer("dbMongo");
var dbMongoConstring = builder.Configuration["db:mongo:constring"];
#endregion

#region Postgres

var dbPostgres = builder.AddPostgresContainer("dbPostgres", int.Parse(builder.Configuration["db:postgres:port"]), builder.Configuration["db:postgres:password"])
    .WithEnvironment("POSTGRES_USER", builder.Configuration["db:postgres:user"])
    .WithEnvironment("POSTGRES_PASSWORD", builder.Configuration["db:postgres:password"])
    .WithVolumeMount(builder.Configuration["db:postgres:volume"], builder.Configuration["db:postgres:volume_path"], VolumeMountType.Named);


var dbPostgresConString =  builder.AddPostgresConnection("dbPostgresConString", builder.Configuration["db:postgres:constring"]);
#endregion

#region Services

#endregion

#region Projects
builder.AddProject<Projects.AppHouseApi>("apphouseapi")
    .WithReference(dbPostgres)
    .WithReference(dbPostgresConString);
#endregion

#pragma warning restore CS8604 // Possible null reference argument end.
builder.Build().Run();
