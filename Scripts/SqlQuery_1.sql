﻿CREATE TABLE [dbo].[Users] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](max),
    [LastName] [nvarchar](max),
    [Email] [nvarchar](max),
    [StatusUser] [int] NOT NULL,
    [IdCompany] [int] NOT NULL,
    [Username] [nvarchar](max),
    [Password] [nvarchar](max),
    [Salt] [nvarchar](max),
    [PasswordResetToken] [nvarchar](max),
    [PasswordResetTokenExpiration] [datetime] NOT NULL,
    [IsLocal] [bit] NOT NULL,
    CONSTRAINT [PK_dbo.Users] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[Roles] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](max),
    CONSTRAINT [PK_dbo.Roles] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[Employees] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](max),
    [Phone] [nvarchar](max),
    [LastName] [nvarchar](max),
    [IdUser] [int] NOT NULL,
    CONSTRAINT [PK_dbo.Employees] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[Companies] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](max),
    [Email] [nvarchar](max),
    [Address] [nvarchar](max),
    [City] [nvarchar](max),
    [Phone] [nvarchar](max),
    CONSTRAINT [PK_dbo.Companies] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[Stuffs] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](max),
    [Description] [nvarchar](max),
    [StuffCategory_Id] [int],
    [StuffManufacture_Id] [int],
    CONSTRAINT [PK_dbo.Stuffs] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_StuffCategory_Id] ON [dbo].[Stuffs]([StuffCategory_Id])
CREATE INDEX [IX_StuffManufacture_Id] ON [dbo].[Stuffs]([StuffManufacture_Id])
CREATE TABLE [dbo].[StuffCategories] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](max),
    CONSTRAINT [PK_dbo.StuffCategories] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[StuffManufactures] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](max),
    CONSTRAINT [PK_dbo.StuffManufactures] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[RoleUsers] (
    [Role_Id] [int] NOT NULL,
    [User_Id] [int] NOT NULL,
    CONSTRAINT [PK_dbo.RoleUsers] PRIMARY KEY ([Role_Id], [User_Id])
)
CREATE INDEX [IX_Role_Id] ON [dbo].[RoleUsers]([Role_Id])
CREATE INDEX [IX_User_Id] ON [dbo].[RoleUsers]([User_Id])
ALTER TABLE [dbo].[Stuffs] ADD CONSTRAINT [FK_dbo.Stuffs_dbo.StuffCategories_StuffCategory_Id] FOREIGN KEY ([StuffCategory_Id]) REFERENCES [dbo].[StuffCategories] ([Id])
ALTER TABLE [dbo].[Stuffs] ADD CONSTRAINT [FK_dbo.Stuffs_dbo.StuffManufactures_StuffManufacture_Id] FOREIGN KEY ([StuffManufacture_Id]) REFERENCES [dbo].[StuffManufactures] ([Id])
ALTER TABLE [dbo].[RoleUsers] ADD CONSTRAINT [FK_dbo.RoleUsers_dbo.Roles_Role_Id] FOREIGN KEY ([Role_Id]) REFERENCES [dbo].[Roles] ([Id]) ON DELETE CASCADE
ALTER TABLE [dbo].[RoleUsers] ADD CONSTRAINT [FK_dbo.RoleUsers_dbo.Users_User_Id] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
CREATE TABLE [dbo].[__MigrationHistory] (
    [MigrationId] [nvarchar](255) NOT NULL,
    [Model] [varbinary](max) NOT NULL,
    [ProductVersion] [nvarchar](32) NOT NULL,
    CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY ([MigrationId])
)
BEGIN TRY
    EXEC sp_MS_marksystemobject 'dbo.__MigrationHistory'
