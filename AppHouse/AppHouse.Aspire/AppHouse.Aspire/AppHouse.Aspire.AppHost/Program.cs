var builder = DistributedApplication.CreateBuilder(args);
#pragma warning disable CS8604 // Possible null reference argument start.
#region Redis
var dbRedis = builder.AddRedisContainer("dbRedis");
#endregion

#region MongoDb
var dbMongo = builder.AddMongoDBContainer("dbMongo")
    .WithVolumeMount(builder.Configuration["db:mongo:data:volume_source"], builder.Configuration["db:mongo:data:volume_target"], VolumeMountType.Named)
    .WithVolumeMount(builder.Configuration["db:mongo:config:volume_source"], builder.Configuration["db:mongo:config:volume_target"], VolumeMountType.Named);

#endregion

#region Postgres

var dbPostgres = builder
    .AddPostgresContainer("dbPostgres", int.Parse(builder.Configuration["db:postgres:port"]), builder.Configuration["db:postgres:password"])
    .WithEnvironment("POSTGRES_USER", builder.Configuration["db:postgres:username"])
    .WithEnvironment("POSTGRES_PASSWORD", builder.Configuration["db:postgres:password"])
    .WithVolumeMount(builder.Configuration["db:postgres:volume_source"], builder.Configuration["db:postgres:volume_target"], VolumeMountType.Named)
    .AddDatabase("db:postgres:database");

#endregion

#region Services

#endregion

#region Projects
builder.AddProject<Projects.AppHouse_BootsStrap>("apphouse.bootsstrap")
    .WithReference(dbPostgres)
    .WithReference(dbMongo)
    .WithReference(dbRedis);
#endregion


#pragma warning restore CS8604 // Possible null reference argument end.
builder.Build().Run();
