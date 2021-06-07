using Microsoft.EntityFrameworkCore.Migrations;

namespace ASM.Migrations
{
    public partial class ASM_NET104New : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChiTietHD",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Count = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false),
                    MaSP = table.Column<int>(nullable: false),
                    IDHoaDon = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietHD", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "HoaDon",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalPrice = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDon", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NHOM_SanPham",
                columns: table => new
                {
                    MaNhom = table.Column<int>(nullable: false),
                    TenNhom = table.Column<string>(unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NHOM_San__234F91CD46FE5B5D", x => x.MaNhom);
                });

            migrationBuilder.CreateTable(
                name: "SANPHAM",
                columns: table => new
                {
                    MaSP = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenSP = table.Column<string>(nullable: false),
                    DonGia = table.Column<decimal>(type: "money", nullable: false),
                    MoTa = table.Column<string>(maxLength: 150, nullable: true),
                    Nhom = table.Column<int>(nullable: false),
                    HinhAnh = table.Column<string>(unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SANPHAM__2725081C4E59260D", x => x.MaSP);
                    table.ForeignKey(
                        name: "FK__SANPHAM__Nhom__15502E78",
                        column: x => x.Nhom,
                        principalTable: "NHOM_SanPham",
                        principalColumn: "MaNhom",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SANPHAM_Nhom",
                table: "SANPHAM",
                column: "Nhom");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietHD");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "SANPHAM");

            migrationBuilder.DropTable(
                name: "NHOM_SanPham");
        }
    }
}
