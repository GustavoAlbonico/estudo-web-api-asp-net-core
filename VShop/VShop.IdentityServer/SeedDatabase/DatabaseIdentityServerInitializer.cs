using Duende.IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using VShop.IdentityServer.Configuration;
using VShopIdentityServer.Data;

namespace VShop.IdentityServer.SeedDatabase;

public class DatabaseIdentityServerInitializer(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager) :IDatabaseSeedInitializer
{
    public void InitializeSeedRoles()
    {
        //Se o Perfil Admin não existir então cria o perfil 
        if (!roleManager.RoleExistsAsync(IdentityConfiguration.Admin).Result)
        {
            //cria o perfil Admin
            IdentityRole roleAdmin = new IdentityRole();
            roleAdmin.Name = IdentityConfiguration.Admin;
            roleAdmin.NormalizedName = IdentityConfiguration.Admin.ToUpper();
            roleManager.CreateAsync(roleAdmin).Wait();
        }

        // se o perfil Client não existir então cria o perfil
        if (!roleManager.RoleExistsAsync(IdentityConfiguration.Client).Result)
        {
            //cria o perfil Client
            IdentityRole roleClient = new IdentityRole();
            roleClient.Name = IdentityConfiguration.Client;
            roleClient.NormalizedName = IdentityConfiguration.Client.ToUpper();
            roleManager.CreateAsync(roleClient).Wait();
        }
    }

    public void InitializeSeedUsers()
    {
        //se o usuario admin não existir cria o usuario , define a senha e atribui ao perfil
        if (userManager.FindByEmailAsync("admin1@com.br").Result == null)
        {
            //define os dados do usuário admin
            ApplicationUser admin = new ApplicationUser()
            {
                UserName = "admin1",
                NormalizedUserName = "ADMIN1",
                Email = "admin1@com.br",
                NormalizedEmail = "ADMIN1@COM.BR",
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumber = "+55 (11) 12345-6789",
                FirstName = "Usuario",
                LastName = "Admin1",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            //cria o usuário Admin e atribui a senha
            IdentityResult resultAdmin = userManager.CreateAsync(admin, "123@D456g").Result;
            if (resultAdmin.Succeeded)
            {
                //inclui o usuário admin ao perfil admin
                userManager.AddToRoleAsync(admin, IdentityConfiguration.Admin).Wait();

                //inclui as claims do usuário admin
                var adminClaims = userManager.AddClaimsAsync(admin, new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
                    new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                    new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin)
                }).Result;
            }
        }

        //se o usuario client não existir cria o usuario , define a senha e atribui ao perfil
        if (userManager.FindByEmailAsync("client1@com.br").Result == null)
        {
            //define os dados do usuário client
            ApplicationUser client = new ApplicationUser()
            {
                UserName = "client1",
                NormalizedUserName = "CLIENT1",
                Email = "client1@com.br",
                NormalizedEmail = "CLIENT1@COM.BR",
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumber = "+55 (11) 12345-6789",
                FirstName = "Usuario",
                LastName = "Client1",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            //cria o usuário Client e atribui a senha
            IdentityResult resultClient = userManager.CreateAsync(client, "123@D456g").Result;
            //inclui o usuário Client ao perfil Client
            if (resultClient.Succeeded)
            {
                userManager.AddToRoleAsync(client, IdentityConfiguration.Client).Wait();

                //adiciona as claims do usuário Client
                var clientClaims = userManager.AddClaimsAsync(client, new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
                    new Claim(JwtClaimTypes.GivenName, client.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, client.LastName),
                    new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client)
                }).Result;
            }
        }
    }

    public void InitializeSeedDatabase()
    {
        //Se o usuario Admin não existir então cria o usuario e a senha e o atribui ao perfil
        if (roleManager.FindByNameAsync(IdentityConfiguration.Admin).Result == null)
        {
            //cria o perfil Admin
            IdentityRole roleAdmin = new IdentityRole();
            roleAdmin.Name = IdentityConfiguration.Admin;
            roleAdmin.NormalizedName = IdentityConfiguration.Admin.ToUpper();
            roleManager.CreateAsync(roleAdmin).Wait();

            //define os dados do usuário admin
            ApplicationUser admin = new ApplicationUser()
            {
                UserName = "gustavo-admin",
                NormalizedUserName = "GUSTAVO-ADMIN",
                Email = "gustavo_admin@com.br",
                NormalizedEmail = "GUSTAVO_ADMIN@COM.BR",
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumber = "+55 (99) 12345-6789",
                FirstName = "Gustavo",
                LastName = "Admin",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            //cria o usuário Admin e atribui a senha
            IdentityResult resultAdmin = userManager.CreateAsync(admin, "123@D456g").Result;
            if (resultAdmin.Succeeded)
            {
                //inclui o usuário admin ao perfil admin
                userManager.AddToRoleAsync(admin, IdentityConfiguration.Admin).Wait();

                //inclui as claims do usuário admin
                var adminClaims = userManager.AddClaimsAsync(admin, new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
                    new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                    new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin)
                }).Result;
            }
        }

        // se o perfil Client não existir então cria o perfil, cria o usuario e atribui ao perfil
        if (roleManager.FindByNameAsync(IdentityConfiguration.Client).Result == null)
        {
            //cria o perfil Client
            IdentityRole roleClient = new IdentityRole();
            roleClient.Name = IdentityConfiguration.Client;
            roleClient.NormalizedName = IdentityConfiguration.Client.ToUpper();
            roleManager.CreateAsync(roleClient).Wait();

            //define os dados do usuário client
            ApplicationUser client = new ApplicationUser()
            {
                UserName = "gustavo-client",
                NormalizedUserName = "GUSTAVO-CLIENT",
                Email = "gustavo_client@com.br",
                NormalizedEmail = "GUSTAVO_CLIENT@COM.BR",
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumber = "+55 (11) 12345-6789",
                FirstName = "Gustavo",
                LastName = "Client",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            //cria o usuário Client e atribui a senha
            IdentityResult resultClient = userManager.CreateAsync(client, "123@D456g").Result;
            //inclui o usuário Client ao perfil Client
            if (resultClient.Succeeded)
            {
                userManager.AddToRoleAsync(client, IdentityConfiguration.Client).Wait();

                //adiciona as claims do usuário Client
                var clientClaims = userManager.AddClaimsAsync(client, new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
                    new Claim(JwtClaimTypes.GivenName, client.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, client.LastName),
                    new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client)
                }).Result;
            }
        }
    }
}