END TRY
BEGIN CATCH
END CATCH
INSERT INTO [__MigrationHistory] ([MigrationId], [Model], [ProductVersion]) VALUES ('201312041243126_InitialMigration', 0x1F8B0800000000000400ED5CDD6EE3B815BE2FD0771074D516582BC9EEC5EEC0D9C58C931441C793419CD95E0E188976B82B51AA48A5F1B3F5A28FD4575852BFFC97642BB133CD4D1053E4C7F3C743EA1C1EFDEF3FFF9DFFF294C4DE23CC094AF1B97F3A3BF13D88C334427873EE1774FDDD8FFE2F3FFFF94FF3CB2879F27E6DFA7DCFFBB191989CFB0F9466EF8280840F30016496A0304F49BAA6B3304D0210A5C1D9C9C94FC1E969001984CFB03C6F7E5B608A1258FE603F17290E61460B102FD308C6A46E674F5625AAF7092490642084E7FE3FE1FD0A51B8FC2D9F5DAED9C035DA1439A08CAC25DA54FF5C000A7CEF7D8C00236F05E3B5EF653FBCFB42E08AE629DEAC32D60BC477DB0CB2E76B10135833F32EFB61283F27679C9F00609CD272D29DE4E1B79C325E2F994CE8969355F27BEE338A73B107EBF30FB8951A58D3E73CCD604EB7B7705D8FBB8E7C2F90C705EAC0769830864FCDFEC3F4FB33DFFB54C431B88F612B2126C2154D73F877882193338C3E034A618EF9585892AECDAACCC1FF36B330553013F3BD2BF404A38F106FE8433BD3123C352DEC5FDFFB8211B3483688E6051429AB7EBB27FD08083DC8C4970940F18BCFBA62D65890D2729AA9D9029889CD9A66DD88D7D1224D3280B77D06E286E173E343E8E13320E4DF691EBDBC2A404C0FC6ED2D2490DEA5BF437C04245C3E6528AFBD64450C73D1F00E2570BC35928F6908DA75F5214D6308703FCC27F0883625090AE02D0320BE770BE3F229794059BD66F893AFDC6CD9E3AB3C4DF8EFAA7FD5FAF50EE41BC8559C6A8F566991870A15F3A0F3F04EBFCF71DEFCFE582BB46AB856E12E1A6ED468D070A3FC9D347C996471BA856F5ADEDBD73CA4F8FFE84C711D893BFB908D78B049369BFC9B45BEC663DFFB28CA21212F3EEFA254C337B8E807AF9B152DD6EBB755B3A74A2F200973948947C4039C144A5D2E98D43669BE359E18CA1E5F957EDDD1C1F0583B4398FA980E13BD842E012ED620A4450EFB6895BA1AC9157AB82816BBED75029245F8B67C9ECF3789BA7F93F334722E1249CA5D6CE59A5CC560D38515BFE008E6F1965127CA43666C09937B98371B7948D1231BF82B880BF6F3441383D4FB439C86BFC3A8ED7EEAEEFE050319FE4C67B8624D6C7C4F481AA2D2BB082FA8F57B933CDB258E3CFB4B54178E2ADF70BD65115394C52864323EF7FFA6D16E446B5FBA3BB44AF44EB4792070E166CEE4E26D7439FDBD187DE3678411FCBAF60815B7DB8764FC93D9EC740A21D8FD87915EE346328D284C9B8F0A2DED743B0BA45AF78B14538070BB7C96BFE5AB342E7867FE083E514D207CD80A5239F2D07911C9603596E5C175604A1B5CAD9D9EC14D80C104D0061FFA40DAB8AF06D13EE941285562A2A1368321C36BFB46465E941530044FB0102BA264450AA86032B2B29A4892D0418F33A91BAED363B6F4B7D6100C1ADF2C0E617C4D9BBA93CBCC0C60D47800D639EE75A283DDA8C043634D0E21B81CA78A241AD69472918CC7291AAB6B1DE35C771690C99DAA60F262192CA5E6E4D47AD02EA31B5429DD26F51B5872BFF325C832766C1272C1758BB7AA12C18BEF56E373AE49851184C4907A6DA96D67624754B081CA53EE18237885724279A6F91EF083DE224AB46EC3F78B664271DBD075D878AAA637FFBF1A3130293ED333CAA264AF18B3093B7D977C43C579E8C3CAF43C88416E38FD2F18DF09B6BD41B84657E77A717CD5321CA18BCE8A285DEB70A43A9627C2D44DC331C4B70311486C1F8E26246365115B76641756979115A1BAD6E1485D8A5544EA5A4748ABCC994A722A5BC6D32226404D5489CFF74117739BEE79C49E2334DE243C257D378D3ACE3C5056B3EA4702CD91285E5DF54A837C56753E790E9FA567434BE83E9F651E76689F7520ED74AF03CFA1217336739096EC430FAD29AB07A8920FD252AF9A0EB1433509415922B65DE540D6674C2D4E647B16EC01A6671D79AC9637C569A4CD138A286DE3709C2AEF27822C0C315B17C298757420BBAD5FA79EC36C0D59C312BBCF682DE38ED564A5B49E08243D382E850B618167D3BC2D2632D402ECE30F6D0987D49C14AC7836DD39823643D5E784F8D63528078DF4B788268CBADB9B4233DAF43A600A63F0D098E146A81C88D5653448432590494D5C44EDACA308AAE3743B12C4314612A406F946ABD41830DE71E9A93083764783401D51E21D252B634DA07347487C47124713C5B411A13227784D789ABC4D6A8F617C4AFB9102EB7B9990843481151942E9FB18920837AD2D191208C7674E6EF6FB2D4ACB3EA85DDA2DAECD4228D986791DF9EF2F47D352015517DF63627844114F03ACB684C26456D9E0BFE2458CD8A6DA7560DCA23524558CF0DC3F3B39F951295EDBA1B02C20248A8FB1BA0C71D67BAFFE8C2C0AA98E32D504F811E4E103C8FF9280A7BFEE7BB17B2F30E926F05E487A515729C67D0BB9760151CBB8F6E24B2DCDDA4F4842B9D52454E9255413C3EA6551115B0F7482B2A87BD4A7D9232F463A2637F10AAA7A8E495C06CB17EFEB1F8F7F968B69FA9DE1F197D21CB7194CB71F2AD52E7B6189152C07B1F263AF34396EA332148EEC79D452DF678DEE613094FA2E3310EDB5D4511C9371BCA2B28857293673087598B8ECA150BDAF254AD92FDF7692FD5E6F4641D8A437415D811C2EDDEDCABF8EBACB6D7CA61B9873AB04F122C584E600E96981CF39C221CA406CE4418F320D59535CBE2DAEFAE4026610F3C522B13764A221D1DA165D31C23E593C7391851603DDB9F8E1D0A6E1CEA31DDA3AFA43B02F60203D9928E5F27F6F85D5A916D4BEC1173086147ABCEC8CDF505F0012824877933CB0EB9A5BFBE050D7F82C1664B91438B9D5D8D2862337B9173616530A425098A62C93A2BE216319ACC0973416472E7672633197B3E9550CAAD2F46A366B315B95F638F7A3FB94A9B63A2E996B8E4C756ED6323713ACB114CA5A01E72C8033C10BA573FBD7C79926A81E9ACA8E8CF56AF6EA3913B8B904C859596762404DB4DB6672D450F515DF0DAABDB34EEC2C4C329998C57ABB473653335BF10B17C7A9AA325C05EBADFAD2C7BE82BA3783C198EF511D867D43D1A6ABFCD39273D70E726A1D435FDDA768DCF275A749596CAA049D2C9AEF3F68C70FB5BCEC25581C5196A85F0060FBAFF0D55AB6F913B4E920F8F5060C4369E76DFB5CE375DA9C00148A9A2E4A1C62092988D8B6FC3EA7885B387B1C4242CAAF5CD4DF6EB84CEE61748D6F0A9A1594B10C93FB58F202FC20E19ABFACBD94699EDF94915432050B8C4CC4F39937F84381E2EE1B155786208A05829F50EA0019D725E581B2CDB645FAA45D17B701D5E26B0F5677906DF20C8CDCE01510BE87A1D1D62F435962F30B04363948488DD18D673F99F945C9D3CF7F00D03A37BE77590000, '5.0.0.net45')
