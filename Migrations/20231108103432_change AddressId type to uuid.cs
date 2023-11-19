using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FoodDelivery.Migrations
{
    /// <inheritdoc />
    public partial class changeAddressIdtypetouuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "as_addr_obj",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    objectid = table.Column<int>(type: "integer", nullable: false),
                    objectguid = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    typename = table.Column<string>(type: "text", nullable: false),
                    level = table.Column<string>(type: "text", nullable: false),
                    isactive = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_as_addr_obj", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "as_adm_hierarchy",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    objectid = table.Column<int>(type: "integer", nullable: false),
                    parentobjid = table.Column<int>(type: "integer", nullable: false),
                    isactive = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_as_adm_hierarchy", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "as_houses",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    objectid = table.Column<int>(type: "integer", nullable: false),
                    objectguid = table.Column<Guid>(type: "uuid", nullable: false),
                    housenum = table.Column<string>(type: "text", nullable: false),
                    addnum1 = table.Column<string>(type: "text", nullable: true),
                    addnum2 = table.Column<string>(type: "text", nullable: true),
                    addtype1 = table.Column<int>(type: "integer", nullable: true),
                    addtype2 = table.Column<int>(type: "integer", nullable: true),
                    isactive = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_as_houses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "banned_tokens",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    token = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_banned_tokens", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "baskets",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    dish_id = table.Column<Guid>(type: "uuid", nullable: false),
                    count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_baskets", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "dish_raiting",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    dish_id = table.Column<Guid>(type: "uuid", nullable: false),
                    score = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dish_raiting", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "dishes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    is_vegetarian = table.Column<bool>(type: "boolean", nullable: false),
                    photo = table.Column<string>(type: "text", nullable: false),
                    category = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dishes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "order_to_dishes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    dish_id = table.Column<Guid>(type: "uuid", nullable: false),
                    count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_to_dishes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    delivery_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    address = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    full_name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    birth_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    gender = table.Column<string>(type: "text", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: false),
                    address_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "as_addr_obj");

            migrationBuilder.DropTable(
                name: "as_adm_hierarchy");

            migrationBuilder.DropTable(
                name: "as_houses");

            migrationBuilder.DropTable(
                name: "banned_tokens");

            migrationBuilder.DropTable(
                name: "baskets");

            migrationBuilder.DropTable(
                name: "dish_raiting");

            migrationBuilder.DropTable(
                name: "dishes");

            migrationBuilder.DropTable(
                name: "order_to_dishes");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
