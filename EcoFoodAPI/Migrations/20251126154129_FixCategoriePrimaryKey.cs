using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoFoodAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixCategoriePrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    IdCategorie = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NomCategorie = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.IdCategorie);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nom = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Prenom = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MotDePasse = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ville = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MoyenneRating = table.Column<decimal>(type: "decimal(3,2)", nullable: true),
                    TotalRatingsRecus = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    TotalRatingsDonnes = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    Role = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, defaultValue: "User")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IdUser);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Produits",
                columns: table => new
                {
                    IdProduit = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Titre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Prix = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    DateExpiration = table.Column<DateOnly>(type: "date", nullable: true),
                    ImageUrl = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Statut = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    IdCategorie = table.Column<int>(type: "int", nullable: false),
                    IdCategorieNavigationIdCategorie = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produits", x => x.IdProduit);
                    table.ForeignKey(
                        name: "FK_Produits_Categories_IdCategorieNavigationIdCategorie",
                        column: x => x.IdCategorieNavigationIdCategorie,
                        principalTable: "Categories",
                        principalColumn: "IdCategorie",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Produits_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    IdRating = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Note = table.Column<int>(type: "int", nullable: true),
                    Commentaire = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateRating = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IdFromUser = table.Column<int>(type: "int", nullable: false),
                    IdToUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.IdRating);
                    table.ForeignKey(
                        name: "FK_Ratings_Users_IdFromUser",
                        column: x => x.IdFromUser,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ratings_Users_IdToUser",
                        column: x => x.IdToUser,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    IdMessage = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Texte = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateEnvoi = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IdSender = table.Column<int>(type: "int", nullable: false),
                    IdReceiver = table.Column<int>(type: "int", nullable: false),
                    IdProduit = table.Column<int>(type: "int", nullable: false),
                    IdProduitNavigationIdProduit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.IdMessage);
                    table.ForeignKey(
                        name: "FK_Messages_Produits_IdProduitNavigationIdProduit",
                        column: x => x.IdProduitNavigationIdProduit,
                        principalTable: "Produits",
                        principalColumn: "IdProduit",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Users_IdReceiver",
                        column: x => x.IdReceiver,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_Users_IdSender",
                        column: x => x.IdSender,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    IdReservation = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Statut = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateReservation = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IdProduit = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    IdProduitNavigationIdProduit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.IdReservation);
                    table.ForeignKey(
                        name: "FK_Reservations_Produits_IdProduitNavigationIdProduit",
                        column: x => x.IdProduitNavigationIdProduit,
                        principalTable: "Produits",
                        principalColumn: "IdProduit",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_IdProduitNavigationIdProduit",
                table: "Messages",
                column: "IdProduitNavigationIdProduit");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_IdReceiver",
                table: "Messages",
                column: "IdReceiver");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_IdSender",
                table: "Messages",
                column: "IdSender");

            migrationBuilder.CreateIndex(
                name: "IX_Produits_IdCategorieNavigationIdCategorie",
                table: "Produits",
                column: "IdCategorieNavigationIdCategorie");

            migrationBuilder.CreateIndex(
                name: "IX_Produits_IdUser",
                table: "Produits",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_IdFromUser",
                table: "Ratings",
                column: "IdFromUser");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_IdToUser",
                table: "Ratings",
                column: "IdToUser");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_IdProduitNavigationIdProduit",
                table: "Reservations",
                column: "IdProduitNavigationIdProduit");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_IdUser",
                table: "Reservations",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Produits");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
