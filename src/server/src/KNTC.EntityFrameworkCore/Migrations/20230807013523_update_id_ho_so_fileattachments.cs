using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KNTC.Migrations
{
    /// <inheritdoc />
    public partial class updateidhosofileattachments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql = @"
                        UPDATE ""KNTC"".""FileAttachments""
                        SET ""id_ho_so"" =
                            CASE
                                WHEN ""ComplainId"" IS NOT NULL THEN ""ComplainId""
                                ELSE ""DenounceId""
                            END
                        WHERE ""id_ho_so"" IS NULL;";
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sql = @"
                        UPDATE ""KNTC"".""FileAttachments""
                        SET ""id_ho_so"" = NULL";
            migrationBuilder.Sql(sql);
        }
    }
}