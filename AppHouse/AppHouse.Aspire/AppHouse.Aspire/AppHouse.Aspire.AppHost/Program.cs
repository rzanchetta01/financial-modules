var builder = DistributedApplication.CreateBuilder(args);
#pragma warning disable CS8604 // Possible null reference argument start.
#region Redis
var dbRedis = builder.AddRedisContainer("dbRedis");
#endregion

#region MongoDb
var dbMongo = builder.AddMongoDBContainer("dbMongo")
    .WithEnvironment("MONGO_INITDB_ROOT_USERNAME", "db:mongo:username")
    .WithEnvironment("MONGO_INITDB_ROOT_PASSWORD", "db:mongo:password")
    .WithVolumeMount(builder.Configuration["db:mongo:volume"], builder.Configuration["db:mongo:volume_path"], VolumeMountType.Named, isReadOnly: false);

#endregion

#region Postgres

var dbPostgres = builder.AddPostgresContainer("dbPostgres", int.Parse(builder.Configuration["db:postgres:port"]), builder.Configuration["db:postgres:password"])
    .WithEnvironment("POSTGRES_USER", builder.Configuration["db:postgres:username"])
    .WithEnvironment("POSTGRES_PASSWORD", builder.Configuration["db:postgres:password"])
    .WithVolumeMount(builder.Configuration["db:postgres:volume"], builder.Configuration["db:postgres:volume_path"], VolumeMountType.Named, isReadOnly: false);

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
