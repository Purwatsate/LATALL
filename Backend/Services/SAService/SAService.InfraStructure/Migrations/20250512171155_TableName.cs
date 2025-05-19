using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAService.InfraStructure.Migrations
{
    /// <inheritdoc />
    public partial class TableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "app_user_token",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "app_user",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "app_user_role",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "app_user_login",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "app_user_claim",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "app_role",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "app_role_claim",
                newSchema: "dbo");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "dbo",
                table: "app_user_role",
                newName: "IX_app_user_role_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "dbo",
                table: "app_user_login",
                newName: "IX_app_user_login_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "dbo",
                table: "app_user_claim",
                newName: "IX_app_user_claim_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "dbo",
                table: "app_role_claim",
                newName: "IX_app_role_claim_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_app_user_token",
                schema: "dbo",
                table: "app_user_token",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_app_user",
                schema: "dbo",
                table: "app_user",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_app_user_role",
                schema: "dbo",
                table: "app_user_role",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_app_user_login",
                schema: "dbo",
                table: "app_user_login",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_app_user_claim",
                schema: "dbo",
                table: "app_user_claim",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_app_role",
                schema: "dbo",
                table: "app_role",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_app_role_claim",
                schema: "dbo",
                table: "app_role_claim",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_app_role_claim_app_role_RoleId",
                schema: "dbo",
                table: "app_role_claim",
                column: "RoleId",
                principalSchema: "dbo",
                principalTable: "app_role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_app_user_claim_app_user_UserId",
                schema: "dbo",
                table: "app_user_claim",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "app_user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_app_user_login_app_user_UserId",
                schema: "dbo",
                table: "app_user_login",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "app_user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_app_user_role_app_role_RoleId",
                schema: "dbo",
                table: "app_user_role",
                column: "RoleId",
                principalSchema: "dbo",
                principalTable: "app_role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_app_user_role_app_user_UserId",
                schema: "dbo",
                table: "app_user_role",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "app_user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_app_user_token_app_user_UserId",
                schema: "dbo",
                table: "app_user_token",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "app_user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_app_role_claim_app_role_RoleId",
                schema: "dbo",
                table: "app_role_claim");

            migrationBuilder.DropForeignKey(
                name: "FK_app_user_claim_app_user_UserId",
                schema: "dbo",
                table: "app_user_claim");

            migrationBuilder.DropForeignKey(
                name: "FK_app_user_login_app_user_UserId",
                schema: "dbo",
                table: "app_user_login");

            migrationBuilder.DropForeignKey(
                name: "FK_app_user_role_app_role_RoleId",
                schema: "dbo",
                table: "app_user_role");

            migrationBuilder.DropForeignKey(
                name: "FK_app_user_role_app_user_UserId",
                schema: "dbo",
                table: "app_user_role");

            migrationBuilder.DropForeignKey(
                name: "FK_app_user_token_app_user_UserId",
                schema: "dbo",
                table: "app_user_token");

            migrationBuilder.DropPrimaryKey(
                name: "PK_app_user_token",
                schema: "dbo",
                table: "app_user_token");

            migrationBuilder.DropPrimaryKey(
                name: "PK_app_user_role",
                schema: "dbo",
                table: "app_user_role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_app_user_login",
                schema: "dbo",
                table: "app_user_login");

            migrationBuilder.DropPrimaryKey(
                name: "PK_app_user_claim",
                schema: "dbo",
                table: "app_user_claim");

            migrationBuilder.DropPrimaryKey(
                name: "PK_app_user",
                schema: "dbo",
                table: "app_user");

            migrationBuilder.DropPrimaryKey(
                name: "PK_app_role_claim",
                schema: "dbo",
                table: "app_role_claim");

            migrationBuilder.DropPrimaryKey(
                name: "PK_app_role",
                schema: "dbo",
                table: "app_role");

            migrationBuilder.RenameTable(
                name: "app_user_token",
                schema: "dbo",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "app_user_role",
                schema: "dbo",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "app_user_login",
                schema: "dbo",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "app_user_claim",
                schema: "dbo",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "app_user",
                schema: "dbo",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "app_role_claim",
                schema: "dbo",
                newName: "AspNetRoleClaims");

            migrationBuilder.RenameTable(
                name: "app_role",
                schema: "dbo",
                newName: "AspNetRoles");

            migrationBuilder.RenameIndex(
                name: "IX_app_user_role_RoleId",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_app_user_login_UserId",
                table: "AspNetUserLogins",
                newName: "IX_AspNetUserLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_app_user_claim_UserId",
                table: "AspNetUserClaims",
                newName: "IX_AspNetUserClaims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_app_role_claim_RoleId",
                table: "AspNetRoleClaims",
                newName: "IX_AspNetRoleClaims_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
